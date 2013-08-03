using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary.Shapes;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Input;
using GameHelperLibrary.Controls;
using System;
using LayerMapEditor.Forms;
using OriginsLib.TileEngine;

namespace LayerMapEditor
{
    public enum Tool
    {
        Pencil,
        Fill
    }

    public class HUD
    {
        public const int HUD_WIDTH = 266;
        public const int HUD_HEIGHT = 768;

        private Tool selectedTool = Tool.Pencil;
        private MainGame gameRef;
        private Vector2 startPos;
        private Rectangle bounds;

        private Texture2D background;
        private Color controlColor;

        private TileSet selectedTileSheet;
        private int selectedTileIndex = 0;
        private int selectedTileSetIndex = 0;
        private bool showCollisionMap = true;

        public ControlManager controls;

        private Button layer1;
        private Button layer2;
        private Button layer3;
        private Button collisionLayer;

        private TileSelector selector;

        public int SelectedTileIndex
        {
            get { return selectedTileIndex; }
            set { selectedTileIndex = value; }
        }

        public int SelectedTileSetIndex
        {
            get { return selectedTileSetIndex; }
        }

        public bool ShowCollisionMap
        {
            get { return showCollisionMap; }
        }

        public Tool SelectedTool
        {
            get { return selectedTool; }
        }

        public HUD(MainGame gameRef)
        {
            this.gameRef = gameRef;
            startPos = Vector2.Zero;
        }

        public void Initialize()
        {
            bounds = new Rectangle((int)startPos.X, (int)startPos.Y, HUD_WIDTH, HUD_HEIGHT);
        }

        public void LoadContent()
        {
            background = gameRef.Content.Load<Texture2D>(@"Images/HUD/gradient");
            
            var color = 210;
            controlColor = new Color(color, color, color);

            controls = new ControlManager(gameRef, gameRef.Content.Load<SpriteFont>(@"fonts\kootenay9"));

            layer1 = new Button()
            {
                Name = "btnLayer1",
                Text = "Layer 1",
                Value = 0
            };

            layer2 = new Button()
            {
                Name = "btnLayer2",
                Text = "Layer 2",
                Value = 1
            };

            layer3 = new Button()
            {
                Name = "btnLayer3",
                Text = "Layer 3",
                Value = 2
            };

            collisionLayer = new Button()
            {
                Name = "btnCollisionLayer",
                Text = "Collision Layer",
                Value = -1
            };

            controls.Add(layer1);
            controls.Add(layer2);
            controls.Add(layer3);
            controls.Add(collisionLayer);

            Vector2 startPos = new Vector2(0, 35);

            foreach (Button b in controls)
            {
                b.OnClick += LayerButtonClick;
                b.Position = startPos;
                startPos += new Vector2(0, b.Height + 10);
            }

            Button toggleShowCollision = new Button()
            {
                Name = "btnToggleShowCollision",
                Text = "Show Collision"
            };
            toggleShowCollision.OnClick += (o, e) => { showCollisionMap = !showCollisionMap; };
            toggleShowCollision.Position = controls[3].Position + new Vector2(0, collisionLayer.Height + 10);
            controls.Add(toggleShowCollision);

            Button selectTileSheet = new Button()
            {
                Name = "btnSelectTileSheet",
                Text = "Select Tile Sheet"
            };
            selectTileSheet.OnClick += (o, e) => 
            {
                gameRef.State = State.Freeze;

                frmGetTileSheet frm = new frmGetTileSheet(gameRef);
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    selectedTileSetIndex = frm.SelectedTileSheetId;
                }

                gameRef.State = State.Playing;
            };
            selectTileSheet.Position = controls[4].Position + new Vector2(0, collisionLayer.Height + 10);
            controls.Add(selectTileSheet);

            Button saveMap = new Button()
            {
                Name = "btnSaveMap",
                Text = "Save Map"
            };
            saveMap.OnClick += (o, e) =>
            {
                gameRef.State = State.Freeze;

                System.Windows.Forms.SaveFileDialog saveFileDlg = new System.Windows.Forms.SaveFileDialog();
                saveFileDlg.FileName = "level1.slf";
                saveFileDlg.Filter = "SLF level (.slf)|*.slf";

                if (saveFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // TODO: Save map with method Level.Save(@"File/path/here");
                    gameRef.level.Save(saveFileDlg.FileName);
                }

                gameRef.State = State.Playing;
            };
            saveMap.Position = controls[5].Position + new Vector2(0, collisionLayer.Height + 10);
            controls.Add(saveMap);

