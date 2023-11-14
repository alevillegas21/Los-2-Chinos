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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-B5MIU9C;Database=sistemadeventas;Trusted_Connection=true");
        public void logear(string usuario,string contraseña)
        {
            try {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT nombre,acceso FROM Usuario WHERE Usuario = @Usuario AND Contraseña = @Contraseña",conn);
                cmd.Parameters.AddWithValue("Usuario", usuario);
                cmd.Parameters.AddWithValue("Contraseña", contraseña);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][1].ToString() == "ADMIN")
                {
                    new FrmMenu(dt.Rows[0][0].ToString()).Show();
                }
                else if (dt.Rows[0][1].ToString() == "USUARIO")
                {
                    new Usuario(dt.Rows[0][0].ToString()).Show();
                }
                else
                {
                    MessageBox.Show("usuario y/o contraseña incorrecta");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
              conn.Close(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logear(this.textBox1.Text, this.textBox2.Text);
        }
    }
}
