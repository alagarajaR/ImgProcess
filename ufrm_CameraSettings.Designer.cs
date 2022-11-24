
namespace ImgProcess
{
    partial class ufrm_CameraSettings
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
            this.kryptonPalette = new Krypton.Toolkit.KryptonPalette(this.components);
            this.cmbPreview = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.cmbModel = new Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbResolution = new Krypton.Toolkit.KryptonComboBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.txtWidth = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.txtHeight = new Krypton.Toolkit.KryptonTextBox();
            this.btn_Close = new Krypton.Toolkit.KryptonButton();
            this.btn_Save = new Krypton.Toolkit.KryptonButton();
            this.errLbl1 = new ErrLbl.ErrorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPreview
            // 
            this.cmbPreview.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbPreview.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreview.DropDownWidth = 123;
            this.cmbPreview.IntegralHeight = false;
            this.cmbPreview.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cmbPreview.Location = new System.Drawing.Point(205, 200);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Size = new System.Drawing.Size(111, 25);
            this.cmbPreview.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbPreview.TabIndex = 56;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(19, 200);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(116, 24);
            this.kryptonLabel3.TabIndex = 55;
            this.kryptonLabel3.Values.Text = "Preview Mode : ";
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
            "IDSPeak uEye- Old Version",
            "IDSPeak uEye- New Version"});
            this.cmbModel.Location = new System.Drawing.Point(205, 268);
            this.cmbModel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(283, 25);
            this.cmbModel.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbModel.TabIndex = 54;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(19, 269);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(119, 24);
            this.kryptonLabel2.TabIndex = 53;
            this.kryptonLabel2.Values.Text = "Camera Model : ";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(19, 129);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(65, 24);
            this.kryptonLabel1.TabIndex = 52;
            this.kryptonLabel1.Values.Text = "Height : ";
            // 
            // cmbResolution
            // 
            this.cmbResolution.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.DropDownWidth = 123;
            this.cmbResolution.IntegralHeight = false;
            this.cmbResolution.Items.AddRange(new object[] {
            "640 x 480",
            "1280 x 720",
            "1920 x 1080",
            "2560 x 1440",
            "2048 x 1080",
            "3840 x 2160",
            "7680 x 4320"});
            this.cmbResolution.Location = new System.Drawing.Point(205, 19);
            this.cmbResolution.Margin = new System.Windows.Forms.Padding(4);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(283, 25);
            this.cmbResolution.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbResolution.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 24);
            this.label3.TabIndex = 47;
            this.label3.Values.Text = "Resolution :";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(205, 70);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(108, 27);
            this.txtWidth.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 48;
            this.label1.Values.Text = "Width : ";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(205, 129);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(108, 27);
            this.txtHeight.TabIndex = 50;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(387, 349);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(103, 33);
            this.btn_Close.TabIndex = 58;
            this.btn_Close.Values.Text = "Close";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(266, 349);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(103, 33);
            this.btn_Save.TabIndex = 57;
            this.btn_Save.Values.Text = "Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // errLbl1
            // 
            this.errLbl1.BackColor = System.Drawing.Color.Transparent;
            this.errLbl1.ForeColor = System.Drawing.Color.Green;
            this.errLbl1.Location = new System.Drawing.Point(77, 310);
            this.errLbl1.Margin = new System.Windows.Forms.Padding(5);
            this.errLbl1.Name = "errLbl1";
            this.errLbl1.SetVal = "";
            this.errLbl1.Size = new System.Drawing.Size(332, 26);
            this.errLbl1.TabIndex = 59;
            // 
            // ufrm_CameraSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 395);
            this.Controls.Add(this.errLbl1);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.cmbPreview);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.cmbResolution);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHeight);
            this.Name = "ufrm_CameraSettings";
            this.Palette = this.kryptonPalette;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.Text = "Camera Settings";
            this.Load += new System.EventHandler(this.ufrm_CameraSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonPalette kryptonPalette;
        private Krypton.Toolkit.KryptonComboBox cmbPreview;
        public Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonComboBox cmbModel;
        public Krypton.Toolkit.KryptonLabel kryptonLabel2;
        public Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox cmbResolution;
        public Krypton.Toolkit.KryptonLabel label3;
        public Krypton.Toolkit.KryptonTextBox txtWidth;
        public Krypton.Toolkit.KryptonLabel label1;
        public Krypton.Toolkit.KryptonTextBox txtHeight;
        private Krypton.Toolkit.KryptonButton btn_Close;
        private Krypton.Toolkit.KryptonButton btn_Save;
        private ErrLbl.ErrorLabel errLbl1;
    }
}