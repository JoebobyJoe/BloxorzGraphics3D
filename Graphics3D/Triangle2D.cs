using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Triangle2D
    {
        #region Triangle2D variables
        Point2D[] pts = new Point2D[3];
        #endregion

        #region Triangle2D Constructors
        public Triangle2D()
        {
            pts[0] = new Point2D();
            pts[1] = new Point2D();
            pts[2] = new Point2D();
        }
        public Triangle2D(Point2D p1, Point2D p2, Point2D p3)
        {
            pts[0] = p1;
            pts[1] = p2;
            pts[2] = p3;
        }
        public Triangle2D(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            pts[0] = new Point2D(x1, y1);
            pts[1] = new Point2D(x2, y2);
            pts[2] = new Point2D(x3, y3);
        }
        #endregion

        #region Triangle2D Properties
        #endregion

        #region Triangle2D Methods
        /// <summary>
        /// Rotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void Rotate(double theta)
        {
            pts[0].Rotate(theta);
            pts[1].Rotate(theta);
            pts[2].Rotate(theta);
        }
        /// <summary>
        /// Rotate the triangle about the origin by the angle in degrees
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        public void RotateD(double angle)
        {
            Rotate(angle * Math.PI / 180);
        }
        /// <summary>
        /// Draw a triangle with default color: white, width 1px
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.DrawLine(Pens.White, pts[0].PointF, pts[1].PointF);
            gr.DrawLine(Pens.White, pts[1].PointF, pts[2].PointF);
            gr.DrawLine(Pens.White, pts[2].PointF, pts[0].PointF);
        }
        /// <summary>
        /// Draw the triangle with the specified color and width
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public void Draw(Graphics gr, Color color, double width)
        {
            gr.DrawLine(new Pen(color, (float)width), pts[0].PointF, pts[1].PointF);
            gr.DrawLine(new Pen(color, (float)width), pts[1].PointF, pts[2].PointF);
            gr.DrawLine(new Pen(color, (float)width), pts[2].PointF, pts[0].PointF);
        }
        /// <summary>
        /// Fill the triangle with a default color (white)
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr)
        {
            Fill(gr, Brushes.White);
        }
        /// <summary>
        /// Fill the triangle with a specified color
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, Color color)
        {
            Brush br = new SolidBrush(color);
            Fill(gr, br);
        }
        /// <summary>
        /// Fill the triangle with a specified Brush
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, Brush br)
        {
            PointF[] points = new PointF[pts.Length];
            for (int i = 0; i < pts.Length; i++)
                points[i] = pts[i].PointF;
            gr.FillPolygon(br, points);
        }
        #endregion
    }
}
