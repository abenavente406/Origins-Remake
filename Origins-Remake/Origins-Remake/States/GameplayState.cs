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

namespace Origins_Remake.States
{
    public class GameplayState : BaseGameState
    {
        enum State
        {
            Playing,
            Paused
        }
        
        // Test Random Level
        // -----------------
        //RandomLevel testRandomLevel;

        // Test Dungeon Level
        // ------------------
        DungeonLevel testDungeonLevel;

        // Test Perlin Level
        // -----------------
        //PerlinLevel testPerlinLevel;

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
            // Test Random Level Generation 
            // ----------------------------
            //testRandomLevel = new RandomLevel(100, 50, 32, 32);
            //Camera.Initialize(gameRef, Vector2.Zero, Vector2.Zero,
            //    new Vector2(testRandomLevel.RealWidth - MainGame.GAME_WIDTH, testRandomLevel.RealHeight - MainGame.GAME_HEIGHT));

            // Test Dungeon Level Generation
            // -----------------------------
            testDungeonLevel = new DungeonLevel(100, 50, 32, 32);
            Camera.Initialize(gameRef, Vector2.Zero, testDungeonLevel);

            // Test Perlin Level Generation
            // ----------------------------
            //testPerlinLevel = new PerlinLevel(150, 32, 32);
            //Camera.Initialize(gameRef, Vector2.Zero, testPerlinLevel);

            base.LargeLoadContent(sender);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.KeyDown(Keys.Right)) Camera.Move(new Vector2(2, 0));
            if (InputHandler.KeyDown(Keys.Left)) Camera.Move(new Vector2(-2, 0));
            if (InputHandler.KeyDown(Keys.Up)) Camera.Move(new Vector2(0, -2));
            if (InputHandler.KeyDown(Keys.Down)) Camera.Move(new Vector2(0, 2));

#if DEBUG
            if (InputHandler.KeyDown(Keys.OemPlus)) Camera.Zoom -= .01f;
            if (InputHandler.KeyDown(Keys.OemMinus)) Camera.Zoom += .01f;
#endif

            // Test random level update
            // ------------------------
            // testRandomLevel.Update(gameTime);

            // Test dungeon level update
            // -------------------------
            testDungeonLevel.Update(gameTime);

            // Test perlin level update
            // ------------------------
            //testPerlinLevel.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = gameRef.spriteBatch;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetTransformation());
            
            // Test random level drawing
            // -------------------------
            // testRandomLevel.Draw(spriteBatch, gameTime);

            // Test dungeon level drawing
            // --------------------------
            testDungeonLevel.Draw(spriteBatch, gameTime);

            // Test perlin level drawing
            //testPerlinLevel.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            spriteBatch.Begin();
            FadeOutRect.Draw(spriteBatch, Vector2.Zero, FadeOutColor);
            spriteBatch.End();
        }
    }
}
