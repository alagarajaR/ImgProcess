using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ImgProcess.Objects;
//Color Code : #345D9D
using System.Numerics;

namespace ImgProcess
{

    public static class com
    {
        public static string Remarks { get; set; }
        public static string Length { get; set; }

        public static long PWorkId { get; set; }

        public static string PubWorkName { get; set; }
        public static bool isCalibrationOpen { get; set; }
        public static mysqlclass sqlcn { get; set; }
        public static string MsgTitle { get; set; }
        public static string PubScaleMode { get;set; }
        public static string PubScaleShort { get; set; }

        public static int PubFontSize { get; set; }
       public static string CurrentWork { get; set; }
        public static bool WMLic { get; set; }
        public static bool IMLic { get; set; }

        public static string[] PubMType = new string[] {
            "LENGTH", "ANGLE", "DEPTH", "LEG", "THICKNESS", "MIN THICKNESS", "THROAT", "ROOT PENETRATION", "GAP", "UNDERCUT",
            "MELT THROUGH", "%PENETRATION", "AREA", "DIAMETER", "RADIUS", "CIRCUMFERENCE"
        };

        public static System.Data.DataTable DtXY { get;set;}

        public static System.Drawing.Color pcolor { get; set; }
        public static float pwidth { get; set; }


        public static System.Drawing.Color mcolor { get; set; }
        public static float mwidth { get; set; }

        public static Int32 PubCMag { get; set; }
        
        public static int KPNo { get; set; }

        public static int CPNo { get; set; }


        public static Krypton.Toolkit.PaletteMode kp { get; set; }
        public static ComponentFactory.Krypton.Toolkit.PaletteMode cp { get; set; }


