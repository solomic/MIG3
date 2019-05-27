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
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;

namespace Mig
{
    public partial class fContactDetail : Form
    {
        public string GETNOW = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string ClassName = "Class: fContactDetail.cs\n";
        public string DULState = "";

        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        public fContactDetail()
        {
            InitializeComponent();
            SetDoubleBuffered(dgDocSel, true);
            SetDoubleBuffered(dgMigrHist, true);
            SetDoubleBuffered(DULdataGridView, true);
            SetDoubleBuffered(dgTeach, true);
            SetDoubleBuffered(dgAgreeHist, true);
            SetDoubleBuffered(dgExpellHist, true);
            SetDoubleBuffered(dgAddrHist, true);
            SetDoubleBuffered(dgEntryHist, true);
            SetDoubleBuffered(dgChild, true);
            
        }
      
        //public void LoadDelegate()
        //{
        //    DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });
        //    /*Представитель*/
        //    this.tDelLast.Text = ContactTable.Rows[0]["delegate_last_name"].ToString();
        //    this.tDelFirst.Text = ContactTable.Rows[0]["delegate_first_name"].ToString();
        //    this.tDelSec.Text = ContactTable.Rows[0]["delegate_second_name"].ToString();
        //    this.cmbDelDul.Text = ContactTable.Rows[0]["delegate_dul"].ToString();
        //    this.tDelSer.Text = ContactTable.Rows[0]["delegate_ser"].ToString();
        //    this.tDelNum.Text = ContactTable.Rows[0]["delegate_num"].ToString();
        //    this.tDelIssue.Text = ContactTable.Rows[0]["delegate_dul_issue_dt"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["delegate_dul_issue_dt"]).ToString("dd.MM.yyyy");
        //    this.cmbDelCountry.Text = ContactTable.Rows[0]["delegate_country"].ToString();
        //    this.cmbDelNat.Text = ContactTable.Rows[0]["delegate_nationality"].ToString();
        //}

        public void LoadContactSpr()
        {
            /*заполняем справочники*/

            /*контакт*/

            cmbSex.DataSource = DB.QueryTableMultipleParams("SELECT code, value FROM cmodb.lov where type=@param1 order by ord;", new List<object> { "SEX" });
            cmbSex.SelectedIndex = -1;
            //"SELECT name, code  FROM cmodb.nationality order by name;"
            cmbNat.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
            cmbNat.SelectedIndex = -1;
            //"SELECT name, code  FROM cmodb.nationality order by name;"
            cmbBirthCountry.DataSource = DB.QueryTableMultipleParams(pref.NAT, null);
            cmbBirthCountry.SelectedIndex = -1;
            tBirthTown.DataSource = DB.QueryTableMultipleParams(pref.BIRTHTOWN, null);
            tBirthTown.SelectedIndex = -1;

            cmbPosition.DataSource = DB.QueryTableMultipleParams("SELECT code, value FROM cmodb.lov where type=@param1 order by ord;", new List<object> { "POS" });
            cmbPosition.SelectedIndex = -1;


            /*Представитель*/
            cmbDelDul.DataSource = DB.QueryTableMultipleParams("SELECT code, value FROM cmodb.lov where type=@param1 order by ord;", new List<object> { "DUL" });
            cmbDelDul.SelectedIndex = -1;
            cmbDelCountry.DataSource = DB.QueryTableMultipleParams("SELECT name, code  FROM cmodb.nationality order by name;", null);
            cmbDelCountry.SelectedIndex = -1;
            cmbDelNat.DataSource = DB.QueryTableMultipleParams("SELECT name, code  FROM cmodb.nationality order by name;", null);
            cmbDelNat.SelectedIndex = -1;

        }
        public void LoadContact()
        {

            DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });
            this.Text = ContactTable.Rows[0]["last_name"].ToString() + " " + ContactTable.Rows[0]["first_name"].ToString();


            pref.CONFIO = ContactTable.Rows[0]["confio"].ToString().Trim() + "\\";
            pref.CONNAT = ContactTable.Rows[0]["nationality"].ToString();
            pref.DELEGATE = "N";

            this.Text = "(" + ContactTable.Rows[0]["contact_id"].ToString() + ") Информация";
            /*контакт*/

            this.tlastname.Text = ContactTable.Rows[0]["last_name"].ToString();
            this.tfirstname.Text = ContactTable.Rows[0]["first_name"].ToString();
            this.tmidname.Text = ContactTable.Rows[0]["second_name"].ToString();
            this.tlastnameenu.Text = ContactTable.Rows[0]["last_enu"].ToString();
            this.tfirstnameenu.Text = ContactTable.Rows[0]["first_enu"].ToString();
            this.tmidnameenu.Text = ContactTable.Rows[0]["second_enu"].ToString();
            this.cmbSex.Text = ContactTable.Rows[0]["sex"].ToString();
            this.cmbNat.Text = ContactTable.Rows[0]["nationality"].ToString();
            this.cmbBirthCountry.Text = ContactTable.Rows[0]["birth_country"].ToString();
            this.tBirthTown.Text = ContactTable.Rows[0]["birth_town"].ToString();
            // this.dBirthDate.Text = ContactTable.Rows[0]["birthday"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["birthday"]).ToString("dd.MM.yyyy");
            dBirthDate.SelectedDate = ContactTable.Rows[0]["birthday"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["birthday"]).ToString("dd.MM.yyyy");
            
            if (ContactTable.Rows[0]["birthday"] != DBNull.Value)
            {
                int curyear = DateTime.Now.Year;
                int studday = Convert.ToDateTime(ContactTable.Rows[0]["birthday"]).Day;
                int studyear = Convert.ToDateTime(ContactTable.Rows[0]["birthday"]).Year;
                int studmonth = Convert.ToDateTime(ContactTable.Rows[0]["birthday"]).Month;
                //др в високосный год
                DateTime dd;
                if (studday == 29 && studmonth == 2)
                    dd = new DateTime(DateTime.Now.Year, studmonth, 28);
                else
                    dd = new DateTime(DateTime.Now.Year, studmonth, studday);
                int studfullyear = dd > DateTime.Now ? (curyear - studyear - 1) : (curyear - studyear);
                if (studfullyear < 18)
                {
                    lyear.Text = "Нет 18 лет! (" + studfullyear.ToString() + ")";
                    pref.DELEGATE = "Y";
                }
                else
                    lyear.Text = "";

                
            }

