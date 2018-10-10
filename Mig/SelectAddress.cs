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
    public partial class fSelectAddress : Form
    {
        public int SelectedAddressCode { get { return Convert.ToInt32( dgAllAddress.CurrentRow.Cells["code"].Value); } }
        public string SelectedFullAddress { get { return dgAllAddress.CurrentRow.Cells["full_address"].Value.ToString(); } }
        public fSelectAddress()
        {
            InitializeComponent();
        }

        private void fSelectAddress_Load(object sender, EventArgs e)
        {
            dgAllAddress.DataSource = DB.QueryTableMultipleParams("SELECT id, code, kladr_code, full_address FROM cmodb.address order by pin desc;", null);
            dgAllAddress.Columns["id"].Visible = false;
            dgAllAddress.Columns["code"].Visible = false;
            dgAllAddress.Columns["kladr_code"].Visible = false;
            dgAllAddress.Columns["full_address"].HeaderText = "Полный адрес";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bindingSource1 = new BindingSource();

                bindingSource1.DataSource = dgAllAddress.DataSource;
                bindingSource1.Filter = "full_address LIKE'%" + textBox1.Text + "%'";
                dgAllAddress.DataSource = bindingSource1;
            }
            catch (Exception err) {
                Logger.Log.Error(err.ToString());
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            fFindAddress fFindAddressForm = new fFindAddress();
            if(fFindAddressForm.ShowDialog(this) == DialogResult.OK)
            {
                int addr_code = fFindAddressForm.Address_code;
                if (addr_code > 0)
                {
                    fSelectAddress_Load(this, e);
                    BindingSource bindingSource1 = new BindingSource();

                    bindingSource1.DataSource = dgAllAddress.DataSource;
                    bindingSource1.Filter = "code =" + addr_code.ToString() ;
                    dgAllAddress.DataSource = bindingSource1;
                }
            }

            fFindAddressForm = null;
        }

        private void dgAllAddress_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void dgAllAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgAllAddress.SelectedRows.Count == 0)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
