using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Polygon3D
    {
        #region Class Variables
        List<Point3D> pts = new List<Point3D>();
        Pen pen = Pens.White;
        Brush fgBrush = new SolidBrush(Color.White);
        Brush bgBrush = new SolidBrush(Color.White);
        Color fgColor = Color.Red;
        Color bgColor = Color.Blue;

        #endregion

        #region Class Constructors

        public Polygon3D() { }

        #endregion

        #region Class properties

        public Pen Pen { set { pen = value; } }
        public Brush FGBrush { set { fgBrush = value; } }
        public Brush BGBrush { set { bgBrush = value; } }
        public Color FGColor { set { fgColor = value; } }
        public Color BGColor { set { bgColor = value; } }

        #endregion

        #region Class Methods
        /// <summary>
        /// Scale the point by the amount specified
        /// </summary>
        /// <param name="scaleAmount"></param>
        public void Scale(double scaleAmount)
        {
            foreach (Point3D p in pts)
                p.Scale(scaleAmount);
        }
        /// <summary>
        /// Scale the point by the amount specified
        /// </summary>
        /// <param name="scaleAmount"></param>
        public void Scale(Point3D scaleAmount)
        {
            foreach (Point3D p in pts)
            {
                p.X *= scaleAmount.X;
                p.Y *= scaleAmount.Y;
                p.Z *= scaleAmount.Z;
            }
        }
        
        /// <summary>
        /// Move the point by the specified amount
        /// </summary>
        /// <param name="shift"></param>
        public void Translate(Point3D shift)
        {
            foreach (Point3D p in pts)
                p.Translate(shift);
        }
        /// <summary>
        /// determine the reflection "amount" (between 0 and 255) based on the light source and the normal fro this polygon
        /// </summary>
        /// <param name="lightSrc"></param>
        /// <returns></returns>
        private int Reflectivity(Point3D lightSrc)
        {
            Point3D normal = Normal();
            double dotProduct = 0; // this is the cos of the angle between the lightSrc and the normal
            dotProduct = (normal * lightSrc) / (lightSrc.Magnitude * normal.Magnitude); // this is a value between  -1 and +1
            double temp = (1 + dotProduct) / 2;//this is a value between 0 and 1
            return (int)(temp * 255);
        }
        /// <summary>
        /// calculate the normal vector to this polygon (the normal is perpendicular to the surface of the polygon)
        /// </summary>
        /// <returns></returns>
        private Point3D Normal()
        {
            Point3D v1 = pts[1] - pts[0];
            Point3D v2 = pts[2] - pts[0];
            return v1 | v2; // this is the cross product operator
        }

        /// <summary>
        /// Add a point to the list of points in the polygon
        /// </summary>
        /// <param name="p"></param>
        public void Add(Point3D p)
        {
            pts.Add(p);
        }
        public void Add(double x, double y, double z)
        {
            pts.Add(new Point3D(x, y, z));
        }
        public bool Contains(Point2D p, double distance)
        {
            Polygon2D poly2D = ToPolygon2D(distance);
            return poly2D.Contains((int)p.X, (int)p.Y);
        }
        public void SnapToGrid()
        {
            foreach (Point3D p in pts)
            {
                p.X = (double)Math.Round(p.X);
                p.Y = (double)Math.Round(p.Y);
                p.Z = (double)Math.Round(p.Z);
            }
        }
        /// <summary>
        /// gets the max value of the block for rotation
        /// </summary>
        /// <returns></returns>
        public Point3D GetRotatePointMaxX()
        {
            Point3D biggest = new Point3D(double.MinValue,0,0);
            for (int i = 0; i < pts.Count; i++)
                if (pts[i].X > biggest.X && IsZero(pts[i].Y))
                    biggest = pts[i];
            return biggest;
        }
        /// <summary>
        /// gets the min value of the block for rotation
        /// </summary>
        /// <returns></returns>
        public Point3D GetRotatePointMinX()
        {
            Point3D smallest = new Point3D(double.MaxValue,0,0);
            for (int i = 0; i < pts.Count; i++)
                if (pts[i].X < smallest.X && IsZero(pts[i].Y))
                    smallest = pts[i];
            return smallest;
        }
        /// <summary>
        /// gets the max value of the block for rotation
        /// </summary>
        /// <returns></returns>
        public Point3D GetRotatePointMaxZ()
        {
            Point3D biggest = new Point3D(0,0,double.MinValue);
            for (int i = 0; i < pts.Count; i++)
                if (pts[i].Z > biggest.Z && IsZero(pts[i].Y))
                    biggest = pts[i];
            return biggest;
        }
        /// <summary>
        /// gets the min value of the block for rotation
        /// </summary>
        /// <returns></returns>
        public Point3D GetRotatePointMinZ()
        {
            Point3D smallest = new Point3D(0,0,double.MaxValue);
            for (int i = 0; i < pts.Count; i++)
                if (pts[i].Z < smallest.Z && IsZero( pts[i].Y))
                    smallest = pts[i];
            return smallest;
        }
        private bool IsZero(double y)
        {
            return Math.Abs(y) < .0001;
        }
        /// <summary>
        /// Rotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void Rotate(Point3D theta)
        {
            foreach (Point3D p in pts)
                p.Rotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotate(Point3D theta)
        {
            foreach (Point3D p in pts)
                p.UnRotate(theta);
        }
        /// <summary>
        /// Rotate the polygon about the origin by the angle in degrees
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        public void RotateD(Point3D angle)
        {
            Rotate(angle * (double)Math.PI / 180);
        }
        /// <summary>
        /// UnRotate the polygon about the origin by the angle in degrees
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        public void UnRotateD(Point3D angle)
        {
            UnRotate(angle * (double)Math.PI / 180);
        }
        /// <summary>
        /// Change the poly3D to poly2D
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private Polygon2D ToPolygon2D(double distance)
        {
            Polygon2D poly2D = new Polygon2D();
            foreach (Point3D p in pts)
                poly2D.Add(p.Projection(distance));
            return poly2D;
        }

        public void Draw(Graphics gr, Face whichFace, double distance)
        {
            Polygon2D poly2D = ToPolygon2D(distance);
            Face face = poly2D.Face;
            if (face == whichFace)
                poly2D.Draw(gr, pen);
        }

        /// <summary>
        /// FIll the polygon on the display device
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="distance"></param>
        /// <param name="whichFace">The face which is to be drawn</param>
        public void Fill(Graphics gr, double distance, Face whichFace, Point3D lightSrc)
        {
            Polygon2D poly2D = ToPolygon2D(distance);
            Face face = poly2D.Face;
            if (face != whichFace)
                return;
            // define new burshes based on the reflectitvity and the face being drawn
            Brush brush = new SolidBrush(Color.White);

            if (poly2D.Face == Face.foreground)
                brush = new SolidBrush(Color.FromArgb(Reflectivity(lightSrc), fgColor));
            else if (poly2D.Face == Face.background)
                brush = new SolidBrush(Color.FromArgb(255 - Reflectivity(lightSrc), bgColor));

            poly2D.Fill(gr, Brushes.Black);
            poly2D.Fill(gr, brush);
        }
        public Point3D Center
        {
            get
            {
                Point3D temp = new Point3D();
                foreach (Point3D p in pts)
                    temp += p;
                temp /= pts.Count;
                return temp;
            }
        }
        #endregion
    }
}
