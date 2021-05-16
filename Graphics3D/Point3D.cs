using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Graphics3D
{
    class Point3D
    {
        #region Class Variables
        double x = 0, y = 0, z = 0;
        public static Point3D Origin = new Point3D(0, 0, 0);
        #endregion

        #region Class Constructors
        public Point3D() { }
        public Point3D(double x, double y, double z)
        {
            this.x = (double)x;
            this.y = (double)y;
            this.z = (double)z;
        }

        #endregion

        #region Class properties
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Z { get { return z; } set { z = value; } }
        /// <summary>
        /// Get the magnitude of this point.
        /// </summary>
        public double Magnitude { get { return (double)Math.Sqrt(x * x + y * y + z * z); } }
        #endregion
        #region Class Opertators
        /// <summary>
        /// Add two points together by coordinate value and return the result
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="p2">The second Point2D</param>
        /// <returns></returns>
        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }
        /// <summary>
        /// Subtract two points together by coordinate value and return the result
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="p2">The second Point2D</param>
        /// <returns></returns>
        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }
        /// <summary>
        /// Multiply a point by a scalar value
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="scalar">The multiplier value</param>
        /// <returns></returns>
        public static Point3D operator *(Point3D p1, double scalar)
        {
            return new Point3D(p1.X * scalar, p1.Y * scalar, p1.Z * scalar);
        }
        /// <summary>
        /// Multiply a point by a scalar value
        /// </summary>
        /// <param name="scalar">The multiplier value</param>
        /// <param name="p1">The first Point2D</param>
        /// <returns></returns>
        public static Point3D operator *(double scalar, Point3D p1)
        {
            return new Point3D(p1.X * scalar, p1.Y * scalar, p1.Z * scalar);
        }
        /// <summary>
        /// Divide a point by a scalar value
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="scalar">The multiplier value</param>
        /// <returns></returns>
        public static Point3D operator /(Point3D p1, double scalar)
        {
            return new Point3D(p1.X / scalar, p1.Y / scalar, p1.Z / scalar);
        }
        /// <summary>
        /// Calculate the Dot-Product of the two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double operator *(Point3D p1, Point3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }
        /// <summary>
        /// This is the cross product of two vectors giving the normal vector
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point3D operator |(Point3D p1, Point3D p2)
        {
            return new Point3D(
                p1.Y * p2.Z - p1.Z * p2.Y,
                p1.Z * p2.X - p1.X * p2.Z,
                p1.X * p2.Y - p1.Y * p2.X);
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Rotate the point by a radian angle theta(three dimensional rotate)
        /// </summary>
        /// <param name="theta">the angle of rotation, one angle in each axis</param>
        public void Rotate(Point3D theta)
        {
            // Rotate on the z-axis, changes the x,y values
            double xCorrected = (double)(x * Math.Cos(theta.Z) - y * Math.Sin(theta.Z));
            y = (double)(y * Math.Cos(theta.Z) + x * Math.Sin(theta.Z));
            x = xCorrected;

            // rotate on the y-axis,changes the x,z values
            xCorrected = (double)(x * Math.Cos(theta.Y) - z * Math.Sin(theta.Y));
            z = (double)(z * Math.Cos(theta.Y) + x * Math.Sin(theta.Y));
            x = xCorrected;

            //rotates on the x-axis,changes the y,z values
            double zCorrected = (double)(z * Math.Cos(theta.X) - y * Math.Sin(theta.X));
            y = (double)(y * Math.Cos(theta.X) + z * Math.Sin(theta.X));
            z = zCorrected;
        }
        /// <summary>
        /// Rotate the point by a radian angle Degrees(three dimensional rotate)
        /// </summary>
        /// <param name="theta">the angle of rotation, one angle in each axis</param>
        public void RotateD(Point3D degrees)
        {
            Rotate(degrees * (double)Math.PI / 180);
        }
        /// <summary>
        /// to unrotate, we use a negative angle and do all the rotates in the opposite order
        /// </summary>
        /// <param name="theta"></param>
        public void UnRotate(Point3D theta)
        {
            Point3D angle = theta * -1;
            //rotates on the x-axis,changes the y,z values
            double zCorrected = (double)(z * Math.Cos(angle.X) - y * Math.Sin(angle.X));
            y = (double)(y * Math.Cos(angle.X) + z * Math.Sin(angle.X));
            z = zCorrected;

            // rotate on the y-axis,changes the x,z values
            double xCorrected = (double)(x * Math.Cos(angle.Y) - z * Math.Sin(angle.Y));
            z = (double)(z * Math.Cos(angle.Y) + x * Math.Sin(angle.Y));
            x = xCorrected;

            // Rotate on the z-axis, changes the x,y values
            xCorrected = (double)(x * Math.Cos(angle.Z) - y * Math.Sin(angle.Z));
            y = (double)(y * Math.Cos(angle.Z) + x * Math.Sin(angle.Z));
            x = xCorrected;
        }
        /// <summary>
        /// Rotate the point by a radian angle Degrees(three dimensional rotate)
        /// </summary>
        /// <param name="theta">the angle of rotation, one angle in each axis</param>
        public void UnRotateD(Point3D degrees)
        {
            UnRotate(degrees * (double)Math.PI / 180);
        }
        /// <summary>
        /// Project the 3D point on the 2D surface where the viewer is "distance" from the 2D surface
        /// </summary>
        /// <param name="distance">the distance (in pixels) from the screen</param>
        /// <returns></returns>
        public Point2D Projection(double distance)
        { 
          // return new Point2D(x* distance /(distance -z)),(y* distance /(distance -z));
            return new Point2D(x, y) * distance / (distance - z);
        }
        /// <summary>
        /// Scale the point by the amount specified
        /// </summary>
        /// <param name="scaleAmount"></param>
        public void Scale(double scaleAmount)
        {
            x *= scaleAmount;
            y *= scaleAmount;
            z *= scaleAmount;
        }
        /// <summary>
        /// Move the point by the specified amount
        /// </summary>
        /// <param name="shift"></param>
        public void Translate(Point3D shift)
        {
            x += shift.X;
            y += shift.Y;
            z += shift.Z;
        }
        /// <summary>
        /// Draw the point to the user's screen
        /// </summary>
        /// <param name="gr"></param>
        public void Draw(Graphics gr, double distance)
        {
            Projection(distance).DrawPoint(gr);
        }
        #endregion
    }
}
