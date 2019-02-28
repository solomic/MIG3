using Npgsql;
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
    public partial class fHost : Form
    {
        public fHost()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            fHostAddEdit fHostAddEditForm = new fHostAddEdit("Add", 0);
            if (fHostAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                fHost_Load(this, null);
            }

        }

        private void dgPf_DoubleClick(object sender, EventArgs e)
        {
            if (dgHost.SelectedCells.Count == 0)
            {
                return;
            }
            fHostAddEdit fHostAddEditForm = new fHostAddEdit("Edit",Convert.ToInt32( dgHost.CurrentRow.Cells["id"].Value));
            if(fHostAddEditForm.ShowDialog(this)==DialogResult.OK)
            {
                fHost_Load(this, null);
            }

        }

        private void fHost_Load(object sender, EventArgs e)
        {
            dgHost.DataSource = DB.QueryTableMultipleParams("SELECT case when status='Y' then 'Активно' else null end \"Статус\",last_name \"Фамилия\", first_name \"Имя\", second_name \"Отчество\", doc \"ДУЛ\", doc_num \"Номер\", date_issue \"Дата выдачи\", " +
                  " date_valid \"Срок действия\", obl \"Область\", rayon \"Район\", town \"Город\", street \"Улица\", house \"Дом\", korp \"Корпус\", stro \"Строение\", flat \"Квартира\", " +
                   " phone \"Телефон\", org_name \"Организация\", address \"Адрес\", org_phis \"Тип\", inn \"ИНН\", doc_ser \"Серия\", birthday \"Дата рождения\",id " +
                " FROM cmodb.host", null);
            dgHost.Columns["id"].Visible = false;
           
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (dgHost.SelectedRows.Count == 0)
            {
                return;
            }
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {                
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                cmd.Parameters.Clear();
                sql = "UPDATE cmodb.host  SET status = 'N'; ";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.host  SET status = 'Y'  WHERE id =@id; ";
                cmd.Parameters.AddWithValue("id", Convert.ToInt32(dgHost.CurrentRow.Cells["id"].Value));
              
                cmd.CommandText = sql;                

                cmd.ExecuteNonQuery();
                transaction.Commit();
                fHost_Load(this, null);

                MessageBox.Show("Запись активирована", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
              

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(msg.Message);
                MessageBox.Show("Ошибка при активации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HostDelete();
        }

        private void HostDelete()
        {
            if (dgHost.SelectedRows.Count != 1)
            {
                return;
            }
            if (MessageBox.Show("Подтвердите удаление выбранной записи", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn);
                cmd.Parameters.Clear();
                sql = "DELETE FROM cmodb.host  WHERE id=@id; ";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("id", Convert.ToInt32(dgHost.CurrentRow.Cells["id"].Value));
                cmd.ExecuteNonQuery();
                transaction.Commit();
                fHost_Load(this, null);

                MessageBox.Show("Запись успешно удалена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(msg.Message);
                MessageBox.Show("Ошибка при удалении", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
