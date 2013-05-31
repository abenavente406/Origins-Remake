using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Levels
{
    [Serializable]
    public abstract class Level
    {
        protected int realWidth;
        protected int realHeight;
        protected int tileWidth;
        protected int tileHeight;

        public int RealWidth
        {
            get { return realWidth; }
        }

        public int RealHeight
        {
            get { return realHeight; }
        }

        public int WidthInTiles
        {
            get { return realWidth / tileWidth; }
        }

        public int HeightInTiles
        {
            get { return realHeight / tileHeight; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
        }

        public Level(int width, int height, int tileWidth, int tileHeight)
        {
            realWidth = width;
            realHeight = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {

        }
    }
}
