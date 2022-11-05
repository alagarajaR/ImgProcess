
namespace Envision_CamLib
{
    partial class frm_CameraInt
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
            this.kryptonPalette = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.BUTTON_SAVE = new System.Windows.Forms.Button();
            this.BUTTON_PROPERTY = new System.Windows.Forms.Button();
            this.BUTTON_START = new System.Windows.Forms.Button();
            this.BUTTON_OPEN = new System.Windows.Forms.Button();
            this.COMBO_DEVICES = new System.Windows.Forms.ComboBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BUTTON_SAVE
            // 
            this.BUTTON_SAVE.Location = new System.Drawing.Point(228, 13);
            this.BUTTON_SAVE.Margin = new System.Windows.Forms.Padding(4);
            this.BUTTON_SAVE.Name = "BUTTON_SAVE";
            this.BUTTON_SAVE.Size = new System.Drawing.Size(96, 31);
            this.BUTTON_SAVE.TabIndex = 46;
            this.BUTTON_SAVE.Text = "Save Picture";
            this.BUTTON_SAVE.UseVisualStyleBackColor = true;
            this.BUTTON_SAVE.Click += new System.EventHandler(this.BUTTON_SAVE_Click);
            // 
            // BUTTON_PROPERTY
            // 
            this.BUTTON_PROPERTY.Location = new System.Drawing.Point(97, 13);
            this.BUTTON_PROPERTY.Margin = new System.Windows.Forms.Padding(4);
            this.BUTTON_PROPERTY.Name = "BUTTON_PROPERTY";
            this.BUTTON_PROPERTY.Size = new System.Drawing.Size(123, 31);
            this.BUTTON_PROPERTY.TabIndex = 45;
            this.BUTTON_PROPERTY.Text = "Camera Settings";
            this.BUTTON_PROPERTY.UseVisualStyleBackColor = true;
            this.BUTTON_PROPERTY.Click += new System.EventHandler(this.BUTTON_PROPERTY_Click);
            // 
            // BUTTON_START
            // 
            this.BUTTON_START.Location = new System.Drawing.Point(13, 13);
            this.BUTTON_START.Margin = new System.Windows.Forms.Padding(4);
            this.BUTTON_START.Name = "BUTTON_START";
            this.BUTTON_START.Size = new System.Drawing.Size(76, 31);
            this.BUTTON_START.TabIndex = 44;
            this.BUTTON_START.Text = "Play";
            this.BUTTON_START.UseVisualStyleBackColor = true;
            this.BUTTON_START.Click += new System.EventHandler(this.BUTTON_START_Click);
            // 
            // BUTTON_OPEN
            // 
            this.BUTTON_OPEN.Location = new System.Drawing.Point(348, 11);
            this.BUTTON_OPEN.Margin = new System.Windows.Forms.Padding(4);
            this.BUTTON_OPEN.Name = "BUTTON_OPEN";
            this.BUTTON_OPEN.Size = new System.Drawing.Size(76, 31);
            this.BUTTON_OPEN.TabIndex = 43;
            this.BUTTON_OPEN.Text = "Open";
            this.BUTTON_OPEN.UseVisualStyleBackColor = true;
            this.BUTTON_OPEN.Visible = false;
            // 
            // COMBO_DEVICES
            // 
            this.COMBO_DEVICES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_DEVICES.FormattingEnabled = true;
            this.COMBO_DEVICES.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.COMBO_DEVICES.Location = new System.Drawing.Point(444, 15);
            this.COMBO_DEVICES.Margin = new System.Windows.Forms.Padding(4);
            this.COMBO_DEVICES.Name = "COMBO_DEVICES";
            this.COMBO_DEVICES.Size = new System.Drawing.Size(171, 24);
            this.COMBO_DEVICES.TabIndex = 42;
            this.COMBO_DEVICES.Visible = false;
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox.Location = new System.Drawing.Point(4, 62);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1195, 657);
            this.pictureBox.TabIndex = 47;
            this.pictureBox.TabStop = false;
            // 
            // frm_CameraInt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 722);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.BUTTON_SAVE);
            this.Controls.Add(this.BUTTON_PROPERTY);
            this.Controls.Add(this.BUTTON_START);
            this.Controls.Add(this.BUTTON_OPEN);
            this.Controls.Add(this.COMBO_DEVICES);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_CameraInt";
            this.Palette = this.kryptonPalette;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera Integration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_CameraInt_FormClosing);
            this.Load += new System.EventHandler(this.frm_CameraInt_Load);
            this.Resize += new System.EventHandler(this.frm_CameraInt_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette;
        private System.Windows.Forms.Button BUTTON_SAVE;
        private System.Windows.Forms.Button BUTTON_PROPERTY;
        private System.Windows.Forms.Button BUTTON_START;
        private System.Windows.Forms.Button BUTTON_OPEN;
        private System.Windows.Forms.ComboBox COMBO_DEVICES;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}