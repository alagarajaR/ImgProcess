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
using MySql.Data.MySqlClient;
namespace ImgProcess
{
    public partial class ComponentsAdd : KryptonForm
    {
        string Action = "";
        long Editid = 0;

        public ComponentsAdd(string _Action,long _EditId)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.kryptonPalette1.BasePaletteMode = com.kp;
            this.kryptonNavigator1.PaletteMode = com.cp;
            Action = _Action;
            Editid = _EditId;

        }

      

        private enum gCol
        {
            MId,MName,MType,ExpValue,TolP,TolM,Select,Report,Tag
        }


        private enum gCol1
        {
            JName, MName, MType, ExpValue, TolP, TolM, Report, Tag,BtnDelete, MId,JDesc
        }
        private void Measurements_Load(object sender, EventArgs e)
        {
            cmbUnit.Items.Clear();
            cmbUnit.Items.Add("mm");
            cmbUnit.Items.Add("cm");
            cmbUnit.Items.Add("inch");
            cmbUnit.Items.Add("micron");

            if (Action == "New")
            {
                Clear();
                cmdSave.Text = "Save";
            }
            else if (Action == "Edit")
            {
                Display();
                cmdSave.Text = "Update";
            }
            else if (Action == "View")
            {
                Display();
                cmdSave.Text = "OK";
            }
            else if (Action == "Delete")
            {
                Display();
                cmdSave.Text = "Delete";
            }

          
        }

       


        private void grd1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            err1.SetVal = "";

            if (Action == "View")
            {
                this.Close();
                return; 
            }

            if (txtCName.Text.Length == 0)
            {
                err1.SetVal = "Please Enter Component Name";
                return;
            }
            

            if (cmbUnit.Text.Length == 0)
            {
                err1.SetVal = "Please Enter Unit Type";
                return;
            }

            if (txtLocation.Text.Length == 0)
            {
                err1.SetVal = "Please Select Location";
                return;
            }
            

            if (System.IO.Directory.Exists(txtLocation.Text) == false)
            {
                err1.SetVal = "This Location Not Found";
                return;
            }

            string Location = txtLocation.Text;
            Location = Location.Replace("\\", "/");

            if (txtCDesc.Text.Length == 0) txtCDesc.Text = "-";
            if (txtCustomer.Text.Length == 0) txtCustomer.Text = "-";
            if (txtPartName.Text.Length == 0) txtPartName.Text = "-";
            if (txtPartNumber.Text.Length == 0) txtPartNumber.Text = "-";
            if (txtReportHead.Text.Length == 0) txtReportHead.Text = "-";
            if (txtModel.Text.Length == 0) txtModel.Text = "-";


         
            string MsgText = "";
            string MsgTitle = "";
            MessageBoxButtons MsgBtn=MessageBoxButtons.OK;
            MessageBoxIcon MsgIcn = MessageBoxIcon.Information ;

            if (Action == "New")
            {
                MsgText = "Do you want to save?";
                MsgTitle = com.MsgTitle;
                MsgBtn = MessageBoxButtons.YesNo;
                MsgIcn = MessageBoxIcon.Information;
            }
            else if (Action == "Edit")
            {
                MsgText = "Do you want to update?";
                MsgTitle = com.MsgTitle;
                MsgBtn = MessageBoxButtons.YesNo;
                MsgIcn = MessageBoxIcon.Exclamation;
            }
            else if (Action == "Delete")
            {
                MsgText = "Do you want to delete?";
                MsgTitle = com.MsgTitle;
                MsgBtn = MessageBoxButtons.YesNo;
                MsgIcn = MessageBoxIcon.Error;
            }
            if (MessageBox.Show(MsgText, MsgTitle, MsgBtn, MsgIcn) == DialogResult.No)
            {
                return;
            }



