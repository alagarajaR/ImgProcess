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

namespace ImgProcess
{

    public partial class frmImage : Form
    {


        public Rectangle rect;
        public Point StartLocation;
        public Point EndLcation;
        public bool IsMouseDown = false;
        public bool isDrawLine = false;
        public bool isMarkLine = false;
        public bool isMarkArc = false;
        public bool isDrawArrowLine = false;
        public bool isDrawRect = false;
        public bool isCircle = false;
        public bool isCurve = false;
        public bool isPPLine = false;
        public bool isAngle = false;
        public bool isCCurve = false;
        public Point FirstPoint;
        public bool isMove = false;
        public bool isCorp = false;
        public int ImageListPosition = 0;

        public string WriteText = "";

         List<Point> PointList;
        bool isCurvePointsStart = false;
        public string PubDistance;
        public ObjList olist = new ObjList();
        public Shapes MovShap;
        public Shapes PropShap;
        public bool isZoom = false;

        public  List<Image<Bgr, byte>> ImgList=new List<Image<Bgr, byte>>();

        public Image<Bgr, byte> MainImg;
        public Image<Bgr, byte> MainImg_Org;
        public frmImage(string path)
        {
            InitializeComponent();

            MainImg = new Image<Bgr, byte>(path);
            MainImg_Org = new Image<Bgr, byte>(path);
            img1.Image = MainImg;
            img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            ImgList.Clear();

            ImgList.Add(MainImg);

         

        }


        public void LoadImageZoom(Image<Bgr, Byte> img = null)
        {
            if (img == null)
            {

                img1.Image = MainImg;
            }
            else
            {
               
                img1.Image = img;
            }
            


        }

        public void LoadMainImg(Image<Bgr, Byte> img)
        {
            Image<Bgr, Byte> im = img.CopyBlank();
            im.Data = img.Data;
            MainImg_Org = im;
        }
        public void LoadImage(Image<Bgr,Byte> img=null,bool isReUnDo=false)
        {
            if (img == null)
            {
               
                ImgList.Add(MainImg);
                img1.Image = MainImg;
            }
            else
            {
                ImgList.Add(img);
                img1.Image = img;
            }
            if(isReUnDo==false)
            ImageListPosition = ImgList.Count - 1;



        }

      

        private void image_Load(object sender, EventArgs e)
        {
            img1.MouseWheel += Img1_MouseWheel;

            Lyquidity.UtilityLibrary.Controls.RulerControl objr = new RulerControl();

            objr.Height = 30;
            objr.MouseTrackingOn = true;
            objr.ScaleMode = enumScaleMode.smPixels;
            objr.Dock = DockStyle.Top;
            objr.Orientation = enumOrientation.orHorizontal;
            objr.CreateControl();


            Lyquidity.UtilityLibrary.Controls.RulerControl objr1 = new RulerControl();

            objr1.Width = 30;
            objr1.MouseTrackingOn = true;
            objr1.ScaleMode = enumScaleMode.smPixels;
            objr1.Dock = DockStyle.Left;
            objr1.Orientation = enumOrientation.orVertical;
            objr1.CreateControl(); 
            this.Controls.Add(objr);
            this.Controls.Add(objr1);


            img1.Left = 31;
            img1.Top = 31;




            //ObjectList objlist = new ObjectList();
            //objlist.TopLevel = false;
            //objlist.Name = "ObjectList";
            //this.Controls.Add(objlist);
            //objlist.Left = this.Width - objlist.Width - 150;
            //objlist.Top = 100;
            
            //objlist.Parent = this;
            //objlist.TopMost = true;
            //this.Controls.SetChildIndex(img1, 2);
            //this.Controls.SetChildIndex(objlist, 1);
            //objlist.Show();



        }

        private void Img1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isZoom == false)
                return;

            MDIParent1 pf = (MDIParent1)this.MdiParent;

            if (e.Delta > 0)
            {
                if(pf.TZoom.Value+10<= pf.TZoom.Maximum)
                    pf.TZoom.Value = pf.TZoom.Value + 10;
            }
            else
            {
                if (pf.TZoom.Value - 10 >= pf.TZoom.Minimum)
                    pf.TZoom.Value = pf.TZoom.Value - 10;
            }

