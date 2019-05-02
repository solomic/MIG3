using Pref;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mig
{
    public partial class InvForm : Form
    {
        string ClassName = "InvForm";
        public InvForm()
        {
            InitializeComponent();
            SetDoubleBuffered(InvFilterGrid, true);
        }

        private void InvFilterChange()
        {
            try
            {
                if (cmbInvFilter.Text == "")
                    return;
                //if (!ColumnOrderFlt.ContainsKey(cmbFilter.Text))
                //{
                //    MessageBox.Show("Колонки фильтра не определены!\n Редактировать-Колонки фильтров...", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                InvFilterGrid.AllowUserToResizeColumns = false;
                ssStatus.Text = "Загрузка данных...";

                string OldFilter = bindingSource1.Filter;
                bindingSource1.Filter = "";

                // lFilter.ResetText();

                String sql_text = DB.GetTableValue("select [Qr] from [Inventation].[FILTER] where [Name]=@param1", new List<object> { cmbInvFilter.Text });

                if (sql_text == "")
                {
                    throw new Exception("Фильтр не найден!");
                }

                bindingSource1.DataSource = DB.QueryTableMultipleParams(sql_text, null);
                if (InvFilterGrid.DataSource == null)
                    InvFilterGrid.DataSource = bindingSource1;
                //если Фильтр остался прежний, то и подфильтры сохраняем 
                if (pref.INVFLTNAME != "<>" && pref.INVFLTNAME == cmbInvFilter.Text)
                {
                    bindingSource1.Filter = OldFilter;
                }
                else
                    pref.INVFLTNAME = cmbInvFilter.Text;
                stCnt.Text = "Количество записей: " + bindingSource1.Count;

                /*применяем фильтр*/
                InvFilterTextChange();
                // SetColumnOrder();

                if (InvFilterGrid.Columns.Contains("id"))
                    InvFilterGrid.Columns["id"].Visible = false;
                //InvFilterGrid.Columns["warning"].HeaderText = "Предупреждения";
                //InvFilterGrid.Columns["warning"].DisplayIndex = 0;
                //InvFilterGrid.Columns["contact_id"].Visible = false;
                //InvFilterGrid.Columns["graduate"].Visible = false;
                //InvFilterGrid.Columns["deduct"].Visible = false;
                //InvFilterGrid.Columns["reg_extend"].Visible = false;
                //InvFilterGrid.Columns["doc"].Visible = false;

                //InvFilterGrid.Columns["dng"].Visible = false;
                //InvFilterGrid.Columns["dngmes"].Visible = false;
                //InvFilterGrid.Columns["rs_ten"].Visible = false;
                //InvFilterGrid.Columns["rs_ent"].Visible = false;
                //if (InvFilterGrid.Columns.Contains("pass_expire"))
                //    InvFilterGrid.Columns["pass_expire"].Visible = false;
                //if (InvFilterGrid.Columns.Contains("med_to_calc"))
                //    InvFilterGrid.Columns["med_to_calc"].Visible = false;
                ssStatus.Text = "Данные успешно загружены";
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:FilterChange\n Error:" + err);
                throw new Exception(err.Message);
            }
            finally
            {
                InvFilterGrid.AllowUserToResizeColumns = true;
            }
        }

        private void InvFilterTextChange()
        {
            try
            {
                // if (textBox1.Text.Trim() != "")
                string str = InvFilterGrid.FilterString == "" ? "" : " AND " + InvFilterGrid.FilterString;
                bindingSource1.Filter = "[Фамилия] LIKE '" + comboBox1.Text.Trim() + "%' " + str;

            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:textBox1_TextChanged\n Error:" + err);
                //сбрасываем фильтр по колонкам
                InvFilterGrid.ClearFilter();
                bindingSource1.Filter = "[Фамилия] LIKE '" + comboBox1.Text.Trim() + "%' ";
            }
        }

        private void cmbFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            /*выбрали другой фильтр*/
            try
            {
                InvFilterChange();
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:cmbFilter_SelectedValueChanged\n Error:" + err);
                MessageBox.Show("Ошибка фильтра:\n\n" + err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void InvForm_Shown(object sender, EventArgs e)
        {

        }

        private void InvForm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbInvFilter.Items.Clear();
                this.cmbInvFilter.SelectedValueChanged -= new System.EventHandler(this.cmbFilter_SelectedValueChanged);
                cmbInvFilter.DataSource = DB.QueryTableMultipleParams("select [Name] from [Inventation].[FILTER]", null);
                cmbInvFilter.SelectedIndex = -1;
                this.cmbInvFilter.SelectedValueChanged += new System.EventHandler(this.cmbFilter_SelectedValueChanged);
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:InvForm_Load\n Error:" + err);
                throw new Exception("Ошибка при загрузке фильтров:\n\n" + err.Message);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            InvFilterTextChange();
        }
        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PopupMenu2.Show(btnAdd, new Point(0, 20));
        }

        public int[] getInvSelectedRowsId(DataGridView gv)
        {
            int[] iId = { 0 };
            try
            {

                int[] AllCells = new int[gv.SelectedCells.Count];

                for (int i = 0; i < gv.SelectedCells.Count; i++)
                {
                    AllCells[i] = gv.SelectedCells[i].RowIndex;
                }
                int[] DistRows = new int[AllCells.Distinct<int>().Count()];
                int a = 0;
                foreach (var m in AllCells.Distinct<int>())
                {
                    DistRows[a++] = m;
                }
                iId = new int[DistRows.Count()];
                if (DistRows.Count() > 0)
                {

                    for (int i = 0; i < DistRows.Count(); i++)
                    {
                        iId[i] = Convert.ToInt32(gv.Rows[DistRows[i]].Cells["id"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:getInvSelectedRowsId\n Error:" + ex);
                throw new Exception("Ошибка при получении id:\n\n" + ex.Message);
            }

            return iId;
        }

        private void SetInvContactType(string type, int[] ids)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                sql = "UPDATE [Inventation].[Inv] SET Status=@param1 " +
                   "  WHERE [Id] IN (" + String.Join(",", ids) + "); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("param1", type);
                cmd.ExecuteNonQuery();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName + "Function:SetInvContactType\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }

        }

        private void setStatus(object sender, EventArgs e)
        {
            try
            {
                int[] SelRows = getInvSelectedRowsId(InvFilterGrid);

                if (SelRows.Count() != 0)
                {
                    SetInvContactType((sender as Button).Text, SelRows);
                    InvFilterChange();
                    MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:miGraduate_Click\n Error:" + ex);
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GridColor()
        {
            //InvFilterGrid.Rows[i].DefaultCellStyle.BackColor = Color.Red;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (InvFilterGrid.SelectedCells.Count != 1)
                {
                    return;
                }
                
               // pref.CURROW = dataGridView1.CurrentRow.Index;
                if (MessageBox.Show("Точно УДАЛИТЬ данную запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    InvDelete(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                    InvFilterChange();
                    MessageBox.Show("Успешно удалено", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: \n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvDelete(int id)
        {
          
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql = "";
            try
            {
                int cid = DB.GetTableValueInt("SELECT [Contact Id] FROM [Inventation].[Inv] WHERE [Id]=@param1", new List<object> { id });

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);               
                sql = "DELETE FROM [Inventation].[Inv] where [Id]=@Id";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sql, DB.conn, transaction);
                sql = "DELETE FROM [Inventation].[Contact] where [Id]=@Id";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("id", cid);
                cmd.ExecuteNonQuery();

                transaction.Commit();               

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка: \n" + msg.Message);
                throw msg;
            }
        }

        private void PopupMenu2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void InvFilterGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if(e.RowIndex < 0 || cmbInvFilter.Text == "")
            {
                return;
            }
            pref.ROWACTION = "EDIT";
            pref.INV_ID = Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value);
            InvEdit fInvEdit = new InvEdit();

            if (fInvEdit.ShowDialog() == DialogResult.OK)
                InvFilterChange();
        }

        

        private void pmAdd_Click(object sender, EventArgs e)
        {          
            pref.ROWACTION = "ADD";
            InvEdit fInvEdit = new InvEdit();
            if (fInvEdit.ShowDialog() == DialogResult.OK)
                InvFilterChange();

        }

        private void pmCopy_Click(object sender, EventArgs e)
        {
            if (InvFilterGrid.SelectedCells.Count == 0)
            {
                return;
            }
            pref.ROWACTION = "COPY";
            InvEdit fInvEdit = new InvEdit();            
            pref.INV_ID = Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value);
            if (fInvEdit.ShowDialog() == DialogResult.OK)
                InvFilterChange();

        }

        private void InvFilterGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0 || cmbInvFilter.Text == "")
            {
                return;
            }
            pref.INV_ID = Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value);
        }

        private void InvFilterGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string recstate = InvFilterGrid.Rows[e.RowIndex].Cells["Статус"].Value.ToString();
            if (recstate == "Приехал")
                InvFilterGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(pref.INVSTATUSARRIVED);
            if (recstate == "Не приехал(отказ)")
                InvFilterGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(pref.INVSTATUSREJECTION);
            if ((DateTime.Today.AddDays(pref.INVSTATUSSTAYBYINT) >= Convert.ToDateTime(InvFilterGrid.Rows[e.RowIndex].Cells["Пребывание по"].Value)) && (recstate == "Оформление в ФМС" || recstate == "Получено из ФМС" || recstate == "Выдано/Отправлено"))
            {
                InvFilterGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(pref.INVSTATUSSTAYBY);
            }
            
        }

        private void InvDoWord_Click(object sender, EventArgs e)
        {
            try
            {
                string doc = (new pf()).DoWord(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {
                    Process.Start(doc);
                }
            }           
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:"+ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvDocFld_Click(object sender, EventArgs e)
        {
            try { 
                string doc = (new pf()).DoWord(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {                
                    Process.Start(Path.GetDirectoryName(doc));
                }
            }           
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:"+ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvDoWordLetter_Click(object sender, EventArgs e)
        {
            try
            {
                string doc = (new pf()).DoWordLetter(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {
                    Process.Start(doc);
                }
            }                       
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:"+ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvDoWordLetterFld_Click(object sender, EventArgs e)
        {
            try
            {
                string doc = (new pf()).DoWordLetter(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {
                    Process.Start(Path.GetDirectoryName(doc));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoWordConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string doc = (new pf()).DoWordConfirm(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {
                    Process.Start(doc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoWordConfirmFld_Click(object sender, EventArgs e)
        {
            try
            {
                string doc = (new pf()).DoWordConfirm(Convert.ToInt32(InvFilterGrid.CurrentRow.Cells["Id"].Value));
                if (doc != "")
                {
                    Process.Start(Path.GetDirectoryName(doc));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SharedFld_Click(object sender, EventArgs e)
        {
            try
            {
                
                 Process.Start(Path.GetDirectoryName(pref.INVREPORTFOLDER));
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
