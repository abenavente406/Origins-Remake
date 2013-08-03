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
    /// <summary>
    /// Main class for the tile engine
    /// </summary>
    public class Engine
    {
        #region Fields

        static int tileWidth;
        static int tileHeight;

        #endregion

        #region Properties

        /// <summary>
        /// Width of each tile in pixels
        /// </summary>
        public static int TileWidth
        {
            get { return tileWidth; }
        }

        /// <summary>
        /// Height of each tile in pixels
        /// </summary>
        public static int TileHeight
        {
            get { return tileHeight; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the engine
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        public Engine(int tileWidth, int tileHeight)
        {
            Engine.tileWidth = tileWidth;
            Engine.tileHeight = tileHeight;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the tile position from a vector point.
        /// </summary>
        /// <param name="v"></param>
        /// <returns>Vector / tile dimensions</returns>
        public static Point VectorToCell(Vector2 v)
        {
            return new Point((int)v.X / tileWidth, (int)v.Y / tileHeight);
        }

        /// <summary>
        /// Gets the tile position from a Point point.
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Point / tile dimensions</returns>
        public static Point VectorToCell(Point p)
        {
            return new Point(p.X / tileWidth, p.Y / tileHeight);
        }

        #endregion
    }
}
