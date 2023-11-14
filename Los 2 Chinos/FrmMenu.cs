using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Los_2_Chinos
{
    public partial class FrmMenu : Form
    {
        public FrmMenu(string Nombre)
        {
            InitializeComponent();
            lblNombre.Text = Nombre;
        }

        private Form formularioActual = null;  // Variable para mantener referencia al formulario actual
        private void AbrirFormEnPanel(Form form)
        {
            if (formularioActual != null)
            {
                // Cerrar el formulario actual si existe
                formularioActual.Close();
            }

            // Establecer las propiedades necesarias para el nuevo formulario
            form.TopLevel = false;
            form.TopMost = false;
            form.FormBorderStyle = FormBorderStyle.None;  // Quitar bordes del formulario
            form.Dock = DockStyle.Fill;  // Establecer el formulario para llenar completamente el área del contenedor

            // Asignar el nuevo formulario como control secundario del panel
            PanelMenu.Controls.Add(form);

            // Mostrar el nuevo formulario dentro del panel
            form.Show();

            // Actualizar la referencia al formulario actual
            formularioActual = form;
        }
        private void btnArticulos_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario que deseas abrir
            Articulos formArticulos = new Articulos();

            // Llamar al método para abrir el formulario en el panel
            AbrirFormEnPanel(formArticulos);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario que deseas abrir
            Proveedores formProveedores = new Proveedores();

            // Llamar al método para abrir el formulario en el panel
            AbrirFormEnPanel(formProveedores);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario que deseas abrir
            MenuPrincipal formmenu = new MenuPrincipal();

            // Llamar al método para abrir el formulario en el panel
            AbrirFormEnPanel(formmenu);
        }

        private void PanelMenu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
