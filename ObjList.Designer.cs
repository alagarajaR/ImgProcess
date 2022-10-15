namespace ImgProcess
{
    partial class ObjectList
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
            this.lst1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lst1
            // 
            this.lst1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst1.FormattingEnabled = true;
            this.lst1.Location = new System.Drawing.Point(0, 0);
            this.lst1.Name = "lst1";
            this.lst1.Size = new System.Drawing.Size(242, 270);
            this.lst1.TabIndex = 5;
            this.lst1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lst1_KeyDown);
            // 
            // ObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 270);
            this.Controls.Add(this.lst1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ObjectList";
            this.Text = "Object List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ObjectList_FormClosing);
            this.Load += new System.EventHandler(this.ObjectList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox lst1;
    }
}