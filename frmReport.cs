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
using Emgu.CV;
using Emgu.CV.Structure;

namespace ImgProcess
{
    public partial class frmReport : KryptonForm
    {

        long WorkId = 0;
        imageWM f1;
        public frmReport(long WId,imageWM f)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            WorkId = WId;
            f1 = f;

            this.kryptonPalette1.BasePaletteMode = com.kp;

        }

        private void frmReport_Load(object sender, EventArgs e)
        {

            string Location = System.AppContext.BaseDirectory + "\\Report_Templete\\";

            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Location);

            System.IO.FileInfo[] Files = d.GetFiles(); //Getting Text files

            cmbProduct.Items.Clear();
            int i;
            for (i = 0; i <= Files.Length - 1; i++)
            {
                if ((Files[i].Extension.ToUpper() == ".XLSX") && (Files[i].Name.Substring(0,1)!="~"))
                {
                    cmbProduct.Items.Add(Files[i].Name);
                }
            }

            if (cmbProduct.Items.Count > 0)
                cmbProduct.Text = cmbProduct.Items[0].ToString();

        }

        private bool Report(string reporttype)
        {

            if (cmbProduct.Text == "")
            { 
                MessageBox.Show("Select Templete", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            foreach (var process in Process.GetProcessesByName("excel"))
            {
                process.Kill();
            }


            Microsoft.Office.Interop.Excel.Application xlap = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            string curpath = System.AppContext.BaseDirectory;

            workbook = xlap.Workbooks.Open(curpath + "\\Report_Templete\\"+cmbProduct.Text+"");
            worksheet = workbook.Worksheets.Item[1];
            Microsoft.Office.Interop.Excel.Range range1;
            range1 = worksheet.UsedRange;
            System.Data.DataTable dt;
            Microsoft.Office.Interop.Excel.Range range2;

            string[] TableData = new string[9];

            range2 = range1.Find("#TSlNo", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[0] = "SlNo" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TMeasureName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[1] = "MeasureName" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();


            range2 = range1.Find("#TMeasureType", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[2] = "MeasureType" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TExpValue", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[3] = "ExpValue" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TActValue", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[4] = "ActValue" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TTPlus", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[5] = "TPlus" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TTMinus", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[6] = "TMinus" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TPassFail", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[7] = "PassFail" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();

            range2 = range1.Find("#TRemarks", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
            if (range2 != null) TableData[8] = "Remarks" + "|" + range2.Cells.Column.ToString() + "|" + range2.Cells.Row.ToString();


            dt = com.sqlcn.Gettable("select a.ItemName,a.Reference,a.Pass,a.Machine,a.PartName,a.PartNo,a.Model,a.Shift,a.Description" +
                        ",a.Department,a.SupplierName,a.Remarks,a.CheckedBy,a.ApprovedBy,c.Report" +
                        ",b.unit,b.CustName,d.MName,d.MType,c.EValue,c.TPlus,c.TMinus,c.Value,c.Remarks TRemarks,a.magnification,c.result from SaveWork a, componentshead b,saveworkdetail c, Measurements d" +
                        "  where a.componentName = b.CName and c.WorkId = a.WorkId and c.mid = d.mid and a.Workid='" + WorkId + "'");

            int i;



            Image<Bgr, Byte> img = new Image<Bgr, Byte>(f1.img1.Width, f1.img1.Height);
            Bitmap bmp = new Bitmap(f1.img1.ClientSize.Width, f1.img1.ClientSize.Height);
            f1.img1.DrawToBitmap(bmp, f1.img1.ClientRectangle);

            string Path = System.IO.Path.GetDirectoryName(f1.PubPath);
            Path = Path.Substring(0, Path.Length - 7);
            Path = Path + "\\Reports\\";
            string Ext = System.IO.Path.GetExtension(f1.PubPath);

            string ImageFileName = Path + "IMG_" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "" + Ext;
            string ReportFileNamepdf = Path + "Report_" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "." + "pdf";
            string ReportFileNamexl = Path + "Report_" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "." + "xlsx";

            bmp.Save(ImageFileName);

            int i1 = -1;

            int rowNumber = 0;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {


                range2 = range1.Find("#ItemName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["ItemName"];

                range2 = range1.Find("#Reference", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Reference"];

                range2 = range1.Find("#PassFail", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Pass"];

                range2 = range1.Find("#Machine", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Machine"];

                range2 = range1.Find("#PartName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["PartName"];

                range2 = range1.Find("#PartNumber", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["PartNo"];

                range2 = range1.Find("#Model", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Model"];

                range2 = range1.Find("#Shift", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Shift"];

                range2 = range1.Find("#Description", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Description"];

                range2 = range1.Find("#Department", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Department"];

                range2 = range1.Find("#SupplierName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["SupplierName"];

                range2 = range1.Find("#Remarks", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Remarks"];


                range2 = range1.Find("#CheckedBy", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["CheckedBy"];


                range2 = range1.Find("#ApprovedBy", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["ApprovedBy"];


                range2 = range1.Find("#Unit", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["Unit"];

                range2 = range1.Find("#CustName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["CustName"];

                range2 = range1.Find("#Magnification", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = dt.Rows[i]["magnification"];

                string lastuser = (string)Properties.Settings.Default["LastUser"];

                range2 = range1.Find("#UserName", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = lastuser;

                range2 = range1.Find("#Date", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null) range2.Value = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                range2 = range1.Find("#Image", Missing.Value, XlFindLookIn.xlValues, Missing.Value, Missing.Value, XlSearchDirection.xlNext, false, false, Missing.Value);
                if (range2 != null)
                {
                    range2.Value = "test";

                    worksheet.Shapes.AddPicture(ImageFileName, MsoTriState.msoFalse, MsoTriState.msoCTrue, range2.Left, range2.Top, range2.MergeArea.Width, range2.MergeArea.Height);
                }

                string[] x1;
                int c1;
                int r1;
            
                if ((TableData[0] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[0].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    i1 = i1 + 1;

                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = i1 + 1;
                    range2.Cells.BorderAround2();
                }

                if ((TableData[1] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                    {
                    x1 = TableData[1].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                  
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["MName"];
                    range2.Cells.BorderAround2();
                }

                if ((TableData[2] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[2].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["MType"];
                    range2.Cells.BorderAround2();
                }

                if ((TableData[3] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[3].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["EValue"];
                    range2.Cells.BorderAround2();
                }

                if ((TableData[4] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[4].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["Value"];
                    range2.Cells.BorderAround2();
                }

                if ((TableData[5] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[5].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["TPlus"];
                    range2.Cells.BorderAround2();
                }


                if ((TableData[6] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[6].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["TMinus"];
                    range2.Cells.BorderAround2();
                }


                if ((TableData[7] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[7].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["result"];
                    range2.Cells.BorderAround2();
                }

                if ((TableData[8] != null) && (dt.Rows[i]["Report"].ToString() == "1"))
                {
                    x1 = TableData[8].Split('|');
                    c1 = Convert.ToInt32(x1[1]);
                    r1 = Convert.ToInt32(x1[2]);
                    range2 = worksheet.Cells[r1 + i1, c1];
                    range2.Value2 = dt.Rows[i]["TRemarks"];
                    range2.Cells.BorderAround2();
                }

            }


            if (reporttype == "PDF")
            {
                workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, ReportFileNamepdf);
                
                MessageBox.Show("Report Stored in this Location : " + ReportFileNamepdf + "");

                Clipboard.SetText(ReportFileNamepdf);

                Process.Start(ReportFileNamepdf);

            }
            else if (reporttype == "EXCEL")
            {
                workbook.SaveAs(ReportFileNamexl);
                MessageBox.Show("Report Stored in this Location : " + ReportFileNamexl + "");
                Clipboard.SetText(ReportFileNamexl);
                Process.Start(ReportFileNamexl);
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Report("PDF")==true)
            {
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Report("EXCEL") == true)
            {
                this.Close();
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
