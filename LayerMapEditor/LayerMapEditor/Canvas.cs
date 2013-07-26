using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary.Shapes;
using OriginsLibrary.Util;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Input;

namespace LayerMapEditor
{
    public class Canvas
    {
        public const int CANVAS_WIDTH = 1100;
        public const int CANVAS_HEIGHT = 768;

        private MainGame gameRef;
        private Vector2 startPos = new Vector2(1366 - CANVAS_WIDTH, 0);
        private Rectangle bounds;
        private Texture2D background;
        private DrawableRectangle border;

        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public Canvas(Game game)
        {
            gameRef = (MainGame)game;
            bounds = new Rectangle((int)startPos.X, (int)startPos.Y, MainGame.WINDOW_WIDTH, CANVAS_HEIGHT);
        }

        public void Initialize()
        {
        }

        public void LoadContent()
        {
            background = new DrawableRectangle(gameRef.GraphicsDevice, 
                new Vector2(32, 32), Color.White, true).Texture;

            border = new DrawableRectangle(gameRef.GraphicsDevice,
                new Vector2(CANVAS_WIDTH, CANVAS_HEIGHT), Color.White,
                false);
        }

        List<Vector2> positions = new List<Vector2>();

        public void Update(GameTime gameTime)
        {
            var mousePos = InputHandler.MousePos;
            var transformedMousePos = new Vector2(mousePos.X, mousePos.Y) - Camera.Position;

            var pan = InputHandler.KeyDown(Keys.Space);

            if (pan)
            {
                if (InputHandler.MouseButtonDown(MouseButton.LeftButton))
                {
                    positions.Add(new Vector2((int)transformedMousePos.X / 32,
                        (int)transformedMousePos.Y / 32));
                }
            }
            else
            {
            }
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Begin();
            batch.Draw(background, 
                new Rectangle((int)startPos.X, (int)startPos.Y, CANVAS_WIDTH, CANVAS_HEIGHT),
                Color.CornflowerBlue);
            border.Draw(batch, startPos, Color.Gray);
            batch.End();

            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransform());
            foreach (Vector2 v in positions)
            {
                batch.Draw(background, new Rectangle((int)v.X * 32, (int)v.Y * 32, 32, 32), Color.Black);
            }
            batch.End();
        }
    }
}
