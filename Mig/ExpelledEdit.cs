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
    public partial class fExpelledEdit : Form
    {
        string Action = "";
        int id = 0;
        public fExpelledEdit()
        {
            InitializeComponent();
        }
        public fExpelledEdit(string Act,int Id)
        {
            Action = Act;
            id = Id;
            InitializeComponent();
        }

        private void fExpelledEdit_Load(object sender, EventArgs e)
        {
            if (Action == "EDIT")
            {
                DataTable ExpellActiveTable = DB.QueryTableMultipleParams(pref.GetExpellActiveSql, new List<object> {id });
                /*приказ об отчислении*/
                if (ExpellActiveTable.Rows.Count != 0)
                {
                    tExpelled.Text = ExpellActiveTable.Rows[0]["expelled"].ToString();
                    tExpelledNum.Text = ExpellActiveTable.Rows[0]["expelled_num"].ToString();
                    tExpelledDt.SelectedDate = ExpellActiveTable.Rows[0]["expelled_dt"] == DBNull.Value ? null : Convert.ToDateTime(ExpellActiveTable.Rows[0]["expelled_dt"]).ToString("dd.MM.yyyy");

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
                int Contact_id = pref.CONTACTID;
                cmd = new SqlCommand(sql, DB.conn);
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = transaction;
                if (Action == "ADD")
                {
                    sql = "UPDATE cmodb.expell SET status='N' where  contact_id=@contact_id;";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
                if (Action == "ADD")
                {
                    sql = "INSERT INTO cmodb.expell(contact_id, expelled, expelled_num, expelled_dt, status,updated,updated_by) VALUES (@contact_id, @expelled, @expelled_num, @expelled_dt, @status, GETDATE(),CURRENT_USER);";
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("status", "Y");
                }
                else
                {
                    sql = "UPDATE cmodb.expell SET expelled=@expelled, expelled_num=@expelled_num, expelled_dt=@expelled_dt,updated=GETDATE(),updated_by=CURRENT_USER where id=@id;";
                    cmd.Parameters.AddWithValue("id", id);

                }
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("expelled", tExpelled.Text);
                cmd.Parameters.AddWithValue("expelled_num", tExpelledNum.Text);             
                
                if (tExpelledDt.SelectedDate == "")
                {
                    cmd.Parameters.AddWithValue("expelled_dt", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("expelled_dt", Convert.ToDateTime(tExpelledDt.SelectedDate));
                }
                cmd.ExecuteNonQuery();

                /*если дата заполнена, то обновим активное обучение*/
                if (tExpelledDt.SelectedDate != "")
                {
                    sql = "UPDATE cmodb.teach_info set deduct_year=:year where contact_id=:contact_id and status='Y';";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("year", tExpelledDt.SelectedDate.Substring(6));
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();


                MessageBox.Show("Обновлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
              
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка: \n" + msg.Message);
                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
