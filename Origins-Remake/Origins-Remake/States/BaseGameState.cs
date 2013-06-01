using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using GameHelperLibrary.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Origins_Remake.States
{
    public partial class BaseGameState : GameState
    {
        protected MainGame gameRef;
        protected ControlManager controls;
        protected PlayerIndex playerIndexInControl;

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            gameRef = (MainGame)game;
            playerIndexInControl = PlayerIndex.One;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            controls = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>("Fonts\\default"));
        }

        public override void LargeLoadContent(object sender)
        {
            FinishedLoading = true;
        }

        public override void Update(GameTime gameTime)
        {
            controls.Update(gameTime, playerIndexInControl);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var batch = gameRef.spriteBatch;

            batch.Begin();
            controls.Draw(gameRef.spriteBatch, gameTime);
            batch.End();

            base.Draw(gameTime);
        }

        protected void SwitchStateWithFade(GameState targetState)
        {
            this.IsExiting = true;
            StateManager.TargetState = targetState;
        }
    }
}
