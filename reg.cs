using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgProcess
{
    public partial class reg : Form
    {
        public reg(int days, string error)
        {
            InitializeComponent();

            if (days > 0)
            {
                label3.Text = "Remaining Days is " + days.ToString();
            }
            else
            {
                label3.Text = error;
            }
        }
            clsReg regclass =new clsReg();
        private void reg_Load(object sender, EventArgs e)
        {
           this.txtpkey.Text= regclass.GenKey();

            try
            {
                string akey = "";

                string akeypath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                akey = System.IO.File.ReadAllText(akeypath + "\\reg.ky");
                txtakey.Text = akey; 
            }
            catch (Exception ex)
            { 
            }



            lnkSkip.Visible = false;
            string result = regclass.RegCheck(txtakey.Text);

            string result1 = result.Substring(0, 2);

            if (result1 == "OK")
            {
                string[] x = result.Split('-');
                int days = int.Parse(x[1]);
                label3.Text = "Remaining Days is " + days.ToString();
                lnkSkip.Visible = true;
            }
            else if (result1 == "RD")
            {
                string[] x = result.Split('-');
                int days = int.Parse(x[1]);
                label3.Text = "Remaining Days is " + days.ToString();
                lnkSkip.Visible = true;
            }
            else
            {
                label3.Text = result;
                lnkSkip.Visible =false;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Product Key|*.ky";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                string fname=saveFileDialog1.FileName ;
                System.IO.File.WriteAllText(fname,txtpkey.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Key File |*.aky";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtakey.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clsReg r = new clsReg();
            if (r.MatchKey(txtakey.Text) == "1")
            {
                string akeypath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                System.IO.File.WriteAllText(akeypath + "\\reg.ky",txtakey.Text);
                this.Dispose();

                com.RCheck();

            }
            else
            {
                MessageBox.Show("Invalid Key.Contact Software Vendor",com.MsgTitle,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void lnkSkip_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
    }
}
