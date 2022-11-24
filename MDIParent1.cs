using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImgProcess;
using Emgu.CV;
using Emgu.CV.Structure;
using Numpy.Models;
using System.Drawing.Imaging;
using ComponentFactory.Krypton.Toolkit;
using System.Reflection;
using Envision_CamLib;
using System.IO;
using Envision.Common;

namespace ImgProcess
{
    public partial class MDIParent1 : Krypton.Toolkit.KryptonForm
    {
        private int childFormNumber = 0;


        public MDIParent1()
        {
            
            InitializeComponent();
            this.DoubleBuffered = true;
            ApplyTheme();

            mnuOpen.Click += kryptonRibbonGroupButton1_Click_1;
            mnuSave.Click += kryptonRibbonGroupButton2_Click;
            mnuSaveas.Click += kryptonRibbonGroupButton3_Click;
            mnuMeasurements.Click += MnuMeasurements_Click;
            mnuComponents.Click += MnuComponents_Click;
            mnuLogOff.Click += MnuLogOff_Click;
            mnuLogOut.Click += MnuLogOut_Click; ;
            mnuSettings.Click += MnuSettings_Click;

            grp0.Width = 25;
            grp1.Width = 25;
           
            cmdp0.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
            cmdp1.StateCommon.Back.ImageStyle = Krypton.Toolkit.PaletteImageStyle.CenterMiddle;
          
            cmdp0.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayright;
            cmdp1.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayleft;
            
            
            Navi1.SelectedPage = kryptonPage6;


            if (com.WMLic == true)
            {
                grp1.Visible = true;
                grp0.Enabled = false;
                mnuMeasurements.Enabled = true;
                mnuComponents.Enabled = true;
                grp1.Width = 400;
                grp4.Visible = false;

            }
            else
            {
                mnuMeasurements.Enabled = false;
                mnuComponents.Enabled = false;
                grp4.Visible = true;
                grp1.Visible = false;
                grp0.Enabled = true;
                grp4.Width = 250;

            }


            EDControls(false);
        }

        private void MnuSettings_Click(object sender, EventArgs e)
        {
            settings ff = new settings();
            ff.ShowDialog();
        }

        private void MnuLogOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuLogOff_Click(object sender, EventArgs e)
        {
         
            frmLogin ff = new frmLogin();
            ff.Show();
            this.Close();
        }

        public void EDControls(bool enable)
        {
            mnuSaveas.Visible = false;

            if (com.IMLic==true)
            {
                cmdSave.Enabled = true;
                cmdLoad.Enabled = true;
                grp0.Enabled = true;
                mnuSave.Enabled = true;
                
                mnuOpen.Enabled = true;
                cmdSaveAs.Enabled = true;
                cmdCal.Enabled = true;
                cmdScale.Enabled = true;

            }
            else
            {
                cmdSave.Enabled = enable;
                cmdLoad.Enabled = enable;
                grp0.Enabled = enable;
                mnuSave.Enabled = enable;
                mnuSaveas.Enabled = enable;
                mnuOpen.Enabled = enable;
                cmdCal.Enabled = enable;
                cmdScale.Enabled = enable;
            }

             
            
            if(enable==false)
            { 
            txtJobId.Text = "";
            txtWorkId.Text = "";
            cmbComponents.Text = "";
            cmbJobs.Text = "";
            imgPanel.Controls.Clear();
            imgWorkPanel.Controls.Clear();
            grd2.Rows.Clear();
            }
        }
        private void MnuComponents_Click(object sender, EventArgs e)
        {

            
            Components f = new Components();
            f.ShowDialog();

        }

        private void MnuMeasurements_Click(object sender, EventArgs e)
        {
            Measurements f = new Measurements();
            f.ShowDialog();

        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ApplyTheme()
        {
            Krypton.Toolkit.PaletteMode p = com.kp;
            ComponentFactory.Krypton.Toolkit.PaletteMode p1 = com.cp;

            kryptonPalette1.ResetToDefaults(true);
            kryptonPalette1.BasePaletteMode = p;

            Navi1.PaletteMode = p1;
            kryptonNavigator2.PaletteMode = p1;
            kryptonRibbon1.PaletteMode = p1;
            kryptonNavigator3.PaletteMode = p1;
            kryptonNavigator1.PaletteMode = p1;
            kryptonNavigator4.PaletteMode = p1;

            SetThemeButton();
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        int pmode = 1;
        private void button4_Click(object sender, EventArgs e)
        {
            if (pmode > 1)
                pmode = pmode - 1;
            else
                pmode = 11;

            int x = pmode;
            Krypton.Toolkit.PaletteMode p = (Krypton.Toolkit.PaletteMode)x;
            ComponentFactory.Krypton.Toolkit.PaletteMode p1 = (ComponentFactory.Krypton.Toolkit.PaletteMode)x;
            kryptonLabel1.Text = p.ToString();

            kryptonPalette1.ResetToDefaults(true);
            kryptonPalette1.BasePaletteMode = p;
            Navi1.PaletteMode = p1;
            kryptonNavigator2.PaletteMode = p1;
            kryptonRibbon1.PaletteMode = p1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pmode < 11)
                pmode = pmode + 1;
            else
                pmode = 1;

            int x = pmode;
            Krypton.Toolkit.PaletteMode p = (Krypton.Toolkit.PaletteMode)x;
            ComponentFactory.Krypton.Toolkit.PaletteMode p1 = (ComponentFactory.Krypton.Toolkit.PaletteMode)x;
            kryptonLabel1.Text = p.ToString();
        
            kryptonPalette1.ResetToDefaults(true);
            kryptonPalette1.BasePaletteMode = p;
            Navi1.PaletteMode = p1;
            kryptonNavigator2.PaletteMode = p1;
            kryptonRibbon1.PaletteMode = p1;

        }

        public string Header = "";
        private void GetXYValue()
        {
            com.DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + com.PubScaleMode + "'");


        }


        public void LoadMag()
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst=com.sqlcn.Getdata("Select distinct magnification from calibration order by magnification");
            cmbmag.Items.Clear();
            cmbmag1.Items.Clear();
            while (rst.Read())
            {
                cmbmag.Items.Add(rst["magnification"] + "x");
                cmbmag1.Items.Add(rst["magnification"] + "x");
            }
            rst.Close();

            cmbmag.Text = com.PubCMag.ToString() + 'x';
            cmbmag1.Text = com.PubCMag.ToString() + 'x';

        }
        private void MDIParent2_Load(object sender, EventArgs e)
        {
            ApplySettings();

            if (com.IMLic == true)
            {
                string IMLocation = (string)Properties.Settings.Default["IMLocation"];
                txtIMLocation.Text = IMLocation;
            }
            
            com.PubScaleMode = (string)Properties.Settings.Default["Scale"];
            com.PubCMag = (Int32)Convert.ToDouble(Properties.Settings.Default["CMag"]);
            com.pcolor = (System.Drawing.Color)Properties.Settings.Default["pcolor"];
            com.mcolor = (System.Drawing.Color)Properties.Settings.Default["mcolor"];

            LoadMag(); 

            cmbmag.Text = com.PubCMag.ToString()+'x';
            cmbmag1.Text = com.PubCMag.ToString() + 'x';

            cmdColor1.SelectedColor = com.pcolor;

            cmdColor2.SelectedColor = com.mcolor;

            //com.pwidth = (float)Properties.Settings.Default["pwidth"];

            cmbWidth1.Text = com.pwidth.ToString();
            //com.mwidth = (float)Properties.Settings.Default["mwidth"];
            cmbWidth2.Text = com.mwidth.ToString();

            GetXYValue();

            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;


            if (com.PubScaleMode == "cm")
            {
                cmdScale.TextLine1 = "cm";
                com.PubScaleShort = "cm";
                mnucm.Checked = true;
            }
            else if (com.PubScaleMode == "mm")
            {
                cmdScale.TextLine1 = "mm";
                com.PubScaleShort = "mm";
                mnumm.Checked = true;
            }
            else if (com.PubScaleMode == "inch")
            {
                cmdScale.TextLine1 = "inch";
                com.PubScaleShort = "in";
                mnuinch.Checked = true;
            }
            else if (com.PubScaleMode == "micron")
            {
                cmdScale.TextLine1 = "micron";
                com.PubScaleShort = "μm";
                mnumic.Checked = true;
            }

            Navi1.SelectedIndex = 0;

            Navi1.SelectedIndex = 2;

            kryptonNavigator4.SelectedIndex = 0;

            SetThemeButton();

            Header = this.Text;

            if(com.WMLic==true)
            LoadComponents();


            if (com.IMLic == true)
                LoadIMImages();

            kryptonPanel1.Visible = true;
            kryptonPanel2.Visible = false;
            kryptonNavigator1.Height = 385;

            cmbResult.Items.Clear();
            cmbResult.Items.Add("");
            cmbResult.Items.Add("PASS");
            cmbResult.Items.Add("FAIL");
         
        }

        private void SetThemeButton()
        {
            if (com.KPNo == 19)
            {
                cmdBlack.Checked = true;
                cmdBlue.Checked = false;
                cmdSilver.Checked = false;
                SetTheme("B");
            }
            else if (com.KPNo == 12)
            {
                cmdBlue.Checked = true;
                cmdBlack.Checked = false;
                cmdSilver.Checked = false;
                SetTheme("B");
            }
            else if (com.KPNo == 15)
            {
                cmdSilver.Checked = true;
                cmdBlack.Checked = false;
                cmdBlue.Checked = false;
                SetTheme("B");
            }

        }
        private void kryptonRibbonGroupButton1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbon1_SelectedTabChanged(object sender, EventArgs e)
        {
          
        }

