using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mig
{
    public partial class fFacAddEdit : Form
    {
        public fFacAddEdit()
        {
            InitializeComponent();

        }
        public string FacShortName { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string FacName { get { return textBox2.Text; } set { textBox2.Text = value; } }

        private void btnFacadd_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