            Button loadMap = new Button()
            {
                Name = "btnLoadMap",
                Text = "Load Map",
                Position = saveMap.Position + new Vector2(0, saveMap.Height + 10)
            };
            loadMap.OnClick += (o, e) =>
            {
                gameRef.State = State.Freeze;

                var openFileDlg = new System.Windows.Forms.OpenFileDialog();
                openFileDlg.FileName = "";
                openFileDlg.Filter = "SLF Level (.slf)|*.slf";
                if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // TODO: Load map with method level.Load(@"File/path/here");
                    gameRef.level.Load(openFileDlg.FileName);
                }

                gameRef.State = State.Playing;
            };
            controls.Add(loadMap);

            // Button to turn on the pencil tool
            Button pencil = new Button()
            {
                Name = "btnPencil",
                Text = "Pencil",
                Position = loadMap.Position + new Vector2(0, loadMap.Height + 10)
            };
            pencil.OnClick += (o, e) => { selectedTool = Tool.Pencil; };
            controls.Add(pencil);

            // Button to turn on the fill tool
            Button fill = new Button()
            {
                Name = "btnFill",
                Text = "Fill",
                Position = pencil.Position + new Vector2(0, loadMap.Height + 10)
            };
            fill.OnClick += (o, e) => {selectedTool = Tool.Fill; };
            controls.Add(fill);

            Label lblTileIndex = new Label()
            {
                Color = Color.Black,
                Name = "lblTileIndex",
                Text = "{X: " + gameRef.level.CurrentTileSet.SourceRectangles[SelectedTileIndex].X / Engine.TileWidth + ", " +
                        "Y: " + gameRef.level.CurrentTileSet.SourceRectangles[SelectedTileIndex].Y / Engine.TileHeight + "}"
            };
            lblTileIndex.Position = fill.Position + new Vector2(0, fill.Height + 10);
            controls.Add(lblTileIndex);

            //selector = new TileSelector(gameRef, lblTileIndex.Position + new Vector2(0, lblTileIndex.Height + 10));
            //selector.Width = HUD_WIDTH;
        }

        public void Update(GameTime gameTime)
        {
            controls.Update(gameTime, PlayerIndex.One);
            //selector.Update(gameTime);

            if (InputHandler.KeyPressed(Keys.OemPlus) ||
                InputHandler.KeyPressed(Keys.Add))
                selectedTileIndex++;

            if (InputHandler.KeyPressed(Keys.OemMinus) ||
                InputHandler.KeyPressed(Keys.Subtract))
                selectedTileIndex--;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (InputHandler.MouseState.ScrollWheelValue > InputHandler.LastMouseState.ScrollWheelValue)
                    selectedTileIndex++;
                else if (InputHandler.MouseState.ScrollWheelValue < InputHandler.LastMouseState.ScrollWheelValue)
                    selectedTileIndex--;
            }

            if (selectedTileIndex < 0)
                selectedTileIndex = gameRef.level.GetTileSet(SelectedTileSetIndex).SourceRectangles.Length;

            selectedTileIndex %= gameRef.level.GetTileSet(SelectedTileSetIndex).SourceRectangles.Length;

            ((Label)controls[controls.Count - 1]).Text = "{X: " + gameRef.level.CurrentTileSet.SourceRectangles[SelectedTileIndex].X / Engine.TileWidth + ", " +
                        "Y: " + gameRef.level.CurrentTileSet.SourceRectangles[SelectedTileIndex].Y / Engine.TileHeight + "}";
            
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Begin();
            batch.Draw(background, bounds, Color.White);
            batch.Draw(gameRef.level.GetTileSet(selectedTileSetIndex).Image, 
                new Rectangle((int)startPos.X, (int)startPos.Y, Engine.TileWidth, Engine.TileHeight),
                gameRef.level.GetTileSet(selectedTileSetIndex).SourceRectangles[selectedTileIndex],
                Color.White);
            controls.Draw(batch, gameTime);
            //selector.Draw(batch, gameTime);
            batch.End();
        }

        private Texture2D CreateBG(int width, int height)
        {
            var backgroundTex = new Texture2D(gameRef.GraphicsDevice, width, height);

            Color[] bgc = new Color[height * width];

            int texColour = 5;           // Defines the colour of the gradient.
            int gradientThickness = 15;  // Defines how "diluted" the gradient gets. I've found 2 works great, and 16 is a very fine gradient.

            for (int i = 0; i < bgc.Length; i++)
            {
                texColour = (i / (width * gradientThickness));
                bgc[i] = new Color(texColour, texColour, texColour, 1);
            }
            backgroundTex.SetData(bgc);
            return backgroundTex;
        }

        private void LayerButtonClick(object sender, EventArgs e)
        {
            int value = (int)((Control)sender).Value;
            gameRef.level.CurrentLayerIndex = value;
        }
    }
}
