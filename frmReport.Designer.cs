namespace ImgProcess
{
    partial class frmReport
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
            this.kryptonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.button1 = new Krypton.Toolkit.KryptonButton();
            this.button2 = new Krypton.Toolkit.KryptonButton();
            this.txt1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbProduct = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 73);
            this.button1.Name = "button1";
            this.button1.Palette = this.kryptonPalette1;
            this.button1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.button1.Size = new System.Drawing.Size(82, 32);
            this.button1.TabIndex = 0;
            this.button1.Values.Text = "PDF";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 73);
            this.button2.Name = "button2";
            this.button2.Palette = this.kryptonPalette1;
            this.button2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.button2.Size = new System.Drawing.Size(82, 32);
            this.button2.TabIndex = 1;
            this.button2.Values.Text = "EXCEL";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt1
            // 
            this.txt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.ForeColor = System.Drawing.Color.Black;
            this.txt1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.txt1.Location = new System.Drawing.Point(5, 14);
            this.txt1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.txt1.Name = "txt1";
            this.txt1.Palette = this.kryptonPalette1;
            this.txt1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txt1.Size = new System.Drawing.Size(108, 20);
            this.txt1.TabIndex = 15;
            this.txt1.Values.Text = "Report Templete";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.DropDownWidth = 208;
            this.cmbProduct.IntegralHeight = false;
            this.cmbProduct.Location = new System.Drawing.Point(5, 37);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Palette = this.kryptonPalette1;
            this.cmbProduct.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbProduct.Size = new System.Drawing.Size(258, 21);
            this.cmbProduct.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbProduct.TabIndex = 14;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.txt1);
            this.kryptonPanel1.Controls.Add(this.button1);
            this.kryptonPanel1.Controls.Add(this.cmbProduct);
            this.kryptonPanel1.Controls.Add(this.button2);
            this.kryptonPanel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Palette = this.kryptonPalette1;
            this.kryptonPanel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonPanel1.Size = new System.Drawing.Size(266, 128);
            this.kryptonPanel1.TabIndex = 16;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(200, 73);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Palette = this.kryptonPalette1;
            this.kryptonButton1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonButton1.Size = new System.Drawing.Size(63, 32);
            this.kryptonButton1.TabIndex = 16;
            this.kryptonButton1.Values.Text = "Close";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 152);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReport";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private Krypton.Toolkit.KryptonButton button1;
        private Krypton.Toolkit.KryptonButton button2;
        private Krypton.Toolkit.KryptonLabel txt1;
        private Krypton.Toolkit.KryptonComboBox cmbProduct;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}