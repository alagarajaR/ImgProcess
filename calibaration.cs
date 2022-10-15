using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;
namespace ImgProcess
{
    public partial class calibaration : KryptonForm
    {
        public calibaration()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            kryptonPalette1.BasePaletteMode = com.kp;
            kryptonNavigator1.PaletteMode = com.cp;
            dataGridView1.PaletteMode = com.kp;


        }

        private void f1()
        {
            if (optmm.Checked == true)
            {
                lblx1.Text = "mm/pixel";
                lbly1.Text = "mm/pixel";
            }
            else if (optcm.Checked == true)
            {
                lblx1.Text = "cm/pixel";
                lbly1.Text = "cm/pixel";
            }
            else if (optinch.Checked == true)
            {
                lblx1.Text = "inch/pixel";
                lbly1.Text = "inch/pixel";
            }
            else if (optmicrons.Checked == true)
            {
                lblx1.Text = "microns/pixel";
                lbly1.Text = "microns/pixel";
            }

           

        }
        private void optmm_CheckedChanged(object sender, EventArgs e)
        {
            f1();
            
        }

        private void optcm_CheckedChanged(object sender, EventArgs e)
        {
            f1();
        }

        private void optinch_CheckedChanged(object sender, EventArgs e)
        {
            f1();
        }

        private void optmicrons_CheckedChanged(object sender, EventArgs e)
        {
            f1();
        }

        private void calibaration_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            this.errLbl1.SetVal = "";
            txtcid.Text = "";

            clear();


            this.kryptonNavigator1.SelectedIndex = 1;
            f1();


           

