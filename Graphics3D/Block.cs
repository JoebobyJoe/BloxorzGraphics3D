using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Block
    {
        List<Cube> blocks = new List<Cube>();

        public Block()
        {
            blocks.Add(new Cube());
            blocks.Add(new Cube());
            blocks[1].Translate(new Point3D(0, 200, 0));
        }
        public List<Cube> ToCube()
        {
            return blocks;
        }
        public void Scale(double scaleAmount)
        {
            foreach (Cube cube in blocks)
                cube.Scale(scaleAmount);
        }
        public void BlockLocation(Point3D location)
        {
            foreach (Cube cube in blocks)
            {
                Point3D temp = location;
                Point3D temp2 = cube.Center;
                cube.Translate(temp);
                temp2 = cube.Center;
                //cube.CubeLocation(location);
            }
        }
        /// <summary>
        /// Move the point by the specified amount
        /// </summary>
        /// <param name="shift"></param>
        public void Translate(Point3D shift)
        {
            foreach (Cube cube in blocks)
                cube.Translate(shift);
        }
        public void SnapToGrid()
        {
            foreach (Cube cube in blocks)
                cube.SnapToGrid();
        }
        public Point3D GetRotatePointMaxX()
        {
            Point3D biggest = new Point3D(double.MinValue,0,0);
            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].GetRotatePointMaxX().X > biggest.X)
                    biggest = blocks[i].GetRotatePointMaxX();
            return biggest;
        }
        public Point3D GetRotatePointMinX()
        {
            Point3D smallest = new Point3D(double.MaxValue,0,0);
            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].GetRotatePointMinX().X < smallest.X)
                    smallest = blocks[i].GetRotatePointMinX();
            return smallest;
        }
        public Point3D GetRotatePointMaxZ()
        {
            Point3D biggest = new Point3D(0,0,double.MinValue);
            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].GetRotatePointMaxZ().Z > biggest.Z)
                    biggest = blocks[i].GetRotatePointMaxZ();
            return biggest;
        }
        public Point3D GetRotatePointMinZ()
        {
            Point3D smallest = new Point3D(0,0,double.MaxValue);
            for (int i = 0; i < blocks.Count; i++)
                if (blocks[i].GetRotatePointMinZ().Z < smallest.Z)
                    smallest = blocks[i].GetRotatePointMinZ();
            return smallest;
        }
        public void Draw(Graphics gr, double distance, Point3D lightSrc)
        {
            if (blocks[0].Center.Z > blocks[1].Center.Z)
            {
                Cube temp = blocks[0];
                blocks[0] = blocks[1];
                blocks[1] = temp;
            }
            foreach (Cube cube in blocks)
                cube.Draw(gr, distance, lightSrc);
        }
        public void RotateD(Point3D angle)
        {
            foreach (Cube cube in blocks)
                cube.RotateD(angle);
        }
        public void Rotate(Point3D theta)
        {
            foreach (Cube cube in blocks)
                cube.Rotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotate(Point3D theta)
        {
            foreach (Cube cube in blocks)
                cube.UnRotate(theta);
        }
        /// <summary>
        /// UnRotate the triangle about the origin by the angle theta in radians
        /// </summary>
        /// <param name="theta">Rotation angle in radians</param>
        public void UnRotateD(Point3D angle)
        {
            foreach (Cube cube in blocks)
                cube.UnRotateD(angle);
        }
    }
}
