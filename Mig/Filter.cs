using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Reflection;
using Pref;
using NpgsqlTypes;
using System.Configuration;
using System.Diagnostics;
using pk = DocumentFormat.OpenXml.Packaging;
using wp = DocumentFormat.OpenXml.Wordprocessing;

namespace Mig
{
    public partial class fFilter : Form
    {
        public string ClassName = "Class: Filter.cs\n";
        internal sealed class gfDataGridViewSetting : ApplicationSettingsBase
        {
            private static gfDataGridViewSetting _defaultInstace =
                (gfDataGridViewSetting)ApplicationSettingsBase
                .Synchronized(new gfDataGridViewSetting());
            //---------------------------------------------------------------------
            public static gfDataGridViewSetting Default
            {
                get { return _defaultInstace; }
            }
            //---------------------------------------------------------------------
            // Because there can be more than one DGV in the user-application
            // a dictionary is used to save the settings for this DGV.
            // As key the name of the control is used.
            [UserScopedSetting]
            [SettingsSerializeAs(SettingsSerializeAs.Binary)]
            [DefaultSettingValue("")]
            public Dictionary<string, List<ColumnOrderItem>> ColumnOrder
            {
                get { return this["ColumnOrder"] as Dictionary<string, List<ColumnOrderItem>>; }
                set { this["ColumnOrder"] = value; }
            }
        }
        //-------------------------------------------------------------------------
        [Serializable]
        public sealed class ColumnOrderItem
        {
            public int DisplayIndex { get; set; }
            public int Width { get; set; }
            public bool Visible { get; set; }
            public int ColumnIndex { get; set; }
            public String ColumnName { get; set; }
        }

       

