using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clase_Algoritmos
{
    public partial class AlgoritmoCirculos : Form
    {

        private ComboBox comboAlgoritmos;
        private Button btnEjecutar;
        private TextBox txtXmin, txtXmax, txtYmin, txtYmax;
        private Label lblXmin, lblXmax, lblYmin, lblYmax;

        private Button btnPintarRecorte;

        private Point lastCircleCenter;
        private int lastCircleRadius = 0;

        // Polígono dibujado en el formulario
        private List<PointF> polygon = new List<PointF>()
        {
            new PointF(100,100),
            new PointF(300,80),
            new PointF(350,200),
            new PointF(250,300),
            new PointF(120,280)
        };

        // Resultado del polígono luego del recorte
        private List<PointF> clippedPolygon = null;

        public AlgoritmoCirculos()
        {
            InitializeComponent();

            // Crear manualmente controles 
            CreateExtraControls();
        }

        // ============== Boton para dibujar circulo ============
        private async void btnDibujar_Click(object sender, EventArgs e)
        {
            // Validar que el radio sea un número entero positivo
            if (!int.TryParse(txtRadio.Text, out int r) || r <= 0)
            {
                MessageBox.Show("Ingrese un radio válido.");
                return;
            }

            // Limpiar el PictureBox antes de dibujar
            pictureBox1.Refresh();
            pictureBox1.Image = null;

            int xc = pictureBox1.Width / 2;
            int yc = pictureBox1.Height / 2;

            // Dibuja el circulo
            await CircleMidPoint(xc, yc, r);

            lastCircleCenter = new Point(xc, yc);
            lastCircleRadius = r;
        }

        // Algoritmo MidPoint para dibujar un círculo pixel por pixel
        private async Task CircleMidPoint(int xc, int yc, int r)
        {
            int x = 0;
            int y = r;
            int p = 1 - r;

            // Dibujar puntos simétricos
            await PlotPoint(xc, yc, x, y);

            // Algoritmo clásico usando simetría de octantes
            while (x < y)
            {
                x++;

                if (p < 0)
                    p += 2 * x + 3;
                else
                {
                    y--;
                    p += 2 * (x - y) + 5;
                }

                await PlotPoint(xc, yc, x, y);
            }
        }

        // Dibuja los 8 puntos simétricos calculados
        private async Task PlotPoint(int xc, int yc, int x, int y)
        {
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                // Cada FillRectangle representa un píxel dibujado
                g.FillRectangle(Brushes.Black, xc + x, yc + y, 2, 2);
                g.FillRectangle(Brushes.Black, xc - x, yc + y, 2, 2);
                g.FillRectangle(Brushes.Black, xc + x, yc - y, 2, 2);
                g.FillRectangle(Brushes.Black, xc - x, yc - y, 2, 2);

                g.FillRectangle(Brushes.Black, xc + y, yc + x, 2, 2);
                g.FillRectangle(Brushes.Black, xc - y, yc + x, 2, 2);
                g.FillRectangle(Brushes.Black, xc + y, yc - x, 2, 2);
                g.FillRectangle(Brushes.Black, xc - y, yc - x, 2, 2);
            }

            await Task.Delay(10);
        }

        // =================== Boton de limpiar =================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia la pantalla y el último círculo registrado
            pictureBox1.Image = null;
            pictureBox1.Refresh();
            txtRadio.Clear();

            lastCircleRadius = 0;
            lastCircleCenter = Point.Empty;
            clippedPolygon = null;
        }

        // ============== Creacion de controles ===========
        private void CreateExtraControls()
        {
            // Menu para escoger algoritmo

            comboAlgoritmos = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(550, 190),
                Size = new Size(180, 24)
            };
            comboAlgoritmos.Items.AddRange(new object[]
            {
                "Relleno Scanline",
                "Flood Fill",
                "Boundary Fill",
                "Recorte Sutherland-Hodgman"
            });
            this.Controls.Add(comboAlgoritmos);

            // Label informativo
            Label lblCombo = new Label()
            {
                Text = "Seleccione algoritmo:",
                Location = new Point(550, 170),
                AutoSize = true
            };
            this.Controls.Add(lblCombo);

            // Botón para ejecutar algoritmo
            btnEjecutar = new Button()
            {
                Text = "Ejecutar",
                Location = new Point(550, 220),
                Size = new Size(180, 30)
            };
            btnEjecutar.Click += BtnEjecutar_Click;
            this.Controls.Add(btnEjecutar);

            // Botón para pintar el polígono recortado
            btnPintarRecorte = new Button()
            {
                Text = "Pintar Polígono Recortado",
                Location = new Point(550, 255),
                Size = new Size(180, 30)
            };
            btnPintarRecorte.Click += BtnPintarRecorte_Click;
            this.Controls.Add(btnPintarRecorte);

            //Coordenadas de la ventana de recorte

            int xbase = 550;
            int ybase = 300;
            int w = 80;
            int h = 22;

            lblXmin = new Label() { Text = "xmin:", Location = new Point(xbase, ybase), AutoSize = true };
            txtXmin = new TextBox() { Location = new Point(xbase + 40, ybase - 3), Size = new Size(w, h), Text = "150" };

            lblXmax = new Label() { Text = "xmax:", Location = new Point(xbase, ybase + 30), AutoSize = true };
            txtXmax = new TextBox() { Location = new Point(xbase + 40, ybase + 27), Size = new Size(w, h), Text = "350" };

            lblYmin = new Label() { Text = "ymin:", Location = new Point(xbase, ybase + 60), AutoSize = true };
            txtYmin = new TextBox() { Location = new Point(xbase + 40, ybase + 57), Size = new Size(w, h), Text = "150" };

            lblYmax = new Label() { Text = "ymax:", Location = new Point(xbase, ybase + 90), AutoSize = true };
            txtYmax = new TextBox() { Location = new Point(xbase + 40, ybase + 87), Size = new Size(w, h), Text = "300" };

            // Añadir al formulario
            this.Controls.Add(lblXmin);
            this.Controls.Add(txtXmin);
            this.Controls.Add(lblXmax);
            this.Controls.Add(txtXmax);
            this.Controls.Add(lblYmin);
            this.Controls.Add(txtYmin);
            this.Controls.Add(lblYmax);
            this.Controls.Add(txtYmax);
        }

        // ======= Corre el algoritmo seleccionado ===========
        private void BtnEjecutar_Click(object sender, EventArgs e)
        {
            if (comboAlgoritmos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un algoritmo del menú desplegable.");
                return;
            }

            switch (comboAlgoritmos.SelectedIndex)
            {
                case 0:
                    // Relleno por scanline simple
                    FillCircleScanline();
                    break;

                case 1:
                    // Flood fill
                    if (lastCircleRadius == 0)
                    {
                        MessageBox.Show("Primero dibuja el círculo.");
                        return;
                    }
                    FloodFill(lastCircleCenter.X, lastCircleCenter.Y);
                    break;

                case 2:
                    // Boundary fill
                    if (lastCircleRadius == 0)
                    {
                        MessageBox.Show("Primero dibuja el círculo.");
                        return;
                    }
                    BoundaryFill(lastCircleCenter.X, lastCircleCenter.Y, Color.Red, Color.Black);
                    break;

                case 3:
                    // Recorte Sutherland-Hodgman para polígonos
                    if (!ValidateClipWindow(out RectangleF win))
                        return;
                    ClipPolygonWithWindow(win);
                    break;
            }
        }

        // =================== Scanline Fill ===================
        private void FillCircleScanline()
        {
            if (lastCircleRadius == 0) { MessageBox.Show("Dibuja primero el círculo."); return; }

            // Capturar lo dibujado en un bitmap
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            int xc = lastCircleCenter.X;
            int yc = lastCircleCenter.Y;
            int r = lastCircleRadius;

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Recorrer filas (dy) y dibujar líneas horizontales
                for (int dy = -r; dy <= r; dy++)
                {
                    int dx = (int)Math.Floor(Math.Sqrt(r * r - dy * dy));
                    int x1 = xc - dx;
                    int x2 = xc + dx;

                    g.DrawLine(Pens.Red, x1, yc + dy, x2, yc + dy);
                }
            }

            pictureBox1.Image = bmp;
        }

        // ===================== Flood Fill ====================
        private void FloodFill(int startX, int startY)
        {
            if (lastCircleRadius == 0)
            {
                MessageBox.Show("Primero dibuja el círculo.");
                return;
            }

            // Obtener límites del círculo para evitar salirnos del borde
            int xc = lastCircleCenter.X;
            int yc = lastCircleCenter.Y;
            int r = lastCircleRadius;
            int r2 = r * r;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            Color fillColor = Color.Red;

            Stack<Point> stk = new Stack<Point>();
            stk.Push(new Point(startX, startY));

            // Flood fill no recursivo
            while (stk.Count > 0)
            {
                Point p = stk.Pop();

                // Validar que estemos dentro del bitmap
                if (p.X < 0 || p.Y < 0 || p.X >= bmp.Width || p.Y >= bmp.Height)
                    continue;

                // Limitar estrictamente al círculo
                if ((p.X - xc) * (p.X - xc) + (p.Y - yc) * (p.Y - yc) > r2)
                    continue;

                if (bmp.GetPixel(p.X, p.Y).ToArgb() != fillColor.ToArgb())
                {
                    // Pintar y añadir vecinos
                    bmp.SetPixel(p.X, p.Y, fillColor);

                    stk.Push(new Point(p.X + 1, p.Y));
                    stk.Push(new Point(p.X - 1, p.Y));
                    stk.Push(new Point(p.X, p.Y + 1));
                    stk.Push(new Point(p.X, p.Y - 1));
                }
            }

            pictureBox1.Image = bmp;
        }

        // ===================== Boundary Fill =================
        private void BoundaryFill(int startX, int startY, Color fillColor, Color boundaryColor)
        {
            if (lastCircleRadius == 0)
            {
                MessageBox.Show("Primero dibuja el círculo.");
                return;
            }

            // Información del círculo
            int xc = lastCircleCenter.X;
            int yc = lastCircleCenter.Y;
            int r = lastCircleRadius;
            int r2 = r * r;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            Stack<Point> stk = new Stack<Point>();
            stk.Push(new Point(startX, startY));

            // Boundary fill clásico usando pila
            while (stk.Count > 0)
            {
                Point p = stk.Pop();

                if (p.X < 0 || p.Y < 0 || p.X >= bmp.Width || p.Y >= bmp.Height)
                    continue;

                // Evitar que el algoritmo se salga del círculo
                if ((p.X - xc) * (p.X - xc) + (p.Y - yc) * (p.Y - yc) > r2)
                    continue;

                Color actual = bmp.GetPixel(p.X, p.Y);

                // Solo pintar si no es borde ni color de relleno
                if (actual.ToArgb() != boundaryColor.ToArgb() &&
                    actual.ToArgb() != fillColor.ToArgb())
                {
                    bmp.SetPixel(p.X, p.Y, fillColor);

                    stk.Push(new Point(p.X + 1, p.Y));
                    stk.Push(new Point(p.X - 1, p.Y));
                    stk.Push(new Point(p.X, p.Y + 1));
                    stk.Push(new Point(p.X, p.Y - 1));
                }
            }

            pictureBox1.Image = bmp;
        }

        // ========= Validacion de la ventana de recorte ===========
        private bool ValidateClipWindow(out RectangleF window)
        {
            window = new RectangleF();

            // Validar campos numéricos
            if (!float.TryParse(txtXmin.Text, out float xmin) ||
                !float.TryParse(txtXmax.Text, out float xmax) ||
                !float.TryParse(txtYmin.Text, out float ymin) ||
                !float.TryParse(txtYmax.Text, out float ymax))
            {
                MessageBox.Show("Ingrese valores válidos para la ventana de recorte.");
                return false;
            }

            // Validación geométrica básica
            if (xmax <= xmin || ymax <= ymin)
            {
                MessageBox.Show("La ventana debe cumplir xmax > xmin y ymax > ymin.");
                return false;
            }

            // Crear rectángulo
            window = new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
            return true;
        }

        private void AlgoritmoCirculos_Load(object sender, EventArgs e)
        {

        }

        // ============ Recorte Sutherland–Hodgman ===============
        private void ClipPolygonWithWindow(RectangleF win)
        {
            // Polígono original
            List<PointF> output = polygon;

            // Recortar contra cada borde del rectángulo (en sentido horario)
            output = ClipEdge(output, new PointF(win.Left, win.Top), new PointF(win.Right, win.Top));       // Borde superior
            output = ClipEdge(output, new PointF(win.Right, win.Top), new PointF(win.Right, win.Bottom));   // Derecha
            output = ClipEdge(output, new PointF(win.Right, win.Bottom), new PointF(win.Left, win.Bottom)); // Inferior
            output = ClipEdge(output, new PointF(win.Left, win.Bottom), new PointF(win.Left, win.Top));     // Izquierda

            // Guardar resultado para ser repintado con botón extra
            clippedPolygon = output;

            // Dibujar todo en nuevo bitmap
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Dibujar polígono original en negro (con etiquetas)
                if (polygon.Count > 1)
                    DrawPolygonWithLabels(g, polygon, Pens.Black, Brushes.Black);

                // Dibujar ventana azul
                g.DrawRectangle(Pens.Blue, win.X, win.Y, win.Width, win.Height);

                // Dibujar polígono recortado (solo borde)
                if (output.Count > 1)
                    g.DrawPolygon(Pens.Green, output.ToArray());
            }

            pictureBox1.Image = bmp;
        }

        // ======= Dibujar poligono con vertices ==========
        private void DrawPolygonWithLabels(Graphics g, List<PointF> poly, Pen pen, Brush labelBrush)
        {
            // Dibujar el borde del polígono
            if (poly.Count > 1)
                g.DrawPolygon(pen, poly.ToArray());

            // Calcular el centro aproximado para desplazar números hacia adentro
            float cx = 0, cy = 0;
            foreach (var pt in poly)
            {
                cx += pt.X;
                cy += pt.Y;
            }
            cx /= poly.Count;
            cy /= poly.Count;

            // Dibujar número de cada vértice
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            {
                for (int i = 0; i < poly.Count; i++)
                {
                    var pt = poly[i];

                    // Vector hacia el centro
                    float vx = cx - pt.X;
                    float vy = cy - pt.Y;

                    float len = (float)Math.Sqrt(vx * vx + vy * vy);
                    float dx = 0, dy = 0;

                    if (len > 0.001f)
                    {
                        // Desplazar etiqueta hacia adentro
                        dx = vx / len * 8f;
                        dy = vy / len * 8f;
                    }

                    float lx = pt.X + dx - 6;
                    float ly = pt.Y + dy - 6;

                    var rect = new RectangleF(lx - 2, ly - 2, 16, 16);

                    g.FillRectangle(Brushes.White, rect);
                    g.DrawString((i + 1).ToString(), font, labelBrush, lx, ly);
                }
            }
        }

        // =========== Pintar poligono recortado ============
        private void BtnPintarRecorte_Click(object sender, EventArgs e)
        {
            if (clippedPolygon == null)
            {
                MessageBox.Show("Primero genere el recorte (Ejecutar con la opción Recorte).");
                return;
            }

            if (clippedPolygon.Count == 0)
            {
                MessageBox.Show("El polígono recortado está vacío (no hay intersección).");
                return;
            }

            // Respetar lo que ya está dibujado
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Pintar con azul 
                Color azulClaro = Color.FromArgb(128, 150, 200, 255);
                using (Brush b = new SolidBrush(azulClaro))
                {
                    g.FillPolygon(b, clippedPolygon.ToArray());
                }

                // Borde azul para visibilidad
                g.DrawPolygon(Pens.Blue, clippedPolygon.ToArray());
            }

            pictureBox1.Image = bmp;
        }

        // ========= Funciones del recorte ============
        private List<PointF> ClipEdge(List<PointF> poly, PointF A, PointF B)
        {
            List<PointF> outList = new List<PointF>();

            // Recorrer cada par de vértices consecutivos P -> Q
            for (int i = 0; i < poly.Count; i++)
            {
                PointF P = poly[i];
                PointF Q = poly[(i + 1) % poly.Count];

                bool Pin = Inside(P, A, B);
                bool Qin = Inside(Q, A, B);

                // Casos del algoritmo Sutherland-Hodgman
                if (Pin && Qin)
                    outList.Add(Q);

                else if (Pin && !Qin)
                    outList.Add(Intersect(P, Q, A, B));

                else if (!Pin && Qin)
                {
                    outList.Add(Intersect(P, Q, A, B));
                    outList.Add(Q);
                }
            }

            return outList;
        }

        // Determina si un punto está "dentro" según el lado del borde
        private bool Inside(PointF p, PointF A, PointF B)
        {
            // Multiplicación cruzada para determinar lado del borde
            return ((B.X - A.X) * (p.Y - A.Y) - (B.Y - A.Y) * (p.X - A.X)) >= 0;
        }

        // Intersección de dos líneas (P->Q y A->B)
        private PointF Intersect(PointF P, PointF Q, PointF A, PointF B)
        {
            float A1 = Q.Y - P.Y;
            float B1 = P.X - Q.X;
            float C1 = A1 * P.X + B1 * P.Y;

            float A2 = B.Y - A.Y;
            float B2 = A.X - B.X;
            float C2 = A2 * A.X + B2 * A.Y;

            float det = A1 * B2 - A2 * B1;

            if (Math.Abs(det) < 0.0001f)
                return P;

            float x = (B2 * C1 - B1 * C2) / det;
            float y = (A1 * C2 - A2 * C1) / det;

            return new PointF(x, y);
        }
    }
}
