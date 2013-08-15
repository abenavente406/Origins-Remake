using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLib.TileEngine;
using OriginsLib.Util;

namespace LayerMapEditor
{
    public enum State
    {
        Playing,
        Freeze
    }

    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public const int WINDOW_WIDTH = 1366;
        public const int WINDOW_HEIGHT = 768;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private State state = State.Playing;
        public HUD hud;
        public Canvas canvas;

        public TileMap level;

        Engine engine = new Engine(32, 32);

        public State State
        {
            get { return state; }
            set { state = value; }
        }

        public int SelectedTileIndex
        {
            get { return hud.SelectedTileIndex; }
            set { hud.SelectedTileIndex = value; }
        }

        public int SelectedTileSheetIndex
        {
            get { return hud.SelectedTileSetIndex; }
        }

        public bool ShowCollisionMap
        {
            get { return hud.ShowCollisionMap; }
        }

        public Tool SelectedTool
        {
            get { return hud.SelectedTool; }
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));

            Config.Initialize(this);

            hud = new HUD(this);
            hud.Initialize();

            SheetManager.Initialize(this);
            canvas = new Canvas(this);

            level = new TileMap(30, 30);
            level.AddTileSet(new TileSet("TileSheets\\dirt.png", 32, 32));

            for (int i = 0; i < 3; i++)
                level.AddLayer(new MapLayer(level));

            level.CurrentLayerIndex = 0;

            canvas.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hud.LoadContent();
            canvas.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (state == LayerMapEditor.State.Playing)
            {
                if (canvas.Bounds.Contains(InputHandler.MousePos))
                {
                    // Handle stuff in the canvas
                    canvas.Update(gameTime);
                }

                hud.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            canvas.Draw(spriteBatch, gameTime);
            hud.Draw(spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
