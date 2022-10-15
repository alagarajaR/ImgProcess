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
using Krypton.Toolkit;

namespace ImgProcess
{
    public partial class MDIParent1_test : KryptonForm
    {
        private int childFormNumber = 0;

        public MDIParent1_test()
        {
            InitializeComponent();
            //SetTheme();
            ButtonPalette1.ResetToDefaults(true);
            ButtonPalette1.BasePaletteMode = PaletteMode.Office365Black;
        }
        public void SetTheme()
        {
            //form
            this.StateCommon.Back.Color1 = Theme.FormBackColor1;
            this.StateCommon.Back.Color1 = Theme.FormBackColor2;
            this.StateCommon.Header.Back.Color1 = Theme.FormHeadColor1;
            this.StateCommon.Header.Back.Color2 = Theme.FormHeadColor2;
            this.StateCommon.Border.Color1 = Theme.FormBorderColor1;
            this.StateCommon.Border.Color1 = Theme.FormBorderColor2;
            this.StateCommon.Header.Content.ShortText.Color1 = Theme.TextColor1;
            this.StateCommon.Header.Content.LongText.Color1 = Theme.TextColor1;

            //button
            ButtonPalette1.ButtonStyles.ButtonCommon.StateCommon.Back.Color1 = Theme.ButtonColor1;
            ButtonPalette1.ButtonStyles.ButtonCommon.StateCommon.Back.Color2 = Theme.ButtonColor2;
            ButtonPalette1.ButtonStyles.ButtonCommon.StateCommon.Content.ShortText.Color1 = Theme.TextColor1;
            ButtonPalette1.ButtonStyles.ButtonCommon.StateCommon.Content.LongText.Color1 = Theme.TextColor1;


            //label
            ButtonPalette1.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = Theme.TextColor2;
            ButtonPalette1.LabelStyles.LabelCommon.StateCommon.LongText.Color1 = Theme.TextColor2;

            //CheckBox
            ButtonPalette1.Common.StateCommon.Content.ShortText.Color1 = Theme.TextColor2;
            ButtonPalette1.Common.StateCommon.Content.LongText.Color1 = Theme.TextColor2;

            //TextBox
            ButtonPalette1.Common.StateCommon.Border.Color1 = Theme.FormHeadColor2;


            //Tracker
            ButtonPalette1.TrackBar.StateCommon.Track.Color1 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Track.Color2 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Track.Color3 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Track.Color4 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Track.Color5 = Theme.FormHeadColor1;

            ButtonPalette1.TrackBar.StateCommon.Position.Color1 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Position.Color2 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Position.Color3 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Position.Color4 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Position.Color5 = Theme.FormHeadColor1;

            ButtonPalette1.TrackBar.StateCommon.Tick.Color1 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Tick.Color2 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Tick.Color3 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Tick.Color4 = Theme.FormHeadColor1;
            ButtonPalette1.TrackBar.StateCommon.Tick.Color5 = Theme.FormHeadColor1;


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
            Application.Exit();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
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


        private void ContrastBrightnessAdjust()
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, byte> imgOutput;
                string CurrentContrast = ((float)trackBar1.Value / 100).ToString();
                imgOutput = f.MainImg.Mul(double.Parse(CurrentContrast)) + trackBar2.Value;

              

                f.LoadImage(imgOutput);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


       


        public string Header = "";
        private void MDIParent1_Load(object sender, EventArgs e)
        {


            com.PubScaleMode = (string)Properties.Settings.Default["Scale"];
            com.PubCMag = (Int32)Convert.ToDouble(Properties.Settings.Default["CMag"]);
            com.pcolor = (System.Drawing.Color)Properties.Settings.Default["pcolor"];
            com.pwidth=(float)Properties.Settings.Default["pwidth"];
            cmbwidth.Text = com.pwidth.ToString();

            GetXYValue();

            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;
            if (com.PubScaleMode == "cm")
            {
                com.PubScaleShort = "cm";
                mnucm.Checked = true;
            }
            else if (com.PubScaleMode == "mm")
            {

                com.PubScaleShort = "mm";
                mnumm.Checked= true;
            }
            else if (com.PubScaleMode == "inch")
            {

                com.PubScaleShort = "in";
                mnuinch.Checked = true;
            }
            else if (com.PubScaleMode == "micron")
            {

                com.PubScaleShort = "μ";
                mnumic.Checked = true;
            }


            Header = this.Text;

            this.tabControl1.SelectedIndex = 1;
            this.tabControl1.SelectedIndex = 0;

            try
            {
               this.cmbmag.Items.Clear();

                for (int i = 50; i <= 200; i = i + 10)
                {
                    cmbmag.Items.Add(i.ToString() + "x");
                }

                cmbmag.Text = "100x";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmLogin f = new frmLogin();
            f.ShowDialog();
        }

        private void OpenFileBtn(object sender, EventArgs e)
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
                Path= Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }


            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.InitialDirectory = Path;
            
            saveFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";


            TZoom.Value = 100;
            trackBar1.Value = 150;
            trackBar2.Value = 50;

            this.trackHue.Value = 100;
            this.trackSat.Value = 100; 
            this.trackLight.Value = 100; 


            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
               
                string FileName = saveFileDialog.FileName;
                Path = System.IO.Path.GetDirectoryName(FileName);
                Properties.Settings.Default["LastPath"] = Path;
                Properties.Settings.Default.Save();

                frmImage f = new frmImage(FileName);
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Normal;
                f.WindowState = FormWindowState.Maximized;

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f != null)
                {
                    f.MainImg = f.MainImg.Rotate(90, new Bgr(Color.Transparent),false);
                    f.LoadImage();
                }
            }
            catch
            {

            }
        }

        public frmImage GetActiveImageWindow()
        {
            frmImage f;

            try
            {
                f = (frmImage)this.ActiveMdiChild;
            }
            catch (Exception ex)
            {
                f = null;
            }
            return f;

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void toolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            frmImage f = GetActiveImageWindow();
            if (f != null)
            {
                if (f.img1.FunctionalMode == Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom)
                {
                    f.img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
                    f.img1.SetZoomScale(1, new Point(100, 100));
                    f.img1.Cursor = Cursors.Default;

                }
                else
                {
                    f.img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
                    f.img1.Cursor = Cursors.Cross;
                }
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            frmImage f = GetActiveImageWindow();
            if (f != null)
            {
                //f.img1.SetZoomScale(2, new Point(400, 400));



                if (f.img1.FunctionalMode == Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom)
                {
                    f.img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
                    //f.img1.SetZoomScale(1, new Point(100, 100));
                    f.img1.SizeMode = PictureBoxSizeMode.AutoSize;
                    f.img1.Refresh();

                    f.img1.Cursor = Cursors.Default;

                    toolStripMenuItem7.CheckState = CheckState.Unchecked;

                }
                else
                {
                    f.img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
                    f.img1.Cursor = Cursors.Cross;
                    toolStripMenuItem7.CheckState = CheckState.Checked;
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;

                f.MainImg = f.MainImg.Not();
                f.LoadImage();

            }
            catch (Exception ex)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void trackBar1_Scroll(object sender, EventArgs e)
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

        private void trackBar1_Scroll_1(object sender, EventArgs e)
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

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
           
                try
                {
                    frmImage f = GetActiveImageWindow();
                    if (f == null)
                        return;

                int w = f.MainImg.Width;
                int h = f.MainImg.Height;

                Image<Bgr, Byte> im = new Image<Bgr, Byte>(w, h);
                im.Data = f.MainImg.Data;

                f.MainImg_Org = im;
                   
                }
                catch
                {

                }


           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr,Byte> img = f.MainImg.Sub(new Bgr(255,255,0));
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
                frmImage f = GetActiveImageWindow();

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
                frmImage f = GetActiveImageWindow();

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
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> img = f.MainImg_Org.Sub(new Bgr(0, 0, 0));
                f.LoadImage(img);
            }
            catch
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(f.MainImg.Width, f.MainImg.Height);
                CvInvoke.GaussianBlur(f.MainImg, OutImg, new Size(0, 0), 3);
                f.LoadImage(OutImg);
            }
            catch
            {

            }

            //frmImage f = GetActiveImageWindow();
            //Image<Hsv, Byte> OutImg = new Image<Hsv, Byte>(f.MainImg.Width, f.MainImg.Height);
            //Image<Bgr, Byte> OutImg1 = new Image<Bgr, Byte>(f.MainImg.Width, f.MainImg.Height);
            //Emgu.CV.CvInvoke.CvtColor(f.MainImg, OutImg, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);
            //OutImg1.Data = OutImg.Data;
            //f.LoadImage(OutImg1);

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

        private void button14_Click(object sender, EventArgs e)
        {


            try
            {

                frmImage f = GetActiveImageWindow();

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

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                f.clear();
                f.isCorp = true;
              

               
            }
            catch (Exception ex)
            {
                
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                frmImage f = GetActiveImageWindow();

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


                frmImage f = GetActiveImageWindow();

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


                frmImage f = GetActiveImageWindow();

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

                frmImage f = GetActiveImageWindow();

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

        private void cmbmag_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Properties.Settings.Default["CMag"] = cmbmag.Text.Replace('x',' ');
                Properties.Settings.Default.Save();
                com.PubCMag = Convert.ToInt32(cmbmag.Text.Replace('x',' '));


            frmImage f = GetActiveImageWindow();

                if(f==null)
                    return;


            int w = f.MainImg_Org.Width;
            int h = f.MainImg_Org.Height;

            int mag = Convert.ToInt32(cmbmag.Text.Replace('x',' '));
            int mag1 = mag-100;
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





            Image < Bgr, Byte> OutImg = new Image<Bgr, Byte>(w1, h1);
            CvInvoke.Resize(f.MainImg,OutImg,new Size(w1,h1),0,0,Emgu.CV.CvEnum.Inter.Linear);
            f.MainImg = OutImg;
            f.LoadImage();

            }
            catch(System.NullReferenceException ex)
            {

            }

        }

        private void cmdok_Click(object sender, EventArgs e)
        {

            frmImage f = GetActiveImageWindow();

            if (f == null)
                return;

            if (f.isCorp == true)
            {
                f.isCorp = false;

                f.MainImg = f.MainImg.GetSubRect(f.rect);
                f.LoadImage();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {

            
            frmImage f = GetActiveImageWindow();

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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = Header + " - ["+ tabControl1.SelectedTab.Text + "]";

           

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (com.isCalibrationOpen == true)
            {

            }
            else
            {

                calibaration fFormObj = new calibaration();
                fFormObj.TopLevel = false;
                this.Controls.Add(fFormObj);
                fFormObj.Left = this.Width - fFormObj.Width-100;
                fFormObj.Top = 100;

                fFormObj.Parent = this;
                fFormObj.TopMost = true;
                fFormObj.Show();

                com.isCalibrationOpen = true;

                
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            frmImage f = GetActiveImageWindow();
            f.MainImg = (Image<Bgr,Byte>) f.img1.Image;
            f.LoadImage();
        }

        private void GetXYValue()
        {
            com.DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + com.PubScaleMode + "'");
           

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
            Properties.Settings.Default["Scale"]=com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();
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

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();
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

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();
        }

        private void mnumic_Click(object sender, EventArgs e)
        {

            mnucm.Checked = false;
            mnumm.Checked = false;
            mnuinch.Checked = false;
            mnumic.Checked = false;

            mnumic.Checked = true;
            com.PubScaleMode = "micron";
            com.PubScaleShort = "μ";

            Properties.Settings.Default["Scale"] = com.PubScaleMode;
            Properties.Settings.Default.Save();
            GetXYValue();
        }

        public void TZoom_Scroll(object sender, EventArgs e)
        {
            try
            {

               

                frmImage f = GetActiveImageWindow();

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

        private void TZoom_MouseUp(object sender, MouseEventArgs e)
        {

            frmImage f = GetActiveImageWindow();

            if (f == null)
                return;
           // f.LoadImage();
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

                frmImage f = GetActiveImageWindow();

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

        private void linkLabel1_LinkClicked(object sender, EventArgs e)
        {
            grpFlip.Visible = false;
            grpZoom.Visible = false;
            grpHSL.Visible = false;
           

            if (grpFilter.Visible == true)
                grpFilter.Visible = false;
            else
                grpFilter.Visible = true;

        }

        private void linkLabel2_LinkClicked(object sender, EventArgs e)
        {

            grpFilter.Visible = false;
            grpZoom.Visible = false;
            grpHSL.Visible = false;



            if (grpFlip.Visible == true)
                grpFlip.Visible = false;
            else
                grpFlip.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((com.Val(txtWidth.Text) == 0)|| (com.Val(txtHeight.Text) == 0))
            {
                return;
            }

            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;

                int w1 = (int)com.Val(txtWidth.Text);
            int h1 = (int)com.Val(txtHeight.Text);

            Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(w1, h1);
            CvInvoke.Resize(f.MainImg, OutImg, new Size(w1, h1), 0, 0, Emgu.CV.CvEnum.Inter.Linear);
            f.MainImg = OutImg;
            f.LoadImage();

        }
            catch(System.NullReferenceException ex)
            {

            }

}

        private void linkLabel3_LinkClicked(object sender, EventArgs e)
        {
            grpFilter.Visible = false;
            grpFlip.Visible = false;
            grpHSL.Visible = false;

            if (grpZoom.Visible == true)
                grpZoom.Visible = false;
            else
                grpZoom.Visible = true;
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Bitmap StartImage;
                //string tmppath = System.IO.Path.GetTempPath() + "/" + DateTime.Now.ToString("ddmmyyhhmmss.fff") + ".bmp";
                //f.MainImg.Save(tmppath);

                StartImage = f.MainImg.ToBitmap();

                StartImage = f.RoundCorners(StartImage, 50, Color.White);

                f.MainImg = new Image<Bgr, Byte>(StartImage);
                f.LoadImage();
            }
            catch (System.NullReferenceException ex)
            {

            }
            catch
            {
                
            }
        }




        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                frmImage f = GetActiveImageWindow();

                if (f == null)
                    return;


                Bitmap StartImage;
                //string tmppath = System.IO.Path.GetTempPath() + "/" + DateTime.Now.ToString("ddmmyyhhmmss.fff") + ".bmp";
                //f.MainImg.Save(tmppath);



                StartImage = f.MainImg.ToBitmap();

                StartImage = f.Vignette(StartImage);

                f.MainImg = new Image<Bgr, Byte>(StartImage);
                f.LoadImage();
            }
            catch (System.NullReferenceException ex)
            {

            }
            catch
            {
                
            }
        }

        const float rwgt = 0.3086f;
        const float gwgt = 0.6094f;
        const float bwgt = 0.0820f;

        private ImageAttributes imageAttributes = new ImageAttributes();
        private ColorMatrix colorMatrix = new ColorMatrix();
        private float saturation = 1.0f;
        private float brightness = 1.0f;


        private void HUL()
        {
            frmImage f = GetActiveImageWindow();

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
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            HUL();



        }

        private void label7_Click(object sender, EventArgs e)
        {
            grpFilter.Visible = false;
            grpFlip.Visible = false;
            grpZoom.Visible = false;

            if (grpHSL.Visible == true)
                grpHSL.Visible = false;
            else
                grpHSL.Visible = true;
        }

        private void trackLight_Scroll(object sender, EventArgs e)
        {
            HUL();

        }

        private void trackHue_Scroll(object sender, EventArgs e)
        {
            HUL();

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

                frmImage f = GetActiveImageWindow();

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


                frmImage f = GetActiveImageWindow();

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

        private void button25_Click(object sender, EventArgs e)
        {


         

          
        }

        private void button26_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            com.pcolor = cd.Color;
            Properties.Settings.Default["pcolor"] = com.pcolor;
            Properties.Settings.Default.Save();



        }

        private void cmbwidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            com.pwidth = (float)Convert.ToDecimal(cmbwidth.Text);
            Properties.Settings.Default["pwidth"] = com.pwidth;
            Properties.Settings.Default.Save();

        }

        private void button27_Click(object sender, EventArgs e)
        {
            errlbl.Text = "";

            try
            {
                frmImage f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();
                f.isZoom = true;
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
                frmImage f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();

                string txt = Microsoft.VisualBasic.Interaction.InputBox("Enter Text", "Insert Text", "");
                f.WriteText = txt;
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
                frmImage f = GetActiveImageWindow();
                if (f == null)
                    return;

                f.clear();

             
            }
            catch
            {

            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    frmImage f = GetActiveImageWindow();
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


                frmImage f = GetActiveImageWindow();

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
                frmImage f = GetActiveImageWindow();

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
                frmImage f = GetActiveImageWindow();

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

        private void TZoom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
