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
using MySql.Data.MySqlClient;
using ImgProcess.Objects;
namespace ImgProcess
{
    public partial class welmet1 : KryptonForm
    {
        List<DrawObject> DrawObjects;
        MDIParent1 mp;
        imageWM imgpage;
        string xobj1;
        string xobj2;
        string MType;
        public welmet1(List<DrawObject> drawObjects, MDIParent1 mp1, imageWM imgpage1, string obj11, string obj21,string mtype1)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.kryptonPalette1.BasePaletteMode = com.kp;
            DrawObjects = drawObjects;
            mp = mp1;
            imgpage = imgpage1;
            xobj1 = obj11;
            xobj2 = obj21;
            MType = mtype1;

        }

        private bool ObjLoad(string MType, string ObjType)
        {

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
        private void welmet1_Load(object sender, EventArgs e)
        {
            grd1.Rows.Clear();
            int i;
            cmbObj1.Items.Clear();
            cmbObj1.Items.Add("");
            cmbObj2.Items.Clear();
            cmbObj2.Items.Add("");


            //            "LENGTH", "ANGLE", "DEPTH", "LEG", "THICKNESS", "MIN THICKNESS", "THROAT", "ROOT PENETRATION", "GAP", "UNDERCUT",
            //            "MELT THROUGH", "%PENETRATION", "AREA", "DIAMETER", "RADIUS", "CIRCUMFERENCE"

            for (i = 0; i <= DrawObjects.Count - 1; i++)
            {
                cLine o = (cLine)DrawObjects[i].Obj;

                if((o.ObjType == "LINE")|| (o.ObjType == "POINT") || (o.ObjType == "PPLINE") || (o.ObjType == "CIRCLE") || (o.ObjType == "RECT") || (o.ObjType == "ANGLE"))
                { 
                grd1.Rows.Add();
                grd1.Rows[grd1.Rows.Count -1].Cells[0].Value = o.LineName;
                grd1.Rows[grd1.Rows.Count - 1].Cells[1].Value = o.ObjType;
                }


                if (o.ObjType == "LINE")
                {
                    if(ObjLoad(MType,o.ObjType))
                    {
                        if (MType == "ANGLE")
                        {
                            cmbObj1.Items.Add(o.LineName);
                            cmbObj2.Items.Add(o.LineName);
                        }
                        else
                        {
                            cmbObj1.Items.Add(o.LineName);
                            cmbObj1.Items.Add(o.LineName + "-START");
                            cmbObj1.Items.Add(o.LineName + "-END");

                            cmbObj2.Items.Add(o.LineName);
                            cmbObj2.Items.Add(o.LineName + "-START");
                            cmbObj2.Items.Add(o.LineName + "-END");
                        }
                    }
                }
                else if (o.ObjType == "PPLINE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        if (MType == "ANGLE")
                        {
                            cmbObj1.Items.Add(o.LineName);
                            cmbObj2.Items.Add(o.LineName);
                        }
                        else
                        {
                            cmbObj1.Items.Add(o.LineName);
                            cmbObj1.Items.Add(o.LineName + "-START");
                            cmbObj1.Items.Add(o.LineName + "-END");

                            cmbObj2.Items.Add(o.LineName);
                            cmbObj2.Items.Add(o.LineName + "-START");
                            cmbObj2.Items.Add(o.LineName + "-END");
                        }
                    }
                }
                else if (o.ObjType == "POINT")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        cmbObj1.Items.Add(o.LineName);
                        cmbObj2.Items.Add(o.LineName);
                    }
                }
                else if (o.ObjType == "CIRCLE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        cmbObj1.Items.Add(o.LineName);
                        cmbObj1.Items.Add(o.LineName + "-CENTER");

                        if ((MType != "AREA") && (MType != "DIAMETER") && (MType != "CIRCUMFERENCE") & (MType != "RADIUS"))
                        {
                            cmbObj2.Items.Add(o.LineName);
                            cmbObj2.Items.Add(o.LineName + "-CENTER");

                        }

                    }
                }
                else if (o.ObjType == "RECT")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        cmbObj1.Items.Add(o.LineName);

                        if ((MType != "AREA") && (MType != "DIAMETER") && (MType != "CIRCUMFERENCE") && (MType != "RADIUS"))
                        {
                            cmbObj2.Items.Add(o.LineName);


                        }
                    }
                }
                else if (o.ObjType == "ANGLE")
                {
                    if (ObjLoad(MType, o.ObjType))
                    {
                        cmbObj1.Items.Add(o.LineName);
                    }
                }
        }

          
           

            cmbObj1.Text = xobj1;
            cmbObj2.Text = xobj2;


            cmbObj1.Refresh();
            cmbObj2.Refresh();


        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private cLine GetObj(string ObjName)
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


        public enum mpgcol
        {
            MeasureName, MeasureType, EValue, TPlus, TMinus, Value, Remarks,Report, BtnExec, BtnEdit,TextObj,Obj1,Obj2
        }

        private static int GetDistance(double x1, double y1, double x2, double y2)
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }

        


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            err1.SetVal="";

            Random Rnd = new Random();

            err1.SetVal=com.Compute(DrawObjects,mp, cmbObj1.Text, cmbObj2.Text, imgpage, MType,mp.grd2.CurrentCell.RowIndex,Rnd);

            return;


            mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = "";
            mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = "";



            mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Obj1].Value = cmbObj1.Text;
            mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Obj2].Value = cmbObj2.Text;

            string TextObj1 = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.TextObj].Value.ToString();

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

            if (cmbObj1.Text == "")
            { 
                err1.SetVal = "Please Select Object 1";
                return;
            }

            if((cmbObj1.Text!="") && (cmbObj2.Text==""))
            {
                cLine o = GetObj(cmbObj1.Text);
                if (o == null)
                    return;

                o.MObj = null;

                if ((o.ObjType == "LINE") || (o.ObjType == "PPLINE"))
                {

                    com.Length = o.GetLength(xValue, yValue).ToString();
                    com.Remarks = o.LineName + "-Len";
                    com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000,3).ToString();

                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(Brushes.Black, com.pwidth);
                    obj.DrawColor = Color.Black;
                    obj.DrawWidth = com.pwidth;

                    obj.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showlength = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return;
                }
                else if (o.ObjType == "CIRCLE")
                {
                    if (MType == "RADIUS")
                    {
                        com.Length = o.GetRadius(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length)/1000 , 3).ToString();

                        com.Remarks = o.LineName + "-Rad";
                    }
                    else if (MType == "AREA")
                    {
                        com.Length = o.GetCircleArea(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) , 3).ToString();

                        com.Remarks = o.LineName + "-Area";
                    }
                    else if (MType == "DIAMETER")
                    {
                        com.Length = (o.GetRadius(xValue, yValue)*2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length)/1000 , 3).ToString();

                        com.Remarks = o.LineName + "-Dia";
                    }
                    else if (MType == "CIRCUMFERENCE")
                    {
                        com.Length = (o.GetCircleCircum(xValue, yValue) * 2).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) , 3).ToString();

                        com.Remarks = o.LineName + "-Cirm";
                    }


                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(Brushes.Black, com.pwidth);
                    obj.DrawColor = Color.Black;
                    obj.DrawWidth = com.pwidth;

                    obj.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return;

                }




                else if (o.ObjType == "RECT")
                {
                   if (MType == "AREA")
                    {
                        com.Length = o.GetRectArea(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) , 3).ToString();

                        com.Remarks = o.LineName + "-Area";
                    }
                    else if (MType == "CIRCUMFERENCE")
                    {
                        com.Length = o.GetRectCicum(xValue, yValue).ToString();
                        com.Length = Math.Round(Convert.ToDouble(com.Length) , 3).ToString();

                        com.Remarks = o.LineName + "-Cirm";
                    }


                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;




                    cLine obj = new cLine();
                    obj.X2 = ((o.X1 + o.X2) / 2) + 10;
                    obj.Y2 = ((o.Y1 + o.Y2) / 2) - 20;

                    obj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(Brushes.Black, com.pwidth);
                    obj.DrawColor = Color.Black;
                    obj.DrawWidth = com.pwidth;

                    obj.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;


                    o.Showdia = true;
                    o.MObj = obj;

                    imgpage.img1.Invalidate();


                    return;

                }

                else if (o.ObjType == "ANGLE")
                {
                    double angle = o.GetAngle();

                    com.Length = angle.ToString();
                    com.Remarks = o.LineName + "-Angle";
                    cLine obj = new cLine();

                    obj.X2 = (o.PointList[1].X) + 30;
                    obj.Y2 = (o.PointList[1].Y) - 10;

                    obj.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //obj.DrawPen = new Pen(Brushes.Black, com.pwidth);
                    obj.DrawColor = Color.Black;
                    obj.DrawWidth = com.pwidth;

                    obj.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + angle + "° ";


                    o.Showlength = true;
                    o.MObj = obj;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;

                    imgpage.img1.Invalidate();


                    return;


                }

            }
            else if ((cmbObj1.Text != "") && (cmbObj2.Text != ""))
            {
                cLine o1 = GetObj(cmbObj1.Text);
                cLine o2 = GetObj(cmbObj2.Text);

                if (o1 == null)
                    return;
                if (o2 == null)
                    return;

                o1.MObj = null;
                o2.MObj = null;


                string tx1 = "";
                string tx2 = "";

                int X1 = 0;
                int Y1 = 0;
                int X2 = 0;
                int Y2 = 0;


                if ((o1.ObjType == "LINE") || (o1.ObjType == "PPLINE"))
                {
                    if (cmbObj1.Text.IndexOf("-START") > 0)
                        tx1 = "START";
                    else if (cmbObj1.Text.IndexOf("-END") > 0)
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
                        X1 = (o1.X1 + o1.X2) / 2;
                        Y1 = (o1.Y1 + o1.Y2) / 2;
                    }


                }
                else if (o1.ObjType == "CIRCLE")
                {
                    if (cmbObj1.Text.IndexOf("-CENTER") > 0)
                        tx1 = "CENTER";

                    if (tx1 == "CENTER")
                    {
                        X1 = o1.X1;
                        Y1 = o1.Y1;
                    }
                    else if (tx1 == "")
                    {
                        X1 = (o1.X2) ;
                        Y1 = (o1.Y2);
                    }
                }
                else if (o1.ObjType == "POINT")
                {
                    X1 = o1.X2;
                    Y1 = o1.Y2;
                }

                    if ((o2.ObjType == "LINE")|| (o2.ObjType == "PPLINE"))
                    {
                    if (cmbObj2.Text.IndexOf("-START") > 0)
                        tx2 = "START";
                    else if (cmbObj2.Text.IndexOf("-END") > 0)
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
                        X2 = (o2.X1 + o2.X2) / 2;
                        Y2 = (o2.Y1 + o2.Y2) / 2;
                    }

                }
                else if (o2.ObjType == "CIRCLE")
                {
                    if (cmbObj2.Text.IndexOf("-CENTER") > 0)
                        tx2 = "CENTER";

                    if (tx2 == "CENTER")
                    {
                        X2 = o2.X2;
                        Y2 = o2.Y2;
                    }
                    else if (tx2 == "")
                    {
                        X2 = (o2.X2);
                        Y2 = (o2.Y2);
                    }
                }
                else if (o2.ObjType == "POINT")
                {
                    X2 = o2.X2;
                    Y2 = o2.Y2;
                }


                
                    
                
                    com.Length = GetDistance(X1 * xValue, Y1 * yValue, X2 * xValue, Y2 * yValue).ToString();
                    com.Length = Math.Round(Convert.ToDouble(com.Length) / 1000, 3).ToString();


                if (MType == "ANGLE")
                {
                    {


                        double theta1 = Math.Atan2(o1.Y1 - o1.Y2, o1.X1 - o1.X2);
                        double theta2 = Math.Atan2(o2.Y1 - o2.Y2, o2.X1 - o2.X2);
                        //double diff = Math.Abs(theta1 - theta2);
                        double diff = Math.Abs(theta1 - theta2) * 180 / Math.PI;
                        double angle = Math.Round(Math.Min(diff, Math.Abs(180 - diff)), 0);

                        com.Length =angle.ToString() + "° ";
                     

                    }
                }


                    com.Remarks = o1.LineName + " & " + o2.LineName;
                    string TextObj= "M1L" + new Random().Next(100000, 999999).ToString(); 
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Value].Value = com.Length;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.Remarks].Value = com.Remarks;
                    mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.TextObj].Value = TextObj;




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
                    obj.DrawColor = Color.Black;
                    obj.DrawWidth = com.pwidth;
                    obj.LineStyle= System.Drawing.Drawing2D.DashStyle.Dot;
                    //obj.DrawPen = new Pen(Brushes.Black, com.pwidth);
                    //obj.DrawPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    obj.Showlength = false;
                    obj.Showcircum = false;
                    obj.Showarea = false;
                    obj.Showdia = false;
                    obj.Showrad = false;
                    obj.Showangle = false;

                    cLine objt = new cLine();
                    objt.X2 = ((obj.X1 + obj.X2) / 2) + 10;
                    objt.Y2 = ((obj.Y1 + obj.Y2) / 2) - 10;

                    objt.LineName = "T" + new Random().Next(100000, 999999).ToString();
                    //objt.DrawPen = new Pen(com.pcolor, com.pwidth);
                    objt.DrawColor = com.pcolor;
                    objt.DrawWidth = com.pwidth;
                    if(MType=="ANGLE")
                    objt.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length;
                    else
                    objt.Text = mp.grd2.Rows[mp.grd2.CurrentCell.RowIndex].Cells[(int)mpgcol.MeasureName].Value.ToString() + " = " + com.Length + " " + com.PubScaleShort;
                    obj.MObj = objt;
                    DrawObject dobj = new DrawObject();
                    dobj.ObjType = "M1LINE";
                    obj.ObjType = dobj.ObjType;
                    dobj.Obj = obj;
                    imgpage.DrawObjects.Add(dobj);

                    imgpage.img1.Invalidate();


                    return;
                


            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            cmbObj1.Text = "";
            cmbObj2.Text = "";
            kryptonButton1.PerformClick();
        }

        private void err1_Load(object sender, EventArgs e)
        {

        }
    }
}