        private void SetColumnOrder()
        {

            try
            {
                if (!gfDataGridViewSetting.Default.ColumnOrder.ContainsKey(pref.USER + " " + cmbFilter.Text))
                    return;


                List<ColumnOrderItem> columnOrder =
                    gfDataGridViewSetting.Default.ColumnOrder[pref.USER + " " + cmbFilter.Text];

                if (columnOrder != null)
                {
                    var sorted = columnOrder.OrderBy(i => i.DisplayIndex);
                    foreach (var item in sorted)
                    {
                        dataGridView1.Columns[item.ColumnName].DisplayIndex = item.DisplayIndex;
                        dataGridView1.Columns[item.ColumnName].Width = item.Width;
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:SetColumnOrder\n Error:" + err);
                throw new Exception("Ошибка при сортировке колонок:\n\n"+err.Message);
            }

        }
        void DirectMenu(bool Key)
        {
            button1.Enabled = Key;
            btnSpr.Enabled = Key;
            textBox1.Enabled = Key;
            cmbFilter.Enabled = Key;
            tpBackup.Enabled = Key;
            tpSync.Enabled = Key;
            btnColDef.Enabled = Key;
            //  btnConnectDB.Enabled = !Key;

        }
        private void SaveColumnOrder()
        {
            try
            {
                if (dataGridView1.AllowUserToResizeColumns)
                {
                    List<ColumnOrderItem> columnOrder = new List<ColumnOrderItem>();
                    DataGridViewColumnCollection columns = dataGridView1.Columns;
                    for (int i = 0; i < columns.Count; i++)
                    {
                        columnOrder.Add(new ColumnOrderItem
                        {
                            ColumnIndex = i,
                            DisplayIndex = columns[i].DisplayIndex,
                            Visible = columns[i].Visible,
                            Width = columns[i].Width,
                            ColumnName = columns[i].Name
                        });
                    }

                    gfDataGridViewSetting.Default.ColumnOrder[pref.USER + " " + cmbFilter.Text] = columnOrder;
                    gfDataGridViewSetting.Default.Save();


                }
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:SaveColumnOrder\n Error:" + ex);
                throw new Exception("Ошибка сохранения положения колонок:\n\n"+ ex.Message);
            }
        }

        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }
        public fFilter()
        {
            InitializeComponent();
            SetDoubleBuffered(dataGridView1, true);
        }

       

        private void fMigr_Load(object sender, EventArgs e)
        {            
                DirectMenu(false);           
        }

        private void FilterLoad()
        {
            try
            {
                cmbFilter.Items.Clear();
                foreach (DataRow row in DB.QueryTableMultipleParams("SELECT filtername FROM cmodb.filters order by filter_order ASC;", null).Rows)
                    cmbFilter.Items.Add(row.ItemArray[0].ToString());
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:fMigr_Load\n Error:" + err);
                throw new Exception("Ошибка при загрузке фильтров:\n\n"+err.Message);                
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            /*выбрали другой фильтр*/  
            try
            {
                FilterChange();
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:comboBox1_SelectedValueChanged\n Error:" + err);
                MessageBox.Show("Ошибка фильтра:\n\n"+err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void FilterChange()
        {
            try
            {
                tsFilterLoad.Text = "Загрузка данных...";

                bindingSource1.Filter = "";
                dataGridView1.DataSource = null;
                lFilter.ResetText();

                int filter_code = DB.GetTableValueInt("SELECT code FROM cmodb.filters where filtername=:param1;", new List<object> { cmbFilter.Text });
                String sql_text = DB.GetTableValue("SELECT filter_expr FROM cmodb.user_filter where filter_id=:param1 AND user_name=:param2;", new List<object> { filter_code, pref.USER });

                bindingSource1.DataSource = DB.QueryTableMultipleParams(sql_text, null);
                dataGridView1.DataSource = bindingSource1;
                stCnt.Text = "Количество записей: " + bindingSource1.Count;

                /*применяем фильтр*/                
                FilterTextChange();

                //  dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns["warning"].HeaderText = "Предупреждения";
                dataGridView1.Columns["warning"].DisplayIndex = 0;
                dataGridView1.Columns["contact_id"].Visible = false;
                dataGridView1.Columns["graduate"].Visible = false;
                dataGridView1.Columns["deduct"].Visible = false;
                dataGridView1.Columns["reg_extend"].Visible = false;
                dataGridView1.Columns["doc"].Visible = false;

                dataGridView1.Columns["dng"].Visible = false;
                dataGridView1.Columns["dngmes"].Visible = false;
                dataGridView1.Columns["rs_ten"].Visible = false;
                dataGridView1.Columns["rs_ent"].Visible = false;
                if (dataGridView1.Columns.Contains("pass_expire"))
                    dataGridView1.Columns["pass_expire"].Visible = false;

                SetColumnOrder();
                tsFilterLoad.Text = "Данные успешно загружены";
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:FilterChange\n Error:" + err);
                throw new Exception("Ошибка фильтра:\n\n" + err.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterTextChange();
        }

        private void FilterTextChange()
        {
            try
            {
                // if (textBox1.Text.Trim() != "")
                string str = dataGridView1.FilterString == "" ? "" : " AND " + dataGridView1.FilterString;
                bindingSource1.Filter = "[Фамилия] LIKE '" + textBox1.Text.Trim() + "%' "+ str;                        
                  
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:textBox1_TextChanged\n Error:" + err);
                //сбрасываем фильтр по колонкам
                dataGridView1.ClearFilter();
                bindingSource1.Filter = "[Фамилия] LIKE '" + textBox1.Text.Trim() + "%' ";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBox1.Text != "")
                advancedDataGridView1_CellDoubleClick(this, null);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
                toolTip1.Show("Переключите раскладку клавиатуры", textBox1);
            }
        }




        public int[] getSelectedRowsId(DataGridView gv)
        {
            int[] iId = {0};
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
                        iId[i] = Convert.ToInt32(gv.Rows[DistRows[i]].Cells["contact_id"].Value);
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:getSelectedRowsId\n Error:" + ex);
                throw new Exception("Ошибка при получении id:\n\n" + ex.Message);
            }

            return iId;
        }

        private void miGraduate_Click(object sender, EventArgs e)
        {            
            try
            {
                int[] SelRows = getSelectedRowsId(dataGridView1);

                if (SelRows.Count() != 0)
                {
                    SetContactType("graduate", SelRows);
                    FilterChange();
                    MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
            }

            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:miGraduate_Click\n Error:" + ex);
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetContactType(string type, int[] ids)
        {           
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.contact SET " +
                   " type=:type,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id = ANY(:contact_id); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);                            
                cmd.Parameters.AddWithValue("type", type);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();               
                Logger.Log.Error(ClassName + "Function:SetContactType\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
           
        }
        private void SetRegExtend(int type, int[] ids)
        {           
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.contact SET " +
                   " reg_extend=:reg_extend,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id = ANY(:contact_id); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);                           
                cmd.Parameters.AddWithValue("reg_extend", type);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();               
                Logger.Log.Error(ClassName + "Function:SetRegExtend\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
           
        }

        private void SetDeduct(string type, int[] ids)
        {            
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.contact SET " +
                   " deduct=:type,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id = ANY(:contact_id); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);
                cmd.Parameters.AddWithValue("type", type);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();               
                Logger.Log.Error(ClassName + "Function:SetDeduct\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
           
        }
        private void SetRF(string type, int[] ids)
        {            
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.contact SET " +
                   " rf=:type,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id = ANY(:contact_id); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);
                cmd.Parameters.AddWithValue("type", type);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();               
                Logger.Log.Error(ClassName + "Function:SetRF\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
           
        }
        private void SetDoc(int type, string dt, int[] ids)
        {
           
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {

                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);
                sql = "UPDATE cmodb.contact SET " +
                   " doc_state=:doc,date_entry_future=:date_entry_future,updated=now(),updated_by=CURRENT_USER " +
                   "  WHERE contact_id = ANY(:contact_id); ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                NpgsqlParameter arpar = new NpgsqlParameter();
                arpar.ParameterName = "contact_id";
                arpar.NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer;
                arpar.Value = ids;
                cmd.Parameters.Add(arpar);
                cmd.Parameters.AddWithValue("doc", type);
                if (dt == "")
                    cmd.Parameters.AddWithValue("date_entry_future", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("date_entry_future", Convert.ToDateTime(dt));
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();               
                Logger.Log.Error(ClassName + "Function:SetDoc\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
            
        }



        private void miContinueTeach_Click(object sender, EventArgs e)
        {
            try
            {
                int[] SelRows = getSelectedRowsId(dataGridView1);

                if (SelRows.Count() != 0)
                {
                    SetContactType("continue_teach", SelRows);
                    FilterChange();
                    MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void miStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int[] SelRows = getSelectedRowsId(dataGridView1);

                if (SelRows.Count() != 0)
                {
                    SetContactType("student", SelRows);
                    FilterChange();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpRegExendSet_Click(object sender, EventArgs e)
        {
            try
            {
                int[] SelRows = getSelectedRowsId(dataGridView1);

                if (SelRows.Count() != 0)
                {
                    SetRegExtend(1, SelRows);
                    FilterChange();
                    MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
            }            
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void tpRegExendClear_Click(object sender, EventArgs e)
        {
            try { 
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {

                        SetRegExtend(0, SelRows);
                        FilterChange();
                        MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpDocDone_Click(object sender, EventArgs e)
        {
            //виза документы сданы
            try { 
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {
                        fDocFms fDocFmsForm = new fDocFms();
                        if (fDocFmsForm.ShowDialog(this) == DialogResult.OK)
                        {
                            if (fDocFmsForm.DocFmsDt == "")
                            {
                                MessageBox.Show("Укажите дату", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            SetDoc(1, fDocFmsForm.DocFmsDt, SelRows);
                            FilterChange();                            
                        }

                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpDocClear_Click(object sender, EventArgs e)
        {
            try { 
                    //виза документы сданы - снять выделение
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {
                        SetDoc(0, "", SelRows);
                        FilterChange();
                        MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpDeductSet_Click(object sender, EventArgs e)
        {
            try { 
                //на отчисление
                int[] SelRows = getSelectedRowsId(dataGridView1);

                if (SelRows.Count() != 0)
                {

                   SetDeduct("1", SelRows);
                    FilterChange();
                   MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpDeductClear_Click(object sender, EventArgs e)
        {
            //снять на отчисление
            try { 
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {
                        SetDeduct("0", SelRows);
                        FilterChange();
                        MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpDelete_Click(object sender, EventArgs e)
        {
            //отчислен
            try { 
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {
                         SetContactType("expelled", SelRows);
                         FilterChange();
                         MessageBox.Show("Успешно отчислен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);    
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpRecover_Click(object sender, EventArgs e)
        {
            //восстановить 
            try
            {
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {

                       SetContactType("student", SelRows);
                       FilterChange();
                       MessageBox.Show("Восстановлен, вперед учиться!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpRF_Click(object sender, EventArgs e)
        {
            //получен паспорт РФ
            try { 
                    int[] SelRows = getSelectedRowsId(dataGridView1);

                    if (SelRows.Count() != 0)
                    {

                        SetRF("1", SelRows);
                        FilterChange();
                        MessageBox.Show("Успешно обновлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка :\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void advancedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }
            pref.CONTACTID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["contact_id"].Value);/*СОХРАНЯЕМ ID ВЫБРАННОГО КОНТАЧА*/
            pref.CURROW = dataGridView1.CurrentRow.Index;

            fContactDetail fContactDetailForm = new fContactDetail();
            fContactDetailForm.ShowDialog(this);
            fContactDetailForm.Dispose();

            /*обновляем фильтр*/

            FilterRefresh();
        }

        private void FilterRefresh()
        {
            FilterChange();
            if (pref.CONTACTID > 0)
            {
                try
                {
                    bindingSource1.Position = pref.CURROW;
                }
                catch (Exception err)
                {
                    Logger.Log.Error(ClassName + "Function:advancedDataGridView1_CellDoubleClick\n Error:" + err.Message);
                }
            }
        }

        private void advancedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
            if (e.ColumnIndex == dataGridView1.Columns["warning"].Index && e.RowIndex != -1)
            {
                string warn = "";
                Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                e.CellBounds.Height - 4);

                using (
                    Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                            e.CellBounds.Top, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom);


                        int dx = 1;
                        string grd = dataGridView1.Rows[e.RowIndex].Cells["graduate"].Value.ToString();
                        string rsten = dataGridView1.Rows[e.RowIndex].Cells["rs_ten"].Value.ToString();
                        //выпускник
                        if (grd == "graduate")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.graduate, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "Выпускник.";
                            dx += 18;
                        }
                        //продолжение обучения
                        if (grd == "continue_teach")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.stairs_up, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "Продолжение обучения.";
                            dx += 18;
                        }
                        if (dataGridView1.Rows[e.RowIndex].Cells["deduct"].Value.ToString() == "1")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.deduct, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "На отчисление.";
                            dx += 18;
                        }
                        //документы сданы
                        if (dataGridView1.Rows[e.RowIndex].Cells["doc"].Value.ToString() == "1")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.doc, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "Документы сданы.";
                            dx += 18;
                        }
                        //если не въехал то костер
                        if (dataGridView1.Rows[e.RowIndex].Cells["rs_ent"].Value.ToString() == "-1")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.fire_red, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "Не выехал!";
                            dx += 18;
                        }
                        //истекла регистрация - красная лампа
                        if (rsten == "-1")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.red_lamp, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "Истекла регистрация!";
                            dx += 18;
                        }
                        //20 дней до истечения регистрации - желтая лампа
                        if (rsten == "-2")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.yellow_lamp, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "20 дней до истечения регистрации!";
                            dx += 18;
                        }
                        //30 дней до истечения регистрации - зеленая лампа
                        if (rsten == "-3")
                        {
                            e.Graphics.DrawImage(Mig.Properties.Resources.green_lamp, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                            warn += "30 дней до истечения регистрации!";
                            dx += 18;
                        }
                        //паспорт менее 18 месяцев
                        if (dataGridView1.Columns.Contains("pass_expire"))
                            {
                            if (dataGridView1.Rows[e.RowIndex].Cells["pass_expire"].Value.ToString() == "1")
                            {
                                e.Graphics.DrawImage(Mig.Properties.Resources.password, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                                warn += "Паспорт дейст. менее 18 месяцев!";
                                dx += 18;
                            }
                        }
                        //продление регистрации
                        try
                        {
                            if (dataGridView1.Rows[e.RowIndex].Cells["reg_extend"].Value.ToString() == "1")
                            {
                                dataGridView1["МК: срок пребывания по", e.RowIndex].Style.BackColor = Color.Silver;
                                dataGridView1.Rows[e.RowIndex].Cells["reg_extend"].ToolTipText= "Продление регистрации.";
                                //e.Graphics.DrawImage(Mig.Properties.Resources.doc, new Rectangle(e.CellBounds.X + dx, e.CellBounds.Y + 1, 16, 16));
                                //dx += 18;

                            }

                        }    
                        catch (Exception err)
                        {
                            Logger.Log.Error(err.ToString());
                        }
                        // e.Graphics.DrawImage(Mig.Properties.Resources.Add_40971, new Rectangle(e.CellBounds.X + 34, e.CellBounds.Y + 1, 16, 16));
                        e.Handled = true;
                        dataGridView1.Rows[e.RowIndex].Cells["warning"].ToolTipText = warn;
                    }
                }

            }
         }

        private void dataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            bindingSource1.Sort = dataGridView1.SortString;
        }

        private void dataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            //if (bindingSource1.Filter != "")
            //    bindingSource1.Filter += (" AND " + dataGridView1.FilterString);
            //else
            //    bindingSource1.Filter = dataGridView1.FilterString;
            FilterTextChange();
        }

        private void bindingSource1_ListChanged(object sender, ListChangedEventArgs e)
        {
            stCnt.Text = string.Format("Количество записей: {0}", this.bindingSource1.List.Count);
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                SaveColumnOrder();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void allViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPrefDB fPrefDBForm = new fPrefDB();
            fPrefDBForm.ShowDialog(this);
        }

        private void tpColumns_Click(object sender, EventArgs e)
        {
            /*редактирование колонок фильтров*/
            fFilterColumnEdit fFilterColumnEditForm = new fFilterColumnEdit();
            fFilterColumnEditForm.ShowDialog(this);
            fFilterColumnEditForm = null;
        }

        private void tpBackup_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Log.Info("Запущен процесс резервного копирования...");
                Process.Start(Application.StartupPath.ToString() + "\\backup.bat");
                Logger.Log.Info("Запущен процесс резервного копирования:Успешно");
            }
            catch (Exception err)
            {
                Logger.Log.Error(ClassName + "Function:fStat_Load\n Error:" + err);
                MessageBox.Show("Ошибка резервного копирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tpInvite_Click(object sender, EventArgs e)
        {
            fHost fHostForm = new fHost();
            fHostForm.ShowDialog(this);
        }

        private void tpSync_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Logger.Log.Info("Запущен процесс загрузки данных...");
            //    DB.GetTableValue("SELECT cmodb.migration();", null);
            //    MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Logger.Log.Info("Запущен процесс загрузки данных:Успешно");
            //}
            //catch (Exception err)
            //{
            //    Logger.Log.Error(ClassName + "Function:tpSync_Click\n Error:" + err);
            //    MessageBox.Show("Ошибка синхронизации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void tpAddress_Click(object sender, EventArgs e)
        {
            fAddressAll fAddressAllForm = new fAddressAll();
            fAddressAllForm.ShowDialog(this);
        }

     

        private void btnConnectDB_Click(object sender, EventArgs e)
        {
            fAuth fAuthForm = new fAuth();
            try
            { 
                if (fAuthForm.ShowDialog(this) == DialogResult.OK)
                {
                    this.Text += " ("+pref.DBNAME+")";
                    DirectMenu(true);
                    FilterLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tpSpec_Click(object sender, EventArgs e)
        {
            fSpecEdit fSpecEditForm = new fSpecEdit();
            fSpecEditForm.ShowDialog(this);
        }

        private void btnColDef_Click(object sender, EventArgs e)
        {
            if (!gfDataGridViewSetting.Default.ColumnOrder.ContainsKey(pref.USER + " " + cmbFilter.Text))
                return;
            gfDataGridViewSetting.Default.ColumnOrder[pref.USER + " " + cmbFilter.Text] = null;
            gfDataGridViewSetting.Default.Save();
            FilterRefresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Добавление*/
            fContactAdd fAddContactForm = new fContactAdd();
            fAddContactForm.ShowDialog(this);
            fAddContactForm = null;
        }

      

        private void btnExit_Click(object sender, EventArgs e)
        {           
            Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void удалениеСтудентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

       
    }
}