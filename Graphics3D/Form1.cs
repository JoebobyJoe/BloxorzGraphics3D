using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Reflection;


namespace Graphics3D
{
    enum Face { foreground, background }

    public partial class Form1 : Form
    {
        #region Program Variables
        //for file game
        Size gridSize = new Size();
        Level gameGrid;
        int gridnum = 0, NumReset = 0,numMoves = 0;
        ArrayList gameGrids = new ArrayList();
        ArrayList gridList = new ArrayList();


        List<Line3D> lines = new List<Line3D>();
        List<Sphere3D> spheres = new List<Sphere3D>();
        List<Tile> tiles = new List<Tile>();
        List<Point3D> blockRoll = new List<Point3D>();
        // Cube cube = new Cube();
        Grid grid = new Grid();
        Block block;
        Point3D totalBlockRotation = new Point3D(0, 0, 0);
        Tile winTile;

        Point3D angle = new Point3D(); // the angle of rotation in degrees
        Point3D lightSrc;
        Sphere3D sun;

        Random rand = new Random(0);
        int distance = 7000;
        double scale = 1.1f;
        bool pause = false;
        List<Cube> cubes = new List<Cube>();
        bool shouldFall = false;
        #endregion

        #region Startup
        /// <summary>
        /// Form Constructor - all initialization happens here - AFTER the InitializeComponent function.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // to push the output onto a specific monitor (parameter = 0 for primary, = 1 for secondary monitor, etc)
            ShowOnMonitor(0);
            timer1.Enabled = true;
            // create the demo objects
            //  MakeHouse(lines);
            // cube.Scale(.5f);
            //  cube.Translate(new Point3D(0, 50, 0));

            // foreach (Line3D line in lines)
            //{
            //    //line.translate(new Point3D(0, 0, 0));
            //    line.Scale(scale);
            //}

            //variables for file reading

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Graphics3D.BlockNRollSpecs.bnr";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                ReadFile(reader);
                Setup_Grid(gridnum);
            }

           // AxisLines();

            // make a collection of spheres
            // MakeSpheres();

