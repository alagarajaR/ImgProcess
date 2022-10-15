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
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;
using Emgu.CV.Features2D;

namespace sampleEmgucv
{
    public partial class frm_MainForm : Form
    {

        VideoCapture capture;
        
        bool pause = false;
        public frm_MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            
            openDlg.Filter = "Image Files (*.jpg;*.png;*.bmp;) | *.jpg;*.png;*.bmp; | All Files (*.*) | *.*; ";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                var img = new Image<Bgr, byte>(openDlg.FileName);
                if (img != null)
                {
                    pbMainImage.Image = img.ToBitmap();
                }
                else
                {
                    MessageBox.Show("Error Opening image");
                }
            }
        }

        private void stichToolStripMenuItem_Click(object sender, EventArgs e)
        {
   


            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Multiselect = true;
            openDlg.Filter = "Image Files (*.jpg;*.png;*.bmp;) | *.jpg;*.png;*.bmp; | All Files (*.*) | *.*; ";
            VectorOfMat images = new VectorOfMat();
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in openDlg.FileNames)
                {
                    images.Push(new Mat(file));
                }

                Brisk detector = new Brisk();
                WarperCreator warper = new PlaneWarper();


                Cursor = Cursors.WaitCursor;
                Stitcher stitcher = new Stitcher();
                stitcher.SetFeaturesFinder(detector);
                stitcher.SetWarper(warper);

                Mat output = new Mat();
                var status = stitcher.Stitch(images, output);



                if (status == Stitcher.Status.Ok)
                {
                    pbMainImage.Image = output.ToBitmap();
                    MessageBox.Show("Image Stitching Successfull!!");

                }
                else
                {
                    MessageBox.Show("Stitching Failed !!!");
                }

                Cursor = Cursors.Default;
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        public static Image<Bgr, byte> HConcatenateImages(List<Image<Bgr, byte>> images)
        {
            try
            {
                int MaxRows = images.Max(x => x.Rows);
                int totalCols = images.Sum(x => x.Cols);

                Image<Bgr, byte> imgOutput = new Image<Bgr, byte>(totalCols, MaxRows, new Bgr(0, 0, 0));

                int xcord = 0;
                for (int i = 0; i < images.Count; i++)
                {
                    imgOutput.ROI = new Rectangle(xcord, 0, images[i].Width, images[i].Height);
                    images[i].CopyTo(imgOutput);
                    imgOutput.ROI = Rectangle.Empty;
                    xcord += images[i].Width;
                }
                return imgOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static Image<Bgr, byte> HConcatenateImages(Image<Bgr, byte> img1, Image<Bgr, byte> img2)
        {
            try
            {
                int MaxRows = img1.Rows > img2.Rows ? img1.Rows : img2.Rows;
                int totalCols = img1.Cols + img2.Cols;

                Image<Bgr, byte> imgOutput = new Image<Bgr, byte>(totalCols, MaxRows, new Bgr(0, 0, 0));


                imgOutput.ROI = new Rectangle(0, 0, img1.Width, img1.Height);
                img1.CopyTo(imgOutput);
                imgOutput.ROI = Rectangle.Empty;

                imgOutput.ROI = new Rectangle(img1.Width, 0, img2.Width, img2.Height);
                img2.CopyTo(imgOutput);
                imgOutput.ROI = Rectangle.Empty;
                return imgOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        public static Image<Bgr, byte> VConcatenateImages(List<Image<Bgr, byte>> images)
        {
            try
            {
                int MaxCols = images.Max(x => x.Cols);
                int totalRows = images.Sum(x => x.Rows);

                Image<Bgr, byte> imgOutput = new Image<Bgr, byte>(MaxCols, totalRows, new Bgr(0, 0, 0));

                int ycord = 0;
                for (int i = 0; i < images.Count; i++)
                {
                    imgOutput.ROI = new Rectangle(0, ycord, images[i].Width, images[i].Height);
                    images[i].CopyTo(imgOutput);
                    imgOutput.ROI = Rectangle.Empty;
                    ycord += images[i].Height;
                }

                return imgOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns></returns>
        public static Image<Bgr, byte> VConcatenateImages(Image<Bgr, byte> img1, Image<Bgr, byte> img2)
        {
            try
            {
                int MaxCols = img1.Cols > img2.Cols ? img1.Cols : img2.Cols;
                int totalRows = img1.Rows + img2.Rows;

                Image<Bgr, byte> imgOutput = new Image<Bgr, byte>(MaxCols, totalRows, new Bgr(0, 0, 0));


                imgOutput.ROI = new Rectangle(0, 0, img1.Width, img1.Height);
                img1.CopyTo(imgOutput);
                imgOutput.ROI = Rectangle.Empty;

                imgOutput.ROI = new Rectangle(0, img1.Height, img2.Width, img2.Height);
                img2.CopyTo(imgOutput);
                imgOutput.ROI = Rectangle.Empty;
                return imgOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void imageSplicingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void horizonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Image<Bgr, byte>> list = new List<Image<Bgr, byte>>();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png|*.jpg;*.jpeg;*.png)";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in dialog.FileNames)
                    {
                        list.Add(new Image<Bgr, byte>(file));
                    }
                }

                if (list.Count > 0)
                {
                    var img = HConcatenateImages(list);
                    pbMainImage.Image = img.AsBitmap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Image<Bgr, byte>> list = new List<Image<Bgr, byte>>();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png|*.jpg;*.jpeg;*.png)";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in dialog.FileNames)
                    {
                        list.Add(new Image<Bgr, byte>(file));
                    }
                }

                if (list.Count > 0)
                {
                    var img = VConcatenateImages(list);
                    pbMainImage.Image = img.AsBitmap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iCameraIndex = 0;
            try 
            { 

                    if (txt_CameraIndex.Text == string.Empty )
                    {
                         iCameraIndex = 0;
                    }
                    else
                    {
                     iCameraIndex = Convert.ToInt32(txt_CameraIndex.Text);
                    }
                    if (capture== null )
                    {
                            capture = new VideoCapture(iCameraIndex);
                    }
                    capture.Set(Emgu.CV.CvEnum.CapProp.FrameWidth, 1024);
                    capture.Set(Emgu.CV.CvEnum.CapProp.FrameHeight, 768);
                    capture.ImageGrabbed += Capture_ImageGrabbed;

                capture.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Starting Image Capture" + ex.Message);
            }


        }

        private void Capture_ImageGrabbed(object sender,EventArgs args)
        {
            try
            {
                Mat mt = new Mat();
                capture.Retrieve(mt);
                pbMainImage.Image = mt.ToImage<Bgr, byte>().AsBitmap();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Grabbing the image : " +  ex.Message);

            }
            
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }
        }

        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pbMainImage.Image.Save(saveFileDialog.FileName);
                    }
                }
                MessageBox.Show("Image Saved Successfully !!!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Saving images" + ex.Message);
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }
        }
    }
}
