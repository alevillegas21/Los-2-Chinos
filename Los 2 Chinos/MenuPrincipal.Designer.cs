namespace Los_2_Chinos
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtgArticulosMenu = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dtgCarrito = new System.Windows.Forms.DataGridView();
            this.CodigoDeBarra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtCodigoDeBarra = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgArticulosMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCarrito)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgArticulosMenu
            // 
            this.dtgArticulosMenu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgArticulosMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgArticulosMenu.Location = new System.Drawing.Point(804, 134);
            this.dtgArticulosMenu.Name = "dtgArticulosMenu";
            this.dtgArticulosMenu.RowHeadersWidth = 51;
            this.dtgArticulosMenu.RowTemplate.Height = 24;
            this.dtgArticulosMenu.Size = new System.Drawing.Size(687, 221);
            this.dtgArticulosMenu.TabIndex = 0;
            this.dtgArticulosMenu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgArticulosMenu_CellClick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(131)))), ((int)(((byte)(209)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(996, 90);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(316, 41);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Seleccionar Articulo";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtgCarrito
            // 
            this.dtgCarrito.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgCarrito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCarrito.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoDeBarra,
            this.Detalle,
            this.PrecioVenta,
            this.Cantidad,
            this.Eliminar});
            this.dtgCarrito.Location = new System.Drawing.Point(30, 12);
            this.dtgCarrito.Name = "dtgCarrito";
            this.dtgCarrito.RowHeadersWidth = 51;
            this.dtgCarrito.RowTemplate.Height = 24;
            this.dtgCarrito.Size = new System.Drawing.Size(431, 486);
            this.dtgCarrito.TabIndex = 2;
            this.dtgCarrito.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgCarrito_CellContentClick);
            // 
            // CodigoDeBarra
            // 
            this.CodigoDeBarra.HeaderText = "CodigoDeBarras";
            this.CodigoDeBarra.MinimumWidth = 6;
            this.CodigoDeBarra.Name = "CodigoDeBarra";
            // 
            // Detalle
            // 
            this.Detalle.HeaderText = "Detalle";
            this.Detalle.MinimumWidth = 6;
            this.Detalle.Name = "Detalle";
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio";
            this.PrecioVenta.MinimumWidth = 6;
            this.PrecioVenta.Name = "PrecioVenta";
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.MinimumWidth = 6;
            this.Eliminar.Name = "Eliminar";
            // 
            // txtCodigoDeBarra
            // 
            this.txtCodigoDeBarra.Location = new System.Drawing.Point(996, 403);
            this.txtCodigoDeBarra.Multiline = true;
            this.txtCodigoDeBarra.Name = "txtCodigoDeBarra";
            this.txtCodigoDeBarra.Size = new System.Drawing.Size(200, 26);
            this.txtCodigoDeBarra.TabIndex = 3;
            this.txtCodigoDeBarra.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(1362, 407);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(44, 22);
            this.txtCantidad.TabIndex = 4;
            this.txtCantidad.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1255, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cantidad:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(125, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "TOTAL:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(215, 534);
            this.txtTotal.Multiline = true;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(200, 26);
            this.txtTotal.TabIndex = 3;
            this.txtTotal.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(844, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Codigo Barra:";
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(131)))), ((int)(((byte)(209)))));
            this.ClientSize = new System.Drawing.Size(1503, 817);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtCodigoDeBarra);
            this.Controls.Add(this.dtgCarrito);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dtgArticulosMenu);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgArticulosMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgArticulosMenu;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dtgCarrito;
        private System.Windows.Forms.TextBox txtCodigoDeBarra;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoDeBarra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Detalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
    }
}