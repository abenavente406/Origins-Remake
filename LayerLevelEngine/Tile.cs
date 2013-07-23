using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LayerLevelEngine
{
    public abstract class Tile
    {
        private Level parent;
        private Vector2 pos;

        private int width;
        private int height;

        private int sheetId;
        private int sourceX;
        private int sourceY;

        public Tile(int width, int height)
            : this(width, height, 0, 0)
        {
        }

        public Tile(int width, int height, int sourceX, int sourceY)
        {
            this.width = width;
            this.height = height;
            this.sourceX = sourceX;
            this.sourceY = sourceY;
        }

        public virtual void Update(GameTime gameTime)
        {
            return;
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
        }
    }
}