        private void kryptonRibbonGroupButton1_Click_1(object sender, EventArgs e)
        {
            string Path = "";
            try
            {
                Path = Properties.Settings.Default["LastPath"].ToString();
                if (Path == "")
                {
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
            }
            catch (Exception ex)
            {
                Path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }


            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.InitialDirectory = Path;

            saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";


            TZoom.Value = 100;
            trackBar1.Value = 100;
            trackBar2.Value = 0;

            this.trackHue.Value = 100;
            this.trackSat.Value = 100;
            this.trackLight.Value = 100;


            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                com.CurrentWork = "WM";
                string FileName = saveFileDialog.FileName;
                Path = System.IO.Path.GetDirectoryName(FileName);
                Properties.Settings.Default["LastPath"] = Path;
                Properties.Settings.Default.Save();

                if ((com.WMLic == true) && (txtJobId.Text != ""))
                {
                    string ext = System.IO.Path.GetExtension(FileName);

                    Random rn = new Random();
                    string rnd = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");

                    string xFileName = txtLoc.Text + "/Images/" + txtJobId.Text + "_" + "x" + "_" + rnd + ext;

                    System.IO.File.Copy(FileName, xFileName);

                    ImgPanelAdd();


                    return;
                }

                if (com.IMLic == true)
                {

                    com.CurrentWork = "IM";
                    string FileName1 = saveFileDialog.FileName;
                    Path = System.IO.Path.GetDirectoryName(FileName1);
                    Properties.Settings.Default["LastPath"] = Path;
                    Properties.Settings.Default.Save();

                   
                        string ext = System.IO.Path.GetExtension(FileName);

                        Random rn = new Random();
                        string rnd = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");

                        string xFileName = txtIMLocation.Text + "\\" + "IM_IMG_X" + "_" + rnd + ext;

                        System.IO.File.Copy(FileName1, xFileName);


                        imageWM f = new imageWM(xFileName);
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Normal;
                        f.WindowState = FormWindowState.Maximized;

                        grp0.Enabled = true;

                        LoadIMImages();

                        EDControls(true);


                    

                    /*              imageWM f = new imageWM(xFileName);
                                    f.MdiParent = this;
                                    f.Show();
                                    f.WindowState = FormWindowState.Normal;
                                    f.WindowState = FormWindowState.Maximized;
                    */

                }
            }
        }


        public void AdjustBrightness(int Value)
        {

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                Bitmap TempBitmap = f.MainImg.ToBitmap();

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float FinalValue = (float)Value / 255.0f;

            float[][] FloatColorMatrix ={

                    new float[] {1, 0, 0, 0, 0},

                    new float[] {0, 1, 0, 0, 0},

                    new float[] {0, 0, 1, 0, 0},

                    new float[] {0, 0, 0, 1, 0},

                    new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);

            Attributes.Dispose();

            NewGraphics.Dispose();

                Image<Bgr, byte> imgOutput;

                imgOutput = NewBitmap.ToImage<Bgr, Byte>();
                string CurrentContrast = ((float)trackBar1.Value / 100).ToString();
                imgOutput = imgOutput.Mul(double.Parse(CurrentContrast));



                f.LoadImage(imgOutput);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private void ContrastBrightnessAdjust()
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, byte> imgOutput;
                string CurrentContrast = ((float)trackBar1.Value / 100).ToString();
                imgOutput = f.MainImg.Mul(double.Parse(CurrentContrast));



                f.LoadImage(imgOutput);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        


        
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                AdjustBrightness(trackBar2.Value);

              // ContrastBrightnessAdjust();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ContrastBrightnessAdjust();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public imageWM GetActiveImageWindow()
        {
            imageWM f;

            try
            {
                f = (imageWM)this.ActiveMdiChild;
            }
            catch (Exception ex)
            {
                f = null;
            }
            return f;

        }

        public frmmag GetMagWindow()
        {
            frmmag f;

            try
            {
                f = (frmmag)this.ActiveMdiChild;
            }
            catch (Exception ex)
            {
                f = null;
            }
            return f;

        }


        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> img = f.MainImg.Sub(new Bgr(255, 255, 0));
                f.LoadImage(img);
            }
            catch
            {

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;



                Image<Bgr, Byte> img = f.MainImg.Sub(new Bgr(255, 0, 255));
                f.LoadImage(img);
            }
            catch
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;