        public static void DrawCircle(this Graphics g, Pen pen,
                                 float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static float Val(object input)
        {
            try
            {
                return (float)Convert.ToDouble(input);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static Point FindNearestPointOnLine(Point Start, Point End, Point Ptn)
        {
            //Get heading

            UnityEngine.Vector2 origin = new UnityEngine.Vector2(Start.X, Start.Y);
            UnityEngine.Vector2 end = new UnityEngine.Vector2(End.X, End.Y);
            UnityEngine.Vector2 point = new UnityEngine.Vector2(Ptn.X, Ptn.Y);



            UnityEngine.Vector2 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            UnityEngine.Vector2 lhs = point - origin;
            float dotP = UnityEngine.Vector2.Dot(lhs, heading);
            dotP = UnityEngine.Mathf.Clamp(dotP, 0f, magnitudeMax);

            UnityEngine.Vector2 v = origin + heading * dotP;
            
            return new Point((int)v.x,(int)v.y);
        }


        public static Point FindNearestPointOnRect(Rectangle r, Point Ptn)
        {
            //Get heading


            Point r1 = new Point(r.X, r.Y);
            Point r2 = new Point(r.X + r.Width, r.Y);
            Point r3 = new Point(r.X + r.Width, r.Y + r.Height);
            Point r4 = new Point(r.X, r.Y + r.Height);

            Point p1 = FindNearestPointOnLine(r1, r2, Ptn);

            Point p2 = FindNearestPointOnLine(r2, r3, Ptn);

            Point p3 = FindNearestPointOnLine(r4, r3, Ptn);

            Point p4 = FindNearestPointOnLine(r1, r4, Ptn);

            int a = GetDistance(Ptn.X, Ptn.Y, p1.X, p1.Y);
            int b = GetDistance(Ptn.X, Ptn.Y, p2.X, p2.Y);
            int c = GetDistance(Ptn.X, Ptn.Y, p3.X, p3.Y);
            int d = GetDistance(Ptn.X, Ptn.Y, p4.X, p4.Y);


            if ((a <= b) && (a <= c) && (a <=d))
            {
                return p1;
            }
            else if ((b <= a) && (b <= c) && (b <= d))
            {
                return p2;
            }
            else if ((c <= a) && (c <= b) && (c <= d))
            {
                return p3;
            }
            else if ((d <= a) && (d <=b) && (d <=c))
            {
                return p4;
            }


            return new Point(0,0);
        }
        private static void swap(int a, int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        private static cLine GetObj(string ObjName, List<DrawObject> DrawObjects)
        {
            cLine o;
            int i;

            ObjName = ObjName.Replace("-START", "");
            ObjName = ObjName.Replace("-END", "");

            ObjName = ObjName.Replace("-CENTER", "");

            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                o = (cLine)DrawObjects[i].Obj;
                if (o.LineName == ObjName)
                    return o;
            }
            return null;
        }

        private enum mpgcol
        {
            MeasureName, Value, Obj1, Obj2, MeasureType, EValue, TPlus, TMinus, Remarks, Report, BtnExec, BtnEdit, TextObj, CmbResult
        }

        private static int GetDistance(double x1, double y1, double x2, double y2)
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }


        private static Point FindNearestPointOnCircle(int cX,int cY,int pX,int pY,double R)
        {

            double vX = pX - cX;
            double vY = pY - cY;
            double magV = Math.Sqrt(vX * vX + vY * vY);

            double aX = cX + vX / magV * R;
            double aY = cY + vY / magV * R;

            return new Point((int)aX, (int)aY);
        }

        public static List<Point> getArcPoints(Point obj_Center, float obj_Radius, float start_Angle, float sweep_Angle)
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


        private static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private static List<Point> OrderByDistance(List<Point> pointList,Point pt)
        {
            var orderedList = new List<Point>();

            var currentPoint = pointList[0];
            currentPoint = pt;

                var closestPointIndex = 0;
                var closestDistance = double.MaxValue;

                for (var i = 0; i < pointList.Count; i++)
                {
                    var distance = Distance(currentPoint, pointList[i]);
                    if (distance < closestDistance)
                    {
                        closestPointIndex = i;
                        closestDistance = distance;
                    }
                }

                currentPoint = pointList[closestPointIndex];
            

            // Add the last point.
            orderedList.Add(currentPoint);

            return orderedList;
        }

        private static Point FindNearestPointOnArc(int cX, int cY, int pX, int pY, double R,float sAngle,float swAngle)
        {
            List<Point> ArcPoints=getArcPoints(new Point(cX, cY), (float)R, sAngle, swAngle);

            List<Point> ArcPoints1 = OrderByDistance(ArcPoints, new Point(pX, pY));

            return ArcPoints1[0];
        }


        private static Point FindNearestPointOnCur(List<Point> CurPoints, int pX, int pY)
        {
         
            List<Point> ArcPoints1 = OrderByDistance(CurPoints, new Point(pX, pY));

            return ArcPoints1[0];
        }


        public static Point RotatePoint(Point pointToRotate, PointF centerPoint, double angleInDegrees)
        {
            angleInDegrees = angleInDegrees * -1;

            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public static string Compute(List<DrawObject> DrawObjects,MDIParent1 mp, string cmbObj1, string cmbObj2, imageWM imgpage, string MType,int RowIndex, Random Rnd)
        {
            //err1.SetVal = "";

            string err = "";

            
            mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = "";
            mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = "";



            //mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Obj1].Value = cmbObj1;
            //mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Obj2].Value = cmbObj2;

            if (mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.TextObj].Value == null)
                mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.TextObj].Value = "";

            string TextObj1 = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.TextObj].Value.ToString();

            int j;
            for (j = 0; j <= imgpage.DrawObjects.Count - 1; j++)
            {
                cLine tobj1 = (cLine)imgpage.DrawObjects[j].Obj;
                if (tobj1.LineName == TextObj1)
                {

                    imgpage.DrawObjects.RemoveAt(j);
                    DrawObjects = imgpage.DrawObjects;
                    goto l1;
                }
                if(tobj1.MObj!=null)
                { 
                if (tobj1.MObj.LineName == TextObj1)
                {
                    tobj1.MObj = null;
                    DrawObjects = imgpage.DrawObjects;
                    goto l1;
                }
                }
            }

        l1:
            imgpage.img1.Invalidate();

            double xValue = 0;
            double yValue = 0;
            System.Data.DataRow[] dr = com.DtXY.Select("mag='" + com.PubCMag.ToString() + "'");
            if (dr.Length > 0)
            {
                xValue = Convert.ToDouble(dr[0]["xValue"]);
                yValue = Convert.ToDouble(dr[0]["yValue"]);
            }

            if (cmbObj1 == "")
            {
                err = "";
                return err;
            }

            //single obj -start 
            if ((cmbObj1 != "") && (cmbObj2 == ""))
            {
                cLine o = GetObj(cmbObj1, DrawObjects);
                if (o == null)
                    return err;

                o.MObj = null;

                if ((o.ObjType == "LINE") || (o.ObjType == "PPLINE"))
                {

                    com.Length = o.GetLength(xValue, yValue).ToString();
                    com.Remarks = o.LineName + "-Len";
                    com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();

                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;

                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                   
                    obj.LineName = "T" + Rnd.Next(10000,99999).ToString();
               
                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showlength = true;
                    o.MObj = obj;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.TextObj].Value = obj.LineName;

                    imgpage.img1.Invalidate();


                    return err;
                }
                else if (o.ObjType == "CIRCLE")
                {
                    if (MType == "RADIUS")
                    {
                        com.Length = o.GetRadius(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Rad";
                    }


                    else if (MType == "AREA")
                    {
                        com.Length = o.GetCircleArea(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Area";
                    }
                    else if (MType == "DIAMETER")
                    {
                        com.Length = (o.GetRadius(xValue, yValue) * 2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Dia";
                    }
                    else if (MType == "CIRCUMFERENCE")
                    {
                        com.Length = (o.GetCircleCircum(xValue, yValue) * 2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Cirm";
                    }


                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return err;

                }


                else if (o.ObjType == "ARC")
                {
                    if (MType == "RADIUS")
                    {
                        com.Length = o.GetRadius(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Rad";
                    }


                    else if (MType == "AREA")
                    {
                        com.Length = o.GetCircleArea(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Area";
                    }
                    else if (MType == "DIAMETER")
                    {
                        com.Length = (o.GetRadius(xValue, yValue) * 2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Dia";
                    }
                    else if (MType == "CIRCUMFERENCE")
                    {
                        com.Length = (o.GetCircleCircum(xValue, yValue) * 2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Cirm";
                    }
                    else
                    {
                        com.Length = (o.ArcLength(xValue, yValue) ).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();
                        com.Remarks = o.LineName + "-Len";

                    }

                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return err;

                }


                else if ((o.ObjType == "CURVE")|| (o.ObjType == "CCURVE"))
                {
                        com.Length = (o.CurLength(xValue, yValue)/1000).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();
                        com.Remarks = o.LineName + "-Len";

                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return err;

                }


                else if (o.ObjType == "RECT")
                {
                    if (MType == "AREA")
                    {
                        com.Length = o.GetRectArea(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Area";
                    }
                    else if (MType == "CIRCUMFERENCE")
                    {
                        com.Length = o.GetRectCicum(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length),MySettings.DPoint).ToString();

                        com.Remarks = o.LineName + "-Cirm";
                    }


                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return err;

                }

                else if (o.ObjType == "ANGLE")
                {
                    double angle = o.GetAngle();

                    com.Length = angle.ToString();
                    com.Remarks = o.LineName + "-Angle";
                    cLine obj = new cLine();

                    obj.X2 = (o.PointList[1].X) + 30;
                    obj.Y2 = (o.PointList[1].Y) - 10;

                    obj.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                    //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                    obj.DrawColor = com.mcolor;
                    obj.DrawWidth = com.mwidth;

                    obj.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + angle + "° ";


                    o.Showlength = true;
                    o.MObj = obj;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;

                    imgpage.img1.Invalidate();


                    return err;


                }

            }
            //single obj -end


            //double obj -start
            else if ((cmbObj1 != "") && (cmbObj2 != ""))
            {
                cLine o1 = GetObj(cmbObj1, DrawObjects);
                cLine o2 = GetObj(cmbObj2, DrawObjects);

                if (o1 == null)
                    return err;
                if (o2 == null)
                    return err;

                //o1.MObj = null;
                //o2.MObj = null;


                string tx1 = "";
                string tx2 = "";

                int X1 = 0;
                int Y1 = 0;
                int X2 = 0;
                int Y2 = 0;


                if ((o1.ObjType == "LINE") || (o1.ObjType == "PPLINE")|| (o1.ObjType == "ARROW"))
                {
                    if (cmbObj1.IndexOf("-START") > 0)
                        tx1 = "START";
                    else if (cmbObj1.IndexOf("-END") > 0)
                        tx1 = "END";


                    if (tx1 == "START")
                    {
                        X1 = o1.X1;
                        Y1 = o1.Y1;

                    }
                    else if (tx1 == "END")
                    {
                        X1 = o1.X2;
                        Y1 = o1.Y2;
                    }
                    else if (tx1 == "")
                    {
                        if (o2.ObjType == "POINT")
                        {
                            Point pp = com.FindNearestPointOnLine(new Point(o1.X1, o1.Y1), new Point(o1.X2, o1.Y2), new Point(o2.X2, o2.Y2));
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else
                        { 
                        X1 = (o1.X1 + o1.X2) / 2;
                        Y1 = (o1.Y1 + o1.Y2) / 2;
                        }
                    }


                }

                else if (o1.ObjType == "RECT")
                {
                    if (o2.ObjType == "POINT")
                    {
                        Rectangle r = new Rectangle(o1.X1, o1.Y1, o1.Width, o1.Height);
                        Point pp = FindNearestPointOnRect(r, new Point(o2.X2, o2.Y2));
                        X1 = pp.X;
                        Y1 = pp.Y;
                    }
                    else if ((o2.ObjType == "LINE") || (o2.ObjType == "PPLINE") || (o2.ObjType == "ARROW") || (o2.ObjType == "CIRCLE"))
                    {
                        int Xx=0;
                        int Yy=0;

                        if (cmbObj2.IndexOf("-START") > 0)
                            tx2 = "START";
                        else if (cmbObj2.IndexOf("-END") > 0)
                            tx2 = "END";
                        else if (cmbObj2.IndexOf("-CENTER") > 0)
                            tx2 = "CENTER";


                        if (tx2 == "START")
                        {
                            Xx = o2.X1;
                            Yy = o2.Y1;
                        }
                        else if (tx2 == "END")
                        {
                            Xx = o2.X2;
                            Yy = o2.Y2;
                        }
                        else if (tx2 == "CENTER")
                        {
                            Xx = o2.X1;
                            Yy = o2.Y1;
                        }
                        else if (tx2 == "")
                        {

                             Xx = (o2.X1 + o2.X2) / 2;
                             Yy = (o2.Y1 + o2.Y2) / 2;
                        }
                        Rectangle r = new Rectangle(o1.X1, o1.Y1, o1.Width, o1.Height);
                        Point pp = FindNearestPointOnRect(r, new Point(Xx, Yy));
                        X1 = pp.X;
                        Y1 = pp.Y;
                    }
                }

                else if (o1.ObjType == "CIRCLE")
                {
                    if (cmbObj1.IndexOf("-CENTER") > 0)
                        tx1 = "CENTER";

                    if (tx1 == "CENTER")
                    {
                        X1 = o1.X1;
                        Y1 = o1.Y1;
                    }
                    else if (tx1 == "")
                    {
                        if (o2.ObjType == "POINT")
                        {
                            Point pp = FindNearestPointOnCircle(o1.X1, o1.Y1, o2.X2, o2.Y2, o1.GetRadius());
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else if ((o2.ObjType == "LINE") || (o2.ObjType == "PPLINE") || (o2.ObjType == "ARROW") || (o2.ObjType == "CIRCLE"))
                        {
                            int Xx = 0;
                            int Yy = 0;

                            if (cmbObj2.IndexOf("-START") > 0)
                                tx2 = "START";
                            else if (cmbObj2.IndexOf("-END") > 0)
                                tx2 = "END";
                            else if (cmbObj2.IndexOf("-CENTER") > 0)
                                tx2 = "CENTER";


                            if (tx2 == "START")
                            {
                                Xx = o2.X1;
                                Yy = o2.Y1;
                            }
                            else if (tx2 == "END")
                            {
                                Xx = o2.X2;
                                Yy = o2.Y2;
                            }
                            else if (tx2 == "CENTER")
                            {
                                Xx = o2.X1;
                                Yy = o2.Y1;
                            }
                            else if (tx2 == "")
                            {

                                Xx = (o2.X1 + o2.X2) / 2;
                                Yy = (o2.Y1 + o2.Y2) / 2;
                            }
                            Rectangle r = new Rectangle(o1.X1, o1.Y1, o1.Width, o1.Height);
                            Point pp = FindNearestPointOnCircle(o1.X1, o1.Y1, Xx, Yy, o1.GetRadius());
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else
                        { 
                        X1 = (o1.X2);
                        Y1 = (o1.Y2);
                        }
                    }
                }



                else if (o1.ObjType == "ARC")
                {
                    
                        if (o2.ObjType == "POINT")
                        {
                            Point pp = FindNearestPointOnArc(o1.X1, o1.Y1, o2.X2, o2.Y2, o1.Arc_Radius,o1.Arc_StartAngle,o1.Arc_SweepAngle);
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else if ((o2.ObjType == "LINE") || (o2.ObjType == "PPLINE") || (o2.ObjType == "ARROW") || (o2.ObjType == "CIRCLE"))
                        {
                            int Xx = 0;
                            int Yy = 0;

                            if (cmbObj2.IndexOf("-START") > 0)
                                tx2 = "START";
                            else if (cmbObj2.IndexOf("-END") > 0)
                                tx2 = "END";
                            else if (cmbObj2.IndexOf("-CENTER") > 0)
                                tx2 = "CENTER";


                            if (tx2 == "START")
                            {
                                Xx = o2.X1;
                                Yy = o2.Y1;
                            }
                            else if (tx2 == "END")
                            {
                                Xx = o2.X2;
                                Yy = o2.Y2;
                            }
                            else if (tx2 == "CENTER")
                            {
                                Xx = o2.X1;
                                Yy = o2.Y1;
                            }
                            else if (tx2 == "")
                            {

                                Xx = (o2.X1 + o2.X2) / 2;
                                Yy = (o2.Y1 + o2.Y2) / 2;
                            }
                            Rectangle r = new Rectangle(o1.X1, o1.Y1, o1.Width, o1.Height);
                            Point pp = FindNearestPointOnArc(o1.X1, o1.Y1, Xx, Yy, o1.Arc_Radius,o1.Arc_StartAngle,o1.Arc_SweepAngle);
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else
                        {
                            X1 = (o1.X2);
                            Y1 = (o1.Y2);
                        }
                    
                }

                else if ((o1.ObjType == "CURVE")|| (o1.ObjType == "CCURVE"))
                {

                    if (o2.ObjType == "POINT")
                    {
                        Point pp = FindNearestPointOnCur(o1.PathPoints, o2.X2, o2.Y2);
                        X1 = pp.X;
                        Y1 = pp.Y;
                    }
                    else if ((o2.ObjType == "LINE") || (o2.ObjType == "PPLINE") || (o2.ObjType == "ARROW") || (o2.ObjType == "CIRCLE"))
                    {
                        int Xx = 0;
                        int Yy = 0;

                        if (cmbObj2.IndexOf("-START") > 0)
                            tx2 = "START";
                        else if (cmbObj2.IndexOf("-END") > 0)
                            tx2 = "END";
                        else if (cmbObj2.IndexOf("-CENTER") > 0)
                            tx2 = "CENTER";


                        if (tx2 == "START")
                        {
                            Xx = o2.X1;
                            Yy = o2.Y1;
                        }
                        else if (tx2 == "END")
                        {
                            Xx = o2.X2;
                            Yy = o2.Y2;
                        }
                        else if (tx2 == "CENTER")
                        {
                            Xx = o2.X1;
                            Yy = o2.Y1;
                        }
                        else if (tx2 == "")
                        {

                            Xx = (o2.X1 + o2.X2) / 2;
                            Yy = (o2.Y1 + o2.Y2) / 2;
                        }
                        Rectangle r = new Rectangle(o1.X1, o1.Y1, o1.Width, o1.Height);
                        Point pp = FindNearestPointOnCur(o1.PathPoints, Xx, Yy);
                        X1 = pp.X;
                        Y1 = pp.Y;
                    }
                    else
                    {
                        X1 = (o1.X2);
                        Y1 = (o1.Y2);
                    }

                }

                else if (o1.ObjType == "POINT")
                {
                    X1 = o1.X2;
                    Y1 = o1.Y2;
                }




                if ((o2.ObjType == "LINE") || (o2.ObjType == "PPLINE") || (o2.ObjType == "ARROW"))
                {
                    if (cmbObj2.IndexOf("-START") > 0)
                        tx2 = "START";
                    else if (cmbObj2.IndexOf("-END") > 0)
                        tx2 = "END";


                    if (tx2 == "START")
                    {
                        X2 = o2.X1;
                        Y2 = o2.Y1;
                    }
                    else if (tx2 == "END")
                    {
                        X2 = o2.X2;
                        Y2 = o2.Y2;
                    }
                    else if (tx2 == "")
                    {

                        if (o1.ObjType == "POINT")
                        {
                            Point pp = com.FindNearestPointOnLine(new Point(o2.X1, o2.Y1), new Point(o2.X2, o2.Y2), new Point(o1.X2, o1.Y2));
                            X1 = pp.X;
                            Y1 = pp.Y;
                        }
                        else
                        {
                            X2 = (o2.X1 + o2.X2) / 2;
                            Y2 = (o2.Y1 + o2.Y2) / 2;
                        }
                     
                    }

                }
                else if (o2.ObjType == "CIRCLE")
                {
                    if (cmbObj2.IndexOf("-CENTER") > 0)
                        tx2 = "CENTER";

                    if (tx2 == "CENTER")
                    {
                        X2 = o2.X2;
                        Y2 = o2.Y2;
                    }
                    else if (tx2 == "")
                    {
                        if (o1.ObjType == "POINT")
                        {
                            Point pp = FindNearestPointOnCircle(o2.X1, o2.Y1, o1.X2, o1.Y2, o2.GetRadius());
                            X2 = pp.X;
                            Y2 = pp.Y;
                        }

                        else if ((o1.ObjType == "LINE") || (o1.ObjType == "PPLINE") || (o1.ObjType == "ARROW") || (o1.ObjType == "CIRCLE"))
                        {
                            int Xx = 0;
                            int Yy = 0;

                            if (cmbObj1.IndexOf("-START") > 0)
                                tx1 = "START";
                            else if (cmbObj2.IndexOf("-END") > 0)
                                tx1 = "END";
                            else if (cmbObj2.IndexOf("-CENTER") > 0)
                                tx1 = "CENTER";


                            if (tx1 == "START")
                            {
                                Xx = o1.X1;
                                Yy = o1.Y1;
                            }
                            else if (tx1 == "END")
                            {
                                Xx = o1.X2;
                                Yy = o1.Y2;
                            }
                            else if (tx1 == "CENTER")
                            {
                                Xx = o1.X1;
                                Yy = o1.Y1;
                            }
                            else if (tx1 == "")
                            {

                                Xx = (o1.X1 + o1.X2) / 2;
                                Yy = (o1.Y1 + o1.Y2) / 2;
                            }
                            Rectangle r = new Rectangle(o2.X1, o2.Y1, o2.Width, o2.Height);
                            Point pp = FindNearestPointOnCircle(o2.X1,o2.Y1,Xx,Yy,o2.GetRadius());
                            X2 = pp.X;
                            Y2 = pp.Y;
                        }
                        else
                        {
                            X2 = (o2.X2);
                            Y2 = (o2.Y2);
                        }

                    }
                }



                else if (o2.ObjType == "ARC")
                {

                    if (o1.ObjType == "POINT")
                    {
                        Point pp = FindNearestPointOnArc(o2.X1, o2.Y1, o1.X2, o1.Y2, o2.Arc_Radius, o2.Arc_StartAngle, o2.Arc_SweepAngle);
                        X2 = pp.X;
                        Y2 = pp.Y;
                    }
                    else if ((o1.ObjType == "LINE") || (o1.ObjType == "PPLINE") || (o1.ObjType == "ARROW") || (o1.ObjType == "CIRCLE"))
                    {
                        int Xx = 0;
                        int Yy = 0;

                        if (cmbObj1.IndexOf("-START") > 0)
                            tx1 = "START";
                        else if (cmbObj1.IndexOf("-END") > 0)
                            tx1 = "END";
                        else if (cmbObj1.IndexOf("-CENTER") > 0)
                            tx1 = "CENTER";


                        if (tx1 == "START")
                        {
                            Xx = o1.X1;
                            Yy = o1.Y1;
                        }
                        else if (tx1 == "END")
                        {
                            Xx = o1.X2;
                            Yy = o1.Y2;
                        }
                        else if (tx1 == "CENTER")
                        {
                            Xx = o1.X1;
                            Yy = o1.Y1;
                        }
                        else if (tx1 == "")
                        {

                            Xx = (o1.X1 + o1.X2) / 2;
                            Yy = (o1.Y1 + o1.Y2) / 2;
                        }
                        Rectangle r = new Rectangle(o2.X1, o2.Y1, o2.Width, o2.Height);
                        Point pp = FindNearestPointOnArc(o2.X1, o2.Y1, Xx, Yy, o2.Arc_Radius, o2.Arc_StartAngle, o2.Arc_SweepAngle);
                        X2 = pp.X;
                        Y2 = pp.Y;
                    }
                    else
                    {
                        X2 = (o2.X2);
                        Y2 = (o2.Y2);
                    }

                }



                else if (o2.ObjType == "RECT")
                {
                    if (o1.ObjType == "POINT")
                    {
                        Rectangle r = new Rectangle(o2.X1, o2.Y1, o2.Width, o2.Height);
                        Point pp = FindNearestPointOnRect(r, new Point(o1.X2, o1.Y2));
                        X2 = pp.X;
                        Y2 = pp.Y;

                    }

                    else if ((o1.ObjType == "LINE") || (o1.ObjType == "PPLINE") || (o1.ObjType == "ARROW")|| (o1.ObjType == "CIRCLE"))
                    {
                        int Xx=0;
                        int Yy=0;

                        if (cmbObj1.IndexOf("-START") > 0)
                            tx1 = "START";
                        else if (cmbObj2.IndexOf("-END") > 0)
                            tx1 = "END";
                        else if (cmbObj2.IndexOf("-CENTER") > 0)
                            tx1 = "CENTER";


                        if (tx1 == "START")
                        {
                            Xx = o1.X1;
                            Yy = o1.Y1;
                        }
                        else if (tx1 == "END")
                        {
                            Xx = o1.X2;
                            Yy = o1.Y2;
                        }
                        else if (tx1 == "CENTER")
                        {
                            Xx = o1.X1;
                            Yy = o1.Y1;
                        }
                        else if (tx1 == "")
                        {

                            Xx = (o1.X1 + o1.X2) / 2;
                            Yy = (o1.Y1 + o1.Y2) / 2;
                        }
                        Rectangle r = new Rectangle(o2.X1, o2.Y1, o2.Width, o2.Height);
                        Point pp = FindNearestPointOnRect(r, new Point(Xx, Yy));
                        X2 = pp.X;
                        Y2 = pp.Y;
                    }

                }
                else if (o2.ObjType == "POINT")
                {
                    X2 = o2.X2;
                    Y2 = o2.Y2;
                }





                com.Length = GetDistance(X1 * xValue, Y1 * yValue, X2 * xValue, Y2 * yValue).ToString();
                com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,MySettings.DPoint).ToString();


                if (MType == "ANGLE")
                {
                    {


                        double theta1 = Math.Atan2(o1.Y1 - o1.Y2, o1.X1 - o1.X2);
                        double theta2 = Math.Atan2(o2.Y1 - o2.Y2, o2.X1 - o2.X2);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);

                        com.Length = angle.ToString() + "° ";


                    }
                }


                com.Remarks = o1.LineName + " & " + o2.LineName;
                string TextObj = "M1L" + Rnd.Next(10000, 99999).ToString();
              
                mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;
                mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.TextObj].Value = TextObj;




                cLine obj = new cLine();
                obj.X1 = X1;
                obj.Y1 = Y1;
                obj.X2 = X2;
                obj.Y2 = Y2;

                obj.X1Copy = X1;
                obj.Y1Copy = Y1;
                obj.X2Copy = X2;
                obj.Y2Copy = Y2;

                //imgpage.MaxLineNumber = imgpage.MaxLineNumber + 1;
                obj.LineName = TextObj;
                obj.DrawColor = com.mcolor;
                obj.DrawWidth = com.mwidth;
                obj.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                //obj.DrawPen = new Pen(Brushes.Black, com.mwidth);
                //obj.DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                obj.Showlength = false;
                obj.Showcircum = false;
                obj.Showarea = false;
                obj.Showdia = false;

                cLine objt = new cLine();
                objt.X2 = ((obj.X1 + obj.X2) / 2) + 10;
                objt.Y2 = ((obj.Y1 + obj.Y2) / 2) - 10;

                objt.LineName = "T" + Rnd.Next(10000, 99999).ToString();

                //objt.DrawPen = new Pen(com.mcolor, com.mwidth);
                objt.DrawColor = com.mcolor;
                objt.DrawWidth = com.mwidth;
                if (MType == "ANGLE")
                    objt.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length;
                else
                    objt.Text = mp.grd2.Rows[RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;
                obj.MObj = objt;
                DrawObject dobj = new DrawObject();
                dobj.ObjType = "M1LINE";
                obj.ObjType = dobj.ObjType;
                dobj.Obj = obj;
                imgpage.DrawObjects.Add(dobj);

                imgpage.img1.Invalidate();


                return err;

            }
            //double obj -end
            return err;

        }




        public static void RCheck()
        {
            try
            {
                string akey = "";
                 
                string akeypath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                akey = System.IO.File.ReadAllText(akeypath + "\\reg.ky");
                
                clsReg regclass = new clsReg();
                string result = regclass.RegCheck(akey);

                string result1 = result.Substring(0, 2);

                if (result1 == "OK")
                {

                }
                else if (result1 == "RD")
                {
                        string[] x = result.Split('-');
                        int days = int.Parse(x[1]);

                        reg f = new reg(days, "");
                        f.ShowDialog();
                
                }
                else
                {
                        reg f = new reg(0, result);
                        f.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                    reg f = new reg(0, "");
                    f.ShowDialog();

            }
           
        }

    }

    public static class ThemeList
    {
        public static List<string> ThemeName = new List<string>();
        public static List<int> KP = new List<int>();
        public static List<int> CP = new List<int>();


        public static void ThemeListAdd()
        {
            

            ThemeList.ThemeName.Add("Office 2007 Black");
            ThemeList.KP.Add(10);
            ThemeList.CP.Add(5);

            ThemeList.ThemeName.Add("Office 2007 Blue");
            ThemeList.KP.Add(3);
            ThemeList.CP.Add(3);

            ThemeList.ThemeName.Add("Office 2007 Silver");
            ThemeList.KP.Add(6);
            ThemeList.CP.Add(4);

            ThemeList.ThemeName.Add("Office 2010 Black");
            ThemeList.KP.Add(19);
            ThemeList.CP.Add(8);

            ThemeList.ThemeName.Add("Office 2010 Blue");
            ThemeList.KP.Add(12);
            ThemeList.CP.Add(6);

            ThemeList.ThemeName.Add("Office 2010 Silver");
            ThemeList.KP.Add(15);
            ThemeList.CP.Add(7);

            ThemeList.ThemeName.Add("Sparkle Blue");
            ThemeList.KP.Add(31);
            ThemeList.CP.Add(9);

            ThemeList.ThemeName.Add("Sparkle Orange");
            ThemeList.KP.Add(34);
            ThemeList.CP.Add(10);

            ThemeList.ThemeName.Add("Sparkle Purple");
            ThemeList.KP.Add(37);
            ThemeList.CP.Add(11);

        }

    }


    internal static class Program
    {
          
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Global._applicationPath = Path.GetDirectoryName(Application.ExecutablePath);

            Application.Run(new frmLogin());
            //Application.Run(new camera());
            //Application.Run(new test.sakthipower());

          //Application.Run(new testnew());

        }

    }

        public static class Theme
        {

        public static System.Drawing.Color TextColor1 { get; set; }
        public static System.Drawing.Color TextColor2 { get; set; }

        public static System.Drawing.Color FormHeadColor1 { get; set; }
           public static System.Drawing.Color FormHeadColor2 { get; set; }

           public static System.Drawing.Color FormBackColor1 { get; set; }
           public static System.Drawing.Color FormBackColor2 { get; set; }

           public static System.Drawing.Color FormBorderColor1 { get; set; }
           public static System.Drawing.Color FormBorderColor2 { get; set; }

        public static System.Drawing.Color ButtonColor1 { get; set; }
        public static System.Drawing.Color ButtonColor2 { get; set; }
    }


    public static class MySettings
    {
        public static string CmpName { get; set; }
        public static string Add1 { get; set; }
        public static string Add2 { get; set; }
        public static string City { get; set; }
        public static string State { get; set; }
        public static string Country { get; set; }
        public static int DPoint { get; set; }
        public static string AngleDis { get; set; }
        public static string ReportLocation { get; set; }
        public static string LogoLeft { get; set; }
        public static string LogoRight { get; set; }
        public static string ReportFormat { get; set; }
        public static string ImgFormat { get; set; }
        public static int NoOfImageDis { get; set; }
        public static int NoOfWorkDis { get; set; }
        public static int MaxMergedReport { get; set; }
        public static string Caption { get; set; }
        public static int Threshold { get; set; }
        public static int Status { get; set; }
        public static int EValue { get; set; }
        public static int Tol { get; set; }
        public static int Thick { get; set; }
        public static int AutoDraw { get; set; }
        public static int OWidth { get; set; }
        public static int FontSize { get; set; }
        public static int LT { get; set; }
        public static int LTP { get; set; }
        public static int isDate { get; set; }
        public static int isResult { get; set; }
        public static int isName { get; set; }
        public static int Thresholding { get; set; }


        public static void ReadSettings()
        {
            MySql.Data.MySqlClient.MySqlDataReader rst;

            rst = com.sqlcn.Getdata("select * from Settings");
            if (rst.Read())
            {

                CmpName = rst["CmpName"].ToString();
                Add1 = rst["Add1"].ToString();
                Add2 = rst["Add2"].ToString();
                City = rst["City"].ToString();
                State = rst["State"].ToString();
                Country = rst["Country"].ToString();
                DPoint = (int)rst["DPoint"];
                AngleDis = rst["AngleDis"].ToString();
                ReportLocation = rst["ReportLocation"].ToString();
                LogoLeft = rst["LogoLeft"].ToString();
                LogoRight = rst["LogoRight"].ToString();
                ReportFormat = rst["ReportFormat"].ToString();
                ImgFormat = rst["ImgFormat"].ToString();
                NoOfImageDis = (int)rst["NoOfImageDis"];
                NoOfWorkDis = (int)rst["NoOfWorkDis"];
                MaxMergedReport = (int)rst["MaxMergedReport"];
                Caption = rst["Caption"].ToString();
                Threshold = (int)rst["Threshold"];
                Status = (int)rst["Status"];
                EValue = (int)rst["EValue"];
                Tol = (int)rst["Tol"];
                Thick = (int)rst["Thick"];
                AutoDraw = (int)rst["AutoDraw"];
                OWidth = (int)rst["OWidth"];
                FontSize = (int)rst["FontSize"];
                LT = (int)rst["LT"];
                LTP = (int)rst["LTP"];
                isDate = (int)rst["isDate"];
                isResult = (int)rst["isResult"];
                isName = (int)rst["isName"];
                Thresholding = (int)rst["Thresholding"];

            }
            rst.Close();
            }
        }

    public static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

       

        }

}
