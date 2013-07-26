using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;
using Microsoft.Xna.Framework.Input;
using GameHelperLibrary;

namespace LayerLevelEngine
{
    public class LevelTest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch batch;

        Level level;

        public LevelTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            batch = new SpriteBatch(GraphicsDevice);

            SheetManager.Initialize(this);

            Components.Add(new InputHandler(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            level = new Level(@"D:\level.slf");

            Camera.Initialize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 
                level.WidthInPixels, level.HeightInPixels);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            level.Update(gameTime);

            if (InputHandler.KeyPressed(Keys.F11))
                level.Save(@"D:\level.slf");

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Camera.SetPosition(Camera.Position + new Vector2(-5, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Camera.SetPosition(Camera.Position + new Vector2(5, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Camera.SetPosition(Camera.Position + new Vector2(0, -5));
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Camera.SetPosition(Camera.Position + new Vector2(0, 5));

            if (Keyboard.GetState().IsKeyDown(Keys.Add))
                Camera.Zoom += .01f;

            if (Keyboard.GetState().IsKeyDown(Keys.Subtract))
                Camera.Zoom -= .01f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransform());
            level.Draw(batch, gameTime);
            batch.End();

            base.Draw(gameTime);
        }
    }
}
