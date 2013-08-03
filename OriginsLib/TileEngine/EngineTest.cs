using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using OriginsLib.Util;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace OriginsLib.TileEngine
{
    public class EngineTest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Engine engine = new Engine(32, 32);

        TileSet tileSet;
        TileMap map;

        MouseState lastState;
        MouseState currState;

        int selectedTileIndex = 0;

        public EngineTest()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var fs = File.Open(@"C:\Users\ANTHONY\Downloads\tilesets2\tileset1.png", FileMode.Open);
            Texture2D tilesetTexture = Texture2D.FromStream(GraphicsDevice, fs);
            tileSet = new TileSet(tilesetTexture, Engine.TileWidth, Engine.TileHeight);
            fs.Close();
            fs.Dispose();

            map = new TileMap(40, 40);

            MapLayer mapLayer = new MapLayer(map);
            MapLayer bushes = new MapLayer(map);

            for (int x = 0; x < 40; x++)
                for (int y = 0; y < 40; y++)
                    mapLayer.SetTile(x, y, new Tile(0, 0));

            mapLayer.Visible = false;

            map.AddTileSet(tileSet);
            map.AddLayer(mapLayer);
            map.AddLayer(bushes);

            map.CurrentLayerIndex = 1;

            Camera.Initialize(
                Vector2.Zero,
                new Rectangle()
                {
                    Width = graphics.PreferredBackBufferWidth,
                    Height = graphics.PreferredBackBufferHeight
                }, 
                new Vector2(map.WidthInTiles * Engine.TileWidth,
                    map.HeightInTiles * Engine.TileHeight)
            );

            Camera.MaxClamp = new Vector2(
                                  map.WidthInTiles * Engine.TileWidth - Camera.View.Width,
                                  map.HeightInTiles * Engine.TileHeight - Camera.View.Height
            );
        }

        protected override void Update(GameTime gameTime)
        {
            currState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Camera.Move(new Vector2(-5, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Camera.Move(new Vector2(5, 0));
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Camera.Move(new Vector2(0, -5));
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Camera.Move(new Vector2(0, 5));

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
                Camera.Zoom += .05f;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
                Camera.Zoom += -.05f;

            if (currState.ScrollWheelValue > lastState.ScrollWheelValue)
                selectedTileIndex++;
            else if (currState.ScrollWheelValue < lastState.ScrollWheelValue)
                selectedTileIndex--;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                map.SetTile (
                    Engine.VectorToCell (
                        new Point(Mouse.GetState().X + Camera.View.X, Mouse.GetState().Y + Camera.View.Y)
                    ), 
                    new Tile(selectedTileIndex, 0)
                );
            }
            else if (currState.RightButton == ButtonState.Pressed)
            {
                map.SetTile(
                    Engine.VectorToCell(
                        new Point(currState.X + Camera.View.X, currState.Y + Camera.View.Y)
                    ),
                    new Tile(-1, -1)
                );
            }

            lastState = currState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(
                SpriteSortMode.Immediate, 
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Camera.GetTransform());

            map.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    static class TileEngineTestEntry
    {
        public static void Main(string[] args)
        {
            using (var game = new EngineTest())
            {
                game.Run();
            }
        }
    }
}
