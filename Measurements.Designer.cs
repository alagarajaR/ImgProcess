namespace ImgProcess
{
    partial class Measurements
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.grd1 = new Krypton.Toolkit.KryptonDataGridView();
            this.kryptonGroup1 = new Krypton.Toolkit.KryptonPanel();
            this.grp1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.txtMName = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.cmbMType = new Krypton.Toolkit.KryptonComboBox();
            this.txtMDesc = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.chkDefault = new Krypton.Toolkit.KryptonCheckBox();
            this.txtMId = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            this.txtPGroup = new Krypton.Toolkit.KryptonNumericUpDown();
            this.cmdNew = new Krypton.Toolkit.KryptonButton();
            this.err1 = new ErrLbl.ErrorLabel();
            this.cmdCancel = new Krypton.Toolkit.KryptonButton();
            this.cmdSave = new Krypton.Toolkit.KryptonButton();
            this.cmdUp = new Krypton.Toolkit.KryptonButton();
            this.cmdDown = new Krypton.Toolkit.KryptonButton();
            this.cmdSaveOrder = new Krypton.Toolkit.KryptonButton();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grp1)).BeginInit();
            this.grp1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMType)).BeginInit();
            this.SuspendLayout();
            // 
            // grd1
            // 
            this.grd1.AllowUserToAddRows = false;
            this.grd1.AllowUserToDeleteRows = false;
            this.grd1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.grd1.Location = new System.Drawing.Point(12, 252);
            this.grd1.Name = "grd1";
            this.grd1.Palette = this.kryptonPalette1;
            this.grd1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.grd1.Size = new System.Drawing.Size(709, 149);
            this.grd1.TabIndex = 0;
            this.grd1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd1_CellContentClick);
            this.grd1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd1_CellEnter);
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Controls.Add(this.grp1);
            this.kryptonGroup1.Controls.Add(this.cmdNew);
            this.kryptonGroup1.Controls.Add(this.err1);
            this.kryptonGroup1.Controls.Add(this.cmdCancel);
            this.kryptonGroup1.Controls.Add(this.cmdSave);
            this.kryptonGroup1.Location = new System.Drawing.Point(12, 12);
            this.kryptonGroup1.Name = "kryptonGroup1";
            this.kryptonGroup1.Palette = this.kryptonPalette1;
            this.kryptonGroup1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonGroup1.Size = new System.Drawing.Size(767, 220);
            this.kryptonGroup1.TabIndex = 2;
            // 
            // grp1
            // 
            this.grp1.Controls.Add(this.kryptonLabel1);
            this.grp1.Controls.Add(this.txtMName);
            this.grp1.Controls.Add(this.kryptonLabel2);
            this.grp1.Controls.Add(this.cmbMType);
            this.grp1.Controls.Add(this.txtMDesc);
            this.grp1.Controls.Add(this.kryptonLabel3);
            this.grp1.Controls.Add(this.kryptonLabel4);
            this.grp1.Controls.Add(this.chkDefault);
            this.grp1.Controls.Add(this.txtMId);
            this.grp1.Controls.Add(this.kryptonLabel5);
            this.grp1.Controls.Add(this.txtPGroup);
            this.grp1.Enabled = false;
            this.grp1.Location = new System.Drawing.Point(12, 15);
            this.grp1.Name = "grp1";
            this.grp1.Palette = this.kryptonPalette1;
            this.grp1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.grp1.Size = new System.Drawing.Size(502, 181);
            this.grp1.TabIndex = 18;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 23);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Palette = this.kryptonPalette1;
            this.kryptonLabel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel1.Size = new System.Drawing.Size(93, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Measure Name";
            // 
            // txtMName
            // 
            this.txtMName.Location = new System.Drawing.Point(130, 23);
            this.txtMName.Name = "txtMName";
            this.txtMName.Palette = this.kryptonPalette1;
            this.txtMName.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtMName.Size = new System.Drawing.Size(205, 23);
            this.txtMName.TabIndex = 1;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(13, 52);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Palette = this.kryptonPalette1;
            this.kryptonLabel2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel2.Size = new System.Drawing.Size(73, 20);
            this.kryptonLabel2.TabIndex = 2;
            this.kryptonLabel2.Values.Text = "Description";
            // 
            // cmbMType
            // 
            this.cmbMType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbMType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMType.DropDownWidth = 205;
            this.cmbMType.IntegralHeight = false;
            this.cmbMType.Location = new System.Drawing.Point(130, 81);
            this.cmbMType.Name = "cmbMType";
            this.cmbMType.Size = new System.Drawing.Size(205, 21);
            this.cmbMType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbMType.TabIndex = 15;
            // 
            // txtMDesc
            // 
            this.txtMDesc.Location = new System.Drawing.Point(130, 52);
            this.txtMDesc.Name = "txtMDesc";
            this.txtMDesc.Palette = this.kryptonPalette1;
            this.txtMDesc.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtMDesc.Size = new System.Drawing.Size(356, 23);
            this.txtMDesc.TabIndex = 3;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(13, 78);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Palette = this.kryptonPalette1;
            this.kryptonLabel3.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel3.Size = new System.Drawing.Size(37, 20);
            this.kryptonLabel3.TabIndex = 4;
            this.kryptonLabel3.Values.Text = "Type";
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(13, 104);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Palette = this.kryptonPalette1;
            this.kryptonLabel4.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel4.Size = new System.Drawing.Size(50, 20);
            this.kryptonLabel4.TabIndex = 6;
            this.kryptonLabel4.Values.Text = "Default";
            // 
            // chkDefault
            // 
            this.chkDefault.Location = new System.Drawing.Point(130, 108);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Palette = this.kryptonPalette1;
            this.chkDefault.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.chkDefault.Size = new System.Drawing.Size(44, 20);
            this.chkDefault.TabIndex = 7;
            this.chkDefault.Values.Text = "Y/N";
            // 
            // txtMId
            // 
            this.txtMId.Location = new System.Drawing.Point(341, 23);
            this.txtMId.Name = "txtMId";
            this.txtMId.Size = new System.Drawing.Size(40, 23);
            this.txtMId.TabIndex = 12;
            this.txtMId.Visible = false;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(13, 134);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Palette = this.kryptonPalette1;
            this.kryptonLabel5.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel5.Size = new System.Drawing.Size(111, 20);
            this.kryptonLabel5.TabIndex = 8;
            this.kryptonLabel5.Values.Text = "Penetration Group";
            // 
            // txtPGroup
            // 
            this.txtPGroup.Location = new System.Drawing.Point(130, 132);
            this.txtPGroup.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPGroup.Name = "txtPGroup";
            this.txtPGroup.Palette = this.kryptonPalette1;
            this.txtPGroup.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtPGroup.Size = new System.Drawing.Size(39, 22);
            this.txtPGroup.TabIndex = 10;
            // 
            // cmdNew
            // 
            this.cmdNew.Location = new System.Drawing.Point(544, 15);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Palette = this.kryptonPalette1;
            this.cmdNew.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdNew.Size = new System.Drawing.Size(76, 31);
            this.cmdNew.TabIndex = 17;
            this.cmdNew.Values.Text = "New";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // err1
            // 
            this.err1.BackColor = System.Drawing.Color.Transparent;
            this.err1.Location = new System.Drawing.Point(544, 148);
            this.err1.Name = "err1";
            this.err1.SetVal = "";
            this.err1.Size = new System.Drawing.Size(194, 48);
            this.err1.TabIndex = 16;
            this.err1.Load += new System.EventHandler(this.err1_Load);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Enabled = false;
            this.cmdCancel.Location = new System.Drawing.Point(544, 94);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Palette = this.kryptonPalette1;
            this.cmdCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdCancel.Size = new System.Drawing.Size(76, 31);
            this.cmdCancel.TabIndex = 14;
            this.cmdCancel.Values.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(544, 57);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Palette = this.kryptonPalette1;
            this.cmdSave.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdSave.Size = new System.Drawing.Size(76, 31);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Values.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdUp
            // 
            this.cmdUp.Location = new System.Drawing.Point(727, 252);
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.Palette = this.kryptonPalette1;
            this.cmdUp.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdUp.Size = new System.Drawing.Size(52, 31);
            this.cmdUp.TabIndex = 15;
            this.cmdUp.Values.Text = "Up";
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // cmdDown
            // 
            this.cmdDown.Location = new System.Drawing.Point(727, 289);
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.Palette = this.kryptonPalette1;
            this.cmdDown.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdDown.Size = new System.Drawing.Size(52, 31);
            this.cmdDown.TabIndex = 16;
            this.cmdDown.Values.Text = "Down";
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdSaveOrder
            // 
            this.cmdSaveOrder.Location = new System.Drawing.Point(727, 326);
            this.cmdSaveOrder.Name = "cmdSaveOrder";
            this.cmdSaveOrder.Palette = this.kryptonPalette1;
            this.cmdSaveOrder.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmdSaveOrder.Size = new System.Drawing.Size(52, 75);
            this.cmdSaveOrder.TabIndex = 17;
            this.cmdSaveOrder.Values.Text = " Save \r\nOrder";
            this.cmdSaveOrder.Click += new System.EventHandler(this.cmdSaveOrder_Click);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "MId";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Description";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Type";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 130;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Default";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Group";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "Edit";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column7.HeaderText = "Edit";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Delete";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column8.HeaderText = "Delete";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Measurements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 413);
            this.Controls.Add(this.cmdSaveOrder);
            this.Controls.Add(this.cmdDown);
            this.Controls.Add(this.cmdUp);
            this.Controls.Add(this.kryptonGroup1);
            this.Controls.Add(this.grd1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Measurements";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Measurements";
            this.Load += new System.EventHandler(this.Measurements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grp1)).EndInit();
            this.grp1.ResumeLayout(false);
            this.grp1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private Krypton.Toolkit.KryptonDataGridView grd1;
        private Krypton.Toolkit.KryptonPanel kryptonGroup1;
        private Krypton.Toolkit.KryptonTextBox txtMName;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox txtMDesc;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonCheckBox chkDefault;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonNumericUpDown txtPGroup;
        private Krypton.Toolkit.KryptonTextBox txtMId;
        private Krypton.Toolkit.KryptonButton cmdSave;
        private Krypton.Toolkit.KryptonButton cmdCancel;
        private Krypton.Toolkit.KryptonButton cmdUp;
        private Krypton.Toolkit.KryptonButton cmdDown;
        private Krypton.Toolkit.KryptonButton cmdSaveOrder;
        private Krypton.Toolkit.KryptonComboBox cmbMType;
        private ErrLbl.ErrorLabel err1;
        private Krypton.Toolkit.KryptonButton cmdNew;
        private Krypton.Toolkit.KryptonPanel grp1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
    }
}