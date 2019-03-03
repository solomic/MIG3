using Npgsql;
using Pref;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Mig
{
    public partial class fHostAddEdit : Form
    {
        string Action = "";
        int Id=0;

        public fHostAddEdit()
        {
            InitializeComponent();
        }
        public fHostAddEdit(string method,int _id)
        {
            Action = method;
            Id = _id;
            InitializeComponent();
        }

        private void fHostAddEdit_Load(object sender, EventArgs e)
        {
            if (Action == "Edit" )
            {

                DataTable HostActiveTable = DB.QueryTableMultipleParams(pref.GetHostActiveSql, new List<object> { Id });
                tLastName.Text= HostActiveTable.Rows[0]["last_name"].ToString();
                tFirstName.Text = HostActiveTable.Rows[0]["first_name"].ToString();
                tMidName.Text = HostActiveTable.Rows[0]["second_name"].ToString();
                cmbDUL.Text = HostActiveTable.Rows[0]["doc"].ToString();
                tNum.Text = HostActiveTable.Rows[0]["doc_num"].ToString();
                tSer.Text = HostActiveTable.Rows[0]["doc_ser"].ToString();
                dIssue.SelectedDate = HostActiveTable.Rows[0]["date_issue"] == DBNull.Value ? null : Convert.ToDateTime(HostActiveTable.Rows[0]["date_issue"]).ToString("dd.MM.yyyy");
                dValidated.SelectedDate = HostActiveTable.Rows[0]["date_valid"] == DBNull.Value ? null : Convert.ToDateTime(HostActiveTable.Rows[0]["date_valid"]).ToString("dd.MM.yyyy");
                dBirthday.SelectedDate = HostActiveTable.Rows[0]["birthday"] == DBNull.Value ? null : Convert.ToDateTime(HostActiveTable.Rows[0]["birthday"]).ToString("dd.MM.yyyy");
                tObl.Text = HostActiveTable.Rows[0]["obl"].ToString();
                tRayon.Text = HostActiveTable.Rows[0]["rayon"].ToString();
                tTown.Text = HostActiveTable.Rows[0]["town"].ToString();
                tStreet.Text = HostActiveTable.Rows[0]["street"].ToString();
                tHouse.Text = HostActiveTable.Rows[0]["house"].ToString();
                tCorp.Text = HostActiveTable.Rows[0]["korp"].ToString();
                tStroenie.Text = HostActiveTable.Rows[0]["stro"].ToString();
                tFlat.Text = HostActiveTable.Rows[0]["flat"].ToString();
                tOrgPhone.Text = HostActiveTable.Rows[0]["phone"].ToString();
                tOrgName.Text = HostActiveTable.Rows[0]["org_name"].ToString();
                tOrgAddress.Text = HostActiveTable.Rows[0]["address"].ToString();
                cmbType.Text = HostActiveTable.Rows[0]["org_phis"].ToString();
                tOrgINN.Text = HostActiveTable.Rows[0]["inn"].ToString();
               
                
                /*
                tMigEntryDt.SelectedDate = MigrCardActiveTable.Rows[0]["entry_dt"] == DBNull.Value ? null : Convert.ToDateTime(MigrCardActiveTable.Rows[0]["entry_dt"]).ToString("dd.MM.yyyy");
                tMigTenureFrom.SelectedDate = MigrCardActiveTable.Rows[0]["tenure_from_dt"] == DBNull.Value ? null : Convert.ToDateTime(MigrCardActiveTable.Rows[0]["tenure_from_dt"]).ToString("dd.MM.yyyy");
                tMigrSer.Text = MigrCardActiveTable.Rows[0]["ser"].ToString();
                tMigrNum.Text = MigrCardActiveTable.Rows[0]["num"].ToString();
                cmbKPP.Text = MigrCardActiveTable.Rows[0]["kpp_code"].ToString();
                tMigrPurpose.Text = MigrCardActiveTable.Rows[0]["purpose_entry"].ToString();

                */
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
              //  int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                cmd.Parameters.Clear();
                if (Action == "Add")
                {
                    sql = "INSERT INTO cmodb.host(last_name, first_name, second_name, doc, doc_num, date_issue, "+
                        " date_valid, obl, rayon, town, street, house, korp, stro, flat, "+
                       " phone, org_name, address, org_phis, inn, doc_ser, birthday, status) "+
                       "  VALUES(@last_name, @first_name, @second_name, @doc, @doc_num, @date_issue, "+
                       " @date_valid, @obl, @rayon, @town, @street, @house, @korp, @stro, @flat, "+
                       " @phone, @org_name, @address, @org_phis, @inn, @doc_ser, @birthday, @status); ";
                    cmd.Parameters.AddWithValue("status", "N");  /*+-*/
                }
                else
                {
                    sql = "UPDATE cmodb.host "+
                       " SET last_name =@last_name, first_name =@first_name, second_name =@second_name, doc =@doc, doc_num =@doc_num, date_issue =@date_issue,  " +
                           " date_valid =@date_valid, obl =@obl, rayon =@rayon, town =@town, street =@street, house =@house, korp =@korp,  " +
                           " stro =@stro, flat =@flat, phone =@phone, org_name =@org_name, address =@address, org_phis =@org_phis, inn =@inn,  " +
                           " doc_ser =@doc_ser,birthday =@birthday,status = status " +
                            "  WHERE id =@id; ";
                    cmd.Parameters.AddWithValue("id", Id); /*+-*/
                }
                cmd.CommandText = sql;
                
                
                cmd.Parameters.AddWithValue("last_name", tLastName.Text);
                cmd.Parameters.AddWithValue("first_name", tFirstName.Text);
                cmd.Parameters.AddWithValue("second_name", tMidName.Text);
                cmd.Parameters.AddWithValue("doc", cmbDUL.Text);
                cmd.Parameters.AddWithValue("doc_num", tNum.Text);
                cmd.Parameters.AddWithValue("doc_ser", tSer.Text);
                if (dIssue.SelectedDate == "")
                    cmd.Parameters.AddWithValue("date_issue", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("date_issue", Convert.ToDateTime(dIssue.SelectedDate));
                if (dValidated.SelectedDate == "")
                    cmd.Parameters.AddWithValue("date_valid", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("date_valid", Convert.ToDateTime(dValidated.SelectedDate));
                cmd.Parameters.AddWithValue("obl", tObl.Text);
                cmd.Parameters.AddWithValue("rayon", tRayon.Text);
                cmd.Parameters.AddWithValue("town", tTown.Text);
                cmd.Parameters.AddWithValue("street", tStreet.Text);
                cmd.Parameters.AddWithValue("house", tHouse.Text);
                cmd.Parameters.AddWithValue("korp", tCorp.Text);
                cmd.Parameters.AddWithValue("stro", tStroenie.Text);
                cmd.Parameters.AddWithValue("flat", tFlat.Text);

                cmd.Parameters.AddWithValue("phone", tOrgPhone.Text);
                cmd.Parameters.AddWithValue("org_name", tOrgName.Text);
                cmd.Parameters.AddWithValue("address", tOrgAddress.Text);
                cmd.Parameters.AddWithValue("org_phis", cmbType.Text);
                cmd.Parameters.AddWithValue("inn", tOrgINN.Text);
                if (dBirthday.SelectedDate == "")
                    cmd.Parameters.AddWithValue("birthday", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birthday", Convert.ToDateTime(dBirthday.SelectedDate));               

                

                cmd.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("Добавлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(msg.Message);
                MessageBox.Show("Ошибка при добавлении", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
