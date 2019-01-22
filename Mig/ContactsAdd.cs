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


namespace Mig
{
    public partial class fContactAdd : Form
    {
        int SelAddr=0;
        
        public fContactAdd()
        {
            InitializeComponent();
        }
     
        private void fContactAdd_Load(object sender, EventArgs e)
        {
          
            try {
                /*заполняем справочники*/

                /*контакт*/

                cmbSex.DataSource = DB.QueryTableMultipleParams(pref.SEX, new List<object> { "SEX" });
                cmbSex.SelectedIndex = -1;

                cmbNat.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
                cmbNat.SelectedIndex = -1;
                cmbBirthCountry.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
                cmbBirthCountry.SelectedIndex = -1;
                // this.tBirthTown.Text = ContactTable.Rows[0]["birth_town"].ToString();

                cmbPosition.DataSource = DB.QueryTableMultipleParams(pref.POS, new List<object> { "POS" });
                cmbPosition.SelectedIndex = -1;
                /*Представитель*/
                cmbDelDul.DataSource = DB.QueryTableMultipleParams(pref.DUL, new List<object> { "DUL" });
                cmbDelDul.SelectedIndex = -1;
                cmbDelCountry.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
                cmbDelCountry.SelectedIndex = -1;
                cmbDelNat.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
                cmbDelNat.SelectedIndex = -1;
                tBirthTown.DataSource = DB.QueryTableMultipleParams(pref.BIRTHTOWN, null);
                tBirthTown.SelectedIndex = -1;
                /*ДУЛ*/

                cmbDulType.DataSource = DB.QueryTableMultipleParams(pref.DUL, new List<object> { "DUL" });
                cmbDulType.SelectedIndex = -1;

                /*тип документа*/
                cmbDocType.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "MIGR.VIEW" });
                cmbDocType.SelectedIndex = -1;
                /*миграционка*/

                cmbKPP.DataSource = DB.QueryTableMultipleParams(pref.KPP, null);
                cmbKPP.SelectedIndex = -1;
               // tMigrPurpose.Text = "УЧЕБА";

