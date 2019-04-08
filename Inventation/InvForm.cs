using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventation
{
    public partial class fMainForm : Form
    {
        public fMainForm()
        {
            InitializeComponent();
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            fAuth fAuthForm = new fAuth();
            try
            {
                if (fAuthForm.ShowDialog()==DialogResult.OK)
                {
                    this.Text += " (" + pref.Database + ")";
                   // DirectMenu(true);                  
                  //  FilterLoad();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