            lightSrc = new Point3D(650, 1100, 650);
            sun = new Sphere3D(lightSrc, 200);
            sun.Brush = new SolidBrush(Color.Yellow);
        }

        private void StartUp()
        {
            Size temp = (Size)gridList[gridnum];
            tiles = new List<Tile>();
            for (int i = 0; i < gameGrid.TotalCubes.Count; i++)
            {
                tiles.Add(gameGrid.TotalCubes[i]);
                tiles[i].Translate(new Point3D(-temp.Width / 2 * 100, -7.5, -temp.Height / 2 * 100));
                tiles[i].Color = Color.Gray;
                tiles[i].Pen = new Pen(Color.Gray);
            }

            winTile = new Tile();
            winTile = gameGrid.WinTile;
            winTile.Color = Color.Aqua;
            winTile.Translate(new Point3D(-temp.Width / 2 * 100, -7.5, -temp.Height / 2 * 100));
            tiles.Add(winTile);

            block = new Block();
            block = gameGrid.getBlock;
            block.Scale(.5f);
            block.BlockLocation(new Point3D(-temp.Width / 2 * 100, -150, -temp.Height / 2 * 100));
            angle.X = 10;
            cubes.Clear();
            foreach (Tile tile in tiles)
                cubes.Add(tile.ToCube());
            cubes.AddRange(block.ToCube());
            totalBlockRotation = new Point3D();
        }

        private void AxisLines()
        {
            if (true)
            {
                lines.Add(new Line3D(new Point3D(0, 0, 0), new Point3D(200, 0, 0))); // x axis
                lines[lines.Count - 1].Pen = Pens.Red;
                lines.Add(new Line3D(new Point3D(0, 0, 0), new Point3D(0, -(200), 0))); // y axis
                lines[lines.Count - 1].Pen = Pens.Blue;
                lines.Add(new Line3D(new Point3D(0, 0, 0), new Point3D(0, 0, 200))); // z axis
                lines[lines.Count - 1].Pen = Pens.Green;
            }
        }
        /// <summary>
        /// Sets up the grid
        /// </summary>
        /// <param name="gridnum"></param>
        private void Setup_Grid(int gridnum)
        {
            //gameGrid = new Level();
            foreach (Cube cube in cubes)
                cube.UnRotateD(totalBlockRotation);
            angle.X = 10;
            gameGrid = (Level)gameGrids[gridnum];
            gameGrid.Reset();
            StartUp();
            blockRoll.Clear();
        }

        private void ReadFile(StreamReader fileInput)
        {
            char[] lineSplitter = new char[] { ',', ' ' }; // to split lines with spaces and commas

            // read in the file data
            while (!fileInput.EndOfStream)
            {
                // read in one line
                string lineOfText = fileInput.ReadLine();
                if (lineOfText[0] == '-')//ignore a line beginning with a dash
                    continue;

                string[] data = lineOfText.Split(lineSplitter, StringSplitOptions.RemoveEmptyEntries);
                gridSize = new Size(Convert.ToInt16(data[0]), Convert.ToInt16(data[1]));
                gridList.Add(gridSize);
                gameGrid = new Level(gridSize);

                for (int row = 0; row < gridSize.Height; row++)
                {
                    lineOfText = fileInput.ReadLine();
                    // parse the entire line, one charater at a time
                    for (int x = 0; x < lineOfText.Length; x++)
                        gameGrid.SetStartUp(x, row, lineOfText[x]);
                }
                gameGrids.Add(gameGrid);
            }
        }

        private static void MakeHouse(List<Line3D> lines)
        {
            // the four lines of the front face of a cube
            lines.Add(new Line3D(new Point3D(-100, -100, 100), new Point3D(100, -100, 100)));
            lines.Add(new Line3D(new Point3D(100, -100, 100), new Point3D(100, 100, 100)));
            lines.Add(new Line3D(new Point3D(100, 100, 100), new Point3D(-100, 100, 100)));
            lines.Add(new Line3D(new Point3D(-100, 100, 100), new Point3D(-100, -100, 100)));

            // the four lines of the back face of a cube
            lines.Add(new Line3D(new Point3D(-100, -100, -100), new Point3D(100, -100, -100)));
            lines.Add(new Line3D(new Point3D(100, -100, -100), new Point3D(100, 100, -100)));
            lines.Add(new Line3D(new Point3D(100, 100, -100), new Point3D(-100, 100, -100)));
            lines.Add(new Line3D(new Point3D(-100, 100, -100), new Point3D(-100, -100, -100)));

            // the four line from the back face to the front face
            lines.Add(new Line3D(new Point3D(-100, -100, -100), new Point3D(-100, -100, 100)));
            lines.Add(new Line3D(new Point3D(100, -100, -100), new Point3D(100, -100, 100)));
            lines.Add(new Line3D(new Point3D(100, 100, -100), new Point3D(100, 100, 100)));
            lines.Add(new Line3D(new Point3D(-100, 100, -100), new Point3D(-100, 100, 100)));

            //the roof
            int roofHight = -200;
            lines.Add(new Line3D(new Point3D(0, roofHight, -100), new Point3D(0, roofHight, 100)));
            lines.Add(new Line3D(new Point3D(0, roofHight, 100), new Point3D(100, -100, 100)));
            lines.Add(new Line3D(new Point3D(0, roofHight, 100), new Point3D(-100, -100, 100)));
            lines.Add(new Line3D(new Point3D(0, roofHight, -100), new Point3D(100, -100, -100)));
            lines.Add(new Line3D(new Point3D(0, roofHight, -100), new Point3D(-100, -100, -100)));

            //the door
            lines.Add(new Line3D(new Point3D(10, 50, 100), new Point3D(-10, 50, 100)));
            lines.Add(new Line3D(new Point3D(10, 50, 100), new Point3D(10, 100, 100)));
            lines.Add(new Line3D(new Point3D(-10, 50, 100), new Point3D(-10, 100, 100)));
            lines.Add(new Line3D(new Point3D(2, 75, 100), new Point3D(3, 75, 100)));

            //the left window
            lines.Add(new Line3D(new Point3D(-50, 0, 100), new Point3D(-10, 0, 100)));
            lines.Add(new Line3D(new Point3D(-50, -30, 100), new Point3D(-10, -30, 100)));
            lines.Add(new Line3D(new Point3D(-50, -30, 100), new Point3D(-50, 0, 100)));
            lines.Add(new Line3D(new Point3D(-10, -30, 100), new Point3D(-10, 0, 100)));

            //the right window
            lines.Add(new Line3D(new Point3D(50, 0, 100), new Point3D(10, 0, 100)));
            lines.Add(new Line3D(new Point3D(50, -30, 100), new Point3D(10, -30, 100)));
            lines.Add(new Line3D(new Point3D(50, -30, 100), new Point3D(50, 0, 100)));
            lines.Add(new Line3D(new Point3D(10, -30, 100), new Point3D(10, 0, 100)));

            //the side window
            lines.Add(new Line3D(new Point3D(100, 80, 80), new Point3D(100, 80, -80)));
            lines.Add(new Line3D(new Point3D(100, 0, 80), new Point3D(100, 0, -80)));
            lines.Add(new Line3D(new Point3D(100, 80, 80), new Point3D(100, 0, 80)));
            lines.Add(new Line3D(new Point3D(100, 80, -80), new Point3D(100, 0, -80)));

            lines.Add(new Line3D(new Point3D(-100, 80, 80), new Point3D(-100, 80, -80)));
            lines.Add(new Line3D(new Point3D(-100, 0, 80), new Point3D(-100, 0, -80)));
            lines.Add(new Line3D(new Point3D(-100, 80, 80), new Point3D(-100, 0, 80)));
            lines.Add(new Line3D(new Point3D(-100, 80, -80), new Point3D(-100, 0, -80)));
        }

        private void MakeSpheres()
        {
            int sphereSize = 80;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                    {
                        Sphere3D sphere = new Sphere3D(new Point3D(-sphereSize + sphereSize * i, -sphereSize + sphereSize * j, -sphereSize + sphereSize * k), 20);
                        sphere.Pen = new Pen(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)), 2);
                        sphere.Brush = new SolidBrush(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));
                        spheres.Add(sphere);
                    }
        }

        /// <summary>
        /// If there is any initialization after the form is loaded - do it here.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// This method provides quick assistance to put the output onto a specific monitor
        /// </summary>
        /// <param name="showOnMonitor">showOnMonitor = 0 for primary, = 1 for secondary monitor, etc</param>
        private void ShowOnMonitor(int showOnMonitor)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            this.Left = sc[showOnMonitor].Bounds.Left;
            this.Top = sc[showOnMonitor].Bounds.Top;
            this.StartPosition = FormStartPosition.Manual;

            this.Show();
        }
        #endregion

        #region User Interface Inputs: Keyboards, mouse
        /// <summary>
        /// Key press processing
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Dispose();
                    break;
                case Keys.W:
                    angle.X += 1;
                    break;
                case Keys.S:
                    angle.X -= 1;
                    break;
                case Keys.A:
                    angle.Y -= 1;
                    break;
                case Keys.D:
                    angle.Y += 1;
                    break;
                case Keys.Q:
                    angle.Z -= 1;
                    break;
                case Keys.E:
                    angle.Z += 1;
                    break;
                case Keys.X:
                    Exploade();
                    break;
                case Keys.PageUp:
                    distance += distance / 2;
                    break;
                case Keys.PageDown:
                    distance -= distance / 2;
                    break;
                case Keys.P:
                    if (pause)
                        pause = false;
                    else
                        pause = true;
                    break;
                case Keys.Right:
                    blockRoll.Add(new Point3D(0, 0, 90));
                    numMoves++;
                    break;
                case Keys.Left:
                    blockRoll.Add(new Point3D(0, 0, -90));
                    numMoves++;
                    break;
                case Keys.Up:
                    blockRoll.Add(new Point3D(-90, 0, 0));
                    numMoves++;
                    break;
                case Keys.Down:
                    blockRoll.Add(new Point3D(90, 0, 0));
                    numMoves++;
                    break;
                case Keys.Insert:
                    if (gridnum < gameGrids.Count - 1)
                        Setup_Grid(++gridnum);
                    NumReset = 0;
                    numMoves = 0;
                    break;
                case Keys.Delete:
                    if (gridnum > 0)
                        Setup_Grid(--gridnum);
                    NumReset = 0;
                    numMoves = 0;
                    break;
                case Keys.R:
                    NumReset++;
                    Setup_Grid(gridnum);
                    break;

            }
            if (distance == 100)
                distance += 1;
        }
        #endregion

        #region User Interface Output: Paint and related methods
        /// <summary>
        /// All painting to the screen must happen in here.
        /// You don't do any calculations in here - just painting.
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.TranslateTransform(ClientRectangle.Width / 2, ClientRectangle.Height / 2);
            foreach (Line3D line in lines)
                line.Draw(gr, distance);
            //sort the spheres before painting
            SortSphere(spheres);


            // sorts the cubes so that the one directly in front are drawn first
            QuickSortCube(cubes, 0, cubes.Count - 1);
            for (int i = 0; i < cubes.Count; i++)
            {
                Cube temp = cubes[i];
                temp.Draw(gr, distance, lightSrc);
            }
            //foreach (Cube cube in cubes)
            //    cube.Draw(gr, distance, lightSrc);
            foreach (Line3D line in lines)
                line.Draw(gr, distance);
            sun.Fill(gr, distance);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Sphere3D sphere in spheres)
                sphere.Center.UnRotateD(totalBlockRotation);
            foreach (Cube cube in cubes)
                cube.UnRotateD(totalBlockRotation);

            if (shouldFall)
                block.Translate(new Point3D(0, 1, 0));
            if (Math.Round(block.ToCube()[0].Center.Y) == -25)
            {
                shouldFall = false;
                Setup_Grid(gridnum);
            }
            totalBlockRotation += angle;

            if (blockRoll.Count > 0 && !shouldFall)
                PerformBlockRoll();
            if (pause)
                return;
            foreach (Sphere3D sphere in spheres)
                sphere.Center.RotateD(totalBlockRotation);
            sun.Center.RotateD(new Point3D(0, 0, .5));
            foreach (Cube cube in cubes)
                cube.RotateD(totalBlockRotation);
            angle = new Point3D();

            this.Invalidate();
        }
        /// <summary>
        /// checks the first cube of the block to see if it is on a tile
        /// </summary>
        /// <returns>True = on block</returns>
        private bool BlockFall1()
        {
            bool temp = true;
            foreach (Tile tile in tiles)
            {
                Cube cube1 = block.ToCube()[0];
                int cubeX1 = (int)Math.Round(cube1.Center.X);
                int cubeZ1 = (int)Math.Round(cube1.Center.Z);
                int tileZ = (int)Math.Round(tile.Center.Z);
                int tileX = (int)Math.Round(tile.Center.X);
                if (cubeX1 == tileX && cubeZ1 == tileZ)
                    temp = false;
            }
            return temp;
        }
        /// <summary>
        /// checks the second cube of the block to see if it is on a tile
        /// </summary>
        /// <returns>True = on block</returns>
        private bool BlockFall2()
        {
            bool temp = true;
            foreach (Tile tile in tiles)
            {
                Cube cube2 = block.ToCube()[1];
                int cubeX2 = (int)Math.Round(cube2.Center.X);
                int cubeZ2 = (int)Math.Round(cube2.Center.Z);
                int tileZ = (int)Math.Round(tile.Center.Z);
                int tileX = (int)Math.Round(tile.Center.X);
                if (cubeX2 == tileX && cubeZ2 == tileZ)
                    temp = false;
            }
            return temp;
        }
        /// <summary>
        ///  unrotate the total angle 
        /// add the angle to the total angle
        ///  make the block roll
        /// rerotate the total angle
        /// </summary>
        private void PerformBlockRoll()
        {
            Point3D roll = blockRoll[0];
            if (roll.Z > 0)
            {
                double RotatePointX = block.GetRotatePointMaxX().X;
                // get the right most x value where y = 0
                // translate by the -ve of the x value
                block.Translate(new Point3D(-RotatePointX, 0, 0));
                block.RotateD(new Point3D(0, 0, 6));
                // translate by the x value back
                block.Translate(new Point3D(RotatePointX, 0, 0));
                roll.Z -= 6;
            }

            if (roll.Z < 0)
            {
                double RotatePointX = block.GetRotatePointMinX().X;
                block.Translate(new Point3D(-RotatePointX, 0, 0));
                block.RotateD(new Point3D(0, 0, -6));
                block.Translate(new Point3D(RotatePointX, 0, 0));
                roll.Z += 6;
            }

            if (roll.X > 0)
            {
                double RotatePointZ = block.GetRotatePointMaxZ().Z;
                // get the right most x value where y = 0
                // translate by the -ve of the x value
                block.Translate(new Point3D(0, 0, -RotatePointZ));
                block.RotateD(new Point3D(6, 0, 0));
                // translate by the x value back
                block.Translate(new Point3D(0, 0, RotatePointZ));
                roll.X -= 6;
            }

            if (roll.X < 0)
            {
                double RotatePointZ = block.GetRotatePointMinZ().Z;
                // get the right most x value where y = 0
                // translate by the -ve of the x value
                block.Translate(new Point3D(0, 0, -RotatePointZ));
                block.RotateD(new Point3D(-6, 0, 0));
                // translate by the x value back
                block.Translate(new Point3D(0, 0, RotatePointZ));
                roll.X += 6;
            }

            if (roll.Magnitude == 0)
            {
                blockRoll.RemoveAt(0);
                if (BlockFall1() || BlockFall2())
                    shouldFall = true;
                if (WinCheck())
                {
                    MessageBox.Show("You Win Total moves: " + numMoves.ToString() + " number of resets : " + NumReset.ToString());
                    if (gridnum < gameGrids.Count - 1)
                        Setup_Grid(++gridnum);
                }
                block.SnapToGrid();
            }
        }
        /// <summary>
        /// checks to see if the block is on the win tile
        /// </summary>
        /// <returns></returns>
        private bool WinCheck()
        {
            Cube cube1 = block.ToCube()[0];
            bool wincheck = false;
            if (IsStanding())
                if (Math.Round(winTile.Center.Z) == Math.Round(cube1.Center.Z))
                    if (Math.Round(winTile.Center.X) == Math.Round(cube1.Center.X))
                        wincheck = true;
            return wincheck;
        }
        /// <summary>
        /// checks to see if the block is standing
        /// </summary>
        /// <returns></returns>
        private bool IsStanding()
        {
            Cube cube1 = block.ToCube()[0];
            Cube cube2 = block.ToCube()[1];
            if (Math.Round(cube1.Center.Z) == Math.Round(cube2.Center.Z))
                if (Math.Round(cube1.Center.X) == Math.Round(cube2.Center.X))
                    return true;
            return false;
        }
        private void Exploade()
        {
            foreach (Cube cube in cubes)
                cube.Translate(new Point3D(rand.Next(-500, 500), rand.Next(-500, 500), rand.Next(-500, 500)));
        }

        /// <summary>
        /// Sort the spheres in z-order
        /// </summary>
        /// <param name="spheres"></param>
        private void SortSphere(List<Sphere3D> spheres)
        {
            for (int i = 0; i < spheres.Count; i++)
                for (int j = i + 1; j < spheres.Count; j++)
                    if (spheres[i].Center.Z > spheres[j].Center.Z)
                    {
                        Sphere3D temp = spheres[i];
                        spheres[i] = spheres[j];
                        spheres[j] = temp;
                    }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X - ClientRectangle.Width / 2;
            int y = e.Y - ClientRectangle.Height / 2;
            for (int i = tiles.Count - 1; i >= 0; i--)
            {
                Tile tile = tiles[i];
                if (tile.Contains(new Point2D(x, y), distance))
                {
                    tiles.Remove(tile);
                    return;
                }
            }
        }
        /// <summary>
        /// Qucksort algorithm - using a recursive method
        /// </summary>
        /// <param name="data">The data to be sorted</param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private void QuickSortCube(List<Cube> cube, int left, int right)
        {
            Point3D viewer = new Point3D(0, 0, distance);
            // set the left/right boundaries
            int i = left, j = right;
            // pivot point is the middle data value
            Cube pivot = cube[(left + right) / 2];
            while (i <= j)
            {
                while ((cube[i].Center - viewer).Magnitude > (pivot.Center - viewer).Magnitude)
                    i++;

                while ((cube[j].Center - viewer).Magnitude < (pivot.Center - viewer).Magnitude)
                    j--;
                if (i <= j)
                {
                    // Swap
                    Cube temp = cube[i];
                    cube[i] = cube[j];
                    cube[j] = temp;

                    i++;
                    j--;
                }
            }
            // Recursive calls
            if (left < j)
                QuickSortCube(cube, left, j);

            if (i < right)
                QuickSortCube(cube, i, right);
        }
    }
}
