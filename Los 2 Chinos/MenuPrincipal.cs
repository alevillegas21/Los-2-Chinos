﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using PdfSharp.Pdf;
using PdfSharp.Drawing;


namespace Los_2_Chinos
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-B5MIU9C;Database=sistemadeventas;Trusted_Connection=true");
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            CrargarDTG();
        }
        private void CrargarDTG()
        {
            SqlDataAdapter comando = new SqlDataAdapter("SELECT codigo_de_barra, precio_venta, presentacion, detalle,stock  FROM Articulos", conn);
            DataTable tabla = new DataTable();
            comando.Fill(tabla);
            dtgArticulosMenu.DataSource = tabla;
        }
        private const string rutaArchivoVentas = "ventas.csv";

        // Resto de tu código...



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el código de barras del TextBox
                string codigo_de_barra = txtCodigoDeBarra.Text;

                // Consultar la base de datos para obtener la información del artículo
                string query = $"SELECT codigo_de_barra, detalle, precio_venta, presentacion FROM Articulos WHERE codigo_de_barra = '{codigo_de_barra}'";

                SqlDataAdapter comando = new SqlDataAdapter(query, conn);
                DataTable tabla = new DataTable();
                comando.Fill(tabla);
                // Verificar si se encontró un artículo con el código de barras dado
                if (tabla.Rows.Count > 0)
                {
                    // Obtener la fila del artículo
                    DataRow fila = tabla.Rows[0];

                    // Verificar si el artículo ya está en el carrito
                    bool encontrado = false;

                    foreach (DataGridViewRow row in dtgCarrito.Rows)
                    {
                        // Verificar si la celda tiene un valor y es igual al código de barras
                        if (row.Cells["CodigoDeBarra"].Value != null &&
                            row.Cells["CodigoDeBarra"].Value.ToString().Trim().Equals(codigo_de_barra, StringComparison.OrdinalIgnoreCase))
                        {
                            // Si el artículo ya está en el carrito, sumar la cantidad
                            int cantidadExistente = int.Parse(row.Cells["Cantidad"].Value.ToString());
                            int nuevaCantidad = cantidadExistente + int.Parse(txtCantidad.Text);
                            row.Cells["Cantidad"].Value = nuevaCantidad;

                            encontrado = true;
                            break;
                        }
                    }
                    if (!encontrado)
                    {
                        // Si el artículo no está en el carrito, agregar una nueva fila
                        dtgCarrito.Rows.Add(fila["codigo_de_barra"], fila["detalle"], fila["precio_venta"], int.Parse(txtCantidad.Text));
                    }
                }
                txtCantidad.Text = "1";
                txtCodigoDeBarra.Text = string.Empty;
                CalcularTotalCarrito();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgArticulosMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCodigoDeBarra.Text = dtgArticulosMenu.SelectedCells[0].Value.ToString();
            }
            catch (Exception) { }

        }
        private void dtgCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgCarrito.Columns["Eliminar"].Index && e.RowIndex >= 0)
                {
                    // Reducir la cantidad en 1
                    int cantidadExistente = int.Parse(dtgCarrito.Rows[e.RowIndex].Cells["Cantidad"].Value.ToString());
                    if (cantidadExistente > 1)
                    {
                        dtgCarrito.Rows[e.RowIndex].Cells["Cantidad"].Value = cantidadExistente - 1;
                    }
                    else
                    {
                        // Si la cantidad es 1, eliminar la fila completa
                        dtgCarrito.Rows.RemoveAt(e.RowIndex);
                    }

                    CalcularTotalCarrito(); // Recalcular el total después de ajustar la cantidad
                }
            }
            catch (Exception) { }

        }
        private void RegistrarVenta()
        {
            conn.Open();

            string consulta = "INSERT INTO Venta (monto, fecha) VALUES (@monto, @fecha)";
            SqlCommand comandoVenta = new SqlCommand(consulta, conn);
            // Eliminar el símbolo de dólar y convertir el valor a decimal

            comandoVenta.Parameters.AddWithValue("@monto", txtTotal.Text); // Asegúrate de convertir el texto a decimal
            comandoVenta.Parameters.AddWithValue("@fecha", DateTime.Now);

            // Ejecutar la consulta de inserción
            comandoVenta.ExecuteNonQuery();

            conn.Close();
        }
        private decimal CalcularTotalCarrito()
        {
            decimal totalCarrito = 0;

            foreach (DataGridViewRow row in dtgCarrito.Rows)
            {
                if (row.Cells["PrecioVenta"].Value != null && row.Cells["Cantidad"].Value != null)
                {
                    decimal precioVenta = Convert.ToDecimal(row.Cells["PrecioVenta"].Value);
                    int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);

                    // Calcular el precio total por artículo y sumarlo al total del carrito
                    decimal precioTotal = precioVenta * cantidad;
                    totalCarrito += precioTotal;
                }
            }

            // Mostrar el total en algún lugar, por ejemplo, en un TextBox
            txtTotal.Text = totalCarrito.ToString(); // Esto formatea el total como moneda

            return totalCarrito; // Devolver el total calculado
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Resto del código para agregar la venta y obtener la información necesaria

                // Calcular el monto total de la venta
                decimal totalCarrito = CalcularTotalCarrito();

                // Registrar la venta en la base de datos
                RegistrarVenta();

                // Generar el PDF con la información de la venta
                GenerarTicketPDF(totalCarrito);

                // Limpiar el carrito después de registrar la venta
                dtgCarrito.Rows.Clear();
                txtTotal.Text = "0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerarTicketPDF(decimal totalVenta)
        {
            // Obtener la ruta del escritorio
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Crear un documento PDF
            PdfDocument document = new PdfDocument();

            // Añadir una página al documento
            PdfPage page = document.AddPage();

            // Obtener un objeto XGraphics para dibujar en la página
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Configurar la fuente y tamaño
            XFont fontTitulo = new XFont("Arial", 16, XFontStyle.Bold);
            XFont fontNormal = new XFont("Arial", 12, XFontStyle.Regular);

            // Dibujar en la página
            gfx.DrawString("Los Dos Chinos Supermercado", fontTitulo, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Avenida Siempre Viva 156", fontNormal, XBrushes.Black, new XRect(10, 30, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Fecha: {DateTime.Now}", fontNormal, XBrushes.Black, new XRect(10, 50, page.Width, page.Height), XStringFormats.TopLeft);

            // Agregar detalles de los productos y cantidades
            gfx.DrawString("Detalle del Ticket", fontTitulo, XBrushes.Black, new XRect(10, 70, page.Width, page.Height), XStringFormats.TopLeft);

            int yPos = 90; // Posición vertical inicial para los productos
            foreach (DataGridViewRow row in dtgCarrito.Rows)
            {
                if (row.Cells["Detalle"].Value != null && row.Cells["Cantidad"].Value != null && row.Cells["PrecioVenta"].Value != null)
                {
                    string detalleProducto = row.Cells["Detalle"].Value.ToString();
                    int cantidadProducto = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    decimal precioProducto = Convert.ToDecimal(row.Cells["PrecioVenta"].Value);

                    string lineaProducto = $"{detalleProducto} - Cantidad: {cantidadProducto} - Precio Unitario: {precioProducto:C}";
                    gfx.DrawString(lineaProducto, fontNormal, XBrushes.Black, new XRect(10, yPos, page.Width, page.Height), XStringFormats.TopLeft);

                    yPos += 20; // Incrementar la posición vertical para el próximo producto
                }
            }

            // Agregar el total
            gfx.DrawString($"Monto Total: {totalVenta:C}", fontNormal, XBrushes.Black, new XRect(10, yPos, page.Width, page.Height), XStringFormats.TopLeft);

            // Obtener un nombre único para el archivo PDF
            string nombreArchivo = $"ticket_venta_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            // Guardar el documento en un archivo en la carpeta del escritorio
            string rutaPDF = Path.Combine(escritorio, "VentasPDF", nombreArchivo);
            document.Save(rutaPDF);

            MessageBox.Show("Ticket generado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }

}



    