                Image<Bgr, Byte> img = f.MainImg.Sub(new Bgr(0, 255, 255));
                f.LoadImage(img);
            }
            catch
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> img = f.MainImg_Org.Sub(new Bgr(0, 0, 0));
                f.LoadImage(img);
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Flip(Emgu.CV.CvEnum.FlipType.Horizontal);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Flip(Emgu.CV.CvEnum.FlipType.Vertical);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Rotate(180, new Bgr(Color.Transparent), false);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Rotate(90, new Bgr(Color.Transparent), false);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Rotate(-90, new Bgr(Color.Transparent), false);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isCorp = true;



            }
            catch (Exception ex)
            {

            }
        }

        public void TZoom_ValueChanged(object sender, EventArgs e)
        {
            try
            {



                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                int w = f.MainImg_Org.Width;
                int h = f.MainImg_Org.Height;

                int mag = Convert.ToInt32(TZoom.Value);
                int mag1 = mag - 100;
                int w1 = System.Math.Abs(w * mag1 / 100);
                int h1 = System.Math.Abs(h * mag1 / 100);

                if (mag1 > 0)
                {
                    w1 = w + w1;
                    h1 = h + h1;
                }
                else
                {
                    w1 = w - w1;
                    h1 = h - h1;
                }

                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(w1, h1);
                CvInvoke.Resize(f.MainImg_Org, OutImg, new Size(w1, h1), 0, 0, Emgu.CV.CvEnum.Inter.Linear);
                f.MainImg = OutImg;
                f.LoadImageZoom();

            }
            catch (System.NullReferenceException ex)
            {

            }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();
                f.isZoom = true;
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((com.Val(txtWidth.Text) == 0) || (com.Val(txtHeight.Text) == 0))
            {
                return;
            }

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                int w1 = (int)com.Val(txtWidth.Text);
                int h1 = (int)com.Val(txtHeight.Text);

                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(w1, h1);
                CvInvoke.Resize(f.MainImg, OutImg, new Size(w1, h1), 0, 0, Emgu.CV.CvEnum.Inter.Linear);
                f.MainImg = OutImg;
                f.LoadImage();

            }
            catch (System.NullReferenceException ex)
            {

            }
        }

        private void trackHue_ValueChanged(object sender, EventArgs e)
        {
            HUL();
        }

        private void trackSat_ValueChanged(object sender, EventArgs e)
        {
            HUL();
        }

        private void trackLight_ValueChanged(object sender, EventArgs e)
        {
            HUL();
        }


        private void SetTheme(string id)
        {
            string Location = System.AppContext.BaseDirectory + "\\Icons\\" + id.ToString() + "\\";

            btnLine.StateCommon.Back.Image =Image.FromFile(Location + "line.png");
            btnPPLine.StateCommon.Back.Image = Image.FromFile(Location + "ppline.png");
            btnCircle.StateCommon.Back.Image = Image.FromFile(Location + "circle.png");
            btnRect.StateCommon.Back.Image = Image.FromFile(Location + "rectangle.png");
            btnAngle.StateCommon.Back.Image = Image.FromFile(Location + "angle.png");
            btnClear1.StateCommon.Back.Image = Image.FromFile(Location + "clear.png");

            btnArrow.StateCommon.Back.Image = Image.FromFile(Location + "arrow.png");
            btnPoint.StateCommon.Back.Image = Image.FromFile(Location + "point.png");
            //btnCurve.StateCommon.Back.Image = Image.FromFile(Location + "curve.png");
            //btnCCurve.StateCommon.Back.Image = Image.FromFile(Location + "ccurve.png");
            btnText.StateCommon.Back.Image = Image.FromFile(Location + "text.png");
            btnMLine.StateCommon.Back.Image = Image.FromFile(Location + "mline.png");
            btnClear2.StateCommon.Back.Image = Image.FromFile(Location + "clear.png");
            kryptonButton2.StateCommon.Back.Image = Image.FromFile(Location + "arc.png");


            btngb.StateCommon.Back.Image = Image.FromFile(Location + "gaussian-blur.png");
            btngs.StateCommon.Back.Image = Image.FromFile(Location + "gaussan sharpen.png");
            btnGray.StateCommon.Back.Image = Image.FromFile(Location + "grascale.png");
            btnVin.StateCommon.Back.Image = Image.FromFile(Location + "vagmette.png");
            btninvert.StateCommon.Back.Image = Image.FromFile(Location + "invert.png");
            btnRcorner.StateCommon.Back.Image = Image.FromFile(Location + "rcorner.png");

            btnHflip.StateCommon.Back.Image = Image.FromFile(Location + "hflip.png");
            btnVflip.StateCommon.Back.Image = Image.FromFile(Location + "vflip.png");
            btnClock.StateCommon.Back.Image = Image.FromFile(Location + "rotateac.png");
            btnAClock.StateCommon.Back.Image = Image.FromFile(Location + "rotatec.png");
            //btnCorp.StateCommon.Back.Image = Image.FromFile(Location + "corp.png");


            btnZoom.StateCommon.Back.Image = Image.FromFile(Location + "zoom.png");
            btnResize.StateCommon.Back.Image = Image.FromFile(Location + "resize.png");


            cmdLine.Image = Image.FromFile(Location + "line16.png");
            cmdPPLine.Image = Image.FromFile(Location + "ppline16.png");
            cmdCircle.Image = Image.FromFile(Location + "circle16.png");
            cmdRect.Image = Image.FromFile(Location + "rectangle16.png");
            cmdClear.Image = Image.FromFile(Location + "clear16.png");
            cmdAngle.Image = Image.FromFile(Location + "angle16.png");

            cmdPoint.Image = Image.FromFile(Location + "point16.png");
            cmdMLine.Image = Image.FromFile(Location + "mline16.png");
            cmdArrow.Image = Image.FromFile(Location + "arrow16.png");
            cmdArc.Image = Image.FromFile(Location + "arc16.png");
            //cmdCurve.Image = Image.FromFile(Location + "rectangle16.png");
            //cmdCCurve.Image = Image.FromFile(Location + "clear16.png");
            cmdText.Image = Image.FromFile(Location + "text16.png");
            
        }
        private void HUL()
        {
            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            float satscale = ((float)trackSat.Value / 100);
            float huescale = ((float)trackHue.Value / 100);
            float lightscale = ((float)trackLight.Value / 100);

            var img = f.MainImg_Org;

            var imhHsv = img.Convert<Hsv, byte>();

            var channels = imhHsv.Split();

            channels[0] = (channels[0] * huescale);
            channels[1] = (channels[1] * satscale);
            channels[2] = (channels[2] * lightscale);

            Mat[] m = new Mat[3];
            m[0] = CvInvoke.CvArrToMat(channels[0]);
            m[1] = CvInvoke.CvArrToMat(channels[1]);
            m[2] = CvInvoke.CvArrToMat(channels[2]);

            Emgu.CV.Util.VectorOfMat vm = new Emgu.CV.Util.VectorOfMat(m);


            CvInvoke.Merge(vm, imhHsv);

            f.MainImg.ConvertFrom<Hsv, byte>(imhHsv);

            f.LoadImage();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Gray, Byte> grayImage = f.MainImg.Convert<Gray, Byte>();
                f.MainImg = grayImage.Convert<Bgr, Byte>();
                f.LoadImage();

            }
            catch (Exception ex)
            {

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                f.MainImg = f.MainImg.Not();
                f.LoadImage();

            }
            catch (Exception ex)
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(f.MainImg.Width, f.MainImg.Height);
                CvInvoke.GaussianBlur(f.MainImg, OutImg, new Size(0, 0), 3);
                f.LoadImage(OutImg);
            }
            catch
            {

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(f.MainImg.Width, f.MainImg.Height);

                Image<Gray, Byte> grayImage = f.MainImg.Convert<Gray, Byte>();
                OutImg = grayImage.Convert<Bgr, Byte>();
                Image<Bgr, byte> img = Sharpen(f.MainImg, f.MainImg.Width, f.MainImg.Height, 1, 1, 10);
                OutImg = img.Convert<Bgr, Byte>();

                f.LoadImage(OutImg);
            }
            catch
            {

            }
        }
        public static Image<Bgr, byte> Sharpen(Image<Bgr, byte> image, int w, int h, double sigma1, double sigma2, int k)
        {

            w = (w % 2 == 0) ? w - 1 : w;
            h = (h % 2 == 0) ? h - 1 : h;
            //apply gaussian smoothing using w, h and sigma 
            var gaussianSmooth = image.SmoothGaussian(w, h, sigma1, sigma2);
            //obtain the mask by subtracting the gaussian smoothed image from the original one 
            var mask = image - gaussianSmooth;
            //add a weighted value k to the obtained mask 
            mask *= k;
            //sum with the original image 
            image += mask;
            return image;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Bitmap StartImage;
                //string tmppath = System.IO.Path.GetTempPath() + "/" + DateTime.Now.ToString("ddmmyyhhmmss.fff") + ".bmp";
                //f.MainImg.Save(tmppath);



                StartImage = f.MainImg.ToBitmap();

                StartImage = f.Vignette(StartImage);

                f.MainImg = StartImage.ToImage<Bgr, Byte>();
                f.LoadImage();
            }
            catch (System.NullReferenceException ex)
            {

            }
            catch
            {

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                Bitmap StartImage;
                //string tmppath = System.IO.Path.GetTempPath() + "/" + DateTime.Now.ToString("ddmmyyhhmmss.fff") + ".bmp";
                //f.MainImg.Save(tmppath);

                StartImage = f.MainImg.ToBitmap();

                StartImage = f.RoundCorners(StartImage, 50, Color.White);

                f.MainImg = StartImage.ToImage<Bgr, Byte>();
                f.LoadImage();
            }
            catch (System.NullReferenceException ex)
            {

            }
            catch
            {

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isDrawLine = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isDrawRect = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isCircle = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isDrawArrowLine = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();


            }
            catch
            {

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isCurve = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isCCurve = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isPPLine = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button28_Click(object sender, EventArgs e)
        {

            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();

                string txt = Microsoft.VisualBasic.Interaction.InputBox("Enter Text", "Insert Text", "");
                f.WriteText = txt;
                f.IsMouseDown = true;
            }
            catch
            {

            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isAngle = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isMarkLine = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isMarkArc = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            com.pcolor = cd.Color;
            Properties.Settings.Default["pcolor"] = com.pcolor;
            Properties.Settings.Default.Save();


        }

        private void button25_Click(object sender, EventArgs e)
        {

          

        }

        private void kryptonRibbonGroupButton2_Click(object sender, EventArgs e)
        {

            try
            {

                if (com.CurrentWork == "WM")
                {

                    imageWM f = GetActiveImageWindow();
                    if (f == null)
                        return;


                    com.PWorkId = 0;
                    long wid = 0;
                    try
                    {
                        wid = (long)Convert.ToUInt64(txtWorkId.Text);
                    }
                    catch { }
                    savework ff = new savework(wid,cmbComponents.Text,cmbJobs.Text,f.PubPath,cmbmag.Text,f);
                    ff.ShowDialog();


                    if (com.PWorkId > 0)
                    {

                        txtWorkId.Text = com.PWorkId.ToString();

                      

                        f.isReDrawCancel = true;
                        f.img1.Invalidate();
                        string Location = f.PubPath;

                        //Bitmap bmp = new Bitmap(f.img1.ClientSize.Width, f.img1.ClientSize.Height);
                        //f.img1.DrawToBitmap(bmp, f.img1.ClientRectangle);

                        Bitmap im = (Bitmap)f.img1.Image;

                        //f.img1.Image.Save(Location);

                        im.Save(Location);

                        
                        //bmp.Save(Location);
                        f.isReDrawCancel = false;
                        f.img1.Invalidate();



                        string[] pathsplit = Location.Split('/');

                        string filename = pathsplit[pathsplit.Length - 1];

                        string ext1 = System.IO.Path.GetExtension(Location);

                        string[] x1 = filename.Split('_');
                        string workfilename =System.IO.Path.GetFileName(x1[0] + "_" + x1[1] + "_" + com.PubWorkName + "_" + x1[3] + "_" + x1[4] + "_" + x1[5] + "_" + x1[6] + "_" + x1[7] + "_" + x1[8] + "_" + x1[9]);

                        string LocationWork = txtLoc.Text + "/working/" + workfilename + ".wk";


                        Image<Bgr, Byte> img = new Image<Bgr, Byte>(f.img1.Width, f.img1.Height);
                        Bitmap bmp = new Bitmap(f.img1.ClientSize.Width, f.img1.ClientSize.Height);
                        f.img1.DrawToBitmap(bmp, f.img1.ClientRectangle);
                        string ImagePath = txtLoc.Text + "/working/" + workfilename;
                        bmp.Save(ImagePath);

                        string stringobj = Newtonsoft.Json.JsonConvert.SerializeObject(f.DrawObjects);

                        string TableVAlue = "";

                        int i = 0;
                        for (i = 0; i <= grd2.Rows.Count - 1; i++)
                        {

                            if (grd2.Rows[i].Cells[(int)gcol.Remarks].Value == null)
                                grd2.Rows[i].Cells[(int)gcol.Remarks].Value = "";

                            if (grd2.Rows[i].Cells[(int)gcol.Report].Value == null)
                                grd2.Rows[i].Cells[(int)gcol.Report].Value = false;

                            int Report = 0;
                            if ((bool)grd2.Rows[i].Cells[(int)gcol.Report].Value == true)
                                Report = 1;
                            


                            string Rowstxt = grd2.Rows[i].Cells[(int)gcol.MeasureName].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.EValue].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.TPlus].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.TMinus].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.Value].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.TextObj].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.Remarks].Value.ToString();
                            Rowstxt = Rowstxt + "|$|" + Report.ToString();
                            Rowstxt = Rowstxt + "|$|" + grd2.Rows[i].Cells[(int)gcol.CmbResult].Value.ToString();
                            if (TableVAlue == "")
                                TableVAlue = Rowstxt;
                            else
                                TableVAlue = TableVAlue + "|#|" + Rowstxt;
                        }

                        stringobj = stringobj + "$$" + TableVAlue + "$$" + com.PWorkId.ToString() + "$$" + f.MaxLineNumber.ToString() + "$$" + f.MaxCircleNumber.ToString() + "$$" + f.MaxRectNumber.ToString() + "$$" + f.MaxArrowNumber.ToString() + "$$" + f.MaxPointNumber.ToString() + "$$" + f.MaxCurveNumber.ToString() + "$$" + f.MaxAngleNumber.ToString() + "$$" + f.MaxIPointNumber.ToString() + "$$" + f.MaxArcNumber.ToString();
                        stringobj = StringCipher.Encrypt(stringobj, "$123?_");
                        System.IO.File.WriteAllText(LocationWork, stringobj);

                        ImgPanelAdd();

                        //int w = f.MainImg.Width;
                        //int h = f.MainImg.Height;
                        //Image<Bgr, Byte> im = new Image<Bgr, Byte>(w, h);
                        //im.Data = f.MainImg.Data;
                        //f.MainImg_Org = im;
                        ///sql save


                        for (i = 0; i <= grd2.Rows.Count - 1; i++)
                        {

                            string MId = "0";

                            MySql.Data.MySqlClient.MySqlDataReader rst;
                            rst = com.sqlcn.Getdata("select MId from Measurements where mName='"+ grd2.Rows[i].Cells[(int)gcol.MeasureName].Value.ToString() + "'");
                            if (rst.Read())
                            {
                                MId = rst[0].ToString();
                            }
                            rst.Close();

                            int Report = 0;
                            if ((bool)grd2.Rows[i].Cells[(int)gcol.Report].Value == true)
                                Report = 1;



                            string iqry = "INSERT INTO `imgdb`.`saveworkdetail` " +
                                            " (`WorkId`," +
                                            " `MId`," +
                                            " `EValue`," +
                                            " `TPlus`," +
                                            " `TMinus`," +
                                            " `Value`," +
                                            " `Remarks`," +
                                            " `Obj1`," +
                                            " `Obj2`," +
                                            " `TexObj`,Report,result" +
                                            " ) " +
                                            " VALUES" +
                                            " ('"+com.PWorkId+"'," +
                                            " '"+MId+"'," +
                                            " '"+ grd2.Rows[i].Cells[(int)gcol.EValue].Value.ToString()+"'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.TPlus].Value.ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.TMinus].Value.ToString() + "'," +
                                            " '" + com.Val(grd2.Rows[i].Cells[(int)gcol.Value].Value).ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.Remarks].Value.ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.TextObj].Value.ToString() + "'," +
                                            " '" + Report.ToString() + "'," +
                                            " '" + grd2.Rows[i].Cells[(int)gcol.CmbResult].Value.ToString() + "'" +
                                            " )";

                            com.sqlcn.Exec(iqry);


                        }

                        ///



                    }
                }
                else if (com.CurrentWork == "IM")
                {
                    SaveFileDialog sd = new SaveFileDialog();
                    sd.Filter = "Bitmap Image (.bmp)|*.bmp|JPEG Image (.jpg)|*.jpg|Png Image (.png)|*.png";
                    if (sd.ShowDialog() == DialogResult.OK)
                    {
                        imageWM f = GetActiveImageWindow();
                        if (f == null)
                            return;

                       Image<Bgr,Byte> img = new Image<Bgr,Byte>(f.img1.Width,f.img1.Height);
                        Bitmap bmp = new Bitmap(f.img1.ClientSize.Width, f.img1.ClientSize.Height);
                        f.img1.DrawToBitmap(bmp, f.img1.ClientRectangle);
                        bmp.Save(sd.FileName);
                    }
                }
            }
            catch
            {

            }

        }

        private void kryptonRibbonGroupButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageWM f = GetActiveImageWindow();
                    if (f == null)
                        return;

                    Bitmap bmp = new Bitmap(f.img1.ClientSize.Width, f.img1.ClientSize.Height);
                    f.img1.DrawToBitmap(bmp, f.img1.ClientRectangle);
                    bmp.Save(sfd.FileName);
                }
                catch
                {

                }


            }
        }

        private void kryptonRibbonGroupButton5_Click(object sender, EventArgs e)
        {
            if (com.isCalibrationOpen == true)
            {

            }
            else
            {

                calibaration fFormObj = new calibaration();
                fFormObj.TopLevel = false;
                this.Controls.Add(fFormObj);
                fFormObj.Left = this.Width - fFormObj.Width - 500;
                fFormObj.Top = 140;

                fFormObj.Parent = this;
                fFormObj.TopMost = true;
                fFormObj.Show();

                fFormObj.Focus();


                com.isCalibrationOpen = true;


            }
        }

        private void mnucm_Click(object sender, EventArgs e)
        {
            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;

            mnucm.Checked = true;
            com.PubScaleMode = "cm";
            com.PubScaleShort = "cm";
            cmdScale.TextLine1 = com.PubScaleMode;

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();


            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

               // f.objr.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smCentimetres;
               // f.objr1.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smCentimetres;


                f.img1.Invalidate();

            }
            catch
            {

            }
        }

        private void mnumm_Click(object sender, EventArgs e)
        {
            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;

            mnumm.Checked = true;
            com.PubScaleMode = "mm";
            com.PubScaleShort = "mm";
            cmdScale.TextLine1 = com.PubScaleMode;

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();


            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

               // f.objr.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smMillimetres;
               // f.objr1.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smMillimetres;

                f.img1.Invalidate();

            }
            catch
            {

            }
        }

        private void mnuinch_Click(object sender, EventArgs e)
        {

            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;

            mnuinch.Checked = true;
            com.PubScaleMode = "inch";
            com.PubScaleShort = "in";
            cmdScale.TextLine1 = com.PubScaleMode;

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;
               // f.objr.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smInches;
               // f.objr1.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smInches;


                f.img1.Invalidate();

            }
            catch
            {

            }
        }

        private void mnumicron_Click(object sender, EventArgs e)
        {
            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;

            mnumic.Checked = true;
            com.PubScaleMode = "micron";
            com.PubScaleShort = "μm";
            cmdScale.TextLine1 = com.PubScaleMode;

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();

            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;
               // f.objr.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smPoints;
               // f.objr1.ScaleMode = Lyquidity.UtilityLibrary.Controls.enumScaleMode.smPoints;


                f.img1.Invalidate();

            }
            catch
            {

            }

        }

        private void cmbWidth1_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.pwidth = (float)Convert.ToDecimal(cmbWidth1.Text);

            Properties.Settings.Default["pwidth"] = com.pwidth;
            Properties.Settings.Default.Save();

        }

        private void cmdColor1_Click(object sender, EventArgs e)
        {
           
        }

        private void cmdColor1_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            com.pcolor = cmdColor1.SelectedColor;
            Properties.Settings.Default["pcolor"] = com.pcolor;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            com.KPNo = 10;
            com.CPNo = 5;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();

            int i = 0;

            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }

            ApplyTheme();
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            com.KPNo = 3;
            com.CPNo = 3;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            com.KPNo = 6;
            com.CPNo = 4;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();

        }



        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            com.KPNo = 12;
            com.CPNo = 6;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            com.KPNo = 19;
            com.CPNo = 8;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();

            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            com.KPNo = 15;
            com.CPNo = 7;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();
            ApplyTheme();

        }

        private void sparkleBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            com.KPNo = 31;
            com.CPNo = 9;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();
            ApplyTheme();

        }

        private void sparkleOrangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            com.KPNo = 34;
            com.CPNo = 10;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();
            ApplyTheme();

        }

        private void sparklePurpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            com.KPNo = 37;
            com.CPNo = 11;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();
            ApplyTheme();

        }

        private void kryptonRibbonGroupButton6_Click(object sender, EventArgs e)
        {
            try
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                f.MainImg = f.ImgList[f.ImageListPosition - 1];
                f.ImageListPosition = f.ImageListPosition - 1;
                f.LoadImage(null, true);
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message, "");

            }
        }

        private void kryptonRibbonGroupButton4_Click(object sender, EventArgs e)
        {
            try
            {


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;
                f.ImageListPosition = f.ImageListPosition + 1;
                f.MainImg = f.ImgList[f.ImageListPosition];
                f.LoadImage(null, true);
            }
            catch (Exception ex)
            {

            }
        }

        private void MDIParent1_ControlRemoved(object sender, ControlEventArgs e)
        {
           
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {

                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isDrawPoint = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            float Width = 0;
            float Height = 0;

            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            

            Width = Screen.PrimaryScreen.WorkingArea.Width;

            Height = Screen.PrimaryScreen.WorkingArea.Height;

            if ((com.Val(Height) == 0) || (com.Val(Width) == 0))
            {
                return;
            }

            try
            {
               

                int w1 = (int)com.Val(Width);
                int h1 = (int)com.Val(Height);

                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(w1, h1);
                CvInvoke.Resize(f.MainImg, OutImg, new Size(w1, h1), 0, 0, Emgu.CV.CvEnum.Inter.Linear);
                f.MainImg = OutImg;
                f.LoadImage();

            }
            catch (System.NullReferenceException ex)
            {

            }
        }
        private void LoadComponents()
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("select CName from ComponentsHead ");// where CDate between '" + txtFdate.Value.ToString("yyyy-MM-dd") + "' and '" + txtTodate.Value.ToString("yyyy-MM-dd") + "' ");
            cmbComponents.Items.Clear();

            while (rst.Read())
            {
                cmbComponents.Items.Add(rst["CName"]);
            }
            rst.Close();


        }
        private void txtFdate_ValueChanged(object sender, EventArgs e)
        {
           // LoadComponents();
            ImgPanelAdd();
        }

        private void txtTodate_ValueChanged(object sender, EventArgs e)
        {
            //LoadComponents();
            ImgPanelAdd();
        }

        private void LoadJobs()
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("SELECT distinct JobName FROM JobDetail where CId in (select CId from componentshead where CName='"+cmbComponents.Text+"')");
            cmbJobs.Items.Clear();

            while (rst.Read())
            {
                cmbJobs.Items.Add(rst["JobName"]);
            }
            rst.Close();
            txtLoc.Text = "";
            string unit = "";
            rst = com.sqlcn.Getdata("select CLocation,cid,unit from componentshead");
            if (rst.Read())
            {
                string fname = Convert.ToInt64(rst[1]).ToString("000000");
                txtLoc.Text = rst[0].ToString();
                txtLoc.Text = txtLoc.Text + "/" + fname;

                unit = rst[2].ToString();

            }
            rst.Close();



            if (unit == "mm")
            {
                mnumm.PerformClick();
            }
            else if (unit == "cm")
            {
                mnucm.PerformClick();
            }
            else if (unit == "inch")
            {
                mnuinch.PerformClick();
            }
            else if (unit == "micron")
            {
                mnumic.PerformClick();
            }
        }
        private void cmbComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadJobs();
        }

        private void cmdp1_Click(object sender, EventArgs e)
        {
           

            if (grp1.Width == 25)
            {
                grp1.Width = 400;
                cmdp1.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayright;
                // cmdp1.Text = ">";
            }
            else if (grp1.Width == 400)
            {
                cmdp1.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayleft;
                grp1.Width = 25;
               // cmdp1.Text = "<";
            }

            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            Type t = typeof(Form);
            PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
            MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
            f.panel1.Size = new Size(cli.Width - 40, cli.Height - 40);


        }

        private void cmdp2_Click(object sender, EventArgs e)
        {
          
        }

        private void cmdp0_Click(object sender, EventArgs e)
        {

         

            if (grp0.Width == 25)
            {
                cmdp0.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayleft;

                grp0.Width = 160;
               // cmdp0.Text = ">";
            }
            else if (grp0.Width == 160)
            {
                grp0.Width = 25;
                cmdp0.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayright;

                //cmdp0.Text = "<";
            }
            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            Type t = typeof(Form);
            PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
            MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
            f.panel1.Size = new Size(cli.Width - 40, cli.Height - 40);
        }

      
        public enum gcol
        {
            MeasureName, Value, Obj1, Obj2, MeasureType, EValue, TPlus,TMinus,Remarks, Report,BtnExec, BtnEdit,TextObj,CmbResult
        }
        private void grd1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;

            if (grd2.CurrentCell.ColumnIndex == (int)gcol.MeasureName)
            {


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                Random Rnd = new Random();

               

                string obj1, obj2, MType;

                try
                {
                    obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                }
                catch
                {
                    obj1 = "";
                }
                try
                {
                    obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
                }
                catch
                {
                    obj2 = "";
                }
                try
                {
                    MType = grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();
                }
                catch
                {
                    MType = "";
                }



                com.Compute(f.DrawObjects, this, obj1, obj2, f, MType, i, Rnd);


            }

            float Evalue = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.EValue].Value);
            float TMin = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TMinus].Value);
            float TMax = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TPlus].Value);
            float Value = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.Value].Value);

            float Evalue1 = Evalue - TMin;
            float Evalue2 = Evalue + TMax;

            if (Evalue != 0.0)
            {
                if ((Value >= Evalue1) && (Value <= Evalue2))
                {
                    grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "PASS";
                }
                else
                {
                    grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "FAIL";
                }
            }
            else
            {
                grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "";
            }




            for (i = 0; i <= grd2.Rows.Count - 1; i++)
            {

                string obj1, obj2, MType;

                try
                {
                    obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                }
                catch
                {
                    obj1 = "";
                }
                try
                {
                    obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
                }
                catch
                {
                    obj2 = "";
                }
                try
                {
                    MType = grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();
                }
                catch
                {
                    MType = "";
                }


                if (MType == "%PENETRATION")
                {
                    decimal v1 = 0;
                    decimal v2 = 0;

                    try
                    {
                        v1 = Convert.ToDecimal(grd2.Rows[i - 2].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {
                    }

                    try
                    {
                        v2 = Convert.ToDecimal(grd2.Rows[i - 1].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {

                    }
                    decimal p = 0;
                    if ((v1 > 0) && (v2 > 0))
                    {
                        p = Math.Round(v1 / v2 * 100, 2);
                    }

                    grd2.Rows[i].Cells[(int)gcol.Value].Value = p.ToString();
                }
                else
                {
                   // com.Compute(f.DrawObjects, this, obj1, obj2, f, MType, i, Rnd);


                }

                float Evalues1 = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.EValue].Value);
                float TMin1 = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TMinus].Value);
                float TMax1 = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TPlus].Value);
                float Value1 = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.Value].Value);

                float Evalue11 = Evalues1 - TMin1;
                float Evalue21 = Evalues1 + TMax1;

                if (Evalues1 != 0.0)
                {
                    if ((Value >= Evalue11) && (Value <= Evalue21))
                    {
                        grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "PASS";
                    }
                    else
                    {
                        grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "FAIL";
                    }
                }
                else
                {
                    grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "";
                }
            }






            if (grd2.CurrentCell.ColumnIndex == (int)gcol.BtnExec)
            {
                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                com.Length = "";
                com.Remarks = "";

                if (grd2.Rows[grd2.CurrentCell.RowIndex].Cells[(int)gcol.MeasureType].Value.ToString() == "%PENETRATION")
                {
                    decimal v1 = 0;
                    decimal v2 = 0;

                    try
                    {
                        v1= Convert.ToDecimal(grd2.Rows[grd2.CurrentCell.RowIndex-2].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    { 
                    }

                    try
                    {
                        v2 = Convert.ToDecimal(grd2.Rows[grd2.CurrentCell.RowIndex - 1].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    { 

                    }
                    decimal p = 0;
                    if ((v1 > 0) && (v2 > 0))
                    {
                        p = Math.Round(v1 / v2 * 100,2);
                    }

                    grd2.Rows[grd2.CurrentCell.RowIndex].Cells[(int)gcol.Value].Value = p.ToString();

                }
                else
                {

                    string obj1 = grd2.Rows[grd2.CurrentCell.RowIndex].Cells[(int)gcol.Obj1].Value.ToString();
                    string obj2 = grd2.Rows[grd2.CurrentCell.RowIndex].Cells[(int)gcol.Obj2].Value.ToString();
                    string mtype = grd2.Rows[grd2.CurrentCell.RowIndex].Cells[(int)gcol.MeasureType].Value.ToString();

                  
                    Type t = typeof(Form);
                    PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
                    MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
                    f.panel1.Size = new Size(cli.Width - 40, cli.Height - 40);

                    welmet1 f1 = new welmet1(f.DrawObjects,this,f,obj1,obj2, mtype);
                    f1.ShowDialog();
                

                    Type t1 = typeof(Form);
                    PropertyInfo pi1 = t1.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
                    MdiClient cli1 = (MdiClient)pi1.GetValue(f.MdiParent, null);
                    f.panel1.Size = new Size(cli1.Width - 40, cli1.Height - 40);



                    cal1();
                }

               // toolStripMenuItem4.PerformClick();

            }

        }

        private void ApplySettings()
        {
            int i = 0;
            for (i = 0; i <= grd2.Rows.Count - 1; i++)
            {
                if (MySettings.EValue == 1)
                    grd2.Rows[i].Cells[(int)gcol.EValue].ReadOnly = false;
                else
                    grd2.Rows[i].Cells[(int)gcol.EValue].ReadOnly = true;


                if (MySettings.Tol == 1)
                { 
                    grd2.Rows[i].Cells[(int)gcol.TPlus].ReadOnly = false;
                    grd2.Rows[i].Cells[(int)gcol.TMinus].ReadOnly = false;
                }
                else
                {
                    grd2.Rows[i].Cells[(int)gcol.TPlus].ReadOnly = true;
                    grd2.Rows[i].Cells[(int)gcol.TMinus].ReadOnly = true;
                }

              

                grd2.Rows[i].Cells[(int)gcol.Value].ReadOnly = true;

                if (MySettings.Thick == 1)
                {
                    if (grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString() == "THICKNESS")
                        grd2.Rows[i].Cells[(int)gcol.Value].ReadOnly = false;
                   
                }
            }

            com.pwidth = MySettings.OWidth;
            com.PubFontSize = MySettings.FontSize;
            com.mwidth = MySettings.OWidth;

            cmbWidth1.Text = com.pwidth.ToString();
            cmbWidth2.Text = com.mwidth.ToString();

        }

        private void cmbJobs_SelectedIndexChanged(object sender, EventArgs e)
        {

            //txtLoc.Text = "";
            txtJobId.Text = "";
            txtWorkId.Text = "";


            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("SELECT MName,MType,EValue,TPlus,TMinus FROM JobDetail a,measurements b where a.Mid=b.Mid and CId in (select CId from componentshead where CName='"+cmbComponents.Text+"') and jobname='"+cmbJobs.Text+"'");
            grd2.Rows.Clear();

            while (rst.Read())
            {
                grd2.Rows.Add();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.MeasureName].Value = rst["MName"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.MeasureType].Value = rst["MType"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.EValue].Value = rst["EValue"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TPlus].Value = rst["TPlus"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TMinus].Value = rst["TMinus"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Value].Value = "0";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TextObj].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Obj1].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Obj2].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.CmbResult].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Report].Value = true;


            }
            rst.Close();

            txtJobId.Text = cmbComponents.Text + "_" + cmbJobs.Text;
            
            ImgPanelAdd();

            EDControls(true);

            ApplySettings();

        }
        private void ImgPanelAdd()
        {
            if (txtLoc.Text == "")
                return;

            string Location = txtLoc.Text + "/Images/";

            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Location);

            System.IO.FileInfo[] Files = d.GetFiles(); //Getting Text files

            int left = 5;
            int top = 5;



            List<classFileList> flist = new List<classFileList>();

            foreach (System.IO.FileInfo file in Files)
            {
                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);



                string[] x1 = new string[0];
                try
                {
                    x1 = file.Name.Split('_');
                    FileDate = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]));
                    FileDateTime = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]), Convert.ToInt32(x1[8]));

                }
                catch
                {
                    continue;
                }






                if (((FileDateTime.Date >= txtFdate.Value.Date) && (FileDateTime.Date <= txtTodate.Value.Date)) && (cmbJobs.Text == x1[1]) && (cmbComponents.Text == x1[0]))
                {
                    string ext = System.IO.Path.GetExtension(Location + "//" + file.Name);
                    classFileList fl = new classFileList();
                    fl.Name = file.Name;
                    fl.Extension = ext;
                    fl.FileDate = FileDateTime;
                    fl.ComName = x1[0];
                    fl.JobName = x1[1];
                    fl.ItemName = x1[2];

                    flist.Add(fl);
                }

            }

            var flist1 = flist.OrderByDescending(o => o.FileDate);

            //foreach (System.IO.FileInfo file in Files)
            int DisCount = 0;

            imgPanel.Controls.Clear();

            foreach (classFileList file in flist1)
            {


                DisCount = DisCount + 1;
                if (MySettings.NoOfImageDis > 0)
                {
                    if (DisCount > MySettings.NoOfImageDis)
                        goto l;
                }


                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);

                string[] x1 = new string[0];
                try
                {
                    x1 = file.Name.Split('_');
                    //FileDate = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]));
                    //FileDateTime = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]));
                    FileDate = file.FileDate.Date;
                    FileDateTime = file.FileDate;
                }

                catch
                {
                    continue;
                }

                try
                {
                    
                    Krypton.Toolkit.KryptonPanel pp = (Krypton.Toolkit.KryptonPanel)imgPanel.Controls[imgPanel.Controls.Count - 1];
                    top = pp.Top + pp.Height + 5;
                    
                }
                catch
                {
                    top = 5;
                }




                if (((FileDateTime.Date >= txtFdate.Value.Date) && (FileDateTime.Date <= txtTodate.Value.Date)) && (cmbJobs.Text==x1[1]) && (cmbComponents.Text == x1[0]))
                {
                    Krypton.Toolkit.KryptonPanel impanel = new Krypton.Toolkit.KryptonPanel();
                    impanel.Tag = Location + file.Name;
                    impanel.Click += P1_Click;
                    impanel.Width = 200;
                    impanel.Height = 50;
                    impanel.Left = left;
                    impanel.Top = top;
                    impanel.Cursor = Cursors.Hand;

                    PictureBox p1 = new PictureBox();
                    using (System.IO.FileStream fs = new System.IO.FileStream(Location + file.Name, System.IO.FileMode.Open))
                    {
                        p1.Image = Image.FromStream(fs);
                        fs.Close();
                    }


                    p1.SizeMode = PictureBoxSizeMode.StretchImage;
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Tag = Location + file.Name;
                    p1.Click += Pi1_Click;
                    p1.Width = 50;
                    p1.Height = 50;
                    p1.Left = 5;
                    p1.Top = 5;
                    impanel.PaletteMode = com.kp;
                    //p1.Cursor = Cursors.Hand;
                    impanel.Controls.Add(p1);

                    Krypton.Toolkit.KryptonLabel lbl = new Krypton.Toolkit.KryptonLabel();
                    lbl.Text = x1[1] + "_" + x1[2] + "\n" + FileDateTime.ToString("dd-MM-yyyy hh:mm tt");
                    lbl.Left = 60;
                    lbl.Top = 5;
                    impanel.Controls.Add(lbl);
                    imgPanel.Controls.Add(impanel);
                }

            }
        l:;

            ImgWorkPanelAdd();
        }

        

        private void ImgWorkPanelAdd()
        {
            string Location = txtLoc.Text + "/Working/";
            string LocationImg = txtLoc.Text + "/Images/";

            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Location);
            System.IO.FileInfo[] Files = d.GetFiles(); //Getting Text files

            int left = 5;
            int top = 5;


            List<classFileList> flist = new List<classFileList>();

            foreach (System.IO.FileInfo file in Files)
            {

                string ext1=System.IO.Path.GetExtension(txtLoc.Text + "/Working/" + file.Name);
                if (ext1 != ".wk")
                    continue;

                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);



                string[] x1 = new string[0];
                try
                {
                    x1 = file.Name.Replace(".wk","").Split('_');
                    FileDate = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]));
                    FileDateTime = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]), Convert.ToInt32(x1[8]));

                }
                catch
                {
                    continue;
                }






                if (((FileDateTime.Date >= txtFdate.Value.Date) && (FileDateTime.Date <= txtTodate.Value.Date)) && (cmbJobs.Text == x1[1]) && (cmbComponents.Text == x1[0]))
                {
                    string ext = System.IO.Path.GetExtension(Location + "//" + file.Name);

                    classFileList fl = new classFileList();
                    fl.Name = file.Name;
                    fl.Extension = ext;
                    fl.FileDate = FileDateTime;
                    fl.ComName = x1[0];
                    fl.JobName = x1[1];
                    fl.ItemName = x1[2];
                    flist.Add(fl);
                }

            }

            var flist1 = flist.OrderByDescending(o => o.FileDate);

            //foreach (System.IO.FileInfo file in Files)
            int DisCount = 0;



            imgWorkPanel.Controls.Clear();


            foreach (classFileList file in flist1)
            {


                DisCount = DisCount + 1;
                if (MySettings.NoOfImageDis > 0)
                {
                    if (DisCount > MySettings.NoOfImageDis)
                        goto l;
                }


                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);

                string[] x1 = new string[0];
                try
                {
                    x1 = file.Name.Split('_');
                    //FileDate = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]));
                    //FileDateTime = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]));
                    FileDate = file.FileDate.Date;
                    FileDateTime = file.FileDate;
                }

                catch
                {

                }



                try
                {
                    Krypton.Toolkit.KryptonPanel pp = (Krypton.Toolkit.KryptonPanel)imgWorkPanel.Controls[imgWorkPanel.Controls.Count - 1];
                    top = pp.Top + pp.Height + 5;
                }
                catch
                {
                    top = 5;
                }



                string fname1 = file.Name.Replace(file.Extension, "");

                string[] xx1 = fname1.Split('_');

                string ifname1 = xx1[0] + "_" + xx1[1] + "_x_" + xx1[3] + "_" + xx1[4] + "_" + xx1[5] + "_" + xx1[6] +"_"+ xx1[7] + "_" + xx1[8] + "_" + xx1[9];



                if (((FileDateTime.Date >= txtFdate.Value.Date) && (FileDateTime.Date <= txtTodate.Value.Date)) && (cmbJobs.Text == x1[1]) && (cmbComponents.Text == x1[0]))
                {

                    Krypton.Toolkit.KryptonPanel impanel = new Krypton.Toolkit.KryptonPanel();
                    impanel.Tag = LocationImg + ifname1 + "\n" + Location +"\\"+fname1+".wk";
                    impanel.Click += P1Work_Click;
                    impanel.Width = 200;
                    impanel.Height = 50;
                    impanel.Left = left;
                    impanel.Top = top;
                    impanel.Cursor = Cursors.Hand;
                    impanel.PaletteMode = com.kp;
                    PictureBox p1 = new PictureBox();
                    if (System.IO.File.Exists(LocationImg + ifname1) == false)
                    {
                        MessageBox.Show("Image File Not Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    using (System.IO.FileStream fs = new System.IO.FileStream(LocationImg + ifname1, System.IO.FileMode.Open))
                    {
                        p1.Image = Image.FromStream(fs);
                        fs.Close();
                    }

                    p1.SizeMode = PictureBoxSizeMode.StretchImage;
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Tag = LocationImg + ifname1 + "\n" + Location + "\\" + fname1 + ".wk";
                    p1.Click += Pi1work_Click;
                    p1.Width = 50;
                    p1.Height = 50;
                    p1.Left = 5;
                    p1.Top = 5;
                    //p1.Cursor = Cursors.Hand;

                    impanel.Controls.Add(p1);

                    Krypton.Toolkit.KryptonLabel lbl = new Krypton.Toolkit.KryptonLabel();
                    lbl.Text = x1[1] + "_" + x1[2] + "\n" + FileDateTime.ToString("dd-MM-yyyy hh:mm tt");
                    lbl.Left = 60;
                    lbl.Top = 5;
                    impanel.Controls.Add(lbl);


                    imgWorkPanel.Controls.Add(impanel);
                }
            l:;

            }

        }
        private void P1Work_Click(object sender, EventArgs e)
        {

            imageWM f1 = GetActiveImageWindow();

            if (f1 != null)
            {
                if (MessageBox.Show("Do you want to Close All Opened Windows?", com.MsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    foreach (imageWM fff in this.MdiChildren)
                    {
                        fff.Close();
                    }
                }
                else
                    return;
            }


            com.CurrentWork = "WM";

            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)sender;
            PictureBox p = (PictureBox)impanel.Controls[0];

            string[] locary = p.Tag.ToString().Split('\n');

            string loc = System.IO.Path.GetDirectoryName(locary[0]);
            string fname1 = System.IO.Path.GetFileName(locary[0]);

            string[] xx1 = fname1.Split('_');

            string ifname1 = xx1[0] + "_" + xx1[1] + "_x_" + xx1[3] + "_" + xx1[4] + "_" + xx1[5] + "_" + xx1[6] + "_" + xx1[7] + "_" + xx1[8] + "_" + xx1[9];



            if (System.IO.File.Exists(loc + "\\" +ifname1) == false)
            {
                MessageBox.Show("File Not Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


           

            imageWM f = new imageWM(loc + "\\" + ifname1);
            string imglocation = loc;
            string Locationwk = locary[1];

            if (System.IO.File.Exists(Locationwk) == false)
            {
                MessageBox.Show("Working File Not Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string fullobjtxt = System.IO.File.ReadAllText(Locationwk);

            fullobjtxt = StringCipher.Decrypt(fullobjtxt, "$123?_");
            string[] fullobj = fullobjtxt.Split(new string[] { "$$" }, StringSplitOptions.None);

            txtWorkId.Text = fullobj[2];

              int MaxLineNumber = Convert.ToInt32(fullobj[3]);
              int MaxCircleNumber = Convert.ToInt32(fullobj[4]);
              int MaxRectNumber = Convert.ToInt32(fullobj[5]);
              int MaxArrowNumber = Convert.ToInt32(fullobj[6]);
              int MaxPointNumber = Convert.ToInt32(fullobj[7]);
              int MaxCurveNumber = Convert.ToInt32(fullobj[8]);
              int MaxAngleNumber = Convert.ToInt32(fullobj[9]);
              int MaxIPointNumber = Convert.ToInt32(fullobj[10]);
              int MaxArcNumber = Convert.ToInt32(fullobj[11]);

            int i;
                f.DrawObjects.Clear();
                f.MaxLineNumber = MaxLineNumber;
                f.MaxCircleNumber = MaxCircleNumber;
                f.MaxRectNumber = MaxRectNumber;
                f.MaxArrowNumber = MaxArrowNumber;
                f.MaxPointNumber = MaxPointNumber;
                f.MaxCurveNumber = MaxCurveNumber;
                f.MaxAngleNumber = MaxAngleNumber;
                f.MaxIPointNumber = MaxIPointNumber;
                f.MaxArcNumber = MaxArcNumber;

            f.DrawObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Objects.DrawObject>>(fullobj[0]);


            for (i = 0; i <= f.DrawObjects.Count - 1; i++)
            {
                f.DrawObjects[i].Obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Objects.cLine>(f.DrawObjects[i].Obj.ToString());
            }


            grd2.Rows.Clear();

            string TableValue = fullobj[1];
            string[] Rows = TableValue.Split(new string[] { "|#|" }, StringSplitOptions.None);

            for (i = 0; i <= Rows.Length - 1; i++)
            {
                string[] Cols = Rows[i].Split(new string[] { "|$|" }, StringSplitOptions.None);
                grd2.Rows.Add();
                grd2.Rows[i].Cells[(int)gcol.MeasureName].Value = Cols[0];
                grd2.Rows[i].Cells[(int)gcol.MeasureType].Value = Cols[1];
                grd2.Rows[i].Cells[(int)gcol.EValue].Value = Cols[2];
                grd2.Rows[i].Cells[(int)gcol.TPlus].Value = Cols[3];
                grd2.Rows[i].Cells[(int)gcol.TMinus].Value = Cols[4];
                grd2.Rows[i].Cells[(int)gcol.Value].Value = Cols[5];
                grd2.Rows[i].Cells[(int)gcol.TextObj].Value = Cols[6];
                grd2.Rows[i].Cells[(int)gcol.Obj1].Value = Cols[7];
                grd2.Rows[i].Cells[(int)gcol.Obj2].Value = Cols[8];
                grd2.Rows[i].Cells[(int)gcol.Remarks].Value = Cols[9];
                grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = Cols[10];

                if (Cols[10] == "1")
                    grd2.Rows[i].Cells[(int)gcol.Report].Value = true;
                else
                    grd2.Rows[i].Cells[(int)gcol.Report].Value = false;

            }


            TZoom.Value = 100;
            trackBar1.Value = 100;
            trackBar2.Value = 0;

            this.trackHue.Value = 100;
            this.trackSat.Value = 100;
            this.trackLight.Value = 100;


            f.MdiParent = this;
            f.Show();
            f.WindowState = FormWindowState.Normal;
            f.WindowState = FormWindowState.Maximized;

            ApplySettings();
        }

        private void Pi1_Click(object sender, EventArgs e)
        {
            PictureBox px = (PictureBox)sender;
            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)px.Parent;
            P1_Click(impanel, e);

        }


        private void Pi1IM_Click(object sender, EventArgs e)
        {
            PictureBox px = (PictureBox)sender;
            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)px.Parent;
            P1IM_Click(impanel, e);

        }
        private void Pi1work_Click(object sender, EventArgs e)
        {
            PictureBox px = (PictureBox)sender;
            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)px.Parent;
            P1Work_Click(impanel, e);

        }



        private void P1IM_Click(object sender, EventArgs e)
        {

            imageWM f1 = GetActiveImageWindow();

            if (f1 != null)
            {
                if (MessageBox.Show("Do you want to Close All Opened Windows?", com.MsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    foreach (imageWM fff in this.MdiChildren)
                    {
                        fff.Close();
                    }
                }
                else
                    return;
            }

            com.CurrentWork = "IM";

            txtWorkId.Text = "";


           


            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)sender;


            PictureBox p = (PictureBox)impanel.Controls[0];

            if (System.IO.File.Exists(p.Tag.ToString()) == false)
            {
                MessageBox.Show("File Not Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            TZoom.Value = 100;
            trackBar1.Value = 100;
            trackBar2.Value = 0;

            this.trackHue.Value = 100;
            this.trackSat.Value = 100;
            this.trackLight.Value = 100;

            imageWM f = new imageWM(p.Tag.ToString());
            f.MdiParent = this;
            f.Show();
            f.WindowState = FormWindowState.Normal;
            f.WindowState = FormWindowState.Maximized;

        }

        private void P1_Click(object sender, EventArgs e)
        {

            imageWM f1 = GetActiveImageWindow();

            if (f1 != null)
            {
                if (MessageBox.Show("Do you want to Close All Opened Windows?", com.MsgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    foreach (imageWM fff in this.MdiChildren)
                    {
                        fff.Close();
                    }
                }
                else
                    return;
            }

            com.CurrentWork = "WM";

            txtWorkId.Text = "";


            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("SELECT MName,MType,EValue,TPlus,TMinus FROM JobDetail a,measurements b where a.Mid=b.Mid and CId in (select CId from componentshead where CName='" + cmbComponents.Text + "') and jobname='" + cmbJobs.Text + "'");
            grd2.Rows.Clear();

            while (rst.Read())
            {
                grd2.Rows.Add();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.MeasureName].Value = rst["MName"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.MeasureType].Value = rst["MType"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.EValue].Value = rst["EValue"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TPlus].Value = rst["TPlus"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TMinus].Value = rst["TMinus"].ToString();
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Value].Value = "0";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.TextObj].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Obj1].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Obj2].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.CmbResult].Value = "";
                grd2.Rows[grd2.Rows.Count - 1].Cells[(int)gcol.Report].Value = true;
            }
            rst.Close();


            Krypton.Toolkit.KryptonPanel impanel = (Krypton.Toolkit.KryptonPanel)sender;


            PictureBox p = (PictureBox)impanel.Controls[0];

            if (System.IO.File.Exists(p.Tag.ToString()) == false)
            { 
                MessageBox.Show("File Not Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            TZoom.Value = 100;
            trackBar1.Value = 100;
            trackBar2.Value = 0;

            this.trackHue.Value = 100;
            this.trackSat.Value = 100;
            this.trackLight.Value = 100;


            imageWM f = new imageWM(p.Tag.ToString());
            f.MdiParent = this;
            f.Show();
            f.WindowState = FormWindowState.Normal;
            f.WindowState = FormWindowState.Maximized;

            ApplySettings();

        }
        private void kryptonRibbonGroupButton1_Click_2(object sender, EventArgs e)
        {
            com.KPNo = 19;
            com.CPNo = 8;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();

            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }


            ApplyTheme();
        }

        private void kryptonRibbonGroupButton2_Click_1(object sender, EventArgs e)
        {
            com.KPNo = 12;
            com.CPNo = 6;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();

        }

        private void kryptonRibbonGroupButton3_Click_1(object sender, EventArgs e)
        {
            com.KPNo = 15;
            com.CPNo = 7;

            com.kp = (Krypton.Toolkit.PaletteMode)com.KPNo;
            com.cp = (ComponentFactory.Krypton.Toolkit.PaletteMode)com.CPNo;

            Properties.Settings.Default["KPNo"] = com.KPNo;
            Properties.Settings.Default["CPNo"] = com.CPNo;
            Properties.Settings.Default.Save();
            int i = 0;
            for (i = 0; i <= this.MdiChildren.Length - 1; i++)
            {
                this.MdiChildren[i].Close();
            }
            ApplyTheme();
           
        }

        private void cmbmag_SelectedIndexChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default["CMag"] = cmbmag.Text.Replace('x', ' ');
            Properties.Settings.Default.Save();
            com.PubCMag = Convert.ToInt32(cmbmag.Text.Replace('x', ' '));


            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;
            f.img1.Invalidate();


        }

        private void txtWorkId_TextChanged(object sender, EventArgs e)
        {

        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {

            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            //int i = 0;
            //for (i = 0; i <= grd2.Rows.Count - 1; i++)
            //{
            //    string obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
            //    string obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
            //    string MType = grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();


            //    if (MType == "%PENETRATION")
            //    {
            //        decimal v1 = 0;
            //        decimal v2 = 0;

            //        try
            //        {
            //            v1 = Convert.ToDecimal(grd2.Rows[i - 2].Cells[(int)gcol.Value].Value);
            //        }
            //        catch
            //        {
            //        }

            //        try
            //        {
            //            v2 = Convert.ToDecimal(grd2.Rows[i - 1].Cells[(int)gcol.Value].Value);
            //        }
            //        catch
            //        {

            //        }
            //        decimal p = 0;
            //        if ((v1 > 0) && (v2 > 0))
            //        {
            //            p = Math.Round(v1 / v2 * 100, 2);
            //        }

            //        grd2.Rows[i].Cells[(int)gcol.Value].Value = p.ToString();
            //    }
            //    else
            //    {
            //        com.Compute(f.DrawObjects, this, obj1, obj2, f, MType,i);
            //    }

            //}



        }

        private void cal1()
        {
            int i = 0;
            for (i = 0; i <= grd2.Rows.Count - 1; i++)
            {
                string obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                string obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
                string MType = grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();


                if (MType == "%PENETRATION")
                {
                    decimal v1 = 0;
                    decimal v2 = 0;

                    try
                    {
                        v1 = Convert.ToDecimal(grd2.Rows[i - 2].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {
                    }

                    try
                    {
                        v2 = Convert.ToDecimal(grd2.Rows[i - 1].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {

                    }
                    decimal p = 0;
                    if ((v1 > 0) && (v2 > 0))
                    {
                        p = Math.Round(v1 / v2 * 100, 2);
                    }

                    grd2.Rows[i].Cells[(int)gcol.Value].Value = p.ToString();
                }
                else
                {
                  
                }

            }
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
          

            imageWM f = GetActiveImageWindow();
           
            if (f == null)
                return;

            Random Rnd = new Random();

            int i = 0;


            this.Cursor = Cursors.WaitCursor;

            for (i = 0; i <= grd2.Rows.Count - 1; i++)
            {

                string obj1, obj2, MType;

                try
                {
                    obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                }
                catch
                {
                    obj1 = "";
                }
                try
                {
                    obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();
                }
                catch
                {
                    obj2 = "";
                }
                try {
                    MType = grd2.Rows[i].Cells[(int)gcol.MeasureType].Value.ToString();
                }
                catch {
                    MType = "";
                }


                if (MType == "%PENETRATION")
                {
                    decimal v1 = 0;
                    decimal v2 = 0;

                    try
                    {
                        v1 = Convert.ToDecimal(grd2.Rows[i - 2].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {
                    }

                    try
                    {
                        v2 = Convert.ToDecimal(grd2.Rows[i - 1].Cells[(int)gcol.Value].Value);
                    }
                    catch
                    {

                    }
                    decimal p = 0;
                    if ((v1 > 0) && (v2 > 0))
                    {
                        p = Math.Round(v1 / v2 * 100, 2);
                    }

                    grd2.Rows[i].Cells[(int)gcol.Value].Value = p.ToString();
                }
                else
                {
                    com.Compute(f.DrawObjects, this, obj1, obj2, f, MType, i, Rnd);


                }

                float Evalue = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.EValue].Value);
                float TMin = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TMinus].Value);
                float TMax = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.TPlus].Value);
                float Value = (float)com.Val(grd2.Rows[i].Cells[(int)gcol.Value].Value);

                float Evalue1 = Evalue - TMin;
                float Evalue2 = Evalue + TMax;

                if (Evalue != 0.0)
                {
                    if ((Value >= Evalue1) && (Value <= Evalue2))
                    { grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "PASS"; 
                    }
                    else
                    {
                        grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "FAIL";
                    }
                }
                else
                {
                    grd2.Rows[i].Cells[(int)gcol.CmbResult].Value = "";
                }
            }

            this.Cursor = Cursors.Default; 

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
           string Location=txtLoc.Text + "//Measurements/M.mes";
           string MText = "";

            int i = 0;
            for (i = 0; i <= grd2.Rows.Count - 1; i++)
            {
                string MName = grd2.Rows[i].Cells[(int)gcol.MeasureName].Value.ToString();
                string obj1 = grd2.Rows[i].Cells[(int)gcol.Obj1].Value.ToString();
                string obj2 = grd2.Rows[i].Cells[(int)gcol.Obj2].Value.ToString();

                string C = MName + "#" + obj1 + "#" + obj2;
                if (MText == "")
                    MText = C;
                else
                    MText = MText + "$" + C;

            }

            System.IO.File.WriteAllText(Location, MText);

            MessageBox.Show("Measurements Saved Successfully", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

            string Location = txtLoc.Text + "//Measurements/M.mes";
            if (System.IO.File.Exists(Location) == false)
            {
                MessageBox.Show("No Saved Measurements Found", com.MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           

            string fullobjtxt = System.IO.File.ReadAllText(Location);

            //fullobjtxt = StringCipher.Decrypt(fullobjtxt, "$123?_");

            string[] fullobj = fullobjtxt.Split(new string[] { "$" }, StringSplitOptions.None);

            int i = 0;

            for (i = 0; i<=fullobj.Length - 1; i++)
            {
                string[] c = fullobj[i].Split('#');
                string MName = c[0];
                string obj1 = c[1];
                string obj2 = c[2];

                int j = 0;
                for (j = 0; j <= grd2.Rows.Count - 1; j++)
                {
                    if (grd2.Rows[j].Cells[(int)gcol.MeasureName].Value.ToString() == MName)
                    {
                        grd2.Rows[j].Cells[(int)gcol.Obj1].Value = obj1;
                        grd2.Rows[j].Cells[(int)gcol.Obj2].Value = obj2;
                        goto l1;
                    }
                }
            l1:;
            }

            toolStripMenuItem4.PerformClick();


        }


        private void chkscaleshow_CheckStateChanged1(object sender, EventArgs e)
        {
        }

            private void chkscaleshow_CheckStateChanged(object sender, EventArgs e)
        {
            imageWM f1 = GetActiveImageWindow();

            if (f1 == null)
                return;

            f1.objr.Visible = chkscaleshow.Checked;
            f1.objr1.Visible = chkscaleshow.Checked;

            if (chkscaleshow.Checked == true)
            {
                f1.panel1.Left = 30;
                f1.panel1.Top =30;

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                Type t = typeof(Form);
                PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
                MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
                f.panel1.Size = new Size(cli.Width - 40, cli.Height - 40);

            }
            else
            {
                f1.panel1.Left = 1;
                f1.panel1.Top = 1;


                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;

                Type t = typeof(Form);
                PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
                MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
                f.panel1.Size = new Size(cli.Width - 4, cli.Height - 4);

            }

           
        }

        private void Navi1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (Navi1.SelectedIndex == 2)
            {
                cmdUndo.Enabled = false;
                cmdRedo.Enabled = false;
            }
            else
            {
                cmdUndo.Enabled = true;
                cmdRedo.Enabled = true;
            }

        }

        private void multipleWorkReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMultireport f = new frmMultireport();
            f.ShowDialog();
        }
        private void saveWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdSave.PerformClick();

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            frmReport f1 = new frmReport(com.PWorkId, f);
            f1.ShowDialog();

        }

        private void kryptonDropButton1_Click(object sender, EventArgs e)
        {


            //Krypton.Toolkit.KryptonDropButton btnSender = (Krypton.Toolkit.KryptonDropButton)sender;
            //Point ptLowerLeft = new Point(0, btnSender.Height);
            //ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            //contextMenuStrip3.Show(ptLowerLeft);

            toolStripMenuItem4_Click(sender, e);


        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (grp4.Width == 25)
            {
                grp4.Width = 250;
                cmdp4.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayright;
                // cmdp1.Text = ">";
            }
            else if (grp4.Width == 250)
            {
                cmdp4.StateCommon.Back.Image = global::ImgProcess.Properties.Resources.grayleft;
                grp4.Width = 25;
                // cmdp1.Text = "<";
            }

            imageWM f = GetActiveImageWindow();

            if (f == null)
                return;

            Type t = typeof(Form);
            PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
            MdiClient cli = (MdiClient)pi.GetValue(f.MdiParent, null);
            f.panel1.Size = new Size(cli.Width - 40, cli.Height - 40);

        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtIMLocation.Text = fd.SelectedPath;
                Properties.Settings.Default.IMLocation = txtIMLocation.Text;
                Properties.Settings.Default.Save();
                LoadIMImages();
            }

        }

        public class classFileList
        {
            public string Name {get;set;}

            public DateTime FileDate { get; set; }

            public string Extension { get; set; }

            public string ComName { get; set; }
            public string JobName { get; set; }
            public string ItemName { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadIMImages()
        {
            if (txtIMLocation.Text == "")
                return;

            string Location = txtIMLocation.Text;

            System.IO.DirectoryInfo d = new System.IO.DirectoryInfo(Location);

            System.IO.FileInfo[] Files = d.GetFiles(); //Getting Text files

            int left = 5;
            int top = 5;


            imgPanelIM.Controls.Clear();

            List<classFileList> flist = new List<classFileList>();

            foreach (System.IO.FileInfo file in Files)
            {
                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);

               
                // Code Modified by R.Alagaraja
                string[] x1 = new string[0];
                try
                {                
                    x1 = file.Name.Split('_');
                    FileDate = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]));
                    FileDateTime = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]), Convert.ToInt32(x1[8]));

                }
                catch
                {
                    continue;
                }

                if ((FileDateTime.Date >= txtIMFDate.Value.Date) && (FileDateTime.Date <= txtIMToDate.Value.Date))
                {
                    string ext = System.IO.Path.GetExtension(Location + "//" + file.Name);

                    classFileList fl = new classFileList();
                    fl.Name = file.Name;
                    fl.FileDate = FileDateTime;
                    fl.Extension = ext;
                    flist.Add(fl);
                  }

            }

             var flist1= flist.OrderByDescending(o => o.FileDate);

            //foreach (System.IO.FileInfo file in Files)
            int DisCount = 0;

            foreach (classFileList file in flist1)
            {
                DisCount = DisCount + 1;
                if (MySettings.NoOfImageDis > 0)
                {
                    if (DisCount > MySettings.NoOfImageDis)
                        goto l;
                }


                DateTime FileDate = new DateTime(1900, 01, 01);
                DateTime FileDateTime = new DateTime(1900, 01, 01);

                string[] x1 = new string[0];
                try
                {
                    x1 = file.Name.Split('_');
                    //FileDate = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]));
                    //FileDateTime = new DateTime(Convert.ToInt32(x1[2]), Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]));
                    FileDate = file.FileDate.Date;
                    FileDateTime = file.FileDate;
                }
                catch
                {
                    continue;
                }

                try
                {

                    Krypton.Toolkit.KryptonPanel pp = (Krypton.Toolkit.KryptonPanel)imgPanelIM.Controls[imgPanelIM.Controls.Count - 1];
                    top = pp.Top + pp.Height + 5;

                }
                catch
                {
                    top = 5;
                }




                if ((FileDateTime.Date >= txtIMFDate.Value.Date) && (FileDateTime.Date <= txtIMToDate.Value.Date))
                {
                    Krypton.Toolkit.KryptonPanel impanel = new Krypton.Toolkit.KryptonPanel();
                    impanel.Tag = Location + "\\" + file.Name;
                    impanel.Click += P1IM_Click;
                    impanel.Width = 200;
                    impanel.Height = 50;
                    impanel.Left = left;
                    impanel.Top = top;
                    impanel.Cursor = Cursors.Hand;

                    PictureBox p1 = new PictureBox();
                    using (System.IO.FileStream fs = new System.IO.FileStream(Location + "\\" + file.Name, System.IO.FileMode.Open))
                    {
                        p1.Image = Image.FromStream(fs);
                        fs.Close();
                    }


                    p1.SizeMode = PictureBoxSizeMode.StretchImage;
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Tag = Location + "\\" + file.Name;
                    p1.Click += Pi1IM_Click;
                    p1.Width = 50;
                    p1.Height = 50;
                    p1.Left = 5;
                    p1.Top = 5;
                    impanel.PaletteMode = com.kp;
                    //p1.Cursor = Cursors.Hand;
                    impanel.Controls.Add(p1);

                    Krypton.Toolkit.KryptonLabel lbl = new Krypton.Toolkit.KryptonLabel();
                    lbl.Text = x1[1] + "\n" + FileDateTime.ToString("dd-MM-yyyy hh:mm tt");
                    lbl.Left = 60;
                    lbl.Top = 5;
                    impanel.Controls.Add(lbl);
                    imgPanelIM.Controls.Add(impanel);
                }

             
            }
        l:;

           // ImgWorkPanelAdd();
        }

        private void kryptonButton5_Click_1(object sender, EventArgs e)
        {
            LoadIMImages();
        }




        private void txtIMLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void MDIParent1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                errlbl.Text = "";

                try
                {
                    imageWM f = GetActiveImageWindow();
                    if (f == null)
                        return;

                    f.clear();


                }
                catch
                {

                }
            }
        }

        private void cmbmag1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default["CMag"] = cmbmag1.Text.Replace('x', ' ');
            Properties.Settings.Default.Save();
            com.PubCMag = Convert.ToInt32(cmbmag1.Text.Replace('x', ' '));

            imageWM f = GetActiveImageWindow();
            if (f == null)
                return;

            f.img1.Invalidate();


        }


        // Code Added by ARaja
        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            try
            {
                string modelName = cls_EnvisionConfig.ReadDetailsFromXML(Path.GetDirectoryName(Application.ExecutablePath), @"/configurations/model");

                

                if (modelName == "IDSPeak uEye- Old Version") {

                    frm_uEyeInit frmCamera = new frm_uEyeInit();
                    frmCamera._isWeldMet = com.WMLic;
                    frmCamera._loadRefreshimages += LoadRefreshImages;

                    if (com.IMLic)
                    {
                        if (txtIMLocation.Text == string.Empty)
                        {
                            MessageBox.Show("Image Location is not configured. Initiating Image Location !!!");
                            FolderBrowserDialog fd = new FolderBrowserDialog();

                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                txtIMLocation.Text = fd.SelectedPath;
                                Properties.Settings.Default.IMLocation = txtIMLocation.Text;
                                Properties.Settings.Default.Save();
                            }
                        }
                        frmCamera._imageLocation = txtIMLocation.Text;
                    }
                    else
                    {
                        if (cmbComponents.Text == string.Empty || cmbJobs.Text == string.Empty)
                        {
                            MessageBox.Show("Please select component/Job to proceed capturing images");
                            return;
                        }
                        frmCamera._jobID = txtJobId.Text;
                        frmCamera._imageLocation = txtLoc.Text;
                    }
                    frmCamera.ShowDialog();

                }
                else if (modelName == "IDSPeak uEye- New Version")
                {

                    frm_uEyeInitNew frmCamera = new frm_uEyeInitNew();
                    frmCamera._isWeldMet = com.WMLic;
                    frmCamera._loadRefreshimages += LoadRefreshImages;

                    if (com.IMLic)
                    {
                        if (txtIMLocation.Text == string.Empty)
                        {
                            MessageBox.Show("Image Location is not configured. Initiating Image Location !!!");
                            FolderBrowserDialog fd = new FolderBrowserDialog();

                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                txtIMLocation.Text = fd.SelectedPath;
                                Properties.Settings.Default.IMLocation = txtIMLocation.Text;
                                Properties.Settings.Default.Save();
                            }
                        }
                        frmCamera._imageLocation = txtIMLocation.Text;
                    }
                    else
                    {
                        if (cmbComponents.Text == string.Empty || cmbJobs.Text == string.Empty)
                        {
                            MessageBox.Show("Please select component/Job to proceed capturing images");
                            return;
                        }
                        frmCamera._jobID = txtJobId.Text;
                        frmCamera._imageLocation = txtLoc.Text;
                    }
                    frmCamera.ShowDialog();

                }
                else
                {
                    // Commented by ARaja 14102022
                    frm_CameraInt frmCamera = new frm_CameraInt();
                    frmCamera._isWeldMet = com.WMLic;
                    frmCamera._loadRefreshimages += LoadRefreshImages;

                    if (com.IMLic)
                    {
                        if (txtIMLocation.Text == string.Empty)
                        {
                            MessageBox.Show("Image Location is not configured. Initiating Image Location !!!");
                            FolderBrowserDialog fd = new FolderBrowserDialog();

                            if (fd.ShowDialog() == DialogResult.OK)
                            {
                                txtIMLocation.Text = fd.SelectedPath;
                                Properties.Settings.Default.IMLocation = txtIMLocation.Text;
                                Properties.Settings.Default.Save();
                            }
                        }
                        frmCamera._imageLocation = txtIMLocation.Text;
                    }
                    else
                    {
                        if (cmbComponents.Text == string.Empty || cmbJobs.Text == string.Empty)
                        {
                            MessageBox.Show("Please select component/Job to proceed capturing images");
                            return;
                        }
                        frmCamera._jobID = txtJobId.Text;
                        frmCamera._imageLocation = txtLoc.Text;
                    }
                    frmCamera.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Loading Camera Sceen", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (com.IMLic)
                {
                    LoadIMImages();
                }
                else
                {
                    ImgPanelAdd();
                }
            }
        }

        // Code Added by ARaja
        private void LoadRefreshImages()
        {
            if (com.IMLic)
            {
                LoadIMImages();
            }
            else
            {
                ImgPanelAdd();
            }
        }

        private void cmdColor2_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            com.mcolor = cmdColor2.SelectedColor;
            Properties.Settings.Default["mcolor"] = com.mcolor;
            Properties.Settings.Default.Save();
        }

        private void cmbWidth2_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cmbWidth2_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.mwidth = (float)Convert.ToDecimal(cmbWidth2.Text);

            Properties.Settings.Default["mwidth"] = com.mwidth;
            Properties.Settings.Default.Save();
        }

        private void chkMag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMag2.Checked == true)
            {
                kryptonPanel1.Visible = true;
                kryptonPanel2.Visible = true;
                kryptonNavigator1.Height = 203;
                
            }
            else
            {
                kryptonPanel1.Visible = true;
                kryptonPanel2.Visible = false;
                kryptonNavigator1.Height = 385;
               
            }
        }

        private void grd2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == (int)gcol.Obj1) || (e.ColumnIndex == (int)gcol.Obj2))
            {
              //  toolStripMenuItem4_Click(sender, e);
            }
        }

        private void grp1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkMag2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grd2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            Components ff = new Components();
            ff.ShowDialog();
        }

        private void grp4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonNavigator1_SelectedPageChanged(object sender, EventArgs e)
        {
            kryptonPanel1.Visible = true;
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                if (com.isCalibrationOpen == true)
                {
                    errlbl.Text = "Please Close Calibration Screen";
                    return;
                }

                imageWM f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isMarkArc = true;
                //cmdok.Enabled = true;
            }
            catch
            {

            }
        }

        private void trackBar2_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void trackBar2_RightToLeftChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Code Modified by ARaja 27092022
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kryptonButton3_Click_1(object sender, EventArgs e)
        {
            ufrm_CameraSettings frmCamSettings = new ufrm_CameraSettings();
            frmCamSettings.ShowDialog();
        }
    }
    }
