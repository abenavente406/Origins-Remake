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
    public class Tile
    {
        #region Fields

        private int tileIndex;
        private int tileSet;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of where the tile is on the tile set
        /// </summary>
        public int TileIndex
        {
            get { return tileIndex; }
        }

        /// <summary>
        /// Gets the id of the tile set that the tile resides
        /// </summary>
        public int TileSet
        {
            get { return tileSet; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the tile with the appropriate index and tile set id
        /// </summary>
        /// <param name="tileIndex"></param>
        /// <param name="tileSet"></param>
        public Tile(int tileIndex, int tileSet)
        {
            this.tileIndex = tileIndex;
            this.tileSet = tileSet;
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
