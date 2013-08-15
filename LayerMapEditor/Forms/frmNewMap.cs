using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OriginsLib.TileEngine;
using OriginsLib.Util;
using Microsoft.Xna.Framework;

namespace LayerMapEditor.Forms
{
    public partial class frmNewMap : Form
    {
        MainGame gameRef;

        int selectedTileSet_Layer1 = 0;
        int selectedTileSet_Layer2 = 0;
        int selectedTileSet_Layer3 = 0;

        private int groundTile_Layer1 = 0;
        private int groundTile_Layer2 = 0;
        private int groundTile_Layer3 = 0;

        private int wallTile_Layer1 = 0;
        private int wallTile_Layer2 = 0;
        private int wallTile_Layer3 = 0;


        public int TileSet_Layer1
        {
            get { return selectedTileSet_Layer1; }
        }

        public int TileSet_Layer2
        {
            get { return selectedTileSet_Layer2; }
        }

        public int TileSet_Layer3
        {
            get { return selectedTileSet_Layer3; }
        }
        public int GroundTile_Layer1
        {
            get { return groundTile_Layer1; }
        }

        public int GroundTile_Layer2
        {
            get { return groundTile_Layer2; }
        }

        public int GroundTile_Layer3
        {
            get { return groundTile_Layer3; }
        }

        public int WallTile_Layer1
        {
            get { return wallTile_Layer1; }
        }

        public int WallTile_Layer2
        {
            get { return wallTile_Layer2; }
        }

        public int WallTile_Layer3
        {
            get { return wallTile_Layer3; }
        }

        public frmNewMap(MainGame game)
        {
            this.gameRef = game;
            InitializeComponent();
        }

        private void frmNewMap_Load(object sender, EventArgs e)
        {
            if (gameRef.level.TileSets.Count > 0)
            {
                cboGeneration.Items.Add("Dungeon Generation (Drunken Walk)");
                cboGeneration.Items.Add("Perlin Level (Perlin Noise)");
            }
        }

        private void cboGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGeneration.SelectedIndex != 0)
            {
                btnSelTileSets.Enabled = true;
            }
        }

        private void btnSelTileSets_Click(object sender, EventArgs e)
        {
            var selTileSets = new frmGetTileSetsPerLayer(gameRef);

            if (selTileSets.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedTileSet_Layer1 = selTileSets.TileSet_Layer1;
                selectedTileSet_Layer2 = selTileSets.TileSet_Layer2;
                selectedTileSet_Layer3 = selTileSets.TileSet_Layer3;

                groundTile_Layer1 = selTileSets.GroundTile_Layer1;
                groundTile_Layer2 = selTileSets.GroundTile_Layer2;
                groundTile_Layer3 = selTileSets.GroundTile_Layer3;

                wallTile_Layer1 = selTileSets.WallTile_Layer1;
                wallTile_Layer2 = selTileSets.WallTile_Layer2;
                wallTile_Layer3 = selTileSets.WallTile_Layer3;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            var tileSets = gameRef.level.TileSets;

            gameRef.level = new TileMap((int)numWidth.Value, (int)numHeight.Value);

            if (cboGeneration.SelectedIndex != 0)
            {
                foreach (TileSet s in tileSets)
                    gameRef.level.AddTileSet(s);
            }
            else
            {
                gameRef.level.AddTileSet(new TileSet(@"TileSheets\dirt.png", Engine.TileWidth, Engine.TileHeight));
            }

            var layer1 = new MapLayer(gameRef.level);
            var layer2 = new MapLayer(gameRef.level);
            var layer3 = new MapLayer(gameRef.level);

            switch (cboGeneration.SelectedIndex)
            {
                case 1:
                    // TODO: Generate level with dungeon algorithm
                    for (int x = 0; x < layer1.WidthInTiles; x++)
                        for (int y = 0; y < layer1.HeightInTiles; y++)
                        {
                            layer1.SetTile(x, y, new Tile(groundTile_Layer1, selectedTileSet_Layer1));
                        }


                    layer2 = MapLayer.GenerateFromAlgorithm(MapLayer.GenerationAlgorithm.Dungeon_DrunkenWalk, 
                        gameRef.level, selectedTileSet_Layer2, wallTile:wallTile_Layer2);

                    break;
                case 2:
                    // TODO: Generate level with perlin algorithm
                    break;
            }

            gameRef.level.AddLayer(layer1);
            gameRef.level.AddLayer(layer2);
            gameRef.level.AddLayer(layer3);

            Camera.MaxClamp = new Vector2(gameRef.level.WidthInPixels - Canvas.CANVAS_WIDTH,
                gameRef.level.HeightInPixels - Canvas.CANVAS_HEIGHT);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
