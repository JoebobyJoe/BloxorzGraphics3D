using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Line3D
    {
        #region Class Variables
        Point3D p1, p2; // the two points specifying a line
        Pen pen = Pens.BurlyWood;

        #endregion

        #region Class Constructors
        public Line3D(Point3D p1, Point3D p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        #endregion

        #region Class properties
        public Pen Pen { set { pen = value; } }
        public Point3D Vector { get { return p2 - p1; } }
        #endregion

        #region Class Methods
        /// <summary>
        /// Rotate the point by the 3D radian angle
        /// </summary>
        /// <param name="theta"></param>
        public void Rotate(Point3D theta)
        {
            p1.Rotate(theta);
            p2.Rotate(theta);
        }

        /// <summary>
        /// Rotate the point by the 3D degree angle
        /// </summary>
        /// <param name="angle"></param>
        public void RotateD(Point3D angle)
        {
            p1.RotateD(angle);
            p2.RotateD(angle);
        }
        /// <summary>
        /// Scale hte line by the specifed amount
        /// </summary>
        /// <param name="scaleAmount"></param>
        public void Scale(double scaleAmount)
        {
            p1.Scale(scaleAmount);
            p2.Scale(scaleAmount);
        }
        /// <summary>
        /// translate the line by the specifed amount
        /// </summary>
        /// <param name="scaleAmount"></param>
        public void translate(Point3D shift)
        {
            p1.Translate(shift);
            p2.Translate(shift);
        }
        /// <summary>
        /// Draw the line to the screen
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="distance"></param>
        public void Draw(Graphics gr, double distance)
        {
            gr.DrawLine(pen, p1.Projection(distance).PointF, p2.Projection(distance).PointF);
        }
        #endregion
    }
}
