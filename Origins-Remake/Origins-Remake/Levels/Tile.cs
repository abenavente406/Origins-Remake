using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Levels
{
    public abstract class Tile
    {
        protected int id = -1;
        public int ID { get { return id; } }

        protected Point pos;
        protected int width;
        protected int height;
        protected Texture2D texture;
        protected bool isSolid = false;

        public Point Position { get { return pos; } }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Texture2D Texture { get { return texture; } }

        public Rectangle Bounds
        {
            get { return new Rectangle(pos.X, pos.Y, width, height); }
        }

        public Tile(Point pos, int width, int height)
        {
            this.pos = pos;
            this.width = width;
            this.height = height;
        }

        public abstract void SetId();
        public abstract void SetTileTexture();
        public abstract void SetTileSolid();

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(texture, Bounds, Color.White);
        }
    }
}
