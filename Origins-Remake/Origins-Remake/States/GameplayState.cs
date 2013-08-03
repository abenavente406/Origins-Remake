using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using Origins_Remake.Levels;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Util;
using Origins_Remake.Entities;
using GameHelperLibrary.Controls;
using OriginsLib.Util;

namespace Origins_Remake.States
{
    public class GameplayState : BaseGameState
    {
        enum State
        {
            Playing,
            Paused
        }

        State currentState = State.Playing;

        LevelManager levelManager;
        EntityManager entityManager;
        GaussianBlur blurOverlay;

        ControlManager pauseControls;

        RenderTarget2D mainTarget;
        RenderTarget2D target1;
        RenderTarget2D target2;
        int renderTargetWidth;
        int renderTargetHeight;

        public GameplayState(Game game, GameStateManager manager)
            : base(game, manager) 
        {
        
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void LargeLoadContent(object sender)
        {
            levelManager = new LevelManager(gameRef);
            Camera.Initialize(Vector2.Zero, new Rectangle(0, 0, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT),
                new Vector2(LevelManager.CurrentLevel.WidthInPixels, LevelManager.CurrentLevel.HeightInPixels));
            Camera.MaxClamp -= new Vector2(Camera.View.Width / 2, Camera.View.Height / 2);

            entityManager = new EntityManager(gameRef);

            pauseControls = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>("Fonts\\default"));
            {
                Label lblPauseDisplay = new Label()
                {
                    Name = "lblPauseDisplay",
                    Text = "P A U S E D",
                    Color = Color.White
                };
                lblPauseDisplay.Position = new Vector2(MainGame.GAME_WIDTH / 2 - lblPauseDisplay.Width / 2,
                    MainGame.GAME_HEIGHT / 1.8f - lblPauseDisplay.Height - 10);

                var back = new LinkLabel(1) { Name = "lnklblBack", Text = "Resume" };
                var quit = new LinkLabel(2) { Name = "lnklblQuit", Text = "Quit to Menu" };

                pauseControls.Add(lblPauseDisplay);
                pauseControls.Add(back);
                pauseControls.Add(quit);

                Vector2 startPos = new Vector2(MainGame.GAME_WIDTH / 2, MainGame.GAME_HEIGHT / 1.8f);
                foreach (Control c in pauseControls)
                {
                    if (c is LinkLabel)
                    {
                        var l = (LinkLabel)c;
                        var offset = new Vector2(c.Width / 2, 0);
                        c.Position = startPos - offset;
                        c.Selected += new EventHandler(LinkLabel_Selected);
                        c.Effect = ControlEffect.PULSE;
                        c.Color = Color.Black;
                        l.OnMouseIn += LinkLabel_OnMouseIn;

                        startPos.Y += c.Height + 10f;
                    }
                }
            }

            blurOverlay = new GaussianBlur(gameRef);
            renderTargetWidth = MainGame.GAME_WIDTH;
            renderTargetHeight = MainGame.GAME_HEIGHT;
            blurOverlay.ComputeKernel(7, 2.0f);

            mainTarget = new RenderTarget2D(gameRef.GraphicsDevice, MainGame.GAME_WIDTH, MainGame.GAME_HEIGHT);
            target1 = new RenderTarget2D(gameRef.GraphicsDevice, renderTargetWidth, renderTargetHeight, false,
                gameRef.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);
            target2 = new RenderTarget2D(gameRef.GraphicsDevice, renderTargetWidth, renderTargetHeight, false,
                gameRef.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);

            blurOverlay.ComputeOffsets(renderTargetWidth, renderTargetHeight);

            base.LargeLoadContent(sender);
        }

        protected override void UnloadContent()
        {
            mainTarget.Dispose();
            target1.Dispose();
            target2.Dispose();
            
            base.UnloadContent();
        }

        private void LinkLabel_Selected(object sender, EventArgs e)
        {
            LinkLabel lbl = (LinkLabel)sender;

            if (lbl.Name == "lnklblBack")
                currentState = State.Playing;
            else if (lbl.Name == "lnklblQuit")
                SwitchStateWithFade(new MainMenuState(gameRef, StateManager));
        }

        private void LinkLabel_OnMouseIn(object sender, EventArgs e)
        {
            var lbl = (LinkLabel)sender;

            switch (currentState)
            {
                case State.Playing:
                    break;
                case State.Paused:
                    pauseControls.SelectControl(lbl.Index);
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentState == State.Playing)
            {
#if DEBUG
                if (InputHandler.KeyDown(Keys.OemPlus)) Camera.Zoom -= .01f;
                if (InputHandler.KeyDown(Keys.OemMinus)) Camera.Zoom += .01f;
#endif
                LevelManager.Update(gameTime);
                EntityManager.UpdateAll(gameTime);

                if (InputHandler.KeyPressed(Keys.Escape))
                {
                    currentState = State.Paused;
                }
            }
            else
            {
                pauseControls.Update(gameTime, playerIndexInControl);

                if (InputHandler.KeyPressed(Keys.Escape))
                {
                    currentState = State.Playing;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (currentState == State.Playing)
            {
                gameRef.GraphicsDevice.SetRenderTarget(mainTarget);
                gameRef.GraphicsDevice.Clear(Color.Transparent);
                var spriteBatch = gameRef.spriteBatch;

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransform());

                LevelManager.Draw(spriteBatch, gameTime);
                EntityManager.Draw(spriteBatch, gameTime);

                spriteBatch.End();
                gameRef.GraphicsDevice.SetRenderTarget(null);

                spriteBatch.Begin();
                spriteBatch.Draw(mainTarget, Vector2.Zero, Color.White);
                spriteBatch.End();
            }
            else
            {
                var spriteBatch = gameRef.spriteBatch;
                Texture2D result = blurOverlay.PerformGaussianBlur((Texture2D)mainTarget,
                    target1, target2, spriteBatch);

                spriteBatch.Begin();
                spriteBatch.Draw(result, Vector2.Zero, Color.White);
                pauseControls.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }

            gameRef.spriteBatch.Begin();
            FadeOutRect.Draw(gameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            gameRef.spriteBatch.End();
        }
    }
}
