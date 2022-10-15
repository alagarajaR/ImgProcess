using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgProcess
{
    public partial class testnew : Form
    {
        public testnew()
        {
            InitializeComponent();
        }
		float obj_Radius;
		private void button1_Click(object sender, EventArgs e)
        {

			Point obj_Center;
			Rectangle arc_Rect;
			float start_Angle;
			float sweep_Angle;

			Graphics g = this.CreateGraphics();

			arc_Start_Pt = new Point(500, 20);
			arc_Mid_Pt = new Point(200, 150);
			arc_End_Pt = new Point(600, 600);

			g.DrawCircle(Pens.Red, arc_Start_Pt.X,arc_Start_Pt.Y, 3);
			g.DrawCircle(Pens.Red, arc_Mid_Pt.X, arc_Mid_Pt.Y, 3);
			g.DrawCircle(Pens.Red, arc_End_Pt.X, arc_End_Pt.Y, 3);

			obj_Center=getArcCenter(arc_Start_Pt, arc_Mid_Pt, arc_End_Pt);
			arc_Rect = get_ArcRectangle(obj_Center,obj_Radius);
			start_Angle=get_Start_Angle(obj_Center, arc_Start_Pt);
			sweep_Angle=get_Sweep_Angle(obj_Center,arc_Start_Pt,arc_Mid_Pt,arc_End_Pt);
			g.DrawArc(Pens.Blue, arc_Rect, start_Angle, sweep_Angle);
		}



		

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

		public Rectangle get_ArcRectangle(Point obj_Center,float obj_Radius)
		{
			Rectangle arc_Rect;

			float num = (float)obj_Center.X - obj_Radius;
			float num2 = (float)obj_Center.Y - obj_Radius;
			System.Drawing.Rectangle rectangle = (arc_Rect = checked(new System.Drawing.Rectangle((int)System.Math.Round(num), (int)System.Math.Round(num2), (int)System.Math.Round(2f * obj_Radius), (int)System.Math.Round(2f * obj_Radius))));
			return arc_Rect;
		}

		public float get_Start_Angle(System.Drawing.Point arc_C, System.Drawing.Point pt1)
		{
			float start_Angle=0;

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
		public float get_Sweep_Angle(Point obj_Center,Point arc_Start_Pt,Point arc_Mid_Pt,Point arc_End_Pt)
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


		public void getArcPoints(Point obj_Center,float obj_Radius,float start_Angle,float sweep_Angle)
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
