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
using Lyquidity.UtilityLibrary.Controls;
using System.IO;
using System.Drawing.Drawing2D;
using Numpy;

namespace ImgProcess
{
    public partial class test2 : Form
    {
        public test2()
        {
            InitializeComponent();
        }

        private void test2_Load(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img;


            img = new Image<Bgr, Byte>("E:\\sample images\\SG1.jpg");

            IBox1.Image = img;

        }

        private void IBox1_MouseMove(object sender, MouseEventArgs e)
        {

            int ZoomVal = 100 - (int)TBMag.Value;

            Point s1 = new Point(e.X - ZoomVal, e.Y - ZoomVal);
            int width = ZoomVal*2;
            int height = ZoomVal*2;

            try
            {
                Rectangle RET = new Rectangle(s1.X, s1.Y, width, height);
                Image<Bgr, Byte> img;
                img = (Image<Bgr, Byte>)IBox1.Image;
                img = img.GetSubRect(RET);
                imgMag.Image = img;

              

            }
            catch { }
          

        }

        private void IBox1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Graphics a = imgMag.CreateGraphics();

            int x0 = 200;
            int y0 = 200;
            int x1 = 300;
            int y1 = 300;

            int x2 = 100;
            int y2 = 100;

            int r = (int)Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
            int x = x0 - r;
            int y = y0 - r;
            int width = 2 * r;
            int height = 2 * r;
            int startAngle = (int)(180 / Math.PI * Math.Atan2(y1 - y0, x1 - x0));
            int endAngle = (int)(180 / Math.PI * Math.Atan2(y2 - y0, x2 - x0));
            a.DrawArc(Pens.Red,x, y, width, height, startAngle, endAngle);

        }
    }
}
