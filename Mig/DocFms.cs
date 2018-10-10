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
    public partial class fDocFms : Form
    {
        public string DocFmsDt { get { return myDatePicker1.SelectedDate; } }
        public fDocFms()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}
