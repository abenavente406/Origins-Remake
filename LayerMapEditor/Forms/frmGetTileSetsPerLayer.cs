using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OriginsLib.TileEngine;

namespace LayerMapEditor.Forms
{
    public partial class frmGetTileSetsPerLayer : Form
    {
        MainGame gameRef;

        private int tileSet_Layer1 = 0;
        private int tileSet_Layer2 = 0;
        private int tileSet_Layer3 = 0;

        private int groundTile_Layer1 = 0;
        private int groundTile_Layer2 = 0;
        private int groundTile_Layer3 = 0;

        private int wallTile_Layer1 = 0;
        private int wallTile_Layer2 = 0;
        private int wallTile_Layer3 = 0;

        public int TileSet_Layer1
        {
            get { return tileSet_Layer1; }
        }

        public int TileSet_Layer2
        {
            get { return tileSet_Layer2; }
        }

        public int TileSet_Layer3
        {
            get { return tileSet_Layer3; }
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

        public frmGetTileSetsPerLayer(MainGame gameRef)
        {
            this.gameRef = gameRef;
            InitializeComponent();
        }

        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            var name = ((RadioButton)sender).Name;

            switch (name)
            {
                case "radLayer1":
                    lstTileSets.SelectedIndex = tileSet_Layer1;
                    numGroundTile.Value = groundTile_Layer1;
                    numWallTile.Value = wallTile_Layer1;
                    break;
                case "radLayer2":
                    lstTileSets.SelectedIndex = tileSet_Layer2;
                    numGroundTile.Value = groundTile_Layer2;
                    numWallTile.Value = wallTile_Layer2;
                    break;
                case "radLayer3":
                    lstTileSets.SelectedIndex = tileSet_Layer3;
                    numGroundTile.Value = groundTile_Layer3;
                    numWallTile.Value = wallTile_Layer3;
                    break;
                default:
                    break;
            }
        }

        private void frmGetTileSetsPerLayer_Load(object sender, EventArgs e)
        {
            var tileSets = gameRef.level.TileSets;

            foreach (TileSet set in tileSets)
            {
                lstTileSets.Items.Add(set.Path);
            }

            lstTileSets.SelectedIndex = 0;

            tileSet_Layer1 = 0;
            tileSet_Layer2 = 0;
            tileSet_Layer3 = 0;
        }

        private void lstTileSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radLayer1.Checked)
            {
                tileSet_Layer1 = ((ListBox)sender).SelectedIndex;
            }
            else if (radLayer2.Checked)
                tileSet_Layer2 = ((ListBox)sender).SelectedIndex;
            else if (radLayer3.Checked)
                tileSet_Layer3 = ((ListBox)sender).SelectedIndex;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void numGroundTile_ValueChanged(object sender, EventArgs e)
        {
            if (radLayer1.Checked)
            {
                groundTile_Layer1 = (int)numGroundTile.Value;
            }
            else if (radLayer2.Checked)
            {
                groundTile_Layer2 = (int)numGroundTile.Value;
            }
            else if (radLayer3.Checked)
            {
                groundTile_Layer3 = (int)numGroundTile.Value;
            }
        }

        private void numWallTile_ValueChanged(object sender, EventArgs e)
        {
            if (radLayer1.Checked)
            {
                wallTile_Layer1 = (int)numWallTile.Value;
            }
            else if (radLayer2.Checked)
            {
                wallTile_Layer2 = (int)numWallTile.Value;
            }
            else if (radLayer3.Checked)
            {
                wallTile_Layer3 = (int)numWallTile.Value;
            }
        }
    }
}
