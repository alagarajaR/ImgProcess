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

using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;
using Emgu.CV.Features2D;
using DirectShowLib;

namespace ImgProcess
{
    public partial class camera : KryptonForm
    {
        
        VideoCapture capture;
        
        public camera()
        {
            InitializeComponent();
            this.kryptonPalette1.BasePaletteMode = com.kp;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iCameraIndex = 0;
           


            cmdStart.Enabled = false;
            cmdStop.Enabled =true;
            cmdPause.Enabled = true;
            cmbModel.Enabled = false;



        }


        private void Capture_ImageGrabbedsenthil(object sender, EventArgs args)
        {
            try
            {
                //Mat mt = new Mat();
                //capture.Retrieve(mt);
                //pbMainImage.Image = mt.ToImage<Bgr, byte>().AsBitmap();



                Mat mt = new Mat();
                capture.Retrieve(mt);

                System.Array mtdata = mt.GetData();

                byte[,,] bary = new byte[720, 1280, 3];

                int i, j, k;

                for (i = 0; i < 720; i++)
                {
                    for (j = 0; j < 1280; j++)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            var v = mtdata.GetValue(i, j, k);

                            bary[i, j, k] = (byte)v;
                        }

                    }
                }








                Image<Bgr, Byte> img = new Image<Bgr, Byte>(1280, 720);

                img.Data = bary;

                pbMainImage.Image = img.ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Grabbing the image : " + ex.Message);
            }
        }

        private void Capture_ImageGrabbed(object sender, EventArgs args)
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

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
                capture = null;
            }

            cmdStart.Enabled = true;
            cmdStop.Enabled = true;
            cmdPause.Enabled = false;
            cmbModel.Enabled = false;        
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Dispose();
                capture = null;

            }


            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
            cmdPause.Enabled = false;
            cmbModel.Enabled = true;


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
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving images" + ex.Message);
            }
        }

        private void camera_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt1 = new DataTable();
            dt1 = com.sqlcn.Gettable("SELECT distinct ModelName FROM camerasettings ");

            cmbModel.Items.Clear();
            foreach (DataRow dr in dt1.Rows)
            {
                cmbModel.Items.Add(dr[0]);
            }

            if (cmbModel.Items.Count > 0)
                cmbModel.Text = cmbModel.Items[0].ToString();


            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
            cmdPause.Enabled = false;
            cmbModel.Enabled = true;

          //  cmbModel.Items.Clear();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            var cameraNames = new List<string>();
            foreach (var device in _SystemCamereas)
            {
                cameraNames.Add(device.Name);
            }
        }

        int PubWidth = 0;
        int PubHeight = 0;
        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;
            rst = com.sqlcn.Getdata("select * from Camerasettings where ModelName='" + cmbModel.Text + "'");
            if (rst.Read())
            {
                PubWidth = (int)rst["Width"];
                PubHeight = (int)rst["Height"];
             }
            rst.Close();

        }
    }
}
