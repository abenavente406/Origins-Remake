using GameHelperLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TileEngineLibrary;
using TileEngineLibrary.Tiles;
using OriginsLibrary.Util;
using System;

namespace TiledMapEditor
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum State
        {
            PLAY,
            FREEZE
        }

        public static string[] tileNames = new string[]
        {
            "Black",
            "Grass",
            "Sand",
            "Water",
            "Dungeon Floor",
            "Dungeon Wall"
        };

        public static ContentManager content;

        public static int WIDTH { get { return 800; } }
        public static int HEIGHT { get { return 700; } }

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Color controlColor = new Color(204, 204, 153);

        static GUI.HUD hud;

        public static Texture2D tileSheet;
        public static Texture2D solidTexture;
        public static Texture2D pixelTexture;
        public static SpriteFont basicFont;

        public static State state = State.PLAY;

        public static Vector2 drawOffset = Vector2.Zero;

        public static bool quit = false;

        public static string mapName;
        public static string fileName;
        public static string loadFileName;
        public static string tileSheetFileName;

        public static int drawableLayer = 0;

        public static Level map;

        public static int mapHeight = 20;
        public static int mapWidth = 20;
        public static int tileHeight = 32;
        public static int tileWidth = 32;

        public static int selectedTileNo = 0;

        public static Rectangle MapArea = new Rectangle(0, 0, WIDTH, HEIGHT - 100);

        public static int selectedTile = 1;

        MouseState curState;
        KeyboardState prevState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {

            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;

            graphics.ApplyChanges();

            Components.Add(new InputHandler(this));

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = Content;

            solidTexture = Content.Load<Texture2D>(@"images\solid");
            pixelTexture = Content.Load<Texture2D>(@"images\pixel");

            basicFont = Content.Load<SpriteFont>("Basic");
            
            hud = new GUI.HUD(Content, this);

            SheetManager.Initialize(this);
            NoiseGenerator.Initialize(this);
            Camera.Initialize(this);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        float prevWheelValue;
        float currWheelValue;

        protected override void Update(GameTime gameTime)
        {
            Window.Title = "Map Editor - " + mapName;
            currWheelValue = InputHandler.MouseState.ScrollWheelValue;

            if (state == State.PLAY)
            {
                if (MapArea.Contains(InputHandler.MousePos))
                {
                    if (map != null)
                    {
                        if (InputHandler.KeyDown(Keys.Up)) Camera.Move(new Vector2(0, -2));
                        if (InputHandler.KeyDown(Keys.Down)) Camera.Move(new Vector2(0, 2));
                        if (InputHandler.KeyDown(Keys.Left)) Camera.Move(new Vector2(-2, 0));
                        if (InputHandler.KeyDown(Keys.Right)) Camera.Move(new Vector2(2, 0));

                        if (InputHandler.KeyPressed(Keys.Add)) selectedTile++;
                        if (InputHandler.KeyPressed(Keys.Subtract)) selectedTile--;

                        if (currWheelValue > prevWheelValue) selectedTile++;
                        if (currWheelValue < prevWheelValue) selectedTile--;

                        selectedTile = selectedTile < 0 ? tileNames.Length - 1 : selectedTile;
                        selectedTile %= tileNames.Length;

                        map.Update(gameTime);

                        var tilePos = Vector2Tile(new Vector2(InputHandler.MousePos.X, InputHandler.MousePos.Y) + Camera.Position);

                        if (InputHandler.MouseButtonDown(MouseButton.LeftButton))
                        {
                            switch (selectedTile)
                            {
                                case 0:
                                    map.SetTile(tilePos, new BlackTile(tilePos));
                                    break;
                                case 1:
                                    map.SetTile(tilePos, new GrassTile(tilePos));
                                    break;
                                case 2:
                                    map.SetTile(tilePos, new SandTile(tilePos));
                                    break;
                                case 3:
                                    map.SetTile(tilePos, new WaterTile(tilePos));
                                    break;
                                case 4:
                                    map.SetTile(tilePos, new DungeonFloorTile(tilePos));
                                    break;
                                case 5:
                                    map.SetTile(tilePos, new DungeonWallTile(tilePos));
                                    break;
                            }
                        }
                        else if (InputHandler.MouseButtonDown(MouseButton.RightButton))
                        {
                            map.SetTile(tilePos, new BlackTile(tilePos));
                        }
                    }
                }
                    hud.Update(gameTime);
            }

            prevWheelValue = currWheelValue;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(controlColor);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.GetMatrix());
            {
                if (map != null)
                    map.Draw(spriteBatch, gameTime, new Rectangle((int)Camera.Position.X, (int)Camera.Position.Y, WIDTH, HEIGHT));
            }
            spriteBatch.End();

            spriteBatch.Begin();
            {
                spriteBatch.DrawString(basicFont, InputHandler.MousePos.ToString(), new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(basicFont, tileNames[selectedTile], new Vector2(10, 10) + new Vector2(0, basicFont.LineSpacing), Color.White);
                hud.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();

            base.Draw(gameTime);

        }

        protected Point Vector2Tile(Vector2 pos)
        {
            if (pos.X == 0) return Point.Zero;
            if (pos.Y == 0) return Point.Zero;

            return new Point((int)pos.X / map.TileWidth, (int)pos.Y / map.TileHeight);
        }

        protected Vector2 Tile2Vector(Point pos)
        {
            return new Vector2(pos.X * map.TileWidth, pos.Y * map.TileHeight);
        }
    }

    public class GameMenuStrip : GameComponent
    {
        public GameMenuStrip(Game game) : base(game) { }

    }
}
