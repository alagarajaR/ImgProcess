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
    public partial class camera_config : KryptonForm
    {
        public camera_config()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            kryptonPalette1.BasePaletteMode = com.kp;
            kryptonNavigator1.PaletteMode = com.cp;
            dataGridView1.PaletteMode = com.kp;


        }

       
        private void calibaration_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            this.errLbl1.SetVal = "";
          

            clear();


            this.kryptonNavigator1.SelectedIndex = 0;
           
            Display();

        }

        private void Display()
        {
            System.Data.DataTable dt1 = new DataTable();
            dt1 = com.sqlcn.Gettable("SELECT ModelName FROM camerasettings ");

            this.dataGridView1.DataSource = dt1;
            this.dataGridView1.Refresh();


            this.kryptonNavigator1.SelectedIndex = 0;


        }
        private void calibaration_FormClosing(object sender, FormClosingEventArgs e)
        {
         
            this.Dispose();



        }


   

        private void txtxpic_TextChanged(object sender, EventArgs e)
        {
            //xCalc();

        }

        private void txtxLength_TextChanged(object sender, EventArgs e)
        {
           // xCalc();
        }

       

     

     
        private void button4_Click(object sender, EventArgs e)
        {

        
          
            this.Dispose();

        }

     
        private void clear()
        {
            cmbModel.Text = cmbModel.Items[0].ToString();
            cmbResolution.Text = cmbResolution.Items[0].ToString();
            txtWidth.Text = "";
            txtHeight.Text = "";
            cmbPreview.Text = cmbPreview.Items[0].ToString();

            PubEditModelName = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {

            clear();

        }

        string PubEditModelName = "";


        private void button3_Click(object sender, EventArgs e)
        {
            this.errLbl1.SetVal = "";

            if (cmbResolution.Text.Length == 0)
            {
                errLbl1.SetVal = "Please Select Resolution";
                return;
            }

            if (cmbModel.Text.Length == 0)
            {
                errLbl1.SetVal = "Please Select Model";
                return;
            }

            txtHeight.Text = com.Val(txtHeight.Text).ToString();
            txtWidth.Text = com.Val(txtWidth.Text).ToString();

            if (PubEditModelName.Length == 0)
            {


                string qry = "INSERT INTO camerasettings " +
                                " (Resolution, " +
                                " Width, " +
                                " Height, " +
                                " Preview, " +
                                " ModelName) " +
                                " VALUES " +
                                " ('"+cmbResolution.Text+"', " +
                                " '"+txtWidth.Text+"', " +
                                " '" + txtHeight.Text + "', " +
                                " '"+cmbPreview.Text+"', " +
                                " '"+cmbModel.Text+"')";



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
                string qry = "UPDATE camerasettings " +
                            " SET " +
                            " Resolution = '"+cmbResolution.Text + "', " +
                            " Width = '"+txtWidth.Text+"', " +
                            " Height = '"+txtHeight.Text+"', " +
                            " Preview = '"+cmbPreview.Text+"', " +
                            " ModelName = '"+cmbModel.Text+"' " +
                            " WHERE ModelName = '"+PubEditModelName+"'";


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
                string modelname = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

                if (MessageBox.Show("Do you want to Edit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    clear();

                    

                    MySql.Data.MySqlClient.MySqlDataReader rst;
                    rst = com.sqlcn.Getdata("select * from Camerasettings where ModelName='" + modelname + "'");
                    if (rst.Read())
                    {
                        cmbResolution.Text =rst["Resolution"].ToString();
                        txtWidth.Text = rst["Width"].ToString();
                        txtHeight.Text = rst["Height"].ToString();
                        cmbModel.Text = rst["ModelName"].ToString();
                        cmbPreview.Text = rst["Preview"].ToString();
                        PubEditModelName = cmbModel.Text;
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
                    com.sqlcn.Exec("delete  from CameraSettings where ModelName='" + cid + "'");
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
          
            clear();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
           

            this.Dispose();
        }

        private void kryptonPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
