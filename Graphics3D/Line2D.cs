using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Line2D
    {
        #region Line2D variables
        Point2D[] pts = new Point2D[2];
        #endregion

        #region Line2D Constructors
        public Line2D()
        {
            pts[0] = new Point2D();
            pts[1] = new Point2D();
        }
        public Line2D(Point2D p1, Point2D p2)
        {
            pts[0] = p1;
            pts[1] = p2;
        }
        public Line2D(double x1, double y1, double x2, double y2)
        {
            pts[0] = new Point2D(x1, y1);
            pts[1] = new Point2D(x2, y2);
        }
        #endregion

        #region Line2D Properties
        #endregion

        #region Line2D Methods
        /// <summary>
        /// Rotate the line about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void Rotate(double theta)
        {
            pts[0].Rotate(theta);
            pts[1].Rotate(theta);
        }
        /// <summary>
        /// Rotate the line about the origin by the angle in degrees
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        public void RotateD(double angle)
        {
            Rotate(angle * Math.PI / 180);
        }
        /// <summary>
        /// Draw a line with default color: white, width 1px
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.DrawLine(Pens.White, pts[0].PointF, pts[1].PointF);
        }
        /// <summary>
        /// Draw the line with the specified color and width
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public void Draw(Graphics gr, Color color, double width)
        {
            gr.DrawLine(new Pen(color,(float) width), pts[0].PointF, pts[1].PointF);
        }
        #endregion
    }
}
