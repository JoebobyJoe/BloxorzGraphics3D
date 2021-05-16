using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Point2D
    {
        #region Point2D variables
        // the coordinate values of this point
        double x = 0;
        double y = 0;
        #endregion

        #region Point2D Constructors
        public Point2D() { } // just initialize to the default values

        /// <summary>
        /// Construct a new point based on passed values
        /// </summary>
        /// <param name="x">The point x-value</param>
        /// <param name="y">The point y-value</param>
        public Point2D(double x, double y)
        {
            this.x = (double)x;
            this.y = (double)y;
        }
        /// <summary>
        /// Construct a new point based on a system point
        /// </summary>
        /// <param name="p"></param>
        public Point2D(Point p)
        {
            x = p.X;
            y = p.Y;
        }
        /// <summary>
        /// Construct a new point based on a system pointF
        /// </summary>
        /// <param name="p"></param>
        public Point2D(PointF p)
        {
            x = p.X;
            y = p.Y;
        }
        #endregion

        #region Point2D Properties
        /// <summary>
        /// Gets and sets the x-value of the point
        /// </summary>
        public double X { get { return x; } set { x = value; } }
        /// <summary>
        /// Gets and sets the y-value of the point
        /// </summary>
        public double Y { get { return y; } set { y = value; } }
        /// <summary>
        /// Get/Set the point values to/from a PointF specification
        /// </summary>
        public PointF PointF { get { return new PointF((float)x, (float)y); } set { x = value.X; y = value.Y; } }
        #endregion

        #region Point2D Operators
        /// <summary>
        /// Add two points together by coordinate value and return the result
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="p2">The second Point2D</param>
        /// <returns></returns>
        public static Point2D operator +(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X + p2.X, p1.Y + p2.Y);
        }
        /// <summary>
        /// Subtract two points together by coordinate value and return the result
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="p2">The second Point2D</param>
        /// <returns></returns>
        public static Point2D operator -(Point2D p1, Point2D p2)
        {
            return new Point2D(p1.X - p2.X, p1.Y - p2.Y);
        }
        /// <summary>
        /// Multiply a point by a scalar value
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="scalar">The multiplier value</param>
        /// <returns></returns>
        public static Point2D operator *(Point2D p1, double scalar)
        {
            return new Point2D(p1.X * scalar, p1.Y * scalar);
        }
        /// <summary>
        /// Multiply a point by a scalar value
        /// </summary>
        /// <param name="scalar">The multiplier value</param>
        /// <param name="p1">The first Point2D</param>
        /// <returns></returns>
        public static Point2D operator *(double scalar, Point2D p1)
        {
            return new Point2D(p1.X * scalar, p1.Y * scalar);
        }
        /// <summary>
        /// Divide a point by a scalar value
        /// </summary>
        /// <param name="p1">The first Point2D</param>
        /// <param name="scalar">The multiplier value</param>
        /// <returns></returns>
        public static Point2D operator /(Point2D p1, double scalar)
        {
            return new Point2D(p1.X / scalar, p1.Y / scalar);
        }
        /// <summary>
        /// Calculate the Dot-Product of the two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double operator *(Point2D p1, Point2D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }
        /// <summary>
        /// Get the magnitude of this point.
        /// </summary>
        public double Magnitude { get { return (double)Math.Sqrt(x * x + y * y ); } }
        #endregion

        #region Point2D Methods
        /// <summary>
        /// Rotates the point about the origin by an angle theta in radians
        /// </summary>
        /// <param name="theta">Angle in radians</param>
        public void Rotate(double theta)
        {
            // now de-skew
            double xCorrected = (double)(x * Math.Cos(theta) - y * Math.Sin(theta));
            y = (double)(y * Math.Cos(theta) + x * Math.Sin(theta));
            x = xCorrected;
        }
        /// <summary>
        /// Rotates the point about the origin by an angle in degrees
        /// </summary>
        /// <param name="angle"></param>
        public void RotateD(double angle)
        {
            Rotate(angle * Math.PI / 180);
        }

        /// <summary>
        /// Draw the point on the user screen with the specified color and size
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        public void DrawPoint(Graphics gr, Color color, Size size)
        {
            gr.FillEllipse(new SolidBrush(color), (float)(x - size.Width / 2), (float)y - size.Height / 2, size.Width, size.Height);
        }
        /// <summary>
        /// Draw the point on the user screen with the specified color
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="color"></param>
        public void DrawPoint(Graphics gr, Color color)
        {
            gr.FillEllipse(new SolidBrush(color), (float)x - 2, (float)y - 2, 4, 4);
        }
        /// <summary>
        /// Draw the point on the user screen with default color (white) and size 4px by 4px
        /// </summary>
        /// <param name="gr"></param>
        public void DrawPoint(Graphics gr)
        {
            gr.FillEllipse(Brushes.White, (float)x - 2, (float)y - 2, 4, 4);
        }
        #endregion
    }
}
