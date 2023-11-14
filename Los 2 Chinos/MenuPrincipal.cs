using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            catch(Exception) { }
            
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
            }catch(Exception) { }
            
        }
        private void CalcularTotalCarrito()
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
            txtTotal.Text = totalCarrito.ToString("C"); // Esto formatea el total como moneda
        }
    }
}
