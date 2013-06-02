using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.States;
using Origins_Remake.Util;
using OriginsLibrary.Util;

namespace Origins_Remake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public const string VERSION = "v.0.0.5";
        public const int GAME_WIDTH = 800;
        public const int GAME_HEIGHT = 480;

        public static ContentManager gameContent;

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        GameStateManager manager;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = Content;

            Components.Add(new InputHandler(this));

            SheetManager.Initialize(this);

            manager = new GameStateManager(this);
            Components.Add(manager);
            manager.ChangeState(new MainMenuState(this, manager));

            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            NoiseGenerator.Initialize(this);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
