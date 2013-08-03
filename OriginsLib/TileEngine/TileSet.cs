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
using System.IO;
using OriginsLib.Util;

namespace OriginsLib.TileEngine
{
    [Serializable]
    public class TileSet
    {
        #region Fields

        [NonSerialized]
        private Texture2D image;

        private int tileWidthInPixels;
        private int tileHeightInPixels;
        private int widthInTiles;
        private int heightInTiles;
        private Rectangle[] sourceRectangles;
        private string path;

        #endregion

        #region Properties

        /// <summary>
        /// Source image for the tile set
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            private set { image = value; }
        }

        /// <summary>
        /// Width of tiles in the tile set in pixels
        /// </summary>
        public int TileWidth
        {
            get { return tileWidthInPixels; }
            private set { tileWidthInPixels = value; }
        }

        /// <summary>
        /// Height of tiles in the tile set in pixels
        /// </summary>
        public int TileHeight
        {
            get { return tileHeightInPixels; }
            private set { tileHeightInPixels = value; }
        }

        /// <summary>
        /// Width of the tile set in tiles
        /// </summary>
        public int WidthInTiles
        {
            get { return widthInTiles; }
        }

        /// <summary>
        /// Height of the tile set in tiles
        /// </summary>
        public int HeightInTiles
        {
            get { return heightInTiles; }
        }

        /// <summary>
        /// Width of the tile set in pixels
        /// </summary>
        public int WidthInPixels
        {
            get { return image.Width; }
        }

        /// <summary>
        /// Height of the tile set in pixels
        /// </summary>
        public int HeightInPixels
        {
            get { return image.Height; }
        }

        /// <summary>
        /// A copy of the source rectangles of all the tiles in the tile set
        /// </summary>
        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); }
        }

        /// <summary>
        /// Gets the path to the tile set
        /// </summary>
        public string Path
        {
            get { return path == null ? "No path found. {Image:" + image.Name + 
                ", Width In Tiles: " + WidthInTiles + ", Height In Tiles: " + heightInTiles + "}": path; }
            set { path = value; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the tile set
        /// </summary>
        /// <param name="image">Source image</param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public TileSet(Texture2D image, int tileWidth, int tileHeight, string path = null)
        {
            Image = image;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            widthInTiles = WidthInPixels / tileWidth;
            heightInTiles = HeightInPixels / tileHeight;
            this.path = path;

            int tiles = WidthInTiles * HeightInTiles;

            Initialize();
        }

        /// <summary>
        /// Creates a tile set from a path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public TileSet(string path, int tileWidth, int tileHeight)
        {
            var fs = File.Open(path, FileMode.Open);
            var image = Texture2D.FromStream(Config.GraphicsDevice, fs);

            Image = image;

            fs.Close();
            fs.Dispose();

            TileWidth = tileWidth;
            TileHeight = tileHeight;
            widthInTiles = WidthInPixels / tileWidth;
            heightInTiles = HeightInPixels / tileHeight;
            this.path = path;

            Initialize(path);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles initialization of the tile set
        /// </summary>
        public void Initialize(string path = null)
        {
            if (path != null)
            {
                var fs = File.Open(path, FileMode.Open);
                var image = Texture2D.FromStream(Config.GraphicsDevice, fs);

                if (Image == null)
                    Image = image;
            }

            int tiles = WidthInTiles * HeightInTiles;

            sourceRectangles = new Rectangle[tiles];

            int tile = 0;
            for (int y = 0; y < HeightInTiles; y++)
            {
                for (int x = 0; x < WidthInTiles; x++)
                {
                    sourceRectangles[tile] = new Rectangle(x * TileWidth, y * TileHeight,
                                                           TileWidth, TileHeight);
                    ++tile;
                }
            }
        }

        #endregion
    }
}
