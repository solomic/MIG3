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
    public partial class fDocAddEdit : Form
    {
        public string ClassName = "Class: fDocAddEdit.cs\n";

        string Action = "";
        public fDocAddEdit()
        {
            InitializeComponent();
        }
        public fDocAddEdit(string method)
        {
            Action = method;
            InitializeComponent();
        }

        private void fDocMigrAdd_Load(object sender, EventArgs e)
        {
            try {
                /*тип документа*/
                cmbDocType.DataSource = DB.QueryTableMultipleParams("SELECT code, value FROM cmodb.lov where type=@param1 order by ord;", new List<object> { "MIGR.VIEW" });
                cmbDocType.SelectedIndex = -1;

                if (Action == "Edit")
                {
                    /*виза\доки*/
                    DataTable DocActiveTable = DB.QueryTableMultipleParams(pref.GetDocActiveSql, new List<object> { pref.CONTACTID });
                    if (DocActiveTable.Rows.Count != 0)
                    {
                        cmbDocType.Text = DocActiveTable.Rows[0]["type"] == DBNull.Value ? "" : DocActiveTable.Rows[0]["type"].ToString();
                        tIdent.Text = DocActiveTable.Rows[0]["ident"].ToString();
                        tInvite.Text = DocActiveTable.Rows[0]["invite_num"].ToString();
                        tDocSer.Text = DocActiveTable.Rows[0]["ser"].ToString();
                        tDocNum.Text = DocActiveTable.Rows[0]["num"].ToString();
                        tDocIssue.SelectedDate = DocActiveTable.Rows[0]["issue_dt"] == DBNull.Value ? null : Convert.ToDateTime(DocActiveTable.Rows[0]["issue_dt"]).ToString("dd.MM.yyyy");
                        tDocValidFrom.SelectedDate = DocActiveTable.Rows[0]["validity_from_dt"] == DBNull.Value ? null : Convert.ToDateTime(DocActiveTable.Rows[0]["validity_from_dt"]).ToString("dd.MM.yyyy");
                        tDocValidTo.SelectedDate = DocActiveTable.Rows[0]["validity_to_dt"] == DBNull.Value ? null : Convert.ToDateTime(DocActiveTable.Rows[0]["validity_to_dt"]).ToString("dd.MM.yyyy");
                    }

                }
            }
            catch (Exception msg)
            {             
                Logger.Log.Error(ClassName + "Function:fDocMigrAdd_Load\n Error:" + msg.Message);
                MessageBox.Show( msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!tDocIssue.ValidateDate() || !tDocValidFrom.ValidateDate() || !tDocValidTo.ValidateDate())
                return;

            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql="";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                if (Action == "Add")
                {
                    sql = "UPDATE cmodb.document SET status='N',updated=now(),updated_by=CURRENT_USER where contact_id=:contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();
                }
                if (Action == "Add")
                {
                    sql = "INSERT INTO cmodb.document( " +
                    " contact_id, ident, type, invite_num, ser, num, issue_dt,  " +
                    " validity_from_dt, validity_to_dt, status,code,updated,updated_by) " +
                     " VALUES(:contact_id, :ident, :type, :invite_num, :ser, :num, :issue_dt, " +
                    " :validity_from_dt, :validity_to_dt, :status, (select MAX(code)+1 from cmodb.document),now(),CURRENT_USER); ";
                }
                else
                {
                    sql = "UPDATE cmodb.document SET " +
                    "  ident=:ident, type=:type, invite_num=:invite_num, ser=:ser, num=:num, issue_dt=:issue_dt,  " +
                    " validity_from_dt=:validity_from_dt, validity_to_dt=:validity_to_dt,updated=now(),updated_by=CURRENT_USER " +                     
                    " WHERE contact_id=:contact_id and status=:status; ";

                }
                cmd.CommandText = sql;                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("ident", tIdent.Text);
                cmd.Parameters.AddWithValue("type", cmbDocType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("invite_num", tInvite.Text);
                cmd.Parameters.AddWithValue("ser", tDocSer.Text);
                cmd.Parameters.AddWithValue("num", tDocNum.Text);
                if (tDocIssue.SelectedDate == "")
                    cmd.Parameters.AddWithValue("issue_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("issue_dt", Convert.ToDateTime(tDocIssue.SelectedDate));
                if (tDocValidFrom.SelectedDate == "")
                    cmd.Parameters.AddWithValue("validity_from_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("validity_from_dt", Convert.ToDateTime(tDocValidFrom.SelectedDate));
                if (tDocValidTo.SelectedDate == "")
                    cmd.Parameters.AddWithValue("validity_to_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("validity_to_dt", Convert.ToDateTime(tDocValidTo.SelectedDate));
                cmd.Parameters.AddWithValue("status", "Y");                
                cmd.ExecuteNonQuery();
                           
                transaction.Commit();

                MessageBox.Show("Добавлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
               
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName + "Function:btnSave_Click\n Error:" + msg.Message);
                MessageBox.Show("Ошибка при добавлении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
