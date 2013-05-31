using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;

namespace Origins_Remake.States
{
    public class GameplayState : BaseGameState
    {
        enum State
        {
            Playing,
            Paused
        }

        public GameplayState(Game game, GameStateManager manager)
            : base(game, manager) 
        {
        
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
