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
    public partial class NewTileSheetForm : Form
    {

        public String sheetFileName;

        public int tileWidth = 32;
        public int tileHeight = 32;

        public NewTileSheetForm()
        {
            InitializeComponent();
        }

        private void numTileWidth_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sheetFileName = openFileDialog.FileName;
                textBox1.Text = sheetFileName;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(textBox1.Text))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this, "You haven't loaded a tile sheet!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
