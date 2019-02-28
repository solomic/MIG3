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
using Npgsql;
using System.Data.SqlClient;

namespace Mig
{
    public partial class fTeachAddEdit : Form
    {
        string Action = "";
        int TeachId = 0;
        public fTeachAddEdit()
        {
            InitializeComponent();
        }
        public fTeachAddEdit(string method,int id)
        {
            Action = method;
            TeachId = id;
            InitializeComponent();
        }

        private void fTeachAddEdit_Load(object sender, EventArgs e)
        {

            ///*Образование*/
            cmbFac.SelectedValueChanged -= cmbFac_SelectedValueChanged;

            cmbFac.DataSource = DB.QueryTableMultipleParams(pref.FAC, null);
            cmbFac.SelectedIndex = -1;
            cmbFO.DataSource = DB.QueryTableMultipleParams(pref.FO, null);
            cmbFO.SelectedIndex = -1;
            cmbFin.DataSource = DB.QueryTableMultipleParams(pref.FIN, null);
            cmbFin.SelectedIndex = -1;
            cmbPO.DataSource = DB.QueryTableMultipleParams(pref.PO, null);
            cmbPO.SelectedIndex = -1;
            cmbFac.SelectedValueChanged += cmbFac_SelectedValueChanged;
            if (Action == "Edit")
            {
                /*Образование*/
                DataTable TeachActiveTable = DB.QueryTableMultipleParams(pref.GetTeachActiveSql, new List<object> { TeachId });

                if (TeachActiveTable.Rows.Count != 0)
                {
                    tYear.Text = TeachActiveTable.Rows[0]["postup_year"].ToString();
                    cmbPO.Text = TeachActiveTable.Rows[0]["prog_teach"].ToString();
                    cmbFac.Text = TeachActiveTable.Rows[0]["fac_name"].ToString();
                    cmbSpec.Text = TeachActiveTable.Rows[0]["spec_name"].ToString();                    
                   cmbFO.Text = TeachActiveTable.Rows[0]["form_teach"].ToString();
                   cmbFin.Text = TeachActiveTable.Rows[0]["form_pay"].ToString();                   
                   tTeachTotal.Text = TeachActiveTable.Rows[0]["period_total"].ToString();
                   tTeachInd.Text = TeachActiveTable.Rows[0]["period_ind"].ToString();
                   cmbTotalSrok.Text = TeachActiveTable.Rows[0]["period_total_p"].ToString();
                   cmbIndSrok.Text = TeachActiveTable.Rows[0]["period_ind_p"].ToString();
                   tSpecCode.Text = TeachActiveTable.Rows[0]["code"].ToString();
                   tYearAmount.Text = TeachActiveTable.Rows[0]["amount"].ToString();


                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql="";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                if (Action == "Add")
                { 
                    sql = "UPDATE cmodb.teach_info set status='N' where contact_id=@contact_id and status='Y'";                    
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                    cmd.ExecuteNonQuery();
                
                    sql = "INSERT INTO cmodb.teach_info( " +
                           " postup_year, contact_id, status, spec_code, form_teach_code,  " +
                           "  form_pay_code, prog_teach_code, period_total, period_ind, period_total_p,  " +
                           "   period_ind_p, facult_code, amount) " +
                           "   VALUES(@postup_year, @contact_id, @status, @spec_code, @form_teach_code, " +
                           "  @form_pay_code, @prog_teach_code, @period_total, @period_ind, @period_total_p, " +
                           "  @period_ind_p, @facult_code, @amount);";
                
                }
                if (Action == "Edit")
                {
                    sql = "UPDATE cmodb.teach_info SET " +
                      " postup_year=@postup_year, spec_code=@spec_code, form_teach_code=@form_teach_code,  " +
                      "  form_pay_code=@form_pay_code, prog_teach_code=@prog_teach_code, period_total=@period_total, period_ind=@period_ind, period_total_p=@period_total_p,  " +
                      "   period_ind_p=@period_ind_p, facult_code=@facult_code, amount=@amount " +
                      "  WHERE contact_id=@contact_id and status=@status;";
                }
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.Parameters.AddWithValue("postup_year", tYear.Text);
                cmd.Parameters.AddWithValue("status", "Y");
                if (cmbSpec.Text != "")
                    cmd.Parameters.AddWithValue("spec_code", Convert.ToInt32(cmbSpec.SelectedValue.ToString()));
                else
                    cmd.Parameters.AddWithValue("spec_code", DBNull.Value);
                cmd.Parameters.AddWithValue("form_teach_code", cmbFO.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("form_pay_code", cmbFin.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("prog_teach_code", cmbPO.SelectedValue.ToString());
                if (tTeachTotal.Text != "")
                {
                    cmd.Parameters.AddWithValue("period_total", Convert.ToInt16(tTeachTotal.Text));
                    cmd.Parameters.AddWithValue("period_total_p", cmbTotalSrok.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("period_total", DBNull.Value);
                    cmd.Parameters.AddWithValue("period_total_p", DBNull.Value);
                }
                if (tTeachInd.Text != "")
                {
                    cmd.Parameters.AddWithValue("period_ind", Convert.ToInt16(tTeachInd.Text));
                    cmd.Parameters.AddWithValue("period_ind_p", cmbIndSrok.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("period_ind", DBNull.Value);
                    cmd.Parameters.AddWithValue("period_ind_p", DBNull.Value);
                }
                
               
                cmd.Parameters.AddWithValue("facult_code", Convert.ToInt16(cmbFac.SelectedValue));
                if(tYearAmount.Text!="")
                    cmd.Parameters.AddWithValue("amount", Convert.ToDecimal(tYearAmount.Text));
                else
                    cmd.Parameters.AddWithValue("amount", DBNull.Value);

                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при добавлении : \n" + msg.Message);
                MessageBox.Show("Ошибка при добавлении : \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFac_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string POCode;
                if (cmbPO.Text != "")
                {
                    POCode = DB.GetTableValue("select code from cmodb.lov where type='PTEACH' AND value = @param1;", new List<object> { cmbPO.Text });
                    cmbSpec.DataSource = DB.QueryTableMultipleParams(pref.SPECLOAD, new List<object> { Convert.ToInt16(cmbFac.SelectedValue), POCode });
                }
                else
                    cmbSpec.DataSource = DB.QueryTableMultipleParams(pref.SPECLOAD, new List<object> { Convert.ToInt16(cmbFac.SelectedValue), DBNull.Value });


                cmbSpec.SelectedIndex = -1;
            }
            catch (Exception er)
            {
                MessageBox.Show("Ошибка при выборе факультета: \n" + er.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSpec_SelectedValueChanged(object sender, EventArgs e)
        {
            tSpecCode.Text = cmbSpec.SelectedValue == null ? "" : DB.GetTableValue("select code from cmodb.speciality where spec_code=@param1;",new List<object> {Convert.ToInt32( cmbSpec.SelectedValue) }) ;
        }

        private void cmbPO_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbFac_SelectedValueChanged(this, null);
        }
    }
}
