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
    public partial class NewMapForm : Form
    {

        public int mapWidth, mapHeight, tileWidth, tileHeight;

        public string mapName = "";

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

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}
