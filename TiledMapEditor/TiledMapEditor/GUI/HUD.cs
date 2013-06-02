using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TileEngineLibrary;
using TileEngineLibrary.Tiles;

namespace TiledMapEditor.GUI
{
    public class HUD
    {
        // Textures to hold buttons
        Texture2D panel, newMap, saveMap, loadMap, loadTiles, backLayer, frontLayer, collisionLayer, previewPanelTexture;

        Vector2 position;

        Button btnNewMap;
        Button btnBackLayer;
        Button btnCollisionLayer;
        Button btnFrontLayer;
        Button btnLoadMap;
        Button btnLoadTile;
        Button btnSaveMap;

        // Button list
        List<Button> buttons = new List<Button>();

        public HUD(ContentManager content)
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

            // Set HUD position
            position = new Vector2(0, (int)Game1.HEIGHT - panel.Height);

            btnNewMap = new Button(newMap, new Vector2(position.X + 25, position.Y + 25));
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
                        // TODO: load tilesheet
                    }

                    Game1.state = Game1.State.PLAY;
                };
            btnLoadMap = new Button(loadMap, new Vector2(position.X + 275, position.Y + 25));
            btnLoadMap.OnClick += (o, e) =>
                {
                    Game1.state = Game1.State.FREEZE;

                    var openFile = new System.Windows.Forms.OpenFileDialog();

                    openFile.Title = "Open Map";
                    openFile.FileName = "";
                    openFile.Filter = "Saved Level Format|*.slf";

                    if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        Game1.map.LoadLevelFromFile(openFile.FileName);

                    Game1.state = Game1.State.PLAY;
                };
            btnLoadTile = new Button(loadTiles, new Vector2(position.X + 400, position.Y + 25));
            btnLoadTile.OnClick += (o, e) =>
                {
                    Game1.state = Game1.State.FREEZE;

                    GameForms.NewTileSheetForm frmTileSheet = new GameForms.NewTileSheetForm();
                    frmTileSheet.ShowDialog();

                    // TODO: Load tilesheet into game

                    Game1.state = Game1.State.PLAY;
                };
            btnSaveMap = new Button(saveMap, new Vector2(position.X + 150, position.Y + 25));
            btnSaveMap.OnClick += (o, e) =>
                {
                    Game1.state = Game1.State.FREEZE;

                    var saveFileDialog = new System.Windows.Forms.SaveFileDialog();

                    saveFileDialog.Title = "Save Map";
                    saveFileDialog.Filter = "Saved Level Foramt| *.slf";
                    saveFileDialog.FileName = Game1.mapName;

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Game1.map.SaveLevel(saveFileDialog.FileName);
                    }

                    saveFileDialog.Dispose();

                    Game1.state = Game1.State.PLAY;
                };

            buttons.Add(btnNewMap);
            buttons.Add(btnLoadMap);
            buttons.Add(btnLoadTile);
            buttons.Add(btnSaveMap);
        }

        public void Update()
        {
            foreach (Button b in buttons)
            {
                b.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(panel, position, Game1.controlColor);

            foreach (Button b in buttons)
                b.Draw(spriteBatch);

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
