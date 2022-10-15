using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Krypton.Toolkit;
namespace ImgProcess
{
    public partial class frmLogin : KryptonForm 
    {

      
        public frmLogin()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            com.KPNo = (int)Properties.Settings.Default["KPNo"];
            com.CPNo = (int)Properties.Settings.Default["CPNo"];

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;



            //ThemeValueAssign("BLACK");
            //SetTheme();
            //int x=pmode;
            //PaletteMode p = (PaletteMode)x;


            //ButtonPalette1.ResetToDefaults(true);
            //ButtonPalette1.BasePaletteMode = p;


            Krypton.Toolkit.PaletteMode p = com.kp;
            ComponentFactory.Krypton.Toolkit.PaletteMode p1 = com.cp;
            
            ButtonPalette1.ResetToDefaults(true);
            ButtonPalette1.BasePaletteMode = p;

           

        }

        public void ThemeValueAssign(string ThemeName)
        {

            if (ThemeName == "BLUE")
            {
                Theme.FormBackColor1 = Color.FromArgb(0, 0, 64);
                Theme.FormBackColor2 = Color.Navy;
                Theme.FormHeadColor1 = Color.FromArgb(0, 0, 64);
                Theme.FormHeadColor2 = Color.Navy;
                Theme.FormBorderColor1 = Color.FromArgb(0, 0, 64);
                Theme.FormBorderColor2 = Color.Navy;
                Theme.ButtonColor1 = Color.FromArgb(0, 0, 64); 
                Theme.ButtonColor2 = Color.Navy;
                Theme.TextColor1 = Color.White;
                Theme.TextColor2 = Color.Navy;
            }
            else if (ThemeName == "MAROON")
            {
                Theme.FormBackColor1 = Color.Maroon;
                Theme.FormBackColor2 = Color.FromArgb(192, 0, 0);
                Theme.FormHeadColor1 = Color.Maroon;
                Theme.FormHeadColor2 = Color.FromArgb(192, 0, 0);
                Theme.FormBorderColor1 = Color.Maroon;
                Theme.FormBorderColor2 = Color.FromArgb(192, 0, 0);
                Theme.ButtonColor1 = Color.Maroon;
                Theme.ButtonColor2 = Color.FromArgb(192, 0, 0);
                Theme.TextColor1 = Color.White;
                Theme.TextColor2 = Color.FromArgb(192, 0, 0);
            }
            else if (ThemeName == "BLACK")
            {
                Theme.FormBackColor1 = Color.Black;
                Theme.FormBackColor2 = Color.FromArgb(64, 64, 64);
                Theme.FormHeadColor1 = Color.Black;
                Theme.FormHeadColor2 = Color.FromArgb(64, 64, 64);
                Theme.FormBorderColor1 = Color.Black;
                Theme.FormBorderColor2 = Color.FromArgb(64, 64, 64);
                Theme.ButtonColor1 = Color.Black;
                Theme.ButtonColor2 = Color.FromArgb(64, 64, 64);
                Theme.TextColor1 = Color.White;
                Theme.TextColor2 = Color.FromArgb(64, 64, 64);
            }

            else if (ThemeName == "GREEN")
            {
                Theme.FormBackColor1 = Color.Green;
                Theme.FormBackColor2 = Color.FromArgb(0, 192, 0);
                Theme.FormHeadColor1 = Color.Green;
                Theme.FormHeadColor2 = Color.FromArgb(0, 192, 0);
                Theme.FormBorderColor1 = Color.Green;
                Theme.FormBorderColor2 = Color.FromArgb(0, 192, 0);
                Theme.ButtonColor1 = Color.Green;
                Theme.ButtonColor2 = Color.FromArgb(0, 192, 0);
                Theme.TextColor1 = Color.White;
                Theme.TextColor2 = Color.FromArgb(0, 192, 0);
            }
            else if (ThemeName == "TEAL")
            {
                Theme.FormBackColor2 = Color.Teal;
                Theme.FormBackColor1 = Color.FromArgb(0, 192, 192);
                Theme.FormHeadColor2 = Color.Teal;
                Theme.FormHeadColor1 = Color.FromArgb(0, 192, 192);
                Theme.FormBorderColor2 = Color.Teal;
                Theme.FormBorderColor1 = Color.FromArgb(0, 192, 192);
                Theme.ButtonColor2 = Color.Teal;
                Theme.ButtonColor1 = Color.FromArgb(0, 192, 192);
                Theme.TextColor1 = Color.Blue;
                Theme.TextColor2 = Color.Blue;
            }
        }

