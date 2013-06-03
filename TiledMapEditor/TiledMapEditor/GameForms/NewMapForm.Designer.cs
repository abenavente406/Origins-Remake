namespace TiledMapEditor.GameForms
{
    partial class NewMapForm
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
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.numMapWidth = new System.Windows.Forms.NumericUpDown();
            this.numTileWidth = new System.Windows.Forms.NumericUpDown();
            this.numMapHeight = new System.Windows.Forms.NumericUpDown();
            this.numTileHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstLevelTypes = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(80, 55);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(210, 20);
            this.txtMapName.TabIndex = 0;
            // 
            // numMapWidth
            // 
            this.numMapWidth.Location = new System.Drawing.Point(79, 97);
            this.numMapWidth.Name = "numMapWidth";
            this.numMapWidth.Size = new System.Drawing.Size(57, 20);
            this.numMapWidth.TabIndex = 1;
            this.numMapWidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numTileWidth
            // 
            this.numTileWidth.Location = new System.Drawing.Point(79, 123);
            this.numTileWidth.Name = "numTileWidth";
            this.numTileWidth.Size = new System.Drawing.Size(57, 20);
            this.numTileWidth.TabIndex = 1;
            this.numTileWidth.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // numMapHeight
            // 
            this.numMapHeight.Location = new System.Drawing.Point(233, 97);
            this.numMapHeight.Name = "numMapHeight";
            this.numMapHeight.Size = new System.Drawing.Size(57, 20);
            this.numMapHeight.TabIndex = 1;
            this.numMapHeight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numTileHeight
            // 
            this.numTileHeight.Location = new System.Drawing.Point(233, 123);
            this.numTileHeight.Name = "numTileHeight";
            this.numTileHeight.Size = new System.Drawing.Size(57, 20);
            this.numTileHeight.TabIndex = 1;
            this.numTileHeight.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Map Width:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tile Width:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Map Height:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(165, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tile Height:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(113, 31);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(176, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 31);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lstLevelTypes
            // 
            this.lstLevelTypes.FormattingEnabled = true;
            this.lstLevelTypes.Items.AddRange(new object[] {
            "Blank Level",
            "Dugeon Level",
            "100% Random Level",
            "Perlin Level"});
            this.lstLevelTypes.Location = new System.Drawing.Point(12, 12);
            this.lstLevelTypes.Name = "lstLevelTypes";
            this.lstLevelTypes.Size = new System.Drawing.Size(278, 30);
            this.lstLevelTypes.TabIndex = 4;
            // 
            // NewMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 211);
            this.Controls.Add(this.lstLevelTypes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numTileHeight);
            this.Controls.Add(this.numTileWidth);
            this.Controls.Add(this.numMapHeight);
            this.Controls.Add(this.numMapWidth);
            this.Controls.Add(this.txtMapName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewMapForm";
            this.Text = "New Map";
            this.Load += new System.EventHandler(this.NewMapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.NumericUpDown numMapWidth;
        private System.Windows.Forms.NumericUpDown numTileWidth;
        private System.Windows.Forms.NumericUpDown numMapHeight;
        private System.Windows.Forms.NumericUpDown numTileHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstLevelTypes;
    }
}