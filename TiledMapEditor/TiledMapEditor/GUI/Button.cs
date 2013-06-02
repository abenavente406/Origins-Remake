using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TiledMapEditor.GUI
{
    public class Button
    {
        public event EventHandler OnClick;
        public event EventHandler OnHover;

        protected bool clicked = false;
        protected Texture2D texture;

        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; set; }

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            OnHover += Hover;
        }

        public virtual void Update()
        {
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

            var mouse = Mouse.GetState();
            var mousePos = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mousePos.Intersects(Bounds) && mouse.LeftButton == ButtonState.Pressed && !clicked)
            {
                clicked = true;
                OnClick(this, null);
            }
            else
            {
                clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

            var mouse = Mouse.GetState();
            var mousePos = new Rectangle(mouse.X, mouse.Y, 1, 1);

            spriteBatch.Draw(texture, Position, Color.White);

            if (mousePos.Intersects(Bounds))
                OnHover(this, null);
        }

        protected void Hover(object sender, EventArgs e)
        {
            Game1.spriteBatch.Draw(texture, Position, new Color(255, 0, 0, 180));
        }

    }
}
