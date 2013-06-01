using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary.Shapes;
using System.Threading;
using GameHelperLibrary.Controls;

namespace Origins_Remake.States
{
    public class LoadingState : BaseGameState
    {
        GameState nextState;
        Label loading;

        public LoadingState(Game game, GameStateManager manager, GameState nextState)
            : base(game, manager) { this.nextState = nextState; }

        protected override void LoadContent()
        {
            base.LoadContent();

            loading = new Label()
            {
                Name = "lblLoading",
                Text = "Loading...",
                Effect = ControlEffect.PULSE
            };
            loading.Position = new Vector2(MainGame.GAME_WIDTH / 2 - loading.SpriteFont.MeasureString(loading.Text).X / 2,
                MainGame.GAME_HEIGHT / 2 - loading.SpriteFont.LineSpacing / 2);
            controls.Add(loading);

            ThreadPool.QueueUserWorkItem(new WaitCallback(nextState.LargeLoadContent));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (nextState.FinishedLoading)
            {
                SwitchStateWithFade(nextState);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            gameRef.spriteBatch.Begin();
            FadeOutRect.Draw(gameRef.spriteBatch, Vector2.Zero, FadeOutColor);
            gameRef.spriteBatch.End();
        }
    }
}
