using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Sphere3D
    {
        #region Class Variables
        Point3D center = new Point3D();
        double radius = 1;
        Pen pen = Pens.White;
        Brush brush = Brushes.White;
        #endregion

        #region Class Constructors
        public Sphere3D() { }
        /// <summary>
        /// construct a sphere2D object with the specified center and radius values
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Sphere3D(Point3D center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }
        #endregion

        #region Class properties
        /// <summary>
        /// Get/Set the center of the sphere2d object
        /// </summary>
        public Point3D Center { get { return center; } set { center = value; } }
        /// <summary>
        /// Get/Set the center of the sphere2d object
        /// </summary>
        public double Radius { get { return radius; } set { radius = value; } }

        public Pen Pen { set { pen = value; } }
        public Brush Brush { set { brush = value; } }

        /// <summary>
        /// convert the 3D sphere to a 2D sphere
        /// </summary>
        /// <param name="distance">from the user to the screen</param>
        /// <returns></returns>
        private Sphere2D ToSphere2D(double distance)
        {
            Point2D center2D = center.Projection(distance);
            double temp = (double)Math.Sqrt(center.X * center.X + center.Y * center.Y);
            double radius2D = (radius + temp) * distance / (distance - center.Z) - center2D.Magnitude;
            Sphere2D sphere2D = new Sphere2D(center2D, radius2D);
            return sphere2D;
        }

        #endregion

        #region Class Methods

        /// <summary>
        /// Draw the sphere to the graphics output
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr, double distance)
        {
            Sphere2D sphere2D = ToSphere2D(distance);
            sphere2D.Pen = pen;
            sphere2D.Draw(gr);
        }

      
        /// <summary>
        /// Fill the sphere on the graphics output
        /// </summary>
        /// <param name="gr"></param>
        public void Fill(Graphics gr, double distance)
        {
            Sphere2D sphere2D = ToSphere2D(distance);
            sphere2D.Brush = brush;
            sphere2D.Fill(gr);
        }

        #endregion
    }
}
