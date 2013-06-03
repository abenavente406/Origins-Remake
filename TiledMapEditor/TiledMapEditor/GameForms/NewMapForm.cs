using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TiledMapEditor.GameForms
{
    public enum LevelType
    {

        Blank,
        Random,
        Dungeon,
        Perlin
    }

    public partial class NewMapForm : Form
    {
        public int mapWidth, mapHeight, tileWidth, tileHeight;

        public string mapName = "";

        public LevelType levelType = LevelType.Blank;

        public NewMapForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
        }

        private void NewMapForm_Load(object sender, EventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mapName = txtMapName.Text;
            mapWidth = Convert.ToInt32(numMapWidth.Value);
            mapHeight= Convert.ToInt32(numMapHeight.Value);
            tileWidth = Convert.ToInt32(numTileWidth.Value);
            tileHeight = Convert.ToInt32(numTileHeight.Value);

            switch (lstLevelTypes.SelectedIndex)
            {
                case 0:
                    levelType = LevelType.Blank;
                    break;
                case 1:
                    levelType = LevelType.Dungeon;
                    break;
                case 2:
                    levelType = LevelType.Random;
                    break;
                case 3:
                    levelType = LevelType.Perlin;
                    break;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}
