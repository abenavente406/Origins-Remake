using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TileEngineLibrary;
using TileEngineLibrary.Tiles;
using GameHelperLibrary.Controls;

namespace TiledMapEditor.GUI
{
    public class HUD
    {
        // Textures to hold buttons
        Texture2D panel, newMap, saveMap, loadMap, loadTiles, backLayer, frontLayer, collisionLayer, previewPanelTexture;

        Vector2 position;

        ControlManager Controls { get; set; }

        Button btnNewMap;
        Button btnLoadMap;
        Button btnSaveMap;

        public HUD(ContentManager content, Game game)
        {
            panel = LoadTexture(content, @"images\panel");
            newMap = LoadTexture(content, "images\\newMapButton");
            saveMap = LoadTexture(content, "images\\saveMapButton");
            loadTiles = LoadTexture(content, "images\\loadTileButton");
            loadMap = LoadTexture(content, "images\\loadMapButton");
            backLayer = LoadTexture(content, "images\\backLayer");
            frontLayer = LoadTexture(content, "images\\frontLayer");
            collisionLayer = LoadTexture(content, "images\\collisionLayerButton");
            previewPanelTexture = LoadTexture(content, "images\\previewpanel");
            var defaultTex = LoadTexture(content, "images\\defaultButton");

            SpriteFont basicFont = content.Load<SpriteFont>("Basic");

            Controls = new ControlManager(game, basicFont);

            // Set HUD position
            position = new Vector2(0, (int)Game1.HEIGHT - panel.Height);

            btnNewMap = new Button(basicFont);
            btnNewMap.Position = new Vector2(position.X + 25, position.Y + 25);
            btnNewMap.BackgroundImage = newMap;
            btnNewMap.Width = newMap.Width;
            btnNewMap.Height = newMap.Height;
            btnNewMap.OnClick += (o, e) =>
                {
                    Game1.state = Game1.State.FREEZE;

                    GameForms.NewMapForm newMapForm = new GameForms.NewMapForm();
                    newMapForm.ShowDialog();

                    if (newMapForm.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        Game1.mapHeight = newMapForm.mapHeight;
                        Game1.mapWidth = newMapForm.mapWidth;
                        Game1.tileHeight = newMapForm.tileHeight;
                        Game1.tileWidth = newMapForm.tileWidth;
                        Game1.mapName = newMapForm.mapName;

                        Game1.selectedTileNo = 0;
                        Game1.drawOffset = Vector2.Zero;

                        switch (newMapForm.levelType)
                        {
                            case GameForms.LevelType.Blank:
                                Game1.map = new SlfLevel(null)
                                {
                                    realWidth = Game1.mapWidth * 32,
                                    realHeight = Game1.mapHeight * 32,
                                    tileWidth = Game1.tileWidth,
                                    tileHeight = Game1.tileHeight,
                                    map = new Tile[Game1.mapWidth, Game1.mapHeight]
                                };

                                for (int x = 0; x < Game1.map.WidthInTiles; x++)
                                    for (int y = 0; y < Game1.map.HeightInTiles; y++)
                                        Game1.map.SetTile(new Point(x, y), new BlackTile(new Point(x, y)));
                                break;
                            case GameForms.LevelType.Dungeon:

                                Game1.map = new DungeonLevel(newMapForm.mapWidth, newMapForm.mapHeight,
                                    newMapForm.tileWidth, newMapForm.tileHeight);
                                break;
                            case GameForms.LevelType.Random:
                                Game1.map = new RandomLevel(newMapForm.mapWidth, newMapForm.mapHeight,
                                    newMapForm.tileWidth, newMapForm.tileHeight);
                                break;
                            case GameForms.LevelType.Perlin:
                                Game1.map = new PerlinLevel(newMapForm.mapWidth, newMapForm.tileWidth,
                                    newMapForm.tileHeight);
                                break;
                        }
                        // TODO: load tilesheet
                    }

                    Game1.state = Game1.State.PLAY;
                };
            btnLoadMap = new Button(basicFont);
            btnLoadMap.Position = new Vector2(position.X + 275, position.Y + 25);
            btnLoadMap.BackgroundImage = loadMap;
            btnLoadMap.Width = loadMap.Width;
            btnLoadMap.Height = loadMap.Height;
            btnLoadMap.OnClick += (o, e) =>
                {

                    var openFile = new System.Windows.Forms.OpenFileDialog();

                    openFile.Title = "Open Map";
                    openFile.FileName = "";
                    openFile.Filter = "Saved Level Format|*.slf";

                    Game1.map = new RandomLevel(1, 1, 1, 1);

                    if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        Game1.map.LoadLevelFromFile(openFile.FileName);

                };
            btnSaveMap = new Button(basicFont);
            btnSaveMap.Position = new Vector2(position.X + 150, position.Y + 25);
            btnSaveMap.BackgroundImage = saveMap;
            btnSaveMap.Width = saveMap.Width;
            btnSaveMap.Height = saveMap.Height;
            btnSaveMap.OnClick += (o, e) =>
                {

                    var saveFileDialog = new System.Windows.Forms.SaveFileDialog();

                    saveFileDialog.Title = "Save Map";
                    saveFileDialog.Filter = "Saved Level Foramt| *.slf";
                    saveFileDialog.FileName = Game1.mapName;

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Game1.map.SaveLevel(saveFileDialog.FileName);
                    }

                    saveFileDialog.Dispose();

                };

            Controls.Add(btnNewMap);
            Controls.Add(btnLoadMap);
            Controls.Add(btnSaveMap);
        }

        public void Update(GameTime gameTime)
        {
            Controls.Update(gameTime, PlayerIndex.One);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(panel, position, Game1.controlColor);
            Controls.Draw(spriteBatch, gameTime);
        }

        Texture2D LoadTexture(ContentManager content, string assetName)
        {
            return content.Load<Texture2D>(assetName);
        }

        private Texture2D TextureFromFile(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            var tex = Texture2D.FromStream(Game1.spriteBatch.GraphicsDevice, fs);

            fs.Close();

            return tex;
        }   
    }
}
