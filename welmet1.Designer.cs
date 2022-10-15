namespace ImgProcess
{
    partial class welmet1
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
            this.grd1 = new Krypton.Toolkit.KryptonDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kryptonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbObj2 = new Krypton.Toolkit.KryptonComboBox();
            this.cmbObj1 = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonHeader2 = new Krypton.Toolkit.KryptonHeader();
            this.kryptonHeader1 = new Krypton.Toolkit.KryptonHeader();
            this.kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            this.err1 = new ErrLbl.ErrorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbObj2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbObj1)).BeginInit();
            this.SuspendLayout();
            // 
            // grd1
            // 
            this.grd1.AllowUserToAddRows = false;
            this.grd1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.grd1.Location = new System.Drawing.Point(12, 33);
            this.grd1.Name = "grd1";
            this.grd1.Palette = this.kryptonPalette1;
            this.grd1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.grd1.Size = new System.Drawing.Size(365, 182);
            this.grd1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Type";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonButton3);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.cmbObj2);
            this.kryptonPanel1.Controls.Add(this.cmbObj1);
            this.kryptonPanel1.Controls.Add(this.kryptonHeader2);
            this.kryptonPanel1.Location = new System.Drawing.Point(12, 221);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Palette = this.kryptonPalette1;
            this.kryptonPanel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonPanel1.Size = new System.Drawing.Size(365, 160);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Location = new System.Drawing.Point(25, 115);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Palette = this.kryptonPalette1;
            this.kryptonButton3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonButton3.Size = new System.Drawing.Size(70, 25);
            this.kryptonButton3.TabIndex = 7;
            this.kryptonButton3.Values.Text = "Clear";
            this.kryptonButton3.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(111, 115);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Palette = this.kryptonPalette1;
            this.kryptonButton1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonButton1.Size = new System.Drawing.Size(70, 25);
            this.kryptonButton1.TabIndex = 4;
            this.kryptonButton1.Values.Text = "Compute";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(25, 74);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Palette = this.kryptonPalette1;
            this.kryptonLabel2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel2.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel2.TabIndex = 6;
            this.kryptonLabel2.Values.Text = "Object 2";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(25, 48);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Palette = this.kryptonPalette1;
            this.kryptonLabel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel1.Size = new System.Drawing.Size(56, 20);
            this.kryptonLabel1.TabIndex = 4;
            this.kryptonLabel1.Values.Text = "Object 1";
            // 
            // cmbObj2
            // 
            this.cmbObj2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbObj2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObj2.DropDownWidth = 108;
            this.cmbObj2.IntegralHeight = false;
            this.cmbObj2.Location = new System.Drawing.Point(100, 75);
            this.cmbObj2.Name = "cmbObj2";
            this.cmbObj2.Palette = this.kryptonPalette1;
            this.cmbObj2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbObj2.Size = new System.Drawing.Size(83, 21);
            this.cmbObj2.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbObj2.TabIndex = 5;
            // 
            // cmbObj1
            // 
            this.cmbObj1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbObj1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObj1.DropDownWidth = 83;
            this.cmbObj1.IntegralHeight = false;
            this.cmbObj1.Location = new System.Drawing.Point(100, 48);
            this.cmbObj1.Name = "cmbObj1";
            this.cmbObj1.Palette = this.kryptonPalette1;
            this.cmbObj1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbObj1.Size = new System.Drawing.Size(83, 21);
            this.cmbObj1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbObj1.TabIndex = 4;
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.Palette = this.kryptonPalette1;
            this.kryptonHeader2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonHeader2.Size = new System.Drawing.Size(365, 31);
            this.kryptonHeader2.TabIndex = 4;
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "Compute";
            this.kryptonHeader2.Values.Image = null;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Location = new System.Drawing.Point(12, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Palette = this.kryptonPalette1;
            this.kryptonHeader1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonHeader1.Size = new System.Drawing.Size(78, 31);
            this.kryptonHeader1.TabIndex = 3;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Objects";
            this.kryptonHeader1.Values.Image = null;
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(321, 401);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Palette = this.kryptonPalette1;
            this.kryptonButton2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonButton2.Size = new System.Drawing.Size(56, 36);
            this.kryptonButton2.TabIndex = 5;
            this.kryptonButton2.Values.Text = "Close";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // err1
            // 
            this.err1.BackColor = System.Drawing.Color.Transparent;
            this.err1.Location = new System.Drawing.Point(13, 410);
            this.err1.Name = "err1";
            this.err1.SetVal = "";
            this.err1.Size = new System.Drawing.Size(282, 27);
            this.err1.TabIndex = 6;
            this.err1.Load += new System.EventHandler(this.err1_Load);
            // 
            // welmet1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 449);
            this.Controls.Add(this.err1);
            this.Controls.Add(this.kryptonButton2);
            this.Controls.Add(this.kryptonHeader1);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.grd1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "welmet1";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.welmet1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbObj2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbObj1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonDataGridView grd1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonHeader kryptonHeader2;
        private Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox cmbObj2;
        private Krypton.Toolkit.KryptonComboBox cmbObj1;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private ErrLbl.ErrorLabel err1;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
    }
}