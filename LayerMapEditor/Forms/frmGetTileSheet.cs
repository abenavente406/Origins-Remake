using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OriginsLib.TileEngine;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace LayerMapEditor.Forms
{
    public partial class frmGetTileSheet : Form
    {
        private MainGame gameRef;

        public int SelectedTileSheetId { get; set; }

        public frmGetTileSheet(MainGame game)
        {
            InitializeComponent();

            gameRef = game;
        }

        private void frmGetTileSheet_Load(object sender, EventArgs e)
        {
            foreach (TileSet t in gameRef.level.TileSets)
            {
                lstTileSheets.Items.Add(t.Path);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedTileSheetId = lstTileSheets.SelectedIndex;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.FileName = "";
            openFileDlg.Filter = "PNG Images (.png)|*.png";
            openFileDlg.Multiselect = false;
            DialogResult result = openFileDlg.ShowDialog();


            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var filename = openFileDlg.FileName.Split('\\');
                var fs = File.Open(openFileDlg.FileName, FileMode.Open);
                var tileSet = new TileSet(Texture2D.FromStream(gameRef.GraphicsDevice, fs),
                    Engine.TileWidth, Engine.TileHeight, filename[filename.Length - 2] + "\\" + filename[filename.Length - 1]);
                gameRef.level.AddTileSet(tileSet);
                lstTileSheets.Items.Add(
                    gameRef.level.TileSets[gameRef.level.TileSets.Count - 1].Path
                );
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gameRef.level.TileSets.Count > 0)
            {
                gameRef.hud.SelectedTileIndex = 0;
                gameRef.hud.SelectedTileSetIndex--;
                gameRef.level.TileSets.RemoveAt(lstTileSheets.SelectedIndex);
                lstTileSheets.Items.RemoveAt(lstTileSheets.SelectedIndex);
            }
        }
    }
}
