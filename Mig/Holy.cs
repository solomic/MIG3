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
    public partial class fHolyForm : Form
    {
        public fHolyForm()
        {
            InitializeComponent();
        }

        private void fHolyForm_Load(object sender, EventArgs e)
        {
            bsHoly.DataSource = DB.QueryTableMultipleParams("SELECT dt as \"Дата\", case when type='HOLYDAY' THEN 'Праздник' else 'Исключение' END as \"Тип\"  FROM cmodb.holliday ORDER BY dt ASC;", null);
            adgHoly.DataSource = bsHoly;
        }
    }
}
