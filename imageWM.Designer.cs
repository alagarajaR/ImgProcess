namespace ImgProcess
{
    partial class imageWM
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
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdMPoint = new System.Windows.Forms.Button();
            this.cmdPlineL = new System.Windows.Forms.Button();
            this.cmdPLine = new System.Windows.Forms.Button();
            this.cmdHLine = new System.Windows.Forms.Button();
            this.cmdVLine = new System.Windows.Forms.Button();
            this.pan3 = new Krypton.Toolkit.KryptonPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.txtSize = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.cmbunit1 = new Krypton.Toolkit.KryptonComboBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.txtMove = new Krypton.Toolkit.KryptonTextBox();
            this.pan2 = new Krypton.Toolkit.KryptonPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtAngle = new Krypton.Toolkit.KryptonTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pan1 = new Krypton.Toolkit.KryptonPanel();
            this.chkLength = new Krypton.Toolkit.KryptonCheckBox();
            this.chkArea = new Krypton.Toolkit.KryptonCheckBox();
            this.chkCirum = new Krypton.Toolkit.KryptonCheckBox();
            this.chkDia = new Krypton.Toolkit.KryptonCheckBox();
            this.chkRad = new Krypton.Toolkit.KryptonCheckBox();
            this.chkAngle = new Krypton.Toolkit.KryptonCheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtLength = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbw1 = new Krypton.Toolkit.KryptonComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.img1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pan3)).BeginInit();
            this.pan3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbunit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pan2)).BeginInit();
            this.pan2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pan1)).BeginInit();
            this.pan1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbw1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::ImgProcess.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.copyToolStripMenuItem.Text = "Clone";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = global::ImgProcess.Properties.Resources.options_icon;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdMPoint);
            this.groupBox1.Controls.Add(this.cmdPlineL);
            this.groupBox1.Controls.Add(this.cmdPLine);
            this.groupBox1.Controls.Add(this.cmdHLine);
            this.groupBox1.Controls.Add(this.cmdVLine);
            this.groupBox1.Controls.Add(this.pan3);
            this.groupBox1.Controls.Add(this.kryptonPanel1);
            this.groupBox1.Controls.Add(this.pan2);
            this.groupBox1.Controls.Add(this.pan1);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.cmbw1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(334, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 332);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            this.groupBox1.Visible = false;
            this.groupBox1.MouseHover += new System.EventHandler(this.groupBox1_MouseHover);
            // 
            // cmdMPoint
            // 
            this.cmdMPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMPoint.Location = new System.Drawing.Point(286, 254);
            this.cmdMPoint.Name = "cmdMPoint";
            this.cmdMPoint.Size = new System.Drawing.Size(146, 27);
            this.cmdMPoint.TabIndex = 25;
            this.cmdMPoint.Text = "Mid Point";
            this.cmdMPoint.UseVisualStyleBackColor = true;
            this.cmdMPoint.Click += new System.EventHandler(this.cmdMPoint_Click);
            // 
            // cmdPlineL
            // 
            this.cmdPlineL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPlineL.Location = new System.Drawing.Point(286, 221);
            this.cmdPlineL.Name = "cmdPlineL";
            this.cmdPlineL.Size = new System.Drawing.Size(146, 27);
            this.cmdPlineL.TabIndex = 24;
            this.cmdPlineL.Text = "Perpendicular -Left";
            this.cmdPlineL.UseVisualStyleBackColor = true;
            this.cmdPlineL.Click += new System.EventHandler(this.button12_Click_1);
            // 
            // cmdPLine
            // 
            this.cmdPLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPLine.Location = new System.Drawing.Point(286, 188);
            this.cmdPLine.Name = "cmdPLine";
            this.cmdPLine.Size = new System.Drawing.Size(146, 27);
            this.cmdPLine.TabIndex = 23;
            this.cmdPLine.Text = "Perpendicular -Right";
            this.cmdPLine.UseVisualStyleBackColor = true;
            this.cmdPLine.Click += new System.EventHandler(this.cmdPLine_Click);
            // 
            // cmdHLine
            // 
            this.cmdHLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHLine.Location = new System.Drawing.Point(366, 143);
            this.cmdHLine.Name = "cmdHLine";
            this.cmdHLine.Size = new System.Drawing.Size(66, 27);
            this.cmdHLine.TabIndex = 22;
            this.cmdHLine.Text = "H Line";
            this.cmdHLine.UseVisualStyleBackColor = true;
            this.cmdHLine.Click += new System.EventHandler(this.cmdHLine_Click);
            // 
            // cmdVLine
            // 
            this.cmdVLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdVLine.Location = new System.Drawing.Point(286, 143);
            this.cmdVLine.Name = "cmdVLine";
            this.cmdVLine.Size = new System.Drawing.Size(66, 27);
            this.cmdVLine.TabIndex = 21;
            this.cmdVLine.Text = "V Line";
            this.cmdVLine.UseVisualStyleBackColor = true;
            this.cmdVLine.Click += new System.EventHandler(this.cmdVLine_Click);
            // 
            // pan3
            // 
            this.pan3.Controls.Add(this.label7);
            this.pan3.Controls.Add(this.button10);
            this.pan3.Controls.Add(this.button11);
            this.pan3.Controls.Add(this.txtSize);
            this.pan3.Location = new System.Drawing.Point(286, 17);
            this.pan3.Name = "pan3";
            this.pan3.Size = new System.Drawing.Size(146, 117);
            this.pan3.StateCommon.Color1 = System.Drawing.Color.WhiteSmoke;
            this.pan3.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Size By";
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(87, 65);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(37, 35);
            this.button10.TabIndex = 15;
            this.button10.Text = "-";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(33, 65);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(37, 35);
            this.button11.TabIndex = 14;
            this.button11.Text = "+";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(56, 22);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(31, 23);
            this.txtSize.TabIndex = 13;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.cmbunit1);
            this.kryptonPanel1.Controls.Add(this.button9);
            this.kryptonPanel1.Controls.Add(this.button8);
            this.kryptonPanel1.Controls.Add(this.label6);
            this.kryptonPanel1.Controls.Add(this.button6);
            this.kryptonPanel1.Controls.Add(this.button7);
            this.kryptonPanel1.Controls.Add(this.txtMove);
            this.kryptonPanel1.Location = new System.Drawing.Point(134, 139);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(146, 177);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.WhiteSmoke;
            this.kryptonPanel1.TabIndex = 19;
            // 
            // cmbunit1
            // 
            this.cmbunit1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbunit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbunit1.DropDownWidth = 66;
            this.cmbunit1.FormattingEnabled = true;
            this.cmbunit1.IntegralHeight = false;
            this.cmbunit1.Items.AddRange(new object[] {
            "px",
            "mm",
            "cm",
            "inch",
            "micron"});
            this.cmbunit1.Location = new System.Drawing.Point(61, 31);
            this.cmbunit1.Name = "cmbunit1";
            this.cmbunit1.Palette = this.kryptonPalette1;
            this.cmbunit1.PaletteMode = Krypton.Toolkit.PaletteMode.Custom;
            this.cmbunit1.Size = new System.Drawing.Size(66, 21);
            this.cmbunit1.TabIndex = 44;
            this.cmbunit1.Text = "px";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(90, 96);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(37, 35);
            this.button9.TabIndex = 18;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(55, 131);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(37, 35);
            this.button8.TabIndex = 17;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Move By";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(55, 62);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(37, 35);
            this.button6.TabIndex = 15;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(20, 96);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(37, 35);
            this.button7.TabIndex = 14;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtMove
            // 
            this.txtMove.Location = new System.Drawing.Point(27, 29);
            this.txtMove.Name = "txtMove";
            this.txtMove.Size = new System.Drawing.Size(31, 23);
            this.txtMove.TabIndex = 13;
            // 
            // pan2
            // 
            this.pan2.Controls.Add(this.label5);
            this.pan2.Controls.Add(this.button5);
            this.pan2.Controls.Add(this.button4);
            this.pan2.Controls.Add(this.txtAngle);
            this.pan2.Controls.Add(this.label3);
            this.pan2.Location = new System.Drawing.Point(134, 16);
            this.pan2.Name = "pan2";
            this.pan2.Size = new System.Drawing.Size(146, 117);
            this.pan2.StateCommon.Color1 = System.Drawing.Color.WhiteSmoke;
            this.pan2.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Rotate By";
            // 
            // button5
            // 
            this.button5.Image = global::ImgProcess.Properties.Resources.rotate_clockwise;
            this.button5.Location = new System.Drawing.Point(82, 66);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(37, 35);
            this.button5.TabIndex = 15;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Image = global::ImgProcess.Properties.Resources.rotate_anticlockwise;
            this.button4.Location = new System.Drawing.Point(28, 66);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(37, 35);
            this.button4.TabIndex = 14;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(74, 33);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(31, 23);
            this.txtAngle.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(34, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Angle";
            // 
            // pan1
            // 
            this.pan1.Controls.Add(this.chkLength);
            this.pan1.Controls.Add(this.chkArea);
            this.pan1.Controls.Add(this.chkCirum);
            this.pan1.Controls.Add(this.chkDia);
            this.pan1.Controls.Add(this.chkRad);
            this.pan1.Controls.Add(this.chkAngle);
            this.pan1.Location = new System.Drawing.Point(14, 140);
            this.pan1.Name = "pan1";
            this.pan1.Size = new System.Drawing.Size(114, 176);
            this.pan1.StateCommon.Color1 = System.Drawing.Color.White;
            this.pan1.StateCommon.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pan1.StateCommon.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            this.pan1.TabIndex = 17;
            // 
            // chkLength
            // 
            this.chkLength.Location = new System.Drawing.Point(13, 3);
            this.chkLength.Name = "chkLength";
            this.chkLength.Size = new System.Drawing.Size(61, 20);
            this.chkLength.TabIndex = 6;
            this.chkLength.Values.Text = "Length";
            // 
            // chkArea
            // 
            this.chkArea.Location = new System.Drawing.Point(13, 29);
            this.chkArea.Name = "chkArea";
            this.chkArea.Size = new System.Drawing.Size(49, 20);
            this.chkArea.TabIndex = 7;
            this.chkArea.Values.Text = "Area";
            // 
            // chkCirum
            // 
            this.chkCirum.Location = new System.Drawing.Point(13, 55);
            this.chkCirum.Name = "chkCirum";
            this.chkCirum.Size = new System.Drawing.Size(62, 20);
            this.chkCirum.TabIndex = 8;
            this.chkCirum.Values.Text = "Circum";
            // 
            // chkDia
            // 
            this.chkDia.Location = new System.Drawing.Point(13, 81);
            this.chkDia.Name = "chkDia";
            this.chkDia.Size = new System.Drawing.Size(41, 20);
            this.chkDia.TabIndex = 9;
            this.chkDia.Values.Text = "Dia";
            // 
            // chkRad
            // 
            this.chkRad.Location = new System.Drawing.Point(13, 107);
            this.chkRad.Name = "chkRad";
            this.chkRad.Size = new System.Drawing.Size(60, 20);
            this.chkRad.TabIndex = 10;
            this.chkRad.Values.Text = "Radius";
            // 
            // chkAngle
            // 
            this.chkAngle.Location = new System.Drawing.Point(13, 133);
            this.chkAngle.Name = "chkAngle";
            this.chkAngle.Size = new System.Drawing.Size(55, 20);
            this.chkAngle.TabIndex = 11;
            this.chkAngle.Values.Text = "Angle";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabel1.Location = new System.Drawing.Point(307, 303);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(36, 15);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Move";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(86, 82);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(31, 23);
            this.txtLength.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Line Length";
            // 
            // button2
            // 
            this.button2.Image = global::ImgProcess.Properties.Resources.ok1;
            this.button2.Location = new System.Drawing.Point(357, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 33);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = global::ImgProcess.Properties.Resources.cancel;
            this.button3.Location = new System.Drawing.Point(398, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 33);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmbw1
            // 
            this.cmbw1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbw1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbw1.DropDownWidth = 59;
            this.cmbw1.IntegralHeight = false;
            this.cmbw1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbw1.Location = new System.Drawing.Point(58, 49);
            this.cmbw1.Name = "cmbw1";
            this.cmbw1.Size = new System.Drawing.Size(59, 21);
            this.cmbw1.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbw1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 24);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // img1
            // 
            this.img1.BackColor = System.Drawing.Color.Transparent;
            this.img1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.img1.Location = new System.Drawing.Point(5, 3);
            this.img1.Name = "img1";
            this.img1.Size = new System.Drawing.Size(500, 500);
            this.img1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.img1.TabIndex = 3;
            this.img1.TabStop = false;
            this.img1.Click += new System.EventHandler(this.img1_Click);
            this.img1.Paint += new System.Windows.Forms.PaintEventHandler(this.img1_Paint);
            this.img1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.img1_MouseDown);
            this.img1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.img1_MouseMove);
            this.img1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.img1_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.img1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 559);
            this.panel1.TabIndex = 6;
            // 
            // imageWM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(50, 0);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1167, 676);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "imageWM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImage1_FormClosing);
            this.Load += new System.EventHandler(this.image_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmImage_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageWM_MouseMove);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pan3)).EndInit();
            this.pan3.ResumeLayout(false);
            this.pan3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbunit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pan2)).EndInit();
            this.pan2.ResumeLayout(false);
            this.pan2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pan1)).EndInit();
            this.pan1.ResumeLayout(false);
            this.pan1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbw1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox img1;
        private Krypton.Toolkit.KryptonPalette kryptonPalette1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonComboBox cmbw1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private Krypton.Toolkit.KryptonCheckBox chkRad;
        private Krypton.Toolkit.KryptonCheckBox chkDia;
        private Krypton.Toolkit.KryptonCheckBox chkCirum;
        private Krypton.Toolkit.KryptonCheckBox chkArea;
        private Krypton.Toolkit.KryptonCheckBox chkLength;
        private Krypton.Toolkit.KryptonCheckBox chkAngle;
        private Krypton.Toolkit.KryptonTextBox txtLength;
        private System.Windows.Forms.Label label4;
        private Krypton.Toolkit.KryptonTextBox txtAngle;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Krypton.Toolkit.KryptonPanel pan1;
        private Krypton.Toolkit.KryptonPanel pan2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private Krypton.Toolkit.KryptonTextBox txtMove;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private Krypton.Toolkit.KryptonPanel pan3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private Krypton.Toolkit.KryptonTextBox txtSize;
        private Krypton.Toolkit.KryptonComboBox cmbunit1;
        private System.Windows.Forms.Button cmdVLine;
        private System.Windows.Forms.Button cmdHLine;
        private System.Windows.Forms.Button cmdPLine;
        private System.Windows.Forms.Button cmdPlineL;
        private System.Windows.Forms.Button cmdMPoint;
    }
}