using System.Windows.Forms;

namespace Clase_Algoritmos
{
    partial class AlgoritmoCirculos
    {
        private System.ComponentModel.IContainer components = null;

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox txtRadio;
        private Button btnDibujar;
        private Button btnLimpiar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRadio = new System.Windows.Forms.TextBox();
            this.btnDibujar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(20, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(497, 366);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(547, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Radio:";
            // 
            // txtRadio
            // 
            this.txtRadio.Location = new System.Drawing.Point(600, 30);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new System.Drawing.Size(120, 22);
            this.txtRadio.TabIndex = 2;
            // 
            // btnDibujar
            // 
            this.btnDibujar.Location = new System.Drawing.Point(550, 72);
            this.btnDibujar.Name = "btnDibujar";
            this.btnDibujar.Size = new System.Drawing.Size(170, 30);
            this.btnDibujar.TabIndex = 3;
            this.btnDibujar.Text = "Dibujar";
            this.btnDibujar.Click += new System.EventHandler(this.btnDibujar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(550, 124);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(170, 30);
            this.btnLimpiar.TabIndex = 4;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // AlgoritmoCirculos
            // 
            this.ClientSize = new System.Drawing.Size(749, 414);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRadio);
            this.Controls.Add(this.btnDibujar);
            this.Controls.Add(this.btnLimpiar);
            this.Name = "AlgoritmoCirculos";
            this.Text = "Algoritmo de Círculos";
            this.Load += new System.EventHandler(this.AlgoritmoCirculos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
