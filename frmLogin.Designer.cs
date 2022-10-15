namespace ImgProcess
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.ButtonPalette1 = new Krypton.Toolkit.KryptonPalette(this.components);
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.txtusername = new Krypton.Toolkit.KryptonTextBox();
            this.txtpassword = new Krypton.Toolkit.KryptonTextBox();
            this.button1 = new Krypton.Toolkit.KryptonButton();
            this.button2 = new Krypton.Toolkit.KryptonButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkBox1 = new Krypton.Toolkit.KryptonCheckBox();
            this.linkLabel1 = new Krypton.Toolkit.KryptonLinkLabel();
            this.kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.cmbProduct = new Krypton.Toolkit.KryptonComboBox();
            this.txt1 = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.label1.Location = new System.Drawing.Point(26, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Palette = this.ButtonPalette1;
            this.label1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "User Name";
            // 
            // ButtonPalette1
            // 
            this.ButtonPalette1.BasePaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.ButtonPalette1.Common.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Red;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.label2.Location = new System.Drawing.Point(26, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Palette = this.ButtonPalette1;
            this.label2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 1;
            this.label2.Values.Text = "Password";
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(121, 43);
            this.txtusername.Margin = new System.Windows.Forms.Padding(5);
            this.txtusername.Name = "txtusername";
            this.txtusername.Palette = this.ButtonPalette1;
            this.txtusername.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtusername.Size = new System.Drawing.Size(208, 23);
            this.txtusername.TabIndex = 2;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(121, 79);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Palette = this.ButtonPalette1;
            this.txtpassword.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txtpassword.PasswordChar = '#';
            this.txtpassword.Size = new System.Drawing.Size(208, 23);
            this.txtpassword.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(123, 145);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Palette = this.ButtonPalette1;
            this.button1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.button1.Size = new System.Drawing.Size(98, 37);
            this.button1.TabIndex = 4;
            this.button1.Values.Text = "Login";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gray;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(231, 145);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Palette = this.ButtonPalette1;
            this.button2.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.button2.Size = new System.Drawing.Size(98, 37);
            this.button2.TabIndex = 5;
            this.button2.Values.Text = "Close";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // checkBox1
            // 
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.checkBox1.Location = new System.Drawing.Point(260, 112);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Palette = this.ButtonPalette1;
            this.checkBox1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.checkBox1.Size = new System.Drawing.Size(72, 20);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Values.Text = "Save me";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(38, 501);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 20);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.Values.Text = "Registration";
            this.linkLabel1.Visible = false;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlCustom1;
            this.kryptonGroupBox1.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.kryptonGroupBox1.Location = new System.Drawing.Point(324, 110);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.txt1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cmbProduct);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.checkBox1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.button2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txtusername);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txtpassword);
            this.kryptonGroupBox1.Panel.Controls.Add(this.label2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.button1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.label1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(353, 201);
            this.kryptonGroupBox1.StateCommon.Back.Color1 = System.Drawing.Color.Silver;
            this.kryptonGroupBox1.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.kryptonGroupBox1.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.kryptonGroupBox1.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonGroupBox1.TabIndex = 8;
            this.kryptonGroupBox1.Values.Heading = "";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.ForeColor = System.Drawing.Color.Black;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(21, 96);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Palette = this.ButtonPalette1;
            this.kryptonLabel1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.kryptonLabel1.Size = new System.Drawing.Size(6, 2);
            this.kryptonLabel1.TabIndex = 7;
            this.kryptonLabel1.Values.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(602, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 33);
            this.button3.TabIndex = 9;
            this.button3.Text = "nxt";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(544, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 33);
            this.button4.TabIndex = 10;
            this.button4.Text = "Prv";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // cmbProduct
            // 
            this.cmbProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.DropDownWidth = 208;
            this.cmbProduct.IntegralHeight = false;
            this.cmbProduct.Location = new System.Drawing.Point(121, 14);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(208, 21);
            this.cmbProduct.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbProduct.TabIndex = 11;
            // 
            // txt1
            // 
            this.txt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.ForeColor = System.Drawing.Color.Black;
            this.txt1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.txt1.Location = new System.Drawing.Point(26, 15);
            this.txt1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.txt1.Name = "txt1";
            this.txt1.Palette = this.ButtonPalette1;
            this.txt1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.txt1.Size = new System.Drawing.Size(56, 20);
            this.txt1.TabIndex = 13;
            this.txt1.Values.Text = "Product";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(673, 304);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.linkLabel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Sitka Small", 11.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Palette = this.ButtonPalette1;
            this.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonLabel label1;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonTextBox txtusername;
        private Krypton.Toolkit.KryptonTextBox txtpassword;
        private Krypton.Toolkit.KryptonButton button1;
        private Krypton.Toolkit.KryptonButton button2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Krypton.Toolkit.KryptonLinkLabel linkLabel1;
        private Krypton.Toolkit.KryptonCheckBox checkBox1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonPalette ButtonPalette1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonLabel txt1;
        private Krypton.Toolkit.KryptonComboBox cmbProduct;
    }
}