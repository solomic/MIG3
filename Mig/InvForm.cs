using Pref;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
