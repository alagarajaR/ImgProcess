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
    public partial class ObjectList : Form
    {
        public ObjectList()
        {
            InitializeComponent();
        }

        private void ObjectList_Load(object sender, EventArgs e)
        {

        }

        private void ObjectList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void lst1_KeyDown(object sender, KeyEventArgs e)
        {
           
            
        }
    }
}
