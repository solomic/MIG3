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
    public partial class fDocMigrHist : Form
    {
        public fDocMigrHist()
        {
            InitializeComponent();
        }
        public DataTable MigrCardDs { set { dgMigrCard.DataSource = value; } }
        public DataTable DocDs { set { dgDoc.DataSource = value; } }

        private void fDocMigrHist_Load(object sender, EventArgs e)
        {
            DataTable MigrCardTable = DB.QueryTableMultipleParams(pref.GetMigrCardSql, new List<object> { pref.CONTACTID });
            DataTable DocTable = DB.QueryTableMultipleParams(pref.GetDocSql, new List<object> { pref.CONTACTID });
            /*Миграционные карты*/
            this.MigrCardDs = MigrCardTable;
            /*Документы пребывания*/
            this.DocDs = DocTable;

            dgMigrCard.Columns["id"].Visible = false;
            dgMigrCard.Columns["contact_id"].Visible = false;
            dgMigrCard.Columns["document_id"].Visible = false;
            dgMigrCard.Columns["ser"].HeaderText = "Серия";
            dgMigrCard.Columns["num"].HeaderText = "Номер";
            dgMigrCard.Columns["kpp_code"].HeaderText = "КПП";
            dgMigrCard.Columns["entry_dt"].HeaderText = "Дата въезда";
            dgMigrCard.Columns["status"].HeaderText = "Статус";
            dgMigrCard.Columns["tenure_from_dt"].HeaderText = "Срок пребывания с";
            dgMigrCard.Columns["tenure_to_dt"].HeaderText = "Срок пребывания до";
            dgMigrCard.Columns["purpose_entry"].HeaderText = "Цель въезда";
            //dgMigrCard.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            // dgMigrCard.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgDoc.Columns["id"].Visible = false;
            dgDoc.Columns["contact_id"].Visible = false;
            dgDoc.Columns["code"].Visible = false;
            dgDoc.Columns["type"].HeaderText = "Вид";
            dgDoc.Columns["ser"].HeaderText = "Серия";
            dgDoc.Columns["num"].HeaderText = "Номер";
            dgDoc.Columns["ident"].HeaderText = "Идентификатор";
            dgDoc.Columns["invite_num"].HeaderText = "Номер приглашения";
            dgDoc.Columns["issue_dt"].HeaderText = "Дата выдачи";
            dgDoc.Columns["validity_from_dt"].HeaderText = "Срок действия с";
            dgDoc.Columns["validity_to_dt"].HeaderText = "Срок действия по";
            dgDoc.Columns["status"].HeaderText = "Статус";

            dgDoc.Focus();
            
        }

        private void dgDoc_Click(object sender, EventArgs e)
        {
            if (dgDoc.Rows.Count != 0)
            {

                string flt;
                if (!checkBox1.Checked)
                {
                    flt = "document_id =" + dgDoc.CurrentRow.Cells["code"].Value + "";
                }
                else
                    flt = "";
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.DataSource = dgMigrCard.DataSource;
                bindingSource1.Filter = flt;
                dgMigrCard.DataSource = bindingSource1;
            }

        }

       
    }
}
