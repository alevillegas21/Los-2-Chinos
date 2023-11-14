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

namespace Los_2_Chinos
{
    public partial class Articulos : Form
    {
        public Articulos()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-B5MIU9C;Database=sistemadeventas;Trusted_Connection=true");

        private void CrargarDTG()
        {
            SqlDataAdapter comando = new SqlDataAdapter("SELECT * FROM Articulos", conn);
            DataTable tabla = new DataTable();
            comando.Fill(tabla);
            dtgArticulos.DataSource = tabla;
        }

        private void Articulos_Load(object sender, EventArgs e)
        {
            CrargarDTG();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro de agregar el Articulo?", "Confirmar Agregado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {
                conn.Open();
                // Resto del código para agregar el proveedor

                string Consulta = "INSERT INTO Articulos VALUES (@stock, @codigo_de_barra, @detalle, @precio_venta, @precio_compra, @presentacion)";
                SqlCommand comandoAgregar = new SqlCommand(Consulta, conn);
                comandoAgregar.Parameters.AddWithValue("@stock", txtStcok.Text);
                comandoAgregar.Parameters.AddWithValue("@codigo_de_barra", txtCodigo.Text);
                comandoAgregar.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                comandoAgregar.Parameters.AddWithValue("@precio_venta", txtVenta.Text);
                comandoAgregar.Parameters.AddWithValue("@precio_compra", txtCompra.Text);
                comandoAgregar.Parameters.AddWithValue("@presentacion", txtPresentacion.Text);

                comandoAgregar.ExecuteNonQuery();

                conn.Close();
                LIMPIAR();
                // Después de agregar el proveedor, recargamos los datos en el DataGridView.
                CrargarDTG();
                conn.Close();
            }
            else MessageBox.Show("El Proveedor no fue agregado");
        }
        private void LIMPIAR()
        {
            txtStcok.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtDetalle.Text = string.Empty;
            txtVenta.Text = string.Empty;
            txtCompra.Text = string.Empty;
            txtPresentacion.Text = string.Empty;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro de modificar el Articulo?", "Confirmar Agregado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {
                conn.Open();
                string Consulta = "UPDATE Articulos SET stock = @stock, codigo_de_barra = @codigo_de_barra, detalle = @detalle, precio_venta = @precio_venta, precio_compra = @precio_compra, presentacion = @presentacion WHERE idarticulo = @idarticulo";
                SqlCommand comandoModificar = new SqlCommand(Consulta, conn);
                comandoModificar.Parameters.AddWithValue("@idarticulo", txtID.Text);
                comandoModificar.Parameters.AddWithValue("@stock", txtStcok.Text);
                comandoModificar.Parameters.AddWithValue("@codigo_de_barra", txtCodigo.Text);
                comandoModificar.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                comandoModificar.Parameters.AddWithValue("@precio_venta", txtVenta.Text);
                comandoModificar.Parameters.AddWithValue("@precio_compra", txtCompra.Text);
                comandoModificar.Parameters.AddWithValue("@presentacion", txtPresentacion.Text);
                comandoModificar.ExecuteNonQuery();


                conn.Close();
                LIMPIAR();
                CrargarDTG();
            }
            else MessageBox.Show("El Articulo no fue modificado");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtStcok.Text = dtgArticulos.SelectedCells[6].Value.ToString();
                txtCodigo.Text = dtgArticulos.SelectedCells[5].Value.ToString();
                txtDetalle.Text = dtgArticulos.SelectedCells[1].Value.ToString();
                txtVenta.Text = dtgArticulos.SelectedCells[4].Value.ToString();
                txtCompra.Text = dtgArticulos.SelectedCells[3].Value.ToString();
                txtPresentacion.Text = dtgArticulos.SelectedCells[2].Value.ToString();
                txtID.Text = dtgArticulos.SelectedCells[0].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Consulta SQL para eliminar el proveedor por su ID
                string consultaEliminar = "DELETE FROM Articulos WHERE codigo_de_barra = @codigo_de_barra";

                // Crear el comando con la consulta y la conexión
                using (SqlCommand comandoEliminar = new SqlCommand(consultaEliminar, conn))
                {
                    // Añadir parámetro para el ID del proveedor
                    comandoEliminar.Parameters.AddWithValue("@codigo_de_barra", txtCodigo.Text);

                    // Ejecutar la consulta de eliminación
                    int filasAfectadas = comandoEliminar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show($"articulo con codigo {txtCodigo.Text} eliminado correctamente.");
                        CrargarDTG();
                    }
                    else
                    {
                        MessageBox.Show(($"No se encontró un codigo con numero {txtCodigo.Text}."));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(($"Error al intentar eliminar el Codigo: {ex.Message}"));
            }
        }
    }
}
