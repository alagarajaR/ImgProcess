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

namespace ImgProcess.Objects
{

    public class DrawObject
    {
        public string ObjType { get; set; }
        public object Obj { get; set; }

    }

    public class cLine
    {
        public string ObjType { get; set; }
        public string LineName { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

       public System.Drawing.Drawing2D.DashStyle LineStyle { get; set; }
        public int X1Copy { get; set; }
        public int Y1Copy { get; set; }
        public int X2Copy { get; set; }
        public int Y2Copy { get; set; }

        //public Pen DrawPen { get; set; }

        public List<Point> PathPoints { get; set; }
        public float DrawWidth { get; set; }
        public Color DrawColor { get; set; }

        public string Text { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public AdjustableArrowCap EndCap { get; set; }

        public List<Point> PointList { get; set; }

        public cLine MObj { get; set; }

        public bool Showlength { get; set; }

        public bool Showarea { get; set; }
        public bool Showdia { get; set; }
        public bool Showangle { get; set; }

        public bool Showrad { get; set; }
        public bool Showcircum { get; set; }

        public float Arc_StartAngle { get; set; }

        public Rectangle Arc_Rectangle { get; set; }
        public float Arc_SweepAngle { get; set; }
        public float Arc_Radius { get; set; }

       

        public void PointFtoPointList(PointF[] pf,GraphicsPath gp,Pen dp)
        {
            PathPoints = new List<Point>();
            int i = 0;
            for (i = 0; i <= pf.Length - 1; i++)
            {
               if(gp.IsOutlineVisible(new Point((int)pf[i].X, (int)pf[i].Y),dp))    
                    PathPoints.Add(new Point((int)pf[i].X, (int)pf[i].Y));
            }
        }

        public float CurLength(double xvalue, double yvalue)
        {
            float clength = 0;

            int i = 0;
            for (i = 0; i <= PathPoints.Count - 1; i++)
            {
                Point p1=new Point(0,0);
                Point p2 = new Point(0, 0);
                p1 = PathPoints[i];

                try
                {
                    p2 = PathPoints[i + 1];
                }
                catch
                {
                    p2 = PointList[PointList.Count - 1];
                }
                clength=clength+(float)GetDistance(p1.X, p1.Y, p2.X, p2.Y);
                
            }
            clength = clength * (float)xvalue;


            return clength;
        }
        public float ArcLength(double xvalue, double yvalue)
        {
            float alength = 0;


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


           Point obj_Center = getArcCenter(p1, p2, p3);
           float start_Angle = get_Start_Angle(obj_Center, arc_Start_Pt);
           float sweep_Angle = get_Sweep_Angle(obj_Center, arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);

            alength = ((float)((2 * Math.PI * (Arc_Radius)) * (sweep_Angle / 360)) * (float)xvalue)/1000;


            return alength;

        }
        public double GetLength(double xvalue,double yvalue)
        {
            return Math.Round(Convert.ToDouble(Math.Sqrt(Math.Pow(((X2*xvalue) - (X1*xvalue)), 2) + Math.Pow(((Y2*yvalue) - (Y1*yvalue)), 2))),3);
        }


        public double GetWidth(double xvalue, double yvalue)
        {
            double width = Math.Abs(X1 - X2);
            return width;
        }


        public double GetHeight(double xvalue, double yvalue)
        {
            double height = Math.Abs(Y1 - Y2);
            return height;
        }

        public double GetRadius(double xvalue, double yvalue)
        {
            return Math.Round(Convert.ToDouble(Math.Sqrt(Math.Pow(((X2 * xvalue) - (X1 * xvalue)), 2) + Math.Pow(((Y2 * yvalue) - (Y1 * yvalue)), 2))),3);
        }

        public double GetRadius()
        {
            return Math.Round(Convert.ToDouble(Math.Sqrt(Math.Pow(((X2 ) - (X1 )), 2) + Math.Pow(((Y2 ) - (Y1 )), 2))), 3);
        }

        public double GetCircleArea(double xvalue, double yvalue)
        {
            double  R = GetRadius(xvalue, yvalue) / 1000.0;
            double Area = (3.14 * R * R);
            return Math.Round(Area,3);
        }

        public double GetCircleCircum(double xvalue, double yvalue)
        {
            double R = GetRadius(xvalue, yvalue) / 1000.0;
            double Cir = 2 * 3.14 * R ;
            return Math.Round(Cir,3);
        }


        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Round(Convert.ToDouble(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2))),3);
        }


        public double GetRectLength(double xvalue,double yvalue)
        {
            double l = GetDistance(X1 * xvalue, Y1 * yvalue, X2 * xvalue, Y1 * yvalue);
            return Math.Round(l,3);
        }

        public double GetRectBearth(double xvalue, double yvalue)
        {
            double b = GetDistance(X1 * xvalue, Y1 * yvalue, X1 * xvalue, Y2 * yvalue);
            return Math.Round(b,3);
        }

        public double GetLineAngle()
        {

          

            double theta1 = Math.Atan2(Y1 - Y2, X1 - X2);
            double theta2 = Math.Atan2(10 - 10, 10 - 100);
            //double diff = Math.Abs(theta1 - theta2);
            double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
            double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);


            return angle;

        }


        public double GetAngle()
        {

            Point[] points = new Point[PointList.Count];


            int i = 0;
            for (i = 0; i <= PointList.Count - 1; i++)
            {
                points[i] = PointList[i];
            }

            double theta1 = Math.Atan2(points[0].Y - points[1].Y, points[0].X - points[1].X);
            double theta2 = Math.Atan2(points[1].Y - points[2].Y, points[1].X - points[2].X);
            //double diff = Math.Abs(theta1 - theta2);
            double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
            double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);


            return angle;

        }
   


        public double GetRectArea(double xvalue, double yvalue)
        {
            double l = GetDistance(X1 * xvalue, Y1 * yvalue, X2 * xvalue, Y1 * yvalue)/1000.0;
            double b = GetDistance(X1 * xvalue, Y1 * yvalue, X1 * xvalue, Y2 * yvalue)/ 1000.0;

            return l*b;
        }

        public double GetRectCicum(double xvalue, double yvalue)
        {
            double l = GetDistance(X1 * xvalue, Y1 * yvalue, X2 * xvalue, Y1 * yvalue) / 1000.0;
            double b = GetDistance(X1 * xvalue, Y1 * yvalue, X1 * xvalue, Y2 * yvalue) / 1000.0;

            return l+l+b+b;
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


        public void getArcPoints(Point obj_Center, float obj_Radius, float start_Angle, float sweep_Angle)
        {
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
                    //obj_Pts.Add(num, new System.Drawing.Point((int)System.Math.Round(num6), (int)System.Math.Round(num7)));
                }
            }
        }



    }


}
