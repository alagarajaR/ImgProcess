namespace ImgProcess
{
    partial class frmMultireport
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
            this.button1 = new Krypton.Toolkit.KryptonButton();
            this.button2 = new Krypton.Toolkit.KryptonButton();
            this.txt1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbProduct = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbComponents = new Krypton.Toolkit.KryptonComboBox();
            this.txtTDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.txtFDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonLabel7 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.grd2 = new Krypton.Toolkit.KryptonDataGridView();
            this.Column4 = new Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbComponents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 325);
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
            this.button2.Location = new System.Drawing.Point(97, 325);
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
            this.txt1.Location = new System.Drawing.Point(9, 266);
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
            this.cmbProduct.Location = new System.Drawing.Point(9, 289);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Palette = this.kryptonPalette1;
            this.cmbProduct.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbProduct.Size = new System.Drawing.Size(258, 21);
            this.cmbProduct.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbProduct.TabIndex = 14;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.grd2);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.txtTDate);
            this.kryptonPanel1.Controls.Add(this.txtFDate);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.cmbComponents);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.txt1);
            this.kryptonPanel1.Controls.Add(this.button1);
            this.kryptonPanel1.Controls.Add(this.cmbProduct);
            this.kryptonPanel1.Controls.Add(this.button2);
            this.kryptonPanel1.Location = new System.Drawing.Point(12, 12);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Palette = this.kryptonPalette1;
            this.kryptonPanel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonPanel1.Size = new System.Drawing.Size(457, 364);
            this.kryptonPanel1.TabIndex = 16;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(380, 325);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Palette = this.kryptonPalette1;
            this.kryptonButton1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonButton1.Size = new System.Drawing.Size(63, 32);
            this.kryptonButton1.TabIndex = 16;
            this.kryptonButton1.Values.Text = "Close";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.ForeColor = System.Drawing.Color.Black;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(8, 51);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Palette = this.kryptonPalette1;
            this.kryptonLabel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel1.Size = new System.Drawing.Size(79, 20);
            this.kryptonLabel1.TabIndex = 18;
            this.kryptonLabel1.Values.Text = "Component";
            // 
            // cmbComponents
            // 
            this.cmbComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComponents.DropDownWidth = 208;
            this.cmbComponents.IntegralHeight = false;
            this.cmbComponents.Location = new System.Drawing.Point(8, 74);
            this.cmbComponents.Name = "cmbComponents";
            this.cmbComponents.Palette = this.kryptonPalette1;
            this.cmbComponents.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbComponents.Size = new System.Drawing.Size(258, 21);
            this.cmbComponents.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbComponents.TabIndex = 17;
            this.cmbComponents.SelectedIndexChanged += new System.EventHandler(this.cmbComponents_SelectedIndexChanged);
            // 
            // txtTDate
            // 
            this.txtTDate.CustomFormat = "dd-MM-yyyy";
            this.txtTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTDate.Location = new System.Drawing.Point(111, 27);
            this.txtTDate.Name = "txtTDate";
            this.txtTDate.Palette = this.kryptonPalette1;
            this.txtTDate.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtTDate.Size = new System.Drawing.Size(98, 21);
            this.txtTDate.TabIndex = 21;
            this.txtTDate.ValueChanged += new System.EventHandler(this.txtIMToDate_ValueChanged);
            // 
            // txtFDate
            // 
            this.txtFDate.CustomFormat = "dd-MM-yyyy";
            this.txtFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFDate.Location = new System.Drawing.Point(9, 27);
            this.txtFDate.Name = "txtFDate";
            this.txtFDate.Palette = this.kryptonPalette1;
            this.txtFDate.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtFDate.Size = new System.Drawing.Size(98, 21);
            this.txtFDate.TabIndex = 20;
            this.txtFDate.ValueChanged += new System.EventHandler(this.txtIMFDate_ValueChanged);
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(5, 3);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Palette = this.kryptonPalette1;
            this.kryptonLabel7.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel7.Size = new System.Drawing.Size(89, 20);
            this.kryptonLabel7.TabIndex = 19;
            this.kryptonLabel7.Values.Text = "Date Selection";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel2.ForeColor = System.Drawing.Color.Black;
            this.kryptonLabel2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel2.Location = new System.Drawing.Point(8, 98);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Palette = this.kryptonPalette1;
            this.kryptonLabel2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel2.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel2.TabIndex = 22;
            this.kryptonLabel2.Values.Text = "Saved Work";
            // 
            // grd2
            // 
            this.grd2.AllowUserToAddRows = false;
            this.grd2.AllowUserToDeleteRows = false;
            this.grd2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.grd2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column3});
            this.grd2.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Custom1;
            this.grd2.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.GridBackgroundCustom1;
            this.grd2.GridStyles.StyleColumn = Krypton.Toolkit.GridStyle.Custom1;
            this.grd2.GridStyles.StyleDataCells = Krypton.Toolkit.GridStyle.Custom1;
            this.grd2.GridStyles.StyleRow = Krypton.Toolkit.GridStyle.Custom1;
            this.grd2.Location = new System.Drawing.Point(8, 122);
            this.grd2.Name = "grd2";
            this.grd2.Palette = this.kryptonPalette1;
            this.grd2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.grd2.Size = new System.Drawing.Size(435, 141);
            this.grd2.StateCommon.Background.Color1 = System.Drawing.Color.White;
            this.grd2.StateCommon.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.grd2.StateCommon.Background.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Dashed;
            this.grd2.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundCustom1;
            this.grd2.TabIndex = 23;
            // 
            // Column4
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column4.FalseValue = null;
            this.Column4.HeaderText = "";
            this.Column4.IndeterminateValue = null;
            this.Column4.Name = "Column4";
            this.Column4.TrueValue = null;
            this.Column4.Width = 30;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "WorkId";
            this.Column1.HeaderText = "WorkId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Visible = false;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "JobName";
            this.Column5.HeaderText = "Job";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ItemName";
            this.Column2.HeaderText = "Item Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "SDate";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Date";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // frmMultireport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 388);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMultireport";
            this.Palette = this.kryptonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbComponents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd2)).EndInit();
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
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox cmbComponents;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonDateTimePicker txtTDate;
        private Krypton.Toolkit.KryptonDateTimePicker txtFDate;
        private Krypton.Toolkit.KryptonLabel kryptonLabel7;
        public Krypton.Toolkit.KryptonDataGridView grd2;
        private Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}