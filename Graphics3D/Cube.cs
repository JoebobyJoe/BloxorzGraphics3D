using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Cube
    {
        List<Polygon3D> polygons = new List<Polygon3D>();
        Pen pen = new Pen(Color.Gray);
        Color color = Color.Green;
        public Cube(int x = 100, int y = 100, int z = 100)
        {
            //back
            Polygon3D polygon = new Polygon3D();
            polygon.Add(new Point3D(-x, -y, -z));
            polygon.Add(new Point3D(x, -y, -z));
            polygon.Add(new Point3D(x, y, -z));
            polygon.Add(new Point3D(-x, y, -z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);
            //left
            polygon = new Polygon3D();
            polygon.Add(new Point3D(-x, -y, -z));
            polygon.Add(new Point3D(-x, y, -z));
            polygon.Add(new Point3D(-x, y, z));
            polygon.Add(new Point3D(-x, -y, z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);
            //right
            polygon = new Polygon3D();
            polygon.Add(new Point3D(x, y, -z));
            polygon.Add(new Point3D(x, -y, -z));
            polygon.Add(new Point3D(x, -y, z));
            polygon.Add(new Point3D(x, y, z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);
            //front
            polygon = new Polygon3D();
            polygon.Add(new Point3D(x, -y, z));
            polygon.Add(new Point3D(-x, -y, z));
            polygon.Add(new Point3D(-x, y, z));
            polygon.Add(new Point3D(x, y, z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);

            // bottom
            polygon = new Polygon3D();
            polygon.Add(new Point3D(-x, y, -z));
            polygon.Add(new Point3D(x, y, -z));
            polygon.Add(new Point3D(x, y, z));
            polygon.Add(new Point3D(-x, y, z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);

            // top 
            polygon = new Polygon3D();
            polygon.Add(new Point3D(x, -y, -z));
            polygon.Add(new Point3D(-x, -y, -z));
            polygon.Add(new Point3D(-x, -y, z));
            polygon.Add(new Point3D(x, -y, z));
            polygon.FGBrush = Brushes.Red;
            polygon.BGBrush = Brushes.Blue;
            polygon.Pen = Pens.CornflowerBlue;
            polygons.Add(polygon);
        }
        public Pen Pen { set { pen = value; } }
        public Color Color { set { color = value; } }
        public Point3D Center
        {
            get
            {
                Point3D temp = new Point3D();
                foreach (Polygon3D polygon in polygons)
                    temp += polygon.Center;
                temp /= polygons.Count;
                return temp;
              
            }
           
        }
        
        public bool Contains(Point2D p, double distance)
        {
            foreach (Polygon3D polygon in polygons)
                if (polygon.Contains(p, distance)) return true;
            return false;
        }
        public void SnapToGrid()
        {
            foreach (Polygon3D polygon in polygons)
                polygon.SnapToGrid();
        }
        public Point3D GetRotatePointMaxX()
        {
            Point3D biggest = new Point3D(double.MinValue,0,0);
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].GetRotatePointMaxX().X > biggest.X)
                    biggest = polygons[i].GetRotatePointMaxX();
            return biggest;
        }
        public Point3D GetRotatePointMinX()
        {
            Point3D smallest = new Point3D(double.MaxValue,0,0);
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].GetRotatePointMinX().X < smallest.X)
                    smallest = polygons[i].GetRotatePointMinX();
            return smallest;
        }

        public Point3D GetRotatePointMaxZ()
        {
            Point3D biggest = new Point3D(0,0,double.MinValue);
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].GetRotatePointMaxZ().Z > biggest.Z)
                    biggest = polygons[i].GetRotatePointMaxZ();
            return biggest;
        }
        public Point3D GetRotatePointMinZ()
        {
            Point3D smallest = new Point3D(0,0,double.MaxValue);
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].GetRotatePointMinZ().Z < smallest.Z)
                    smallest = polygons[i].GetRotatePointMinZ();
            return smallest;
        }
        public void Scale(double scaleAmount)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.Scale(scaleAmount);
        }
        public void Scale(Point3D scaleAmount)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.Scale(scaleAmount);
        }
        /// <summary>
        /// Move the point by the specified amount
        /// </summary>
        /// <param name="shift"></param>
        public void Translate(Point3D shift)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.Translate(shift);
        }

        public void Draw(Graphics gr, double distance, Point3D lightSrc)
        {
            //the Forground of the polygons
            //foreach (Polygon3D polygon in polygons)
            //{
            //    polygon.FGBrush = brush;
            //    polygon.Fill(gr, distance, Face.foreground, lightSrc);
            //    polygon.Pen = pen;

            //    polygon.Draw(gr, distance);
            //}

            //the background of the polygons
            foreach (Polygon3D polygon in polygons)
            {
                polygon.BGColor = color;
                polygon.Fill(gr, distance, Face.background, lightSrc);
                polygon.Pen = pen;
                
                polygon.Draw(gr, Face.background, distance);
            }
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotate(Point3D theta)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.UnRotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotateD(Point3D angle)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.UnRotateD(angle);
        }
        public void RotateD(Point3D angle)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.RotateD(angle);
        }
        public void Rotate(Point3D theta)
        {
            foreach (Polygon3D polygon in polygons)
                polygon.Rotate(theta);
        }
    }
}
