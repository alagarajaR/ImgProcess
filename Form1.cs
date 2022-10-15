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
    public partial class Form1 : Form
    {
        mysqlclass sqlcn = new mysqlclass();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            


            if (sqlcn.OpenCon() == true)
            {
                MessageBox.Show("Opened");
            }
            else
            {
                MessageBox.Show(sqlcn.ErrMsg);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            if (sqlcn.CloseCon() == true)
            {
                MessageBox.Show("Closed");
            }
            else
            {
                MessageBox.Show(sqlcn.ErrMsg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            sqlcn.Exec("Insert Into UserTable Values(3,'test3','123')");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = sqlcn.Gettable("select * from usertable");

        }
    }
}
