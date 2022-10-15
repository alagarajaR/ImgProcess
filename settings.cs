using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Drawing2D;
using Lyquidity.UtilityLibrary.Controls;
using Krypton.Toolkit;
using ImgProcess.Objects;
using System.Numerics;
using System.Reflection;


namespace ImgProcess
{
    public partial class settings : KryptonForm
    {
        public settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settings_Load(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;

            rst = com.sqlcn.Getdata("select * from Settings");
            if (rst.Read())
            {

                string CmpName = rst["CmpName"].ToString();
                string Add1 = rst["Add1"].ToString();
                string Add2 = rst["Add2"].ToString();
                string City = rst["City"].ToString();
                string State = rst["State"].ToString();
                string Country = rst["Country"].ToString();
                int   DPoint = (int)rst["DPoint"];
                string AngleDis = rst["AngleDis"].ToString();
                string ReportLocation = rst["ReportLocation"].ToString();
                string LogoLeft = rst["LogoLeft"].ToString();
                string LogoRight = rst["LogoRight"].ToString();
                string ReportFormat = rst["ReportFormat"].ToString();
                string ImgFormat = rst["ImgFormat"].ToString();
                int NoOfImageDis = (int)rst["NoOfImageDis"];
                int   NoOfWorkDis = (int)rst["NoOfWorkDis"];
                int   MaxMergedReport = (int)rst["MaxMergedReport"];
                string Caption = rst["Caption"].ToString();
                int   Threshold = (int)rst["Threshold"];
                int   Status = (int)rst["Status"];
                int   EValue = (int)rst["EValue"];
                int   Tol = (int)rst["Tol"];
                int   Thick = (int)rst["Thick"];
                int   AutoDraw = (int)rst["AutoDraw"];
                int   OWidth = (int)rst["OWidth"];
                int   FontSize = (int)rst["FontSize"];
                int   LT = (int)rst["LT"];
                int   LTP = (int)rst["LTP"];
                int   isDate = (int)rst["isDate"];
                int   isResult = (int)rst["isResult"];
                int   isName = (int)rst["isName"];
                int   Thresholding= (int)rst["Thresholding"];

                txtCmpName.Text = CmpName;
                txtAdd1.Text = Add1;
                txtAdd2.Text = Add2;
                txtCity.Text = City;
                txtState.Text = State;
                txtCountry.Text = Country;
                txtDecimal.Text = DPoint.ToString("0");
                
                if (AngleDis == "Hr")
                    chkHr.Checked = true;
                else if (AngleDis == "Min")
                    chkMin.Checked = true;
                else if (AngleDis == "Sec")
                    chkSec.Checked = true;

                txtReportLoc.Text = ReportLocation;
                txtLogoLeft.Text = LogoLeft;
                txtLogoRight.Text = LogoRight;
                cmbReport.Text = ReportFormat;
                cmbImage.Text = ImgFormat;
                txtImage.Text = NoOfImageDis.ToString("0");
                txtWork.Text = NoOfWorkDis.ToString("0");
                txtMaxParameter.Text = MaxMergedReport.ToString("0");

                txtCaption.Text = txtCaption.Text;
                txtThreshold.Text = Threshold.ToString("0");

                if (Status == 1)
                    optStatusY.Checked = true;
                else
                    optStatusN.Checked = true;

                if (EValue == 1)
                    optEValueY.Checked = true;
                else
                    optEValueN.Checked = true;

                if (Tol == 1)
                    optTolY.Checked = true;
                else
                    optTolN.Checked = true;

                if (Thick == 1)
                    optthickY.Checked = true;
                else
                    optthickN.Checked = true;

                if (AutoDraw == 1)
                    optDrawY.Checked = true;
                else
                    optDrawN.Checked = true;


                txtOWidth.Text = OWidth.ToString("0");
                txtFontSize.Text = FontSize.ToString("0");
                txtLTP.Text = LTP.ToString("0");

                if (LT == 1)
                    optLThickY.Checked = true;
                else
                    optLThickN.Checked = true;

                if (isDate == 1)
                    optDateY.Checked = true;
                else
                    optDateN.Checked = true;


                if (isResult == 1)
                    optResultY.Checked = true;
                else
                    optResultN.Checked = true;


                if (isName == 1)
                    optNameY.Checked = true;
                else
                    optNameN.Checked = true;


                if (Thresholding == 1)
                    optDefault.Checked = true;
                else
                    optGaussian.Checked = true;

            }
            rst.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {

            string CmpName = txtCmpName.Text;
            string Add1 = txtAdd1.Text;
            string Add2 = txtAdd2.Text;
            string City = txtCity.Text;
            string State = txtState.Text;
            string Country = txtCountry.Text;
            int DPoint = (int)Convert.ToInt32(txtDecimal.Text);

            string AngleDis="Hr";
            if (chkHr.Checked == true)
                AngleDis = "Hr";
            else if (chkMin.Checked == true)
                AngleDis = "Min";
            else if (chkSec.Checked == true)
                AngleDis = "Sec";


            string ReportLocation = txtReportLoc.Text;
            string LogoLeft = txtLogoLeft.Text;
            string LogoRight = txtLogoRight.Text;
            string ReportFormat = cmbReport.Text;
            string ImgFormat = cmbImage.Text;
            int NoOfImageDis = (int)Convert.ToInt32(txtImage.Text);
            int NoOfWorkDis = (int)Convert.ToInt32(txtWork.Text);
            int MaxMergedReport = (int)Convert.ToInt32(txtMaxParameter.Text);
            string Caption = txtCaption.Text;
            int Threshold = (int)Convert.ToInt32(txtThreshold.Text);
            int Status=0;
            int EValue = 0;
            int Tol = 0;
            int Thick = 0;
            int AutoDraw = 0;
            int OWidth = (int)Convert.ToInt32(txtOWidth.Text);
            int FontSize = (int)Convert.ToInt32(txtFontSize.Text);
            int LT = 0;
            int LTP = (int)Convert.ToInt32(txtLTP.Text);
            int isDate = 0;
            int isResult = 0;
            int isName = 0;
            int Thresholding = 0;

            if (OWidth == 0)
                OWidth = 1;

            if (FontSize == 0)
                FontSize = 8;


            if (optStatusY.Checked == true)
                Status = 1;
            if (optEValueY.Checked == true)
                EValue = 1;
            if (optTolY.Checked == true)
                Tol = 1;
            if (optthickY.Checked == true)
                Thick = 1;
            if (optDrawY.Checked == true)
                AutoDraw = 1;
            if (optLThickY.Checked == true)
                LT = 1;
            if (optDateY.Checked == true)
                isDate = 1;
            if (optResultY.Checked == true)
                isResult = 1;
            if (optNameY.Checked == true)
                isName = 1;
            if (optDefault.Checked == true)
                Thresholding = 1;



            try

            {
            
                com.sqlcn.Exec("delete from Settings");

                string insqry = "INSERT INTO `settings` " +
                    " (`CmpName`, " +
                    " `Add1`, " +
                    " `Add2`, " +
                    " `City`, " +
                    " `State`, " +
                    " `Country`, " +
                    " `DPoint`, " +
                    " `AngleDis`, " +
                    " `ReportLocation`, " +
                    " `LogoLeft`, " +
                    " `LogoRight`, " +
                    " `ReportFormat`, " +
                    " `NoOfImageDis`, " +
                    " `NoOfWorkDis`, " +
                    " `MaxMergedReport`, " +
                    " `Caption`, " +
                    " `Threshold`, " +
                    " `Status`, " +
                    " `EValue`, " +
                    " `Tol`, " +
                    " `Thick`, " +
                    " `AutoDraw`, " +
                    " `OWidth`, " +
                    " `FontSize`, " +
                    " `LT`, " +
                    " `LTP`, " +
                    " `isDate`, " +
                    " `isResult`, " +
                    " `isName`, " +
                    " `Thresholding`) " +
                    " VALUES(" +
                    "'" + CmpName + "', " +
                    "'" + Add1 + "', " +
                    "'" + Add2 + "', " +
                    "'" + City + "', " +
                    "'" + State + "', " +
                    "'" + Country + "', " +
                    "'" + DPoint + "', " +
                    "'" + AngleDis + "', " +
                    "'" + ReportLocation + "', " +
                    "'" + LogoLeft + "', " +
                    "'" + LogoRight + "', " +
                    "'" + ReportFormat + "', " +
                    "'" + NoOfImageDis + "', " +
                    "'" + NoOfWorkDis + "', " +
                    "'" + MaxMergedReport + "', " +
                    "'" + Caption + "', " +
                    "'" + Threshold + "', " +
                    "'" + Status + "', " +
                    "'" + EValue + "', " +
                    "'" + Tol + "', " +
                    "'" + Thick + "', " +
                    "'" + AutoDraw + "', " +
                    "'" + OWidth + "', " +
                    "'" + FontSize + "', " +
                    "'" + LT + "', " +
                    "'" + LTP + "', " +
                    "'" + isDate + "', " +
                    "'" + isResult + "', " +
                    "'" + isName + "', " +
                    "'" + Thresholding + "') ";


                com.sqlcn.Exec(insqry);
                MessageBox.Show("Successfully Saved", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                MySettings.ReadSettings();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