            //pf.TZoom_Scroll(sender, e);

        }

        public void PaintVignette(Graphics g, Rectangle bounds)
        {
            Rectangle ellipsebounds = bounds;
            ellipsebounds.Offset(-ellipsebounds.X, -ellipsebounds.Y);
            int x = ellipsebounds.Width - (int)Math.Round(.70712 * ellipsebounds.Width);
            int y = ellipsebounds.Height - (int)Math.Round(.70712 * ellipsebounds.Height);
            ellipsebounds.Inflate(x, y);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ellipsebounds);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.WrapMode = WrapMode.Tile;
                    brush.CenterColor = Color.FromArgb(0, 0, 0, 0);
                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 0, 0, 0) };
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0.0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0F };
                    blend.Factors = new float[] { 0.0f, 0.5f, 1f, 1f, 1.0f, 1.0f };
                    brush.Blend = blend;
                    Region oldClip = g.Clip;
                    g.Clip = new Region(bounds);
                    g.FillRectangle(brush, ellipsebounds);
                    g.Clip = oldClip;
                }
            }
        }

        public Bitmap Vignette(Bitmap b)
        {
            Bitmap final = new Bitmap(b);
            using (Graphics g = Graphics.FromImage(final))
            {
                PaintVignette(g, new Rectangle(0, 0, final.Width, final.Height));
                return final;
            }
        }



        private void img1_Click(object sender, EventArgs e)
        {

        }

        private void img1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((isDrawLine == true) || (isMarkLine == true))
            {
                StartLocation = e.Location;
                IsMouseDown = true;
            }
            else if ((isDrawArrowLine == true) || (isMarkArc == true))
            {
                StartLocation = e.Location;
                IsMouseDown = true;
            }
            else if (isDrawRect == true)
            {
                StartLocation = e.Location;
                IsMouseDown = true;
            }
            else if (isCorp == true)
            {
                StartLocation = e.Location;
                IsMouseDown = true;
            }
            else if (isCircle == true)
            {
                StartLocation = e.Location;
                IsMouseDown = true;
            }
            else if (isCurve == true)
            {
                isCurvePointsStart = true;
                StartLocation = e.Location;
                PointList.Add(e.Location);
                IsMouseDown = true;

            }
            else if (isPPLine == true)
            {
                isCurvePointsStart = true;
                StartLocation = e.Location;
                PointList.Add(e.Location);
                IsMouseDown = true;

            }
            else if (isAngle == true)
            {
                isCurvePointsStart = true;
                StartLocation = e.Location;
                PointList.Add(e.Location);
                IsMouseDown = true;

            }
            else if (isMove == true)
                IsMouseDown = true;
            else if (isCCurve == true)
            {
                if (isCurvePointsStart == false)
                    FirstPoint = e.Location;

                isCurvePointsStart = true;
                StartLocation = e.Location;
                PointList.Add(e.Location);
                //PointList.Add(FirstPoint);
                IsMouseDown = true;
            }

            else if (WriteText.Length > 0)
            {
                IsMouseDown = true;
            }
        }


        public Bitmap RoundCorners(Image StartImage, float CornerRadius, Color BackgroundColor)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);

            using (Graphics g = Graphics.FromImage(RoundedImage))
            {
                g.Clear(BackgroundColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Brush brush = new TextureBrush(StartImage))
                {
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        gp.AddArc(0, -1, CornerRadius, CornerRadius, 180, 90);
                        gp.AddArc(-1 + RoundedImage.Width - CornerRadius, -1, CornerRadius, CornerRadius, 270, 90);
                        gp.AddArc(-1 + RoundedImage.Width - CornerRadius, -1 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                        gp.AddArc(0, -1 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

                        g.FillPath(brush, gp);
                    }
                }

                return RoundedImage;
            }
        }

        private Point RCenter(Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,rect.Top + rect.Height / 2);
        }


        private void img1_MouseUp(object sender, MouseEventArgs e)
        {


            if (IsMouseDown == true)
            {

                Pen p = new Pen(com.pcolor, com.pwidth);
                Brush br = new SolidBrush(com.pcolor);


                EndLcation = e.Location;
                IsMouseDown = false;


                double xValue = 0;
                double yValue = 0;
                System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
                if (dr.Length > 0)
                {
                    xValue = Convert.ToDouble(dr[0]["xValue"]);
                    yValue = Convert.ToDouble(dr[0]["yValue"]);
                }


                if (((isDrawLine == true) || (isMarkLine==true)) && (isCorp == false))
                {


                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    string Distance = "";


                    if (com.isCalibrationOpen == true)
                    {
                        Distance = GetDistance(StartLocation.X, StartLocation.Y, EndLcation.X, EndLcation.Y).ToString();
                    }
                    else
                    {
                        Distance = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndLcation.X * xValue, EndLcation.Y * yValue).ToString();
                    }


                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    Bitmap xx = timg.ToBitmap();
                    Graphics g = Graphics.FromImage(xx);

                    if (com.isCalibrationOpen == false)
                    {
                        DrawLine(g, p, StartLocation, EndLcation, Distance, TPoint, br,false);
                    }
                    timg = new Image<Bgr, Byte>(xx);

                    MainImg = timg;
                    //LoadMainImg(MainImg);

                    LoadImage(timg);

                    img1.Invalidate();


                    if (com.isCalibrationOpen == true)
                    {

                        PubDistance = Distance;


                        calibaration f = getCalForm();
                        if (f == null)
                            return;

                    }

                }

                else if (((isDrawArrowLine == true) || (isMarkArc == true)) && (isCorp == false))
                {


                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    string Distance = "";


                    if (com.isCalibrationOpen == true)
                    {
                        Distance = GetDistance(StartLocation.X, StartLocation.Y, EndLcation.X, EndLcation.Y).ToString();
                    }
                    else
                    {
                        Distance = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndLcation.X * xValue, EndLcation.Y * yValue).ToString();
                    }

                    Bitmap xx = timg.ToBitmap();
                    Graphics g = Graphics.FromImage(xx);


                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                    p.CustomEndCap = bigArrow;


                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                    DrawLine(g, p, StartLocation, EndLcation, Distance, TPoint, br,false);

                    timg = new Image<Bgr, Byte>(xx);

                    MainImg = timg;
                    LoadMainImg(MainImg);

                    LoadImage(timg);
                    img1.Invalidate();


                    if (com.isCalibrationOpen == true)
                    {

                        PubDistance = Distance;


                        calibaration f = getCalForm();
                        if (f == null)
                            return;




                    }



                }

                else if ((isDrawRect == true) && (isCorp == false))
                {

                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                    rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                    rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);



                    int l = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndLcation.X * xValue, StartLocation.Y * yValue);
                    int b = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, StartLocation.X * xValue, EndLcation.Y * yValue);


                    string Distance = "L: " + l.ToString() + " " + com.PubScaleShort + " B: " + b.ToString() + " " + com.PubScaleShort;
                    string Area = (l * b).ToString() + " " + com.PubScaleShort;
                    string premeter = (b+b+l+l).ToString() + " " + com.PubScaleShort;

                    Bitmap xx = timg.ToBitmap();
                    Graphics g = Graphics.FromImage(xx);


                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    g.DrawString("" + Distance.ToString(), new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 18);
                    g.DrawString("Area:" + Area.ToString(), new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 30);
                    g.DrawString("Permeter:" + premeter.ToString(), new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    g.DrawRectangle(p, rect);
                    g.Flush();
                    g.Save();

                    timg = new Image<Bgr, Byte>(xx);


                    MainImg = timg;
                    LoadMainImg(MainImg);


                    LoadImage(timg);
                    img1.Invalidate();


                }

                else if ((isCircle == true) && (isCorp == false))
                {

                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                    rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                    rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);

                    Bitmap xx = timg.ToBitmap();
                    Graphics g = Graphics.FromImage(xx);


                    g.DrawEllipse(p, rect);
                    g.Flush();
                    g.Save();



                    int l = GetDistance(StartLocation.X, StartLocation.Y, EndLcation.X, StartLocation.Y);

                    double r = Convert.ToDouble(l) / 2;
                    double area = Math.Round(3.14 * (r * r), 0);
                    double permeter = Math.Round(2 * 3.14 * r,2);

                    string Distance = "Area:" + area.ToString() + com.PubScaleShort;
                    string Dia="Dia:" + l.ToString() + com.PubScaleShort;
                    string Permter = "Permeter:" + permeter.ToString() + com.PubScaleShort;



                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    g.DrawString(Distance, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 18);
                    g.DrawString(Dia, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 30);
                    g.DrawString(Permter, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    timg = new Image<Bgr, Byte>(xx);

                    MainImg = timg;
                    LoadMainImg(MainImg);

                    LoadImage(timg);
                    img1.Invalidate();

                }

                else if ((isCurve == true) && (isCorp == false))
                {
                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    Point[] points = new Point[PointList.Count];

                    Emgu.CV.IInputArray iary;

                    // iary = points;

                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length >= 1)
                    {

                        if ((e.Button == MouseButtons.Right) && (points.Length > 1))
                        {
                            Bitmap xx = timg.ToBitmap();

                            Graphics g = Graphics.FromImage(xx);

                           
                            g.DrawCurve(p, points);
                            g.Flush();
                            g.Save();





                            timg = new Image<Bgr, Byte>(xx);
                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImage(timg);
                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            i = 0;
                            for (i = 0; i <= PointList.Count - 1; i++)
                            {

                                points[i] = PointList[i];
                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);
                                g.DrawEllipse(p, points[i].X, points[i].Y, (float)0.5, (float)0.5);

                                g.Flush();
                                g.Save();
                                timg = new Image<Bgr, Byte>(xx);

                            }

                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImageZoom(timg);

                        }

                    }





                }

                else if ((isCCurve == true) && (isCorp == false))
                {



                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    Point[] points = new Point[PointList.Count];

                    Emgu.CV.IInputArray iary;

                    // iary = points;

                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length >= 1)
                    {

                        if ((e.Button == MouseButtons.Right) && (points.Length > 1))
                        {
                            Bitmap xx = timg.ToBitmap();

                            Graphics g = Graphics.FromImage(xx);

                            g.DrawClosedCurve(p, points);
                            g.Flush();
                            g.Save();





                            timg = new Image<Bgr, Byte>(xx);
                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImage(timg);
                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            i = 0;
                            for (i = 0; i <= PointList.Count - 1; i++)
                            {

                                points[i] = PointList[i];
                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);
                                g.DrawEllipse(p, points[i].X, points[i].Y, (float)0.5, (float)0.5);

                                g.Flush();
                                g.Save();
                                timg = new Image<Bgr, Byte>(xx);

                            }

                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImageZoom(timg);

                        }

                    }






                }

                else if ((isPPLine == true) && (isCorp == false))
                {


                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    Point[] points = new Point[PointList.Count];

                    Emgu.CV.IInputArray iary;

                    // iary = points;

                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length >= 1)
                    {

                        if ((e.Button == MouseButtons.Right) && (points.Length > 1))
                        {
                            /* i = 0;
                             for (i = 0; i <= PointList.Count - 1; i++)
                             {
                                 points[i] = PointList[i];
                                 CvInvoke.Line(MainImg, points[i], points[i], new MCvScalar(0, 0, 255), 4, Emgu.CV.CvEnum.LineType.EightConnected);
                                 LoadImage(MainImg);
                                 img1.Invalidate();
                             }
                             CvInvoke.Polylines(MainImg,points,false, new MCvScalar(0, 0, 255), 1, Emgu.CV.CvEnum.LineType.EightConnected);
                             LoadMainImg(MainImg);
                             LoadImage(MainImg);*/

                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            i = 0;
                            for (i = 0; i <= PointList.Count - 1; i++)
                            {

                                points[i] = PointList[i];
                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);
                                g.DrawEllipse(p, points[i].X, points[i].Y, (float)0.5, (float)0.5);

                                g.Flush();
                                g.Save();
                                timg = new Image<Bgr, Byte>(xx);





                            }


                            if (points.Length == 2)
                            {

                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);


                                g.DrawLine(p, points[0], points[1]);
                                g.Flush();
                                g.Save();


                                PointList.Clear();
                                Point TPoint = new Point(points[1].X + 5, points[1].Y + 5);
                                var Distance = GetDistance(points[0].X * xValue, points[0].Y * yValue, points[1].X * xValue, points[1].Y * yValue).ToString();
                                g.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                                timg = new Image<Bgr, Byte>(xx);

                            }


                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImage(timg);
                            img1.Invalidate();


                        }

                    }





                }

                else if ((isAngle == true) && (isCorp == false))
                {


                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;

                    Point[] points = new Point[PointList.Count];

                    Emgu.CV.IInputArray iary;

                    // iary = points;

                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length >= 1)
                    {

                        if ((e.Button == MouseButtons.Right) && (points.Length > 1))
                        {
                            /* i = 0;
                             for (i = 0; i <= PointList.Count - 1; i++)
                             {
                                 points[i] = PointList[i];
                                 CvInvoke.Line(MainImg, points[i], points[i], new MCvScalar(0, 0, 255), 4, Emgu.CV.CvEnum.LineType.EightConnected);
                                 LoadImage(MainImg);
                                 img1.Invalidate();
                             }
                             CvInvoke.Polylines(MainImg,points,false, new MCvScalar(0, 0, 255), 1, Emgu.CV.CvEnum.LineType.EightConnected);
                             LoadMainImg(MainImg);
                             LoadImage(MainImg);*/

                        }
                        else if (e.Button == MouseButtons.Left)
                        {
                            i = 0;
                            for (i = 0; i <= PointList.Count - 1; i++)
                            {

                                points[i] = PointList[i];
                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);
                                g.DrawEllipse(p, points[i].X, points[i].Y, (float)0.5, (float)0.5);

                                g.Flush();
                                g.Save();
                                timg = new Image<Bgr, Byte>(xx);





                            }


                            if (points.Length == 3)
                            {

                                Bitmap xx = timg.ToBitmap();

                                Graphics g = Graphics.FromImage(xx);


                                g.DrawLine(p, points[0], points[1]);
                                g.DrawLine(p, points[1], points[2]);
                                g.Flush();
                                g.Save();

                               

                                double theta1 = Math.Atan2(points[0].Y - points[1].Y, points[0].X - points[1].X);
                                double theta2 = Math.Atan2(points[1].Y - points[2].Y, points[1].X - points[2].X);
                                //double diff = Math.Abs(theta1 - theta2);
                                double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                                double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)),0);
                              
                                PointList.Clear();
                                Point TPoint = new Point(points[1].X + 5, points[1].Y + 5);
                                g.DrawString(angle.ToString() + "° " , new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                                timg = new Image<Bgr, Byte>(xx);

                            }


                            MainImg = timg;
                            LoadMainImg(MainImg);
                            LoadImage(timg);
                            img1.Invalidate();


                        }

                    }





                }

                else if (WriteText.Length > 0)
                {
                    Image<Bgr, Byte> timg = new Image<Bgr, Byte>(MainImg.Width, MainImg.Height);
                    timg.Data = MainImg.Data;


                    Bitmap xx = timg.ToBitmap();

                    Graphics g = Graphics.FromImage(xx);
                    g.DrawString(WriteText, new Font("Verdana", 8, FontStyle.Bold), br, e.Location);

                    g.Flush();
                    g.Save();
                    timg = new Image<Bgr, Byte>(xx);
                    MainImg = timg;
                    LoadMainImg(MainImg);
                    LoadImage(timg);
                    img1.Invalidate();

                }

                else if ((isMove == true) && (isCorp == false))
                {

                    if (MovShap == null)
                        return;

                    if (MovShap.ShapeType == "LINE")
                    {
                        ShapeLine l = (ShapeLine)MovShap.Shape;
                        Point newS = e.Location;


                        int xDiff = e.Location.X - l.xSPoint.X;
                        int yDiff = e.Location.Y - l.xSPoint.Y;


                        Point newE = new Point(l.xEPoint.X + xDiff, l.xEPoint.Y + yDiff);

                        //Graphics g = img1.CreateGraphics();
                        //g.DrawLine(l.xPen, newS, newE);


                        Point TPoint = new Point(newE.X + 5, newE.Y + 5);


                        ShapeLine l1 = new ShapeLine();
                        Shapes S1 = new Shapes();

                        S1.ShapeType = "LINE";
                        S1.ShapeName = new Random().Next(100, 200).ToString();
                        l1.xPen = l.xPen;
                        l1.xSPoint = newS;
                        l1.xEPoint = newE;
                        S1.Shape = l1;
                        S1.Distance = MovShap.Distance;
                        S1.fnt = MovShap.fnt;
                        S1.Color = MovShap.Color;
                        S1.Br = MovShap.Br;
                        S1.TPoint = TPoint;

                        int i;
                        for (i = 0; i <= olist.Objects.Count - 1; i++)
                        {
                            if (olist.Objects[i].ShapeName == MovShap.ShapeName)
                                olist.Objects[i] = S1;
                        }
                        //redraw();
                    }

                }
                    if (isCorp == true)
                {
                    isCorp = false;
                    MainImg = MainImg.GetSubRect(rect);
                    LoadImage();

                }

               
            }


        }

        private void img1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                EndLcation = e.Location;
                img1.Invalidate();
            }

            if (isCurvePointsStart == true)
            {
                EndLcation = e.Location;
                img1.Invalidate();
            }
        }

        public void DrawLine(Graphics g,Pen p,Point S,Point E,string Distance,Point TPoint,Brush br,bool isReDraw)
        {

            g.DrawLine(p, S, E);

            if (com.isCalibrationOpen == true)
            {
                g.DrawString(Distance.ToString() + " px", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
            }
            else
            {
                if(isDrawLine==true)
                g.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
            }


            if (isReDraw==false)
            { 
            ShapeLine l1 = new ShapeLine();
            Shapes S1 = new Shapes();

            S1.ShapeType = "LINE";
            S1.ShapeName = new Random().Next(100, 200).ToString();
            l1.xPen =p;
            l1.xSPoint = StartLocation;
            l1.xEPoint = EndLcation;
            S1.Shape = l1;
            S1.Distance = Distance;
            S1.Unit = com.PubScaleShort;
            S1.fnt = new Font("Verdana", 8, FontStyle.Bold);
            S1.Br = br;
            S1.TPoint = TPoint;
            olist.Objects.Add(S1);

           // loadList();
            //redraw();
            }

           

        }
        private void loadList()
        {
            ObjectList objList = (ObjectList)this.Controls["ObjectList"];

            objList.lst1.Items.Clear();
            int i = 0;
            for (i = 0; i <= olist.Objects.Count - 1; i++)
            {
                objList.lst1.Items.Add(olist.Objects[i].ShapeType+"-"+olist.Objects[i].ShapeName);
            }
        }

        private void redraw()
        {
            LoadImageZoom(MainImg_Org);
            img1.Refresh();
            
            Graphics g = img1.CreateGraphics();
            int i;
            for (i = 0; i <= olist.Objects.Count - 1; i++)
            {
                Shapes o1 = olist.Objects[i];
                if (o1.ShapeType == "LINE")
                {
                    ShapeLine l = (ShapeLine)o1.Shape;
                    DrawLine(g, l.xPen, l.xSPoint, l.xEPoint,o1.Distance,o1.TPoint,o1.Br,true);

                }

            }

        }

        public void clear()
        {

            IsMouseDown = false;
            isDrawLine = false;
            isDrawArrowLine = false;
            isDrawRect = false;
            isCircle = false;
            isCurve = false;
            isCurvePointsStart = false;
            isCCurve = false;
            isCorp = false;
            isZoom = false;
            WriteText = "";
            isAngle = false;
            isMarkLine = false;
            isMarkArc = false;

            PointList = new List<Point>();

        }


        private static int GetDistance(double x1, double y1, double x2, double y2)
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }

        private calibaration getCalForm()
        {
            MDIParent1 mdi = (MDIParent1)this.MdiParent;
            int i;
            for (i = 0; i <= mdi.Controls.Count - 1; i++)
            {
                if (mdi.Controls[i].Name == "calibaration")
                {
                    calibaration f = (calibaration)mdi.Controls[i];
                    return f;
                }
            }
            return null;
        }


   


        private void img1_Paint(object sender, PaintEventArgs e)
        {

            Pen p = new Pen(com.pcolor, com.pwidth);
            Brush br = new SolidBrush(com.pcolor);


            if (IsMouseDown == false)
            {
                if (com.isCalibrationOpen == false)
                { 
                    e.Graphics.DrawLine(Pens.Transparent, new Point(1, 1), new Point(1, 1));

                   
                    return;
                }
             }

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }


            if ((isDrawLine == true)|| (isMarkLine == true))
            {

                string Distance = "";
                Point EndPoint;
                if (com.isCalibrationOpen == true)
                {
                    EndPoint = new Point(EndLcation.X, StartLocation.Y);
                    Distance = GetDistance(StartLocation.X, StartLocation.Y, EndPoint.X , EndPoint.Y ).ToString();
                }
                else
                {
                    EndPoint = EndLcation;
                    Distance = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndPoint.X * xValue, EndPoint.Y * yValue).ToString();
                }



               

                Point TPoint=new Point(EndPoint.X+5, EndPoint.Y+5);
                if (com.isCalibrationOpen == true)
                {
                    e.Graphics.DrawString(Distance.ToString() + " px" , new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                   
                }
                else
                { 
                    if (isDrawLine==true)
                        e.Graphics.DrawString(Distance.ToString() + " " + com.PubScaleShort,new Font("Verdana",8,FontStyle.Bold),br,TPoint);
                   
                }

               


                
                e.Graphics.DrawLine(p, StartLocation, EndPoint);
                e.Graphics.Flush();
                e.Graphics.Save();


                if (com.isCalibrationOpen == true)
                {

                    PubDistance = Distance;


                   calibaration f= getCalForm();
                    if (f == null)
                        return;
                }
            }

            else if ((isDrawArrowLine == true)|| (isMarkArc == true))
            {

                string Distance = "";

                Distance = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndLcation.X * xValue, EndLcation.Y * yValue).ToString();
              

                Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                if (com.isCalibrationOpen == true)
                {
                    e.Graphics.DrawString(Distance.ToString() + " px", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                }
                else
                {
                    if (isDrawArrowLine == true)
                        e.Graphics.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                }

                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                p.CustomEndCap = bigArrow;
                e.Graphics.DrawLine(p, StartLocation, EndLcation);
                e.Graphics.Flush();
                e.Graphics.Save();

            }

            else if ((isDrawRect == true) || (isCorp == true))
            {
                rect = new Rectangle();
                rect.X = Math.Min(StartLocation.X, EndLcation.X);
                rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);



                int l = GetDistance(StartLocation.X*xValue, StartLocation.Y * yValue, EndLcation.X * xValue, StartLocation.Y * yValue);
                int b = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, StartLocation.X * xValue, EndLcation.Y * yValue);


                string Distance = "L: " + l.ToString() + " " + com.PubScaleShort + " B: " + b.ToString() + " " + com.PubScaleShort;

                Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                e.Graphics.DrawString("" + Distance.ToString() , new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

      
           

                e.Graphics.DrawRectangle(p, rect);
                e.Graphics.Flush();
                e.Graphics.Save();
            }

            else if (isCircle == true)
            {
                rect = new Rectangle();
                rect.X = Math.Min(StartLocation.X, EndLcation.X);
                rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);

                e.Graphics.DrawEllipse(p, rect);
                e.Graphics.Flush();
                e.Graphics.Save();

               

                int l = GetDistance(StartLocation.X, StartLocation.Y, EndLcation.X, StartLocation.Y);

                double r = Convert.ToDouble(l) / 2;
                double area = Math.Round(3.14 * (r * r), 0);

                string Distance = "Dia:" + l.ToString() + com.PubScaleShort;


                Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                e.Graphics.DrawString(Distance, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);


                // toolStripLabel1.Text = l.ToString() + "-" + r.ToString() + "=" + area.ToString();

            }

            else if (isCurve == true)
            {
                
               // Point[] points = new Point[PointList.Count];

               // Emgu.CV.IInputArray iary;

               //// iary = points;

               // int i = 0;
               // for (i = 0; i <= PointList.Count - 1; i++)
               // {
               //     points[i] = PointList[i];
               // }

               // if (points.Length > 2)
               // {

               //     e.Graphics.DrawCurve(Pens.Red, points);
               //     e.Graphics.Flush();
               //     e.Graphics.Save();
               // }
                

            }
            
            else if (isCCurve == true)
            {
                //Point[] points = new Point[PointList.Count];

                //Emgu.CV.IInputArray iary;

                //// iary = points;

                //int i = 0;
                //for (i = 0; i <= PointList.Count - 1; i++)
                //{
                //    points[i] = PointList[i];
                //}

                //if (points.Length > 1)
                //{

               
                //    e.Graphics.DrawClosedCurve(Pens.Red, points);
                //    e.Graphics.Flush();
                //    e.Graphics.Save();
                //}


            }


            // MainImg = (Image<Bgr, Byte>)img1.Image;

            // LoadImage();



           // redraw();


        }

        private void frmImage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (img1.FunctionalMode == Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom)
                img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            else
                img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }







    public class ObjList
    {
        public List<Shapes> Objects = new List<Shapes>();

    }

    public class Shapes
    {
        public string ShapeType { get; set; }
        public string ShapeName { get; set; }
        public object Shape { get; set; }

        public string Distance { get; set; }
        public Color Color { get; set; }
        public Brush Br { get; set; }

        public string Unit { get; set; }
        public Font fnt { get; set; }

        public Point TPoint { get; set; }
    }

    public class ShapeLine
    {
        public Pen xPen { get; set; }
        public Point xSPoint { get; set; }
        public Point xEPoint { get; set; }
    }


    public class ShapeRect
    {
        public Pen xPen { get; set; }
        public Point xSPoint { get; set; }
        public Point xEPoint { get; set; }
    }
}
