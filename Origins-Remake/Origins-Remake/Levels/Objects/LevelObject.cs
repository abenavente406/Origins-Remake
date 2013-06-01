using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.Levels.Objects
{
    [Serializable]
    public abstract class LevelObject
    {
        protected Vector2 pos;
        protected int width;
        protected int height;
        protected bool isSolid = false;

        [NonSerialized]
        protected Texture2D texture;

        public Vector2 Position
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

        public bool IsSolid
        {
            get { return isSolid; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, width, height); }
        }

        public LevelObject()
        {
            pos = Vector2.Zero;
            width = 1;
            height = 1;

            SetTexture();
            SetSolid();
        }

        public abstract void SetTexture();
        public abstract void SetSolid();

        public virtual void Update(GameTime gameTime)
        {
            return;
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(texture, Position, Color.White);
        }
    }
}
