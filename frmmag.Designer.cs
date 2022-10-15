namespace ImgProcess
{
    partial class frmmag
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
            this.kryptonPanel2 = new Krypton.Toolkit.KryptonPanel();
            this.TBMagWM = new Krypton.Toolkit.KryptonTrackBar();
            this.imgMagWM = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgMagWM)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.TBMagWM);
            this.kryptonPanel2.Controls.Add(this.imgMagWM);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(208, 169);
            this.kryptonPanel2.TabIndex = 44;
            // 
            // TBMagWM
            // 
            this.TBMagWM.LargeChange = 10;
            this.TBMagWM.Location = new System.Drawing.Point(169, 7);
            this.TBMagWM.Maximum = 90;
            this.TBMagWM.Minimum = 30;
            this.TBMagWM.Name = "TBMagWM";
            this.TBMagWM.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TBMagWM.Size = new System.Drawing.Size(27, 150);
            this.TBMagWM.SmallChange = 10;
            this.TBMagWM.TabIndex = 40;
            this.TBMagWM.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TBMagWM.TrackBarSize = Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.TBMagWM.Value = 30;
            // 
            // imgMagWM
            // 
            this.imgMagWM.BackColor = System.Drawing.Color.Transparent;
            this.imgMagWM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgMagWM.Location = new System.Drawing.Point(9, 7);
            this.imgMagWM.Name = "imgMagWM";
            this.imgMagWM.Size = new System.Drawing.Size(150, 150);
            this.imgMagWM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgMagWM.TabIndex = 6;
            this.imgMagWM.TabStop = false;
            // 
            // frmmag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 169);
            this.Controls.Add(this.kryptonPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmmag";
            this.Text = "Magnifier";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgMagWM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel2;
        public Krypton.Toolkit.KryptonTrackBar TBMagWM;
        public System.Windows.Forms.PictureBox imgMagWM;
    }
}