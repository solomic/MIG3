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
    public partial class fAgreeAddEdit : Form
    {
        string Action = "";
        int id = 0;
        public fAgreeAddEdit()
        {
            InitializeComponent();
        }
        public fAgreeAddEdit(string method,int id)
        {
            Action = method;
            this.id = id;
            InitializeComponent();
        }
        private void fAgreeAddEdit_Load(object sender, EventArgs e)
        {
            if (Action == "Edit")
            {
                /*Договора*/
                DataTable AgreeActiveTable = DB.QueryTableMultipleParams(pref.GetAgreeActiveSql, new List<object> { id });                
                if (AgreeActiveTable.Rows.Count != 0)
                {
                    tAgree.Text = AgreeActiveTable.Rows[0]["num"].ToString();
                    tpAgreeDt.SelectedDate = AgreeActiveTable.Rows[0]["dt"] == DBNull.Value ? null : Convert.ToDateTime(AgreeActiveTable.Rows[0]["dt"]).ToString("dd.MM.yyyy");
                    tpAgreeFromDt.SelectedDate = AgreeActiveTable.Rows[0]["from_dt"] == DBNull.Value ? null : Convert.ToDateTime(AgreeActiveTable.Rows[0]["from_dt"]).ToString("dd.MM.yyyy");
                    tpAgreeToDt.SelectedDate = AgreeActiveTable.Rows[0]["to_dt"] == DBNull.Value ? null : Convert.ToDateTime(AgreeActiveTable.Rows[0]["to_dt"]).ToString("dd.MM.yyyy");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                if (Action == "Add")
                {
                    sql = "UPDATE cmodb.agree SET status='N' where contact_id=@contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();
                }
                if (Action == "Add")
                {
                    sql = "INSERT INTO cmodb.agree( " +
                    "  contact_id,num, dt, from_dt, to_dt,status ) " +
                     " VALUES(@contact_id, @num, @dt, @from_dt, @to_dt, " +
                    " @status); ";
                }
                else
                {
                    sql = "UPDATE cmodb.agree SET " +
                       "num=@num, dt=@dt, from_dt=@from_dt, to_dt=@to_dt,status=@status  " +                   
                       "  WHERE contact_id=@contact_id and status=@status; ";

                }
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("num", tAgree.Text);
                if (tpAgreeDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("dt", Convert.ToDateTime(tpAgreeDt.SelectedDate));
                if (tpAgreeFromDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("from_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("from_dt", Convert.ToDateTime(tpAgreeFromDt.SelectedDate));
                if (tpAgreeToDt.SelectedDate == "")
                    cmd.Parameters.AddWithValue("to_dt", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("to_dt", Convert.ToDateTime(tpAgreeToDt.SelectedDate));
                cmd.Parameters.AddWithValue("status", "Y");

                cmd.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при добавлении: \n" + msg.Message);
                MessageBox.Show("Ошибка при добавлении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
