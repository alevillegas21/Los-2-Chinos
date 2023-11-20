
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
        public partial class Usuario : Form
        {
            public Usuario(string nombre)
            {
                InitializeComponent();
                lblmensajeusuario.Text = nombre;
            }
        }
    }


