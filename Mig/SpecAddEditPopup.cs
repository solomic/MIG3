using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pref;

namespace Mig
{
    public partial class fSpecAddEdit : Form
    {
        public fSpecAddEdit()
        {
            InitializeComponent();
        }

        public string SpecCode {get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string SpecName { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string FacName { get { return label4.Text; } set { label4.Text = value; } }

        public string POCode { get { return cmbPO.SelectedIndex==-1?"":cmbPO.SelectedValue.ToString(); } set {cmbPO.Text = value; } }

        public string POCodeStr;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fSpecAddEdit_Load(object sender, EventArgs e)
        {
            cmbPO.DataSource = DB.QueryTableMultipleParams(pref.PO, null);
            cmbPO.SelectedIndex = -1;

            cmbPO.Text = POCodeStr;
        }
    }
}
