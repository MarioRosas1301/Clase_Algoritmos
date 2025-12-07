using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clase_Algoritmos
{
    public partial class Form1 : Form
    {
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private async void btnGraficar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            g.Clear(Color.White);

            int x1 = int.Parse(txtX1.Text);
            int y1 = int.Parse(txtY1.Text);
            int x2 = int.Parse(txtX2.Text);
            int y2 = int.Parse(txtY2.Text);

            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);

            List<Point> puntos = new List<Point>();

            switch (cmbAlgoritmo.Text)
            {
                case "DDA":
                    puntos = await Algoritmos.DDA(p1, p2, pictureBox1.Width, pictureBox1.Height);
                    break;

                case "Bresenham":
                    puntos = await Algoritmos.Bresenham(p1, p2, pictureBox1.Width, pictureBox1.Height);
                    break;

                case "Punto Medio":
                    puntos = await Algoritmos.PuntoMedio(p1, p2, pictureBox1.Width, pictureBox1.Height);
                    break;
            }

            // animación
            foreach (var p in puntos)
            {
                g.FillEllipse(Brushes.Black, p.X, p.Y, 4, 4);
                listBox1.Items.Add($"({p.X},{p.Y})");
                await Task.Delay(30);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);

            txtX1.Clear();
            txtY1.Clear();
            txtX2.Clear();
            txtY2.Clear();

            listBox1.Items.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
