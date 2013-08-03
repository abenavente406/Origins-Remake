using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OriginsLib.TileEngine
{
    [Serializable]
    public class MapLayer
    {
        #region Fields

        private Tile[,] map;
        private TileMap parent;
        private bool isVisible = true;
        private float opacity = 1.0f;
        
        #endregion

        #region Properties

        /// <summary>
        /// Returns the width of the layer in tiles
        /// </summary>
        public int WidthInTiles
        {
            get { return parent.WidthInTiles; }
        }

        /// <summary>
        /// Returns the height of the layer in tiles
        /// </summary>
        public int HeightInTiles
        {
            get { return parent.HeightInTiles; }
        }

        /// <summary>
        /// Gets whether the layer is visible or not
        /// </summary>
        public bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// Gets the opacity of the level
        /// </summary>
        public float Opacity
        {
            get { return opacity; }
            set { opacity = MathHelper.Clamp(value, 0, 1); }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new map layer with an existing map
        /// </summary>
        /// <param name="map"></param>
        public MapLayer(TileMap parent, Tile[,] map)
        {
            this.parent = parent;
            this.map = (Tile[,])map.Clone();
        }

        /// <summary>
        /// Initializes a new map layer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MapLayer(TileMap parent)
        {
            this.parent = parent;
            map = new Tile[HeightInTiles, WidthInTiles];

            for (int y = 0; y < HeightInTiles; y++)
            {
                for (int x = 0; x < WidthInTiles; x++)
                {
                    map[y, x] = new Tile(-1, -1);
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the tile at the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                  y < 0 || x < 0)
                return null;

            return map[y, x];   
        }

        /// <summary>
        /// Sets the tile at the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="t">Tile to replace the selected tile</param>
        public void SetTile(int x, int y, Tile t)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                y < 0 || x < 0)
                return;

            map[y, x] = t;
        }

        /// <summary>
        /// Sets the tile at the position by creating a tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index">Index of the tile</param>
        /// <param name="tileSet">Tile set of the tile</param>
        public void SetTile(int x, int y, int index, int tileSet)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                  y < 0 || x < 0)
                return;

            map[y, x] = new Tile(index, tileSet);
        }

        #endregion
    }
}
