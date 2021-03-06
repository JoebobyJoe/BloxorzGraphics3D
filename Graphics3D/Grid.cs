using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Grid
    {
        List<Line3D> lines = new List<Line3D>();
        public Grid()
        {
            // the x axis lines
            for (int i = 0; i < 5; i++)
            {
                lines.Add(new Line3D(new Point3D(-50 - i * 100, 0, -500), new Point3D(-50 - i * 100, 0, 500)));
                lines.Add(new Line3D(new Point3D(50 + i * 100, 0, -500), new Point3D(50 + i * 100, 0, 500)));
            }

            //the z axis lines 
            for (int i = 0; i < 5; i++)
            {
                lines.Add(new Line3D(new Point3D(-500, 0, -50 - i * 100), new Point3D(500, 0, -50 - i * 100)));
                lines.Add(new Line3D(new Point3D(-500, 0, 50 + i * 100), new Point3D(500, 0, 50 + i * 100)));
            }
        }
        /// <summary>
        /// calculate the normal vector to this polygon (the normal is perpendicular to the surface of the polygon)
        /// </summary>
        /// <returns></returns>
        public Point3D Normal()
        {
            Point3D v1 = lines[0].Vector;
            Point3D v2 = lines[10].Vector;
           return v1 | v2 ; // this is the cross product operator
        }
        public void Draw(Graphics gr, double distance)
        {
            foreach (Line3D line in lines)
                line.Draw(gr, distance);
        }
        public void RotateD(Point3D angle)
        {
            foreach (Line3D line in lines)
                line.RotateD(angle);
        }
        public void Rotate(Point3D theta)
        {
            foreach (Line3D line in lines)
                line.Rotate(theta);
        }
    }
}
