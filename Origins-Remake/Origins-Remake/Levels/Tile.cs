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
        protected string name;
        protected Point pos;
        protected int width;
        protected int height;

        [NonSerialized]
        protected Texture2D texture;

        protected bool isSolid = false;

        public Point Position
        {
            get { return pos; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
        
        public Texture2D Texture
        { 
            get { return texture; } 
        }

        public Rectangle GridBounds
        {
            get { return new Rectangle(pos.X, pos.Y, width, height); }
        }

        public Rectangle RealBounds
        {
            get { return new Rectangle(pos.X * width, pos.Y * height, width, height); }
        }

        public Tile(Point pos, int width, int height)
        {
            this.pos = pos;
            this.width = width;
            this.height = height;

            SetTileTexture();
            SetTileSolid();
        }

        public abstract void SetTileTexture();
        public abstract void SetTileSolid();

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(texture, GridBounds, Color.White);
        }
    }
}