                ///*Образование*/
                cmbFac.DataSource = DB.QueryTableMultipleParams(pref.FAC, null);
                cmbFac.SelectedIndex = -1;
                cmbFO.DataSource = DB.QueryTableMultipleParams(pref.FO, null);
                cmbFO.SelectedIndex = -1;
                cmbFin.DataSource = DB.QueryTableMultipleParams(pref.FIN, null);
                cmbFin.SelectedIndex = -1;
                cmbPO.DataSource = DB.QueryTableMultipleParams(pref.PO, null);
                cmbPO.SelectedIndex = -1;
               

            }
            catch(Exception err)
            {
                MessageBox.Show("Ошибка при загрузке карточки студента:\n"+err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            


        }

  

        private void btnDocMigrHist_Click_1(object sender, EventArgs e)
        {
            fDocMigrHist fDocMigrHistForm = new fDocMigrHist();
            fDocMigrHistForm.ShowDialog(this);
            fDocMigrHistForm = null;
        }




        private void button3_Click(object sender, EventArgs e)
        {
            fSelectAddress fSelectAddressForm = new fSelectAddress();
            if( fSelectAddressForm.ShowDialog(this) ==DialogResult.OK)
            {
                SelAddr = fSelectAddressForm.SelectedAddressCode;
                tFullAddress.Text = fSelectAddressForm.SelectedFullAddress;

            }

            fSelectAddressForm = null;

        }


        private void CheckRusChars(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '-' && l !=' ')
            {
                e.Handled = true;               
            }
        }
        private void CheckEnuChars(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'A' || l > 'z') && l != '\b' && l != '-' && l != ' ')
            {
                e.Handled = true;
            }
        }

        string CheckDULFields()
        {
            string Res = "";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (cmbDulType.Text != "" || tDulSer.Text != "" || tDulNum.Text != "" || tDulIssue.SelectedDate != "" ||
                tDulValidity.SelectedDate != "")
            {
                if (cmbDulType.Text.Trim() == "")
                    Res += "Вид документа\n";
               /* if (tDulSer.Text.Trim() == "")
                    Res += "Серия\n";*/
                if (tDulNum.Text.Trim() == "")
                    Res += "Номер\n";
                if (tDulIssue.SelectedDate == "")
                    Res += "Дата выдачи\n";
                if (tDulValidity.SelectedDate == "")
                    Res += "Срок действия\n";
                
                if (Res != "")
                    msg = "Заполните обязательные поля:\n ДУЛ\n \n" + Res;

            }
            else
                msg = "not add";


            return msg;
        }
        string CheckAgreeFields()
        {
            string Res = "";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (tAgree.Text != "" || tpAgreeDt.SelectedDate != "" || tpAgreeFromDt.SelectedDate != "" || tpAgreeToDt.SelectedDate != "" )
            {
                if (tAgree.Text.Trim() == "")
                    Res += "Договор/Направление\n";
                if (tpAgreeDt.SelectedDate == "")
                    Res += "От\n";
                if (tpAgreeFromDt.SelectedDate == "")
                    Res += "С\n";
                if (tpAgreeToDt.SelectedDate == "")
                    Res += "По\n";
                

                if (Res != "")
                    msg = "Заполните обязательные поля:\n ДОГОВОР\n \n" + Res;

            }
            else
                msg = "not add";


            return msg;
        }
        string CheckMigrCardFields()
        {
            string Res = "";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (tMigEntryDt.SelectedDate != "" || tMigTenureFrom.SelectedDate != "" || tMigTenureTo.SelectedDate != "" || tMigrSer.Text != "" ||
                tMigrNum.Text != "" || cmbKPP.Text != "" || tMigrPurpose.Text != "" )
            {
                if (tMigEntryDt.SelectedDate == "")
                    Res += "Дата въезда\n";
                if (tMigTenureTo.SelectedDate == "")
                    Res += "Срок пребывания до\n";
                if (tMigrSer.Text.Trim() == "")
                    Res += "Серия\n";
                if (tMigrNum.Text.Trim() == "")
                    Res += "Номер\n";
                if (cmbKPP.Text.Trim() == "")
                    Res += "КПП\n";
                if (tMigrPurpose.Text.Trim() == "")
                    Res += "Цель въезда\n";

                if (Res != "")
                    msg = "Заполните обязательные поля:\n МИГРАЦИОННАЯ КАРТА\n \n" + Res;

            }
            else
                msg = "not add";


            return msg;
        }
        string CheckTeachFields()
        {
            string Res = "";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (/*cmbFac.Text != "" ||*/ cmbSpec.Text != "" || cmbFO.Text != "" || /*cmbFin.Text != "" ||*/
                cmbPO.Text != "" || tYear.Text != "" || tTeachTotal.Text != "" || tTeachInd.Text != "" || tYearAmount.Text != "")
            {
                //if (cmbFac.Text == "")
                //    Res += "Факультет\n";
                if (cmbFO.Text == "")
                    Res += "Форма обучение\n";
                //if (cmbFin.Text == "")
                //    Res += "Финансирование\n";
                if (cmbPO.Text.Trim() == "")
                    Res += "Программа обучения\n";
                if (tYear.Text.Trim() == "")
                    Res += "Год поступления\n";                

                if (Res != "")
                    msg = "Заполните обязательные поля:\n ОБУЧЕНИЕ\n \n" + Res;

            }
            else
                msg = "not add";


            return msg;
        }
        string CheckContactFields()
        {
            string msg = "";
            string Res = "";
            string Del = "";
            /*Обязательные поля*/
            if (tlastname.Text.Trim() == "")
                Res += "Фамилия\n";
            if (tfirstname.Text.Trim() == "")
                Res += "Имя\n";
            if (dBirthDate.SelectedDate == "")
                Res += "Дата рождения\n";
            if (cmbNat.Text == "")
                Res += "Гражданство\n";
            if (cmbBirthCountry.Text == "")
                Res += "Государство(место рождения)\n";
            if (cmbSex.Text == "")
                Res += "Пол\n";

            /*если начали заполнять представителя, то и продолжаемс*/
            if (tDelLast.Text!="" ||tDelFirst.Text!="" /*|| tDelSec.Text!=""||cmbDelNat.Text!="" ||cmbDelDul.Text!="" ||
                tDelSer.Text!="" ||tDelNum.Text!=""||tDelIssue.SelectedDate!="" ||cmbDelCountry.Text!=""*/)
            {
                if (tDelLast.Text.Trim() == "")
                    Del += "Фамилия\n";
                if (tDelFirst.Text.Trim() == "")
                    Del += "Имя\n";
                //if (cmbDelNat.Text == "")
                //    Del += "Гражданство\n";
                //if (cmbDelDul.Text == "")
                //    Del += "Тип документа\n";
                //if (tDelSer.Text.Trim() == "")
                //    Del += "Серия\n";
                //if (tDelNum.Text.Trim() == "")
                //    Del += "Номер\n";
                //if (tDelIssue.SelectedDate == "")
                //    Del += "Дата выдачи\n";
                //if (cmbDelCountry.Text == "")
                //    Del += "Страна выдачи\n";
            }
            if (Res != "")
                msg = "Заполните обязательные поля:\n\nКОНТАКТ\n \n" + Res;
            if (Del != "")
                msg += "\nПРЕДСТАВИТЕЛЬ\n\n" +Del;
            return msg;
        }
        string CheckDocFields()
        {
            string Res="";
            string msg = "";
            //проверяем что заполнено хотя бы одно поле
            if (cmbDocType.Text != "" || tDocNum.Text != "" || tDocSer.Text != "" || tDocIssue.SelectedDate != "" ||
                tDocValidFrom.SelectedDate != "" || tDocValidTo.SelectedDate != "" || tIdent.Text != "" || tInvite.Text != "")
            {
                if (cmbDocType.Text.Trim() == "")
                    Res += "Тип документа\n";
                if (tDocNum.Text.Trim() == "")
                    Res += "Номер документа\n";
                if (tDocValidFrom.SelectedDate == "")
                    Res += "Срок действия с\n";
                if (tDocValidTo.SelectedDate == "")
                    Res += "Срок действия по\n";

                if (Res != "")
                    msg = "Заполните обязательные поля:\n ДОКУМЕНТ\n \n" + Res;

            }
            else
                msg = "not add";


            return msg;
        }
        private void btnAddContact_Click(object sender, EventArgs e)
        {
            /*добавление нового студента*/
            /*СПГО*/
            /*СТУДЕНТ*/
            string ConValid = CheckContactFields();
            if (ConValid != "")
            {
                MessageBox.Show(ConValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }
            /*ДОКУМЕНТ*/
            string DocValid = CheckDocFields();
            if (DocValid != "" && DocValid != "not add")
            {
                MessageBox.Show(DocValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*МИГРАЦИОНКА*/
            string MigrValid = CheckMigrCardFields();
            if (MigrValid != "" && MigrValid != "not add")
            {
                MessageBox.Show(MigrValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*ДУЛ*/
            // string DULValid = CheckDULFields();
            string DULValid = Checks.CheckDULFields("add", cmbDulType.Text, tDulSer.Text, tDulNum.Text, tDulIssue.SelectedDate, tDulValidity.SelectedDate);
            if (DULValid != "" && DULValid != "not add")
            {
                MessageBox.Show(DULValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*ДОГОВОР*/
            string AgreeValid = CheckAgreeFields();
            if (AgreeValid != "" && AgreeValid != "not add")
            {
                MessageBox.Show(AgreeValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*ОБУЧЕНИЕ*/
            string TeachValid = CheckTeachFields();
            if (TeachValid != "" && TeachValid != "not add")
            {
                MessageBox.Show(TeachValid, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*==============================*/


            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                sql = "select nextval('cmodb.con_id')";
                cmd = new NpgsqlCommand(sql, DB.conn);                
                int Contact_id =  Convert.ToInt32(cmd.ExecuteScalar());
                /*==========================================================*/
                sql = "INSERT INTO cmodb.contact("+
               " last_name, second_name, birthday, birth_town, sex, first_name, comments, "+
              "  last_enu, first_enu, address_home, "+
             "   position_code, relatives, med, second_enu,  "+
             "   phone, nationality, type, contact_id, birth_country, "+
              "  delegate_last_name, delegate_first_name, delegate_second_name, "+
              "  delegate_ser, delegate_num, delegate_dul_issue_dt, delegate_country, "+
              "  delegate_nationality, delegate_dul_code,status)" +
             "  VALUES(:last_name, :second_name, :birthday, :birth_town, :sex, :first_name, :comments, "+
               " :last_enu, :first_enu, :address_home, "+
               " :position_code, :relatives, :med, :second_enu, "+
              "  :phone, :nationality_code, :type, :contact_id, :birth_country_code, " +
               " :delegate_last_name, :delegate_first_name, :delegate_second_name, "+
              "  :delegate_ser, :delegate_num, :delegate_dul_issue_dt, :delegate_country_code, "+
               " :delegate_nationality_code, :delegate_dul_code, :status) ; ";
                cmd = new NpgsqlCommand(sql, DB.conn);
                cmd.Transaction = transaction;
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("last_name", tlastname.Text);
                cmd.Parameters.AddWithValue("second_name", tmidname.Text);
                if (dBirthDate.SelectedDate == "")
                    cmd.Parameters.AddWithValue("birthday", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birthday", Convert.ToDateTime(dBirthDate.SelectedDate));              
                if (tBirthTown.SelectedValue == null)
                    cmd.Parameters.AddWithValue("birth_town", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birth_town", tBirthTown.Text);             
                cmd.Parameters.AddWithValue("sex", cmbSex.Text);
                cmd.Parameters.AddWithValue("first_name", tfirstname.Text);
                cmd.Parameters.AddWithValue("comments", tComments.Text);
                cmd.Parameters.AddWithValue("last_enu", tlastnameenu.Text);
                cmd.Parameters.AddWithValue("first_enu", tfirstnameenu.Text);
                cmd.Parameters.AddWithValue("address_home", tAddressHome.Text);
                if (cmbPosition.SelectedValue == null)
                    cmd.Parameters.AddWithValue("position_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("position_code", cmbPosition.SelectedValue.ToString());                
                cmd.Parameters.AddWithValue("relatives", tRelatives.Text);
                cmd.Parameters.AddWithValue("med", tInsurance.Text);
                cmd.Parameters.AddWithValue("second_enu", tmidnameenu.Text);
                cmd.Parameters.AddWithValue("phone", tPhone.Text);
                if (cmbNat.SelectedValue == null)
                    cmd.Parameters.AddWithValue("nationality_code",DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("nationality_code", cmbNat.Text);
                cmd.Parameters.AddWithValue("type", "student");
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                if (cmbBirthCountry.SelectedValue == null)
                    cmd.Parameters.AddWithValue("birth_country_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birth_country_code",cmbBirthCountry.Text);
        
                cmd.Parameters.AddWithValue("delegate_last_name", tDelLast.Text);
                cmd.Parameters.AddWithValue("delegate_first_name", tDelFirst.Text);
                cmd.Parameters.AddWithValue("delegate_second_name", tDelSec.Text);
                cmd.Parameters.AddWithValue("delegate_ser", tDelSer.Text);
                cmd.Parameters.AddWithValue("delegate_num", tDelNum.Text);
                if(tDelIssue.SelectedDate == "")
                {
                    cmd.Parameters.AddWithValue("delegate_dul_issue_dt", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("delegate_dul_issue_dt", Convert.ToDateTime(tDelIssue.SelectedDate));
                }
                
                if (cmbDelCountry.SelectedValue == null)
                    cmd.Parameters.AddWithValue("delegate_country_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("delegate_country_code", Convert.ToInt16(cmbDelCountry.SelectedValue));
                if (cmbDelNat.SelectedValue == null)
                    cmd.Parameters.AddWithValue("delegate_nationality_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("delegate_nationality_code", Convert.ToInt16(cmbDelNat.SelectedValue));
             
                cmd.Parameters.AddWithValue("delegate_dul_code", cmbDelDul.SelectedValue==null? "" : cmbDelDul.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("status", "Y");
                cmd.ExecuteNonQuery();

                /*==========================================================*/
               
                if (DocValid != "not add")
                {
                    sql = "INSERT INTO cmodb.document( " +
                    " contact_id, ident, type, invite_num, ser, num, issue_dt,  " +
                    " validity_from_dt, validity_to_dt, status,code) " +
                     " VALUES(:contact_id, :ident, :type, :invite_num, :ser, :num, :issue_dt, " +
                    " :validity_from_dt, :validity_to_dt, :status, (select MAX(code)+1 from cmodb.document)) RETURNING code; ";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("ident", tIdent.Text);
                    cmd.Parameters.AddWithValue("type", cmbDocType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("invite_num", tInvite.Text);
                    cmd.Parameters.AddWithValue("ser", tDocSer.Text);
                    cmd.Parameters.AddWithValue("num", tDocNum.Text);
                    if (tDocIssue.SelectedDate == "")
                        cmd.Parameters.AddWithValue("issue_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("issue_dt", Convert.ToDateTime(tDocIssue.SelectedDate));
                    if (tDocValidFrom.SelectedDate == "")
                        cmd.Parameters.AddWithValue("validity_from_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("validity_from_dt", Convert.ToDateTime(tDocValidFrom.SelectedDate));
                    if (tDocValidTo.SelectedDate == "")
                        cmd.Parameters.AddWithValue("validity_to_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("validity_to_dt", Convert.ToDateTime(tDocValidTo.SelectedDate));
                    cmd.Parameters.AddWithValue("status", "Y");
                    NpgsqlParameter pr = new NpgsqlParameter("code", DbType.Int16);
                    pr.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(pr);

                    cmd.ExecuteNonQuery();
                }

                /*==========================================================*/
               
                if (MigrValid != "not add")
                {
                    sql = "INSERT INTO cmodb.migr_card( " +
                        " contact_id, ser, num, kpp_code, entry_dt, tenure_from_dt,  " +
                        " tenure_to_dt, status, purpose_entry) " +
                         " VALUES(:contact_id, :ser, :num, :kpp_code, :entry_dt, :tenure_from_dt, " +
                        " :tenure_to_dt, :status, :purpose_entry); ";
                    cmd.CommandText = sql;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("ser", tMigrSer.Text);
                    cmd.Parameters.AddWithValue("num", tMigrNum.Text);
                    cmd.Parameters.AddWithValue("kpp_code", cmbKPP.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("status", "Y");
                    cmd.Parameters.AddWithValue("purpose_entry", tMigrPurpose.Text);
                  //  cmd.Parameters.AddWithValue("document_id", pr.Value);
                    if (tMigEntryDt.SelectedDate == "")
                        cmd.Parameters.AddWithValue("entry_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("entry_dt", Convert.ToDateTime(tMigEntryDt.SelectedDate));
                    if (tMigTenureFrom.SelectedDate == "")
                        cmd.Parameters.AddWithValue("tenure_from_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("tenure_from_dt", Convert.ToDateTime(tMigTenureFrom.SelectedDate));
                    if (tMigTenureTo.SelectedDate == "")
                        cmd.Parameters.AddWithValue("tenure_to_dt", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("tenure_to_dt", Convert.ToDateTime(tMigTenureTo.SelectedDate));
                    cmd.Parameters.AddWithValue("status", "Y");
                    cmd.ExecuteNonQuery();
                }
                /*==========================================================*/

                if (DULValid != "not add")
                {
                    sql = "INSERT INTO cmodb.dul( " +
                    " contact_id,type, ser, num, issue, validity, status) " +
                    " VALUES(:contact_id,:type, :ser, :num, :issue, :validity, :status); ";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("type", cmbDulType.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("ser", tDulSer.Text);
                    cmd.Parameters.AddWithValue("num", tDulNum.Text);
                    cmd.Parameters.AddWithValue("status", "Y");                    
                    if (tDulIssue.SelectedDate == "")
                        cmd.Parameters.AddWithValue("issue", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("issue", Convert.ToDateTime(tDulIssue.SelectedDate));
                    if (tDulValidity.SelectedDate == "")
                        cmd.Parameters.AddWithValue("validity", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("validity", Convert.ToDateTime(tDulValidity.SelectedDate));

                    cmd.ExecuteNonQuery();
                }
                /*==========================================================*/
                if (AgreeValid != "not add")
                {
                    sql = "INSERT INTO cmodb.agree( " +
                    " contact_id, num, dt, from_dt, to_dt, status) " +
                   " VALUES(:contact_id, :num, :dt, :from_dt, :to_dt, :status); ";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("num", tAgree.Text);
                    cmd.Parameters.AddWithValue("status", "Y");
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
                    cmd.ExecuteNonQuery();
                }
                /*==========================================================*/
                if (TeachValid != "not add")
                {
                    sql = "INSERT INTO cmodb.teach_info( " +
                    " postup_year, contact_id, status, spec_code, form_teach_code,  " +
                    "  form_pay_code, prog_teach_code, period_total, period_ind, period_total_p,  " +
                    "   period_ind_p, facult_code, amount) " +
                    "   VALUES(:postup_year, :contact_id, :status, :spec_code, :form_teach_code, " +
                    "  :form_pay_code, :prog_teach_code, :period_total, :period_ind, :period_total_p, " +
                    "  :period_ind_p, :facult_code, :amount);";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("postup_year", tYear.Text);
                    cmd.Parameters.AddWithValue("status", "Y");
                    if (cmbSpec.Text != "")
                        cmd.Parameters.AddWithValue("spec_code", Convert.ToInt32(cmbSpec.SelectedValue.ToString()));
                    else
                        cmd.Parameters.AddWithValue("spec_code", DBNull.Value);
                   // cmd.Parameters.AddWithValue("spec_code", cmbSpec.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("form_teach_code", cmbFO.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("form_pay_code", cmbFin.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("prog_teach_code", cmbPO.SelectedValue.ToString());
                    /*
                    cmd.Parameters.AddWithValue("period_total", Convert.ToInt16(tTeachTotal.Text));
                    cmd.Parameters.AddWithValue("period_ind", Convert.ToInt16(tTeachInd.Text));
                    cmd.Parameters.AddWithValue("period_total_p", cmbTotalSrok.Text);
                    cmd.Parameters.AddWithValue("period_ind_p", cmbIndSrok.Text);*/
                    if (tTeachTotal.Text != "")
                    {
                        cmd.Parameters.AddWithValue("period_total", Convert.ToInt16(tTeachTotal.Text));
                        cmd.Parameters.AddWithValue("period_total_p", cmbTotalSrok.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("period_total", DBNull.Value);
                        cmd.Parameters.AddWithValue("period_total_p", DBNull.Value);
                    }
                    if (tTeachInd.Text != "")
                    {
                        cmd.Parameters.AddWithValue("period_ind", Convert.ToInt16(tTeachInd.Text));
                        cmd.Parameters.AddWithValue("period_ind_p", cmbIndSrok.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("period_ind", DBNull.Value);
                        cmd.Parameters.AddWithValue("period_ind_p", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("facult_code", Convert.ToInt16(cmbFac.SelectedValue));
                    //cmd.Parameters.AddWithValue("amount", Convert.ToDecimal(tYearAmount.Text));
                    if (tYearAmount.Text != "")
                        cmd.Parameters.AddWithValue("amount", Convert.ToDecimal(tYearAmount.Text));
                    else
                        cmd.Parameters.AddWithValue("amount", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
                /*==========================================================*/

                if (SelAddr != 0)
                {
                    sql = "INSERT INTO cmodb.addr_inter(contact_id,status,address_code)VALUES (:contact_id,:status,:address_code);";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("address_code", SelAddr);
                    cmd.Parameters.AddWithValue("status", "Y");

                    cmd.ExecuteNonQuery();
                }
                /*==========================================================*/

                transaction.Commit();

                MessageBox.Show("Успешно сохранено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при добавлении студента: \n" + msg.Message);
                MessageBox.Show("Ошибка при добавлении студента: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            MonthCalendar cl = new MonthCalendar();
            cl.PointToScreen(new Point(400, 400));
            cl.Parent = this;
            cl.BringToFront();
            cl.Show();
        }

        private void cmbFac_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //cmbSpec.DataSource = DB.QueryTableMultipleParams(pref.SPECLOAD, new List<object>{Convert.ToInt16( cmbFac.SelectedValue) });
                string POCode;
                if (cmbPO.Text != "")
                {
                    POCode = DB.GetTableValue("select code from cmodb.lov where type='PTEACH' AND value = :param1;", new List<object> { cmbPO.Text });
                    cmbSpec.DataSource = DB.QueryTableMultipleParams(pref.SPECLOAD, new List<object> { Convert.ToInt16(cmbFac.SelectedValue), POCode });
                }
                else
                    cmbSpec.DataSource = DB.QueryTableMultipleParams(pref.SPECLOAD, new List<object> { Convert.ToInt16(cmbFac.SelectedValue), DBNull.Value });




                cmbSpec.SelectedIndex = -1;
            }
            catch(Exception er)
            {
                MessageBox.Show("Ошибка при выборе факультета: \n" + er.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSpec_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbSpec.SelectedValue == null)
                tSpecCode.Text = "";
            else
            {               
                
                    tSpecCode.Text = DB.GetTableValue("select code from cmodb.speciality where spec_code=:param1;", new List<object> { Convert.ToInt32(cmbSpec.SelectedValue) });

            }
            // tSpecCode.Text = cmbSpec.SelectedValue == null ? "" : DB.GetTableValue("select code from cmodb.speciality where spec_code=:param1;", new List<object> { Convert.ToInt32(cmbSpec.SelectedValue) });
        }

        private void btnAddrClear_Click(object sender, EventArgs e)
        {
            SelAddr = 0;
            tFullAddress.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Действительно закрыть???", "Инфо", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.DialogResult = DialogResult.Cancel;
        }

        private void cmbPO_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbFac_SelectedValueChanged(this, null);
        }
    }

  
}
