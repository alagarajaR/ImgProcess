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
//using Numpy.Models;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
//using Numpy;

using Emgu.CV.Util;

namespace ImgProcess
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            
        }

        private void fun1(Image<Bgr,Byte> _img,Color _DClr,Color _LClr)
        {

            Image<Bgr, Byte> ret;


            ret = new Image<Bgr, Byte>(_img.Width, _img.Height);
            ret.Data = _img.Data;
            Image<Gray, Byte> imgD;
            Image<Gray, Byte> imgL;

            MCvScalar dc = new MCvScalar(_DClr.B, _DClr.G, _DClr.R);
            MCvScalar lc = new MCvScalar(_LClr.B, _LClr.G, _LClr.R);
            Emgu.CV.Mat mat = _img.Mat;
            if (optr1.Checked == true)
            {
                imgD = _img.InRange(new Bgr(0, 0, 0), new Bgr(D, D, D));
                //imgL = _img.Not().InRange(new Bgr(0, 0, 0), new Bgr(D, D, D));
                mat.SetTo(dc, imgD);
            }
            else
            {   imgL = _img.Not().InRange(new Bgr(0, 0, 0), new Bgr(L, L, L));
                // imgD = _img.InRange(new Bgr(0, 0, 0), new Bgr(L, L, L));
                mat.SetTo(lc, imgL);
            }

            mat.CopyTo(ret);

            
            Image x = ret.ToBitmap();

            IBox1.Image = x;

            button3.PerformClick();
          //  IBox3.Image = ret;
        }
        private void test_Load(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img;


           
            IBox1.Image = new Bitmap("E:\\sample images\\SG1.jpg");
         


        }

        int D=190;
        int L = 255;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            D = trackBar1.Value;
            Image<Bgr, Byte> img;
            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");
            IBox1.Image = img.AsBitmap();
            fun1(img, DarkClr,LightClr);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        Color DarkClr=Color.Black;
        Color LightClr = Color.White;

        private void button1_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.ShowDialog();

            DarkClr = cd.Color;
            


             optr1.Checked = true;
          

            Bitmap img = new Bitmap("E:\\sample images\\SG1.jpg");
            IBox1.Image = img;
            D = 128;
            trackBar1.Value = D;
            fun1(img.ToImage<Bgr,Byte>(), DarkClr,LightClr);

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
          
            L = trackBar2.Value;
            Image<Bgr, Byte> img;
            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");
            IBox1.Image = new Bitmap("E:\\sample images\\SG1.jpg");
            fun1(img, DarkClr,LightClr);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();

            LightClr = cd.Color;


            optr2.Checked = true;
            Image<Bgr, Byte> img;


            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");

            IBox1.Image = new Bitmap("E:\\sample images\\SG1.jpg");
            L = 128;
            trackBar2.Value = L;
            fun1(img,DarkClr,LightClr);

        }

        private void optr2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBar2.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img = new Bitmap(IBox1.Image).ToImage<Bgr,Byte>();

            int i = 0;
            int j = 0;

            long TotalSize = img.Width * img.Height;
            long SelectedSize = 0;
            long DarkCount = 0;
            long LightCount = 0;

            for(i=0;i<=img.Width-1;i++)
            {
                for (j = 0; j <= img.Height - 1; j++)
                {

                  

                    byte B = img.Data[j, i, 0];
                    byte G = img.Data[j, i, 1];
                    byte R = img.Data[j, i, 2];


                    Color pc = Color.FromArgb(0,R, G, B);

                    if ((pc.R == DarkClr.R) && (pc.G == DarkClr.G) && (pc.B == DarkClr.B))
                    {
                        DarkCount = DarkCount + 1;
                    }

                    if ((pc.R == LightClr.R) && (pc.G == LightClr.G) && (pc.B == LightClr.B))
                    {
                        LightCount = LightCount + 1;
                    }

                }
            }

            textBox1.Text = TotalSize.ToString();
            textBox2.Text = DarkCount.ToString();
            textBox3.Text = LightCount.ToString();


            textBox4.Text = (com.Val(textBox1.Text) / com.Val(textBox1.Text) * 100).ToString("000.00");
            textBox5.Text = (com.Val(textBox2.Text) / com.Val(textBox1.Text) * 100).ToString("000.00");
            textBox6.Text = (com.Val(textBox3.Text) / com.Val(textBox1.Text) * 100).ToString("000.00");


        }

        private class cont
        {
            public long SlNo { get; set; }
            public long ContId { get; set; }
            public decimal ArcLength { get; set; }
            public decimal Area { get; set; }

        }
        List<cont> contList = new List<cont>();

        private void button4_Click(object sender, EventArgs e)
        {
          
           // Emgucv33Apps.FormShapeDetection f = new Emgucv33Apps.FormShapeDetection();
           // f.Show();
        }

        private void HUL()
        {

            
           
            float satscale = ((float)10 / 100);
            float huescale = ((float)10 / 100);
            float lightscale = ((float)10 / 100);

            Image<Bgr, Byte> img = new Bitmap(IBox1.Image).ToImage<Bgr, Byte>();

            var imhHsv = img.Convert<Hsv, byte>();
            CvInvoke.CvtColor(img, imhHsv, Emgu.CV.CvEnum.ColorConversion.Bgr2Hsv);


            //var channels = (IOutputArray)null;

            Emgu.CV.Util.VectorOfMat channels = new Emgu.CV.Util.VectorOfMat();

            CvInvoke.Split(imhHsv, channels);

            IntPtr[] c2 = new IntPtr[3];


            c2[0] = (channels[0] * huescale);
            c2[1] = (channels[1] * satscale);
            c2[2] = (channels[2] * lightscale);

            Mat[] m = new Mat[3];
            m[0] = CvInvoke.CvArrToMat(c2[0]);
            m[1] = CvInvoke.CvArrToMat(c2[1]);
            m[2] = CvInvoke.CvArrToMat(c2[2]);

            Emgu.CV.Util.VectorOfMat vm = new Emgu.CV.Util.VectorOfMat(m);

            CvInvoke.Merge(vm, imhHsv);


            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Status = true;
            Image<Bgr, Byte> img;


            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");

            IBox1.Image = new Bitmap("E:\\sample images\\SG1.jpg");
            L = 128;
            trackBar2.Value = L;
            fun1(img, DarkClr, LightClr);


            Image<Bgr, Byte> imgInput = new Bitmap(IBox1.Image).ToImage<Bgr,Byte>();

            if (imgInput == null)
            {
                return;
            }

            try
            {
                // var temp = imgInput.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinaryInv(new Gray(230), new Gray(255));

                Image<Gray, Byte> temp = imgInput.Convert<Gray, Byte>().ThresholdBinaryInv(new Gray(128),new Gray(255));


                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Emgu.CV.Mat m = new Emgu.CV.Mat();

                CvInvoke.FindContours(temp, contours, m, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxTc89L1);

                this.dataGridView1.Rows.Clear();

                int slno = 0;
                contList.Clear();
                for (int i = 0; i < contours.Size; i++)
                {
                    double perimeter = CvInvoke.ArcLength(contours[i], true);
                    //if (perimeter < 1)
                    //    continue;

                    double Area = CvInvoke.ContourArea(contours[i],false);


                    VectorOfPoint approx = new VectorOfPoint();
                    //CvInvoke.ApproxPolyDP(contours[i], approx, 0.01 * perimeter, true);

                    CvInvoke.DrawContours(imgInput, contours,i, new MCvScalar(0, 0, 255),1);
                 
                    //moments  center of the shape

                    var moments = CvInvoke.Moments(contours[i]);
                    int x = (int)(moments.M10 / moments.M00);
                    int y = (int)(moments.M01 / moments.M00);

                    if (approx.Size == 3)
                    {
                    //    CvInvoke.PutText(imgInput, "Triangle", new Point(x, y),
                    //        Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);
                    //
                    
                    }

                    if (approx.Size == 4)
                    {
                        Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);

                        double ar = (double)rect.Width / rect.Height;

                        if (ar >= 0.95 && ar <= 1.05)
                        {
                            //CvInvoke.PutText(imgInput, "Square", new Point(x, y),
                            //Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);
                        }
                        else
                        {
                            //CvInvoke.PutText(imgInput, "Rectangle", new Point(x, y),
                            //Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);
                        }

                    }

                    if (approx.Size == 6)
                    {
                        //CvInvoke.PutText(imgInput, "Hexagon", new Point(x, y),
                        //    Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);
                    }


                    if (approx.Size > 6)
                    {
                        //CvInvoke.PutText(imgInput, "Circle", new Point(x, y),
                        //    Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.5, new MCvScalar(0, 0, 255), 2);
                    }

                    slno = slno + 1;


                    cont c = new cont();
                    c.SlNo = slno;
                    c.ContId = i;
                    c.ArcLength =(decimal)perimeter;
                    c.Area = 0;
                    contList.Add(c);
                    
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = slno.ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = perimeter.ToString("000.00");
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = Area.ToString("0000.00");
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = i.ToString();



                }
                label2.Text = dataGridView1.Rows.Count.ToString();

                IBox1.Image = imgInput.AsBitmap();

              


              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Status = false;

        }

        private void contReDraw(long _contId)
        {
            Image<Bgr, Byte> img;


            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");
            IBox1.Image = new Bitmap("E:\\sample images\\SG1.jpg");
            fun1(img, DarkClr, LightClr);



            Image<Bgr, Byte> imgInput = new Bitmap(IBox1.Image).ToImage<Bgr,Byte>();
            if (imgInput == null)
            {
                return;
            }

            try
            {
                

                Image<Gray, Byte> temp = imgInput.Convert<Gray, Byte>().ThresholdBinaryInv(new Gray(128), new Gray(255));


                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Emgu.CV.Mat m = new Emgu.CV.Mat();

                CvInvoke.FindContours(temp, contours, m, Emgu.CV.CvEnum.RetrType.Ccomp, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxTc89L1);

               

                int slno = 0;
                contList.Clear();
                for (int i = 0; i < contours.Size; i++)
                {
                    double perimeter = CvInvoke.ArcLength(contours[i], true);
                    //if (perimeter < 1)
                     //   continue;

               if (i!=_contId)
                    CvInvoke.DrawContours(imgInput, contours, i, new MCvScalar(0, 0, 255), 1);
                else
                    CvInvoke.DrawContours(imgInput, contours, i, new MCvScalar(0, 0, 255), 2);



                }

                IBox1.Image = imgInput.AsBitmap();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool Status = false;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Status == true)
                return;

            long id = (long)com.Val(dataGridView1.Rows[e.RowIndex].Cells[3].Value);

            contReDraw(id);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            HUL();

        }
    }
    
}
