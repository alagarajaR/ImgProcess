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
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Microsoft.Office;

namespace ImgProcess
{
    public partial class savework : KryptonForm
    {
        long WorkId = 0;
        string ComName = "";
        string JobName = "";
        string Location = "";
        string Location1 = "";
        string Magnification = "";
        imageWM f1;
        public savework(long WId,string CName,string JName,string Loc,string mag,imageWM f)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            WorkId = WId;
            ComName = CName;
            JobName = JName;
            Location = Loc;
            Location1 = Location;
            Magnification = mag;
            f1 = f;

            this.kryptonPalette1.BasePaletteMode = com.kp;
        }

        private void savework_Load(object sender, EventArgs e)
        {

            if (MySettings.Status == 1)
                cmbPass.Enabled = false;
            else
                cmbPass.Enabled = true;


            cmdreport.Enabled = false;
            if (WorkId > 0)
            {
                cmdreport.Enabled = true;
                MySql.Data.MySqlClient.MySqlDataReader rst;
                rst=com.sqlcn.Getdata("select * from SaveWork where WorkId='" + WorkId + "'");
                if (rst.Read())
                {
                    txtItemName.Text = rst["ItemName"].ToString();
                    txtReference.Text = rst["Reference"].ToString();
                    cmbPass.Text = rst["Pass"].ToString();
                    txtMachine.Text = rst["Machine"].ToString();
                    txtPartName.Text = rst["PartName"].ToString();
                    txtPartNumber.Text = rst["PartNo"].ToString();
                    txtModel.Text = rst["Model"].ToString();
                    txtShift.Text = rst["Shift"].ToString();
                    txtDesc.Text = rst["Description"].ToString();
                    txtDept.Text = rst["Department"].ToString();
                    txtSupplier.Text = rst["SupplierName"].ToString();
                    txtRemarks.Text = rst["Remarks"].ToString();
                    txtCheckedBy.Text = rst["CheckedBy"].ToString();
                    txtApprovedBy.Text = rst["ApprovedBy"].ToString();

                }
                rst.Close();

            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

           



            if (txtItemName.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Item Name", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtItemName.Select();
                return;
            }

            string xxLocation = "";

            string ImageName = System.IO.Path.GetFileName(Location);
            xxLocation = System.IO.Path.GetDirectoryName(Location);
            xxLocation = xxLocation.Substring(0, xxLocation.Length - 6);

            xxLocation = xxLocation.Replace("\\", "\\\\");

            string fname1 = ImageName;

            string[] xx1 = fname1.Split('_');

            string ifname1 = xx1[0] + "_" + xx1[1] + "_"+txtItemName.Text + "_" + xx1[3] + "_" + xx1[4] + "_" + xx1[5] + "_" + xx1[6] + "_" + xx1[7] + "_" + xx1[8] + "_" + xx1[9];



            if (txtReference.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Reference", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReference.Select();
                return;
            }


            if (cmbPass.Text.Length == 0)
            {
                MessageBox.Show("Please Select Pass or Fail", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbPass.Select();
                return;
            }

            if (WorkId == 0)
            {
                MySql.Data.MySqlClient.MySqlDataReader rst;
                rst = com.sqlcn.Getdata("select (max(workid)) from SaveWork");
                if (rst.Read())
                    try
                    {
                        WorkId = (long)rst[0] + 1;
                    }
                    catch
                    {
                        WorkId = 1;
                    }
                else
                    WorkId = 1;

                rst.Close();

            


                string qry = "INSERT INTO `savework` " +
" (`WorkId`, " +
" `ItemName`, " +
" `Reference`, " +
" `Pass`, " +
" `Machine`, " +
" `PartName`, " +
" `PartNo`, " +
" `Model`, " +
" `Shift`, " +
" `Description`, " +
" `Department`, " +
" `SupplierName`, " +
" `Remarks`, " +
" `CheckedBy`, " +
" `ApprovedBy`, " +
" `ComponentName`, " +
" `JobName`, " +
" `Location`, " +
" `ImageName`,sdatetime,sdate,magnification) " +
" VALUES " +
" (" + WorkId + ", " +
" '" + txtItemName.Text + "', " +
" '" + txtReference.Text + "', " +
" '" + cmbPass.Text + "', " +
" '" + txtMachine.Text + "', " +
" '" + txtPartName.Text + "', " +
" '" + txtPartNumber.Text + "', " +
" '" + txtModel.Text + "', " +
" '" + txtShift.Text + "', " +
" '" + txtDesc.Text + "', " +
" '" + txtDept.Text + "', " +
" '" + txtSupplier.Text + "', " +
" '" + txtRemarks.Text + "', " +
" '" + txtCheckedBy.Text + "', " +
" '" + txtApprovedBy.Text + "', " +
" '" + ComName + "', " +
" '" + JobName + "', " +
" '" + xxLocation + "', " +
" '" + ifname1 + "', " +
" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', " +
" '" + DateTime.Now.ToString("yyyy-MM-dd") + "','"+Magnification+"')";



                try
                {
                    com.sqlcn.Exec(qry);
                    com.PWorkId = WorkId;
                    com.PubWorkName = txtItemName.Text;
                    this.Close();
                }
                catch(Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                    {
                        MessageBox.Show("Duplicate Item Name", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }


            }
            else
            {
                string qry = "UPDATE `savework`" +
" SET" +
" `ItemName` = '"+txtItemName.Text+"'," +
" `Reference` = '"+txtReference.Text+"'," +
" `Pass` = '"+cmbPass.Text+"'," +
" `Machine` = '"+txtMachine.Text+"'," +
" `PartName` = '"+txtPartName.Text+"'," +
" `PartNo` = '"+txtPartNumber.Text+"'," +
" `Model` = '"+txtModel.Text+"'," +
" `Shift` = '"+txtShift.Text+"'," +
" `Description` = '"+txtDesc.Text+"'," +
" `Department` = '"+txtDept.Text+"'," +
" `SupplierName` = '"+txtSupplier.Text+"'," +
" `Remarks` = '"+txtRemarks.Text+"'," +
" `CheckedBy` = '"+txtCheckedBy.Text+"'," +
" `ApprovedBy` = '"+txtApprovedBy.Text+"', " +
" `ComponentName` = '" + ComName + "', " +
" `JobName` = '" + JobName + "', " +
" `Location` = '" + xxLocation + "', " +
" `ImageName` = '" + ifname1 + "', " +
" `sdatetime` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "', " +
" `sdate` = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
" magnification='"+Magnification+"'" +
"  WHERE `WorkId` = " +WorkId+" ";

   
                try
                {
                    com.sqlcn.Exec(qry);
                    com.PWorkId = WorkId;
                    com.PubWorkName = txtItemName.Text;

                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                    {
                        MessageBox.Show("Duplicate Item Name", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }


            com.sqlcn.Exec("delete from saveworkdetail where WorkId='" + WorkId + "'");



        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            frmReport f = new frmReport(WorkId,f1);
            f.ShowDialog();



            //foreach (var process in Process.GetProcessesByName("excel"))
            //{
            //    process.Kill();
            //}

            //Microsoft.Office.Interop.Excel.Application xlap = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook workbook;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet;

            //workbook = xlap.Workbooks.Open("e:\\textxl.xlsx");
            //worksheet = workbook.Worksheets.Item[1];

            //Microsoft.Office.Interop.Excel.Range range1;
            //Microsoft.Office.Interop.Excel.Range range2;

            //range1 = worksheet.UsedRange;
            //range2 = range1.Find("#Tag", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            //range2.Value = "h1";
            //worksheet.Shapes.AddPicture("e:\\v.PNG", MsoTriState.msoFalse, MsoTriState.msoCTrue, range2.Left, range2.Top, 300, 300);


            //workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, "e:\\file.pdf");
            //xlap.Visible = true;
        }
    }
}
