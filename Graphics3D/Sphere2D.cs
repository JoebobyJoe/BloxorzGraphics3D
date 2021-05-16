using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    /// <summary>
    /// A two-dimensional sphere(Circle)
    /// 
    /// </summary>
    class Sphere2D
    {
        #region Class Variables
        Point2D center = new Point2D();
        double radius = 1;
        Pen pen = new Pen(Color.White, 1);// a default pen for outlining
        Brush brush = new SolidBrush(Color.White);//a default brush for filling 

        #endregion

        #region Class Constructors
        /// <summary>
        /// creates a sphere2D object with the default center, radius
        /// </summary>
        public Sphere2D() { }
        /// <summary>
        /// construct a sphere2D object with the specified center and radius values
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Sphere2D(Point2D center,double radius)
        {
            this.center = center;
            this.radius = radius;
        }
        #endregion

        #region Class properties
        /// <summary>
        /// Get/Set the center of the sphere2d object
        /// </summary>
        public Point2D Center { get { return center; } set { center = value; } }
        /// <summary>
        /// Get/Set the center of the sphere2d object
        /// </summary>
        public double Radius { get { return radius; } set { radius = value; } }

        public Pen Pen { set { pen = value;} }
        public Brush Brush { set { brush = value; } }

        #endregion

        #region Class Methods
        /// <summary>
        /// Draw the sphere to the graphics output
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr)
        {
            gr.DrawEllipse(pen, (float)(center.X - radius), (float)(center.Y - radius), 2 * (float)radius, 2 * (float)radius);

        }
        /// <summary>
        /// Fill the sphere on the graphics output
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr)
        {
            gr.FillEllipse(brush, (float)(center.X - radius), (float)(center.Y - radius), 2 * (float)radius, 2 * (float)radius);

        }

        #endregion
    }
}
