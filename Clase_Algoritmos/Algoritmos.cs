using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Clase_Algoritmos
{
    internal class Algoritmos
    {
        // GENERA ESCALADO PARA QUE SIEMPRE SE VEA LA LÍNEA
        private static Point ScalePoint(Point p, int width, int height)
        {
            int s = 20; // FACTOR DE ESCALA PARA VALORES PEQUEÑOS

            return new Point(p.X * s, height - (p.Y * s));
        }


        // ======== DDA =========
        public static async Task<List<Point>> DDA(Point p1, Point p2, int w, int h)
        {
            List<Point> puntos = new List<Point>();

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            float pasos = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xinc = dx / pasos;
            float yinc = dy / pasos;

            float x = p1.X;
            float y = p1.Y;

            for (int k = 0; k <= pasos; k++)
            {
                puntos.Add(ScalePoint(new Point((int)Math.Round(x), (int)Math.Round(y)), w, h));
                x += xinc;
                y += yinc;
            }

            return puntos;
        }

        // ========= BRESENHAM ==========

        public static async Task<List<Point>> Bresenham(Point p1, Point p2, int w, int h)
        {
            List<Point> puntos = new List<Point>();

            int x1 = p1.X, y1 = p1.Y;
            int x2 = p2.X, y2 = p2.Y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            while (true)
            {
                puntos.Add(ScalePoint(new Point(x1, y1), w, h));

                if (x1 == x2 && y1 == y2) break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }

            return puntos;
        }

        // ======== PUNTO MEDIO =========

        public static async Task<List<Point>> PuntoMedio(Point p1, Point p2, int w, int h)
        {
            List<Point> puntos = new List<Point>();

            int x1 = p1.X;
            int y1 = p1.Y;
            int x2 = p2.X;
            int y2 = p2.Y;

            int dx = x2 - x1;
            int dy = y2 - y1;

            float d = dy - (dx / 2f);

            int x = x1, y = y1;

            puntos.Add(ScalePoint(new Point(x, y), w, h));

            while (x < x2)
            {
                x++;

                if (d < 0)
                {
                    d += dy;
                }
                else
                {
                    y++;
                    d += (dy - dx);
                }

                puntos.Add(ScalePoint(new Point(x, y), w, h));
            }

            return puntos;
        }
    }
}
