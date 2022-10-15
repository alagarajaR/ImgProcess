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
    public partial class Components : KryptonForm
    {
        public Components()
        {
         


            InitializeComponent();
            this.DoubleBuffered = true;
            this.kryptonPalette1.BasePaletteMode = com.kp;


        }

        string Action = "";

        private enum gCol
        {
            CId,CName,CDate,CDesc,Unit,Location,BtnEdit,BtnView,BtnDelete
        }
        private void Measurements_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            status = true;
            System.Data.DataTable Dt;
            Dt = com.sqlcn.Gettable("select * from ComponentsHead Order By Cid desc");
            grd1.Rows.Clear();
            int i;
            for (i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                grd1.Rows.Add();
                grd1.Rows[i].Cells[(int)gCol.CId].Value = Dt.Rows[i]["CId"];
                grd1.Rows[i].Cells[(int)gCol.CName].Value = Dt.Rows[i]["CName"];
                grd1.Rows[i].Cells[(int)gCol.CDesc].Value = Dt.Rows[i]["CDesc"];
                grd1.Rows[i].Cells[(int)gCol.Unit].Value = Dt.Rows[i]["Unit"];
                grd1.Rows[i].Cells[(int)gCol.Location].Value = Dt.Rows[i]["CLocation"];
                grd1.Rows[i].Cells[(int)gCol.CDate].Value = Convert.ToDateTime(Dt.Rows[i]["CDate"]).ToString("dd-MM-yyyy");
            }
            status = false;
        }


        bool status = false;
        private void grd1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (status == true)
                return;


            string Cname = grd1.Rows[grd1.CurrentCell.RowIndex].Cells[(int)gCol.CName].Value.ToString();

            System.Data.DataTable Dt;
            Dt = com.sqlcn.Gettable("select distinct JobName,JobDesc from JobDetail where CId in (select Cid from ComponentsHead where CName='"+Cname+"')");
            grdjob.Rows.Clear();
            int i;
            for (i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                grdjob.Rows.Add();
                grdjob.Rows[i].Cells[0].Value = Dt.Rows[i]["JobName"];
                grdjob.Rows[i].Cells[1].Value = Dt.Rows[i]["JobDesc"];
               
            }
        }

    
        
        private void grd1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)gCol.BtnEdit)
            {
                long CId = (long)grd1.Rows[grd1.CurrentCell.RowIndex].Cells[(int)gCol.CId].Value;

                ComponentsAdd f = new ComponentsAdd("Edit", CId);
                f.ShowDialog();
                LoadData();
            }
            else if (e.ColumnIndex == (int)gCol.BtnView)
            {
                long CId = (long)grd1.Rows[grd1.CurrentCell.RowIndex].Cells[(int)gCol.CId].Value;

                ComponentsAdd f = new ComponentsAdd("View", CId);
                f.ShowDialog();
                LoadData();
            }
            else if (e.ColumnIndex == (int)gCol.BtnDelete)
            {
                long CId = (long)grd1.Rows[grd1.CurrentCell.RowIndex].Cells[(int)gCol.CId].Value;

                ComponentsAdd f = new ComponentsAdd("Delete", CId);
                f.ShowDialog();
                LoadData();
            }
        }

        private void err1_Load(object sender, EventArgs e)
        {

        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            ComponentsAdd f = new ComponentsAdd("New",0);
            f.ShowDialog();
            LoadData();
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    
    }
}
