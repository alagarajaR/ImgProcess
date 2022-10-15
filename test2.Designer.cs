namespace ImgProcess
{
    partial class test2
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
            this.IBox1 = new Emgu.CV.UI.ImageBox();
            this.imgMag = new Emgu.CV.UI.ImageBox();
            this.TBMag = new System.Windows.Forms.TrackBar();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.IBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBMag)).BeginInit();
            this.SuspendLayout();
            // 
            // IBox1
            // 
            this.IBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBox1.Location = new System.Drawing.Point(1, 12);
            this.IBox1.Name = "IBox1";
            this.IBox1.Size = new System.Drawing.Size(736, 503);
            this.IBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IBox1.TabIndex = 3;
            this.IBox1.TabStop = false;
            this.IBox1.Click += new System.EventHandler(this.IBox1_Click);
            this.IBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IBox1_MouseMove);
            // 
            // imgMag
            // 
            this.imgMag.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imgMag.Location = new System.Drawing.Point(788, 12);
            this.imgMag.Name = "imgMag";
            this.imgMag.Size = new System.Drawing.Size(150, 150);
            this.imgMag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgMag.TabIndex = 4;
            this.imgMag.TabStop = false;
            // 
            // TBMag
            // 
            this.TBMag.LargeChange = 10;
            this.TBMag.Location = new System.Drawing.Point(788, 168);
            this.TBMag.Maximum = 90;
            this.TBMag.Minimum = 30;
            this.TBMag.Name = "TBMag";
            this.TBMag.Size = new System.Drawing.Size(150, 45);
            this.TBMag.TabIndex = 5;
            this.TBMag.TickFrequency = 10;
            this.TBMag.Value = 30;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(801, 280);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(108, 63);
            this.kryptonButton1.TabIndex = 6;
            this.kryptonButton1.Values.Text = "kryptonButton1";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // test2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 619);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.TBMag);
            this.Controls.Add(this.imgMag);
            this.Controls.Add(this.IBox1);
            this.Name = "test2";
            this.Text = "test2";
            this.Load += new System.EventHandler(this.test2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBMag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox IBox1;
        private Emgu.CV.UI.ImageBox imgMag;
        private System.Windows.Forms.TrackBar TBMag;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}