            DULRefresh();

        }

        private void DULRefresh()
        {
            string GetDul = "SELECT cmodb.LookupValue('DUL',type) as \"Тип\", ser as \"Серия\", num as \"Номер\", CONVERT(varchar(10),issue,104) as \"Выдан\", " +
            " CONVERT(varchar(10),validity,104) as \"Годен до\", " +
            " CASE WHEN validity IS NOT NULL THEN CONVERT(varchar(10),DATEADD(MONTH,-6,validity),104) END \"Действие с визой\"  "
            + " FROM cmodb.dul where contact_id=@param1 AND status='Y' ;";
            DataTable DULTable = DB.QueryTableMultipleParams(GetDul, new List<object> { pref.CONTACTID });
            if (DULTable.Rows.Count != 0)
            {
                textBox1.Text = DULTable.Rows[0]["Тип"].ToString();
                textBox2.Text = (DULTable.Rows[0]["Серия"].ToString() + " " + DULTable.Rows[0]["Номер"].ToString()).Trim();
                textBox3.Text = DULTable.Rows[0]["Выдан"].ToString();
                textBox4.Text = DULTable.Rows[0]["Годен до"].ToString();
                textBox5.Text = DULTable.Rows[0]["Действие с визой"].ToString();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        public void LoadContactEx()
        {
            DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });

            this.tRelatives.Text = ContactTable.Rows[0]["relatives"].ToString();
            this.cmbPosition.Text = ContactTable.Rows[0]["position"].ToString();
            this.tInsurance.Text = ContactTable.Rows[0]["med"].ToString();
            med_fr.SelectedDate = ContactTable.Rows[0]["med_from"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["med_from"]).ToString("dd.MM.yyyy");
            med_to.SelectedDate = ContactTable.Rows[0]["med_to"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["med_to"]).ToString("dd.MM.yyyy");

            this.tPersAddress.Text = ContactTable.Rows[0]["address_home"].ToString();
            //  DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });
            this.tComments.Text = ContactTable.Rows[0]["comments"].ToString();
            this.tInfo.Text = ContactTable.Rows[0]["phone"].ToString();

        }
        public void LoadConDelegate()
        {
            DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });            
            /*Представитель*/
            this.tDelLast.Text = ContactTable.Rows[0]["delegate_last_name"].ToString();
            this.tDelFirst.Text = ContactTable.Rows[0]["delegate_first_name"].ToString();
            this.tDelSec.Text = ContactTable.Rows[0]["delegate_second_name"].ToString();
            this.cmbDelDul.Text = ContactTable.Rows[0]["delegate_dul"].ToString();
            this.tDelSer.Text = ContactTable.Rows[0]["delegate_ser"].ToString();
            this.tDelNum.Text = ContactTable.Rows[0]["delegate_num"].ToString();
            this.tDelIssue.Text = ContactTable.Rows[0]["delegate_dul_issue_dt"] == DBNull.Value ? null : Convert.ToDateTime(ContactTable.Rows[0]["delegate_dul_issue_dt"]).ToString("dd.MM.yyyy");
            this.cmbDelCountry.Text = ContactTable.Rows[0]["delegate_country"].ToString();
            this.cmbDelNat.Text = ContactTable.Rows[0]["delegate_nationality"].ToString();
        }
        public void LoadDUL()
        {
            DULdataGridView.Columns["dulstatus"].DataPropertyName = "dulstatus";
            DULdataGridView.DataSource = DB.QueryTableMultipleParams(pref.GetDulAllSql, new List<object> { pref.CONTACTID });           
            DULdataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DULdataGridView.Columns["id"].Visible = false;
        }
        public void LoadExpell()
        {
            dgExpellHist.Columns["status"].DataPropertyName = "status";
            dgExpellHist.DataSource = DB.QueryTableMultipleParams("SELECT id,case when status='Y' then 'true' else 'false' end as status,expelled, expelled_num, expelled_dt,updated ,updated_by  FROM cmodb.expell where contact_id=@param1 and deleted='N';", new List<object> { pref.CONTACTID });
            dgExpellHist.Columns["id"].Visible = false;
            dgExpellHist.Columns["status"].HeaderText = "Основной";
            dgExpellHist.Columns["expelled"].HeaderText = "Основание";
            dgExpellHist.Columns["expelled_num"].HeaderText = "Номер приказа";
            dgExpellHist.Columns["expelled_dt"].HeaderText = "Дата";
            dgExpellHist.Columns["updated"].HeaderText = "Обновлено";
            dgExpellHist.Columns["updated_by"].HeaderText = "Обновил(а)";
        }
        public void LoadDoc()
        {            
            string sql;
            sql = "SELECT id, CASE WHEN status='Y' THEN 'true' ELSE 'false' END as docstatus,cmodb.LookupValue('MIGR.VIEW',type) as type, ser, num, issue_dt,validity_from_dt, validity_to_dt,ident, invite_num "+
                ",updated,updated_by FROM cmodb.document where contact_id=@param1 AND deleted='N' ORDER BY status DESC,validity_to_dt DESC;";
            dgDocSel.Columns["docstatus"].DataPropertyName = "docstatus";
            dgDocSel.DataSource = DB.QueryTableMultipleParams(sql, new List<object> { pref.CONTACTID });
            dgDocSel.Columns["id"].Visible = false;
            dgDocSel.Columns["type"].HeaderText = "Тип";
            dgDocSel.Columns["ser"].HeaderText = "Серия";
            dgDocSel.Columns["num"].HeaderText = "Номер";
            dgDocSel.Columns["issue_dt"].HeaderText = "Дата выдачи";
            dgDocSel.Columns["validity_from_dt"].HeaderText = "Срок действия с";
            dgDocSel.Columns["validity_to_dt"].HeaderText = "Срок действия по";
            dgDocSel.Columns["ident"].HeaderText = "Идентификатор";
            dgDocSel.Columns["invite_num"].HeaderText = "Номер приглашения";
            dgDocSel.Columns["updated"].HeaderText = "Обновлено";
            dgDocSel.Columns["updated_by"].HeaderText = "Обновил(а)";
           
        }
        public void LoadMigr()
        {
            
            string sql = "SELECT id,CASE WHEN status='Y' THEN 'true' ELSE 'false' END as migrstatus,ser, num, kpp_code, entry_dt, tenure_from_dt, tenure_to_dt,purpose_entry " +
                ",updated,updated_by FROM cmodb.migr_card where contact_id=@param1 AND deleted='N' ORDER BY status desc,tenure_to_dt DESC;";
            dgMigrHist.Columns["migrstatus"].DataPropertyName = "migrstatus";
            dgMigrHist.DataSource = DB.QueryTableMultipleParams(sql, new List<object> { pref.CONTACTID });
            dgMigrHist.Columns["id"].Visible = false;
            dgMigrHist.Columns["ser"].HeaderText = "Серия";
            dgMigrHist.Columns["num"].HeaderText = "Номер";
            dgMigrHist.Columns["kpp_code"].HeaderText = "КПП";
            dgMigrHist.Columns["entry_dt"].HeaderText = "Дата въезда";
            dgMigrHist.Columns["tenure_from_dt"].HeaderText = "Срок пребывания с";
            dgMigrHist.Columns["tenure_to_dt"].HeaderText = "Срок пребывания по";
            dgMigrHist.Columns["purpose_entry"].HeaderText = "Цель въезда";
            dgMigrHist.Columns["updated"].HeaderText = "Обновлено";
            dgMigrHist.Columns["updated_by"].HeaderText = "Обновил(а)";
        }
        public void LoadChild()
        {
            /*детишки*/
            dgChild.DataSource = DB.QueryTableMultipleParams(pref.GetChildSql, new List<object> { pref.CONTACTID });
            dgChild.Columns["id"].Visible = false;
            dgChild.Columns["fio"].HeaderText = "ФИО";
            dgChild.Columns["birthday"].HeaderText = "Дата рождения";
            dgChild.Columns["address"].HeaderText = "Адрес";
            dgChild.Columns["nationality"].HeaderText = "Гражданство";

        }
        public void LoadTeach()
        {
            dgTeach.Columns["teachstatus"].DataPropertyName = "teachstatus";
            dgTeach.DataSource= DB.QueryTableMultipleParams(pref.GetTeachAllSql, new List<object> { pref.CONTACTID });
            dgTeach.Columns["id"].Visible = false;
            dgTeach.Columns["facult_code"].Visible = false;
            dgTeach.Columns["period_total"].Visible = false;
            dgTeach.Columns["period_ind"].Visible = false;
            dgTeach.Columns["period_total_p"].Visible = false;
            dgTeach.Columns["period_ind_p"].Visible = false;
            dgTeach.Columns["amount"].Visible = false;
            dgTeach.Columns["code"].Visible = false;

            dgTeach.Columns["teachstatus"].HeaderText = "Основной";
            dgTeach.Columns["postup_year"].HeaderText = "Год поступления";
            dgTeach.Columns["deduct_year"].HeaderText = "Год отчисления";
            dgTeach.Columns["spec_name"].HeaderText = "Специальность";
            dgTeach.Columns["fac_name"].HeaderText = "Факультет";
            dgTeach.Columns["form_teach"].HeaderText = "Форма обучения";
            dgTeach.Columns["form_pay"].HeaderText = "Финансирование";
            dgTeach.Columns["form_teach"].HeaderText = "Форма обучения";
            dgTeach.Columns["prog_teach"].HeaderText = "Программа обучения";
            dgTeach.Columns["code"].HeaderText = "Шифр";

            
        }
        public void LoadAgree()
        {
            dgAgreeHist.Columns["agreestatus"].DataPropertyName = "agreestatus";
            dgAgreeHist.DataSource = DB.QueryTableMultipleParams("SELECT id,case when status='Y' then 'true' Else 'false' end as agreestatus,num, dt, from_dt,to_dt  FROM cmodb.agree where contact_id=@param1 and deleted='N' order by CASE WHEN status IS NULL THEN 1 ELSE 0 END DESC,status desc,to_dt desc;", new List<object> { pref.CONTACTID });
            dgAgreeHist.Columns["id"].Visible = false;
            dgAgreeHist.Columns["agreestatus"].HeaderText = "Основной";
            dgAgreeHist.Columns["num"].HeaderText = "Договор/Направление";
            dgAgreeHist.Columns["dt"].HeaderText = "От";
            dgAgreeHist.Columns["from_dt"].HeaderText = "С";
            dgAgreeHist.Columns["to_dt"].HeaderText = "По";
       
        }
       
        public void LoadPf()
        {
            dgPf.DataSource = DB.QueryTableMultipleParams(pref.GetPfSql, new List<object> { pref.CONTACTID });
            dgPf.Columns["name"].HeaderText = "Наименование";
            dgPf.Columns["created"].HeaderText = "Дата создания";
            dgPf.Columns["created_by"].HeaderText = "Создал(а)";
            dgPf.Columns["id"].Visible = false;
        }

       /* public void LoadStage()
        {
            dgStage.DataSource = DB.QueryTableMultipleParams(pref.GetStageSql, new List<object> { pref.CONTACTID });
            dgStage.Columns["id"].Visible = false;
            dgStage.Columns["stage"].HeaderText = "Этап";
            dgStage.Columns["due_dt"].HeaderText = "Срок оплаты";
            dgStage.Columns["amount"].HeaderText = "Сумма";
            dgStage.Columns["receipt"].HeaderText = "Квитанция";
            dgStage.Columns["pay_dt"].HeaderText = "Факт. дата оплаты";
            //SELECT id, stage, due_dt, to_dt, amount, receipt, pay_dt FROM cmodb.stage WHERE status='Y' and contact_id=param1;
        }*/
       
        private void fContactDetail_Load(object sender, EventArgs e)
        {
            try {
               
                /*ОТОБРАЖЕНИЕ ВКЛАДОК В ЗАВИСИМОСТИ ОТ ПОЗИЦИИ*/
                LoadContactSpr();
                LoadContact();
                LoadContactEx();             
                LoadPf();
                
                /*ПРОГРУЖАЕМ ДАННЫЕ*/

               
                /*Дней в РФ*/
                tDays.Text = DB.GetTableValue("SELECT allday FROM cmodb.\"ResidentView\" where contact_id=@param1;", new List<object> { pref.CONTACTID });

                /*ИНИЦИАЛИЗАЦИЯ ПАРАМЕТРОВ ОТЧЕТОВ*/
                //ходатайство обычное
                cmbPetitionStandartParam1.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.ENTTITY" });
                cmbPetitionStandartParam1.SelectedIndex = -1;
                cmbPetitionStandartParam2.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.REASON" });
                cmbPetitionStandartParam2.SelectedIndex = -1;
                //ходатайство смена визы
                cmbPetVisa1.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.ENTTITY" });
                cmbPetVisa1.SelectedIndex = -1;
                cmbPetVisa2.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.REASON" });
                cmbPetVisa2.SelectedIndex = -1;
                //ходатайство смена паспорта
                cmbPetPass1.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.ENTTITY" });
                cmbPetPass1.SelectedIndex = -1;
                cmbPetPass2.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "REG.HOD.REASON" });
                cmbPetPass2.SelectedIndex = -1;
                //Визовая анкета.анкета
                cmbVisaAction.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "VISA.ANK" });
                cmbVisaAction.SelectedIndex = -1;
                cmbVisaKrat.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "VISA.KRAT" });
                cmbVisaKrat.SelectedIndex = 2;//многократная по умол.

                cmbVisaCat.SelectedIndexChanged -= cmbVisaCat_SelectedIndexChanged;
                cmbVisaCat.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "VISA.CAT" });
                cmbVisaCat.SelectedIndex = 0;//обыкновенная по умол.
                cmbVisaCat.SelectedIndexChanged += cmbVisaCat_SelectedIndexChanged;

                cmbVisaSubCat.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "VISA.SUBCAT" });
                cmbVisaSubCat.SelectedIndex = 6;//учебная по умол.
                cmbVisaPurpose.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "VISA.PURPOSE" });
                cmbVisaPurpose.SelectedIndex = -1;
                cmbAnkHodReason.DataSource = DB.QueryTableMultipleParams(pref.DOCTYPE, new List<object> { "ANK.HOD.REASON" });
                cmbAnkHodReason.SelectedIndex = -1;


                /*скрываем параметры отчетов*/
                treeView1.ExpandAll();
                foreach (TabPage tab in tabParam.TabPages)
                {
                    tab.Parent = null;
                }
                tabParamNull.Parent = tabParam;

                

                /*поле действие с визой*/
              /*  if (this.dDulValidity.SelectedDate != "")
                {
                    
                    DateTime dt;
                    dt = Convert.ToDateTime(this.dDulValidity.SelectedDate);                    
                    tVisaAction.Text = dt.AddMonths(-6).ToString("dd.MM.yyyy") ;
                }*/
               /* if DM.ZContactQuery.FieldByName('PER_DATE_VALIDITY').AsString <> '' then
                    Edit1.Text:= datetostr(IncMonth(DM.ZContactQuery.FieldByName('PER_DATE_VALIDITY').AsDateTime, -6));*/
            }
            catch(Exception err)
            {
                Logger.Log.Error(ClassName + "Function:fContactDetail_Load\n Error:" + err.Message);
                MessageBox.Show("Ошибка при загрузке карточки студента:\n"+err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach(TabPage tab in tabParam.TabPages)
            {
                tab.Parent = null;
            }
            switch (e.Node.Name)
            {
                case "Node5":
                    tabParam1.Parent = tabParam;/*ходатайство обычное*/
                    break;
                case "Node6":
                    tabParam3.Parent = tabParam;/*ходатайство смена визы*/
                    break;
                case "Node7":
                    tabParam4.Parent = tabParam;/*ходатайство смена паспорта*/
                    break;
                case "Node8":
                    tabParamNull.Parent = tabParam;/*ходатайство утеря уведомления*/
                    break;
                case "Node9":
                    tabParam5.Parent = tabParam;/*Визовая анкета: анкета*/
                    break;
                case "Node10":
                    tabParamNull.Parent = tabParam;/*Гарантия*/
                    break;
                case "Node11":
                    tabParam7.Parent = tabParam;/*Ходатайство на визу*/
                    break;
                case "Node12":
                    tabParamNull.Parent = tabParam;/*Уведомление о прибытии*/
                    break;
                case "Node13":
                    tabParam8.Parent = tabParam;/*Уведомление на отчисление*/
                    break;
                case "Node15":
                    tabParamNull.Parent = tabParam;/*Уведомление на отчисление в УФМС*/
                    break;
                case "Node14":
                    tabParamNull.Parent = tabParam;/*Уведомление на отчисление в ДО*/
                    break;
                case "Node1":
                    tabParamNull.Parent = tabParam;/*Справка в банк*/
                    break;
                default:
                    tabParamNull.Parent = tabParam;
                    break;
            }
            btnMakePF.Enabled = e.Node.Tag.ToString() == "1" ? true : false;

            lRepName.Text = e.Node.Text;

        }

        private void btnDocMigrHist_Click_1(object sender, EventArgs e)
        {
            fDocMigrHist fDocMigrHistForm = new fDocMigrHist();
            fDocMigrHistForm.ShowDialog(this);
            fDocMigrHistForm = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*СФОРМИРОВАТЬ*/
            string RepRes = "";
            try { 
                    switch (treeView1.SelectedNode.Name)
                    {
                        case "Node5":                   
                            (new pf()).GeneratePetitionStandart(cmbPetitionStandartParam1.Text, cmbPetitionStandartParam2.Text);                    
                            break;
                        case "Node6":                           
                            (new pf()).GeneratePetitionVisa(cmbPetVisa1.Text, cmbPetVisa2.Text);                           
                            break;
                        case "Node7":                           
                             (new pf()).GeneratePetitionPassport(cmbPetPass1.Text, cmbPetPass2.Text);                            
                            break;
                        case "Node8":
                            (new pf()).GeneratePetitionLost();                           
                            break;
                        case "Node9": /*Визовая анкета: анкета*/                        
                             (new pf()).GenerateVisaAnketa(cmbVisaAction.SelectedIndex.ToString(),cmbVisaKrat.Text,cmbVisaCat.SelectedIndex.ToString(),cmbVisaSubCat.SelectedIndex.ToString(),cmbVisaPurpose.Text );
                           break;
                        case "Node10":
                            (new pf()).GenerateVisaGuarant();                            
                            break;
                        case "Node11":                            
                            (new pf()).GenerateVisaPetition(cmbVisaPetition.Text, cmbAnkHodReason.Text);                           
                            break;
                        case "Node12":
                            (new pf()).GenerateDepObr();                       
                        break;
                    case "Node13":                            
                            (new pf()).GeneratePetitionOut(cmbPetitionOut.SelectedIndex);                            
                            break;
                        case "Node15":                   
                           (new pf()).GeneratePetitionDeduct();                           
                            break;
                        case "Node14":
                           (new pf()).GeneratePetitionDeductDo();                            
                            break;
                        case "Node1":
                            (new pf()).GenerateSprBank();                           
                            break;
                        case "NodeNotify":     
                           // if (pref.NOTIFYTEMPLATE == "xls")               
                                RepRes = (new pf()).GenerateNotifyXls();
                           // else
                                //RepRes = (new pf()).GenerateNotify();                            
                            break;
                    }
                MessageBox.Show("Документ успешно сформирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPf();
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:button5_Click\n Error:" + ex);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnContactEdit_Click(object sender, EventArgs e)
        {
           /* fContactEdit fContactEditForm = new fContactEdit();
            if( fContactEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadContact();
                LoadDUL();
            }

            fContactEditForm = null;*/
        }

       

        private void dgPf_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dgPf.SelectedRows.Count != 0)
                {
                    Process.Start(pref.REPORTFOLDER + pref.CONNAT+"\\"+pref.CONFIO + dgPf.CurrentRow.Cells["name"].Value);
                }
            }
            catch(Exception msg)
            {
                MessageBox.Show("Ошибка при открытии файла: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnStageAdd_Click(object sender, EventArgs e)
        {
           /* fStage fStageForm = new fStage("Add",0);
            if (fStageForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadStage();
            }*/
        }



        private void btnStageDelete_Click(object sender, EventArgs e)
        {
           /* if (dgStage.SelectedRows.Count != 0)
            {

                if (MessageBox.Show("Точно удаляем?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    string sql = "";
                    try
                    {
                        int Contact_id = pref.CONTACTID;
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd = new SqlCommand(sql, DB.conn);
                        sql = "UPDATE cmodb.stage SET status=:status where id=:id;";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", Convert.ToInt32(dgStage.CurrentRow.Cells["id"].Value));
                        cmd.Parameters.AddWithValue("status", "N");
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                       // LoadEntry();
                        MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        Console.WriteLine("Ошибка при удалении: \n" + msg.Message);
                        MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadStage();
                }
            }*/
        }

        private void cmbVisaCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVisaCat.SelectedIndex == 0)
            {
                cmbVisaSubCat.Enabled = true;
                cmbVisaSubCat.SelectedIndex = 6;
            }
            else
            {
                cmbVisaSubCat.Enabled = false;
                cmbVisaSubCat.SelectedIndex = -1;
                //cmbVisaSubCat.Text = "";
            }
          

        }


       
   

        private void tabDUL_Enter(object sender, EventArgs e)
        {
            LoadDUL();
        }

      

        private void toolContactEdit_Click(object sender, EventArgs e)
        {
            /*разрешаем редактирование студента*/
            CanEditContact(true);
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            /*сохраняем изменения контакта*/
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql;
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                // sql = "UPDATE cmodb.contact SET status='N' where contact_id=:contact_id and status='Y' returning id;";
                sql = "UPDATE cmodb.contact SET " +
                    " last_name= @last_name, second_name= @second_name, birthday= @birthday, birth_town= @birth_town, updated=GETDATE(), updated_by= SYSTEM_USER,  " +
                    " sex= @sex, first_name= @first_name, " +
                    "last_enu= @last_enu, first_enu= @first_enu, " +
                    " second_enu= @second_enu,  " +
                    " nationality=@nationality_code, birth_country = @birth_country_code, " +
                    " med_from=@med_from,med_to=@med_to," +
                     " status = @status where contact_id =@contact_id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
              //  cmd.Transaction = transaction;

                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("status", "Y");
                cmd.Parameters.AddWithValue("last_name", tlastname.Text);
                cmd.Parameters.AddWithValue("second_name", tmidname.Text);
                if (dBirthDate.SelectedDate == "")
                    cmd.Parameters.AddWithValue("birthday", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birthday", Convert.ToDateTime(dBirthDate.SelectedDate));
               
                cmd.Parameters.AddWithValue("sex", cmbSex.Text);
                cmd.Parameters.AddWithValue("first_name", tfirstname.Text);
                cmd.Parameters.AddWithValue("last_enu", tlastnameenu.Text);
                cmd.Parameters.AddWithValue("first_enu", tfirstnameenu.Text);

                cmd.Parameters.AddWithValue("second_enu", tmidnameenu.Text);
                if (cmbNat.Text == null) /*SelectedValue*/
                    cmd.Parameters.AddWithValue("nationality_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("nationality_code", cmbNat.Text);
                if (cmbBirthCountry.Text == null) /*SelectedValue*/
                    cmd.Parameters.AddWithValue("birth_country_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birth_country_code", cmbBirthCountry.Text);
                if (tBirthTown.Text == null)
                    cmd.Parameters.AddWithValue("birth_town", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("birth_town", tBirthTown.Text);
                // cmd.Parameters.AddWithValue("birth_town", tBirthTown.Text);


                cmd.ExecuteNonQuery();
                
                transaction.Commit();

                DataTable ContactTable = DB.QueryTableMultipleParams(pref.GetContactDetailSql, new List<object> { pref.CONTACTID });
                this.Text = ContactTable.Rows[0]["last_name"].ToString() + " " + ContactTable.Rows[0]["first_name"].ToString();


                pref.CONFIO = ContactTable.Rows[0]["confio"].ToString().Trim() + "\\";
                pref.CONNAT = ContactTable.Rows[0]["nationality"].ToString();


                MessageBox.Show("Обновлен успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при редактировании студента: \n" + msg.Message);
                MessageBox.Show("Ошибка при редактировании студента: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
            CanEditContact(false);
            LoadContact();
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            LoadContact();

            CanEditContact(false);
        }

        public void CanEditContact(bool can)
        {
                toolContactEdit.Enabled = !can;
                btnSaveContact.Enabled = can;
                btnUndoContact.Enabled = can;

                tlastname.ReadOnly = !can;
                tfirstname.ReadOnly = !can;
                tmidname.ReadOnly = !can;
                tlastnameenu.ReadOnly = !can;
                tfirstnameenu.ReadOnly = !can;
                tmidnameenu.ReadOnly = !can;
                cmbNat.Enabled = can; 
                cmbBirthCountry.Enabled = can;
                tBirthTown.Enabled = can;
                dBirthDate.Enabled = can;
                cmbSex.Enabled = can;
               
           
        }

        public void CanEditDelegate(bool can)
        {
            tDelLast.ReadOnly = !can;
            tDelFirst.ReadOnly = !can;
            tDelSec.ReadOnly = !can;
            cmbDelNat.Enabled = can;
            cmbDelDul.Enabled = can;
            tDelSer.ReadOnly = !can;
            tDelNum.ReadOnly = !can;
            tDelIssue.Enabled = can;
            cmbDelCountry.Enabled = can;

        }
        public void CanEditContactEx(bool can)
        {
                toolStripButton24.Enabled = !can;
                btnSaveContactEx.Enabled = can;
                btnUndoContactEx.Enabled = can;

                cmbPosition.Enabled = can;
                tPersAddress.ReadOnly = !can;
                tInsurance.ReadOnly = !can;
                tRelatives.ReadOnly = !can;
                tComments.ReadOnly = !can;
                tInfo.ReadOnly = !can;
                med_fr.Enabled = can;
                med_to.Enabled = can;
        }

      

        private void btnSaveContactEx_Click(object sender, EventArgs e)
        {
            SaveContactEx();
        }

        private void SaveContactEx()
        {

            /*
            + убираем пробелы
            + проверка при сохранении
            */
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql;
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                sql = "UPDATE cmodb.contact " +
                   " SET address_home =@address_home, position_code =@position_code, relatives =@relatives, med =@med,updated=GETDATE(),updated_by=SYSTEM_USER, " +

                        " comments= @comments, " +
                         " phone= @phone, " +
                         "med_from=@med_from,med_to=@med_to" +
                         " WHERE contact_id =@contact_id and status = 'Y';";

                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("address_home", tPersAddress.Text.Trim());
                if (cmbPosition.SelectedValue == null)
                    cmd.Parameters.AddWithValue("position_code", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("position_code", cmbPosition.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("relatives", tRelatives.Text.Trim());
                cmd.Parameters.AddWithValue("med", tInsurance.Text.Trim());
                if (med_fr.SelectedDate == "")
                    cmd.Parameters.AddWithValue("med_from", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("med_from", Convert.ToDateTime(med_fr.SelectedDate));
                if (med_to.SelectedDate == "")
                    cmd.Parameters.AddWithValue("med_to", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("med_to", Convert.ToDateTime(med_to.SelectedDate));
                cmd.Parameters.AddWithValue("comments", tComments.Text.Trim());
                cmd.Parameters.AddWithValue("phone", tInfo.Text.Trim());

                cmd.ExecuteNonQuery();
                transaction.Commit();

                CanEditContactEx(false);

                MessageBox.Show("Обновлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при редактировании студента: \n" + msg.Message);
                MessageBox.Show("Ошибка при редактировании студента: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            /*разрешаем редактирование студента*/
            CanEditContactEx(true);
        }

        private void btnUndoContactEx_Click(object sender, EventArgs e)
        {
            CanEditContactEx(false);
            LoadContactEx();

        }

             

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            /*добавить ДУЛ*/
            fContactEdit fDULAdd = new fContactEdit("ADD", 0);
            if (fDULAdd.ShowDialog(this) == DialogResult.OK)
            {
                LoadDUL();
                DULRefresh();
            }


        }

        private void btnDULEdit_Click_1(object sender, EventArgs e)
        {
            /*редактировать текущий паспорт*/
            /*добавить ДУЛ*/
            fContactEdit fDULAdd = new fContactEdit("EDIT", Convert.ToInt32(DULdataGridView.CurrentRow.Cells["id"].Value));
            if (fDULAdd.ShowDialog(this) == DialogResult.OK)
            {
                LoadDUL();
            }
            DULRefresh();

        }

       

      

        private void btnDULPrimary_Click(object sender, EventArgs e)
        {
            /*Сделать основным*/
            if (DULdataGridView.SelectedRows.Count != 0)
            {
                DULdataGridView.Focus();
                SetDulPrimary(Convert.ToInt32(DULdataGridView.CurrentRow.Cells["id"].Value));
            }
            LoadDUL();
            DULRefresh();
        }

        public void SetDulPrimary(int DULid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                sql = "UPDATE  cmodb.dul SET status='N',updated=GETDATE(),updated_by=SYSTEM_USER where contact_id=@contact_id ;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
              //  cmd.Transaction = transaction;
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.dul SET status='Y',updated=GETDATE(),updated_by=SYSTEM_USER where id=@dulid ;";

                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("dulid", DULid);
                    

                cmd.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("Обновлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();               
                MessageBox.Show("Ошибка при обновлении паспорта: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void DelDul(int DULid)
        {
            if (MessageBox.Show("Удалить выбранный паспорт?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                SqlTransaction transaction = null;
                SqlCommand cmd;
                string sql;
                try
                {
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                    sql = "UPDATE  cmodb.dul SET status='N', deleted='Y',updated=GETDATE(),updated_by=SYSTEM_USER where id=@dulid ;";
                    cmd = new SqlCommand(sql, DB.conn, transaction);
                    //cmd.Transaction = transaction;
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("dulid", DULid);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Удалено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception msg)
                {
                    if (transaction != null) transaction.Rollback();
                    MessageBox.Show("Ошибка при удалении паспорта: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDULDelete_Click(object sender, EventArgs e)
        {
            /*удалить паспорт*/
            if (DULdataGridView.SelectedCells.Count != 0)
            {
                DelDul(Convert.ToInt32(DULdataGridView.CurrentRow.Cells["id"].Value));
                LoadDUL();
                DULRefresh();
            }
           
        }

       

        private void tabDUL_Leave(object sender, EventArgs e)
        {
           
        }

        private void tabDocMigr_Enter(object sender, EventArgs e)
        {
            LoadDoc();
            LoadMigr();
            LoadVisaExtDate();
        }
        public void LoadVisaExtDate()
        {
            tdelivery_dt.Text = DB.GetTableValue("select CONVERT(varchar(10),a.delivery_dt,104) from [cmodb].[contact] a where a.contact_id=@param1", new List<object> {pref.CONTACTID });
            tdate_entry_future.Text = DB.GetTableValue("select CONVERT(varchar(10),a.date_entry_future,104) from [cmodb].[contact] a where a.contact_id=@param1", new List<object> { pref.CONTACTID });
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            /*ДОБАВЛЕНИЕ ДОКУМЕНТА*/
            fDocAddEdit fDocAddEditForm = new fDocAddEdit("Add");
            if (fDocAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadDoc();
            }
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            /*РЕДАКТИРОВАНИЕ ДОКУМЕНТА*/
            if (dgDocSel.SelectedRows.Count == 0)
                return;

            if (dgDocSel.CurrentRow.Cells["docstatus"].Value.ToString() != "true")
            {
                MessageBox.Show("Разрешено редактирование только активного документа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fDocAddEdit fDocAddEditForm = new fDocAddEdit("Edit");
            if (fDocAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadDoc();
            }
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            /*ДОБАВЛЕНИЕ МИГРАЦИОНКИ*/

            fMigrAddEdit fMigrAddEditForm = new fMigrAddEdit("Add");
            if (fMigrAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadMigr();

            }
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            /*РЕДАКТИРОВАНИЕ МИГРАЦИОНКИ*/
            if (dgMigrHist.CurrentRow.Cells["migrstatus"].Value.ToString() != "true")
            {
                MessageBox.Show("Разрешено редактирование только активной миграционки!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            fMigrAddEdit fMigrAddEditForm = new fMigrAddEdit("Edit");
            if (fMigrAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadMigr();
                int cnt = Convert.ToInt32(DB.GetTableValue("SELECT COUNT(1) FROM cmodb.entry where contact_id=@param1 and status='Y';", new List<object> { pref.CONTACTID }));
                if (cnt != 0)
                {
                    if (MessageBox.Show("Очистить поля въезд/выезд?", "Инфо", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string s = fMigrAddEditForm.EntryClear();
                        if (s == "")
                        {
                            LoadEntry();
                            MessageBox.Show("Успешно очищено", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Ошибка при очистке: \n" + s, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

            }
        }

        private void toolStripButton14_Click_1(object sender, EventArgs e)
        {
            /*ПРОДЛЕНИЕ МИГРАЦИОНКИ*/
            fMigrAddEdit fMigrAddEditForm = new fMigrAddEdit("Extend");
            if (fMigrAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadMigr();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            /*ПЕРЕВЫБИРАЕМ ДОКУМЕНТ*/
            if (dgDocSel.SelectedRows.Count != 0)
            {
                DocActive(Convert.ToInt32(dgDocSel.CurrentRow.Cells["id"].Value));
                LoadDoc();
            }
        }

        private void DocActive(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                string sql;
                sql = "UPDATE cmodb.document SET status='N' where id<>@id and contact_id=@contact_id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.document SET status='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                                                                                          
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Основной документ выбран успешно", "Инфа", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при изменении документа: \n" + msg.Message);
                MessageBox.Show("Ошибка при изменении документа: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
           /* if(e.TabPage.Name=="tabDUL" && DULState!="")
            {
                MessageBox.Show("Необходимо сохранить/отменить изменения!", "Инфа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }*/
;        }
       

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
           
                if (dgDocSel.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Удалить выбранный документ?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DocDel(Convert.ToInt32(dgDocSel.CurrentRow.Cells["id"].Value));
                        LoadDoc();
                    }
                }
        }

        private static void DocDel(int docid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);

                sql = "UPDATE cmodb.document SET deleted='Y',status='N',updated=GETDATE(),updated_by=SYSTEM_USER where id=@docid ;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("docid", docid);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при удалении: \n" + msg.Message);
                MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void MigrDel(int migrid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);

                sql = "UPDATE cmodb.migr_card SET deleted='Y',status='N',updated=GETDATE(),updated_by=SYSTEM_USER where id=@migrid ;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("migrid", migrid);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при удалении: \n" + msg.Message);
                MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
           
                if (dgMigrHist.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("Удалить выбранную миграционную карту?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MigrDel(Convert.ToInt32(dgMigrHist.CurrentRow.Cells["id"].Value));
                        LoadMigr();
                    }
                }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            fChildAddEdit fChildAddEditForm = new fChildAddEdit("Add", 0);
            if (fChildAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadChild();
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (dgChild.SelectedRows.Count != 0)
            {
                fChildAddEdit fChildAddEditForm = new fChildAddEdit("Edit", Convert.ToInt32(dgChild.CurrentRow.Cells["id"].Value));
                if (fChildAddEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadChild();
                }
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (dgChild.SelectedRows.Count != 0)
            {

                if (MessageBox.Show("Точно удаляем?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlTransaction transaction = null;
                    SqlCommand cmd;
                    string sql = "";
                    try
                    {
                        int Contact_id = pref.CONTACTID;
                        transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd = new SqlCommand(sql, DB.conn, transaction);
                        sql = "DELETE FROM cmodb.children where id=@id;";
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("id", Convert.ToInt32(dgChild.CurrentRow.Cells["id"].Value));

                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                       // LoadEntry();
                        MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception msg)
                    {
                        if (transaction != null) transaction.Rollback();
                        Console.WriteLine("Ошибка при удалении: \n" + msg.Message);
                        MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadChild();
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            fTeachAddEdit fTeachAddEditForm = new fTeachAddEdit("Add",0);
            if (fTeachAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadTeach();
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (dgTeach.SelectedRows.Count != 0)
            {
                fTeachAddEdit fTeachAddEditForm = new fTeachAddEdit("Edit", Convert.ToInt32(dgTeach.CurrentRow.Cells["id"].Value));
                if (fTeachAddEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadTeach();
                }
            }
        }

        private void btnSpecHist_Click(object sender, EventArgs e)
        {

        }

        private void tabTeachAgree_Enter(object sender, EventArgs e)
        {
            //this.tTeachTotal.Text = "";
            //this.tTeachInd.Text = "";
            //this.tTotalSrok.Text = "";
            //this.tIndSrok.Text = "";
            //this.tYearAmount.Text = "";

            
            dgTeach.Focus();
            LoadTeach();
            LoadAgree();
            LoadExpell();
        }

        

        public void TeachDel(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
               
                sql = "UPDATE cmodb.teach_info set deleted='Y',status='N' where id=@id";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id",id);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Удалено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                
                MessageBox.Show("Ошибка при удалении : \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (dgTeach.SelectedRows.Count != 0)
            {
                TeachDel(Convert.ToInt32(dgTeach.CurrentRow.Cells["id"].Value));
                LoadTeach();
            }
        }

        public void LoadAddr()
        {

            string sql = "select ai.id, CASE WHEN ai.status='Y' THEN 'true' ELSE 'false' END as addrstatus,full_address as \"Адрес регистрации\" from cmodb.address ad " +
             " left join cmodb.addr_inter ai on ai.address_code = ad.code " +
             " where ai.contact_id =@param1 AND ai.deleted='N' " +
              " order by CASE WHEN ai.status IS NULL THEN 1 ELSE 0 END desc,ai.status desc , ai.created DESC";
            dgAddrHist.Columns["addrstatus"].DataPropertyName = "addrstatus";
            dgAddrHist.DataSource = DB.QueryTableMultipleParams(sql, new List<object> { pref.CONTACTID });
            dgAddrHist.Columns["id"].Visible = false;
           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            /*редактируем адрес*/
            fSelectAddress fSelectAddressForm = new fSelectAddress();
            if (fSelectAddressForm.ShowDialog(this) == DialogResult.OK)
            {
                int SelAddrCode;
                SelAddrCode = fSelectAddressForm.SelectedAddressCode;
                //tFullAddress.Text = fSelectAddressForm.SelectedFullAddress;

                SqlTransaction transaction = null;
                SqlCommand cmd;
                string sql;
                try
                {
                    int Contact_id = pref.CONTACTID;
                    transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                    sql = "UPDATE cmodb.addr_inter SET status='N' WHERE contact_id=@contact_id;";
                    cmd = new SqlCommand(sql, DB.conn, transaction);
                    //cmd.Transaction = transaction;
                    cmd.Parameters.Clear();
                   // cmd.Parameters.AddWithValue("status", "Y");
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.ExecuteNonQuery();

                    sql = "INSERT INTO cmodb.addr_inter(contact_id, status, address_code) VALUES (@contact_id, @status,@address_code);";
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("status", "Y");
                    cmd.Parameters.AddWithValue("contact_id", Contact_id);
                    cmd.Parameters.AddWithValue("address_code", SelAddrCode);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();



                    MessageBox.Show("Адрес обновлен успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception msg)
                {
                    if (transaction != null) transaction.Rollback();
                    Console.WriteLine("Ошибка при обновлении: \n" + msg.Message);
                    MessageBox.Show("Ошибка при обновлении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadAddr();
            }

            fSelectAddressForm = null;
        }

        private void tabAddr_Enter(object sender, EventArgs e)
        {
            tabAddr.Focus();
            LoadAddr();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        { 
            if (dgAddrHist.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Удалить адрес?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DelAddr(Convert.ToInt32(dgAddrHist.CurrentRow.Cells["id"].Value));
                    LoadAddr();
                }
            }
        }

        private static void DelAddr(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql;
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                sql = "UPDATE cmodb.addr_inter SET status='N', deleted='Y' WHERE id=@id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
                //cmd.Transaction = transaction;
                cmd.Parameters.Clear();               
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                
                transaction.Commit();

                MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при удалении: \n" + msg.Message);
                MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            //Активация в данном случае нужна только при отчислении
            if (MessageBox.Show("Активация записи нужна только при ОТЧИСЛЕНИИ, подтвердите действие", "Активация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
                /*выбираем карту*/
                if (dgMigrHist.SelectedRows.Count != 0)
            {
                MigrActive(Convert.ToInt32(dgMigrHist.CurrentRow.Cells["id"].Value));
                LoadMigr();
            }
        }

        public void TeachActive(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                string sql;
                sql = "UPDATE cmodb.teach_info SET status='N' where id<>@id and contact_id=@contact_id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.teach_info SET status='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                                                       
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка: \n" + msg.Message);
                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MigrActive(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                string sql;
                sql = "UPDATE cmodb.migr_card SET status='N' where id<>@id and contact_id=@contact_id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.migr_card SET status='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                                                                                            // cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Основная карта выбрана", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при изменении: \n" + msg.Message);
                MessageBox.Show("Ошибка при изменении основной карты: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MigrDeActive(int contactid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                string sql;
                sql = "UPDATE cmodb.migr_card SET status='N' where contact_id=@contact_id and deleted<>'Y';";
                cmd = new SqlCommand(sql, DB.conn, transaction);
                //cmd.Transaction = transaction;
                cmd.Parameters.Clear();               
                cmd.Parameters.AddWithValue("contact_id", contactid);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Активные мигр. карты очищены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();               
                MessageBox.Show("Ошибка деактивации карты: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EntryActive(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);

                string sql;
                sql = "UPDATE cmodb.entry SET status='N' where id<>@id and contact_id=@contact_id;";
                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id
                cmd.Parameters.AddWithValue("contact_id", pref.CONTACTID);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.entry SET status='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id); //берем id                                                     
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Запись активирована", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();                
                MessageBox.Show("Ошибка при изменении : \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (dgTeach.SelectedRows.Count != 0)
            {
                TeachActive(Convert.ToInt32(dgTeach.CurrentRow.Cells["id"].Value));
                LoadTeach();
            }
            
        }

        public void LoadEntry()
        {
            dgEntryHist.SuspendLayout();
            dgEntryHist.Columns["entrystatus"].DataPropertyName = "entrystatus";
            dgEntryHist.DataSource = DB.QueryTableMultipleParams("SELECT  id,case when status='Y' then 'true' else 'false' end as entrystatus,leave_dt,entry_dt, txt, type "+
                " FROM cmodb.entry where contact_id=@param1 and deleted='N' ORDER BY case when status='Y' then '1' else '0' end desc,leave_dt DESC;", new List<object> { pref.CONTACTID });//nulls
            dgEntryHist.Columns["id"].Visible = false;
            dgEntryHist.Columns["entrystatus"].HeaderText = "Основной";
            dgEntryHist.Columns["type"].HeaderText = "Тип";
            dgEntryHist.Columns["entry_dt"].HeaderText = "Дата въезда";
            dgEntryHist.Columns["leave_dt"].HeaderText = "Дата выезда";
            dgEntryHist.Columns["txt"].HeaderText = "Описание";
            dgEntryHist.ResumeLayout();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            
            fEntryAdd fEntryAddForm = new fEntryAdd("ADD",0);
            if (fEntryAddForm.ShowDialog(this) == DialogResult.OK)
            {
                if (fEntryAddForm.EntryState == "зарубеж")
                {
                    //старье
                    //if (MessageBox.Show("Очистить активную миграционную карту?", "Очистка", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    MigrDeActive(pref.CONTACTID);
                    //}
                    // MessageBox.Show("Очистка миграционной карты", "Деактивация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     MigrDeActive(pref.CONTACTID);
                }
                LoadEntry();
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            
            if (dgEntryHist.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    EntryDel(Convert.ToInt32(dgEntryHist.CurrentRow.Cells["id"].Value));
                    LoadEntry();
                }
            }
            
        }

        private static void EntryDel(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                sql = "UPDATE cmodb.entry SET status='N', deleted='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при обновлении: \n" + msg.Message);
                MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            dgEntryHist.Focus();
            LoadEntry();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            fEntryAdd fEntryAddForm = new fEntryAdd("EDIT", Convert.ToInt32(dgEntryHist.CurrentRow.Cells["id"].Value));
            if (fEntryAddForm.ShowDialog(this) == DialogResult.OK)
            {
                /*if (fEntryAddForm.EntryState == "зарубеж")
                {
                    if (MessageBox.Show("Добавить новую миграционную карту?", "Копирование", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        toolStripButton34_Click(this, null);
                    }
                }*/
                LoadEntry();
            }
        }

       

        private void toolStripButton30_Click_1(object sender, EventArgs e)
        {
            /*ДОБАВЛЕНИЕ ДОГОВОРА*/

            fAgreeAddEdit fAgreeAddEditForm = new fAgreeAddEdit("Add",0);
            if (fAgreeAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadAgree();
            }
        }

        private void toolStripButton31_Click_1(object sender, EventArgs e)
        {
            /*РЕДАКТИРОВАНИЕ ДОГОВОРА*/
            fAgreeAddEdit fAgreeAddEditForm = new fAgreeAddEdit("Edit", Convert.ToInt32(dgAgreeHist.CurrentRow.Cells["id"].Value));
            if (fAgreeAddEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadAgree();
            }
        }

        public void AgreeActive(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
               
                sql = "UPDATE cmodb.agree SET status='N' where contact_id=@contact_id and status='Y';";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.ExecuteNonQuery();

                sql = "UPDATE cmodb.agree SET status='Y' where id=@id ;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                transaction.Commit();

                MessageBox.Show("Успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();                
                MessageBox.Show("Ошибка при обновлении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton20_Click_1(object sender, EventArgs e)
        {
            if (dgAgreeHist.SelectedRows.Count != 0)
            {
                AgreeActive(Convert.ToInt32(dgAgreeHist.CurrentRow.Cells["id"].Value));
                LoadAgree();
            }
        }

        public void AgreDel(int id)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);

                sql = "UPDATE cmodb.agree SET status='N',deleted='Y' where id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                                
                transaction.Commit();

                MessageBox.Show("Успешно удалено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                MessageBox.Show("Ошибка при удалении: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            if (dgAgreeHist.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AgreDel(Convert.ToInt32(dgAgreeHist.CurrentRow.Cells["id"].Value));
                    LoadAgree();
                }
            }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            if (dgExpellHist.SelectedRows.Count != 0)
            {
                fExpelledEdit fExpelledEditForm = new fExpelledEdit("EDIT", Convert.ToInt32(dgExpellHist.CurrentRow.Cells["id"].Value));
                if (fExpelledEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadExpell();
                    LoadTeach();
                }
            }
        }

    
        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            fExpelledEdit fExpelledEditForm = new fExpelledEdit("ADD",0);
            if (fExpelledEditForm.ShowDialog(this) == DialogResult.OK)
            {
                LoadExpell();
                LoadTeach();
            }
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {

            if (dgExpellHist.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ExpellDel(Convert.ToInt32(dgExpellHist.CurrentRow.Cells["id"].Value));
                    LoadExpell();
                }
            }
        }
        public void ExpellDel(int id)
        {
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);               
               // cmd.Transaction = transaction;
                sql = "UPDATE cmodb.expell SET deleted='Y',status='N' where  id=@id;";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                transaction.Commit();

                MessageBox.Show("Удалено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка: \n" + msg.Message);
                MessageBox.Show("Ошибка: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            CanEditDelegate(true);
            toolStripButton11.Enabled = false;
            toolStripButton12.Enabled = false;
            toolStripButton13.Enabled = true;
            toolStripButton21.Enabled = true;
        }

        public void DelegateSave()
        {
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql;
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                sql = "UPDATE cmodb.contact " +
                   " SET updated=GETDATE(),updated_by=SYSTEM_USER, " +
                   "delegate_last_name = @delegate_last_name, delegate_first_name = @delegate_first_name, " +
                         " delegate_second_name = @delegate_second_name, delegate_ser = @delegate_ser, delegate_num = @delegate_num, delegate_dul_issue_dt = @delegate_dul_issue_dt, " +
                         " delegate_country = @delegate_country_code, delegate_nationality = @delegate_nationality_code, delegate_dul_code = @delegate_dul_code " +                       
                         " WHERE contact_id =@contact_id and status = 'Y';";

                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);               
                cmd.Parameters.AddWithValue("delegate_last_name", tDelLast.Text);
                cmd.Parameters.AddWithValue("delegate_first_name", tDelFirst.Text);
                cmd.Parameters.AddWithValue("delegate_second_name", tDelSec.Text);
                cmd.Parameters.AddWithValue("delegate_ser", tDelSer.Text);
                cmd.Parameters.AddWithValue("delegate_num", tDelNum.Text);
                if (tDelIssue.SelectedDate == "")
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

                cmd.Parameters.AddWithValue("delegate_dul_code", cmbDelDul.SelectedValue == null ? "" : cmbDelDul.SelectedValue.ToString());

                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Обновлено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Ошибка при редактировании: \n" + msg.Message);
                MessageBox.Show("Ошибка при редактировании: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DelegateClear()
        {
            SqlTransaction transaction = null;

            SqlCommand cmd;
            string sql;
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                sql = "UPDATE cmodb.contact " +
                   " SET updated=GETDATE(),updated_by=SYSTEM_USER, " +
                   "delegate_last_name = null, delegate_first_name = null, " +
                         " delegate_second_name = null, delegate_ser = null, delegate_num = null, delegate_dul_issue_dt = null, " +
                         " delegate_country = null, delegate_nationality = null, delegate_dul_code = null " +
                         " WHERE contact_id =@contact_id and status = 'Y';";

                cmd = new SqlCommand(sql, DB.conn, transaction);
               // cmd.Transaction = transaction;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);             

                cmd.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Очищено успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();                
                MessageBox.Show("Ошибка п: \n" + msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            DelegateSave();

            CanEditDelegate(false);
            toolStripButton11.Enabled = true;
            toolStripButton12.Enabled = true;
            toolStripButton13.Enabled = false;
            toolStripButton21.Enabled = false;
        }

        private void toolStripButton21_Click_1(object sender, EventArgs e)
        {
            LoadConDelegate();
            CanEditDelegate(false);
            toolStripButton11.Enabled = true;
            toolStripButton12.Enabled = true;
            toolStripButton13.Enabled = false;
            toolStripButton21.Enabled = false;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить данные о представителе?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DelegateClear();
                LoadConDelegate();
            }
        }

        private void tabDelChild_Enter(object sender, EventArgs e)
        {
            LoadConDelegate();
            LoadChild();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void cmbNat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);

        
        }

        private void cmbBirthCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void tBirthTown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            //отключено
            if (dgEntryHist.SelectedRows.Count != 0 )
            {
                if (dgEntryHist.CurrentRow.Cells["type"].Value.ToString()!= "зарубеж")
                {
                    MessageBox.Show("Активировать запись с поездкой по России невозможно!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                EntryActive(Convert.ToInt32(dgEntryHist.CurrentRow.Cells["id"].Value));
                LoadEntry();
            }
        }

        

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Подтвердите удаление студента!", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StudDelete(pref.CONTACTID);
                    MessageBox.Show("Успешно удален", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: \n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void StudDelete(int conid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                sql = "UPDATE cmodb.contact SET " +
                   " status='N',updated=GETDATE(),updated_by=SYSTEM_USER " +
                   "  WHERE contact_id = @contact_id; ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", conid);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName + "Function:StudDelete\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            //удаление ПФ
            try { 
                if(dgPf.SelectedRows.Count==1)
                {
                    PfDelete(Convert.ToInt32(dgPf.CurrentRow.Cells["id"].Value));
                    try
                    {
                        //временно отключаю
                        //File.Delete(pref.REPORTFOLDER + pref.CONNAT + "\\" + pref.CONFIO + dgPf.CurrentRow.Cells["name"].Value);
                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                    catch (Exception ex) { }
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                    LoadPf();
                    MessageBox.Show("Успешно удалено", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: \n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PfDelete(int pfid)
        {
            SqlTransaction transaction = null;
            SqlCommand cmd;
            string sql = "";
            try
            {
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new SqlCommand(sql, DB.conn, transaction);
                sql = "DELETE FROM cmodb.pf "+
                   "  WHERE id = @id; ";
                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", pfid);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName + "Function:PfDelete\n Error:" + ex);
                throw new Exception("Ошибка:\n\n" + ex.Message);
            }
        }
    }
}
