using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
   

    class Polygon2D
    {
        #region Polygon2D variables
        List<Point2D> pts = new List<Point2D>();
        #endregion

        #region Polygon2D Constructors
        public Polygon2D() { }
        #endregion

        #region Polygon2D Properties

        public Face Face
        {
            get
            {
                // calculate two vectors
                Point2D v1 = pts[1] - pts[0];
                Point2D v2 = pts[2] - pts[0];
                if (v1.X * v2.Y - v1.Y * v2.X > 0)
                    return Graphics3D.Face.foreground;
                else
                    return Graphics3D.Face.background;
            }
        }
        public bool Contains(int x, int y)
        {
            return IsInPolygon(new Point(x, y));
        }
        private bool IsInPolygon(Point p)
        {
            PointF[] poly = ToPointF();
            PointF p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }
            var oldPoint = new PointF(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new PointF(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }

                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }
            return inside;
        }

        #endregion

        #region Polygon2D Methods
        /// <summary>
        /// Add a point to the list of points in the polygon
        /// </summary>
        /// <param name="p"></param>
        public void Add(Point2D p)
        {
            pts.Add(p);
        }
        public void Add(PointF p)
        {
            pts.Add(new Point2D(p));
        }
        public void Add(double x, double y)
        {
            pts.Add(new Point2D(x, y));
        }

        /// <summary>
        /// Rotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void Rotate(double theta)
        {
            foreach (Point2D p in pts)
                p.Rotate(theta);
        }
        /// <summary>
        /// Rotate the polygon about the origin by the angle in degrees
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        public void RotateD(double angle)
        {
            Rotate(angle * Math.PI / 180);
        }

        /// <summary>
        /// Convert the list of Point2D values to an array of PointF values
        /// </summary>
        /// <returns></returns>
        private PointF[] ToPointF()
        {
            PointF[] points = new PointF[pts.Count];
            for (int i = 0; i < pts.Count; i++)
                points[i] = pts[i].PointF;
            return points;
        }

        /// <summary>
        /// Draw a polygon with default color: white, width 1px
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            Draw(gr, Color.White, 1);
        }
        /// <summary>
        /// Draw the polygon with the specified color and width
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public void Draw(Graphics gr, Color color, double width)
        {
            PointF[] points = ToPointF();
            // make the pen
            Pen pen = new Pen(color, (float)width);
            // draw the lines
            for (int i = 0; i < points.Length; i++)
                gr.DrawLine(pen, points[i], points[(i + 1) % points.Length]);
        }
        /// <summary>
        /// Draw the polygon with the specified pen and width
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public void Draw(Graphics gr, Pen pen)
        {
            PointF[] points = ToPointF();            
            // draw the lines
            for (int i = 0; i < points.Length; i++)
                gr.DrawLine(pen, points[i], points[(i + 1) % points.Length]);
        }
        /// <summary>
        /// Fill the polygon with a default color (white)
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr)
        {
            Fill(gr, Brushes.White);
        }
        /// <summary>
        /// Fill the polygon with a specified color
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, Color color)
        {
            Brush br = new SolidBrush(color);
            Fill(gr, br);
        }
        /// <summary>
        /// Fill the polygon with a specified Brush
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, Brush br)
        {
            gr.FillPolygon(br, ToPointF());
        }
        #endregion
    }
}
