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

    public partial class imageWM : KryptonForm
    {


        public Rectangle rect;
        public Point StartLocation;
        public Point EndLcation;
        public bool IsMouseDown = false;
        public bool isDrawLine = false;
        public bool isMarkLine = false;
        public bool isDrawPoint = false;
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
        public bool isReSizeLine = false;
        public bool isMoveLine = false;
        public int ImageListPosition = 0;
        public string CurrentObjectName;

        public List<DrawObject> DrawObjects = new List<DrawObject>();
        public string WriteText = "";

         List<Point> PointList;
        bool isCurvePointsStart = false;
        public string PubDistance;
        public bool isZoom = false;

        public  List<Image<Bgr, byte>> ImgList=new List<Image<Bgr, byte>>();

        public Image<Bgr, byte> MainImg;
        public Image<Bgr, byte> MainImg_Org;
        public string PubPath;
        public DateTime PubDate;
        Bitmap PubBitmap;
        public imageWM(string path)
        {
            this.DoubleBuffered = true;

            InitializeComponent();
           
            //SetTheme();
            this.PaletteMode = com.kp;

            PubPath = path;

            string filename = System.IO.Path.GetFileName(path);

            string[] x1 = filename.Split('_');
            PubDate = new DateTime(Convert.ToInt32(x1[3]), Convert.ToInt32(x1[4]), Convert.ToInt32(x1[5]), Convert.ToInt32(x1[6]), Convert.ToInt32(x1[7]), Convert.ToInt32(x1[8]));




            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            Bitmap bm = new Bitmap((System.IO.FileStream)fs);
            PubBitmap = bm;
            MainImg = bm.ToImage<Bgr,Byte>();
            MainImg_Org = bm.ToImage<Bgr, Byte>();

            img1.Image = MainImg.ToBitmap();
            //img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            ImgList.Clear();

         int sw =Screen.GetWorkingArea(this).Width;
            int sh = Screen.GetWorkingArea(this).Height;

            if (img1.Width > sw)
            {
                Image<Bgr, Byte> OutImg = new Image<Bgr, Byte>(sw, sh);
                CvInvoke.Resize(MainImg, OutImg, new Size(sw, sh), 0, 0, Emgu.CV.CvEnum.Inter.Linear);
                MainImg = OutImg;
                MainImg_Org = MainImg;
                LoadImage();
            }
            else
            {
                
            }


            ImgList.Add(MainImg);

            this.Width = img1.Width+100;
            this.Height = img1.Height+100;
            this.Location = new Point(0, 0);

            if (com.CurrentWork == "IM")
            {
                //  groupBox1.Width = 279;
                // //groupBox1.Height = 141;
                //  button2.Location = new Point(198, 157);
                //  button3.Location = new Point(236, 157);
                pan1.Visible = true;

            }
            else if (com.CurrentWork == "WM")
            {
                //  groupBox1.Width = 128;
                ////groupBox1.Height = 131;
                // button2.Location = new Point(44, 157);
                //  button3.Location = new Point(82, 157);
                pan1.Visible = false;

            }


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
           

        }

        public void LoadImageZoom(Image<Bgr, Byte> img = null)
        {
            if (img == null)
            {

                img1.Image = MainImg.ToBitmap();
            }
            else
            {
               
                img1.Image = img.ToBitmap();
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
                img1.Image = MainImg.ToBitmap();
            }
            else
            {
                ImgList.Add(img);
                img1.Image = img.ToBitmap();
            }
            if(isReUnDo==false)
            ImageListPosition = ImgList.Count - 1;

        }


        public Lyquidity.UtilityLibrary.Controls.RulerControl objr;
        public Lyquidity.UtilityLibrary.Controls.RulerControl objr1;
        private void image_Load(object sender, EventArgs e)
        {
            txtAngle.Text = "1";

            groupBox1.MouseDown += GroupBox1_MouseDown;
            groupBox1.MouseUp += GroupBox1_MouseUp;
            groupBox1.MouseMove += GroupBox1_MouseMove;

            Type t = typeof(Form);
            PropertyInfo pi = t.GetProperty("MdiClient", BindingFlags.Instance | BindingFlags.NonPublic);
            MdiClient cli = (MdiClient)pi.GetValue(this.MdiParent, null);
            panel1.Size = new Size(cli.Width - 40, cli.Height - 40);

            img1.MouseWheel += Img1_MouseWheel;

            objr = new RulerControl();

            objr.Height = 30;
            objr.MouseTrackingOn = true;
            objr.ScaleMode = enumScaleMode.smPixels;
            objr.Dock = DockStyle.Top;
            objr.Orientation = enumOrientation.orHorizontal;
            objr.CreateControl();


            objr1 = new RulerControl();

            objr1.Width = 30;
            objr1.MouseTrackingOn = true;
            objr1.ScaleMode = enumScaleMode.smPixels;
            objr1.Dock = DockStyle.Left;
            objr1.Orientation = enumOrientation.orVertical;
            objr1.CreateControl(); 
            this.Controls.Add(objr);
            this.Controls.Add(objr1);

            

            panel1.Left =30;
            panel1.Top = 30;

            ImageInfo();


            if (isDateFound()==false)
            {
                
                cLine obj = new cLine();

                obj.X1 = 0;
                obj.Y1 = 0;

                obj.X2 = 10;
                obj.Y2 = img1.Height - 30 ;


                obj.LineName = "DT" + new Random().Next(100000, 999999).ToString();
                //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                obj.DrawWidth = com.pwidth;
                obj.DrawColor = com.pcolor;
                obj.Text = PubDate.ToString("dd-MM-yyyy hh:mm tt");

                DrawObject dobj = new DrawObject();
                dobj.ObjType = "TEXT";
                obj.ObjType = dobj.ObjType;
                dobj.Obj = obj;
                DrawObjects.Add(dobj);
            }


        }

        public bool isDateFound()
        {

            if (MySettings.isDate == 0)
                return true;
            
            int i = 0;
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine obj = (cLine)DrawObjects[i].Obj;
                if (obj.LineName.Substring(0, 2) == "DT")
                    return true;
            }


            return false;
        }

        private void ImageInfo()
        {

            MDIParent1 mp = (MDIParent1)this.MdiParent;

            System.IO.FileInfo fileinfo = new System.IO.FileInfo(PubPath);

            mp.lblre.Text = "Resolution :" + PubBitmap.Width.ToString() + "x"+ PubBitmap.Height.ToString() + ", Format : " + fileinfo.Extension.ToString();

            fileinfo = null;
            PubBitmap = new Bitmap(10, 10);



        }
        Point GroupMovePoint;
        private void GroupBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (groupBox1.Cursor == Cursors.SizeAll)
            {
                int xx =  e.Location.X- GroupMovePoint.X;
                int yy = e.Location.Y-GroupMovePoint.Y;

                groupBox1.Top = groupBox1.Top + yy;
                groupBox1.Left = groupBox1.Left + xx;

            }
        }

        private void GroupBox1_MouseUp(object sender, MouseEventArgs e)
        {
            groupBox1.Cursor = Cursors.Default;

            

        }

        private void GroupBox1_MouseDown(object sender, MouseEventArgs e)
        {
            groupBox1.Cursor = Cursors.SizeAll;
            GroupMovePoint = e.Location;



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

            pf.TZoom_ValueChanged(sender, e);

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

        Point MouseStart;
        cLine SelectedLine;


        private void GetSelectedText(cLine obj,MouseEventArgs e)
        {
            cLine objl = (cLine)obj.MObj;

            if (objl == null)
                return;

            if (CurrentObjectName == objl.LineName)
            {
                MouseStart = e.Location;
                SelectedLine = objl;
                return;
            }
        }
        private void img1_MouseDown(object sender, MouseEventArgs e)
        {
            if (img1.Cursor == Cursors.Default)
            {
                if ((isDrawLine == true) || (isMarkLine == true) || (isDrawArrowLine == true))
                {
                    StartLocation = e.Location;
                    IsMouseDown = true;
                    return;
                }
                else if  (isMarkArc == true)
                {
                    isCurvePointsStart = true;
                    StartLocation = e.Location;
                    PointList.Add(e.Location);
                    IsMouseDown = true;
                    return;
                }

                else if (isDrawRect == true)
                {
                    StartLocation = e.Location;
                    IsMouseDown = true;
                    return;
                }
                else if (isDrawPoint == true)
                {
                    StartLocation = e.Location;
                    IsMouseDown = true;
                    return;
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
                    return;
                }
                else if (isCurve == true)
                {
                    isCurvePointsStart = true;
                    StartLocation = e.Location;
                    PointList.Add(e.Location);
                    IsMouseDown = true;
                    return;

                }
                else if (isPPLine == true)
                {
                    if (PointList.Count == 2)
                        PointList.Clear();

                    isCurvePointsStart = true;
                    StartLocation = e.Location;
                    PointList.Add(e.Location);
                    IsMouseDown = true;
                    return;

                }
                else if (isAngle == true)
                {
                    if (PointList.Count == 3)
                    {
                        clear();
                        isAngle = true;
                    }

                    isCurvePointsStart = true;
                    StartLocation = e.Location;
                    PointList.Add(e.Location);
                    IsMouseDown = true;
                    return;

                }
                else if (isCCurve == true)
                {
                    if (isCurvePointsStart == false)
                        FirstPoint = e.Location;

                    isCurvePointsStart = true;
                    StartLocation = e.Location;
                    PointList.Add(e.Location);
                    IsMouseDown = true;
                    return;
                }

            }

            else if (img1.Cursor == Cursors.Hand)
            {

                if (e.Button == MouseButtons.Right)
                {
                    
                    this.contextMenuStrip1.Show(MousePosition);
                }


                clear();

                isMoveLine = true;
                IsMouseDown = true;
                int i;
                for (i = 0; i <= DrawObjects.Count - 1; i++)
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;
                    if (DrawObjects[i].ObjType == "LINE")
                    {

                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ARROW")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if ((DrawObjects[i].ObjType == "MLINE") || (DrawObjects[i].ObjType == "M1LINE"))
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ARC")
                    {
                       // cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "RECT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "POINT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CIRCLE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "PPLINE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CURVE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CCURVE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ANGLE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "TEXT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    GetSelectedText(objl, e);


                }

            }

            else if (img1.Cursor == Cursors.PanSE)
            {
                clear();

                isReSizeLine = true;
                IsMouseDown = true;

                int i;
                for (i = 0; i <= DrawObjects.Count - 1; i++)
                {

                    if (DrawObjects[i].ObjType == "LINE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ARROW")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if ((DrawObjects[i].ObjType == "MLINE") || (DrawObjects[i].ObjType == "M1LINE"))
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ARC")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "RECT")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CIRCLE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "PPLINE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CURVE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "CCURVE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                    else if (DrawObjects[i].ObjType == "ANGLE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }


                }

            }



            else if (img1.Cursor == Cursors.PanEast)
            {
                clear();

                isReSizeLine = true;
                IsMouseDown = true;

                int i;
                for (i = 0; i <= DrawObjects.Count - 1; i++)
                {

                    if (DrawObjects[i].ObjType == "LINE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }
                  


                }

            }



            else if (img1.Cursor == Cursors.PanWest)
            {
                clear();

                isReSizeLine = true;
                IsMouseDown = true;

                int i;
                for (i = 0; i <= DrawObjects.Count - 1; i++)
                {

                    if (DrawObjects[i].ObjType == "LINE")
                    {
                        cLine objl = (cLine)DrawObjects[i].Obj;
                        if (CurrentObjectName == objl.LineName)
                        {
                            MouseStart = e.Location;
                            SelectedLine = objl;
                            return;
                        }
                    }



                }

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

            public int MaxLineNumber=0;
            public int MaxIPointNumber = 0;
            public int MaxCircleNumber = 0;
            public int MaxRectNumber = 0;
            public int MaxArrowNumber = 0;
            public int MaxPointNumber = 0;
            public int MaxCurveNumber = 0;
            public int MaxAngleNumber = 0;
            public int MaxArcNumber = 0;


        string pplineobjname = "";
        private void img1_MouseUp(object sender, MouseEventArgs e)
        {

            bool ShowLength = true;
            bool ShowArea = true;
            bool ShowDia = true;
            bool ShowCirm = true;
            bool ShowRad = true;
            bool ShowAngle = true;

            if (IsMouseDown == true)
            {
               

                if (isMoveLine == true)
                {
                    isMoveLine = false;
                    FindIntersect();
                    img1.Invalidate();
                    IsMouseDown = false;

                    return;
                }
                if (isReSizeLine == true)
                {
                    isReSizeLine = false;
                    FindIntersect();
                    img1.Invalidate();
                    IsMouseDown = false;
                    return;
                }
                if (isDrawLine == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    if (com.isCalibrationOpen == true)
                    {
                        DrawObjects.Clear();

                        cLine obj1 = new cLine();
                        obj1.X1 = StartLocation.X;
                        obj1.Y1 = StartLocation.Y;
                        obj1.X2 = EndLcation.X;
                        obj1.Y2 = StartLocation.Y;
                        MaxLineNumber = MaxLineNumber + 1;
                        obj1.LineName = "L" + MaxLineNumber.ToString();
                        //obj1.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj1.DrawWidth = com.pwidth;
                        obj1.DrawColor = com.pcolor;

                        DrawObject dobj1 = new DrawObject();
                        dobj1.ObjType = "LINE";
                        obj1.ObjType = dobj1.ObjType;
                        dobj1.Obj = obj1;
                        DrawObjects.Add(dobj1);
                        img1.Invalidate();
                        return;
                    }

                        cLine obj = new cLine();
                        obj.X1 = StartLocation.X;
                        obj.Y1 = StartLocation.Y;
                        obj.X2 = EndLcation.X;
                        obj.Y2 = EndLcation.Y;
                        MaxLineNumber = MaxLineNumber + 1;
                        obj.LineName = "L" + MaxLineNumber.ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj.DrawWidth = com.pwidth;
                        obj.DrawColor = com.pcolor;
                    
                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;


                    EndLcation = e.Location;
                    IsMouseDown = false;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                    cLine objt = new cLine();
                    objt.X2 = TPoint.X;
                    objt.Y2 = TPoint.Y;

                    objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                    objt.DrawWidth = com.pwidth;
                    objt.DrawColor = com.pcolor;
                    objt.Text = "";
                    obj.MObj = objt;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "LINE";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                  

                }
                else if (isMarkLine == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    cLine obj = new cLine();
                    obj.X1 = StartLocation.X;
                    obj.Y1 = StartLocation.Y;
                    obj.X2 = EndLcation.X;
                    obj.Y2 = EndLcation.Y;
                    obj.LineName = "ML" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;
                    obj.LineStyle = DashStyle.Dot;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "MLINE";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                }
                else if (isDrawPoint == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    cLine obj = new cLine();
                    obj.X1 = StartLocation.X;
                    obj.Y1 = StartLocation.Y;
                    obj.X2 = EndLcation.X;
                    obj.Y2 = EndLcation.Y;
                    MaxPointNumber = MaxPointNumber + 1;
                    obj.LineName = "P" +MaxPointNumber.ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;

                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "POINT";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                }
                else if (isPPLine == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                  //  if(PointList.Count==2)
                 //   { 
                    cLine obj = new cLine();

                    if (PointList.Count == 2)
                    {
                        int x1 = 0;
                        for (x1 = 0; x1 <= DrawObjects.Count - 1; x1++)
                        {
                            obj = (cLine)DrawObjects[x1].Obj;

                            if (pplineobjname == obj.LineName)
                            {
                                pplineobjname = "";
                              
                                goto x2;     
                            }
                        }
                    x2:;

                    }

                        try
                        {
                            obj.X1 = PointList[0].X;
                            obj.Y1 = PointList[0].Y;
                        }
                        catch {
                    }

                        try
                        {
                            obj.X2 = PointList[1].X;
                            obj.Y2 = PointList[1].Y;

                        obj.MObj.X2 = obj.X2;
                        obj.MObj.Y2 = obj.Y2;

                        PointList.Clear();

                    }
                    catch
                        {

                        }


                    if(PointList.Count ==1)
                    { 
                        MaxLineNumber = MaxLineNumber + 1;
                        obj.LineName = "PP" + MaxLineNumber.ToString();
                        if (obj.X2 == 0)
                            pplineobjname = obj.LineName;

                       //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                       obj.DrawWidth = com.pwidth;
                       obj.DrawColor = com.pcolor;

                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;



                        EndLcation = e.Location;
                        IsMouseDown = false;
                        Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                        cLine objt = new cLine();
                        objt.X2 = TPoint.X;
                        objt.Y2 = TPoint.Y;

                        objt.LineName = "T-" + new Random().Next(100000, 999999).ToString();
                        //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                        objt.DrawWidth = com.pwidth;
                        objt.DrawColor = com.pcolor;
                        objt.Text = "";
                        obj.MObj = objt;
                        DrawObject dobj = new DrawObject();
                        dobj.ObjType = "PPLINE";
                        obj.ObjType = dobj.ObjType;
                        dobj.Obj = obj;
                        DrawObjects.Add(dobj);
                    }

                    //  }

                }
                else if (isDrawArrowLine == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    cLine obj = new cLine();
                    obj.X1 = StartLocation.X;
                    obj.Y1 = StartLocation.Y;
                    obj.X2 = EndLcation.X;
                    obj.Y2 = EndLcation.Y;
                    MaxArrowNumber = MaxArrowNumber + 1;
                    obj.LineName = "A" + MaxArrowNumber.ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                    obj.EndCap = bigArrow;

                    obj.Showlength = ShowLength;
                    obj.Showcircum = ShowCirm;
                    obj.Showarea = ShowArea;
                    obj.Showdia = ShowDia;
                    obj.Showrad = ShowRad;
                    obj.Showangle = ShowAngle;



                    EndLcation = e.Location;
                    IsMouseDown = false;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                    cLine objt = new cLine();
                    objt.X2 = TPoint.X;
                    objt.Y2 = TPoint.Y;

                    objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                    objt.DrawWidth = com.pwidth;
                    objt.DrawColor = com.pcolor;
                    objt.Text = "";
                    obj.MObj = objt;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "ARROW";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                   


                }
                else if (isMarkArc == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    //  if(PointList.Count==2)
                    //   { 
                    cLine obj = new cLine();

                    if (PointList.Count >= 2)
                    {
                        int x1 = 0;
                        for (x1 = 0; x1 <= DrawObjects.Count - 1; x1++)
                        {
                            obj = (cLine)DrawObjects[x1].Obj;

                            if (pplineobjname == obj.LineName)
                            {
                                pplineobjname = "";

                                goto x2;
                            }
                        }
                    x2:;

                    }

                    try
                    {
                        obj.X1 = PointList[0].X;
                        obj.Y1 = PointList[0].Y;
                    }
                    catch
                    {
                    }

                    try
                    {
                        obj.X2 = PointList[1].X;
                        obj.Y2 = PointList[1].Y;

                        //obj.MObj.X2 = obj.X2;
                        //obj.MObj.Y2 = obj.Y2;

                        //  PointList.Clear();

                    }
                    catch
                    {

                    }


                    if (PointList.Count == 1)
                    {
                        MaxArcNumber = MaxArcNumber + 1;
                        obj.LineName = "AR" + MaxArcNumber.ToString();
                        if (obj.X2 == 0)
                            pplineobjname = obj.LineName;

                        //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj.DrawWidth = com.pwidth;
                        obj.DrawColor = com.pcolor;

                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;

                        obj.PointList = new List<Point>();

                        foreach (Point pp in PointList)
                        {
                            obj.PointList.Add(pp);
                        }


                        EndLcation = e.Location;
                        IsMouseDown = false;
                        Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                        cLine objt = new cLine();
                        objt.X2 = TPoint.X;
                        objt.Y2 = TPoint.Y;


                        objt.LineName = "T-" + new Random().Next(100000, 999999).ToString();
                        //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                        objt.DrawWidth = com.pwidth;
                        objt.DrawColor = com.pcolor;
                        objt.Text = "";
                        obj.MObj = objt;
                        DrawObject dobj = new DrawObject();
                        dobj.ObjType = "ARC";
                        obj.ObjType = dobj.ObjType;
                        dobj.Obj = obj;
                        DrawObjects.Add(dobj);

                    }
                    else
                    {
                        if (obj.PointList != null)
                        {
                            obj.PointList.Clear();
                            foreach (Point pp in PointList)
                            {
                                obj.PointList.Add(pp);
                            }
                        }
                    }

                    if (PointList.Count==3)
                    {
                        isMarkArc = false;


                        Point p1, p2, p3;

                        try
                        {
                            p1 = PointList[0];
                        }
                        catch
                        {
                            p1 = new Point(0, 0);
                        }

                        try
                        {
                            p2 = PointList[1];
                        }
                        catch
                        {
                            p2 = new Point(0, 0);
                        }

                        try
                        {
                            p3 = PointList[2];
                        }
                        catch
                        {
                            p3 = new Point(0, 0);
                        }

                       if ((p1.X > 0) && (p2.X > 0) && (p3.X > 0))
                        {
                          
                            Point obj_Center;
                            Rectangle arc_Rect;
                            float start_Angle;
                            float sweep_Angle;

                            arc_Start_Pt = p1;
                            arc_Mid_Pt = p2;
                            arc_End_Pt = p3;

                            obj_Center = getArcCenter(arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                            arc_Rect = get_ArcRectangle(obj_Center, obj_Radius);
                            start_Angle = get_Start_Angle(obj_Center, arc_Start_Pt);
                            sweep_Angle = get_Sweep_Angle(obj_Center, arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                            obj.Arc_Radius = obj_Radius;
                            obj.Arc_StartAngle = start_Angle;
                            obj.Arc_SweepAngle = sweep_Angle;
                            obj.X1 = obj_Center.X;
                            obj.Y1 = obj_Center.Y;
                            obj.Arc_Rectangle = arc_Rect;

                        }


                            PointList.Clear();


                    }


                }
                else if (isDrawRect == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;


                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                    rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                    rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);


                    cLine obj = new cLine();
                    obj.X1 = StartLocation.X;
                    obj.Y1 = StartLocation.Y;
                    obj.X2 = EndLcation.X;
                    obj.Y2 = EndLcation.Y;
                    MaxRectNumber = MaxRectNumber + 1;
                    obj.LineName = "R" + MaxRectNumber.ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;
                    obj.Width = rect.Width;
                    obj.Height = rect.Height;

                    obj.Showlength = ShowLength;
                    obj.Showcircum = ShowCirm;
                    obj.Showarea = ShowArea;
                    obj.Showdia = ShowDia;
                    obj.Showrad = ShowRad;
                    obj.Showangle = ShowAngle;



                    EndLcation = e.Location;
                    IsMouseDown = false;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                    cLine objt = new cLine();
                    objt.X2 = TPoint.X;
                    objt.Y2 = TPoint.Y;

                    objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                    objt.DrawWidth = com.pwidth;
                    objt.DrawColor = com.pcolor;
                    objt.Text = "";
                    obj.MObj = objt;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "RECT";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                }
                else if (isCircle == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                  


                    cLine obj = new cLine();
                    obj.X1 = StartLocation.X;
                    obj.Y1 = StartLocation.Y;
                    obj.X2 = EndLcation.X;
                    obj.Y2 = StartLocation.Y;
                    MaxCircleNumber = MaxCircleNumber + 1;
                    obj.LineName = "C" + MaxCircleNumber.ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;
                    obj.Width = rect.Width;
                    obj.Height = rect.Height;

                    obj.Showlength = ShowLength;
                    obj.Showcircum = ShowCirm;
                    obj.Showarea = ShowArea;
                    obj.Showdia = ShowDia;
                    obj.Showrad = ShowRad;
                    obj.Showangle = ShowAngle;



                    EndLcation = e.Location;
                    IsMouseDown = false;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                    cLine objt = new cLine();
                    objt.X2 = TPoint.X;
                    objt.Y2 = TPoint.Y;

                    objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                    objt.DrawWidth = com.pwidth;
                    objt.DrawColor = com.pcolor;
                    objt.Text = "";
                    obj.MObj = objt;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "CIRCLE";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                }
                else if (isAngle == true)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;
                    if(PointList.Count==3)
                    { 
                    cLine obj = new cLine();
                    obj.PointList = PointList;
                    MaxAngleNumber = MaxAngleNumber + 1;
                    obj.LineName = "AN" + MaxAngleNumber.ToString();
                        //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj.DrawWidth = com.pwidth;
                        obj.DrawColor = com.pcolor;

                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;


                        EndLcation = e.Location;
                        IsMouseDown = false;
                        Point TPoint = new Point(obj.PointList[1].X + 5, obj.PointList[1].Y + 5);

                        cLine objt = new cLine();
                        objt.X2 = TPoint.X;
                        objt.Y2 = TPoint.Y;

                        objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                        //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                        objt.DrawWidth = com.pwidth;
                        objt.DrawColor = com.pcolor;
                        objt.Text = "";
                        obj.MObj = objt;
                        DrawObject dobj = new DrawObject();
                        dobj.ObjType = "ANGLE";
                        obj.ObjType = dobj.ObjType;
                        dobj.Obj = obj;
                        DrawObjects.Add(dobj);
                    }

                }
                else if (isCCurve == true)
                {

                    EndLcation = e.Location;
                    IsMouseDown = false;

                    //  if(PointList.Count==2)
                    //   { 
                    cLine obj = new cLine();

                    if (PointList.Count >= 2)
                    {
                        int x1 = 0;
                        for (x1 = 0; x1 <= DrawObjects.Count - 1; x1++)
                        {
                            obj = (cLine)DrawObjects[x1].Obj;

                            if (pplineobjname == obj.LineName)
                            {
                                pplineobjname = "";

                                goto x2;
                            }
                        }
                    x2:;

                    }

                    try
                    {
                        obj.X1 = PointList[0].X;
                        obj.Y1 = PointList[0].Y;
                    }
                    catch
                    {
                    }

                    try
                    {
                        obj.X2 = PointList[1].X;
                        obj.Y2 = PointList[1].Y;

                        //obj.MObj.X2 = obj.X2;
                        //obj.MObj.Y2 = obj.Y2;

                        //  PointList.Clear();

                    }
                    catch
                    {

                    }


                    if (PointList.Count == 1)
                    {
                        MaxCurveNumber = MaxCurveNumber + 1;
                        obj.LineName = "CCR" + MaxCurveNumber.ToString();
                        if (obj.X2 == 0)
                            pplineobjname = obj.LineName;

                        //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj.DrawWidth = com.pwidth;
                        obj.DrawColor = com.pcolor;

                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;

                        obj.PointList = new List<Point>();

                        foreach (Point pp in PointList)
                        {
                            obj.PointList.Add(pp);
                        }


                        EndLcation = e.Location;
                        IsMouseDown = false;
                        Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                        cLine objt = new cLine();
                        objt.X2 = TPoint.X;
                        objt.Y2 = TPoint.Y;

                        objt.LineName = "T-" + new Random().Next(100000, 999999).ToString();
                        //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                        objt.DrawWidth = com.pwidth;
                        objt.DrawColor = com.pcolor;
                        objt.Text = "";
                        obj.MObj = objt;
                        DrawObject dobj = new DrawObject();
                        dobj.ObjType = "CCURVE";
                        obj.ObjType = dobj.ObjType;
                        dobj.Obj = obj;
                        DrawObjects.Add(dobj);



                    }
                    else
                    {
                        if (obj.PointList != null)
                        {
                            obj.PointList.Clear();
                            foreach (Point pp in PointList)
                            {
                                obj.PointList.Add(pp);
                            }
                        }
                    }

                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        isCCurve = false;

                        try
                        {
                            obj.PointList.Add(obj.PointList[0]);
                        }
                        catch
                        { }
                        PointList.Clear();
                    }

                }
                else if (isCurve == true)
                {

                    EndLcation = e.Location;
                    IsMouseDown = false;

                    //  if(PointList.Count==2)
                    //   { 
                    cLine obj = new cLine();

                    if (PointList.Count >= 2)
                    {
                        int x1 = 0;
                        for (x1 = 0; x1 <= DrawObjects.Count - 1; x1++)
                        {
                            obj = (cLine)DrawObjects[x1].Obj;

                            if (pplineobjname == obj.LineName)
                            {
                                pplineobjname = "";

                                goto x2;
                            }
                        }
                    x2:;

                    }

                    try
                    {
                        obj.X1 = PointList[0].X;
                        obj.Y1 = PointList[0].Y;
                    }
                    catch
                    {
                    }

                    try
                    {
                        obj.X2 = PointList[1].X;
                        obj.Y2 = PointList[1].Y;

                        //obj.MObj.X2 = obj.X2;
                        //obj.MObj.Y2 = obj.Y2;

                      //  PointList.Clear();

                    }
                    catch
                    {

                    }


                    if (PointList.Count == 1)
                    {
                        MaxCurveNumber = MaxCurveNumber + 1;
                        obj.LineName = "CR" + MaxCurveNumber.ToString();
                        if (obj.X2 == 0)
                            pplineobjname = obj.LineName;

                        //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                        obj.DrawWidth = com.pwidth;
                        obj.DrawColor = com.pcolor;

                        obj.Showlength = ShowLength;
                        obj.Showcircum = ShowCirm;
                        obj.Showarea = ShowArea;
                        obj.Showdia = ShowDia;
                        obj.Showrad = ShowRad;
                        obj.Showangle = ShowAngle;

                        obj.PointList = new List<Point>();
                       
                        foreach (Point pp in PointList)
                        {
                            obj.PointList.Add(pp);
                        }


                        EndLcation = e.Location;
                        IsMouseDown = false;
                        Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

                        cLine objt = new cLine();
                        objt.X2 = TPoint.X;
                        objt.Y2 = TPoint.Y;

                        objt.LineName = "T-" + new Random().Next(100000, 999999).ToString();
                        //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                        objt.DrawWidth = com.pwidth;
                        objt.DrawColor = com.pcolor;
                        objt.Text = "";
                        obj.MObj = objt;
                        DrawObject dobj = new DrawObject();
                        dobj.ObjType = "CURVE";
                        obj.ObjType = dobj.ObjType;
                        dobj.Obj = obj;
                        DrawObjects.Add(dobj);



                    }
                    else
                    {
                        if(obj.PointList!=null)
                        { 
                        obj.PointList.Clear();
                        foreach (Point pp in PointList)
                        {
                            obj.PointList.Add(pp);
                        }
                        }
                    }

                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        isCurve = false;
                        PointList.Clear();
                    }

                }
                else if (WriteText.Length > 0)
                {
                    EndLcation = e.Location;
                    IsMouseDown = false;

                    cLine obj = new cLine();
                    obj.X2 = EndLcation.X;
                    obj.Y2 = EndLcation.Y;

                    obj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                    obj.DrawWidth = com.pwidth;
                    obj.DrawColor = com.pcolor;
                    obj.Text = WriteText;

                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "TEXT";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    DrawObjects.Add(dobj);

                }

                if (isCorp == true)
                {
                    isCorp = false;
                    MainImg = MainImg.GetSubRect(rect);
                    LoadImage();

                }


                FindIntersect();
                img1.Invalidate();
            }



        }
        Point MoveStartPoint;
        Point MoveEndPoint;
        private void img1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {

                if (isCorp == true)
                {
                    EndLcation = e.Location;
                    img1.Invalidate();

                }

                if ((isDrawLine == true) || (isMarkLine == true) || (isDrawArrowLine == true) || (isDrawRect == true) || (isCircle == true) || (isPPLine == true) || (isAngle == true) || (isDrawPoint == true))
                {
                    EndLcation = e.Location;
                    img1.Invalidate();


                }
                else if (isCurve == true)
                {
                    EndLcation = e.Location;
                  //  PointList.Add(EndLcation);
                    img1.Invalidate();

                }
                else if (isMarkArc == true)
                {
                    EndLcation = e.Location;
                    //  PointList.Add(EndLcation);
                    img1.Invalidate();

                }
                else if (isCCurve == true)
                {
                    EndLcation = e.Location;
                  //  PointList.Add(EndLcation);
                    img1.Invalidate();

                }
                else if (isMoveLine == true)
                {
                    if (SelectedLine == null)
                        return;

                    String ObjType = SelectedLine.ObjType;

                    if (ObjType == "ANGLE")
                    {
                        int n1 = e.Location.X - MouseStart.X;
                        int n2 = e.Location.Y - MouseStart.Y;

                        int i = 0;
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = new Point(SelectedLine.PointList[i].X + n1, SelectedLine.PointList[i].Y + n2);
                            SelectedLine.PointList[i] = pp;
                        }

                        Point TPoint1 = new Point(SelectedLine.PointList[1].X + 5, SelectedLine.PointList[1].Y + 5);
                        SelectedLine.MObj.X2 = TPoint1.X;
                        SelectedLine.MObj.Y2 = TPoint1.Y;

                        MouseStart = e.Location;
                        img1.Invalidate();
                        return;
                    }
                    else if (ObjType == "CURVE")
                    {
                        int n1 = e.Location.X - MouseStart.X;
                        int n2 = e.Location.Y - MouseStart.Y;

                        int i = 0;
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = new Point(SelectedLine.PointList[i].X + n1, SelectedLine.PointList[i].Y + n2);
                            SelectedLine.PointList[i] = pp;
                        }

                        Point TPoint1 = new Point(SelectedLine.PointList[0].X + 5, SelectedLine.PointList[0].Y + 5);
                        if(SelectedLine.MObj!=null)
                        { 
                        SelectedLine.MObj.X2 = TPoint1.X;
                        SelectedLine.MObj.Y2 = TPoint1.Y;
                        }

                        MouseStart = e.Location;
                        img1.Invalidate();
                        return;
                    }

                    else if (ObjType == "ARC")
                    {
                        int n1 = e.Location.X - MouseStart.X;
                        int n2 = e.Location.Y - MouseStart.Y;

                        int i = 0;
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = new Point(SelectedLine.PointList[i].X + n1, SelectedLine.PointList[i].Y + n2);
                            SelectedLine.PointList[i] = pp;
                        }

                        Point TPoint1 = new Point(SelectedLine.PointList[0].X + 5, SelectedLine.PointList[0].Y + 5);

                        if (SelectedLine.MObj != null)
                        {
                            SelectedLine.MObj.X2 = TPoint1.X;
                            SelectedLine.MObj.Y2 = TPoint1.Y;
                        }
                        Point p1, p2, p3;

                        try
                        {
                            p1 = SelectedLine.PointList[0];
                        }
                        catch
                        {
                            p1 = new Point(0, 0);
                        }

                        try
                        {
                            p2 = SelectedLine.PointList[1];
                        }
                        catch
                        {
                            p2 = new Point(0, 0);
                        }

                        try
                        {
                            p3 = SelectedLine.PointList[2];
                        }
                        catch
                        {
                            p3 = new Point(0, 0);
                        }

                        Point obj_Center = getArcCenter(p1, p2, p3);
                        SelectedLine.X1 = obj_Center.X;
                        SelectedLine.Y1 = obj_Center.Y;

                        MouseStart = e.Location;
                        img1.Invalidate();
                        return;
                    }

                    else if (ObjType == "TEXT")
                    {
                        int n1 = e.Location.X - MouseStart.X;
                        int n2 = e.Location.Y - MouseStart.Y;

                        SelectedLine.X2 = SelectedLine.X2 + n1;
                        SelectedLine.Y2 = SelectedLine.Y2 + n2;



                        MouseStart = e.Location;
                        img1.Invalidate();
                        return;
                    }
                    else if (ObjType == "CCURVE")
                    {
                        int n1 = e.Location.X - MouseStart.X;
                        int n2 = e.Location.Y - MouseStart.Y;

                        int i = 0;
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = new Point(SelectedLine.PointList[i].X + n1, SelectedLine.PointList[i].Y + n2);
                            SelectedLine.PointList[i] = pp;
                        }
                        if(SelectedLine.MObj!=null)
                        {
                            Point TPoint1 = new Point(SelectedLine.PointList[0].X + 5, SelectedLine.PointList[0].Y + 5);
                            SelectedLine.MObj.X2 = TPoint1.X;
                        SelectedLine.MObj.Y2 = TPoint1.Y;
                        }
                        MouseStart = e.Location;
                        img1.Invalidate();
                        return;
                    }


                    int num1 = e.Location.X - MouseStart.X;
                    int num2 = e.Location.Y - MouseStart.Y;

                    SelectedLine.X1 = SelectedLine.X1 + num1;
                    SelectedLine.Y1 = SelectedLine.Y1 + num2;

                    SelectedLine.X2 = SelectedLine.X2 + num1;
                    SelectedLine.Y2 = SelectedLine.Y2 + num2;

                    MoveStartPoint.X = SelectedLine.X1;
                    MoveStartPoint.Y = SelectedLine.Y1;

                    MoveEndPoint.X = SelectedLine.X2;
                    MoveEndPoint.Y = SelectedLine.Y2;

                    if (SelectedLine.MObj != null)
                    { 
                        SelectedLine.MObj.X2 = SelectedLine.MObj.X2 + num1;
                        SelectedLine.MObj.Y2 = SelectedLine.MObj.Y2 + num2;

                    }
                    MouseStart = e.Location;

                    img1.Invalidate();
                }
                else if (isReSizeLine == true)
                {
                    int num1 = e.Location.X - MouseStart.X;
                    int num2 = e.Location.Y - MouseStart.Y;

                    if (com.isCalibrationOpen == false)
                    {
                        SelectedLine.X1 = SelectedLine.X1;
                        SelectedLine.Y1 = SelectedLine.Y1;

                        SelectedLine.X2 = SelectedLine.X2 + num1;
                        SelectedLine.Y2 = SelectedLine.Y2 + num2;

                        MoveStartPoint.X = SelectedLine.X1;
                        MoveStartPoint.Y = SelectedLine.Y1;

                        MoveEndPoint.X = SelectedLine.X2;
                        MoveEndPoint.Y = SelectedLine.Y2;

                        if (SelectedLine.MObj != null)
                        {
                            SelectedLine.MObj.X2 = SelectedLine.MObj.X2 + num1;
                            SelectedLine.MObj.Y2 = SelectedLine.MObj.Y2 + num2;
                        }
                    }
                    else
                    {
                        if (img1.Cursor == Cursors.PanEast)
                        {
                            SelectedLine.X1 = SelectedLine.X1;
                            SelectedLine.Y1 = SelectedLine.Y1;

                            SelectedLine.X2 = SelectedLine.X2 + num1;
                            SelectedLine.Y2 = SelectedLine.Y1 + num2;

                            MoveStartPoint.X = SelectedLine.X1;
                            MoveStartPoint.Y = SelectedLine.Y1;

                            MoveEndPoint.X = SelectedLine.X2;
                            MoveEndPoint.Y = SelectedLine.Y2;
                        }
                        else if (img1.Cursor == Cursors.PanWest)
                        {
                            SelectedLine.X1 = SelectedLine.X1 + num1;
                            SelectedLine.Y1 = SelectedLine.Y1;

                            SelectedLine.X2 = SelectedLine.X2;
                            SelectedLine.Y2 = SelectedLine.Y2;

                            MoveStartPoint.X = SelectedLine.X1;
                            MoveStartPoint.Y = SelectedLine.Y1;

                            MoveEndPoint.X = SelectedLine.X2;
                            MoveEndPoint.Y = SelectedLine.Y2;
                        }
                    }
                    MouseStart = e.Location;

                    img1.Invalidate();
                }

            }
            else
            {
                int i;
                int handcount = 0;

                for (i = 0; i <= DrawObjects.Count - 1; i++)
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;


                    if ((DrawObjects[i].ObjType == "LINE") || (DrawObjects[i].ObjType == "MLINE")    || (DrawObjects[i].ObjType == "M1LINE") || (DrawObjects[i].ObjType == "ARROW") ||  (DrawObjects[i].ObjType == "PPLINE"))
                    {
                      
                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (IsOnLine(p1, p2, e.Location, DrawPen, objl.LineName) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                if (com.isCalibrationOpen == false)
                                    img1.Cursor = Cursors.PanSE;
                                else
                                    img1.Cursor = Cursors.PanEast;

                                CurrentObjectName = objl.LineName;
                              
                                return;
                            }
                            else if ((isOnStartOfLine(e.Location, new Point(objl.X1, objl.Y1))) && (com.isCalibrationOpen == true))
                            {
                             
                                img1.Cursor = Cursors.PanWest;
                                CurrentObjectName = objl.LineName;
                                return;
                                
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }

                    }
                    else if (DrawObjects[i].ObjType == "RECT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        int w = objl.Width;
                        int h = objl.Height;
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (IsOnRect(p1, p2, e.Location, DrawPen,w, h,objl.LineName) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }



                    else if (DrawObjects[i].ObjType == "CIRCLE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        int w = objl.Width;
                        int h = objl.Height;
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (IsOnCircle(p1, p2, e.Location,DrawPen, w, h, objl.LineName) == true)
                        {


                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }



                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                          
                        }
                        
                    }
                    else if (DrawObjects[i].ObjType == "POINT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p2 = new Point(objl.X2, objl.Y2);
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (isOnPoint( p2, e.Location, DrawPen, objl.LineName) == true)
                        {


                         
                            
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            



                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;

                        }

                    }


                    else if (DrawObjects[i].ObjType == "ARC")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        int w = objl.Width;
                        int h = objl.Height;
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (isOnArc(objl.PointList, e.Location, DrawPen, objl.LineName) == true)
                        {
                               img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            
                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }

                    else if (DrawObjects[i].ObjType == "CURVE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        int w = objl.Width;
                        int h = objl.Height;
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (isOnCurve(objl.PointList, e.Location,DrawPen, objl.LineName) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }


                    else if (DrawObjects[i].ObjType == "CCURVE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Point p1 = new Point(objl.X1, objl.Y1);
                        Point p2 = new Point(objl.X2, objl.Y2);
                        int w = objl.Width;
                        int h = objl.Height;
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (isOnCCurve(objl.PointList, e.Location, DrawPen, objl.LineName) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }

                    else if (DrawObjects[i].ObjType == "ANGLE")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;

                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);


                        if (isOnAngle(objl.PointList, e.Location, DrawPen, objl.LineName) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            //CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }

                    else if (DrawObjects[i].ObjType == "TEXT")
                    {
                        //cLine objl = (cLine)DrawObjects[i].Obj;


                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        if (isOnText(objl.Text, new Point(objl.X2, objl.Y2), e.Location, DrawPen) == true)
                        {
                            if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                            {
                                img1.Cursor = Cursors.PanSE;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                            else
                            {
                                img1.Cursor = Cursors.Hand;
                                CurrentObjectName = objl.LineName;
                                return;
                            }
                        }
                        else
                        {
                            //CurrentObjectName = "";
                            img1.Cursor = Cursors.Default;
                        }
                    }

                    if (GetMObj(objl, e)==true)
                    return;

                }
                EndLcation = e.Location;
                img1.Invalidate();
            }


            if(com.CurrentWork=="IM")
            { 
            MDIParent1 mp = (MDIParent1)this.MdiParent;

                if(mp.kryptonPanel1.Visible==true)
                { 
            int ZoomVal = 100 - (int)mp.TBMagIM.Value;

            Point s1 = new Point(e.X - ZoomVal, e.Y - ZoomVal);
            int width = ZoomVal * 2;
            int height = ZoomVal * 2;

            try
            {
                Rectangle RET = new Rectangle(s1.X, s1.Y, width, height);
                Image<Bgr, Byte> img;
                Bitmap bm = (Bitmap)img1.Image;

                img = bm.ToImage<Bgr, Byte>();
                img = img.GetSubRect(RET);
                mp.imgMagIM.Image = img.ToBitmap();

                   

            }
            catch { }

            }

            }

            if (com.CurrentWork == "WM")
            {
                MDIParent1 mp = (MDIParent1)this.MdiParent;
                if (mp.kryptonPanel2.Visible == true)
                {
                    int ZoomVal = 100 - (int)mp.TBMagWM.Value;

                    Point s1 = new Point(e.X - ZoomVal, e.Y - ZoomVal);
                    int width = ZoomVal * 2;
                    int height = ZoomVal * 2;

                    try
                    {
                        Rectangle RET = new Rectangle(s1.X, s1.Y, width, height);
                        Image<Bgr, Byte> img;
                        Bitmap bm = (Bitmap)img1.Image;
                        img = bm.ToImage<Bgr, Byte>();
                        img = img.GetSubRect(RET);
                        mp.imgMagWM.Image = img.ToBitmap();

                    }
                    catch { }
                }
            }

        }


        private bool GetMObj(cLine obj, MouseEventArgs e)
        {
            cLine objl = (cLine)obj.MObj;

            if (objl == null)
                return false;

            Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);


            if (isOnText(objl.Text, new Point(objl.X2, objl.Y2), e.Location, DrawPen) == true)
            {
                if (isOnEndOfLine(e.Location, new Point(objl.X2, objl.Y2)))
                {
                    img1.Cursor = Cursors.PanSE;
                    CurrentObjectName = objl.LineName;
                    return true;
                }
                else
                {
                    img1.Cursor = Cursors.Hand;
                    CurrentObjectName = objl.LineName;
                    return true;
                }
            }
            else
            {
                //CurrentObjectName = "";
                img1.Cursor = Cursors.Default;
                return false;
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
            isReSizeLine = false;
            isMoveLine = false;
            isPPLine = false;
            isDrawPoint = false;

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
        private bool ObjLoad(string MType, string ObjType)
        {
            return true;

            //            "LENGTH", "ANGLE", "DEPTH", "LEG", "THICKNESS", "MIN THICKNESS", "THROAT", "ROOT PENETRATION", "GAP", "UNDERCUT",
            //            "MELT THROUGH", "%PENETRATION", "AREA", "DIAMETER", "RADIUS", "CIRCUMFERENCE"
            bool rtn = false;

            if ((MType == "LENGTH") || (MType == "DEPTH") || (MType == "LEG") || (MType == "THICKNESS") || (MType == "MIN THICKNESS") || (MType == "THROAT") || (MType == "GAP") || (MType == "UNDERCUT") || (MType == "ANGLE"))
            {
                if (ObjType == "LINE")
                    return true;
                else if (ObjType == "PPLINE")
                    return true;
                else if (ObjType == "POINT")
                    return true;
                if (ObjType == "CIRCLE")
                    return true;
                if (ObjType == "RECT")
                    return true;
                if (ObjType == "ANGLE")
                    return true;
                else
                    return false;
            }

            else if ((MType == "AREA"))
            {
                if (ObjType == "CIRCLE")
                    return true;
                else if (ObjType == "RECT")
                    return true;
                else
                    return false;
            }

            else if ((MType == "DIAMETER"))
            {
                if (ObjType == "CIRCLE")
                    return true;
                else
                    return false;
            }
            else if ((MType == "RADIUS"))
            {
                if (ObjType == "CIRCLE")
                    return true;
                else
                    return false;
            }
            else if ((MType == "CIRCUMFERENCE"))
            {
                if (ObjType == "RECT")
                    return true;
                else if (ObjType == "CIRCLE")
                    return true;
                else
                    return false;
            }
            else if ((MType == "ANGLE"))
            {
                if (ObjType == "ANGLE")
                    return true;
                else
                    return false;
            }
            return rtn;
        }
        private void LoadObjCombo()
        {

            string MType = "";

            MDIParent1 mp = (MDIParent1)this.MdiParent;
            mp.cmbObj1.Items.Clear();
            mp.cmbObj2.Items.Clear();

            mp.cmbObj1.Items.Add("");
            mp.cmbObj2.Items.Add("");
            int i;

            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine o = (cLine)DrawObjects[i].Obj;

               


                if (o.ObjType == "LINE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        if (MType == "ANGLE")
                        {
                            mp.cmbObj1.Items.Add(o.LineName);
                            mp.cmbObj2.Items.Add(o.LineName);
                        }
                        else
                        {
                            mp.cmbObj1.Items.Add(o.LineName);
                            mp.cmbObj1.Items.Add(o.LineName + "-START");
                            mp.cmbObj1.Items.Add(o.LineName + "-END");

                            mp.cmbObj2.Items.Add(o.LineName);
                            mp.cmbObj2.Items.Add(o.LineName + "-START");
                            mp.cmbObj2.Items.Add(o.LineName + "-END");
                        }
                    }
                }
                else if (o.ObjType == "PPLINE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        if (MType == "ANGLE")
                        {
                            mp.cmbObj1.Items.Add(o.LineName);
                            mp.cmbObj2.Items.Add(o.LineName);
                        }
                        else
                        {
                            mp.cmbObj1.Items.Add(o.LineName);
                            mp.cmbObj1.Items.Add(o.LineName + "-START");
                            mp.cmbObj1.Items.Add(o.LineName + "-END");

                            mp.cmbObj2.Items.Add(o.LineName);
                            mp.cmbObj2.Items.Add(o.LineName + "-START");
                            mp.cmbObj2.Items.Add(o.LineName + "-END");
                        }
                    }
                }
                else if (o.ObjType == "POINT")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                        mp.cmbObj2.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CIRCLE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                        mp.cmbObj1.Items.Add(o.LineName + "-CENTER");

                        if ((MType != "AREA") && (MType != "DIAMETER") && (MType != "CIRCUMFERENCE") & (MType != "RADIUS"))
                        {
                            mp.cmbObj2.Items.Add(o.LineName);
                            mp.cmbObj2.Items.Add(o.LineName + "-CENTER");

                        }

                    }
                }
                else if (o.ObjType == "RECT")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);

                        if ((MType != "AREA") && (MType != "DIAMETER") && (MType != "CIRCUMFERENCE") && (MType != "RADIUS"))
                        {
                            mp.cmbObj2.Items.Add(o.LineName);


                        }
                    }
                }
                else if (o.ObjType == "ANGLE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CURVE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CCURVE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "ARC")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                        mp.cmbObj2.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CURVE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CCURVE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        mp.cmbObj1.Items.Add(o.LineName);
                    }
                }

            }


        }
        public void ReDrawIM(bool Cancel, Graphics e = null)
        {
            if (Cancel == true)
                return;

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }
            int i;
            Graphics g;

            if (e == null)
                g = img1.CreateGraphics();
            else
                g = e;

            GraphicsPath gp = new GraphicsPath();
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                gp = new GraphicsPath();
                if ((DrawObjects[i].ObjType == "LINE"))
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    if (com.isCalibrationOpen == false)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, p1, p2);

                        gp.AddLine(p1, p2);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);


                        Point mp = new Point(p2.X + 5, p2.Y + 5);
                        string MeasureTxt = "";
                        if (objl.Showlength == true)
                            MeasureTxt = Math.Round((objl.GetLength(xValue, yValue)/1000.0),3).ToString() + " " + com.PubScaleShort + "\n";

                       // g.DrawString(MeasureTxt, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                        if (objl.MObj != null)
                        {
                            cLine xobj = objl.MObj;
                            xobj.Text=MeasureTxt;
                            Brush br1 = new SolidBrush(xobj.DrawColor);
                            g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));
                        }



                    }
                    else
                    {
                        Point EndPoint = new Point(p2.X, p1.Y);
                        EndPoint = p2;
                        Pen np1 = new Pen(objl.DrawColor, objl.DrawWidth);
                        Point TPoint = new Point(EndPoint.X + 5, EndPoint.Y + 5);
                        string Distance = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y2).ToString();
                        PubDistance = Distance;
                        g.DrawLine(np1, p1, EndPoint);


                        g.DrawLine(np1, new Point(p1.X, p1.Y - 10), new Point(p1.X, p1.Y + 10));
                        g.DrawLine(np1, new Point(EndPoint.X, EndPoint.Y - 10), new Point(EndPoint.X, EndPoint.Y + 10));


                        g.DrawString(Distance.ToString() + " px", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    }

                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "PPLINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);


                    int radius = 3;
                    SolidBrush br11 = new SolidBrush(objl.DrawColor);

                    g.FillEllipse(br11, objl.X1 - radius, objl.Y1 - radius, radius + radius, radius + radius);

                    g.Flush();
                    g.Save();


                    Point p2;

                    try
                    {
                        p2 = new Point(objl.X2, objl.Y2);

                        if ((p2.X == 0) && (p2.Y == 0))
                            continue;


                        int radius1 = 3;
                        SolidBrush br21 = new SolidBrush(objl.DrawColor);

                        g.FillEllipse(br21, objl.X2 - radius1, objl.Y2 - radius1, radius1 + radius1, radius1 + radius1);

                        g.Flush();
                        g.Save();
                    }
                    catch
                    {
                        continue;
                    }



                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    Point mp = new Point(p2.X + 5, p2.Y + 5);

                    string MeasureTxt = "";
                    if (objl.Showlength == true)
                        MeasureTxt = Math.Round((objl.GetLength(xValue, yValue) / 1000.0),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                        //g.DrawString(MeasureTxt, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                    if (objl.MObj != null)
                    {
                        cLine xobj = objl.MObj;
                        xobj.Text = MeasureTxt;
                        Brush br1 = new SolidBrush(xobj.DrawColor);
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                    }


                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "M1LINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);

                    Point p1copy = new Point(objl.X1Copy, objl.Y1Copy);
                    Point p2copy = new Point(objl.X2Copy, objl.Y2Copy);

                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    DrawPen.DashStyle = objl.LineStyle;

                    Brush br = new SolidBrush(DrawPen.Color);
                    g.DrawLine(DrawPen, p1, p2);
                    g.DrawLine(DrawPen, p1, p1copy);
                    g.DrawLine(DrawPen, p2, p2copy);

                    cLine mobj = objl.MObj;

                    if (mobj != null)
                    {
                        //objl.MObj.Text = Distance.ToString();
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 8, FontStyle.Bold), br, new Point(mobj.X2, mobj.Y2));
                    }

                    g.Flush();
                    g.Save();
                }

                else if (DrawObjects[i].ObjType == "MLINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    DrawPen.DashStyle = objl.LineStyle;

                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    cLine mobj = objl.MObj;

                    if (mobj != null)
                    {
                        //objl.MObj.Text = Distance.ToString();
                        Brush br1 = new SolidBrush(mobj.DrawColor);
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 8, FontStyle.Bold), br1, new Point(mobj.X2, mobj.Y2));
                    }

                    g.Flush();
                    g.Save();
                }

                else if (DrawObjects[i].ObjType == "ARROW")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    DrawPen.CustomEndCap = objl.EndCap;
                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    Point mp = new Point(p2.X + 5, p2.Y + 5);
                    //g.DrawString(objl.LineName, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                    string MeasureTxt = "";
                    if (objl.Showlength == true)
                        MeasureTxt = Math.Round((objl.GetLength(xValue, yValue) / 1000.0),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";

                    //g.DrawString(MeasureTxt, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                    if (objl.MObj != null)
                    {
                        cLine xobj = objl.MObj;
                        xobj.Text = MeasureTxt;
                        Brush br1 = new SolidBrush(xobj.DrawColor);
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                    }

                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "ARC")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    Point p1, p2, p3;

                    try
                    {
                        p1 = objl.PointList[0];
                    }
                    catch
                    {
                        p1 = new Point(0, 0);
                    }

                    try
                    {
                        p2 = objl.PointList[1];
                    }
                    catch
                    {
                        p2 = new Point(0, 0);
                    }

                    try
                    {
                        p3 = objl.PointList[2];
                    }
                    catch
                    {
                        p3 = new Point(0, 0);
                    }




                    if ((p1.X > 0) && (p2.X > 0) && (p3.X == 0))
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, p1, p2);
                        Point mp = new Point(p2.X + 5, p2.Y + 5);
                       
                        g.Flush();
                        g.Save();
                    }
                    else if ((p1.X > 0) && (p2.X > 0) && (p3.X > 0))
                    {

                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        Point obj_Center;
                        Rectangle arc_Rect;
                        float start_Angle;
                        float sweep_Angle;


                        arc_Start_Pt = p1;
                        arc_Mid_Pt = p2;
                        arc_End_Pt = p3;



                        obj_Center = getArcCenter(arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                        arc_Rect = get_ArcRectangle(obj_Center, obj_Radius);
                        start_Angle = get_Start_Angle(obj_Center, arc_Start_Pt);
                        sweep_Angle = get_Sweep_Angle(obj_Center, arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                        g.DrawArc(DrawPen, arc_Rect, start_Angle, sweep_Angle);


                        gp.AddArc( arc_Rect, start_Angle, sweep_Angle);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);


                        objl.Arc_Radius = obj_Radius;
                        objl.Arc_StartAngle = start_Angle;
                        objl.Arc_SweepAngle = sweep_Angle;
                        objl.X1 = obj_Center.X;
                        objl.Y1 = obj_Center.Y;
                        objl.Arc_Rectangle = arc_Rect;

                        Point mp = new Point(p2.X + 5, p2.Y + 5);
                        string MeasureTxt = "";
                        if (objl.Showlength == true)
                            MeasureTxt = Math.Round((objl.ArcLength(xValue, yValue)),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                    
                        if (objl.MObj != null)
                        {
                            cLine xobj = objl.MObj;
                            xobj.Text = MeasureTxt;
                            g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                        }
                        g.Flush();
                        g.Save();

                    }
                }
                else if (DrawObjects[i].ObjType == "RECT")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    rect = new Rectangle();
                    rect.X = Math.Min(objl.X1, objl.X2);
                    rect.Y = Math.Min(objl.Y1, objl.Y2);
                    rect.Width = Math.Abs(objl.X1 - objl.X2);
                    rect.Height = Math.Abs(objl.Y1 - objl.Y2);
                    
                    Point mp = new Point(objl.X2 + 5, objl.Y2 + 5);
                    string MeasureTxt = "";
                    if (objl.Showarea == true)
                        MeasureTxt = "Area: " + Math.Round((objl.GetRectArea(xValue, yValue)),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                    if(objl.Showcircum==true)
                        MeasureTxt = MeasureTxt+"Circum: "+ Math.Round((objl.GetRectCicum(xValue, yValue)),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";

                    //g.DrawString(MeasureTxt, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                    if (objl.MObj != null)
                    {
                        cLine xobj = objl.MObj;
                        xobj.Text = MeasureTxt;
                        Brush br1 = new SolidBrush(xobj.DrawColor);
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                    }


                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    g.DrawRectangle(DrawPen, rect);

                    gp.AddRectangle(rect);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);

                    g.Flush();
                    g.Save();

                }
                else if (DrawObjects[i].ObjType == "CIRCLE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    rect = new Rectangle();
                    rect.X = Math.Min(objl.X1, objl.X2);
                    rect.Y = Math.Min(objl.Y1, objl.Y2);
                    rect.Width = Math.Abs(objl.X1 - objl.X2);
                    rect.Height = Math.Abs(objl.Y1 - objl.Y2);
                    int l = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y1);
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    com.DrawCircle(g, DrawPen, objl.X1, objl.Y1, l);

                    int radius = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y1);
                    gp.AddEllipse(objl.X1 - radius, objl.Y1 - radius, radius + radius, radius + radius);

                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    Point mp = new Point(objl.X2 + 5, objl.Y2 + 5);
                    string MeasureTxt = "";
                    if (objl.Showarea == true)
                        MeasureTxt = "Area: "+ Math.Round((objl.GetCircleArea(xValue, yValue) ),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                    if (objl.Showcircum == true)
                        MeasureTxt = MeasureTxt + "Circum: " +  Math.Round((objl.GetCircleCircum(xValue, yValue) ),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                    if (objl.Showdia == true)
                        MeasureTxt = MeasureTxt + "Dia: " + Math.Round(((objl.GetRadius(xValue, yValue)*2) / 1000.0),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";
                    if (objl.Showrad == true)
                        MeasureTxt = MeasureTxt + "Radius: " + Math.Round(((objl.GetRadius(xValue, yValue)) / 1000.0),MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";

                    //g.DrawString(MeasureTxt, new Font("Verdana", 10, FontStyle.Bold), br, mp);

                    if (objl.MObj != null)
                    {
                        cLine xobj = objl.MObj;
                        xobj.Text = MeasureTxt;
                        Brush br1 = new SolidBrush(xobj.DrawColor);
                        g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                    }


                    g.Flush();
                    g.Save();

                }

                else if (DrawObjects[i].ObjType == "POINT")
                {



                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    int radius = 3;
                    SolidBrush br1 = new SolidBrush(objl.DrawColor);

                    g.FillEllipse(br1, objl.X2 - radius, objl.Y2 - radius, radius + radius, radius + radius);
                   
                    g.Flush();
                    g.Save();

                    cLine mobj = objl.MObj;

                }

                else if (DrawObjects[i].ObjType == "CURVE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);

                    Brush br = new SolidBrush(objl.DrawColor);

                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length > 1)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawCurve(DrawPen, points);


                        gp.AddCurve(points);

                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);

                        //int x1;
                        //for (x1 = 0; x1 <= objl.PathPoints.Count - 1; x1++)
                        //{
                        //    int radius = 3;
                        //    SolidBrush br11 = new SolidBrush(Color.Black);

                        //    g.FillEllipse(br11, objl.PathPoints[x1].X - radius, objl.PathPoints[x1].Y - radius, radius + radius, radius + radius);


                        //}

                        Point mp = new Point(points[points.Length-1].X + 5, points[points.Length - 1].Y + 5);
                        string MeasureTxt = "";
                        if (objl.Showlength == true)
                            MeasureTxt = Math.Round((objl.CurLength(xValue, yValue)), MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";

                        if (objl.MObj != null)
                        {
                            cLine xobj = objl.MObj;
                            xobj.Text = MeasureTxt;
                            g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                        }

                        g.Flush();
                        g.Save();
                    }
                }

                else if (DrawObjects[i].ObjType == "CCURVE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);

                    Brush br = new SolidBrush(objl.DrawColor);

                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length > 1)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                            g.DrawCurve(DrawPen, points);
                        gp.AddCurve(points);

                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);

                        Point mp = new Point(points[points.Length - 1].X + 5, points[points.Length - 1].Y + 5);
                        string MeasureTxt = "";
                        if (objl.Showlength == true)
                            MeasureTxt = Math.Round((objl.CurLength(xValue, yValue)), MySettings.DPoint).ToString() + " " + com.PubScaleShort + "\n";

                        if (objl.MObj != null)
                        {
                            cLine xobj = objl.MObj;
                            xobj.Text = MeasureTxt;
                            g.DrawString(objl.MObj.Text, new Font("Verdana", 10, FontStyle.Bold), br1, new Point(xobj.X2, xobj.Y2));

                        }


                        g.Flush();
                        g.Save();
                    }
                }

                else if (DrawObjects[i].ObjType == "ANGLE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];
                    cLine mobj = objl.MObj;
                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length == 3)
                    {
                        Point P1 = objl.PointList[0];
                        Point P2 = objl.PointList[1];
                        Point P3 = objl.PointList[2];
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, P1, P2);
                        g.DrawLine(DrawPen, P2, P3);

                        gp.AddLine(P1, P2);
                        gp.AddLine(P2, P3);

                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);


                        Point mp = new Point(P2.X + 5, P2.Y + 5);
                        if(objl.Showangle==true)
                        g.DrawString(objl.GetAngle().ToString() + "°", new Font("Verdana", 10, FontStyle.Bold), br1, mp);

                        g.Flush();
                        g.Save();



                        double theta1 = Math.Atan2(P1.Y - P2.Y, P1.X - P2.X);
                        double theta2 = Math.Atan2(P2.Y - P3.Y, P2.X - P3.X);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);

                        Brush br = new SolidBrush(objl.DrawColor);
                        string Angle = "";

                        if (objl.Showlength == true)
                        {
                            Angle = angle.ToString() + "° ";
                        }
                        if (mobj != null)
                        {
                            //mobj.Text = Angle;
                            Brush br2 = new SolidBrush(mobj.DrawColor);
                            g.DrawString(mobj.Text, new Font("Verdana", 8, FontStyle.Bold), br2, new Point(mobj.X2, mobj.Y2));
                        }
                           
                    }
                }

                else if (DrawObjects[i].ObjType == "TEXT")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    string str = objl.Text;
                    Point TPoint = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    g.DrawString(str, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    g.Flush();
                    g.Save();



                }
            }


            if (isDateFound() == false)
            {

                cLine obj = new cLine();

                obj.X1 = 0;
                obj.Y1 = 0;

                obj.X2 = 10;
                obj.Y2 = img1.Height - 30;


                obj.LineName = "DT" + new Random().Next(100000, 999999).ToString();
                //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                obj.DrawWidth = com.pwidth;
                obj.DrawColor = com.pcolor;
                obj.Text = PubDate.ToString("dd-MM-yyyy hh:mm tt");

                DrawObject dobj = new DrawObject();
                dobj.ObjType = "TEXT";
                obj.ObjType = dobj.ObjType;
                dobj.Obj = obj;
                DrawObjects.Add(dobj);
            }



            LoadObjCombo();

        }
        float obj_Radius;
        public Point getArcCenter(System.Drawing.Point pt1, System.Drawing.Point pt2, System.Drawing.Point pt3)
        {
            Point obj_Center;

            double num = pt1.X;
            double num2 = pt1.Y;
            double num3 = pt2.X;
            double num4 = pt2.Y;
            double num5 = pt3.X;
            double num6 = pt3.Y;
            double num7 = (num + num3) / 2.0;
            double num8 = (num2 + num4) / 2.0;
            double num9 = -2.0;
            double num10 = (num3 + num5) / 2.0;
            double num11 = (num4 + num6) / 2.0;
            double num16 = (num5 + num) / 2.0;
            double num17 = (num6 + num2) / 2.0;
            if (num4 - num2 != 0.0 && num2 - num6 != 0.0 && num6 - num4 != 0.0)
            {
                if (num3 - num != 0.0 && num4 - num2 != 0.0)
                {
                    num9 = 1.0 / ((num4 - num2) / (num3 - num)) * -1.0;
                }
                else if (num3 - num == 0.0 || num4 - num2 == 0.0)
                {
                    num9 = 0.0;
                }
                double num12 = default(double);
                if (num5 - num3 != 0.0 && num6 - num4 != 0.0)
                {
                    num12 = 1.0 / ((num6 - num4) / (num5 - num3)) * -1.0;
                }
                else if (num5 - num3 == 0.0 || num6 - num4 == 0.0)
                {
                    num12 = 0.0;
                }
                if (num - num5 != 0.0 && num2 - num6 != 0.0)
                {
                    double num18 = 1.0 / ((num2 - num6) / (num - num5));
                }
                else
                {
                    bool flag = num - num5 == 0.0 || num2 - num6 == 0.0;
                }
                double num13 = (num10 * num12 - num11 - num7 * num9 + num8) / (num12 - num9);
                double num14 = num9 * num13 - num7 * num9 + num8;
                double num15 = System.Math.Pow(System.Math.Pow(System.Math.Abs(num13 - num), 2.0) + System.Math.Pow(System.Math.Abs(num14 - num2), 2.0), 0.5);
                obj_Center = checked(new System.Drawing.Point((int)System.Math.Round(num13), (int)System.Math.Round(num14)));
                obj_Radius = (float)num15;
                return obj_Center;
            }
            else
            {
                Microsoft.VisualBasic.Interaction.MsgBox("Atleast one set of points fall on straight line. Please select points again");
                obj_Center = new System.Drawing.Point(0, 0);
                obj_Radius = 0f;
                return obj_Center;
            }
        }

        public Rectangle get_ArcRectangle(Point obj_Center, float obj_Radius)
        {
            Rectangle arc_Rect;

            float num = (float)obj_Center.X - obj_Radius;
            float num2 = (float)obj_Center.Y - obj_Radius;
            System.Drawing.Rectangle rectangle = (arc_Rect = checked(new System.Drawing.Rectangle((int)System.Math.Round(num), (int)System.Math.Round(num2), (int)System.Math.Round(2f * obj_Radius), (int)System.Math.Round(2f * obj_Radius))));
            return arc_Rect;
        }

        public float get_Start_Angle(System.Drawing.Point arc_C, System.Drawing.Point pt1)
        {
            float start_Angle = 0;

            float num = 0f;
            checked
            {
                if ((pt1.X > arc_C.X) & (pt1.Y < arc_C.Y))
                {
                    num = (float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt1.Y) / (double)System.Math.Abs(arc_C.X - pt1.X));
                    num = System.Math.Abs(num);
                    start_Angle = (float)((double)(num * 180f) / System.Math.PI);
                    start_Angle *= -1f;
                }
                else if ((pt1.X > arc_C.X) & (pt1.Y > arc_C.Y))
                {
                    num = (float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt1.Y) / (double)System.Math.Abs(arc_C.X - pt1.X));
                    num = num;
                    start_Angle = (float)((double)(num * 180f) / System.Math.PI);
                }
                else if ((pt1.X < arc_C.X) & (pt1.Y < arc_C.Y))
                {
                    num = (float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt1.Y) / (double)System.Math.Abs(arc_C.X - pt1.X));
                    num = num;
                    start_Angle = (float)((double)(num * 180f) / System.Math.PI);
                    start_Angle = 90f + (90f - start_Angle);
                    start_Angle *= -1f;
                }
                else if ((pt1.X < arc_C.X) & (pt1.Y > arc_C.Y))
                {
                    num = (float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt1.Y) / (double)System.Math.Abs(arc_C.X - pt1.X));
                    num = System.Math.Abs(num);
                    start_Angle = (float)((double)(num * 180f) / System.Math.PI);
                    start_Angle = 90f + (90f - start_Angle);
                }
                else if ((pt1.X > arc_C.X) & (pt1.Y == arc_C.Y))
                {
                    start_Angle = 0f;
                }
                else if ((pt1.X < arc_C.X) & (pt1.Y == arc_C.Y))
                {
                    start_Angle = -180f;
                }
                else if ((pt1.X == arc_C.X) & (pt1.Y < arc_C.Y))
                {
                    start_Angle = -90f;
                }
                else if ((pt1.X == arc_C.X) & (pt1.Y > arc_C.Y))
                {
                    start_Angle = 90f;
                }
            }

            return start_Angle;
        }
        Point arc_Start_Pt, arc_Mid_Pt, arc_End_Pt;
        public float get_Sweep_Angle(Point obj_Center, Point arc_Start_Pt, Point arc_Mid_Pt, Point arc_End_Pt)
        {
            float sweep_Angle;

            System.Drawing.Point obj_Center2 = obj_Center;
            bool flag = true;
            float num = 0f;
            get_Quadrant(obj_Center, arc_Start_Pt);
            get_Quadrant(obj_Center, arc_Mid_Pt);
            get_Quadrant(obj_Center, arc_End_Pt);
            float num2 = get_Angle_of_Point(obj_Center, arc_Start_Pt);
            float num3 = get_Angle_of_Point(obj_Center, arc_Mid_Pt);
            float num4 = get_Angle_of_Point(obj_Center, arc_End_Pt);
            float num5 = ((!(num3 > num2)) ? (360f - num2 + num3) : (num3 - num2));
            float num6 = ((!(num4 > num2)) ? (360f - num2 + num4) : (num4 - num2));
            flag = ((!(num6 < num5)) ? true : false);
            num = (sweep_Angle = ((!flag) ? ((360f - num6) * -1f) : num6));

            return sweep_Angle;

        }


        private int get_Quadrant(System.Drawing.Point mid, System.Drawing.Point pt)
        {
            int result = 0;
            if ((pt.X > mid.X) & (pt.Y > mid.Y))
            {
                result = 1;
            }
            else if ((pt.X < mid.X) & (pt.Y > mid.Y))
            {
                result = 2;
            }
            else if ((pt.X < mid.X) & (pt.Y < mid.Y))
            {
                result = 3;
            }
            else if ((pt.X > mid.X) & (pt.Y < mid.Y))
            {
                result = 4;
            }
            return result;
        }

        private float get_Angle_of_Point(System.Drawing.Point arc_C, System.Drawing.Point pt)
        {
            float result = 0f;
            checked
            {
                if ((pt.X > arc_C.X) & (pt.Y < arc_C.Y))
                {
                    result = (float)((double)(System.Math.Abs((float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt.Y) / (double)System.Math.Abs(arc_C.X - pt.X))) * 180f) / System.Math.PI);
                    result = 270f + (90f - result);
                }
                else if ((pt.X > arc_C.X) & (pt.Y > arc_C.Y))
                {
                    result = (float)((double)((float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt.Y) / (double)System.Math.Abs(arc_C.X - pt.X)) * 180f) / System.Math.PI);
                }
                else if ((pt.X < arc_C.X) & (pt.Y < arc_C.Y))
                {
                    result = (float)((double)(System.Math.Abs((float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt.Y) / (double)System.Math.Abs(arc_C.X - pt.X))) * 180f) / System.Math.PI);
                    result = 180f + result;
                }
                else if ((pt.X < arc_C.X) & (pt.Y > arc_C.Y))
                {
                    result = (float)((double)(System.Math.Abs((float)System.Math.Atan((double)System.Math.Abs(arc_C.Y - pt.Y) / (double)System.Math.Abs(arc_C.X - pt.X))) * 180f) / System.Math.PI);
                    result = 90f + (90f - result);
                }
                else if ((pt.X > arc_C.X) & (pt.Y == arc_C.Y))
                {
                    result = 0f;
                }
                else if ((pt.X == arc_C.X) & (pt.Y > arc_C.Y))
                {
                    result = 90f;
                }
                else if ((pt.X < arc_C.X) & (pt.Y == arc_C.Y))
                {
                    result = 180f;
                }
                else if ((pt.X == arc_C.X) & (pt.Y < arc_C.Y))
                {
                    result = 270f;
                }
                return result;
            }
        }


        public List<Point> getArcPoints(Point obj_Center, float obj_Radius, float start_Angle, float sweep_Angle)
        {

            List<Point> TempPointList = new List<Point>();

            double num8 = (double)obj_Radius;
            double num9 = (double)obj_Center.X;
            double num10 = (double)obj_Center.Y;
            new System.Collections.Hashtable().Clear();
            //	obj_Pts.Clear();
            int num = 0;
            float num2 = 0f;
            float num3 = 0f;
            float num4 = 0f;
            float num5 = 0f;
            num2 = ((!(start_Angle < 0f)) ? start_Angle : (360f - start_Angle * -1f));
            if (sweep_Angle < 0f)
            {
                num3 = sweep_Angle * -1f;
                num4 = -0.5f;
            }
            else
            {
                num3 = sweep_Angle;
                num4 = 0.5f;
            }
            checked
            {
                while (num5 <= num3)
                {
                    float num6 = (float)((double)obj_Center.X + (double)obj_Radius * System.Math.Cos((double)num2 * System.Math.PI / 180.0));
                    float num7 = (float)((double)obj_Center.Y + (double)obj_Radius * System.Math.Sin((double)num2 * System.Math.PI / 180.0));
                    num2 += num4;
                    if (num2 == 360f)
                    {
                        num2 = 0f;
                    }
                    num5 = (float)((double)num5 + 0.5);
                    num++;
                    TempPointList.Add(new System.Drawing.Point((int)System.Math.Round(num6), (int)System.Math.Round(num7)));
                    //obj_Pts.Add(num, );
                }
            }

            return TempPointList;
        }

      

        public void ReDraw(bool Cancel,Graphics e = null)
        {
            if (Cancel == true)
                return;

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }
            int i;
            Graphics g;

            if (e == null)
                g = img1.CreateGraphics();
            else
                g = e;

            GraphicsPath gp = new GraphicsPath();
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                gp = new GraphicsPath();
                if ((DrawObjects[i].ObjType == "LINE"))
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    if (com.isCalibrationOpen == false)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, p1, p2);
                        
                        gp.AddLine(p1, p2);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf,gp,DrawPen);

                        int xx = ((p1.X + p2.X) / 2) + 10;
                        int yy = ((p1.Y + p2.Y) / 2) - 20;

                        //Point mp = new Point(p2.X + 5, p2.Y + 5);
                        Point mp = new Point(xx+5,yy+5);


                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, mp);
                   
                            string Distance = "";
                       
                            if (mobj != null)
                            {
                                Brush br1 = new SolidBrush(mobj.DrawColor);

                         

                            //objl.MObj.Text = Distance.ToString();
                            if(MySettings.isResult==1)
                                g.DrawString(objl.MObj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, new Point(mobj.X2, mobj.Y2));
                            }
                        

                    }
                    else
                    {
                        Point EndPoint = new Point(p2.X, p1.Y);
                        EndPoint = p2;
                        Pen np1 = new Pen(objl.DrawColor, objl.DrawWidth);
                      
                      
                        Point TPoint = new Point(EndPoint.X + 5, EndPoint.Y + 5);
                        string Distance = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y2).ToString();
                        PubDistance = Distance;
                        g.DrawLine(np1, p1, EndPoint);

                        g.DrawLine(np1, new Point(p1.X,p1.Y-10), new Point(p1.X, p1.Y + 10));
                        g.DrawLine(np1, new Point(EndPoint.X, EndPoint.Y - 10), new Point(EndPoint.X, EndPoint.Y + 10));

                        g.DrawString(Distance.ToString() + " px" , new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, TPoint);

                    }
                    
                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "PPLINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);


                    int radius = 3;
                    SolidBrush br11 = new SolidBrush(objl.DrawColor);

                    g.FillEllipse(br11, objl.X1 - radius, objl.Y1 - radius, radius + radius, radius + radius);

                    g.Flush();
                    g.Save();


                    Point p2;

                    try
                    {
                        p2 = new Point(objl.X2, objl.Y2);

                        if ((p2.X == 0) && (p2.Y == 0))
                            continue;


                        int radius1 = 3;
                        SolidBrush br21 = new SolidBrush(objl.DrawColor);

                        g.FillEllipse(br21, objl.X2 - radius1, objl.Y2 - radius1, radius1 + radius1, radius1 + radius1);

                        g.Flush();
                        g.Save();
                    }
                    catch
                    {
                        continue;
                    }


                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    // Point mp = new Point(p2.X + 5, p2.Y + 5);

                    int xx = ((p1.X + p2.X) / 2) + 10;
                    int yy = ((p1.Y + p2.Y) / 2) - 20;

                    //Point mp = new Point(p2.X + 5, p2.Y + 5);
                    Point mp = new Point(xx + 5, yy + 5);

                    g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, mp);
                   
                    string Distance = "";
                   
                    if (mobj != null)
                    {

                        Brush br1 = new SolidBrush(mobj.DrawColor);
                        if (MySettings.isResult == 1)
                            g.DrawString(objl.MObj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, new Point(mobj.X2, mobj.Y2));
                    }
                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "M1LINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);

                    Point p1copy = new Point(objl.X1Copy, objl.Y1Copy);
                    Point p2copy = new Point(objl.X2Copy, objl.Y2Copy);

                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    DrawPen.DashStyle = objl.LineStyle;
                    
                    Brush br = new SolidBrush(DrawPen.Color);
                    g.DrawLine(DrawPen, p1, p2);
                    g.DrawLine(DrawPen, p1, p1copy);
                    g.DrawLine(DrawPen, p2, p2copy);

                    cLine mobj = objl.MObj;

                    if (mobj != null)
                    {
                        //objl.MObj.Text = Distance.ToString();
                        if(MySettings.isResult==1)
                            g.DrawString(objl.MObj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, new Point(mobj.X2, mobj.Y2));
                        
                    }

                    g.Flush();
                    g.Save();
                }

                else if (DrawObjects[i].ObjType == "MLINE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    DrawPen.DashStyle = objl.LineStyle;

                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);


                    cLine mobj = objl.MObj;

                    if (mobj != null)
                    {
                        //objl.MObj.Text = Distance.ToString();
                        if (MySettings.isResult == 1)
                            g.DrawString(objl.MObj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, new Point(mobj.X2, mobj.Y2));
                    }

                    g.Flush();
                    g.Save();
                }

                else if (DrawObjects[i].ObjType == "ARROW")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point p1 = new Point(objl.X1, objl.Y1);
                    Point p2 = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    DrawPen.CustomEndCap = objl.EndCap;
                    g.DrawLine(DrawPen, p1, p2);

                    gp.AddLine(p1, p2);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);



                    Point mp = new Point(p2.X + 5, p2.Y + 5);
                    g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, mp);

                    string Distance = "";
                    if (objl.Showlength == true)
                    {
                        Distance = GetDistance(p1.X * xValue, p1.Y * yValue, p2.X * xValue, p2.Y * yValue).ToString();
                    Distance= Distance.ToString() + " " + com.PubScaleShort; 
                    }
                    if (mobj != null)
                    {
                        mobj.Text = mobj.Text.ToString();
                        if (MySettings.isResult == 1)
                            g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, new Point(mobj.X2, mobj.Y2));
                    }
                    

                    g.Flush();
                    g.Save();
                }
                else if (DrawObjects[i].ObjType == "ARC")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    Point p1, p2, p3;

                    try
                    {
                        p1 = objl.PointList[0];
                            }
                    catch {
                        p1 = new Point(0, 0);
                    }

                    try
                    {
                        p2 = objl.PointList[1];
                    }
                    catch
                    {
                        p2 = new Point(0, 0);
                    }

                    try
                    {
                        p3 = objl.PointList[2];
                    }
                    catch
                    {
                        p3 = new Point(0, 0);
                    }




                    if ((p1.X > 0) && (p2.X > 0) && (p3.X == 0))
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, p1, p2);
                        Point mp = new Point(p2.X + 5, p2.Y + 5);
                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);

                        g.Flush();
                        g.Save();
                    }
                    else if((p1.X > 0) && (p2.X > 0) && (p3.X > 0))
                    {

                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        cLine mobj = objl.MObj;


                        Point obj_Center;
                        Rectangle arc_Rect;
                        float start_Angle;
                        float sweep_Angle;

                      
                        arc_Start_Pt = p1;
                        arc_Mid_Pt = p2;
                        arc_End_Pt = p3;


                        
                        obj_Center = getArcCenter(arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                        arc_Rect = get_ArcRectangle(obj_Center, obj_Radius);
                        start_Angle = get_Start_Angle(obj_Center, arc_Start_Pt);
                        sweep_Angle = get_Sweep_Angle(obj_Center, arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                        g.DrawArc(DrawPen, arc_Rect, start_Angle, sweep_Angle);
                        //getArcPoints(obj_Center, obj_Radius, start_Angle, sweep_Angle);

                        gp.AddArc(arc_Rect, start_Angle, sweep_Angle);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);


                        Point mp = new Point(p2.X + 5, p2.Y + 5);
                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);


                        if (mobj != null)
                        {
                            mobj.Text = (objl.ArcLength(xValue, yValue)).ToString();
                            Brush br2 = new SolidBrush(mobj.DrawColor);
                            if (MySettings.isResult == 1)
                                g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br2, new Point(mobj.X2, mobj.Y2));
                        }


                        g.Flush();
                        g.Save();

                    }
                }
                else if (DrawObjects[i].ObjType == "RECT")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    rect = new Rectangle();
                    rect.X = Math.Min(objl.X1, objl.X2);
                    rect.Y = Math.Min(objl.Y1, objl.Y2);
                    rect.Width = Math.Abs(objl.X1 - objl.X2);
                    rect.Height = Math.Abs(objl.Y1 - objl.Y2);
                    int l = GetDistance(objl.X1 * xValue, objl.Y1 * yValue, objl.X2 * xValue, objl.Y1 * yValue);
                    int b = GetDistance(objl.X1 * xValue, objl.Y1 * yValue, objl.X1 * xValue, objl.Y2 * yValue);


                    //string Distance = "L: " + l.ToString() + " " + com.PubScaleShort + " B: " + b.ToString() + " " + com.PubScaleShort;

                    string Distance = "";
                    
                   
                    if (mobj != null)
                    {
                        mobj.Text = mobj.Text;
                        Brush br1 = new SolidBrush(mobj.DrawColor);
                        if (MySettings.isResult == 1)
                            g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, new Point(mobj.X2, mobj.Y2));
                    }



                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                    g.DrawRectangle(DrawPen, rect);

                    gp.AddRectangle(rect);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);

                    Point mp = new Point(objl.X2 -30, objl.Y2 -20);
                    g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, mp);
                    g.Flush();
                    g.Save();

                }
                else if (DrawObjects[i].ObjType == "CIRCLE")
                {
                    cLine objl = (cLine)DrawObjects[i].Obj;
                    Brush br = new SolidBrush(objl.DrawColor);
                    cLine mobj = objl.MObj;

                    rect = new Rectangle();
                    rect.X = Math.Min(objl.X1, objl.X2);
                    rect.Y = Math.Min(objl.Y1, objl.Y2);
                    rect.Width = Math.Abs(objl.X1 - objl.X2);
                    rect.Height = Math.Abs(objl.Y1 - objl.Y2);

                    string Distance = "";

                    

                    if (mobj != null)
                    {
                        mobj.Text = mobj.Text;

                        Brush br1 = new SolidBrush(mobj.DrawColor);
                        if (MySettings.isResult == 1)
                            g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, new Point(mobj.X2, mobj.Y2));
                    }


                    
                    int l = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y1);
                    //string Distance = "Dia:" + l.ToString() + com.PubScaleShort;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    //e.Graphics.DrawString(Distance, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

               
                    //g.DrawRectangle(new Pen(Brushes.Transparent,2), rect);
                    Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);
                    com.DrawCircle(g, DrawPen, objl.X1, objl.Y1, l);

                    int radius = GetDistance(objl.X1, objl.Y1, objl.X2, objl.Y1);
                    gp.AddEllipse(objl.X1 - radius, objl.Y1 - radius, radius + radius, radius + radius);
                    PointF[] pf = gp.PathData.Points;
                    objl.PointFtoPointList(pf, gp, DrawPen);

                    //g.DrawEllipse(DrawPen, rect);
                    Point mp = new Point(objl.X2 - 30, objl.Y2 );
                    g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, mp);
                    g.Flush();
                    g.Save();

                }
               
                else if (DrawObjects[i].ObjType == "POINT")
                {

                

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    int radius = 3;
                    SolidBrush br1 = new SolidBrush(objl.DrawColor);

                    g.FillEllipse(br1,objl.X2  - radius, objl.Y2 - radius, radius + radius, radius + radius);
                    Point mp = new Point(objl.X2 + 5, objl.Y2 + 5);
                    g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);

                    g.Flush();
                    g.Save();

                    cLine mobj = objl.MObj;

                }

                else if (DrawObjects[i].ObjType == "CURVE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length > 1)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawCurve(DrawPen, points);
                        cLine mobj = objl.MObj;
                        gp.AddCurve(points);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf,gp, DrawPen);

                    
                        Point mp = new Point(points[points.Length-1].X + 5, points[points.Length - 1].Y + 5);
                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);


                        if (mobj != null)
                        {
                            mobj.Text = (objl.CurLength(xValue, yValue) ).ToString();
                            Brush br2 = new SolidBrush(mobj.DrawColor);
                            if(MySettings.isResult==1)
                            g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br2, new Point(mobj.X2, mobj.Y2));
                        }

                        g.Flush();
                        g.Save();
                    }
                }

                else if (DrawObjects[i].ObjType == "CCURVE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];

                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length > 1)
                    {
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawCurve(DrawPen, points);
                        cLine mobj = objl.MObj;
                        gp.AddCurve(points);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);

                        Point mp = new Point(points[points.Length - 1].X + 5, points[points.Length - 1].Y + 5);
                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);



                        if (mobj != null)
                        {
                            mobj.Text = (objl.CurLength(xValue, yValue)).ToString();
                            Brush br2 = new SolidBrush(mobj.DrawColor);
                            if (MySettings.isResult == 1)
                                g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br2, new Point(mobj.X2, mobj.Y2));
                        }


                        g.Flush();
                        g.Save();
                    }
                }

                else if (DrawObjects[i].ObjType == "ANGLE")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    Point[] points = new Point[objl.PointList.Count];
                    cLine mobj = objl.MObj;
                    SolidBrush br1 = new SolidBrush(objl.DrawColor);


                    int j = 0;
                    for (j = 0; j <= objl.PointList.Count - 1; j++)
                    {
                        points[j] = objl.PointList[j];
                    }

                    if (points.Length == 3)
                    {
                        Point P1 = objl.PointList[0];
                        Point P2 = objl.PointList[1];
                        Point P3 = objl.PointList[2];
                        Pen DrawPen = new Pen(objl.DrawColor, objl.DrawWidth);

                        g.DrawLine(DrawPen, P1, P2);
                        g.DrawLine(DrawPen, P2, P3);

                        gp.AddLine(P1, P2);
                        gp.AddLine(P2, P3);
                        PointF[] pf = gp.PathData.Points;
                        objl.PointFtoPointList(pf, gp, DrawPen);
                        Point mp = new Point(P3.X + 5, P3.Y + 5);
                        g.DrawString(objl.LineName, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br1, mp);

                        g.Flush();
                        g.Save();



                        double theta1 = Math.Atan2(P1.Y - P2.Y, P1.X - P2.X);
                        double theta2 = Math.Atan2(P2.Y - P3.Y, P2.X - P3.X);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);

                        Brush br = new SolidBrush(objl.DrawColor);
                        string Angle = "";

                        
                        if (mobj != null)
                        {
                            mobj.Text = objl.GetAngle().ToString() + "°";
                            Brush br2 = new SolidBrush(mobj.DrawColor);
                            g.DrawString(mobj.Text, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br2, new Point(mobj.X2, mobj.Y2));
                        }
                    }
                }

                else if (DrawObjects[i].ObjType == "TEXT")
                {

                    cLine objl = (cLine)DrawObjects[i].Obj;

                    string str = objl.Text;
                    Point TPoint = new Point(objl.X2, objl.Y2);
                    Brush br = new SolidBrush(objl.DrawColor);
                    g.DrawString(str, new Font("Verdana", MySettings.FontSize, FontStyle.Bold), br, TPoint);

                    g.Flush();
                    g.Save();

                }
            }



            if (isDateFound() == false)
            {

                cLine obj = new cLine();

                obj.X1 = 0;
                obj.Y1 = 0;

                obj.X2 = 10;
                obj.Y2 = img1.Height - 30;


                obj.LineName = "DT" + new Random().Next(100000, 999999).ToString();
                //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
                obj.DrawWidth = com.pwidth;
                obj.DrawColor = com.pcolor;
                obj.Text = PubDate.ToString("dd-MM-yyyy hh:mm tt");

                DrawObject dobj = new DrawObject();
                dobj.ObjType = "TEXT";
                obj.ObjType = dobj.ObjType;
                dobj.Obj = obj;
                DrawObjects.Add(dobj);
            }



            LoadObjCombo();
        }

        private bool isSelectionClear()
        {
          
            if ((IsMouseDown == false) && (isDrawLine == false) && (isDrawArrowLine == false) && (isDrawRect == false) && (isCircle == false)
                && (isCurve == false) && (isCCurve == false) && (isCorp == false) && (isZoom == false) && (WriteText == "")
                && (isAngle == false) && (isMarkLine == false) && (isMarkArc == false)
                && (isPPLine == false) && (isDrawPoint == false)
                 )
            {
                return true;
            }
            else
                return false;


        }

        private bool isOnEndOfLine(Point p, Point P2)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            if (((p.X <= P2.X + 5) && (p.X >= P2.X - 5)) && ((p.Y <= P2.Y + 5) && (p.Y >= P2.Y - 5)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool isOnStartOfLine(Point p, Point P2)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }

            if (((p.X <= P2.X + 5) && (p.X >= P2.X - 5)) && ((p.Y <= P2.Y + 5) && (p.Y >= P2.Y - 5)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        bool IsOnLine(Point p1, Point p2, Point p,Pen pen,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }

          
            using (var path = new GraphicsPath())
            {
                if (com.isCalibrationOpen == true)
                    p2 = new Point(p2.X, p1.Y);

                path.AddLine(p1, p2);


                int xx = ((p1.X + p2.X) / 2) + 10;
                int yy = ((p1.Y + p2.Y) / 2) - 20;


                Point mp = new Point(xx+ 5, yy+ 5);
                Font f= new Font("Verdana", 10, FontStyle.Bold);
                path.AddString(LineName,f.FontFamily,(int)f.Style,f.Size, mp,StringFormat.GenericDefault);


              



                return path.IsOutlineVisible(p, pen);
            }
        }

        bool IsOnRect(Point p1, Point p2, Point p, Pen pen,int w,int h,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                rect = new Rectangle();
                rect.X = Math.Min(p1.X, p2.X);
                rect.Y = Math.Min(p1.Y, p2.Y);
                rect.Width = Math.Abs(p1.X - p2.X);
                rect.Height = Math.Abs(p1.Y - p2.Y);
                path.AddRectangle(rect);
                Point mp = new Point(p2.X - 30, p2.Y - 20);
                Font f = new Font("Verdana", 10, FontStyle.Bold);
                path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);

                return path.IsOutlineVisible(p, pen);

            }
        }

        bool IsOnCircle(Point p1, Point p2, Point p, Pen pen, int w, int h,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }

            using (var path = new GraphicsPath())
            {
                rect = new Rectangle();
                rect.X = Math.Min(p1.X, p2.X);
                rect.Y = Math.Min(p1.Y, p2.Y);
                rect.Width = Math.Abs(p1.X - p2.X);
                rect.Height = Math.Abs(p1.Y - p2.Y);

                int radius = GetDistance(p1.X,p1.Y, p2.X, p1.Y);
                Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                
                path.AddEllipse(p1.X - radius, p1.Y - radius, radius + radius, radius + radius);

                //path.AddEllipse(rect);
                //path.AddRectangle(rect);
                Point mp = new Point(p2.X -30, p2.Y );
                Font f = new Font("Verdana", 10, FontStyle.Bold);
                path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);

                return path.IsOutlineVisible(p, pen);
            }
        }
        bool isOnPoint( Point p2, Point p, Pen pen,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                int radius = 3;
             
                path.AddEllipse( p2.X - radius, p2.Y - radius, radius + radius, radius + radius);
                path.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
                Point mp = new Point(p2.X + 5, p2.Y + 5);
                Font f = new Font("Verdana", 10, FontStyle.Bold);
                path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);

                return path.IsOutlineVisible(p, pen);
            }
        }




        bool isOnArc(List<Point> pointList, Point p, Pen pen, string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {




                Point p1, p2, p3;

                try
                {
                    p1 = pointList[0];
                }
                catch
                {
                    p1 = new Point(0, 0);
                }

                try
                {
                    p2 = pointList[1];
                }
                catch
                {
                    p2 = new Point(0, 0);
                }

                try
                {
                    p3 = pointList[2];
                }
                catch
                {
                    p3 = new Point(0, 0);
                }


                Font f = new Font("Verdana", 10, FontStyle.Bold);

                if ((p1.X > 0) && (p2.X > 0) && (p3.X == 0))
                {

                    path.AddLine(p1, p2);
                    Point mp = new Point(p2.X + 5, p2.Y + 5);
                    path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);

                    return path.IsOutlineVisible(p, pen);
                }
                else if ((p1.X > 0) && (p2.X > 0) && (p3.X > 0))
                {


                    Point obj_Center;
                    Rectangle arc_Rect;
                    float start_Angle;
                    float sweep_Angle;


                    arc_Start_Pt = p1;
                    arc_Mid_Pt = p2;
                    arc_End_Pt = p3;



                    obj_Center = getArcCenter(arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                    arc_Rect = get_ArcRectangle(obj_Center, obj_Radius);
                    start_Angle = get_Start_Angle(obj_Center, arc_Start_Pt);
                    sweep_Angle = get_Sweep_Angle(obj_Center, arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
                    path.AddArc(arc_Rect, start_Angle, sweep_Angle);

                    Point mp = new Point(p2.X + 5, p2.Y + 5);
                    path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);
                    return path.IsOutlineVisible(p, pen);

                }
                else
                {
                    return false;
                }



               

            }
        }

        bool isOnCurve(List<Point> pointList, Point p, Pen pen,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                Point[] points = new Point[pointList.Count];
                int i = 0;
                for (i = 0; i <= pointList.Count - 1; i++)
                {
                    points[i] = pointList[i];
                }
                if (points.Length > 1)
                {
                    path.AddCurve(points);
                    Point mp = new Point(points[points.Length-1].X + 5, points[points.Length - 1].Y + 5);
                    Font f = new Font("Verdana", 10, FontStyle.Bold);
                    path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);
                    
;                    return path.IsOutlineVisible(p, pen);
                }
                else
                {
                    return false;
                }
               
            }
        }

        bool isOnCCurve(List<Point> pointList, Point p, Pen pen,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                Point[] points = new Point[pointList.Count];
                int i = 0;
                for (i = 0; i <= pointList.Count - 1; i++)
                {
                    points[i] = pointList[i];
                }
                if (points.Length > 2)
                {
                    path.AddClosedCurve(points);
                    Point mp = new Point(points[points.Length - 1].X + 5, points[points.Length - 1].Y + 5);
                    Font f = new Font("Verdana", 10, FontStyle.Bold);
                    path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);

                    return path.IsOutlineVisible(p, pen);
                }
                else
                {
                    return false;
                }

              
            }
        }

        bool isOnText(string Text,Point p1, Point p, Pen pen)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                Font f = new Font("Verdana", 8, FontStyle.Bold);

                path.AddString(Text, f.FontFamily, (int)f.Style, f.Size, p1,StringFormat.GenericDefault);

                    return path.IsOutlineVisible(p, pen);
                


            }
        }


        bool isOnAngle(List<Point> pointList, Point p, Pen pen,string LineName)
        {
            if (isSelectionClear() == false)
            {
                return false;
            }


            using (var path = new GraphicsPath())
            {
                Point[] points = new Point[pointList.Count];

                int i = 0;
                for (i = 0; i <= pointList.Count - 1; i++)
                {
                    points[i] = pointList[i];
                }

                if (points.Length == 3)
                {
                    Point P1 = points[0];
                    Point P2 = points[1];
                    Point P3 = points[2];

                    path.AddLine(P1, P2);
                    path.AddLine(P2, P3);
                    Point mp = new Point(P3.X + 5, P3.Y + 5);
                    Font f = new Font("Verdana", 10, FontStyle.Bold);
                    path.AddString(LineName, f.FontFamily, (int)f.Style, f.Size, mp, StringFormat.GenericDefault);


                    return path.IsOutlineVisible(p, pen);
                }
                else
                {
                    return false;
                }


            }
        }
        public bool isReDrawCancel = false;
        private void img1_Paint(object sender, PaintEventArgs e)
        {
          

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }
            
            Pen np = new Pen(com.pcolor, com.pwidth);

            Pen tpen = new Pen(Brushes.Black, 2);
            tpen.DashStyle = DashStyle.Dot;
          
            Brush br = new SolidBrush(com.pcolor);
            if (IsMouseDown == true)
            {
                if ((isDrawLine == true) || (isMarkLine == true))
                {
                    if (com.isCalibrationOpen == false)
                    {
                        Point EndPoint;
                        string Distance;
                        EndPoint = EndLcation;
                        Distance = (Math.Round(GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndPoint.X * xValue, EndPoint.Y * yValue) / 1000.0, MySettings.DPoint)).ToString();
                        if (isMarkLine == true)
                            np.DashStyle = DashStyle.Dot;

                        e.Graphics.DrawLine(tpen, StartLocation, EndLcation);


                        if (isDrawLine == true)
                        {
                            Point TPoint = new Point(EndPoint.X + 5, EndPoint.Y + 5);
                            //e.Graphics.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        Point EndPoint;
                        string Distance;
                        EndPoint = new Point(EndLcation.X, StartLocation.Y);
                        Distance = GetDistance(StartLocation.X, StartLocation.Y, EndPoint.X, EndPoint.Y).ToString();

                        Pen np1 = new Pen(np.Brush, np.Width);

                        e.Graphics.DrawLine(np1, StartLocation, EndPoint);
                        Point TPoint = new Point(EndPoint.X + 5, EndPoint.Y + 5);
                        e.Graphics.DrawString(Distance.ToString() + " px", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    }



                }
                else if ((isDrawArrowLine == true) || (isMarkArc == true))
                {
                    Point EndPoint;
                    string Distance;
                    EndPoint = EndLcation;
                    Distance = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndPoint.X * xValue, EndPoint.Y * yValue).ToString();
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                    np.CustomEndCap = bigArrow;
                    e.Graphics.DrawLine(tpen, StartLocation, EndLcation);

                    if (isDrawArrowLine == true)
                    {
                        Point TPoint = new Point(EndPoint.X + 5, EndPoint.Y + 5);
                        //e.Graphics.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    }



                }

                else if (isDrawRect == true)
                {

                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                    rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                    rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);



                    int l = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, EndLcation.X * xValue, StartLocation.Y * yValue);
                    int b = GetDistance(StartLocation.X * xValue, StartLocation.Y * yValue, StartLocation.X * xValue, EndLcation.Y * yValue);


                    string Distance = "L: " + l.ToString() + " " + com.PubScaleShort + " B: " + b.ToString() + " " + com.PubScaleShort;

                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    // e.Graphics.DrawString("" + Distance.ToString(), new Font("Verdana", 8, FontStyle.Bold), br, TPoint);




                    e.Graphics.DrawRectangle(tpen, rect);
                    e.Graphics.Flush();
                    e.Graphics.Save();
                }

                else if  (isCorp == true)
                {
                    rect = new Rectangle();
                    rect.X = Math.Min(StartLocation.X, EndLcation.X);
                    rect.Y = Math.Min(StartLocation.Y, EndLcation.Y);
                    rect.Width = Math.Abs(StartLocation.X - EndLcation.X);
                    rect.Height = Math.Abs(StartLocation.Y - EndLcation.Y);

                    SolidBrush sb = new SolidBrush(Color.Black);
                    Pen pp = new Pen(sb,3);
                    pp.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawRectangle(pp, rect);
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



                    int l = GetDistance(StartLocation.X, StartLocation.Y, EndLcation.X, StartLocation.Y);
                    string Distance = "Dia:" + l.ToString() + com.PubScaleShort;
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    //e.Graphics.DrawString(Distance, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);
                    e.Graphics.DrawLine(new Pen(Color.Black), new Point(StartLocation.X, StartLocation.Y), new Point(EndLcation.X, StartLocation.Y));
                    com.DrawCircle(e.Graphics, tpen, StartLocation.X, StartLocation.Y, l);
                    //e.Graphics.DrawEllipse(np, rect);
                    e.Graphics.Flush();
                    e.Graphics.Save();
                }
                else if (isDrawPoint == true)
                {
                    Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);
                    int radius = 3;
                    SolidBrush br1 = new SolidBrush(np.Color);

                    e.Graphics.FillEllipse(br1, EndLcation.X - radius, EndLcation.Y - radius, radius + radius, radius + radius);
                    e.Graphics.Flush();
                    e.Graphics.Save();

                }
                else if (isCurve == true)
                {
                    Point[] points = new Point[PointList.Count];


                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length > 1)
                    {

                        e.Graphics.DrawCurve(np, points);
                        e.Graphics.Flush();
                        e.Graphics.Save();
                    }
                }

              
                else if (isCCurve == true)
                {
                    Point[] points = new Point[PointList.Count];


                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length > 2)
                    {

                        e.Graphics.DrawCurve(np, points);
                        e.Graphics.Flush();
                        e.Graphics.Save();
                    }
                }

                else if (isPPLine == true)
                {
                    Point[] points = new Point[PointList.Count];


                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }

                    if (points.Length == 2)
                    {
                        Point P1 = points[0];
                        Point P2 = points[1];

                        string Distance;
                        Distance = GetDistance(P1.X * xValue, P1.Y * yValue, P2.X * xValue, P2.Y * yValue).ToString();


                        e.Graphics.DrawLine(np, P1, P2);

                        Point TPoint = new Point(P2.X + 5, P2.Y + 5);
                        //e.Graphics.DrawString(Distance.ToString() + " " + com.PubScaleShort, new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    }

                 
                   
                }

                else if (isAngle == true)
                {
                    Point[] points = new Point[PointList.Count];


                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }
                    if (points.Length == 2)
                    {
                        e.Graphics.DrawLine(np, points[0], points[1]);
                    }
                    if (points.Length == 3)
                    {
                        Point P1 = points[0];
                        Point P2 = points[1];

                        e.Graphics.DrawLine(np, points[0], points[1]);
                        e.Graphics.DrawLine(np, points[1], points[2]);
                        e.Graphics.Flush();
                        e.Graphics.Save();



                        double theta1 = Math.Atan2(points[0].Y - points[1].Y, points[0].X - points[1].X);
                        double theta2 = Math.Atan2(points[1].Y - points[2].Y, points[1].X - points[2].X);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);


                        Point TPoint = new Point(points[1].X + 5, points[1].Y + 5);
                        //e.Graphics.DrawString(angle.ToString() + "° ", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    }
                }
            }

            else
            {

              
                if (isPPLine == true)
                {
                    if(PointList.Count>0)
                    e.Graphics.DrawLine(tpen, PointList[0], EndLcation);

                }

                if (isAngle == true)
                {
                    Point[] points = new Point[PointList.Count];


                    int i = 0;
                    for (i = 0; i <= PointList.Count - 1; i++)
                    {
                        points[i] = PointList[i];
                    }
                    if (points.Length == 1)
                    {
                        e.Graphics.DrawLine(tpen, points[0], EndLcation);
                       
                    }
                    else if (points.Length == 2)
                    {
                        e.Graphics.DrawLine(np, points[0], points[1]);
                        e.Graphics.DrawLine(tpen, points[1], EndLcation);

                    }
                    if (points.Length == 3)
                    {
                        Point P1 = points[0];
                        Point P2 = points[1];

                        e.Graphics.DrawLine(np, points[0], points[1]);
                        e.Graphics.DrawLine(np, points[1], points[2]);
                        e.Graphics.Flush();
                        e.Graphics.Save();



                        double theta1 = Math.Atan2(points[0].Y - points[1].Y, points[0].X - points[1].X);
                        double theta2 = Math.Atan2(points[1].Y - points[2].Y, points[1].X - points[2].X);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);


                        Point TPoint = new Point(points[1].X + 5, points[1].Y + 5);
                        //e.Graphics.DrawString(angle.ToString() + "° ", new Font("Verdana", 8, FontStyle.Bold), br, TPoint);

                    }
                }

                else  if (isMarkArc == true)
                {


                    Point p1, p2, p3;

                    try
                    {
                        p1 = PointList[0];
                    }
                    catch
                    {
                        p1 = new Point(0, 0);
                    }

                    try
                    {
                        p2 = PointList[1];
                    }
                    catch
                    {
                        p2 = new Point(0, 0);
                    }

                    try
                    {
                        p3 = PointList[2];
                    }
                    catch
                    {
                        p3 = new Point(0, 0);
                    }




                    if ((p1.X > 0) && (p2.X == 0) && (p3.X == 0))
                    {

                        e.Graphics.DrawLine(tpen, p1, EndLcation);
                        e.Graphics.Flush();
                        e.Graphics.Save();
                    }
                    else if ((p1.X > 0) && (p2.X > 0) && (p3.X == 0))
                    {

                        e.Graphics.DrawLine(tpen, p2, EndLcation);
                        e.Graphics.Flush();
                        e.Graphics.Save();
                    }
                }

                else if (isCurve == true)
                {
                    Point p1;

                    if(PointList.Count>0)
                    { 
                    p1 = PointList[PointList.Count - 1];

                    e.Graphics.DrawLine(tpen, p1, EndLcation);
                    e.Graphics.Flush();
                    e.Graphics.Save();
                    }
                }
                else if (isCCurve == true)
                {
                    Point p1;

                    if (PointList.Count > 0)
                    {
                        p1 = PointList[PointList.Count - 1];

                        e.Graphics.DrawLine(tpen, p1, EndLcation);
                        e.Graphics.Flush();
                        e.Graphics.Save();
                    }
                }
            }





            if (com.CurrentWork == "WM")
                ReDraw(isReDrawCancel, e.Graphics);
            else if (com.CurrentWork == "IM")
                ReDrawIM(isReDrawCancel, e.Graphics);




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
            //if (img1.FunctionalMode == Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom)
            //    img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            //else
            //    img1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void kryptonContextMenu1_Closing(object sender, CancelEventArgs e)
        {
            
        }

        private void FrmImage1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           
        }

        private void kryptonContextMenu1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
        }

        private void kryptonContextMenu1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void kryptonCommand1_Execute(object sender, EventArgs e)
        {
           

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine obj = (cLine)(cLine)DrawObjects[i].Obj;
                if (obj.LineName == SelectedLine.LineName)
                {
                    DrawObjects.RemoveAt(i);
                    img1.Cursor = Cursors.Default;
                    img1.Invalidate();
                }
            }
            FindIntersect();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = cd.Color;
            }
        }


        public void DrawIntersect(Graphics g, Pen pen,
                                float centerX, float centerY, float radius)
        {
            Brush brush = new SolidBrush(pen.Color);
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }


        private List<Point> LineAndAngle_Intersect(Point l1, Point l2, Point AL11,Point AL12,Point AL21,Point AL22, Pen lpen, Pen rpen)
        {


            List<Point> lp = new List<Point>();
            lp.Add(LineAndLine_Intersect(l1, l2, AL11, AL12, lpen, rpen));
            lp.Add(LineAndLine_Intersect(l1, l2, AL21, AL22, lpen, rpen));
            
            return lp;
        }


        private List<Point> LineAndRect_Intersect(Point l1, Point l2, Rectangle r,Pen lpen, Pen rpen)
        {


            Point r1 = new Point(r.X, r.Y);
            Point r2 = new Point(r.X + r.Width, r.Y);
            Point r3 = new Point(r.X + r.Width, r.Y + r.Height);
            Point r4 = new Point(r.X, r.Y + r.Height);

            List<Point> lp = new List<Point>();
            lp.Add(LineAndLine_Intersect(l1, l2, r1, r2, lpen, rpen));
            lp.Add(LineAndLine_Intersect(l1, l2, r2, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(l1, l2, r4, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(l1, l2, r1, r4, lpen, rpen));

            return lp;
        }

        private List<Point> RectAndRect_Intersect(Rectangle rect1, Rectangle rect2, Pen lpen, Pen rpen)
        {


            Point r11 = new Point(rect1.X, rect1.Y);
            Point r12 = new Point(rect1.X + rect1.Width, rect1.Y);
            Point r13 = new Point(rect1.X + rect1.Width, rect1.Y + rect1.Height);
            Point r14 = new Point(rect1.X, rect1.Y + rect1.Height);


            Point r21 = new Point(rect2.X, rect2.Y);
            Point r22 = new Point(rect2.X + rect2.Width, rect2.Y);
            Point r23 = new Point(rect2.X + rect2.Width, rect2.Y + rect2.Height);
            Point r24 = new Point(rect2.X, rect2.Y + rect2.Height);


            List<Point> lp = new List<Point>();
            lp.Add(LineAndLine_Intersect(r11, r12, r21, r22, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r12, r22, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r12, r24, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r12, r21, r24, lpen, rpen));

            lp.Add(LineAndLine_Intersect(r12, r13, r21, r22, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r13, r13, r22, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r13, r13, r24, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r13, r13, r21, r24, lpen, rpen));

            lp.Add(LineAndLine_Intersect(r14, r13, r21, r22, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r14, r13, r22, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r14, r13, r24, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r14, r13, r21, r24, lpen, rpen));

            lp.Add(LineAndLine_Intersect(r11, r14, r21, r22, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r14, r22, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r14, r24, r23, lpen, rpen));
            lp.Add(LineAndLine_Intersect(r11, r14, r21, r24, lpen, rpen));


            return lp;
        }

        private List<Point> RectAndCircle_Intersect(Rectangle rect1, float cx, float cy, float radius, Pen lpen)
        {


            Point r11 = new Point(rect1.X, rect1.Y);
            Point r12 = new Point(rect1.X + rect1.Width, rect1.Y);
            Point r13 = new Point(rect1.X + rect1.Width, rect1.Y + rect1.Height);
            Point r14 = new Point(rect1.X, rect1.Y + rect1.Height);

           
            List<Point> lp = new List<Point>();

            lp.AddRange(LineAndCircle_Intersect(cx, cy, radius, r11, r12, lpen));
            lp.AddRange(LineAndCircle_Intersect(cx,cy, radius, r12, r13, lpen));
            lp.AddRange(LineAndCircle_Intersect(cx,cy, radius, r14, r13, lpen));
            lp.AddRange(LineAndCircle_Intersect(cx,cy, radius, r11, r14, lpen));

            return lp;
        }



        private List<Point> RectAndArc_Intersect(Rectangle rect1, float cx, float cy, float radius, Pen lpen,Rectangle rect,float sAngle,float swAngle)
        {


            Point r11 = new Point(rect1.X, rect1.Y);
            Point r12 = new Point(rect1.X + rect1.Width, rect1.Y);
            Point r13 = new Point(rect1.X + rect1.Width, rect1.Y + rect1.Height);
            Point r14 = new Point(rect1.X, rect1.Y + rect1.Height);


            List<Point> lp = new List<Point>();

            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, r11, r12, lpen,rect,sAngle,swAngle));
            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, r12, r13, lpen, rect, sAngle, swAngle));
            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, r14, r13, lpen, rect, sAngle, swAngle));
            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, r11, r14, lpen, rect, sAngle, swAngle));

            return lp;
        }

        private List<Point> LineAndArc_Intersect(float cx, float cy, float radius, Point point1, Point point2, Pen linepen,Rectangle rect,float sangle,float swangle)
        {
            float dx, dy, A, B, C, det, t;

            List<Point> lp = new List<Point>();

            PointF intersection1;
            PointF intersection2;

            dx = point2.X - point1.X;
            dy = point2.Y - point1.Y;

            A = dx * dx + dy * dy;
            B = 2 * (dx * (point1.X - cx) + dy * (point1.Y - cy));
            C = (point1.X - cx) * (point1.X - cx) + (point1.Y - cy) * (point1.Y - cy) - radius * radius;

            det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return lp;
            }
            else if (det == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                intersection2 = new PointF(float.NaN, float.NaN);

                GraphicsPath g = new GraphicsPath();
                g.AddArc(rect, sangle, swangle);
                if (g.IsOutlineVisible(intersection1, linepen) == true)
                {
                    g = new GraphicsPath();
                    g.AddLine(point1, point1);
                    if (g.IsOutlineVisible(intersection1, linepen) == true)
                    {
                        lp.Add(new Point((int)intersection1.X, (int)intersection1.Y));
                    }
                }
                return lp;
            }
            else
            {
                // Two solutions.
                t = (float)((-B + Math.Sqrt(det)) / (2 * A));
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                t = (float)((-B - Math.Sqrt(det)) / (2 * A));
                intersection2 = new PointF(point1.X + t * dx, point1.Y + t * dy);

                Point i1 = new Point((int)intersection1.X, (int)intersection1.Y);
                Point i2 = new Point((int)intersection2.X, (int)intersection2.Y);
                
                GraphicsPath g = new GraphicsPath();
                g.AddArc(rect, sangle, swangle);
                if(g.IsOutlineVisible(i1,linepen)==true)
                {
                    g = new GraphicsPath();
                g.AddLine(point1, point2);
                if (g.IsOutlineVisible(i1, linepen) == true)
                {
                    lp.Add(new Point((int)i1.X, (int)intersection1.Y));
                }
                }
                g = new GraphicsPath();
                g.AddArc(rect, sangle, swangle);
                if (g.IsOutlineVisible(i2, linepen) == true)
                {

                    g = new GraphicsPath();
                    g.AddLine(point1, point2);
                    if (g.IsOutlineVisible(i2, linepen) == true)
                    {
                        lp.Add(new Point((int)i2.X, (int)intersection2.Y));
                    }
                }
                return lp;
            }
        }

       

        private List<Point> AngleAndCircle_Intersect(float cx, float cy, float radius, Point point1, Point point2,Point point3, Pen linepen)
        {
            List<Point> lp = new List<Point>();

            lp.AddRange(LineAndCircle_Intersect(cx, cy, radius, point1, point2, linepen));
            lp.AddRange(LineAndCircle_Intersect(cx, cy, radius, point2, point3, linepen));

            return lp;

        }
        private List<Point> AngleAndArc_Intersect(float cx, float cy, float radius, Point point1, Point point2, Point point3, Pen linepen,Rectangle r,float sAngle,float swAngle)
        {
            List<Point> lp = new List<Point>();

            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, point1, point2, linepen,r,sAngle,swAngle));
            lp.AddRange(LineAndArc_Intersect(cx, cy, radius, point2, point3, linepen, r, sAngle, swAngle));

            return lp;

        }

        private List<Point> AngleAndRect_Intersect(Rectangle r, Point point1, Point point2, Point point3, Pen lpen,Pen rpen)
        {
            List<Point> lp = new List<Point>();


            Point r1 = new Point(r.X, r.Y);
            Point r2 = new Point(r.X + r.Width, r.Y);
            Point r3 = new Point(r.X + r.Width, r.Y + r.Height);
            Point r4 = new Point(r.X, r.Y + r.Height);

            lp.Add(LineAndLine_Intersect(point1, point2, r1, r2, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point1, point2, r2, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point1, point2, r4, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point1, point2, r1, r4, lpen, rpen));

            lp.Add(LineAndLine_Intersect(point2, point3, r1, r2, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point2, point3, r2, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point2, point3, r4, r3, lpen, rpen));
            lp.Add(LineAndLine_Intersect(point2, point3, r1, r4, lpen, rpen));



            return lp;

        }


        private List<Point> LineAndCircle_Intersect(float cx, float cy, float radius, Point point1, Point point2,Pen linepen)
        {
            float dx, dy, A, B, C, det, t;

            List<Point> lp = new List<Point>();

            PointF intersection1;
            PointF intersection2;

            dx = point2.X - point1.X;
            dy = point2.Y - point1.Y;

            A = dx * dx + dy * dy;
            B = 2 * (dx * (point1.X - cx) + dy * (point1.Y - cy));
            C = (point1.X - cx) * (point1.X - cx) + (point1.Y - cy) * (point1.Y - cy) - radius * radius;

            det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = new PointF(float.NaN, float.NaN);
                intersection2 = new PointF(float.NaN, float.NaN);
                return lp;
            }
            else if (det == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                intersection2 = new PointF(float.NaN, float.NaN);

                GraphicsPath g = new GraphicsPath();
                g.AddLine(point1, point1);
                if(g.IsOutlineVisible(intersection1,linepen)==true)
                { 
                lp.Add(new Point((int)intersection1.X, (int)intersection1.Y));
                }
                return lp;
            }
            else
            {
                // Two solutions.
                t = (float)((-B + Math.Sqrt(det)) / (2 * A));
                intersection1 = new PointF(point1.X + t * dx, point1.Y + t * dy);
                t = (float)((-B - Math.Sqrt(det)) / (2 * A));
                intersection2 = new PointF(point1.X + t * dx, point1.Y + t * dy);

                Point i1 = new Point((int)intersection1.X, (int)intersection1.Y);
                Point i2 = new Point((int)intersection2.X, (int)intersection2.Y);

                GraphicsPath g = new GraphicsPath();
                g.AddLine(point1, point2);
                if (g.IsOutlineVisible(i1, linepen) == true)
                {
                    lp.Add(new Point((int)i1.X, (int)intersection1.Y));
                }

                g = new GraphicsPath();
                g.AddLine(point1, point2);
                if (g.IsOutlineVisible(i2, linepen) == true)
                {
                    lp.Add(new Point((int)i2.X, (int)intersection2.Y));
                }
               
                return lp;
            }
        }

        public Point LineAndLine_Intersect(Point line1V1, Point line1V2, Point line2V1, Point line2V2, Pen l1p, Pen l2p)
        {
            //Line1
            float A1 = line1V2.Y - line1V1.Y;
            float B1 = line1V1.X - line1V2.X;
            float C1 = A1 * line1V1.X + B1 * line1V1.Y;

            //Line2
            float A2 = line2V2.Y - line2V1.Y;
            float B2 = line2V1.X - line2V2.X;
            float C2 = A2 * line2V1.X + B2 * line2V1.Y;

            float det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                return new Point(0, 0);
            }
            else
            {
                int x = Convert.ToInt32((B2 * C1 - B1 * C2) / det);
                int y = Convert.ToInt32((A1 * C2 - A2 * C1) / det);
                Point output = new Point(x, y);
                GraphicsPath g = new GraphicsPath();
                g.AddLine(line1V1, line1V2);
                bool b1 = g.IsOutlineVisible(output, l1p);
                g = new GraphicsPath();
                g.AddLine(line2V1, line2V2);
                bool b2 = g.IsOutlineVisible(output, l2p);
                if ((b1 == true) && (b2 == true))
                    return output;
                else
                    return new Point(0, 0);
            }
        }


        public static List<Point> CircleAndCircle_Intersect(PointF center1, PointF center2, double radius1, double? radius2 = null)
        {
            List<Point> t = new List<Point>();
            var (r1, r2) = (radius1, radius2 ?? radius1);
            (double x1, double y1, double x2, double y2) = (center1.X, center1.Y, center2.X, center2.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2)) { return t; }

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            var fx = (x1 + x2) / 2 + a * (x2 - x1);
            var gx = c * (y2 - y1) / 2;

            var fy = (y1 + y2) / 2 + a * (y2 - y1);
            var gy = c * (x1 - x2) / 2;

            var i1 = new Point((int)(fx + gx), (int)(fy + gy));
            var i2 = new Point((int)(fx - gx), (int)(fy - gy));

          
            t.Add(i1);
            t.Add(i2);


            return t;
        }

        public static List<Point> CircleAndArc_Intersect(PointF center1, PointF center2, double radius1, Pen linepen, Rectangle rect, float sangle, float swangle, double? radius2 = null)
        {
            List<Point> t = new List<Point>();
            var (r1, r2) = (radius1, radius2 ?? radius1);
            (double x1, double y1, double x2, double y2) = (center1.X, center1.Y, center2.X, center2.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2)) { return t; }

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            var fx = (x1 + x2) / 2 + a * (x2 - x1);
            var gx = c * (y2 - y1) / 2;

            var fy = (y1 + y2) / 2 + a * (y2 - y1);
            var gy = c * (x1 - x2) / 2;

            var i1 = new Point((int)(fx + gx), (int)(fy + gy));
            var i2 = new Point((int)(fx - gx), (int)(fy - gy));

            GraphicsPath g = new GraphicsPath();

            g.AddArc(rect, sangle, swangle);

            if(g.IsOutlineVisible(i1, linepen) ==true)
            { 
            t.Add(i1);
            }

            g = new GraphicsPath();
            g.AddArc(rect, sangle, swangle);
            if (g.IsOutlineVisible(i2, linepen) == true)
            {
                t.Add(i2);
            }
           
            return t;
        }



        public static List<Point> ArcAndArc_Intersect(PointF center1, PointF center2, double radius1, Pen linepen, Rectangle rect, float sangle, float swangle, Rectangle rect2, float sangle2, float swangle2, double? radius2 = null)
        {
            List<Point> t = new List<Point>();
            var (r1, r2) = (radius1, radius2 ?? radius1);
            (double x1, double y1, double x2, double y2) = (center1.X, center1.Y, center2.X, center2.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2)) { return t; }

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            var fx = (x1 + x2) / 2 + a * (x2 - x1);
            var gx = c * (y2 - y1) / 2;

            var fy = (y1 + y2) / 2 + a * (y2 - y1);
            var gy = c * (x1 - x2) / 2;

            var i1 = new Point((int)(fx + gx), (int)(fy + gy));
            var i2 = new Point((int)(fx - gx), (int)(fy - gy));

            GraphicsPath g = new GraphicsPath();
            g.AddArc(rect2, sangle2, swangle2);
            if(g.IsOutlineVisible(i1,linepen))
            {
                g = new GraphicsPath();
                g.AddArc(rect, sangle, swangle);

            if (g.IsOutlineVisible(i1, linepen) == true)
            {
                t.Add(i1);
            }
            }

            g = new GraphicsPath();
            g.AddArc(rect2, sangle2, swangle2);
            if (g.IsOutlineVisible(i2, linepen))
            {
                g = new GraphicsPath();
                g.AddArc(rect, sangle, swangle);
                if (g.IsOutlineVisible(i2, linepen) == true)
                {
                    t.Add(i2);
                }
            }

            return t;
        }


        private void DeleteIntersect()
        {
            int i;
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine obj=(cLine)DrawObjects[i].Obj;
                if (obj.LineName.Substring(0, 1) == "I")
                {
                    DrawObjects.RemoveAt(i);
                    i = i - 1;
                }
            }

        }

        private void FindIntersect()
        {

            cLine DrawObj;

            DeleteIntersect();
            MaxIPointNumber = 0;

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }


            List<Point> lp = new List<Point>();
            int i = 0;
            int cnt = DrawObjects.Count;
            int k = 0;

            for(k=0;k<=cnt-1;k++)
            {
               DrawObj = (cLine)DrawObjects[k].Obj;
                if (DrawObj.ObjType == "POINT")
                    goto l2;

                for (i = 0; i <= cnt - 1; i++)
                {
                cLine obj = (cLine)DrawObjects[i].Obj;
                if (obj.ObjType == "POINT")
                   goto l1;

               

                if (obj.LineName != DrawObj.LineName)
                {
                        List<Point> t = new List<Point>();

                        if ((obj.ObjType == "LINE") && (DrawObj.ObjType == "LINE"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor), new Pen(DrawObj.DrawColor)));
                        }
                        else if ((obj.ObjType == "PPLINE") && (DrawObj.ObjType == "PPLINE"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor), new Pen(DrawObj.DrawColor)));
                        }
                        else if ((obj.ObjType == "ARROW") && (DrawObj.ObjType == "ARROW"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor), new Pen(DrawObj.DrawColor)));
                        }
                        else if ((obj.ObjType == "LINE") && (DrawObj.ObjType == "PPLINE"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor), new Pen(DrawObj.DrawColor)));

                        }
                        else if ((obj.ObjType == "LINE") && (DrawObj.ObjType == "ARROW"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor), new Pen(DrawObj.DrawColor)));

                        }
                        else if ((obj.ObjType == "PPLINE") && (DrawObj.ObjType == "ARROW"))
                        {
                            t.Add(LineAndLine_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Point(DrawObj.X1, DrawObj.Y1), new Point(DrawObj.X2, DrawObj.Y2), new Pen(obj.DrawColor,obj.DrawWidth), new Pen(DrawObj.DrawColor,DrawObj.DrawWidth)));

                        }
                        else if (((obj.ObjType == "LINE") || (obj.ObjType == "PPLINE") || (obj.ObjType == "ARROW")) && (DrawObj.ObjType == "ANGLE"))
                        {
                            Rectangle r = new Rectangle(DrawObj.X1, DrawObj.Y1, (int)DrawObj.GetWidth(xValue, yValue), (int)DrawObj.GetHeight(xValue, yValue));
                            t = LineAndAngle_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2),DrawObj.PointList[0], DrawObj.PointList[1], DrawObj.PointList[1], DrawObj.PointList[2] ,new Pen(obj.DrawColor, obj.DrawWidth), new Pen(DrawObj.DrawColor, DrawObj.DrawWidth));
                        }

                        else if (((obj.ObjType == "LINE") || (obj.ObjType == "PPLINE") || (obj.ObjType == "ARROW")) && (DrawObj.ObjType == "RECT"))
                        {
                            Rectangle r = new Rectangle(DrawObj.X1, DrawObj.Y1, (int)DrawObj.GetWidth(xValue,yValue), (int)DrawObj.GetHeight(xValue,yValue));
                            t = LineAndRect_Intersect(new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), r, new Pen(obj.DrawColor,obj.DrawWidth), new Pen(DrawObj.DrawColor, DrawObj.DrawWidth));
                        }
                        else if (((obj.ObjType == "LINE") || (obj.ObjType == "PPLINE") || (obj.ObjType == "ARROW")) && (DrawObj.ObjType == "ARC"))
                        {
                            t = LineAndArc_Intersect(DrawObj.X1, DrawObj.Y1, DrawObj.Arc_Radius, new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2),new Pen(obj.DrawColor,obj.DrawWidth),DrawObj.Arc_Rectangle,DrawObj.Arc_StartAngle,DrawObj.Arc_SweepAngle);
                        }
                        else if (((obj.ObjType == "LINE") || (obj.ObjType == "PPLINE") || (obj.ObjType == "ARROW")) && (DrawObj.ObjType == "CIRCLE"))
                        {
                            t = LineAndCircle_Intersect(DrawObj.X1, DrawObj.Y1, (float)DrawObj.GetRadius(), new Point(obj.X1, obj.Y1), new Point(obj.X2, obj.Y2), new Pen(obj.DrawColor, obj.DrawWidth));
                        }

                        else if ((obj.ObjType == "ANGLE") && (DrawObj.ObjType == "CIRCLE"))
                        {
                            t = AngleAndCircle_Intersect(DrawObj.X1, DrawObj.Y1, (float)DrawObj.GetRadius(), new Point(obj.PointList[0].X, obj.PointList[0].Y), new Point(obj.PointList[1].X, obj.PointList[1].Y), new Point(obj.PointList[2].X, obj.PointList[2].Y), new Pen(obj.DrawColor, obj.DrawWidth));
                        }

                        else if ((obj.ObjType == "ANGLE") && (DrawObj.ObjType == "ARC"))
                        {
                            t = AngleAndArc_Intersect(DrawObj.X1, DrawObj.Y1, (float)DrawObj.Arc_Radius, new Point(obj.PointList[0].X, obj.PointList[0].Y), new Point(obj.PointList[1].X, obj.PointList[1].Y), new Point(obj.PointList[2].X, obj.PointList[2].Y), new Pen(obj.DrawColor, obj.DrawWidth),DrawObj.Arc_Rectangle,DrawObj.Arc_StartAngle,DrawObj.Arc_SweepAngle);
                        }

                        else if ((obj.ObjType == "ANGLE") && (DrawObj.ObjType == "RECT"))
                        {
                            Rectangle r = new Rectangle(DrawObj.X1, DrawObj.Y1, (int)DrawObj.GetWidth(xValue, yValue), (int)DrawObj.GetHeight(xValue, yValue));
                            t = AngleAndRect_Intersect(r, new Point(obj.PointList[0].X, obj.PointList[0].Y), new Point(obj.PointList[1].X, obj.PointList[1].Y), new Point(obj.PointList[2].X, obj.PointList[2].Y), new Pen(obj.DrawColor, obj.DrawWidth), new Pen(DrawObj.DrawColor, DrawObj.DrawWidth));
                        }

                        else if ((obj.ObjType == "RECT") && (DrawObj.ObjType == "RECT"))
                        {
                            Rectangle r1 = new Rectangle(DrawObj.X1, DrawObj.Y1, (int)DrawObj.GetWidth(xValue, yValue), (int)DrawObj.GetHeight(xValue, yValue));
                            Rectangle r2 = new Rectangle(obj.X1, obj.Y1, (int)obj.GetWidth(xValue, yValue), (int)obj.GetHeight(xValue, yValue));
                            t = RectAndRect_Intersect(r1, r2, new Pen(DrawObj.DrawColor, DrawObj.DrawWidth), new Pen(obj.DrawColor, obj.DrawWidth));
                        }
                        else if ((obj.ObjType == "RECT") && (DrawObj.ObjType == "CIRCLE"))
                        {
                            Rectangle r2 = new Rectangle(obj.X1, obj.Y1, (int)obj.GetWidth(xValue, yValue), (int)obj.GetHeight(xValue, yValue));
                            t = RectAndCircle_Intersect(r2,DrawObj.X1,DrawObj.Y1,(float)DrawObj.GetRadius(), new Pen(obj.DrawColor, obj.DrawWidth));
                        }
                        else if ((obj.ObjType == "CIRCLE") && (DrawObj.ObjType == "CIRCLE"))
                        {
                            t = CircleAndCircle_Intersect(new PointF(obj.X1,obj.Y1), new PointF(DrawObj.X1, DrawObj.Y1),obj.GetRadius(), DrawObj.GetRadius());
                        }
                        else if ((obj.ObjType == "CIRCLE") && (DrawObj.ObjType == "ARC"))
                        {
                            t = CircleAndArc_Intersect(new PointF(obj.X1, obj.Y1), new PointF(DrawObj.X1, DrawObj.Y1), (float)obj.GetRadius(), new Pen(obj.DrawColor, obj.DrawWidth), DrawObj.Arc_Rectangle, DrawObj.Arc_StartAngle, DrawObj.Arc_SweepAngle, DrawObj.Arc_Radius);
                        }
                        else if ((obj.ObjType == "ARC") && (DrawObj.ObjType == "ARC"))
                        {
                            t = ArcAndArc_Intersect(new PointF(obj.X1, obj.Y1), new PointF(DrawObj.X1, DrawObj.Y1), (float)obj.Arc_Radius, new Pen(obj.DrawColor, obj.DrawWidth), DrawObj.Arc_Rectangle, DrawObj.Arc_StartAngle, DrawObj.Arc_SweepAngle,obj.Arc_Rectangle,obj.Arc_StartAngle,obj.Arc_SweepAngle, DrawObj.Arc_Radius);
                        }
                        else if ((obj.ObjType == "RECT") && (DrawObj.ObjType == "ARC"))
                        {
                            Rectangle r2 = new Rectangle(obj.X1, obj.Y1, (int)obj.GetWidth(xValue, yValue), (int)obj.GetHeight(xValue, yValue));
                            t = RectAndArc_Intersect(r2, DrawObj.X1, DrawObj.Y1, (float)DrawObj.Arc_Radius, new Pen(obj.DrawColor, obj.DrawWidth),DrawObj.Arc_Rectangle,DrawObj.Arc_StartAngle,DrawObj.Arc_SweepAngle);
                        }
                        ///obj 2 type
                        ///







                        int j = 0;
                        for (j = 0; j <= t.Count - 1; j++)
                        {
                            lp.Add(t[j]);
                        }

                     
                    

            
                }

            l1:;
            }
            l2:;
            }


            List<Point> noDupes = lp.Distinct().ToList();

            int i1;
            for (i1 = 0; i1 <= noDupes.Count - 1; i1++)
            {

                if ((noDupes[i1].X == 0) && (noDupes[i1].Y == 0))
                { }
                else
                { 
                cLine obj1 = new cLine();

                obj1.X2 = noDupes[i1].X;
                obj1.Y2 = noDupes[i1].Y;
                MaxIPointNumber = MaxIPointNumber + 1;
                obj1.LineName = "I" + MaxIPointNumber.ToString();
                obj1.DrawWidth = com.pwidth;
                obj1.DrawColor = Color.Yellow;

                DrawObject dobj = new DrawObject();
                dobj.ObjType = "POINT";
                obj1.ObjType = dobj.ObjType;
                dobj.Obj = obj1;
                DrawObjects.Add(dobj);
                }
            }



        }
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chkLength.Enabled = false;
            chkArea.Enabled = false;
            chkCirum.Enabled = false;
            chkDia.Enabled = false;
            chkRad.Enabled = false;
            chkAngle.Enabled = false;

            chkLength.Checked = false;
            chkArea.Checked = false;
            chkCirum.Checked = false;
            chkDia.Checked = false;
            chkRad.Checked = false;
            chkAngle.Checked = false;

            txtAngle.Enabled = false;
            txtLength.Enabled = false;

            txtAngle.Text = "1";
            txtLength.Text = "0";
            txtMove.Text = "1";
            txtSize.Text = "1";

            this.cmdVLine.Enabled = false;
            this.cmdHLine.Enabled = false;
            cmdPLine.Enabled = false;
            cmdPlineL.Enabled = false;
            cmdMPoint.Enabled = false;

            pan2.Enabled = false;

            if ((SelectedLine.ObjType == "LINE")||(SelectedLine.ObjType == "PPLINE") || (SelectedLine.ObjType == "ARROW"))
            {
                chkLength.Enabled = true;
                chkLength.Checked = true;
                txtAngle.Enabled = true;
                txtLength.Enabled = true;
                pan2.Enabled = true;
                cmdPLine.Enabled = true;
                cmdPlineL.Enabled = true;
                cmdMPoint.Enabled = true;

                float angle = (float)SelectedLine.GetLineAngle();
                lineangle = angle;

            }
            else if (SelectedLine.ObjType == "CURVE")
            {
                chkLength.Enabled = true;
                chkLength.Checked = true;
            }

            else if (SelectedLine.ObjType == "POINT")
            {
                this.cmdVLine.Enabled = true;
                this.cmdHLine.Enabled = true;
            }
            else if (SelectedLine.ObjType == "RECT")
            {
                chkArea.Enabled = true;
                chkCirum.Enabled = true;
                chkArea.Checked = true;
                chkCirum.Checked = true;
            }
            else if (SelectedLine.ObjType == "ANGLE")
            {
                chkAngle.Enabled = true;
                chkAngle.Checked = true;
            }
            else if (SelectedLine.ObjType == "CIRCLE")
            {
                chkArea.Enabled = true;
                chkCirum.Enabled = true;
                chkDia.Enabled = true;
                chkRad.Enabled = true;

                chkArea.Checked = true;
                chkCirum.Checked = true;
                chkDia.Checked = true;
                chkRad.Checked = true;
            }


            chkLength.Checked = SelectedLine.Showlength;
            chkArea.Checked = SelectedLine.Showarea;
            chkCirum.Checked =SelectedLine.Showcircum;
            chkDia.Checked = SelectedLine.Showdia;
            chkRad.Checked = SelectedLine.Showrad;
            chkAngle.Checked = SelectedLine.Showarea;


            groupBox1.Location = MouseStart;
            groupBox1.Visible = true;

            button1.BackColor = SelectedLine.DrawColor;
            cmbw1.Text = SelectedLine.DrawWidth.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            groupBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox1.Visible = false;
                Brush br = new SolidBrush(button1.BackColor);
                Pen p1 = new Pen(br, (float)Convert.ToDouble(cmbw1.Text));
                SelectedLine.DrawColor = p1.Color;
                SelectedLine.DrawWidth = p1.Width;

                SelectedLine.Showlength = chkLength.Checked;
                SelectedLine.Showarea = chkArea.Checked;
                SelectedLine.Showcircum = chkCirum.Checked;
                SelectedLine.Showdia = chkDia.Checked;
                SelectedLine.Showrad = chkRad.Checked;
                SelectedLine.Showangle = chkAngle.Checked;

               
               
                if (com.Val(txtLength.Text) != 0)
                {

                    
                   
                    double angle = 0;

                    float cosAngle = (float)Math.Cos(angle);
                    float sinAngle = (float)Math.Sin(angle);
                    float length = (float)com.Val(txtLength.Text);

                         
                    PointF point2 = new PointF((SelectedLine.X1 + (cosAngle * length)), (SelectedLine.Y1 + (sinAngle * length)));

                    SelectedLine.X2 = (int)point2.X;
                    SelectedLine.Y2 = (int)point2.Y;

                    FindIntersect();

                }
                img1.Invalidate();
            }
            catch
            { 
            }
           
        }

        private void frmImage1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MDIParent1 mp = (MDIParent1)this.MdiParent;
            //              mp.EDControls(false);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine obj = (cLine)(cLine)DrawObjects[i].Obj;
                if (obj.LineName == SelectedLine.LineName)
                {
                    cLine newobj = new cLine();
                    DrawObject newdobj = new DrawObject();

                    newobj.DrawColor = SelectedLine.DrawColor;
                    newobj.DrawWidth = SelectedLine.DrawWidth;
                    newobj.EndCap = SelectedLine.EndCap;
                    newobj.Height = SelectedLine.Height;
                    newobj.LineStyle = SelectedLine.LineStyle;
                    newobj.Width = SelectedLine.Width;
                    newobj.Showlength = SelectedLine.Showlength;
                    newobj.Showangle = SelectedLine.Showangle;
                    newobj.Showarea = SelectedLine.Showarea;
                    newobj.Showcircum = SelectedLine.Showcircum;
                    newobj.Showdia = SelectedLine.Showdia;
                    newobj.Showrad = SelectedLine.Showrad;
                    newobj.Text = SelectedLine.Text;
                    newobj.ObjType = SelectedLine.ObjType;

                    cLine newmobj = new cLine();

                    if(SelectedLine.MObj!=null)
                    { 
                    newmobj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    newmobj.X2 = SelectedLine.MObj.X2 + 10;
                    newmobj.Y2 = SelectedLine.MObj.Y2;
                    newmobj.Text = "";
                    newmobj.DrawWidth = SelectedLine.MObj.DrawWidth;
                    newmobj.DrawColor = SelectedLine.MObj.DrawColor;

                    newobj.MObj = newmobj;
                    }

                    if (SelectedLine.ObjType=="LINE")
                    {

                        MaxLineNumber = MaxLineNumber + 1;
                        newobj.LineName = "L" + MaxLineNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "PPLINE")
                    {

                        MaxLineNumber = MaxLineNumber + 1;
                        newobj.LineName = "PP" + MaxLineNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "MLINE")
                    {

                        MaxLineNumber = MaxLineNumber + 1;
                        newobj.LineName = "M" + MaxLineNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "ARROW")
                    {

                        MaxArrowNumber = MaxArrowNumber + 1;
                        newobj.LineName = "A" + MaxArrowNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }

                    else if (SelectedLine.ObjType == "POINT")
                    {

                        MaxPointNumber = MaxPointNumber + 1;
                        newobj.LineName = "P" + MaxPointNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "RECT")
                    {

                        MaxRectNumber = MaxRectNumber + 1;
                        newobj.LineName = "R" + MaxRectNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }

                    else if (SelectedLine.ObjType == "CIRCLE")
                    {

                        MaxCircleNumber = MaxCircleNumber + 1;
                        newobj.LineName = "C" + MaxCircleNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "TEXT")
                    {

                      
                        newobj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newdobj.Obj = newobj;
                        newdobj.ObjType = newobj.ObjType;

                    }

                    else if (SelectedLine.ObjType == "ANGLE")
                    {
                        //goto l;

                        MaxAngleNumber = MaxAngleNumber + 1;
                        newobj.LineName = "AN" + MaxAngleNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newobj.PointList = new List<Point>();
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = SelectedLine.PointList[i];
                            Point pp1 = new Point(pp.X + 10, pp.Y + 10);
                            newobj.PointList.Add(pp1);

                        }
                        newdobj.Obj = newobj;

                        newdobj.ObjType = newobj.ObjType;

                    }

                    else if (SelectedLine.ObjType == "CURVE")
                    {

                        //goto l;

                        MaxCurveNumber = MaxCurveNumber + 1;
                        newobj.LineName = "CR" + MaxCurveNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;

                        newobj.PointList = new List<Point>();
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = SelectedLine.PointList[i];
                            Point pp1 = new Point(pp.X + 10, pp.Y + 10);
                            newobj.PointList.Add(pp1);

                        }
                        newdobj.Obj = newobj;

                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "ARC")
                    {

                       // goto l;

                        MaxArcNumber = MaxArcNumber + 1;
                        newobj.LineName = "AR" + MaxArcNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newobj.PointList = new List<Point>();
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = SelectedLine.PointList[i];
                            Point pp1 = new Point(pp.X + 10, pp.Y + 10);
                            newobj.PointList.Add(pp1);

                        }
                        newdobj.Obj = newobj;

                        newdobj.ObjType = newobj.ObjType;

                    }
                    else if (SelectedLine.ObjType == "CCURVE")
                    {

                     //   goto l;

                        MaxCurveNumber = MaxCurveNumber + 1;
                        newobj.LineName = "CCR" + MaxCurveNumber.ToString();
                        newobj.X1 = SelectedLine.X1 + 10;
                        newobj.Y1 = SelectedLine.Y1;
                        newobj.X2 = SelectedLine.X2 + 10;
                        newobj.Y2 = SelectedLine.Y2;
                        newobj.PointList = new List<Point>();
                        for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                        {
                            Point pp = SelectedLine.PointList[i];
                            Point pp1 = new Point(pp.X + 10, pp.Y + 10);
                            newobj.PointList.Add(pp1);

                        }
                        newdobj.Obj = newobj;

                        newdobj.ObjType = newobj.ObjType;

                    }

                    DrawObjects.Add(newdobj);

                    img1.Cursor = Cursors.Default;
                    img1.Invalidate();

                    goto l;
                }
            }
        l:
            int a;

           // FindIntersect();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.SizeNWSE;


        }

        private void imageWM_MouseMove(object sender, MouseEventArgs e)
        {
           

        }

        private void button4_Click(object sender, EventArgs e)
        {

            double angle = 0;

            if (com.Val(txtAngle.Text) == 0)
                angle = 0;
            else
                angle = com.Val(txtAngle.Text);

            angle = (angle * -1);


            Point P1 = com.RotatePoint(new Point(SelectedLine.X1, SelectedLine.Y1), new PointF(SelectedLine.X1, SelectedLine.Y1), angle);
            Point P2 = com.RotatePoint(new Point(SelectedLine.X2, SelectedLine.Y2), new PointF(SelectedLine.X1, SelectedLine.Y1), angle);

            SelectedLine.X1 = P1.X;
            SelectedLine.Y1 = P1.Y;

            SelectedLine.X2 = P2.X;
            SelectedLine.Y2 = P2.Y;

            if (SelectedLine.MObj != null)
            {
                Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                SelectedLine.MObj.X2 = TPoint.X;
                SelectedLine.MObj.Y2 = TPoint.Y;
            }
            
            img1.Invalidate();

            FindIntersect();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            double angle = 0;

            if (com.Val(txtAngle.Text) == 0)
                angle = 0;
            else
                angle = com.Val(txtAngle.Text);

           
            Point P1 = com.RotatePoint(new Point(SelectedLine.X1, SelectedLine.Y1), new PointF(SelectedLine.X1, SelectedLine.Y1), angle);
            Point P2 = com.RotatePoint(new Point(SelectedLine.X2, SelectedLine.Y2), new PointF(SelectedLine.X1, SelectedLine.Y1), angle);
            SelectedLine.X1 = P1.X;
            SelectedLine.Y1 = P1.Y;

            SelectedLine.X2 = P2.X;
            SelectedLine.Y2 = P2.Y;

            if (SelectedLine.MObj != null)
            {
                Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                SelectedLine.MObj.X2 = TPoint.X;
                SelectedLine.MObj.Y2 = TPoint.Y;
            }



            img1.Invalidate();

            FindIntersect();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int Move = 0;

            System.Data.DataTable DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + cmbunit1.Text + "'");


            double xValue = 1;
            double yValue = 1;
            System.Data.DataRow[] dr = DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }

          

            if (com.Val(txtMove.Text) == 0)
                Move = 0;
            else
                Move = (int)com.Val(txtMove.Text);

            if(cmbunit1.Text!="px")
            Move = Move * (int)xValue/1000;
       
           

            if (SelectedLine.ObjType == "ANGLE")
            {
                Point p1;
                Point p2;
                Point p3;

                p1 = SelectedLine.PointList[0];
                p2 = SelectedLine.PointList[1];
                p3 = SelectedLine.PointList[2];

                p1.X = p1.X;
                p1.Y = p1.Y - Move;

                p2.X = p2.X;
                p2.Y = p2.Y - Move;

                p3.X = p3.X;
                p3.Y = p3.Y - Move;

                SelectedLine.PointList[0] = p1;
                SelectedLine.PointList[1] = p2;
                SelectedLine.PointList[2] = p3;


            }
            else if ((SelectedLine.ObjType == "CURVE")|| (SelectedLine.ObjType == "CCURVE")|| (SelectedLine.ObjType == "ARC"))
            {
                Point p1;
            
                int i;
                for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                {
                    p1 = SelectedLine.PointList[i];
                    p1.X = p1.X;
                    p1.Y = p1.Y - Move;

                    SelectedLine.PointList[i] = p1;
                }

        

            }
            else
            { 
          
            SelectedLine.X1 = SelectedLine.X1;
            SelectedLine.Y1 = SelectedLine.Y1 - Move;

            SelectedLine.X2 = SelectedLine.X2;
            SelectedLine.Y2 = SelectedLine.Y2 - Move;

            if (SelectedLine.MObj != null)
            {
                Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                SelectedLine.MObj.X2 = TPoint.X;
                SelectedLine.MObj.Y2 = TPoint.Y;
            }

            }

            img1.Invalidate();

            FindIntersect();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int Move = 0;

            System.Data.DataTable DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + cmbunit1.Text + "'");


            double xValue = 1;
            double yValue = 1;
            System.Data.DataRow[] dr = DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }



            if (com.Val(txtMove.Text) == 0)
                Move = 0;
            else
                Move = (int)com.Val(txtMove.Text);

            if (cmbunit1.Text != "px")
                Move = Move * (int)xValue / 1000;




            if (SelectedLine.ObjType == "ANGLE")
            {
                Point p1;
                Point p2;
                Point p3;

                p1 = SelectedLine.PointList[0];
                p2 = SelectedLine.PointList[1];
                p3 = SelectedLine.PointList[2];

                p1.X = p1.X;
                p1.Y = p1.Y + Move;

                p2.X = p2.X;
                p2.Y = p2.Y + Move;

                p3.X = p3.X;
                p3.Y = p3.Y + Move;

                SelectedLine.PointList[0] = p1;
                SelectedLine.PointList[1] = p2;
                SelectedLine.PointList[2] = p3;


            }
            else if ((SelectedLine.ObjType == "CURVE") || (SelectedLine.ObjType == "CCURVE") || (SelectedLine.ObjType == "ARC"))
            {
                Point p1;

                int i;
                for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                {
                    p1 = SelectedLine.PointList[i];
                    p1.X = p1.X;
                    p1.Y = p1.Y + Move;

                    SelectedLine.PointList[i] = p1;
                }



            }
            else
            {
                SelectedLine.X1 = SelectedLine.X1;
                SelectedLine.Y1 = SelectedLine.Y1 + Move;

                SelectedLine.X2 = SelectedLine.X2;
                SelectedLine.Y2 = SelectedLine.Y2 + Move;

                if (SelectedLine.MObj != null)
                {
                    Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                    SelectedLine.MObj.X2 = TPoint.X;
                    SelectedLine.MObj.Y2 = TPoint.Y;
                }

            }

            img1.Invalidate();

            FindIntersect();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int Move = 0;

            System.Data.DataTable DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + cmbunit1.Text + "'");


            double xValue = 1;
            double yValue = 1;
            System.Data.DataRow[] dr = DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }



            if (com.Val(txtMove.Text) == 0)
                Move = 0;
            else
                Move = (int)com.Val(txtMove.Text);

            if (cmbunit1.Text != "px")
                Move = Move * (int)xValue / 1000;




            if (SelectedLine.ObjType == "ANGLE")
            {
                Point p1;
                Point p2;
                Point p3;

                p1 = SelectedLine.PointList[0];
                p2 = SelectedLine.PointList[1];
                p3 = SelectedLine.PointList[2];

                p1.X = p1.X - Move;
                p1.Y = p1.Y;

                p2.X = p2.X-Move;
                p2.Y = p2.Y ;

                p3.X = p3.X-Move;
                p3.Y = p3.Y;

                SelectedLine.PointList[0] = p1;
                SelectedLine.PointList[1] = p2;
                SelectedLine.PointList[2] = p3;


            }
            else if ((SelectedLine.ObjType == "CURVE") || (SelectedLine.ObjType == "CCURVE") || (SelectedLine.ObjType == "ARC"))
            {
                Point p1;

                int i;
                for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                {
                    p1 = SelectedLine.PointList[i];
                    p1.X = p1.X-Move;
                    p1.Y = p1.Y ;

                    SelectedLine.PointList[i] = p1;
                }



            }
            else
            {
                SelectedLine.X1 = SelectedLine.X1 - Move;
                SelectedLine.Y1 = SelectedLine.Y1;

                SelectedLine.X2 = SelectedLine.X2 - Move;
                SelectedLine.Y2 = SelectedLine.Y2;

                if (SelectedLine.MObj != null)
                {
                    Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                    SelectedLine.MObj.X2 = TPoint.X;
                    SelectedLine.MObj.Y2 = TPoint.Y;
                }


            }
            img1.Invalidate();

            FindIntersect();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int Move = 0;

            System.Data.DataTable DtXY = com.sqlcn.Gettable("select magnification mag,xValue,yValue from calibration where unit='" + cmbunit1.Text + "'");


            double xValue = 1;
            double yValue = 1;
            System.Data.DataRow[] dr = DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }



            if (com.Val(txtMove.Text) == 0)
                Move = 0;
            else
                Move = (int)com.Val(txtMove.Text);
            if (cmbunit1.Text != "px")
                Move = Move * (int)xValue / 1000;



            if (SelectedLine.ObjType == "ANGLE")
            {
                Point p1;
                Point p2;
                Point p3;

                p1 = SelectedLine.PointList[0];
                p2 = SelectedLine.PointList[1];
                p3 = SelectedLine.PointList[2];

                p1.X = p1.X + Move;
                p1.Y = p1.Y;

                p2.X = p2.X + Move;
                p2.Y = p2.Y;

                p3.X = p3.X + Move;
                p3.Y = p3.Y;

                SelectedLine.PointList[0] = p1;
                SelectedLine.PointList[1] = p2;
                SelectedLine.PointList[2] = p3;


            }
            else if ((SelectedLine.ObjType == "CURVE") || (SelectedLine.ObjType == "CCURVE") || (SelectedLine.ObjType == "ARC"))
            {
                Point p1;

                int i;
                for (i = 0; i <= SelectedLine.PointList.Count - 1; i++)
                {
                    p1 = SelectedLine.PointList[i];
                    p1.X = p1.X+Move;
                    p1.Y = p1.Y ;

                    SelectedLine.PointList[i] = p1;
                }



            }
            else
            {

                SelectedLine.X1 = SelectedLine.X1 + Move;
                SelectedLine.Y1 = SelectedLine.Y1;

                SelectedLine.X2 = SelectedLine.X2 + Move;
                SelectedLine.Y2 = SelectedLine.Y2;

                if (SelectedLine.MObj != null)
                {
                    Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);

                    SelectedLine.MObj.X2 = TPoint.X;
                    SelectedLine.MObj.Y2 = TPoint.Y;
                }

            }

            img1.Invalidate();

            FindIntersect();
        }

        float lineangle = 0;
        private void button11_Click(object sender, EventArgs e)
        {
            int Size = 0;

            if (com.Val(txtSize.Text) == 0)
                Size = 1;
            else
                Size = (int)com.Val(txtSize.Text);


            if (SelectedLine.ObjType == "ANGLE")
            {
              


            }
            else
            {


                float angle = lineangle;


                //angle=SelectedLine.GetLineAngle();

                float cosAngle = (float)Math.Cos(angle);
                float sinAngle = (float)Math.Sin(angle);
                float length = (float)com.Val(Size);

                cosAngle = cosAngle * length;
                sinAngle = sinAngle * length;


                PointF point2 = new PointF((int)cosAngle + SelectedLine.X2, (int)sinAngle + SelectedLine.Y2);

                SelectedLine.X2 = (int)Math.Floor(point2.X);
                SelectedLine.Y2 = (int)Math.Floor(point2.Y);

                if (SelectedLine.MObj != null)
                {
                    Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);
                    SelectedLine.MObj.X2 = TPoint.X;
                    SelectedLine.MObj.Y2 = TPoint.Y;
                }

            }

            img1.Invalidate();

            FindIntersect();
        }

        private void button12_Click(object sender, EventArgs e)
        {
          
        }

        private void cmdVLine_Click(object sender, EventArgs e)
        {

            Point p1 = new Point(SelectedLine.X1, SelectedLine.Y1 - 50);
            Point p2 = new Point(SelectedLine.X1, SelectedLine.Y1 + 50);



            cLine obj = new cLine();
            obj.X1 = p1.X;
            obj.Y1 = p1.Y;
            obj.X2 = p2.X;
            obj.Y2 = p2.Y;
            MaxLineNumber = MaxLineNumber + 1;
            obj.LineName = "L" + MaxLineNumber.ToString();
            //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
            obj.DrawWidth = com.pwidth;
            obj.DrawColor = com.pcolor;

            obj.Showlength = false;
            obj.Showcircum = false;
            obj.Showarea = false;
            obj.Showdia = false;
            obj.Showrad = false;
            obj.Showangle = false;


        
            Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

            cLine objt = new cLine();
            objt.X2 = TPoint.X;
            objt.Y2 = TPoint.Y;

            objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
            //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
            objt.DrawWidth = com.pwidth;
            objt.DrawColor = com.pcolor;
            objt.Text = "";
            obj.MObj = objt;
            DrawObject dobj = new DrawObject();
            dobj.ObjType = "LINE";
            obj.ObjType = dobj.ObjType;
            dobj.Obj = obj;
            DrawObjects.Add(dobj);

            img1.Invalidate();

        }





        private void cmdHLine_Click(object sender, EventArgs e)
        {

            Point p1 = new Point(SelectedLine.X1-50, SelectedLine.Y1 );
            Point p2 = new Point(SelectedLine.X1+50, SelectedLine.Y1);



            cLine obj = new cLine();
            obj.X1 = p1.X;
            obj.Y1 = p1.Y;
            obj.X2 = p2.X;
            obj.Y2 = p2.Y;
            MaxLineNumber = MaxLineNumber + 1;
            obj.LineName = "L" + MaxLineNumber.ToString();
            //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
            obj.DrawWidth = com.pwidth;
            obj.DrawColor = com.pcolor;

            obj.Showlength = false;
            obj.Showcircum = false;
            obj.Showarea = false;
            obj.Showdia = false;
            obj.Showrad = false;
            obj.Showangle = false;



            Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

            cLine objt = new cLine();
            objt.X2 = TPoint.X;
            objt.Y2 = TPoint.Y;

            objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
            //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
            objt.DrawWidth = com.pwidth;
            objt.DrawColor = com.pcolor;
            objt.Text = "";
            obj.MObj = objt;
            DrawObject dobj = new DrawObject();
            dobj.ObjType = "LINE";
            obj.ObjType = dobj.ObjType;
            dobj.Obj = obj;
            DrawObjects.Add(dobj);

            img1.Invalidate();
        }

        private void cmdPLine_Click(object sender, EventArgs e)
        {

            Point p1 = new Point(SelectedLine.X1, SelectedLine.Y1);
            Point p2 = new Point(SelectedLine.X2, SelectedLine.Y2);
            Point midpoint = new Point();
            Point p3 = new Point();
            Point p4 = new Point();

            double slope;

          
            // Calculate the slope of the original line. (y2 - y1) / (x2 - x1) = slope
            slope = ((double)(p2.Y - p1.Y) / (double)(p2.X - p1.X));

            // Perpendicular lines have a slope of (-1 / originalSlope) -- the negative reciprocal.
            slope = -1 / slope;

            // Formula to find the midpoint.
            midpoint.X = (p1.X + p2.X) / 2;
            midpoint.Y = (p1.Y + p2.Y) / 2;

            p3.X = midpoint.X;
            p3.Y = midpoint.Y;

            // Find the y-intercept of this equation. y=mx + b
            double b = -slope * midpoint.X + midpoint.Y;

            // Finally start calculating the final point.
            // Add the length of the line to X.
            p4.X = midpoint.X + 100;

            // Now plug our slope, intercept, and new X into y=mx + b
            p4.Y = (int)(slope * (midpoint.X + 100) + b);

            // Draw that line!


            cLine obj = new cLine();
            obj.X1 = p3.X;
            obj.Y1 = p3.Y;
            obj.X2 = p4.X;
            obj.Y2 = p4.Y;
            MaxLineNumber = MaxLineNumber + 1;
            obj.LineName = "L" + MaxLineNumber.ToString();
            //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
            obj.DrawWidth = com.pwidth;
            obj.DrawColor = com.pcolor;

            obj.Showlength = false;
            obj.Showcircum = false;
            obj.Showarea = false;
            obj.Showdia = false;
            obj.Showrad = false;
            obj.Showangle = false;



            Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

            cLine objt = new cLine();
            objt.X2 = TPoint.X;
            objt.Y2 = TPoint.Y;

            objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
            //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
            objt.DrawWidth = com.pwidth;
            objt.DrawColor = com.pcolor;
            objt.Text = "";
            obj.MObj = objt;
            DrawObject dobj = new DrawObject();
            dobj.ObjType = "LINE";
            obj.ObjType = dobj.ObjType;
            dobj.Obj = obj;
            DrawObjects.Add(dobj);

            img1.Invalidate();



        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Point p1 = new Point(SelectedLine.X1, SelectedLine.Y1);
            Point p2 = new Point(SelectedLine.X2, SelectedLine.Y2);
            Point midpoint = new Point();
            Point p3 = new Point();
            Point p4 = new Point();

            double slope;


            // Calculate the slope of the original line. (y2 - y1) / (x2 - x1) = slope
            slope = ((double)(p2.Y - p1.Y) / (double)(p2.X - p1.X));

            // Perpendicular lines have a slope of (-1 / originalSlope) -- the negative reciprocal.
            slope = -1 / slope;

            // Formula to find the midpoint.
            midpoint.X = (p1.X + p2.X) / 2;
            midpoint.Y = (p1.Y + p2.Y) / 2;

            p3.X = midpoint.X;
            p3.Y = midpoint.Y;

            // Find the y-intercept of this equation. y=mx + b
            double b = -slope * midpoint.X + midpoint.Y;

            // Finally start calculating the final point.
            // Add the length of the line to X.
            p4.X = midpoint.X - 100;

            // Now plug our slope, intercept, and new X into y=mx + b
            p4.Y = (int)(slope * (midpoint.X - 100) + b);

            // Draw that line!


            cLine obj = new cLine();
            obj.X1 = p3.X;
            obj.Y1 = p3.Y;
            obj.X2 = p4.X;
            obj.Y2 = p4.Y;
            MaxLineNumber = MaxLineNumber + 1;
            obj.LineName = "L" + MaxLineNumber.ToString();
            //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
            obj.DrawWidth = com.pwidth;
            obj.DrawColor = com.pcolor;

            obj.Showlength = false;
            obj.Showcircum = false;
            obj.Showarea = false;
            obj.Showdia = false;
            obj.Showrad = false;
            obj.Showangle = false;



            Point TPoint = new Point(EndLcation.X + 5, EndLcation.Y + 5);

            cLine objt = new cLine();
            objt.X2 = TPoint.X;
            objt.Y2 = TPoint.Y;

            objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
            //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
            objt.DrawWidth = com.pwidth;
            objt.DrawColor = com.pcolor;
            objt.Text = "";
            obj.MObj = objt;
            DrawObject dobj = new DrawObject();
            dobj.ObjType = "LINE";
            obj.ObjType = dobj.ObjType;
            dobj.Obj = obj;
            DrawObjects.Add(dobj);

            img1.Invalidate();

        }

        private void cmdMPoint_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(SelectedLine.X1, SelectedLine.Y1);
            Point p2 = new Point(SelectedLine.X2, SelectedLine.Y2);
            Point midpoint = new Point();
            Point p3 = new Point();
            Point p4 = new Point();

            double slope;


            // Calculate the slope of the original line. (y2 - y1) / (x2 - x1) = slope
            slope = ((double)(p2.Y - p1.Y) / (double)(p2.X - p1.X));

            // Perpendicular lines have a slope of (-1 / originalSlope) -- the negative reciprocal.
            slope = -1 / slope;

            // Formula to find the midpoint.
            midpoint.X = (p1.X + p2.X) / 2;
            midpoint.Y = (p1.Y + p2.Y) / 2;

            p3.X = midpoint.X;
            p3.Y = midpoint.Y;




            cLine obj = new cLine();
            obj.X1 = p3.X;
            obj.Y1 = p3.Y;
            obj.X2 = p3.X;
            obj.Y2 = p3.Y;
            MaxPointNumber = MaxPointNumber + 1;
            obj.LineName = "P" + MaxPointNumber.ToString();
            //obj.DrawPen = new Pen(com.pcolor, com.pwidth);
            obj.DrawWidth = com.pwidth;
            obj.DrawColor = Color.Yellow;

            DrawObject dobj = new DrawObject();
            dobj.ObjType = "POINT";
            obj.ObjType = dobj.ObjType;
            dobj.Obj = obj;
            DrawObjects.Add(dobj);


            img1.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int Size = 0;

            if (com.Val(txtSize.Text) == 0)
                Size = 1;
            else
                Size = (int)com.Val(txtSize.Text);


            if (SelectedLine.ObjType == "ANGLE")
            {



            }
            else
            {

                double angle = 0;

                //angle = SelectedLine.GetLineAngle();

                float cosAngle = (float)Math.Cos(angle);
                float sinAngle = (float)Math.Sin(angle);
                float length = (float)com.Val(Size*-1);

                PointF point2 = new PointF((SelectedLine.X2 + (cosAngle * length)), (SelectedLine.Y2 + (sinAngle * length)));

                SelectedLine.X2 = (int)point2.X;
                SelectedLine.Y2 = (int)point2.Y;

                if (SelectedLine.MObj != null)
                {
                    Point TPoint = new Point(SelectedLine.X2 + 5, SelectedLine.Y2 + 5);
                    SelectedLine.MObj.X2 = TPoint.X;
                    SelectedLine.MObj.Y2 = TPoint.Y;
                }

            }

            img1.Invalidate();

            FindIntersect();
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            
        }
    }







}
