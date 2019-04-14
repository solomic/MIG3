using Pref;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mig
{
    public partial class InvEdit : Form
    {
        public InvEdit()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            if (pref.ROWACTION == "EDIT")
            {
                this.Text = "Редактировать";
                BitBtn1.Text = "Сохранить";
                BitBtn2.Enabled = false;

                DataTable InvTable = DB.QueryTableMultipleParams("SELECT * FROM [Inventation].[Inv] WHERE [Id]=@param1", new List<object> { pref.INV_ID });
                
                DBDateTimeEditEh2.SelectedDate = InvTable.Rows[0]["Formalize Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Formalize Dt"]).ToString("dd.MM.yyyy");
                this.ComboBox1.Text = InvTable.Rows[0]["Entity"].ToString();
                this.LabeledEdit8.Text = InvTable.Rows[0]["Tenure"].ToString();
                DBDateTimeEditEh3.SelectedDate = InvTable.Rows[0]["Estimated Entry"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Estimated Entry"]).ToString("dd.MM.yyyy");
                this.Edit3.Text = InvTable.Rows[0]["Fact Entry"].ToString();
                DBDateTimeEditEh4.SelectedDate = InvTable.Rows[0]["Stay Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Stay Dt"]).ToString("dd.MM.yyyy");
                this.ComboBox2.Text = InvTable.Rows[0]["Number Entries"].ToString();
                this.ComboBox3.Text = InvTable.Rows[0]["Visa Type"].ToString();
                this.LabeledEdit9.Text = InvTable.Rows[0]["Visit Points"].ToString();
                DBDateTimeEditEh1.SelectedDate = InvTable.Rows[0]["Create Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Create Dt"]).ToString("dd.MM.yyyy");
                this.ComboBox10.Text = InvTable.Rows[0]["Host Name"].ToString();
                this.ComboBox11.Text = InvTable.Rows[0]["Host Phone"].ToString();
                this.ComboBox12.Text = InvTable.Rows[0]["Doc View"].ToString();
                this.Memo1.Text = InvTable.Rows[0]["Comment"].ToString();

                pref.INV_CONTACT_EDIT =Convert.ToInt32( InvTable.Rows[0]["Contact Id"] );

                DataTable InvConTable = DB.QueryTableMultipleParams("SELECT * FROM [Inventation].[Contact] WHERE [Id]=@param1", new List<object> { pref.INV_CONTACT_EDIT });
                this.LabeledEdit1.Text = InvConTable.Rows[0]["Last Name"].ToString();
                this.LabeledEdit2.Text = InvConTable.Rows[0]["First Name"].ToString();
                this.Edit1.Text = InvConTable.Rows[0]["Last Name Enu"].ToString();
                this.Edit2.Text = InvConTable.Rows[0]["First Name Enu"].ToString();
                DBDateTimeEditEh5.SelectedDate = InvConTable.Rows[0]["Birthday"] == DBNull.Value ? null : Convert.ToDateTime(InvConTable.Rows[0]["Birthday"]).ToString("dd.MM.yyyy");
                this.ComboBox4.Text = InvConTable.Rows[0]["Sex"].ToString();
                this.ComboBox5.Text = InvConTable.Rows[0]["Nationality"].ToString();
                this.LabeledEdit5.Text = InvConTable.Rows[0]["Birth Country"].ToString();
                this.LabeledEdit6.Text = InvConTable.Rows[0]["Birth Country Real"].ToString();
                this.LabeledEdit3.Text = InvConTable.Rows[0]["Second Name"].ToString();
                this.LabeledEdit4.Text = InvConTable.Rows[0]["Country Live"].ToString();
                this.LabeledEdit7.Text = InvConTable.Rows[0]["Country Region"].ToString();
                this.ComboBox6.Text = InvConTable.Rows[0]["Country Get Visa"].ToString();
                this.ComboBox7.Text = InvConTable.Rows[0]["Town Get Visa"].ToString();
                this.LabeledEdit10.Text = InvConTable.Rows[0]["Work"].ToString();
                this.LabeledEdit11.Text = InvConTable.Rows[0]["Work Address"].ToString();
                this.LabeledEdit12.Text = InvConTable.Rows[0]["Work Pos"].ToString();
                this.LabeledEdit13.Text = InvConTable.Rows[0]["Ser"].ToString();
                this.LabeledEdit14.Text = InvConTable.Rows[0]["Num"].ToString();
                DBDateTimeEditEh6.SelectedDate = InvConTable.Rows[0]["Date Issue"] == DBNull.Value ? null : Convert.ToDateTime(InvConTable.Rows[0]["Date Issue"]).ToString("dd.MM.yyyy");
                DBDateTimeEditEh7.SelectedDate = InvConTable.Rows[0]["Tenure"] == DBNull.Value ? null : Convert.ToDateTime(InvConTable.Rows[0]["Tenure"]).ToString("dd.MM.yyyy");
                this.LabeledEdit15.Text = InvConTable.Rows[0]["Address Alleged"].ToString();
                this.ComboBox8.Text = InvConTable.Rows[0]["Form Study"].ToString();
                this.ComboBox9.Text = InvConTable.Rows[0]["Spec"].ToString();
               
            }
            else
            {
                if (pref.ROWACTION == "ADD")
                {
                    this.Text = "Добавить";
                    this.BitBtn1.Text = "Добавить";
                    this.BitBtn2.Enabled = true;
                    this.BitBtn2_Click(this, null);
                }
                if (pref.ROWACTION == "COPY")
                {
                    this.Text = "Добавить";
                    this.BitBtn1.Text = "Добавить";
                    this.BitBtn2.Enabled = true;
                    this.BitBtn2_Click(this, null);

                    DataTable InvTable = DB.QueryTableMultipleParams("SELECT * FROM [Inventation].[Inv] WHERE [Id]=@param1", new List<object> { pref.INV_ID });

                    DBDateTimeEditEh2.SelectedDate = InvTable.Rows[0]["Formalize Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Formalize Dt"]).ToString("dd.MM.yyyy");
                    this.ComboBox1.Text = InvTable.Rows[0]["Entity"].ToString();
                    this.LabeledEdit8.Text = InvTable.Rows[0]["Tenure"].ToString();
                    DBDateTimeEditEh3.SelectedDate = InvTable.Rows[0]["Estimated Entry"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Estimated Entry"]).ToString("dd.MM.yyyy");
                    DBDateTimeEditEh4.SelectedDate = InvTable.Rows[0]["Stay Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Stay Dt"]).ToString("dd.MM.yyyy");
                    this.ComboBox2.Text = InvTable.Rows[0]["Number Entries"].ToString();
                    this.ComboBox3.Text = InvTable.Rows[0]["Visa Type"].ToString();
                    this.LabeledEdit9.Text = InvTable.Rows[0]["Visit Points"].ToString();
                    DBDateTimeEditEh1.SelectedDate = InvTable.Rows[0]["Create Dt"] == DBNull.Value ? null : Convert.ToDateTime(InvTable.Rows[0]["Create Dt"]).ToString("dd.MM.yyyy");
                    this.ComboBox10.Text = InvTable.Rows[0]["Host Name"].ToString();
                    this.ComboBox11.Text = InvTable.Rows[0]["Host Phone"].ToString();
                    this.ComboBox12.Text = InvTable.Rows[0]["Doc View"].ToString();

                    pref.INV_CONTACT_EDIT = Convert.ToInt32(InvTable.Rows[0]["Contact Id"]);

                    DataTable InvConTable = DB.QueryTableMultipleParams("SELECT * FROM [Inventation].[Contact] WHERE [Id]=@param1", new List<object> { pref.INV_CONTACT_EDIT });
                    this.LabeledEdit15.Text = InvConTable.Rows[0]["Address Alleged"].ToString();
                    this.ComboBox8.Text = InvConTable.Rows[0]["Form Study"].ToString();
                    this.ComboBox9.Text = InvConTable.Rows[0]["Spec"].ToString();
                   

                }
            }
        }
      

        private void DBDateTimeEditEh1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (DBDateTimeEditEh1.SelectedDate != null && DBDateTimeEditEh1.SelectedDate !="")
                    DBDateTimeEditEh2.SelectedDate = DB.GetTableValue("SELECT [cmodb].[GetWorkDay] ('" + DBDateTimeEditEh1.SelectedDate + "'," + pref.INVWORKDAY + ")", null);//  Form1.WorkDay(DBDateTimeEditEh1.Value, wdays);
                else
                    DBDateTimeEditEh2.SelectedDate = "";
            }
            catch (Exception msg) { }
        }

        private void BitBtn2_Click(object sender, EventArgs e)
        {
            //очистить
            DBDateTimeEditEh1.Text= "";
            DBDateTimeEditEh2.Text = "";
            DBDateTimeEditEh3.Text = "";
            DBDateTimeEditEh4.Text = "";
            DBDateTimeEditEh5.Text = "";
            DBDateTimeEditEh6.Text = "";
            DBDateTimeEditEh7.Text = "";


            ComboBox1.Text = "";
            ComboBox2.Text = "Однократная";  //кратность визы
            ComboBox3.Text = "Учебная";   //вид визы
            ComboBox4.Text = "";
            ComboBox5.Text= "";
            ComboBox6.Text= "";
            ComboBox7.Text= "";
            ComboBox8.Text= "";
            ComboBox9.Text= "";
            ComboBox10.Text = "";
            ComboBox11.Text= "";
            ComboBox12.Text= "";

            LabeledEdit1.Text= "";
            LabeledEdit2.Text= "";
            LabeledEdit3.Text= "";
            LabeledEdit4.Text= "";
            LabeledEdit5.Text= "";
            LabeledEdit6.Text= "";
            LabeledEdit7.Text = "";
            
            LabeledEdit9.Text = "г. Москва, г. Владимир";
            LabeledEdit10.Text= "";
            LabeledEdit11.Text= "";
            LabeledEdit12.Text= "";
            LabeledEdit13.Text= "";
            LabeledEdit14.Text = "";
            LabeledEdit15.Text = "г. Владимир, ул. Студенческая, д. 10-б";

            Edit1.Text = "";
            Edit2.Text= "";
            Edit3.Text= "";

            Memo1.Clear();
        }
    }
}
