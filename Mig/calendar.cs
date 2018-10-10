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
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Deactivate(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Calendar.SelectionStart = Calendar.SelectionEnd;
            this.Hide();
        }
    }
}
