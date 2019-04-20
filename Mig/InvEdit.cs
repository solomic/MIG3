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
            CheckDul();
            LoadAllCombo();
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

        public void LoadAllCombo()
        {
            try
            {
                LoadValueCombobox(ComboBox5, "[Inventation].[Contact]", "Nationality");
                LoadValueCombobox(LabeledEdit9, "[Inventation].[Inv]", "Visit Points");
                LoadValueCombobox(LabeledEdit5, "[Inventation].[Contact]", "Birth Country");
                LoadValueCombobox(LabeledEdit6, "[Inventation].[Contact]", "Birth Country Real");
                LoadValueCombobox(ComboBox6, "[Inventation].[Contact]", "Country Get Visa");
                LoadValueCombobox(ComboBox7, "[Inventation].[Contact]", "Town Get Visa");
                LoadValueCombobox(LabeledEdit4, "[Inventation].[Contact]", "Country Live");
                LoadValueCombobox(LabeledEdit7, "[Inventation].[Contact]", "Country Region");
                LoadValueCombobox(LabeledEdit15, "[Inventation].[Contact]", "Address Alleged");
                LoadValueCombobox(ComboBox9, "[Inventation].[Contact]", "Spec");
                LoadValueCombobox(ComboBox10, "[Inventation].[Inv]", "Host Name");
                LoadValueCombobox(ComboBox11, "[Inventation].[Inv]", "Host Phone");
            }
            catch(Exception err)
            {
                MessageBox.Show("Ошибка: \n" + err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
        private void BitBtn1_Click(object sender, EventArgs e)
        {
            string msg = "";
            string d;
            int l, i, con_id;
            string resname;
            ////проверка на 18 мес
            //try
            //{
            //    if (!string.IsNullOrEmpty(DBDateTimeEditEh3.SelectedDate) )
            //    {
            //        lValid.Visible = Convert.ToDateTime( DBDateTimeEditEh7.SelectedDate).AddMonths(-18) < Convert.ToDateTime(DBDateTimeEditEh3.SelectedDate);
            //        if (lValid.Visible)
            //            MessageBox.Show("Срок действия паспорта меньше установленного срока!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }

            //}
            //catch (Exception err) { }   
            CheckDul();
            if (lValid.Visible)
                 MessageBox.Show("Срок действия паспорта меньше установленного срока!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (string.IsNullOrEmpty(DBDateTimeEditEh1.SelectedDate ))
                msg += ("  От" + Environment.NewLine);
            if (string.IsNullOrEmpty(DBDateTimeEditEh2.SelectedDate))
                msg+= ("  Желательно оформить до" + Environment.NewLine);
            if (ComboBox9.Text == "" )
                msg+= ("  Специальность" + Environment.NewLine);
            if (ComboBox10.Text == "" )
                msg+= ("  Приглашающая сторона" + Environment.NewLine);
            if (ComboBox11.Text == "" )
                msg+= ("  Телефон приглаш. стороны" + Environment.NewLine);

            if (ComboBox1.Text == "")
                msg+= ("  Цель" + Environment.NewLine);
            if (LabeledEdit8.Text == "")
                msg+= ("  Срок" + Environment.NewLine);
            if (string.IsNullOrEmpty(DBDateTimeEditEh3.SelectedDate))
                msg += ("  Въезд" + Environment.NewLine);
            if (string.IsNullOrEmpty(DBDateTimeEditEh4.SelectedDate)) msg += ("  Пребывание до" + Environment.NewLine);
            if (ComboBox2.Text == "") msg += ("  Кратность визы" + Environment.NewLine);
            if (ComboBox3.Text == "") msg += ("  Вид визы" + Environment.NewLine);
            if (LabeledEdit9.Text == "") msg += ("  Пункты посещения" + Environment.NewLine);

            if (LabeledEdit1.Text == "") msg += ("  Фамилия" + Environment.NewLine);
            if (LabeledEdit2.Text == "") msg += ("  Имя" + Environment.NewLine);
            if (Edit1.Text == "") msg += ("  Фамилия enu" + Environment.NewLine);
            if (Edit2.Text == "") msg += ("  Имя enu" + Environment.NewLine);
            if (string.IsNullOrEmpty(DBDateTimeEditEh5.SelectedDate )) msg += ("  Дата рождения" + Environment.NewLine);
            if (ComboBox4.Text == "") msg += ("  Пол" + Environment.NewLine);            
            if (LabeledEdit14.Text == "") msg += ("  Номер" + Environment.NewLine);
            if (string.IsNullOrEmpty(DBDateTimeEditEh6.SelectedDate)) msg += ("  дата выдачи" + Environment.NewLine);          
            if (ComboBox5.Text == "") msg += ("  Гражданство" + Environment.NewLine);
            if (LabeledEdit5.Text == "") msg += ("  Государство рождения" + Environment.NewLine);
            if (LabeledEdit6.Text == "") msg += ("  место" + Environment.NewLine);
            if (LabeledEdit4.Text == "") msg += ("  государство п.п." + Environment.NewLine);
            if (LabeledEdit7.Text == "") msg += ("  регион" + Environment.NewLine);
            if (ComboBox6.Text == "") msg += ("  Место получения визы:страна" + Environment.NewLine);
            if (ComboBox7.Text == "") msg += ("  город" + Environment.NewLine);
            if (LabeledEdit15.Text == "") msg += ("  Адрес предполагаемого места пребывания" + Environment.NewLine);
            if (msg !="")
            {
                MessageBox.Show("Заполните поля:\n"+msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ContactId = DB.GetTableValueInt("SELECT NEXT VALUE FOR  [Inventation].InvConId", null);
            int InventationId = DB.GetTableValueInt("SELECT NEXT VALUE FOR  [Inventation].InvId", null);

            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
            cmd = new SqlCommand(sql, DB.conn, transaction);
            try
            {
                    if (pref.ROWACTION == "ADD" || pref.ROWACTION == "COPY")
                    {                
                                   
                        sql = "INSERT INTO [Inventation].[Contact]([Id], " +
                            "[Last Name], [First Name], [Last Name Enu], [First Name Enu]," +
                            "[Birthday], [Sex], [Nationality], [Birth Country], [Birth Country Real]," +
                            "[Second Name], [Country Live], [Country Region], [Country Get Visa]," +
                            "[Town Get Visa], [Work], [Work Address], [Work Pos], [Ser], [Num]," +
                            "[Date Issue], [Tenure], [Address Alleged], [Form Study], [Spec])" +
                            "VALUES (@Id, @LastName, @FirstName, @LastNameEnu, @FirstNameEnu," +
                            "@Birthday, @Sex, @Nationality, @BirthCountry, @BirthCountryReal," +
                            "@SecondName, @CountryLive, @CountryRegion, @CountryGetVisa, " +
                            "@TownGetVisa, @Work, @WorkAddress, @WorkPos, @Ser, @Num, "+
                            "@DateIssue, @Tenure, @AddressAlleged, @FormStudy, @Spec); ";
                                               
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Id", ContactId);
                        cmd.Parameters.AddWithValue("LastName", LabeledEdit1.Text);
                        cmd.Parameters.AddWithValue("FirstName", LabeledEdit2.Text);
                        cmd.Parameters.AddWithValue("LastNameEnu", Edit1.Text);
                        cmd.Parameters.AddWithValue("FirstNameEnu", Edit2.Text);
                        cmd.Parameters.AddWithValue("Birthday", DBDateTimeEditEh5.SelectedDate);
                        cmd.Parameters.AddWithValue("Sex", ComboBox4.Text);
                        cmd.Parameters.AddWithValue("Nationality", ComboBox5.Text);
                        cmd.Parameters.AddWithValue("BirthCountry", LabeledEdit5.Text);
                        cmd.Parameters.AddWithValue("BirthCountryReal", LabeledEdit6.Text);
                        cmd.Parameters.AddWithValue("SecondName", LabeledEdit3.Text);
                        cmd.Parameters.AddWithValue("CountryLive", LabeledEdit4.Text);
                        cmd.Parameters.AddWithValue("CountryRegion", LabeledEdit7.Text);
                        cmd.Parameters.AddWithValue("CountryGetVisa", ComboBox6.Text);
                        cmd.Parameters.AddWithValue("TownGetVisa", ComboBox7.Text);
                        cmd.Parameters.AddWithValue("Work", LabeledEdit10.Text);
                        cmd.Parameters.AddWithValue("WorkAddress", LabeledEdit11.Text);
                        cmd.Parameters.AddWithValue("WorkPos", LabeledEdit12.Text);
                        cmd.Parameters.AddWithValue("Ser", LabeledEdit13.Text);
                        cmd.Parameters.AddWithValue("Num", LabeledEdit14.Text);
                        cmd.Parameters.AddWithValue("DateIssue", DBDateTimeEditEh6.SelectedDate);
                        cmd.Parameters.AddWithValue("Tenure", DBDateTimeEditEh7.SelectedDate);
                        cmd.Parameters.AddWithValue("AddressAlleged", LabeledEdit15.Text);
                        cmd.Parameters.AddWithValue("FormStudy", ComboBox8.Text);
                        cmd.Parameters.AddWithValue("Spec", ComboBox9.Text);
                        cmd.ExecuteNonQuery();

                    
                        sql = "INSERT INTO [Inventation].[Inv]([Id]," +
                              "[Formalize Dt], [Entity], [Tenure], [Estimated Entry]," +
                              "[Stay Dt], [Number Entries], [Visa Type], [Visit Points]," +
                              "[Create Dt]," +
                              "[Contact Id],[Status],[Host Name], [Host Phone], [Doc View],[Fact Entry],[Comment])" +
                              "VALUES (@Id,@FormalizeDt, @Entity, @Tenure, @EstimatedEntry," +
                              "@StayDt, @NumberEntries, @VisaType, @VisitPoints," +
                              "@CreateDt," +
                              "@ContactId, @Status, @HostName, @HostPhone, @DocView,@FactEntry,@Comment)";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("Id", InventationId);
                        cmd.Parameters.AddWithValue("FormalizeDt",DBDateTimeEditEh2.SelectedDate);
                        cmd.Parameters.AddWithValue("Entity", ComboBox1.Text);
                        cmd.Parameters.AddWithValue("Tenure", LabeledEdit8.Text);
                        cmd.Parameters.AddWithValue("EstimatedEntry", DBDateTimeEditEh3.SelectedDate);
                        cmd.Parameters.AddWithValue("FactEntry", Edit3.Text);
                        cmd.Parameters.AddWithValue("StayDt", DBDateTimeEditEh4.SelectedDate);
                        cmd.Parameters.AddWithValue("NumberEntries", ComboBox2.Text);
                        cmd.Parameters.AddWithValue("VisaType", ComboBox3.Text);
                        cmd.Parameters.AddWithValue("VisitPoints", LabeledEdit9.Text);
                        cmd.Parameters.AddWithValue("CreateDt", DBDateTimeEditEh1.SelectedDate);
                        cmd.Parameters.AddWithValue("ContactId", ContactId);
                        cmd.Parameters.AddWithValue("Status", "Выполнение");
                        cmd.Parameters.AddWithValue("HostName", ComboBox10.Text);
                        cmd.Parameters.AddWithValue("HostPhone", ComboBox11.Text);
                        cmd.Parameters.AddWithValue("DocView", ComboBox12.Text);
                        cmd.Parameters.AddWithValue("Comment", Memo1.Text);

                        pref.INV_ID = InventationId;
                
                    }
                    else //редактировать
                    {
                   
                        sql = "UPDATE [Inventation].[Contact] " +
                          "set [Last Name]=@LastName, [First Name]=@FirstName, [Last Name Enu]=@LastNameEnu, [First Name Enu]=@FirstNameEnu," +
                          "[Birthday]=@Birthday, [Sex]=@Sex, [Nationality]=@Nationality, [Birth Country]=@BirthCountry, [Birth Country Real]=@BirthCountryReal," +
                          "[Second Name] =@SecondName, [Country Live]=@CountryLive, [Country Region]=@CountryRegion, [Country Get Visa]=@CountryGetVisa," +
                          "[Town Get Visa] =@TownGetVisa, [Work]=@Work, [Work Address]=@WorkAddress, [Work Pos]=@WorkPos, [Ser]=@Ser, [Num]=@Num," +
                          "[Date Issue] =@DateIssue, [Tenure]=@Tenure, [Address Alleged]=@AddressAlleged, [Form Study]=@FormStudy ," +
                          " [Updated By] = suser_name(), [Updated] =getdate(), [Spec]=@Spec where [Id]=@Id;";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("LastName", LabeledEdit1.Text);
                        cmd.Parameters.AddWithValue("FirstName", LabeledEdit2.Text);
                        cmd.Parameters.AddWithValue("LastNameEnu", Edit1.Text);
                        cmd.Parameters.AddWithValue("FirstNameEnu", Edit2.Text);
                        cmd.Parameters.AddWithValue("Birthday", DBDateTimeEditEh5.SelectedDate);
                        cmd.Parameters.AddWithValue("Sex", ComboBox4.Text);
                        cmd.Parameters.AddWithValue("Nationality", ComboBox5.Text);
                        cmd.Parameters.AddWithValue("BirthCountry", LabeledEdit5.Text);
                        cmd.Parameters.AddWithValue("BirthCountryReal", LabeledEdit6.Text);
                        cmd.Parameters.AddWithValue("SecondName", LabeledEdit3.Text);
                        cmd.Parameters.AddWithValue("CountryLive", LabeledEdit4.Text);
                        cmd.Parameters.AddWithValue("CountryRegion", LabeledEdit7.Text);
                        cmd.Parameters.AddWithValue("CountryGetVisa", ComboBox6.Text);
                        cmd.Parameters.AddWithValue("TownGetVisa", ComboBox7.Text);
                        cmd.Parameters.AddWithValue("Work", LabeledEdit10.Text);
                        cmd.Parameters.AddWithValue("WorkAddress", LabeledEdit11.Text);
                        cmd.Parameters.AddWithValue("WorkPos", LabeledEdit12.Text);
                        cmd.Parameters.AddWithValue("Ser", LabeledEdit13.Text);
                        cmd.Parameters.AddWithValue("Num", LabeledEdit14.Text);
                        cmd.Parameters.AddWithValue("DateIssue", DBDateTimeEditEh6.SelectedDate);
                        cmd.Parameters.AddWithValue("Tenure", DBDateTimeEditEh7.SelectedDate);
                        cmd.Parameters.AddWithValue("AddressAlleged", LabeledEdit15.Text);
                        cmd.Parameters.AddWithValue("FormStudy", ComboBox8.Text);
                        cmd.Parameters.AddWithValue("Spec", ComboBox9.Text);
                        cmd.Parameters.AddWithValue("Id",pref.INV_CONTACT_EDIT);
                        cmd.ExecuteNonQuery();

                        sql = "UPDATE [Inventation].[Inv] " +
                          "set [Formalize Dt]=@FormalizeDt, [Entity]=@Entity, [Tenure]=@Tenure, [Estimated Entry]=@EstimatedEntry," +
                          "[Stay Dt]=@StayDt, [Number Entries]=@NumberEntries, [Visa Type]=@VisaType, [Visit Points]=@VisitPoints," +
                          "[Create Dt]=@CreateDt, [Updated By]=suser_name(), [Updated]=getdate(), [Host Name]=@HostName, [Host Phone]=@HostPhone, [Doc View]=@DocView, [Fact Entry]=@FactEntry, [Comment]=@Comment WHERE [Id]=@Id";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("FormalizeDt", DBDateTimeEditEh2.SelectedDate);
                        cmd.Parameters.AddWithValue("Entity", ComboBox1.Text);
                        cmd.Parameters.AddWithValue("Tenure", LabeledEdit8.Text);
                        cmd.Parameters.AddWithValue("EstimatedEntry", DBDateTimeEditEh3.SelectedDate);
                        cmd.Parameters.AddWithValue("FactEntry", Edit3.Text);
                        cmd.Parameters.AddWithValue("StayDt", DBDateTimeEditEh4.SelectedDate);
                        cmd.Parameters.AddWithValue("NumberEntries", ComboBox2.Text);
                        cmd.Parameters.AddWithValue("VisaType", ComboBox3.Text);
                        cmd.Parameters.AddWithValue("VisitPoints", LabeledEdit9.Text);
                        cmd.Parameters.AddWithValue("CreateDt", DBDateTimeEditEh1.SelectedDate);
                        cmd.Parameters.AddWithValue("ContactId", ContactId);
                        cmd.Parameters.AddWithValue("Status", "Выполнение");
                        cmd.Parameters.AddWithValue("HostName", ComboBox10.Text);
                        cmd.Parameters.AddWithValue("HostPhone", ComboBox11.Text);
                        cmd.Parameters.AddWithValue("DocView", ComboBox12.Text);
                        cmd.Parameters.AddWithValue("Comment", Memo1.Text);
                        cmd.Parameters.AddWithValue("Id", pref.INV_ID);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    MessageBox.Show(pref.ROWACTION == "EDIT" ? "Обновлено успешно" : "Добавлено успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    

            }
            catch (Exception msgerr)
            {
                if (transaction != null) transaction.Rollback();
                MessageBox.Show("Ошибка: \n" + msgerr.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pref.INV_CONTACT_EDIT = 0;
            //LoadAllCombo();
            this.DialogResult = DialogResult.OK;
         
        }

        private void DBDateTimeEditEh3_ValueChanged(object sender, EventArgs e)
        {
            TermChange();

            CheckDul();
        }

        private void TermChange()
        {
            try
            {
                if (!string.IsNullOrEmpty(DBDateTimeEditEh3.SelectedDate) && LabeledEdit8.Text != "")
                    DBDateTimeEditEh4.SelectedDate = Convert.ToDateTime(DBDateTimeEditEh3.SelectedDate).AddDays(Convert.ToInt32(LabeledEdit8.Text) - 1).ToString();


            }
            catch (Exception err) { }
        }

        private void DBDateTimeEditEh7_ValueChanged(object sender, EventArgs e)
        {
            CheckDul();
        }

        private void CheckDul()
        {
            //проверка на 18 мес (pref.INVCHECKDUL)
            try
            {
                if (!string.IsNullOrEmpty(DBDateTimeEditEh7.SelectedDate) && !string.IsNullOrEmpty(DBDateTimeEditEh3.SelectedDate))
                    lValid.Visible = Convert.ToDateTime(DBDateTimeEditEh7.SelectedDate).AddMonths(-pref.INVCHECKDUL) < Convert.ToDateTime(DBDateTimeEditEh3.SelectedDate);
                else
                    lValid.Visible = false;
            }
            catch (Exception err) { }
        }

        private void LabeledEdit8_TextChanged(object sender, EventArgs e)
        {
            TermChange();
        }
        private void LoadValueCombobox(ComboBox cmb, string tbl, string  fldname)
        {
            try
            {
                cmb.DataSource = DB.QueryTableMultipleParams("SELECT DISTINCT [" + fldname + "] FROM " + tbl + " ORDER BY 1 ASC", null);
                cmb.ValueMember = fldname;   
            }
            catch(Exception err)
            {
            }
        }

    }
}