        public void SetTheme()
        {
            //form
            this.StateCommon.Back.Color1 = Theme.FormBackColor1;
            this.StateCommon.Back.Color1 = Theme.FormBackColor2;
            this.StateCommon.Header.Back.Color1 = Theme.FormHeadColor1;
            this.StateCommon.Header.Back.Color2 = Theme.FormHeadColor2;
            this.StateCommon.Border.Color1 = Theme.FormBorderColor1;
            this.StateCommon.Border.Color1 = Theme.FormBorderColor2;
            this.StateCommon.Header.Content.ShortText.Color1 = Theme.TextColor1;
            this.StateCommon.Header.Content.LongText.Color1 = Theme.TextColor1;

            //button
            ButtonPalette1.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = Theme.ButtonColor1;
            ButtonPalette1.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = Theme.ButtonColor2;
            ButtonPalette1.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color1 = Theme.TextColor1;
            ButtonPalette1.ButtonStyles.ButtonStandalone.StateCommon.Content.LongText.Color1 = Theme.TextColor1;



          

            //label
            ButtonPalette1.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = Theme.TextColor2;
            ButtonPalette1.LabelStyles.LabelCommon.StateCommon.LongText.Color1 = Theme.TextColor2;

            //CheckBox
            ButtonPalette1.Common.StateCommon.Content.ShortText.Color1 = Theme.TextColor2;
            ButtonPalette1.Common.StateCommon.Content.LongText.Color1 = Theme.TextColor2;

            //TextBox
            ButtonPalette1.Common.StateCommon.Border.Color1 = Theme.FormHeadColor2;


           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.errorProvider1.Clear();


            com.IMLic = false;
            com.WMLic = false;
            if (cmbProduct.Text == "Image Measurements")
                com.IMLic = true;
            else if (cmbProduct.Text == "WeldMet")
                com.WMLic = true;
            else
            {
                MessageBox.Show("Please select Product Type", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


          

            MySqlDataReader rst;

            rst = com.sqlcn.Getdata("select * from usertable where username='" + txtusername.Text + "' ");

            if (rst.Read())
            {
                rst.Close();
                rst = com.sqlcn.Getdata("select * from usertable where username='" + txtusername.Text + "' and password='" + txtpassword.Text + "'");
                if (rst.Read())
                {
                    this.Visible = false;

                   Properties.Settings.Default["LastUser"]=txtusername.Text;
                    Properties.Settings.Default["ProductName"] = cmbProduct.Text;

                    if (checkBox1.Checked == true)
                        Properties.Settings.Default["SaveMe"] = true;
                    else
                        Properties.Settings.Default["SaveMe"] = false;



                    Properties.Settings.Default.Save();
                    rst.Close();

                    MySettings.ReadSettings();

                    MDIParent1 m = new MDIParent1();
                    m.ShowDialog();
                }
                else
                {
                    rst.Close();
                    this.errorProvider1.SetError(txtpassword, "Incorrect Password");
                }

            }
            else
            {
                rst.Close();
                this.errorProvider1.SetError(txtusername, "Incorrect User Name");
            }


            
        }

        int pmode = 10;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            ThemeList.ThemeListAdd();
            com.RCheck();

            cmbProduct.Items.Clear();
            cmbProduct.Items.Add("Image Measurements");
            cmbProduct.Items.Add("WeldMet");
            cmbProduct.Items.Add("");

            cmbProduct.Text = "";


            com.sqlcn=new mysqlclass();
           
            com.sqlcn.OpenCon();

            bool saveme=(bool)Properties.Settings.Default["SaveMe"];
            this.checkBox1.Checked = saveme;


            string Productname = Properties.Settings.Default["ProductName"].ToString();
            cmbProduct.Text = Productname;

            string lastuser = (string)Properties.Settings.Default["LastUser"];
            txtusername.Text = lastuser;

            if (checkBox1.Checked == true)
            {
                MySqlDataReader rst;
                rst = com.sqlcn.Getdata("select Password from usertable where username='" + txtusername.Text + "' ");

                if (rst.Read())
                {
                    txtpassword.Text = rst.GetString("Password");
                }
                rst.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reg f = new reg(0,"");
            f.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pmode < 39)
                pmode = pmode + 1;
            else
                pmode = 10;

            int x = pmode;
            PaletteMode p = (PaletteMode)x;


            kryptonLabel1.Text = p.ToString();

            ButtonPalette1.ResetToDefaults(true);
            ButtonPalette1.BasePaletteMode = p;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pmode > 11)
                pmode = pmode - 1;
            else
                pmode = 39;

            int x = pmode;
            PaletteMode p = (PaletteMode)x;

            kryptonLabel1.Text = p.ToString();

            ButtonPalette1.ResetToDefaults(true);
            ButtonPalette1.BasePaletteMode = p;
        }
    }
}