            if (Action == "New")
            {
                string CId = "";
                MySqlDataReader rst;
                rst = com.sqlcn.Getdata("select IFNULL(max(CId),0)+1 from ComponentsHead");
                if (rst.Read())
                {
                    CId = rst[0].ToString();
                }
                else
                {
                    CId = "1";
                }
                rst.Close();

                txtCId.Text = CId.ToString();

                DateTime CDate = DateTime.Now.Date;

               
                string InsQry = "INSERT INTO `componentshead` " +
                                " (`CId`, " +
                                " `CDate`, " +
                                " `CName`, " +
                                " `CDesc`, " +
                                " `CLocation`, " +
                                " `Unit`, " +
                                " `CustName`, " +
                                " `ReportHeading`, " +
                                " `PartName`, " +
                                " `PartNumber`, " +
                                " `Model`) " +
                                " VALUES (" +
                                " '"+CId+"', " +
                                " '"+CDate.ToString("yyyy-MM-dd")+"', " +
                                " '"+txtCName.Text+"', " +
                                " '"+txtCDesc.Text+"', " +
                                " '"+ Location + "', " +
                                " '"+cmbUnit.Text+"', " +
                                " '"+txtCustomer.Text+"', " +
                                " '"+txtReportHead.Text+"', " +
                                " '"+txtPartName.Text+"', " +
                                " '"+txtPartNumber.Text+"', " +
                                " '"+txtModel.Text+"')";

                try
                {
                    com.sqlcn.Exec(InsQry);

                }
                catch ( Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                        err1.SetVal = "Duplicate Component Name";
                    else
                    err1.SetVal = ex.Message;

                    return;
                }

            }
            else if (Action == "Edit")
            {

                string upqry = "UPDATE `componentshead` " +
                            " SET " +
                            " `CName` = '"+txtCName.Text+"', " +
                            " `CDesc` = '"+txtCDesc.Text+"', " +
                            " `CLocation` = '"+ Location + "', " +
                            " `Unit` = '"+cmbUnit.Text+"', " +
                            " `CustName` = '"+txtCustomer.Text+"', " +
                            " `ReportHeading` = '"+txtReportHead.Text+"', " +
                            " `PartName` = '"+txtPartName.Text+"', " +
                            " `PartNumber` = '"+txtPartNumber.Text+"', " +
                            " `Model` = '"+txtModel.Text+"' " +
                            "  WHERE `CId` = '"+txtCId.Text+"'";

                try
                {
                    com.sqlcn.Exec(upqry);

                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                        err1.SetVal = "Duplicate Component Name";
                    else
                        err1.SetVal = ex.Message;

                    return;
                }

            }
            else if (Action == "Delete")
            {


                string upqry = "delete from `ComponentsHead`" +
                        "  WHERE `CId` = '" + txtCId.Text + "'";

                string upqry1 = "delete from `JobDetail`" +
                     "  WHERE `CId` = '" + txtCId.Text + "'";

                try
                {
                    com.sqlcn.Exec(upqry);
                    com.sqlcn.Exec(upqry1);
                }
                catch (Exception ex)
                {
                    err1.SetVal = ex.Message;
                    return;
                }
            }

            ///detail save - start
            if((Action=="New") || (Action == "Edit"))
            {
                int i = 0;

                com.sqlcn.Exec("delete from JobDetail where CId='" + Editid + "'");

                for (i = 0; i <= grd2.Rows.Count - 1; i++)
                {
                    string JName = grd2.Rows[i].Cells[(int)gCol1.JName].Value.ToString();
                    string MId = grd2.Rows[i].Cells[(int)gCol1.MId].Value.ToString();
                    float ExpValue = (float)Convert.ToDouble(grd2.Rows[i].Cells[(int)gCol1.ExpValue].Value);
                    float TolP = (float)Convert.ToDouble(grd2.Rows[i].Cells[(int)gCol1.TolP].Value);
                    float TolM = (float)Convert.ToDouble(grd2.Rows[i].Cells[(int)gCol1.TolM].Value);
                    bool Report = (bool)grd2.Rows[i].Cells[(int)gCol1.Report].Value;
                    bool Tag = (bool)grd2.Rows[i].Cells[(int)gCol1.Tag].Value;
                    string JDesc = grd2.Rows[i].Cells[(int)gCol1.JDesc].Value.ToString();

                    string Report1 = "0";
                    string Tag1 = "";
                    if (Report == true)
                        Report1 = "1";
                    if (Tag == true)
                        Tag1 = "1";


                    string qqry = "INSERT INTO `jobdetail` " +
                                    " (`CId`, " +
                                    " `MId`, " +
                                    " `EValue`, " +
                                    " `TPlus`, " +
                                    " `TMinus`, " +
                                    " `Report`, " +
                                    " `Tag`, " +
                                    " `JobName`, " +
                                    " `JobDesc`) " +
                                    " VALUES (" +
                                    " '"+txtCId.Text+"', " +
                                    " '"+MId+"', " +
                                    " '"+ExpValue+"', " +
                                    " '"+TolP+"', " +
                                    " '"+TolM+"', " +
                                    " '"+Report1+"', " +
                                    " '"+Tag1+"', " +
                                    " '"+JName+"', " +
                                    " '"+JDesc+"')";

                    try
                    {
                        com.sqlcn.Exec(qqry);
                        
                    }
                    catch (Exception ex)
                    {
                        err1.SetVal = ex.Message;
                        return;
                    }



                }


            }

