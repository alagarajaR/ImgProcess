namespace ImgProcess
{
    partial class camera
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
            this.components = new System.ComponentModel.Container();
            this.pbMainImage = new System.Windows.Forms.PictureBox();
            this.cmdStart = new Krypton.Toolkit.KryptonButton();
            this.kryptonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.cmdPause = new Krypton.Toolkit.KryptonButton();
            this.cmdStop = new Krypton.Toolkit.KryptonButton();
            this.saveAsImageToolStripMenuItem = new Krypton.Toolkit.KryptonButton();
            this.cmbModel = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModel)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMainImage
            // 
            this.pbMainImage.Location = new System.Drawing.Point(2, 82);
            this.pbMainImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbMainImage.Name = "pbMainImage";
            this.pbMainImage.Size = new System.Drawing.Size(1396, 666);
            this.pbMainImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMainImage.TabIndex = 33;
            this.pbMainImage.TabStop = false;
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(265, 23);
            this.cmdStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Palette = this.kryptonPalette1;
            this.cmdStart.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdStart.Size = new System.Drawing.Size(86, 26);
            this.cmdStart.TabIndex = 34;
            this.cmdStart.Values.Text = "Start";
            this.cmdStart.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // cmdPause
            // 
            this.cmdPause.Location = new System.Drawing.Point(373, 23);
            this.cmdPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdPause.Name = "cmdPause";
            this.cmdPause.Palette = this.kryptonPalette1;
            this.cmdPause.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdPause.Size = new System.Drawing.Size(86, 26);
            this.cmdPause.TabIndex = 35;
            this.cmdPause.Values.Text = "Pause";
            this.cmdPause.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Location = new System.Drawing.Point(477, 23);
            this.cmdStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Palette = this.kryptonPalette1;
            this.cmdStop.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdStop.Size = new System.Drawing.Size(86, 26);
            this.cmdStop.TabIndex = 36;
            this.cmdStop.Values.Text = "Stop";
            this.cmdStop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // saveAsImageToolStripMenuItem
            // 
            this.saveAsImageToolStripMenuItem.Location = new System.Drawing.Point(581, 23);
            this.saveAsImageToolStripMenuItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Palette = this.kryptonPalette1;
            this.saveAsImageToolStripMenuItem.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.saveAsImageToolStripMenuItem.Size = new System.Drawing.Size(86, 26);
            this.saveAsImageToolStripMenuItem.TabIndex = 37;
            this.saveAsImageToolStripMenuItem.Values.Text = "Save as Image";
            this.saveAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageToolStripMenuItem_Click);
            // 
            // cmbModel
            // 
            this.cmbModel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.DropDownWidth = 123;
            this.cmbModel.IntegralHeight = false;
            this.cmbModel.Items.AddRange(new object[] {
            "M30",
            "M50",
            "M60",
            "MDX10",
            "IDS Peak"});
            this.cmbModel.Location = new System.Drawing.Point(76, 24);
            this.cmbModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(164, 25);
            this.cmbModel.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbModel.TabIndex = 46;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 25);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Palette = this.kryptonPalette1;
            this.kryptonLabel2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel2.Size = new System.Drawing.Size(55, 24);
            this.kryptonLabel2.TabIndex = 45;
            this.kryptonLabel2.Values.Text = "Model";
            // 
            // camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 763);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.saveAsImageToolStripMenuItem);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdPause);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.pbMainImage);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "camera";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "camera";
            this.Load += new System.EventHandler(this.camera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbMainImage;
        private Krypton.Toolkit.KryptonButton cmdStart;
        private Krypton.Toolkit.KryptonButton cmdPause;
        private Krypton.Toolkit.KryptonButton cmdStop;
        private Krypton.Toolkit.KryptonButton saveAsImageToolStripMenuItem;
        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private Krypton.Toolkit.KryptonComboBox cmbModel;
        public Krypton.Toolkit.KryptonLabel kryptonLabel2;
    }
}