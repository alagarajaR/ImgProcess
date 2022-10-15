using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;

namespace ImgProcess
{
    public  class mysqlclass
    {
       MySql.Data.MySqlClient.MySqlConnection sqlcn =new MySql.Data.MySqlClient.MySqlConnection();
       public string ErrMsg { get; set; }
        public bool OpenCon()
        {
            bool rtn = false;

            string Server = "";
            string UserName = "";
            string Password = "";
            string Database = "";

            string Fname = AppDomain.CurrentDomain.BaseDirectory + "\\cn.txt";

            string FullText = System.IO.File.ReadAllText(Fname);
            string[] x = FullText.Split(',');
            Server = x[0];
            UserName = x[1];
            Password = x[2];
            Database = x[3];

            sqlcn.ConnectionString = "server="+Server+";uid="+UserName+";pwd="+Password+";database="+Database+"";
            try
            {
                sqlcn.Open();
                ErrMsg = "";
                rtn = true;


            }
            catch (Exception ex)
            {
                ErrMsg=ex.Message; 
                rtn = false;
            }


            return (rtn);
            
        }
        public bool CloseCon()
        {
            bool rtn = false;
            try
            {
                sqlcn.Close();
                ErrMsg = "";
                rtn = true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;

                rtn = false;
            }


            return (rtn);
        }
        public MySql.Data.MySqlClient.MySqlDataReader Getdata(string Query)
        {

            MySql.Data.MySqlClient.MySqlDataReader rst;

            if (sqlcn.State == System.Data.ConnectionState.Closed)
            {
                OpenCon();
            }

            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection = sqlcn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = Query;
                rst = cmd.ExecuteReader();

                ErrMsg = "";
            }
            catch (Exception ex)
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                path = path + "\\errLog\\";

                string FileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_tt") + ".txt";
                path = path + FileName;
                path = path.Replace("file:\\", "");

                string Inexp = "";
                if (ex.InnerException != null)
                {
                    Inexp = ex.InnerException.Message;
                }

                string LastErrLog = "Query: " + Query + '\n' + "Err: " + ex.Message + "\nInner Err: " + Inexp;


                System.IO.File.WriteAllText(path, LastErrLog);

                throw new Exception(ex.Message);
            }
            return rst;

        }
        public System.Data.DataTable Gettable(string Query)
        {

            System.Data.DataTable Dt=new System.Data.DataTable();

            if (sqlcn.State == System.Data.ConnectionState.Closed)
            {
                OpenCon();
            }
            try
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection = sqlcn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = Query;
                MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                da.Fill(Dt);
                ErrMsg = "";
            }
            catch (Exception ex)
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                path = path + "\\errLog\\";

                string FileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_tt") + ".txt";
                path = path + FileName;
                path = path.Replace("file:\\", "");

                string Inexp = "";
                if (ex.InnerException != null)
                {
                    Inexp = ex.InnerException.Message;
                }

                string LastErrLog = "Query: " + Query + '\n' + "Err: " + ex.Message + "\nInner Err: " + Inexp;


                System.IO.File.WriteAllText(path, LastErrLog);

                throw new Exception(ex.Message);
            }
            return Dt;

        }
        public void Exec(string Query)
        {

            if (sqlcn.State == System.Data.ConnectionState.Closed)
            {
                OpenCon();
            }

                MySql.Data.MySqlClient.MySqlCommand cmd=new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection= sqlcn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = Query;

            try
            {
                cmd.ExecuteNonQuery();
                ErrMsg = "";

            }
            catch (Exception ex)
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                path=path + "\\errLog\\";

                string FileName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_tt") + ".txt";
                path = path + FileName;
                path = path.Replace("file:\\", "");

                string Inexp = "";
                if (ex.InnerException != null)
                {
                    Inexp=ex.InnerException.Message;
                }

                string LastErrLog = "Query: " + Query + '\n' + "Err: " + ex.Message +  "\nInner Err: " + Inexp;


                System.IO.File.WriteAllText(path, LastErrLog);

                throw new Exception(ex.Message);
            }
            
           


        }
    }
}