            ///detail save - stop
            ///


            string FolderName = Convert.ToInt64(txtCId.Text).ToString("000000");
            Location = Location + "\\" + FolderName;
            if (System.IO.Directory.Exists(Location) == false)
            {
                
                
                System.IO.Directory.CreateDirectory(Location);
                System.IO.Directory.CreateDirectory(Location + "\\Images");
                System.IO.Directory.CreateDirectory(Location + "\\Working");
                System.IO.Directory.CreateDirectory(Location + "\\Reports");
                System.IO.Directory.CreateDirectory(Location + "\\Measurements");


            }



            if (Action == "New")
            {
                this.Close();
            }
            else if (Action == "Edit")
            {
                this.Close();
            }
            else if (Action == "Delete")
            {
                this.Close();
            }
            else if(Action == "View")
            {
                this.Close();
            }
        }

        private void Clear()
        {
            txtCId.Text = "";
            txtCName.Text = "";
            txtCDesc.Text = "";
            txtLocation.Text = "";
            cmbUnit.Text = "mm";
            txtCustomer.Text = "";
            txtPartName.Text = "";
            txtPartNumber.Text = "";
            txtModel.Text = "";
            txtReportHead.Text = "";
            txtJobName.Text = "";
            txtJDesc.Text = "";


            grd1.Rows.Clear();
            grd2.Rows.Clear();
            chkSelect.Checked = false;
            chkReport.Checked = false;
            chkTag.Checked = false;



            System.Data.DataTable Dt;
            Dt = com.sqlcn.Gettable("select * from Measurements Order By slno");
            grd1.Rows.Clear();
            int i;
            for (i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                grd1.Rows.Add();
                grd1.Rows[i].Cells[(int)gCol.MId].Value = Dt.Rows[i]["MId"];
                grd1.Rows[i].Cells[(int)gCol.MName].Value = Dt.Rows[i]["MName"];
                grd1.Rows[i].Cells[(int)gCol.MType].Value = Dt.Rows[i]["MType"];
                grd1.Rows[i].Cells[(int)gCol.ExpValue].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.TolP].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.TolM].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.Select].Value = 0;
                grd1.Rows[i].Cells[(int)gCol.Report].Value = 0;
                grd1.Rows[i].Cells[(int)gCol.Tag].Value = 0;
            }

        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            Clear();
            grp1.Enabled = true;
            Action = "New";
            grd1.Enabled = false;
            cmdNew.Enabled = false;
           
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Display()
        {
            txtCId.Text = Editid.ToString();

            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("select * from ComponentsHead where CId='" + txtCId.Text + "'");
            if (rst.Read())
            {
                txtCName.Text = rst["CName"].ToString();
                txtCDesc.Text = rst["CDesc"].ToString();
                cmbUnit.Text = rst["Unit"].ToString();

                txtLocation.Text = rst["CLocation"].ToString();
                cmbUnit.Text = rst["Unit"].ToString();
                txtCustomer.Text = rst["CustName"].ToString();
                txtPartName.Text = rst["PartName"].ToString();
                txtPartNumber.Text = rst["PartNumber"].ToString();
                txtModel.Text = rst["Model"].ToString();
                txtReportHead.Text = rst["ReportHeading"].ToString();
                

            }
            rst.Close();





            System.Data.DataTable Dt1;
            Dt1 = com.sqlcn.Gettable("select a.*,b.MName,b.MType from JobDetail a,Measurements b where a.Mid=b.Mid and a.CId='"+txtCId.Text+"'");
            grd2.Rows.Clear();
            int i;
            for (i = 0; i <= Dt1.Rows.Count - 1; i++)
            {
                bool report, Tag;
                if (Dt1.Rows[i]["Report"].ToString() == "1")
                    report = true;
                else
                    report = false;

                if (Dt1.Rows[i]["tag"].ToString() == "1")
                    Tag = true;
                else
                    Tag = false;


                grd2.Rows.Add();
                grd2.Rows[i].Cells[(int)gCol1.MId].Value = Dt1.Rows[i]["MId"];
                grd2.Rows[i].Cells[(int)gCol1.MName].Value = Dt1.Rows[i]["MName"];
                grd2.Rows[i].Cells[(int)gCol1.MType].Value = Dt1.Rows[i]["MType"];
                grd2.Rows[i].Cells[(int)gCol1.ExpValue].Value = Dt1.Rows[i]["EValue"];
                grd2.Rows[i].Cells[(int)gCol1.TolP].Value = Dt1.Rows[i]["TPlus"];
                grd2.Rows[i].Cells[(int)gCol1.TolM].Value = Dt1.Rows[i]["TMinus"];
                grd2.Rows[i].Cells[(int)gCol1.Report].Value = report;
                grd2.Rows[i].Cells[(int)gCol1.Tag].Value = Tag;
                grd2.Rows[i].Cells[(int)gCol1.JName].Value = Dt1.Rows[i]["JobName"];
                grd2.Rows[i].Cells[(int)gCol1.JDesc].Value = Dt1.Rows[i]["JobDesc"];
            }









            txtJobName.Text = "";
            txtJDesc.Text = "";


            System.Data.DataTable Dt;
            Dt = com.sqlcn.Gettable("select * from Measurements Order By slno");
            grd1.Rows.Clear();
          
            for (i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                grd1.Rows.Add();
                grd1.Rows[i].Cells[(int)gCol.MId].Value = Dt.Rows[i]["MId"];
                grd1.Rows[i].Cells[(int)gCol.MName].Value = Dt.Rows[i]["MName"];
                grd1.Rows[i].Cells[(int)gCol.MType].Value = Dt.Rows[i]["MType"];
                grd1.Rows[i].Cells[(int)gCol.ExpValue].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.TolP].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.TolM].Value = "0";
                grd1.Rows[i].Cells[(int)gCol.Select].Value = 0;
                grd1.Rows[i].Cells[(int)gCol.Report].Value = 0;
                grd1.Rows[i].Cells[(int)gCol.Tag].Value = 0;
            }


        }
        private void grd1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void err1_Load(object sender, EventArgs e)
        {

        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            DataGridView dgv = grd1;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }

        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = grd1;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void cmdSaveOrder_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                string Mid = grd1.Rows[i].Cells[(int)gCol.MId].Value.ToString();
                com.sqlcn.Exec("update Measurements set slno='" + (i + 1) + "' where Mid='" + Mid + "'");
            }

            MessageBox.Show("Sort Order Updated Successfully", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                if (chkSelect.CheckState == CheckState.Checked)
                    grd1.Rows[i].Cells[(int)gCol.Select].Value = true;
                else if(chkSelect.CheckState == CheckState.Unchecked)
                    grd1.Rows[i].Cells[(int)gCol.Select].Value = false;
            }
        }

        private void chkReport_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                if (chkReport.CheckState == CheckState.Checked)
                    grd1.Rows[i].Cells[(int)gCol.Report].Value = true;
                else if (chkReport.CheckState == CheckState.Unchecked)
                    grd1.Rows[i].Cells[(int)gCol.Report].Value =false;
            }
        }

        private void chkTag_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                if (chkTag.CheckState == CheckState.Checked)
                    grd1.Rows[i].Cells[(int)gCol.Tag].Value = true;
                else if (chkTag.CheckState == CheckState.Unchecked)
                    grd1.Rows[i].Cells[(int)gCol.Tag].Value = false;
            }
        }

        private void ChkStateUpdate()
        {
            int i = 0;

            int SelectCnt = 0;
            int ReportCnt = 0;
            int TagCnt = 0;

            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                if (grd1.Rows[i].Cells[(int)gCol.Select].Value.ToString() == "True")
                    SelectCnt = SelectCnt + 1;
                if (grd1.Rows[i].Cells[(int)gCol.Report].Value.ToString() == "True")
                    ReportCnt = ReportCnt + 1;
                if (grd1.Rows[i].Cells[(int)gCol.Tag].Value.ToString() == "True")
                    TagCnt = TagCnt + 1;
            }

            if (SelectCnt == 0)
                chkSelect.CheckState = CheckState.Unchecked;
            else if (SelectCnt == grd1.Rows.Count)
                chkSelect.CheckState = CheckState.Checked;
            else
                chkSelect.CheckState = CheckState.Indeterminate;

            if (TagCnt == 0)
                chkTag.CheckState = CheckState.Unchecked;
            else if (TagCnt == grd1.Rows.Count)
                chkTag.CheckState = CheckState.Checked;
            else
                chkTag.CheckState = CheckState.Indeterminate;

            if (ReportCnt == 0)
                chkReport.CheckState = CheckState.Unchecked;
            else if (ReportCnt == grd1.Rows.Count)
                chkReport.CheckState = CheckState.Checked;
            else
                chkReport.CheckState = CheckState.Indeterminate;

        }



        private void grd1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == (int)gCol.Select) || (e.ColumnIndex == (int)gCol.Report) || (e.ColumnIndex == (int)gCol.Tag))
                ChkStateUpdate();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            err1.SetVal = "";
            if (txtJobName.Text == "")
            {
                err1.SetVal = "Please Enter Job Name";
                txtJobName.Select();
                return;
            }

            int i = 0;
            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {
                if (grd1.Rows[i].Cells[(int)gCol.Select].Value.ToString() == "True")
                {

                    int k = 0;
                    for (k = 0; k <= grd2.Rows.Count - 1; k++)
                    {
                        if ((grd2.Rows[k].Cells[(int)gCol1.JName].Value.ToString() == txtJobName.Text) && (grd2.Rows[k].Cells[(int)gCol1.MName].Value.ToString() == grd1.Rows[i].Cells[(int)gCol.MName].Value.ToString()))
                        {
                            MessageBox.Show(grd2.Rows[k].Cells[(int)gCol1.MName].Value + " Already Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                }

            }


            int j = 0;
            j = grd2.Rows.Count;

            for (i = 0; i <= grd1.Rows.Count - 1; i++)
            {

                if(grd1.Rows[i].Cells[(int)gCol.Select].Value.ToString()=="True")
                {

                    

                grd2.Rows.Add();
                j = grd2.Rows.Count - 1;
                grd2.Rows[j].Cells[(int)gCol1.JName].Value = txtJobName.Text;
                grd2.Rows[j].Cells[(int)gCol1.JDesc].Value = txtJDesc.Text;
                grd2.Rows[j].Cells[(int)gCol1.MName].Value = grd1.Rows[i].Cells[(int)gCol.MName].Value;
                grd2.Rows[j].Cells[(int)gCol1.MType].Value = grd1.Rows[i].Cells[(int)gCol.MType].Value;
                grd2.Rows[j].Cells[(int)gCol1.ExpValue].Value = grd1.Rows[i].Cells[(int)gCol.ExpValue].Value;
                grd2.Rows[j].Cells[(int)gCol1.TolP].Value = grd1.Rows[i].Cells[(int)gCol.TolP].Value;
                grd2.Rows[j].Cells[(int)gCol1.TolM].Value = grd1.Rows[i].Cells[(int)gCol.TolM].Value;
                grd2.Rows[j].Cells[(int)gCol1.Report].Value = grd1.Rows[i].Cells[(int)gCol.Report].Value;
                grd2.Rows[j].Cells[(int)gCol1.Tag].Value = grd1.Rows[i].Cells[(int)gCol.Tag].Value;
                    grd2.Rows[j].Cells[(int)gCol1.MId].Value = grd1.Rows[i].Cells[(int)gCol.MId].Value;
                }
            }

            kryptonNavigator1.SelectedIndex = 1;

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            DataGridView dgv = grd2;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            DataGridView dgv = grd2;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void grd2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)gCol1.BtnDelete)
            {
                if (MessageBox.Show("Do you want to Delete Current Row?", com.MsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    grd2.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void cmdLoc_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if(f.ShowDialog()==DialogResult.OK)
            txtLocation.Text = f.SelectedPath;
        }
    }
}
