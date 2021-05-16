using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics3D
{
    class Level
    {
        #region Parameters
        char[,] startUpInfo;
        Size gridsize;
        List<Tile> tilelist = new List<Tile>();
        Block block = new Block();
        Tile winTile = new Tile();
        Point3D blockLocation = new Point3D();
        Point3D originalBlockLocation = null;
        #endregion

        #region Constuctor
        public Level(Size gridSize)
        {
            startUpInfo = new char[gridSize.Width, gridSize.Height];
            this.gridsize = new Size(gridSize.Width,gridSize.Height);
        }
        #endregion

        #region Class Properties

        public List<Tile> TotalCubes { get { return tilelist; } }
        public Block getBlock { get { return block; } }
        public Tile WinTile { get { return winTile; } }
        #endregion

        #region Class Methods
        /// <summary>
        /// Resets the map
        /// </summary>
        public void Reset()
        {
            tilelist.Clear();
            block = new Block();
            for(int x = 0; x < gridsize.Width;x++)
                for (int z = 0; z < gridsize.Height; z++)
                { 
                    if(startUpInfo[x,z] != null)
                    SetCell( x,  z, startUpInfo[x,z]);
                }
        }
        /// <summary>
        /// Sets the cellType of the x,y location in the gamegrid
        /// </summary>
        /// <param name="x">the x-location</param>
        /// <param name="z">the z-location</param>
        /// <param name="cc">the character from the BlockNRoll Map</param>
        public void SetStartUp(int x, int z, char cc)
        {
            startUpInfo[x, z] = cc; 
        }
        /// <summary>
        /// Sets the cellType of the x,y location in the gamegrid
        /// </summary>
        /// <param name="x">the x-location</param>
        /// <param name="z">the z-location</param>
        /// <param name="cc">the character from the BlockNRoll Map</param>
        private void SetCell(int x, int z, char cc)
        {
            switch (cc)
            {
                case 'T':
                    Tile tempt = new Tile();
                    tempt.Translate(new Point3D(x * 100, 10, z * 100));
                    tilelist.Add(tempt);
                    break;
                case 'S':
                    block = new Block();
                    blockLocation = new Point3D(x * 100, 0, z  * 100);
                    if (originalBlockLocation == null)
                        originalBlockLocation = blockLocation + new Point3D();
                    block.BlockLocation(originalBlockLocation);
                    block.BlockLocation(originalBlockLocation);
                    tempt = new Tile();
                    tempt.Translate(new Point3D(x * 100, 10, z * 100));
                    tilelist.Add(tempt);
                    break;
                case 'F':
                    winTile = new Tile();
                    winTile.Translate(new Point3D(x * 100, 10, z * 100));
                    winTile.Color = Color.Aqua;
                    break;
                case 'B':
                    break;

                case ' ':
                    break;
            }
        }

        #endregion
    }
}


