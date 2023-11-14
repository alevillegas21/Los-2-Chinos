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
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-B5MIU9C;Database=sistemadeventas;Trusted_Connection=true");
        private void Proveedores_Load(object sender, EventArgs e)
        {
            CrargarDTG();
        }
        #region METODOS
        private void CrargarDTG()
        {
            SqlDataAdapter comando = new SqlDataAdapter("SELECT * FROM Proveedor", conn);
            DataTable tabla = new DataTable();
            comando.Fill(tabla);
            dtgProveedor.DataSource = tabla;
        }
        private void LIMPIAR()
        {
            txtCelular.Text = string.Empty;
            txtCUIT.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtRubro.Text = string.Empty;
            txtProveedorID.Text = string.Empty;
        }
        private void dtgProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtProveedorID.Text = dtgProveedor.SelectedCells[0].Value.ToString();
                txtNombre.Text = dtgProveedor.SelectedCells[1].Value.ToString();
                txtCUIT.Text = dtgProveedor.SelectedCells[2].Value.ToString();
                txtEmail.Text = dtgProveedor.SelectedCells[3].Value.ToString();
                txtCelular.Text = dtgProveedor.SelectedCells[4].Value.ToString();
                txtRubro.Text = dtgProveedor.SelectedCells[5].Value.ToString();
                txtDireccion.Text = dtgProveedor.SelectedCells[6].Value.ToString();
            }
            catch (Exception)
            {

            }

        }
        #endregion
        #region BTN AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro de agregar el proveedor?", "Confirmar Agregado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {
                conn.Open();
                // Resto del código para agregar el proveedor

                string Consulta = "INSERT INTO Proveedor VALUES (@Nombre, @CUIT, @Email, @Celular, @Rubro, @Direccion)";
                SqlCommand comandoAgregar = new SqlCommand(Consulta, conn);
                comandoAgregar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                comandoAgregar.Parameters.AddWithValue("@CUIT", txtCUIT.Text);
                comandoAgregar.Parameters.AddWithValue("@Email", txtEmail.Text);
                comandoAgregar.Parameters.AddWithValue("@Celular", txtCelular.Text);
                comandoAgregar.Parameters.AddWithValue("@Rubro", txtRubro.Text);
                comandoAgregar.Parameters.AddWithValue("@Direccion", txtDireccion.Text);

                comandoAgregar.ExecuteNonQuery();

                conn.Close();
                LIMPIAR();
                // Después de agregar el proveedor, recargamos los datos en el DataGridView.
                CrargarDTG();
            }
            else MessageBox.Show("El Proveedor no fue agregado");
        }
        #endregion
        #region BTN MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo de confirmación
            DialogResult result = MessageBox.Show("¿Estás seguro de modificar el proveedor?", "Confirmar Agregado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {
                conn.Open();
                string Consulta = "UPDATE Proveedor SET Nombre = @Nombre, CUIT = @CUIT, Email = @Email, Celular = @Celular, Rubro = @Rubro, Direccion = @Direccion WHERE ProveedorID = @ProveedorID";
                SqlCommand comandoModificar = new SqlCommand(Consulta, conn);
                comandoModificar.Parameters.AddWithValue("@ProveedorID", txtProveedorID.Text);
                comandoModificar.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                comandoModificar.Parameters.AddWithValue("@CUIT", txtCUIT.Text);
                comandoModificar.Parameters.AddWithValue("@Email", txtEmail.Text);
                comandoModificar.Parameters.AddWithValue("@Celular", txtCelular.Text);
                comandoModificar.Parameters.AddWithValue("@Rubro", txtRubro.Text);
                comandoModificar.Parameters.AddWithValue("@Direccion", txtDireccion.Text);

                comandoModificar.ExecuteNonQuery();

                conn.Close();
                LIMPIAR();
                CrargarDTG();
            }
            else MessageBox.Show("El Proveedor no fue agregado");
        }
        #endregion

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Consulta SQL para eliminar el proveedor por su ID
                string consultaEliminar = "DELETE FROM Proveedor WHERE Nombre = @Nombre";

                // Crear el comando con la consulta y la conexión
                using (SqlCommand comandoEliminar = new SqlCommand(consultaEliminar, conn))
                {
                    // Añadir parámetro para el ID del proveedor
                    comandoEliminar.Parameters.AddWithValue("@Nombre", txtNombre.Text);

                    // Ejecutar la consulta de eliminación
                    int filasAfectadas = comandoEliminar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show($"Proveedor con Nombre {txtNombre.Text} eliminado correctamente.");
                        CrargarDTG();
                    }
                    else
                    {
                        MessageBox.Show(($"No se encontró un Nombre con ID {txtNombre.Text}."));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(($"Error al intentar eliminar el Nombre: {ex.Message}"));
            }
            LIMPIAR();
            conn.Close();
        }
    }
}
