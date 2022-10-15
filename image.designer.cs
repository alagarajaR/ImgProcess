namespace ImgProcess
{
    partial class frmImage
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
            this.img1 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.img1)).BeginInit();
            this.SuspendLayout();
            // 
            // img1
            // 
            this.img1.BackColor = System.Drawing.Color.Transparent;
            this.img1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.img1.Location = new System.Drawing.Point(8, 2);
            this.img1.Name = "img1";
            this.img1.Size = new System.Drawing.Size(800, 600);
            this.img1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.img1.TabIndex = 3;
            this.img1.TabStop = false;
            this.img1.Click += new System.EventHandler(this.img1_Click);
            this.img1.Paint += new System.Windows.Forms.PaintEventHandler(this.img1_Paint);
            this.img1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.img1_MouseDown);
            this.img1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.img1_MouseMove);
            this.img1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.img1_MouseUp);
            // 
            // frmImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(50, 0);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1245, 749);
            this.Controls.Add(this.img1);
            this.DoubleBuffered = true;
            this.Name = "frmImage";
            this.Text = "image";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.image_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmImage_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.img1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Emgu.CV.UI.ImageBox img1;
    }
}