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
    public partial class Measurements : KryptonForm
    {
        public Measurements()
        {
         


            InitializeComponent();
            this.DoubleBuffered = true;
            this.kryptonPalette1.BasePaletteMode = com.kp;


        }

        string Action = "";

        private enum gCol
        {
            MId,MName,MDesc,MType,Default,PGroup,BtnEdit,BtnDelete
        }
        private void Measurements_Load(object sender, EventArgs e)
        {
            cmbMType.Items.Clear();

            int i;
            for (i = 0; i <= com.PubMType.Length - 1; i++)
            {
                cmbMType.Items.Add(com.PubMType[i]);
            }

            LoadData();
        }

        private void LoadData()
        {

            System.Data.DataTable Dt;
            Dt = com.sqlcn.Gettable("select * from Measurements Order By slno");
            grd1.Rows.Clear();
            int i;
            for (i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                grd1.Rows.Add();
                grd1.Rows[i].Cells[(int)gCol.MId].Value = Dt.Rows[i]["MId"];
                grd1.Rows[i].Cells[(int)gCol.MName].Value = Dt.Rows[i]["MName"];
                grd1.Rows[i].Cells[(int)gCol.MDesc].Value = Dt.Rows[i]["MDesc"];
                grd1.Rows[i].Cells[(int)gCol.MType].Value = Dt.Rows[i]["MType"];
                grd1.Rows[i].Cells[(int)gCol.Default].Value = Dt.Rows[i]["Default"];
                grd1.Rows[i].Cells[(int)gCol.PGroup].Value = Dt.Rows[i]["PGroup"];
            }

        }


        private void grd1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            err1.SetVal = "";

            if (txtMName.Text.Length == 0)
            {
                err1.SetVal = "Please Enter Measurement Name";
                return;
            }
            if (txtMDesc.Text.Length == 0)
            {
                txtMDesc.Text = "-";
            }

            if (cmbMType.Text.Length == 0)
            {
                err1.SetVal = "Please Enter Measurement Type";
                return;
            }

            int isDefault = 0;
            if (chkDefault.Checked)
                isDefault = 1;

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
                MsgIcn = MessageBoxIcon.Asterisk;
            }
            if (MessageBox.Show(MsgText, MsgTitle, MsgBtn, MsgIcn) == DialogResult.No)
            {
                return;
            }



            if (Action == "New")
            {
                string MId = "";
                MySqlDataReader rst;
                rst = com.sqlcn.Getdata("select IFNULL(max(MId),0)+1 from measurements");
                if (rst.Read())
                {
                    MId = rst[0].ToString();
                }
                else
                {
                    MId = "1";
                }
                rst.Close();
                string SlNo = (grd1.Rows.Count+1).ToString();

                string InsQry = "INSERT INTO `measurements`" +
                            " (`MId`," +
                            " `MName`," +
                            " `MType`," +
                            " `Default`," +
                            " `PGroup`," +
                            " `SlNo`," +
                            " `MDesc`)" +
                            " VALUES (" +
                            " '"+MId+"'," +
                            " '"+txtMName.Text+"'," +
                            " '" + cmbMType.Text + "'," +
                           " " + isDefault + "," +
                            " '" + txtPGroup.Value.ToString() + "'," + 
                            " '"+SlNo+"'," +
                            " '"+txtMDesc.Text+"')";

                try
                {
                    com.sqlcn.Exec(InsQry);

                }
                catch ( Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                        err1.SetVal = "Duplicate Measure Name";
                    else
                    err1.SetVal = ex.Message;

                    return;
                }

            }
            else if (Action == "Edit")
            {
            
                string upqry= "UPDATE `measurements`" +
                        " SET " +
                        " `MName` = '"+txtMName.Text+"'," +
                        " `MType` = '" + cmbMType.Text + "'," +
                        " `Default` = " + isDefault + "," +
                        " `PGroup` = '" + txtPGroup.Value.ToString() + "'," +
                        " `MDesc` = '" + txtMDesc.Text + "'" +
                        "  WHERE `MId` = '" +txtMId.Text+"'";
                try
                {
                    com.sqlcn.Exec(upqry);

                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("UNIQUE") > 0)
                        err1.SetVal = "Duplicate Measure Name";
                    else
                        err1.SetVal = ex.Message;

                    return;
                }

            }
            else if (Action == "Delete")
            {


                string upqry = "delete from `measurements`" +
                        "  WHERE `MId` = '" + txtMId.Text + "'";
                try
                {
                    com.sqlcn.Exec(upqry);

                }
                catch (Exception ex)
                {
                    err1.SetVal = ex.Message;
                    return;
                }
            }

            Clear();

            LoadData();


            Action = "";
            grp1.Enabled = false;
            cmdSave.Enabled = false;
            cmdCancel.Enabled = false;
            grd1.Enabled = true;
            cmdSaveOrder.Enabled = true;
            cmdNew.Enabled = true;
            cmdSave.Text = "Save";


        }

        private void Clear()
        {
            txtMId.Text = "";
            txtMName.Text = "";
            txtMDesc.Text = "";
            txtPGroup.Value = 0;
            chkDefault.Checked = false;
        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            Clear();
            grp1.Enabled = true;
            Action = "New";
            cmdSave.Enabled = true;
            cmdCancel.Enabled = true;
            grd1.Enabled = false;
            cmdNew.Enabled = false;
           
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Action = "";
            grp1.Enabled = false;
            cmdSave.Enabled = false;
            cmdCancel.Enabled = false;
            grd1.Enabled = true;
            cmdNew.Enabled = true;
            cmdSave.Text = "Save";

        }

        private void Display()
        {
            txtMId.Text = grd1.Rows[grd1.CurrentCell.RowIndex].Cells[(int)gCol.MId].Value.ToString();

            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("select * from Measurements where Mid='" + txtMId.Text + "'");
            if (rst.Read())
            {
                txtMName.Text = rst["MName"].ToString();
                txtMDesc.Text = rst["MDesc"].ToString();
                cmbMType.Text = rst["MType"].ToString();
                txtPGroup.Text = rst["PGroup"].ToString();
                int isDefault = (int)rst["Default"];
                if (isDefault == 0)
                    chkDefault.Checked = false;
                else
                    chkDefault.Checked = true;
            }
            rst.Close();


        }
        private void grd1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)gCol.BtnEdit)
            {


                Display();
                grp1.Enabled = true;
                Action = "Edit";
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                grd1.Enabled = false;
                cmdNew.Enabled = false;
                cmdSave.Text = "Update";

            }
            else if (e.ColumnIndex == (int)gCol.BtnDelete)
            {
                Display();
                grp1.Enabled = true;
                Action = "Delete";
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                grd1.Enabled = false;
                cmdNew.Enabled = false;
                cmdSave.Text = "Delete";
            }
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
    }
}
