namespace TiledMapEditor.GameForms
{
    partial class NewTileSheetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpTileDimensions = new System.Windows.Forms.GroupBox();
            this.numTileWidth = new System.Windows.Forms.NumericUpDown();
            this.numTileHeight = new System.Windows.Forms.NumericUpDown();
            this.lblTileWidthPrompt = new System.Windows.Forms.Label();
            this.lblTileHeighPrompt = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpTileDimensions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTileDimensions
            // 
            this.grpTileDimensions.Controls.Add(this.lblTileHeighPrompt);
            this.grpTileDimensions.Controls.Add(this.lblTileWidthPrompt);
            this.grpTileDimensions.Controls.Add(this.numTileHeight);
            this.grpTileDimensions.Controls.Add(this.numTileWidth);
            this.grpTileDimensions.Location = new System.Drawing.Point(15, 52);
            this.grpTileDimensions.Name = "grpTileDimensions";
            this.grpTileDimensions.Size = new System.Drawing.Size(200, 100);
            this.grpTileDimensions.TabIndex = 0;
            this.grpTileDimensions.TabStop = false;
            this.grpTileDimensions.Text = "Tile Dimensions";
            // 
            // numTileWidth
            // 
            this.numTileWidth.Location = new System.Drawing.Point(102, 25);
            this.numTileWidth.Name = "numTileWidth";
            this.numTileWidth.Size = new System.Drawing.Size(78, 20);
            this.numTileWidth.TabIndex = 0;
            this.numTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numTileWidth.ValueChanged += new System.EventHandler(this.numTileWidth_ValueChanged);
            // 
            // numTileHeight
            // 
            this.numTileHeight.Location = new System.Drawing.Point(102, 63);
            this.numTileHeight.Name = "numTileHeight";
            this.numTileHeight.Size = new System.Drawing.Size(78, 20);
            this.numTileHeight.TabIndex = 0;
            this.numTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // lblTileWidthPrompt
            // 
            this.lblTileWidthPrompt.AutoSize = true;
            this.lblTileWidthPrompt.Location = new System.Drawing.Point(6, 27);
            this.lblTileWidthPrompt.Name = "lblTileWidthPrompt";
            this.lblTileWidthPrompt.Size = new System.Drawing.Size(58, 13);
            this.lblTileWidthPrompt.TabIndex = 1;
            this.lblTileWidthPrompt.Text = "Tile Width:";
            // 
            // lblTileHeighPrompt
            // 
            this.lblTileHeighPrompt.AutoSize = true;
            this.lblTileHeighPrompt.Location = new System.Drawing.Point(6, 65);
            this.lblTileHeighPrompt.Name = "lblTileHeighPrompt";
            this.lblTileHeighPrompt.Size = new System.Drawing.Size(64, 13);
            this.lblTileHeighPrompt.TabIndex = 1;
            this.lblTileHeighPrompt.Text = "Tile Height: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tile Sheet Location:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(74, 174);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 37);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(238, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(106, 37);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(317, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PNG Image | *.png";
            this.openFileDialog.Title = "New Tile Sheet";
            // 
            // NewTileSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 238);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.grpTileDimensions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewTileSheetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Tile Sheet";
            this.grpTileDimensions.ResumeLayout(false);
            this.grpTileDimensions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTileDimensions;
        private System.Windows.Forms.Label lblTileHeighPrompt;
        private System.Windows.Forms.Label lblTileWidthPrompt;
        private System.Windows.Forms.NumericUpDown numTileHeight;
        private System.Windows.Forms.NumericUpDown numTileWidth;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}