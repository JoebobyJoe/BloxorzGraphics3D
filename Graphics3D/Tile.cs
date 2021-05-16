using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Tile
    {
        // List<Cube> cubeFloor = new List<Cube>();
        Cube aCube = new Cube(100, 100, 100);
        Pen pen = new Pen(Color.Gray);
        Color color = Color.Gray;
        public Tile()
        {
            aCube.Scale(new Point3D(1, .1, 1));
            aCube.Scale(.5f);
        }
        public Pen Pen { set { pen = value; } }
        public Color Color { set { color = value; aCube.Color = value; } }
        public Point3D Center { get { return aCube.Center; } }
        public Cube ToCube()
        {
            return aCube;
        }
        public void Scale(double scaleAmount)
        {
            aCube.Scale(scaleAmount);
        }
        
        public bool Contains(Point2D p, double distance)
        {
            return aCube.Contains(p, distance);
        }
        /// <summary>
        /// Move the point by the specified amount
        /// </summary>
        /// <param name="shift"></param>
        public void Translate(Point3D shift)
        {
            aCube.Translate(shift);
        }
        public void RotateD(Point3D angle)
        {
            aCube.RotateD(angle);
        }
        public void Rotate(Point3D theta)
        {
            aCube.Rotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotate(Point3D theta)
        {
            aCube.UnRotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotateD(Point3D angle)
        {
            aCube.UnRotateD(angle);
        }
        public void Draw(Graphics gr, double distance, Point3D lightSrc)
        {
            aCube.Pen = pen;
            aCube.Color = color;
            aCube.Draw(gr, distance, lightSrc);
        }
    }
}