            Display();

        }

        private void Display()
        {
            System.Data.DataTable dt1 = new DataTable();
            dt1 = com.sqlcn.Gettable("SELECT cid,calname,name FROM calibration");

            this.dataGridView1.DataSource = dt1;
            this.dataGridView1.Refresh();

        }
        private void calibaration_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.isCalibrationOpen = false;
            MDIParent1 mp = (MDIParent1)this.Parent;
            mp.LoadMag();

            this.Dispose();



        }


        private void yCalc()
        {
            if (optmm.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txty.Text = (com.Val(txtxLength.Text) * 1000 / com.Val(txtxpic.Text)).ToString();
                else
                    txty.Text = "";
            }
            else if (optcm.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txty.Text = (com.Val(txtxLength.Text) * 10000 / com.Val(txtxpic.Text)).ToString();
                else
                    txty.Text = "";
            }
            else if (optinch.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txty.Text = (com.Val(txtxLength.Text) * 24500 / com.Val(txtxpic.Text)).ToString();
                else
                    txty.Text = "";
            }
            else if (optmicrons.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txty.Text = (com.Val(txtxLength.Text) / com.Val(txtxpic.Text)).ToString();
                else
                    txty.Text = "";
            }


        }


        private void xCalc()
        {
            if (optmm.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txtx.Text = (com.Val(txtxLength.Text) * 1000 / com.Val(txtxpic.Text)).ToString();
                else
                    txtx.Text = "";
            }
            else if (optcm.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txtx.Text = (com.Val(txtxLength.Text) * 10000 / com.Val(txtxpic.Text)).ToString();
                else
                    txtx.Text = "";
            }
            else if (optinch.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txtx.Text = (com.Val(txtxLength.Text) * 24500 / com.Val(txtxpic.Text)).ToString();
                else
                    txtx.Text = "";
            }
            else if (optmicrons.Checked == true)
            {
                if (com.Val(txtxpic.Text) > 0)
                    txtx.Text = (com.Val(txtxLength.Text)  / com.Val(txtxpic.Text)).ToString();
                else
                    txtx.Text = "";
            }


        }
        private void txtxpic_TextChanged(object sender, EventArgs e)
        {
            //xCalc();

        }

        private void txtxLength_TextChanged(object sender, EventArgs e)
        {
           // xCalc();
        }

       

     

        private void button1_Click(object sender, EventArgs e)
        {
            MDIParent1 mdi= (MDIParent1)this.Parent;

            imageWM f = mdi.GetActiveImageWindow();
            txtxpic.Text = f.PubDistance;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MDIParent1 mdi = (MDIParent1)this.Parent;

            imageWM f = mdi.GetActiveImageWindow();
            txtxpic.Text = f.PubDistance;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            com.isCalibrationOpen = false;
            MDIParent1 mp = (MDIParent1)this.Parent;
            mp.LoadMag();

            this.Dispose();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            xCalc();
            yCalc(); 

        }

        private void clear()
        {
            txtx.Text = "";
            txty.Text = "";
            txtxpic.Text = "";
            txtxpic.Text = "";
            txtName.Text = "";
            txtmag.Text = "100";
            txtxLength.Text = "";
            txtxLength.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {

            clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.errLbl1.SetVal = "";

            if (com.Val(txtmag.Text.Replace('x',' ')) == 0)
            {
                this.errLbl1.SetVal = "Please Select Magnificaiton";
                return;
            }
            if (txtName.Text.Length == 0)
            {
                this.errLbl1.SetVal = "Please Enter Calibration Name";
                return;
            }

            if (com.Val(txtxLength.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Enter X-Axis Length";
                return;
            }

            if (com.Val(txtxpic.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Enter X-Axis Pixel";
                return;
            }


            if (com.Val(txtxLength.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Enter Y-Axis Length";
                return;
            }

            if (com.Val(txtxpic.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Enter Y-Axis Pixel";
                return;
            }


            if (com.Val(txtx.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Calibrate X-Axis";
                return;
            }

            if (com.Val(txty.Text) == 0)
            {
                this.errLbl1.SetVal = "Please Calibrate Y-Axis";
                return;
            }

            string unit = "";
            

            if (optmm.Checked == true)
            {
                unit = "mm";
            }
            else if (optcm.Checked == true)
            {
                unit = "cm";
            }
            else if (optinch.Checked == true)
            {
                unit = "inch";
            }
            else if (optmicrons.Checked == true)
            {
                unit = "micron";
            }


            string calname = "M:"+txtmag.Text + "x U:"+ unit+" xL:"+txtxLength.Text+ " xP:" + txtxpic.Text + " yL:"  + txtxLength.Text + " yP:"+ txtxpic.Text;


            if (com.Val(txtcid.Text) == 0)
            {

           
                string qry = "INSERT INTO calibration " +
                        "( calname, name, xLength, xPixel, yLength, yPixel, unit, xValue, yValue, magnification) " +
                        " VALUES(" +
                        " '" + calname + "' " +
                        " ,'" + txtName.Text + "' " +
                        " ,'" + com.Val(txtxLength.Text).ToString() + "' " +
                        " ,'" + com.Val(txtxpic.Text).ToString() + "' " +
                        " ,'" + com.Val(txtxLength.Text).ToString() + "' " +
                        " ,'" + com.Val(txtxpic.Text).ToString() + "' " +
                        " ,'" + unit + "' " +
                        " ,'" + com.Val(txtx.Text).ToString() + "' " +
                        " ,'" + com.Val(txty.Text).ToString() + "' " +
                        " ,'" + com.Val(txtmag.Text.Replace('x', ' ')).ToString() + "') ";



                try
                {
                   com.sqlcn.Exec(qry);
                    this.errLbl1.SetVal = "Successfully Added";
                    clear();
                    Display();

                }
                catch (Exception ex)
                {

                }




            }
            else
            { 
                string qry= "UPDATE calibration " +
                        " SET " +
                        " calname = '" + calname + "'" +
                        " ,name = '"+txtName.Text+"' " +
                        " ,xLength = '"+ com.Val(txtxLength.Text).ToString() +"' " +
                        " ,xPixel = '"+ com.Val(txtxpic.Text).ToString() +"' " +
                        " ,yLength =  '" + com.Val(txtxLength.Text).ToString() + "' " +
                        " ,yPixel =  '" + com.Val(txtxpic.Text).ToString() + "' " +
                        " ,unit =  '" + unit + "' " +
                        " ,xValue =  '" + com.Val(txtx.Text).ToString() + "' " +
                        " ,yValue = '" + com.Val(txty.Text).ToString() + "' " +
                        " ,magnification =  '" + com.Val(txtmag.Text.Replace('x', ' ')).ToString() + "' " +
                        "  WHERE cid = '" + txtcid.Text + "' ";


                try
                {
                    com.sqlcn.Exec(qry);
                    this.errLbl1.SetVal = "Successfully Updated";
                    clear();
                    Display();
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string cid = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

                if (MessageBox.Show("Do you want to Edit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    txtcid.Text = "";
                    clear();

                    

                    MySql.Data.MySqlClient.MySqlDataReader rst;
                    rst = com.sqlcn.Getdata("select * from calibration where cid="+cid+"");
                    if (rst.Read())
                    {
                        txtcid.Text =rst["cid"].ToString();
                        txtx.Text = rst["xValue"].ToString();
                        txty.Text = rst["yValue"].ToString();
                        txtxpic.Text = rst["xPixel"].ToString();
                        txtxpic.Text = rst["yPixel"].ToString();
                        txtName.Text = rst["name"].ToString();
                        txtmag.Text = rst["magnification"].ToString();
                        txtxLength.Text = rst["xLength"].ToString();
                        txtxLength.Text = rst["yLength"].ToString();
                        string unit = rst["unit"].ToString();

                        if (unit=="mm")
                        {
                            optmm.Checked = true;
                        }
                        else if (unit=="cm")
                        {
                            optcm.Checked = true;
                        }
                        else if (unit=="inch")
                        {
                            optinch.Checked = true;
                        }
                        else if (unit=="micron")
                        {
                            optmicrons.Checked = true;
                        }


                    }

                    rst.Close();


                    this.kryptonNavigator1.SelectedIndex = 1;
                    

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string cid = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

                if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    com.sqlcn.Exec("delete  from calibration where cid=" + cid + "");
                    Display();
                    clear();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.kryptonNavigator1.SelectedIndex = 1;
            txtcid.Text = "";
            clear();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            com.isCalibrationOpen = false;
            MDIParent1 mp = (MDIParent1)this.Parent;
            mp.LoadMag();

            this.Dispose();
        }

        private void kryptonPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
