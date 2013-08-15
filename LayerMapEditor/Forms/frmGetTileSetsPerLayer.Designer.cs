namespace LayerMapEditor.Forms
{
    partial class frmGetTileSetsPerLayer
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
            this.lstTileSets = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radLayer3 = new System.Windows.Forms.RadioButton();
            this.radLayer2 = new System.Windows.Forms.RadioButton();
            this.radLayer1 = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numGroundTile = new System.Windows.Forms.NumericUpDown();
            this.numWallTile = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroundTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallTile)).BeginInit();
            this.SuspendLayout();
            // 
            // lstTileSets
            // 
            this.lstTileSets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTileSets.FormattingEnabled = true;
            this.lstTileSets.Location = new System.Drawing.Point(12, 91);
            this.lstTileSets.Name = "lstTileSets";
            this.lstTileSets.Size = new System.Drawing.Size(384, 80);
            this.lstTileSets.TabIndex = 0;
            this.lstTileSets.SelectedIndexChanged += new System.EventHandler(this.lstTileSets_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select tile set for this layer";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radLayer3);
            this.groupBox1.Controls.Add(this.radLayer2);
            this.groupBox1.Controls.Add(this.radLayer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the layer to edit";
            // 
            // radLayer3
            // 
            this.radLayer3.AutoSize = true;
            this.radLayer3.Location = new System.Drawing.Point(277, 28);
            this.radLayer3.Name = "radLayer3";
            this.radLayer3.Size = new System.Drawing.Size(98, 17);
            this.radLayer3.TabIndex = 0;
            this.radLayer3.Text = "Layer 3 (Decor)";
            this.radLayer3.UseVisualStyleBackColor = true;
            this.radLayer3.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radLayer2
            // 
            this.radLayer2.AutoSize = true;
            this.radLayer2.Location = new System.Drawing.Point(141, 28);
            this.radLayer2.Name = "radLayer2";
            this.radLayer2.Size = new System.Drawing.Size(112, 17);
            this.radLayer2.TabIndex = 0;
            this.radLayer2.Text = "Layer 2 (Collisions)";
            this.radLayer2.UseVisualStyleBackColor = true;
            this.radLayer2.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // radLayer1
            // 
            this.radLayer1.AutoSize = true;
            this.radLayer1.Checked = true;
            this.radLayer1.Location = new System.Drawing.Point(16, 28);
            this.radLayer1.Name = "radLayer1";
            this.radLayer1.Size = new System.Drawing.Size(104, 17);
            this.radLayer1.TabIndex = 0;
            this.radLayer1.TabStop = true;
            this.radLayer1.Text = "Layer 1 (Ground)";
            this.radLayer1.UseVisualStyleBackColor = true;
            this.radLayer1.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(67, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(242, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // numGroundTile
            // 
            this.numGroundTile.Location = new System.Drawing.Point(90, 177);
            this.numGroundTile.Name = "numGroundTile";
            this.numGroundTile.Size = new System.Drawing.Size(90, 20);
            this.numGroundTile.TabIndex = 4;
            this.numGroundTile.ValueChanged += new System.EventHandler(this.numGroundTile_ValueChanged);
            // 
            // numWallTile
            // 
            this.numWallTile.Location = new System.Drawing.Point(276, 177);
            this.numWallTile.Name = "numWallTile";
            this.numWallTile.Size = new System.Drawing.Size(90, 20);
            this.numWallTile.TabIndex = 4;
            this.numWallTile.ValueChanged += new System.EventHandler(this.numWallTile_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ground Tile:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Wall Tile:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // frmGetTileSetsPerLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 255);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numWallTile);
            this.Controls.Add(this.numGroundTile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstTileSets);
            this.Name = "frmGetTileSetsPerLayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGetTileSetsPerLayer";
            this.Load += new System.EventHandler(this.frmGetTileSetsPerLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGroundTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWallTile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTileSets;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radLayer3;
        private System.Windows.Forms.RadioButton radLayer2;
        private System.Windows.Forms.RadioButton radLayer1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numGroundTile;
        private System.Windows.Forms.NumericUpDown numWallTile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}