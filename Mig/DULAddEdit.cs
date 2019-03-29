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
using System.Data.SqlClient;

namespace Mig
{
    public partial class fContactEdit : Form
    {
        string action = "";
        int id = 0;
        public fContactEdit()
        {
            InitializeComponent();
        }
        public fContactEdit(string Act,int Id)
        {
            action = Act;
            id = Id;
            InitializeComponent();
        }

        

        private void fContactEdit_Load(object sender, EventArgs e)
        {
            tDulType.SelectedIndexChanged -= tDulType_SelectedIndexChanged;
            tDulSer.TextChanged-= tDulSer_TextChanged;
            tDulNum.TextChanged -= tDulNum_TextChanged;

            /*заполняем справочники*/

            /*ДУЛ*/

            tDulType.DataSource = DB.QueryTableMultipleParams("SELECT code, value FROM cmodb.lov where type=@param1 order by ord;", new List<object> { "DUL" });
            tDulType.SelectedIndex = -1;


            /*ПРОГРУЖАЕМ ДАННЫЕ*/

            if (action == "EDIT")
            {
                DataTable DulTable = DB.QueryTableMultipleParams(pref.GetDulIdSql, new List<object> { id });
                if (DulTable.Rows.Count != 0)
                {
                    tDulType.Text = DulTable.Rows[0]["type"].ToString();
                    tDulSer.Text = DulTable.Rows[0]["ser"].ToString();
                    tDulNum.Text = DulTable.Rows[0]["num"].ToString();
                    tDulIssue.SelectedDate = DulTable.Rows[0]["issue"] == DBNull.Value ? null : Convert.ToDateTime(DulTable.Rows[0]["issue"]).ToString("dd.MM.yyyy");
                    tDulValidity.SelectedDate = DulTable.Rows[0]["validity"] == DBNull.Value ? null : Convert.ToDateTime(DulTable.Rows[0]["validity"]).ToString("dd.MM.yyyy");
                }
            }
          
            tDulType.SelectedIndexChanged += this.tDulType_SelectedIndexChanged;
            tDulSer.TextChanged += tDulSer_TextChanged;
            tDulNum.TextChanged += tDulNum_TextChanged;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            DulSave();
        }

        private void DulSave()
        {
            string msgerr = "";
            /* указать тип документа*/
            msgerr = Checks.CheckDULFields("update",tDulType.Text, tDulSer.Text, tDulNum.Text, tDulIssue.SelectedDate, tDulValidity.SelectedDate);
            if (msgerr != "")
            {
                MessageBox.Show(msgerr, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }           
           
         

            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                //cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                if (action == "EDIT")
                {
                    sql = "UPDATE  cmodb.dul SET type=@type,updated=GETDATE(),updated_by=SYSTEM_USER,ser=@ser,num=@num,issue=@issue,validity=@validity where id=@id ;";
                    cmd.Parameters.AddWithValue("id", id);
                }
                if (action == "ADD")
                {
                    sql = "INSERT INTO cmodb.dul(type, ser, num, issue, validity, status, contact_id, updated, updated_by) " +
                    " VALUES(@type, @ser, @num, @issue, @validity, @status, @contact_id, GETDATE(), SYSTEM_USER); ";
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("status", "N");
                }


                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("type", tDulType.SelectedValue);

                cmd.Parameters.AddWithValue("ser", tDulSer.Text.Trim());
                cmd.Parameters.AddWithValue("num", tDulNum.Text.Trim());

                if (tDulIssue.SelectedDate == "")
                    cmd.Parameters.AddWithValue("issue", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("issue", Convert.ToDateTime(tDulIssue.SelectedDate));
                if (tDulValidity.SelectedDate == "")
                    cmd.Parameters.AddWithValue("validity", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("validity", Convert.ToDateTime(tDulValidity.SelectedDate));

                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Обновлен успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();

                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tDulType_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void tDulSer_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void tDulNum_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbNat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);

        }

        private void cmbBirthCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
