using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using Pref;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using iTextSharp.text.pdf;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Reflection;
using WordDoc = Microsoft.Office.Interop.Word;
using ClosedXML.Excel;

namespace Mig
{
    class pf
    {
        public string GETNOW = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string ClassName = "Class: pf.cs\n";
        Object missingObj = System.Reflection.Missing.Value;
        Object trueObj = true;
        Object falseObj = false;
        


        string CapitalizeString(Match matchString)
        {
            string strTemp = matchString.ToString();
            strTemp = char.ToUpper(strTemp[0]) + strTemp.Substring(1, strTemp.Length - 1).ToLower();
            return strTemp;
        }
        public String FirstUpper(String str) /*Один символ после пробела Заглавный*/
        {
            str = str.ToLower();
            string[] s = str.Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1);
                else s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }

        public string InsertPf(string name)
        { 
            string Res = "";
            NpgsqlTransaction transaction = null;
            NpgsqlCommand cmd;
            string sql = "";
            try
            {
                int Contact_id = pref.CONTACTID;
                transaction = DB.conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd = new NpgsqlCommand(sql, DB.conn);     
                sql = "INSERT INTO cmodb.pf(name, contact_id, created, created_by)VALUES(:name, :contact_id, now(), CURRENT_USER);";

                cmd.CommandText = sql;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("contact_id", Contact_id);
                cmd.Parameters.AddWithValue("name", name);              
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception msg)
            {
                if (transaction != null) transaction.Rollback();
                Logger.Log.Error(ClassName+ "Function:InsertPf\n Error:" + msg);
                Res = msg.Message;
            }


            return Res;
        }
        public string GeneratePetitionStandart(string s1,string s2)
        {
            Logger.Log.Debug("Регистрация.Ходатайство.Обычное:" + DateTime.Now.ToString());
            /*Регистрация.Ходатайство.Обычное*/
            string TemplateName = "PETITION.STANDART.doc";
            string TemplatePath = Directory.GetCurrentDirectory()+@"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Ходатайство_Обычное.doc";
            //Word._Application oWord = new Word.Application();
            
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
          //  Logger.Log.Debug("new WordDoc.Application()" + DateTime.Now.ToString());
            application = new WordDoc.Application();
         //   Logger.Log.Debug("new WordDoc.Application() OK" + DateTime.Now.ToString());
            try
            {
               // Logger.Log.Debug("Открываем шаблон - "+ TemplatePath+": " + DateTime.Now.ToString());
                oDoc = application.Documents.Add(TemplatePath);
               // Logger.Log.Debug("Открываем шаблон OK:" + DateTime.Now.ToString());
                oDoc.Bookmarks["change"].Range.Text = s1;
                oDoc.Bookmarks["post"].Range.Text = s2;

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
               // Logger.Log.Debug("Данные получены:" + DateTime.Now.ToString());

                oDoc.Bookmarks["gr"].Range.Text = pfreq.Rows[0]["gr"].ToString();
              //  Logger.Log.Debug("gr " + DateTime.Now.ToString());
                oDoc.Bookmarks["nationality"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
              //  Logger.Log.Debug("nat " + DateTime.Now.ToString());
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper( pfreq.Rows[0]["con_fio"].ToString()) ;
               // Logger.Log.Debug("fio " + DateTime.Now.ToString());
                oDoc.Bookmarks["birthday"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
               // Logger.Log.Debug("др " + DateTime.Now.ToString());
                oDoc.Bookmarks["dul_type"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
               // Logger.Log.Debug("дул " + DateTime.Now.ToString());
                oDoc.Bookmarks["dul_ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
               // Logger.Log.Debug("серия " + DateTime.Now.ToString());
                oDoc.Bookmarks["dul_num"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
               // Logger.Log.Debug("номер " + DateTime.Now.ToString());
                oDoc.Bookmarks["dul_issue"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
               // Logger.Log.Debug("выдано " + DateTime.Now.ToString()); 
                oDoc.Bookmarks["card_tenure_to_dt"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
               // Logger.Log.Debug("мигр до" + DateTime.Now.ToString());
                oDoc.Bookmarks["full_address"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
               // Logger.Log.Debug("адрес " + DateTime.Now.ToString());
              //  Logger.Log.Debug("Создаем папку:" + DateTime.Now.ToString());
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper()+@"\"+pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH+ pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\"+pref.CONFIO + ReportName);   //Путь к заполненному шаблону
               // Logger.Log.Debug("Сохранили отчет:" + DateTime.Now.ToString());
                oDoc.Close();
               // Logger.Log.Debug("Закрыли Doc:" + DateTime.Now.ToString());
                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch(Exception e)  {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionStandart\n Error:" + e);             
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally {                
               
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GenerateDepObr()
        {
            
            /*Уведомление о прибытии*/
            string TemplateName = "DEP.OBR.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление_о_прибытии.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper( pfreq.Rows[0]["con_fio"].ToString());

                
                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)); 
                oDoc.Bookmarks["birth"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["num"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["pfrom"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["pteach"].Range.Text = Regex.Replace(pfreq.Rows[0]["teach_pt"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)); 
                if (pfreq.Rows[0]["con_sex"].ToString() == "МУЖСКОЙ")
                {
                    oDoc.Bookmarks["p1"].Range.Text = "прибыл следующий иностранный гражданин";                   
                }
                else
                {
                    oDoc.Bookmarks["p1"].Range.Text = "прибыла следующая иностранная гражданка";                   
                }               

                             

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH  + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   //Путь к заполненному шаблону
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateDepObr\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
       
        public string GeneratePetitionDeduct()
        {
           
            /*Уведомление об отчислении*/
            string TemplateName = "PETITION.DEDUCT.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление_отчисление_УФМС.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString());
                
                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)); ;
                oDoc.Bookmarks["birth"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["dul"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
                oDoc.Bookmarks["ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["n"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["dul_from"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["pr_num"].Range.Text = pfreq.Rows[0]["exp_num"].ToString();
                oDoc.Bookmarks["pr_from"].Range.Text = pfreq.Rows[0]["exp_dt"].ToString();
                oDoc.Bookmarks["osn"].Range.Text = pfreq.Rows[0]["exp_expelled"].ToString();
                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    oDoc.Bookmarks["p1"].Range.Text = "отчислена";
                    oDoc.Bookmarks["p2"].Range.Text = "следующая иностранная гражданка";
                }
                else
                {
                    oDoc.Bookmarks["p1"].Range.Text = "отчислен";
                    oDoc.Bookmarks["p2"].Range.Text = "следующий иностранный гражданин";
                }



                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   //Путь к заполненному шаблону
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionDeduct\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }

        public string GenerateNotify()
        {
           
            /*Уведомление о прибытии*/
            string TemplateName = "Notify Template.pdf";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string pathFont = Directory.GetCurrentDirectory() + @"\ARIALUNI.ttf";
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление_о_прибытии.pdf";

            DataTable pfhost = DB.QueryTableMultipleParams(pref.PfHost, null);
            if(pfhost.Rows.Count==0)
            {
                return "Нет активного исполнителя!";
            }
            DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
            

            string NewFile = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;


            Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
            try
            {
                // BaseFont baseFont = BaseFont.CreateFont(pathFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(pathFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);

               // File.Copy(TemplatePath, NewFile,true);
                

                PdfReader reader = new PdfReader(TemplatePath);
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(NewFile, FileMode.Create)))
                {
                    AcroFields fields = stamper.AcroFields;

                    fields.AddSubstitutionFont(bf);
                   

                    // set form fields

                    fields.SetField("comb_1", pfreq.Rows[0]["con_last_name"].ToString());
                    fields.SetField("comb_2", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());
                    fields.SetField("comb_3", pfreq.Rows[0]["con_nat"].ToString());
                    if (pfreq.Rows[0]["con_birthday"].ToString() != "")
                    {
                        fields.SetField("comb_4", pfreq.Rows[0]["con_birthday"].ToString().Substring(0, 2));
                        fields.SetField("comb_5", pfreq.Rows[0]["con_birthday"].ToString().Substring(3, 2));
                        fields.SetField("comb_6", pfreq.Rows[0]["con_birthday"].ToString().Substring(6, 4));
                    }
                    if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                        fields.SetField("comb_61", "X");
                    else
                        fields.SetField("comb_60", "X");
                    fields.SetField("comb_8", pfreq.Rows[0]["con_birth_town"].ToString());
                    fields.SetField("comb_7", pfreq.Rows[0]["con_birth_country"].ToString());
                    fields.SetField("comb_9", FirstUpper(pfreq.Rows[0]["dul_type"].ToString().ToUpper()));
                    fields.SetField("comb_10", pfreq.Rows[0]["dul_ser"].ToString());
                    fields.SetField("comb_11", pfreq.Rows[0]["dul_num"].ToString());
                    if (pfreq.Rows[0]["dul_issue"].ToString() != "")
                    {
                        fields.SetField("comb_12", pfreq.Rows[0]["dul_issue"].ToString().Substring(0, 2));
                        fields.SetField("comb_13", pfreq.Rows[0]["dul_issue"].ToString().Substring(3, 2));
                        fields.SetField("comb_14", pfreq.Rows[0]["dul_issue"].ToString().Substring(6, 4));
                    }
                    if (pfreq.Rows[0]["dul_validity"].ToString() != "")
                    {
                        fields.SetField("comb_15", pfreq.Rows[0]["dul_validity"].ToString().Substring(0, 2));
                        fields.SetField("comb_16", pfreq.Rows[0]["dul_validity"].ToString().Substring(3, 2));
                        fields.SetField("comb_17", pfreq.Rows[0]["dul_validity"].ToString().Substring(6, 4));
                    }
                    switch (pfreq.Rows[0]["doc_type"].ToString())
                    {
                        case "Виза":
                            fields.SetField("comb_62", "X");
                            break;
                        case "ВНЖ":
                            fields.SetField("comb_63", "X");
                            break;
                        case "РВП":
                            fields.SetField("comb_64", "X");
                            break;
                    }
                    fields.SetField("comb_18", pfreq.Rows[0]["doc_ser"].ToString());
                    fields.SetField("comb_19", pfreq.Rows[0]["doc_num"].ToString());

                    if (pfreq.Rows[0]["doc_issue_dt"].ToString() != "")
                    {
                        fields.SetField("comb_20", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(0, 2));
                        fields.SetField("comb_21", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(3, 2));
                        fields.SetField("comb_22", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(6, 4));
                    }
                    if (pfreq.Rows[0]["doc_validity_to_dt"].ToString() != "")
                    {
                        fields.SetField("comb_23", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(0, 2));
                        fields.SetField("comb_24", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(3, 2));
                        fields.SetField("comb_25", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(6, 4));
                    }
                    switch (pfreq.Rows[0]["card_purpose_entry"].ToString())
                    {
                        case "СЛУЖЕБНАЯ":
                        fields.SetField("comb_65", "X");
                        break;
                        case "ТУРИЗМ":
                            fields.SetField("comb_66", "X");
                            break;
                        case "ДЕЛОВАЯ":
                            fields.SetField("comb_67", "X");
                            break;
                        case "УЧЕБА":
                            fields.SetField("comb_68", "X");
                            break;
                        case "РАБОТА":
                            fields.SetField("comb_69", "X");
                            break;
                        case "ЧАСТНАЯ":
                            fields.SetField("comb_70", "X");
                            break;
                        case "ТРАНЗИТ":
                            fields.SetField("comb_71", "X");
                            break;
                        case "ГУМАНИТАРНАЯ":
                            fields.SetField("comb_72", "X");
                            break;
                        case "ДРУГАЯ":
                            fields.SetField("comb_73", "X");
                            break;
                    }

                    fields.SetField("comb_26", "");//профессия


                    if (pfreq.Rows[0]["card_entry_dt"].ToString() != "")
                    {
                        fields.SetField("comb_27", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(0, 2));
                        fields.SetField("comb_28", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(3, 2));
                        fields.SetField("comb_29", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(6, 4));
                    }
                    if (pfreq.Rows[0]["card_tenure_to_dt"].ToString() != "")
                    {
                        fields.SetField("comb_30", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(0, 2));
                        fields.SetField("comb_31", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(3, 2));
                        fields.SetField("comb_32", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(6, 4));
                    }
                    fields.SetField("comb_33", pfreq.Rows[0]["card_ser"].ToString());
                    fields.SetField("comb_34", pfreq.Rows[0]["card_num"].ToString());


                    fields.SetField("comb_35", "");//законный представитель
                    fields.SetField("comb_36", "");//законный представитель

                    fields.SetField("comb_37", "");//прежний адрес
                    fields.SetField("comb_38", "");//прежний адрес
                    fields.SetField("comb_39", "");//прежний адрес


                    /*-----------линия отрыва-----------------*/
                    fields.SetField("comb_40", pfreq.Rows[0]["con_last_name"].ToString());
                    fields.SetField("comb_41", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());
                    fields.SetField("comb_42", pfreq.Rows[0]["con_nat"].ToString());
                    if (pfreq.Rows[0]["con_birthday"].ToString() != "")
                    {
                        fields.SetField("comb_43", pfreq.Rows[0]["con_birthday"].ToString().Substring(0, 2));
                        fields.SetField("comb_44", pfreq.Rows[0]["con_birthday"].ToString().Substring(3, 2));
                        fields.SetField("comb_45", pfreq.Rows[0]["con_birthday"].ToString().Substring(6, 4));
                    }
                    if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                        fields.SetField("comb_75", "X");
                    else
                        fields.SetField("comb_74", "X");
                    fields.SetField("comb_46", FirstUpper(pfreq.Rows[0]["dul_type"].ToString()));
                    fields.SetField("comb_47", pfreq.Rows[0]["dul_ser"].ToString());
                    fields.SetField("comb_48", pfreq.Rows[0]["dul_num"].ToString());

                    if (pfreq.Rows[0]["card_tenure_to_dt"].ToString() != "")
                    {
                        fields.SetField("comb_57", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(0, 2));
                        fields.SetField("comb_58", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(3, 2));
                        fields.SetField("comb_59", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(6, 4));
                    }

                    fields.SetField("comb_49", pfreq.Rows[0]["ad_obl"].ToString().ToUpper());
                    fields.SetField("comb_1_2", pfreq.Rows[0]["ad_obl"].ToString().ToUpper());

                    if (pfreq.Rows[0]["ad_socr_rayon"].ToString() != "мкр.")
                    {
                        fields.SetField("comb_50", pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
                        fields.SetField("comb_2_2", pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
                    }
                    else
                    {
                        fields.SetField("comb_50", "МКР. " + pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
                        fields.SetField("comb_2_2", "МКР. " + pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
                    }

                    fields.SetField("comb_51", (pfreq.Rows[0]["ad_socr_town"].ToString()+" "+pfreq.Rows[0]["ad_town"].ToString()).ToUpper());
                    fields.SetField("comb_3_2", (pfreq.Rows[0]["ad_socr_town"].ToString() + " " + pfreq.Rows[0]["ad_town"].ToString()).ToUpper());

                    switch (pfreq.Rows[0]["ad_socr_street"].ToString())
                    {
                        case "пр-кт":
                            fields.SetField("comb_52", "ПРОСПЕКТ "+pfreq.Rows[0]["ad_street"].ToString().ToUpper());
                            fields.SetField("comb_4_2", "ПРОСПЕКТ " + pfreq.Rows[0]["ad_street"].ToString().ToUpper());
                            break;

                        default:
                            fields.SetField("comb_52", pfreq.Rows[0]["ad_socr_street"].ToString().ToUpper()+" "+pfreq.Rows[0]["ad_street"].ToString().ToUpper());
                            fields.SetField("comb_4_2", pfreq.Rows[0]["ad_socr_street"].ToString().ToUpper() + " " + pfreq.Rows[0]["ad_street"].ToString().ToUpper());
                            break;
                    }

                    fields.SetField("comb_53", pfreq.Rows[0]["ad_house"].ToString());
                    fields.SetField("comb_5_2", pfreq.Rows[0]["ad_house"].ToString());
                    fields.SetField("comb_54", pfreq.Rows[0]["ad_corp"].ToString());
                    fields.SetField("comb_6_2", pfreq.Rows[0]["ad_corp"].ToString());
                    fields.SetField("comb_55", pfreq.Rows[0]["ad_stroenie"].ToString());
                    fields.SetField("comb_7_2", pfreq.Rows[0]["ad_stroenie"].ToString());
                    fields.SetField("comb_56", pfreq.Rows[0]["ad_flat"].ToString());
                    fields.SetField("comb_8_2", pfreq.Rows[0]["ad_flat"].ToString());

                    /*Сведения о принимающей стороне*/
                    if (pfhost.Rows[0]["org_phis"].ToString() == "Организация")
                    {
                        fields.SetField("comb_43_2", "X");
                        fields.SetField("comb_44_2", " ");
                    }
                    else
                    {
                        fields.SetField("comb_43_2", " ");
                        fields.SetField("comb_44_2", "X");
                    }
                    fields.SetField("comb_10_2", pfhost.Rows[0]["last_name"].ToString().ToUpper());
                    fields.SetField("comb_14_2", pfhost.Rows[0]["first_name"].ToString().ToUpper() + " "+ pfhost.Rows[0]["second_name"].ToString().ToUpper());

                    if (pfhost.Rows[0]["birthday"].ToString() != "")
                    {
                        fields.SetField("comb_11_2", pfhost.Rows[0]["birthday"].ToString().Substring(0, 2));
                        fields.SetField("comb_12_2", pfhost.Rows[0]["birthday"].ToString().Substring(3, 2));
                        fields.SetField("comb_13_2", pfhost.Rows[0]["birthday"].ToString().Substring(6, 4));
                    }
                    fields.SetField("comb_15_2", pfhost.Rows[0]["doc"].ToString());
                    fields.SetField("comb_16_2", pfhost.Rows[0]["doc_ser"].ToString());
                    fields.SetField("comb_17_2", pfhost.Rows[0]["doc_num"].ToString());
                    if (pfhost.Rows[0]["date_issue"].ToString() != "")
                    {
                        fields.SetField("comb_18_2", pfhost.Rows[0]["date_issue"].ToString().Substring(0, 2));
                        fields.SetField("comb_19_2", pfhost.Rows[0]["date_issue"].ToString().Substring(3, 2));
                        fields.SetField("comb_20_2", pfhost.Rows[0]["date_issue"].ToString().Substring(6, 4));
                    }
                    if (pfhost.Rows[0]["date_valid"].ToString() != "")
                    {
                        fields.SetField("comb_21_2", pfhost.Rows[0]["date_valid"].ToString().Substring(0, 2));
                        fields.SetField("comb_22_2", pfhost.Rows[0]["date_valid"].ToString().Substring(3, 2));
                        fields.SetField("comb_23_2", pfhost.Rows[0]["date_valid"].ToString().Substring(6, 4));
                    }
                    fields.SetField("comb_24_2", pfhost.Rows[0]["obl"].ToString().ToUpper());
                    fields.SetField("comb_25_2", pfhost.Rows[0]["rayon"].ToString().ToUpper());
                    fields.SetField("comb_26_2", pfhost.Rows[0]["town"].ToString().ToUpper());
                    fields.SetField("comb_27_2", pfhost.Rows[0]["street"].ToString().ToUpper());
                    fields.SetField("comb_28_2", pfhost.Rows[0]["house"].ToString().ToUpper());
                    fields.SetField("comb_29_2", pfhost.Rows[0]["korp"].ToString().ToUpper());
                    fields.SetField("comb_30_2", pfhost.Rows[0]["stro"].ToString().ToUpper());
                    fields.SetField("comb_31_2", pfhost.Rows[0]["flat"].ToString().ToUpper());

                    fields.SetField("comb_32_2", pfhost.Rows[0]["phone"].ToString().ToUpper());

                    //перенести  - наименование орг.
                    string tPerenos = pfhost.Rows[0]["org_name"].ToString();
                    string res = "";
                    string res2 = "";
                    if (tPerenos.Length < 24)
                    {
                        res = tPerenos;
                    }
                    else
                    {
                        int i;
                        string stemp = "";
                        stemp = tPerenos.Substring(0, 24);
                        i = stemp.LastIndexOf(" ");
                        res = tPerenos.Substring(0, i + 1);
                        res2 = tPerenos.Substring(i, tPerenos.Length - i);
                    }
                    fields.SetField("comb_33_2", res.ToUpper());
                    fields.SetField("comb_34_2", res2.ToUpper());

                    //адрес
                    tPerenos = pfhost.Rows[0]["address"].ToString();
                    res = "";
                    res2 = "";
                    if (tPerenos.Length < 24)
                    {
                        res = tPerenos;
                    }
                    else
                    {
                        int i;
                        string stemp = "";
                        stemp = tPerenos.Substring(0, 24);
                        i = stemp.LastIndexOf(" ");
                        res = tPerenos.Substring(0, i + 1);
                        res2 = tPerenos.Substring(i, tPerenos.Length - i);
                    }
                    fields.SetField("comb_35_2", res.ToUpper());
                    fields.SetField("comb_36_2", res2.ToUpper());

                    fields.SetField("comb_37_2", pfhost.Rows[0]["inn"].ToString().ToUpper());



                    fields.SetField("comb_41_2", pfreq.Rows[0]["con_last_name"].ToString());
                    fields.SetField("comb_42_2", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());


                    //for (int i = 1; i < 120; i++)
                     //   fields.SetField("comb_" + i.ToString()+"_2", (i).ToString());
                    
                    // stamper.FormFlattening = true;
                    stamper.Close();
                 }
                

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateNotify\n Error:" + e);
                ErrMsg = "Ошибка ^_^";
            }
            finally
            {
                
            }
            return ErrMsg;
        }
        public string GeneratePetitionDeductDo()
        {
            
            /*Уведомление об отчислении*/
            string TemplateName = "PETITION.DEDUCT.DO.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление_отчисление_ДО.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
              
              
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper( pfreq.Rows[0]["con_fio"].ToString());
                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["birth"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["dul"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
                oDoc.Bookmarks["ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["n"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["dul_from"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["pr_num"].Range.Text = pfreq.Rows[0]["exp_num"].ToString();
                oDoc.Bookmarks["pr_from"].Range.Text = pfreq.Rows[0]["exp_dt"].ToString();
                oDoc.Bookmarks["osn"].Range.Text = pfreq.Rows[0]["exp_expelled"].ToString();
                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    oDoc.Bookmarks["p1"].Range.Text = "отчислена";
                    oDoc.Bookmarks["p2"].Range.Text = "следующая иностранная гражданка";
                }
                else
                {
                    oDoc.Bookmarks["p1"].Range.Text = "отчислен";
                    oDoc.Bookmarks["p2"].Range.Text = "следующий иностранный гражданин";
                }



                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   //Путь к заполненному шаблону
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionDeductDo\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = "Ошибка ^_^";
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GenerateSprBank()
        {
            
            /*Справка в банк*/
            string TemplateName = "SPR_TO_BANK.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Справка_в_банк.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });


                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString());
                oDoc.Bookmarks["to"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                oDoc.Bookmarks["addr"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   //Путь к заполненному шаблону
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateSprBank\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = "Ошибка ^_^";
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GeneratePetitionOut(int s1)
        {
            
            /*уведомление об отчислении*/
            string TemplateName = "PETITION.OUT.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление(отчисление)УФМС.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                if(s1 == 0 )
                {
                    oDoc.Bookmarks["p1"].Range.Text = "X";
                    oDoc.Bookmarks["p2"].Range.Text = " ";
                }
                else
                { 
                    oDoc.Bookmarks["p1"].Range.Text = " ";
                    oDoc.Bookmarks["p2"].Range.Text = "X";
                }
                oDoc.Bookmarks["l"].Range.Text = pfreq.Rows[0]["con_last_name"].ToString();
                oDoc.Bookmarks["lu"].Range.Text = pfreq.Rows[0]["con_last_enu"].ToString();
                oDoc.Bookmarks["f"].Range.Text = pfreq.Rows[0]["con_first_name"].ToString();
                oDoc.Bookmarks["fu"].Range.Text = pfreq.Rows[0]["con_first_enu"].ToString();
                oDoc.Bookmarks["m"].Range.Text = pfreq.Rows[0]["con_second_name"].ToString();
                oDoc.Bookmarks["mu"].Range.Text = pfreq.Rows[0]["con_second_enu"].ToString();
                oDoc.Bookmarks["br"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                
                if (pfreq.Rows[0]["con_birth_town"].ToString() == "")
                    oDoc.Bookmarks["brcon"].Range.Text = pfreq.Rows[0]["con_birth_country"].ToString();
                else
                    oDoc.Bookmarks["brcon"].Range.Text = pfreq.Rows[0]["con_birth_town"].ToString()+","+pfreq.Rows[0]["con_birth_country"].ToString();

                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));

                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    oDoc.Bookmarks["p3"].Range.Text = " ";
                    oDoc.Bookmarks["p4"].Range.Text = "X";
                }
                else
                {
                    oDoc.Bookmarks["p3"].Range.Text = "X";
                    oDoc.Bookmarks["p4"].Range.Text = " ";
                }

                oDoc.Bookmarks["dul"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
                oDoc.Bookmarks["ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["num"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["dfr"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["dto"].Range.Text = pfreq.Rows[0]["dul_validity"].ToString();

                oDoc.Bookmarks["addr"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                
                switch(pfreq.Rows[0]["doc_type"].ToString())
                {
                    case "РВП":
                        oDoc.Bookmarks["post"].Range.Text = "РВП";
                        break;
                    case "ВНЖ":
                        oDoc.Bookmarks["post"].Range.Text = "ВНЖ";
                        break;
                    default:
                        oDoc.Bookmarks["post"].Range.Text = "месту пребывания";
                        break;
                }
                oDoc.Bookmarks["mfr"].Range.Text = pfreq.Rows[0]["card_entry_dt"].ToString();
                oDoc.Bookmarks["mto"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                //заполняем только если виза
                if (pfreq.Rows[0]["doc_type"].ToString() == "Виза")
                {
                    oDoc.Bookmarks["kr"].Range.Text = "многократная";
                    oDoc.Bookmarks["cat"].Range.Text = "обыкновенная";
                    switch (pfreq.Rows[0]["con_pos"].ToString())
                    {
                        case "студент":
                            oDoc.Bookmarks["entity"].Range.Text = "учеба";
                            break;
                        case "аспирант":
                            oDoc.Bookmarks["entity"].Range.Text = "аспирантура";
                            break;
                        case "стажер":
                            oDoc.Bookmarks["entity"].Range.Text = "стажировка";
                            break;
                        case "курсант":
                            oDoc.Bookmarks["entity"].Range.Text = "курсы";
                            break;
                        default:
                            oDoc.Bookmarks["entity"].Range.Text = "";
                            break;
                    }
                    oDoc.Bookmarks["vser"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
                    oDoc.Bookmarks["vnum"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();
                    oDoc.Bookmarks["ident"].Range.Text = pfreq.Rows[0]["doc_ident"].ToString();
                    oDoc.Bookmarks["fr"].Range.Text = pfreq.Rows[0]["doc_validity_from_dt"].ToString();
                    oDoc.Bookmarks["to"].Range.Text = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                }
                else
                {
                    oDoc.Bookmarks["kr"].Range.Text = "";
                    oDoc.Bookmarks["cat"].Range.Text = "";
                    oDoc.Bookmarks["entity"].Range.Text = "";
                    oDoc.Bookmarks["vser"].Range.Text = "";
                    oDoc.Bookmarks["vnum"].Range.Text = "";
                    oDoc.Bookmarks["ident"].Range.Text = "";
                    oDoc.Bookmarks["fr"].Range.Text = "";
                    oDoc.Bookmarks["to"].Range.Text = "";
                }

                oDoc.Bookmarks["dd"].Range.Text = pfreq.Rows[0]["agr_dt"].ToString();
                oDoc.Bookmarks["dn"].Range.Text = pfreq.Rows[0]["agr_num"].ToString();
                oDoc.Bookmarks["ddfr"].Range.Text = pfreq.Rows[0]["agr_from_dt"].ToString();
                oDoc.Bookmarks["ddto"].Range.Text = pfreq.Rows[0]["agr_to_dt"].ToString();

                oDoc.Bookmarks["exp"].Range.Text = pfreq.Rows[0]["exp_expelled"].ToString();
                oDoc.Bookmarks["expnum"].Range.Text = pfreq.Rows[0]["exp_num"].ToString();
                oDoc.Bookmarks["expdt"].Range.Text = pfreq.Rows[0]["exp_dt"].ToString();

                if (pfreq.Rows[0]["teach_fp"].ToString() == "НАПРАВЛЕНИЕ")
                    oDoc.Bookmarks["bud"].Range.Text = "(студент обучался по направлению Минобрнауки России)";
                else
                    oDoc.Bookmarks["bud"].Range.Text = "";
               

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   //Путь к заполненному шаблону
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionOut\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg ="Ошибка ^_^";
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GeneratePetitionVisa(string s1, string s2)
        {
            
            /*Регистрация.Ходатайство.Смена визы*/
            string TemplateName = "PETITION.VISA.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Ходатайство_Смена_визы.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);
                oDoc.Bookmarks["change"].Range.Text = s1;
                oDoc.Bookmarks["post"].Range.Text = s2;

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });


                oDoc.Bookmarks["gr"].Range.Text = pfreq.Rows[0]["gr"].ToString();
                oDoc.Bookmarks["nationality"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString()) ;
                oDoc.Bookmarks["birthday"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["doc_ser"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
                oDoc.Bookmarks["doc_num"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();
                oDoc.Bookmarks["doc_issue_dt"].Range.Text = pfreq.Rows[0]["doc_issue_dt"].ToString();
                oDoc.Bookmarks["doc_validity_to_dt"].Range.Text = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                oDoc.Bookmarks["full_address"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                oDoc.Bookmarks["card_tenure_to_dt"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                oDoc.Bookmarks["p3"].Range.Text = pfreq.Rows[0]["p3"].ToString();
                oDoc.Bookmarks["p4"].Range.Text = pfreq.Rows[0]["p4"].ToString();

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);   
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionVisa\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GenerateVisaGuarant()
        {
           
            /*Гарантия*/
            string TemplateName = "VISA.GUARANT.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Гарантия.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                if (pfreq.Rows[0]["con_sex"].ToString() == "МУЖСКОЙ")
                {
                    oDoc.Bookmarks["p1"].Range.Text = "иностранному гражданину";
                    oDoc.Bookmarks["p2"].Range.Text = "его";
                }
                else
                {
                    oDoc.Bookmarks["p1"].Range.Text = "иностранной гражданке";
                    oDoc.Bookmarks["p2"].Range.Text = "её";
                }

                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString());
                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["pass"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString() + pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["pfrom"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["ptill"].Range.Text = pfreq.Rows[0]["dul_validity"].ToString();
                oDoc.Bookmarks["med"].Range.Text = pfreq.Rows[0]["con_med"].ToString();                
                oDoc.Bookmarks["addr"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaGuarant\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GenerateVisaPetition(string s1,string s2)
        {
           
            /*Ходатайство на визу*/
            string TemplateName = "";
            string TemplatePath = "";
            string ErrMsg = "";
            string ReportName = GETNOW + "_Ходатайство_на_визу.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                if (pfreq.Rows[0]["teach_fp"].ToString() == "НАПРАВЛЕНИЕ")
                    TemplateName = "VISA.PETITION.BUDGET.doc";
                else
                    TemplateName = "VISA.PETITION.CONTRACT.doc";
                TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
                oDoc = application.Documents.Add(TemplatePath);

                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString());
                oDoc.Bookmarks["p1"].Range.Text = s1;
                oDoc.Bookmarks["p2"].Range.Text = s2;
                oDoc.Bookmarks["birthday"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                if (pfreq.Rows[0]["con_sex"].ToString()=="МУЖСКОЙ")
                {
                    oDoc.Bookmarks["gr"].Range.Text = "гражданину";
                    oDoc.Bookmarks["p3"].Range.Text = "поставлен";
                    oDoc.Bookmarks["p4"].Range.Text = "имеющему";
                    oDoc.Bookmarks["p5"].Range.Text = "прибывшему";
                }
                else
                {
                    oDoc.Bookmarks["gr"].Range.Text = "гражданке";
                    oDoc.Bookmarks["p3"].Range.Text = "поставлена";
                    oDoc.Bookmarks["p4"].Range.Text = "имеющей";
                    oDoc.Bookmarks["p5"].Range.Text = "прибывшей";
                }
                
                oDoc.Bookmarks["nat"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["dul"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
                oDoc.Bookmarks["pass"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString() + pfreq.Rows[0]["dul_num"].ToString();

                string dul_issue = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["d1"].Range.Text = dul_issue.Substring(0, 2);
                oDoc.Bookmarks["d2"].Range.Text = GetMonthPad(dul_issue.Substring(3, 2)); 
                oDoc.Bookmarks["d3"].Range.Text = dul_issue.Substring(6, 4);
                string dul_validity = pfreq.Rows[0]["dul_validity"].ToString();
                oDoc.Bookmarks["d4"].Range.Text = dul_validity.Substring(0, 2);
                oDoc.Bookmarks["d5"].Range.Text = GetMonthPad(dul_validity.Substring(3, 2));
                oDoc.Bookmarks["d6"].Range.Text = dul_validity.Substring(6, 4);

                oDoc.Bookmarks["addr"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                oDoc.Bookmarks["kpp"].Range.Text = pfreq.Rows[0]["card_kpp"].ToString();
                oDoc.Bookmarks["vser"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
                oDoc.Bookmarks["vnum"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();

                string doc_validity_from_dt = pfreq.Rows[0]["doc_validity_from_dt"].ToString();
                oDoc.Bookmarks["v1"].Range.Text = doc_validity_from_dt.Substring(0, 2);
                oDoc.Bookmarks["v2"].Range.Text = GetMonthPad(doc_validity_from_dt.Substring(3, 2));
                oDoc.Bookmarks["v3"].Range.Text = doc_validity_from_dt.Substring(6, 4);
                string doc_validity_to_dt = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                oDoc.Bookmarks["v4"].Range.Text = doc_validity_to_dt.Substring(0, 2);
                oDoc.Bookmarks["v5"].Range.Text = GetMonthPad(doc_validity_to_dt.Substring(3, 2));
                oDoc.Bookmarks["v6"].Range.Text = doc_validity_to_dt.Substring(6, 4);

                string doc_validity_to_dt_1 = pfreq.Rows[0]["doc_validity_to_dt_1"].ToString();
                oDoc.Bookmarks["s1"].Range.Text = doc_validity_to_dt_1.Substring(0, 2);
                oDoc.Bookmarks["s2"].Range.Text = GetMonthPad(doc_validity_to_dt_1.Substring(3, 2));
                oDoc.Bookmarks["s3"].Range.Text = doc_validity_to_dt_1.Substring(6, 4);

                string card_tenure_to_dt = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                oDoc.Bookmarks["m1"].Range.Text = card_tenure_to_dt.Substring(0, 2);
                oDoc.Bookmarks["m2"].Range.Text = GetMonthPad(card_tenure_to_dt.Substring(3, 2));
                oDoc.Bookmarks["m3"].Range.Text = card_tenure_to_dt.Substring(6, 4);

                oDoc.Bookmarks["mfrom"].Range.Text = pfreq.Rows[0]["card_entry_dt"].ToString();
                       
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaPetition\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GetMonthPad(string m)
        {
            string Res = "";
            
            switch (m)
            {
                case "01":
                    Res = "января";
                break;
                case "02":
                    Res = "февраля";
                    break;
                case "03":
                    Res = "марта";
                    break;
                case "04":
                    Res = "апреля";
                    break;
                case "05":
                    Res = "мая";
                    break;
                case "06":
                    Res = "июня";
                    break;
                case "07":
                    Res = "июля";
                    break;
                case "08":
                    Res = "августа";
                    break;
                case "09":
                    Res = "сентября";
                    break;
                case "10":
                    Res = "октября";
                    break;
                case "11":
                    Res = "ноября";
                    break;
                case "12":
                    Res = "декабря";
                    break;

            }
                return Res;
        }
        public string GeneratePetitionPassport(string s1, string s2)
        {
            
            /*Регистрация.Ходатайство.Смена паспорта*/
            string TemplateName = "PETITION.PASSPORT.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Ходатайство_Смена_паспорта.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);
                oDoc.Bookmarks["change"].Range.Text = s1;
                oDoc.Bookmarks["post"].Range.Text = s2;

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });

                
                oDoc.Bookmarks["gr"].Range.Text = pfreq.Rows[0]["gr"].ToString();
                oDoc.Bookmarks["nationality"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString()); 
               // oDoc.Bookmarks["birthday"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["dul_ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["dul_num"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["dul_issue"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["dul_validity"].Range.Text = pfreq.Rows[0]["dul_validity"].ToString();
                oDoc.Bookmarks["doc_ser"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
                oDoc.Bookmarks["doc_num"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();
                oDoc.Bookmarks["doc_issue_dt"].Range.Text = pfreq.Rows[0]["doc_issue_dt"].ToString();
                oDoc.Bookmarks["doc_validity_to_dt"].Range.Text = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                oDoc.Bookmarks["full_address"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                oDoc.Bookmarks["card_tenure_to_dt"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                oDoc.Bookmarks["p3"].Range.Text = pfreq.Rows[0]["p3"].ToString();
                oDoc.Bookmarks["p4"].Range.Text = pfreq.Rows[0]["p4"].ToString();

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionPassport\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GeneratePetitionLost()
        {
            
            /*Регистрация.Ходатайство.Утеря уведомления*/
            string TemplateName = "ARRIVAL.LOST.doc";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
            string ErrMsg = "";
            string ReportName = GETNOW + "_Ходатайство_Утеря_уведомления.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {
                oDoc = application.Documents.Add(TemplatePath);
                // oDoc.Bookmarks["change"].Range.Text = s1;
                // oDoc.Bookmarks["post"].Range.Text = s2;

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });


                oDoc.Bookmarks["gr"].Range.Text = pfreq.Rows[0]["gr"].ToString();
                oDoc.Bookmarks["nationality"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["fio"].Range.Text = FirstUpper(pfreq.Rows[0]["con_fio"].ToString());
                oDoc.Bookmarks["birthday"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                oDoc.Bookmarks["dul_type"].Range.Text = FirstUpper(pfreq.Rows[0]["dul_type"].ToString());
                oDoc.Bookmarks["dul_ser"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["dul_num"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["dul_issue"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
               // oDoc.Bookmarks["dul_validity"].Range.Text = pfreq.Rows[0]["dul_validity"].ToString();
                //oDoc.Bookmarks["doc_ser"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
               // oDoc.Bookmarks["doc_num"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();
               // oDoc.Bookmarks["doc_issue_dt"].Range.Text = pfreq.Rows[0]["doc_issue_dt"].ToString();
               // oDoc.Bookmarks["doc_validity_to_dt"].Range.Text = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                oDoc.Bookmarks["full_address"].Range.Text = pfreq.Rows[0]["ad_full_address"].ToString();
                oDoc.Bookmarks["card_entry_dt"].Range.Text = pfreq.Rows[0]["card_entry_dt"].ToString();
                oDoc.Bookmarks["card_tenure_to_dt"].Range.Text = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                // oDoc.Bookmarks["p3"].Range.Text = pfreq.Rows[0]["p3"].ToString();
                // oDoc.Bookmarks["p4"].Range.Text = pfreq.Rows[0]["p4"].ToString();

                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionLost\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }
        public string GenerateVisaAnketa(string s1, string s2, string s3, string s4, string s5)
        {
            
            /*Визовая анкета: анкета*/

            string TemplateName = "";           
            string TemplatePath ="";
            string ErrMsg = "";
            string ReportName = GETNOW + "_Визовая_анкета.doc";
            WordDoc._Application application;
            WordDoc._Document oDoc = null;
            application = new WordDoc.Application();
            try
            {

                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                if (pfreq.Rows[0]["teach_fp"].ToString() == "НАПРАВЛЕНИЕ")
                    TemplateName = "Anketa_byudzhet.doc";
                else
                    TemplateName = "Anketa_kontract.doc";
                TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
                oDoc = application.Documents.Add(TemplatePath);


                switch (s1)
                {   //оформить
                    case "0":
                        oDoc.Bookmarks["p43"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p44"].Range.Text = "оформить";
                        oDoc.Bookmarks["p45"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p45"].Range.Text = "продлить";
                        oDoc.Bookmarks["p46"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p47"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p47"].Range.Text = "восстановить";
                        oDoc.Bookmarks["p48"].Range.Text = string.Empty;
                        break;
                    //продлить
                    case "1":
                        oDoc.Bookmarks["p43"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p43"].Range.Text = "оформить";
                        oDoc.Bookmarks["p44"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p45"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p46"].Range.Text = "продлить";
                        oDoc.Bookmarks["p47"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p47"].Range.Text = "восстановить";
                        oDoc.Bookmarks["p48"].Range.Text = string.Empty;
                        break;
                    //восстановить
                    case "2":
                        oDoc.Bookmarks["p43"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p43"].Range.Text = "оформить";                       
                        oDoc.Bookmarks["p44"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p45"].Range.Font.StrikeThrough = 1;
                        oDoc.Bookmarks["p45"].Range.Text = "продлить";                       
                        oDoc.Bookmarks["p46"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p47"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p48"].Range.Text = "восстановить";
                        
                        break;
                }
                oDoc.Bookmarks["p15"].Range.Text = pfreq.Rows[0]["con_last_name"].ToString();
                oDoc.Bookmarks["p16"].Range.Text = pfreq.Rows[0]["con_last_enu"].ToString();
                oDoc.Bookmarks["p17"].Range.Text = pfreq.Rows[0]["con_first_name"].ToString();
                oDoc.Bookmarks["p18"].Range.Text = pfreq.Rows[0]["con_first_enu"].ToString();
                if (pfreq.Rows[0]["con_second_name"].ToString() != "")
                    oDoc.Bookmarks["p19"].Range.Text = pfreq.Rows[0]["con_second_name"].ToString();
                else
                    oDoc.Bookmarks["p19"].Range.Text = string.Empty;
                if (pfreq.Rows[0]["con_second_enu"].ToString() != "")
                    oDoc.Bookmarks["p20"].Range.Text = pfreq.Rows[0]["con_second_enu"].ToString();
                else
                    oDoc.Bookmarks["p20"].Range.Text = string.Empty;

                oDoc.Bookmarks["p41"].Range.Text = s5;/*цель продления*/

                switch (s2)
                {   
                    case "Однократная":
                        oDoc.Bookmarks["p1"].Range.Text = "X";
                        oDoc.Bookmarks["p2"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p3"].Range.Text = string.Empty;
                        break;
                    case "Двукратная":
                        oDoc.Bookmarks["p1"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p2"].Range.Text = "X";
                        oDoc.Bookmarks["p3"].Range.Text = string.Empty;
                        break;
                    case "Многократная":
                        oDoc.Bookmarks["p1"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p2"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p3"].Range.Text = "X";
                        break;
                }
                switch (s3) /*категория*/
                {
                    case "0":
                        oDoc.Bookmarks["p4"].Range.Text = "X";
                        oDoc.Bookmarks["p5"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p6"].Range.Text = string.Empty;
                        break;
                    case "1":
                        oDoc.Bookmarks["p4"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p5"].Range.Text = "X";
                        oDoc.Bookmarks["p6"].Range.Text = string.Empty;
                        break;
                    case "2":
                        oDoc.Bookmarks["p4"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p5"].Range.Text = string.Empty;
                        oDoc.Bookmarks["p6"].Range.Text = "X";
                        break;
                }
                

                if (s3 == "0") /*если обыкновенная*/
                {
                    switch (s4) /*подкатегория*/
                    {
                        case "0":
                            oDoc.Bookmarks["p7"].Range.Text = "X";
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "1":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = "X";
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "2":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = "X";
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "3":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = "X";
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "4":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = "X";
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "5":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = "X";
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "6":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = "X";
                            oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                            break;
                        case "7":
                            oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                            oDoc.Bookmarks["p14"].Range.Text = "X";
                            break;
                    }
                }
                else
                {
                    oDoc.Bookmarks["p7"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p8"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p9"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p10"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p11"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p12"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p13"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p14"].Range.Text = string.Empty;
                }

                oDoc.Bookmarks["p21"].Range.Text = pfreq.Rows[0]["con_birthday"].ToString();
                //Regex.Replace(StringToCap, @"\w+", new MatchEvaluator(CapitalizeString))

                string con_birth_country = Regex.Replace(pfreq.Rows[0]["con_birth_country"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                string con_birth_town = Regex.Replace(pfreq.Rows[0]["con_birth_town"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                if (con_birth_town == "")
                    oDoc.Bookmarks["p22"].Range.Text = con_birth_country;
                else
                    oDoc.Bookmarks["p22"].Range.Text = con_birth_country + "," + con_birth_town;

                if (pfreq.Rows[0]["con_sex"].ToString()=="МУЖСКОЙ")
                {
                    oDoc.Bookmarks["p28"].Range.Text = "X";
                    oDoc.Bookmarks["p29"].Range.Text = string.Empty;
                }
                else
                {
                    oDoc.Bookmarks["p28"].Range.Text = string.Empty;
                    oDoc.Bookmarks["p29"].Range.Text = "X";
                }

                oDoc.Bookmarks["p23"].Range.Text = Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                oDoc.Bookmarks["p40"].Range.Text = pfreq.Rows[0]["dul_type"].ToString().ToLower();
                oDoc.Bookmarks["p24"].Range.Text = pfreq.Rows[0]["dul_ser"].ToString();
                oDoc.Bookmarks["p25"].Range.Text = pfreq.Rows[0]["dul_num"].ToString();
                oDoc.Bookmarks["p26"].Range.Text = pfreq.Rows[0]["dul_issue"].ToString();
                oDoc.Bookmarks["p27"].Range.Text = pfreq.Rows[0]["dul_validity"].ToString();
                oDoc.Bookmarks["p30"].Range.Text = "г. Владимир";

                TextBox tb = new TextBox();
                tb.WordWrap = true;
                tb.Multiline = true;
                tb.Text = pfreq.Rows[0]["con_address_home"].ToString();

                /*--------------------------------*/
                string tPerenos = pfreq.Rows[0]["con_address_home"].ToString();
                string res = "";
                string res2 = "";              
                if (tPerenos.Length < 40)
                {
                    res = tPerenos;
                }
                else
                {
                    int i;
                    string stemp = "";
                    stemp = tPerenos.Substring(0, 40);
                    i = stemp.LastIndexOf(" ");
                    res = tPerenos.Substring(0, i + 1);
                    res2 = tPerenos.Substring(i, tPerenos.Length - i);
                }
                oDoc.Bookmarks["p31"].Range.Text = res;                               
                oDoc.Bookmarks["p42"].Range.Text = res2;

                oDoc.Bookmarks["p32"].Range.Text = pfreq.Rows[0]["con_pos"].ToString();
                /*--------------------------------*/
                tPerenos = pfreq.Rows[0]["con_relatives"].ToString();
                res = "";
                res2 = "";               
                if (tPerenos.Length < 80)
                {
                    res = tPerenos;
                }
                else
                {
                    int i;
                    string stemp = "";
                    stemp = tPerenos.Substring(0, 80);
                    i = stemp.LastIndexOf(" ");
                    res = tPerenos.Substring(0, i + 1);
                    res2 = tPerenos.Substring(i, tPerenos.Length - i);
                }
                oDoc.Bookmarks["p49"].Range.Text = res;
                oDoc.Bookmarks["p50"].Range.Text = res2;
                /*--------------------------------*/
                //адрес проживания
                tPerenos = pfreq.Rows[0]["ad_full_address"].ToString();
                res = "";
                res2 = "";
                if (tPerenos.Length < 30)
                {
                    res = tPerenos;
                }
                else
                {
                    int i;
                    string stemp = "";
                    stemp = tPerenos.Substring(0, 30);
                    i = stemp.LastIndexOf(" ");
                    res = tPerenos.Substring(0, i + 1);
                    res2 = tPerenos.Substring(i, tPerenos.Length - i);
                }
                oDoc.Bookmarks["ADDR1"].Range.Text = res;
                oDoc.Bookmarks["ADDR2"].Range.Text = res2;
                /*--------------------------------*/
                oDoc.Bookmarks["p34"].Range.Text = pfreq.Rows[0]["doc_ser"].ToString();
                oDoc.Bookmarks["p35"].Range.Text = pfreq.Rows[0]["doc_num"].ToString();
                oDoc.Bookmarks["p36"].Range.Text = pfreq.Rows[0]["doc_ident"].ToString();
                oDoc.Bookmarks["p37"].Range.Text = pfreq.Rows[0]["doc_validity_from_dt"].ToString();
                oDoc.Bookmarks["p38"].Range.Text = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                oDoc.Bookmarks["p39"].Range.Text = pfreq.Rows[0]["doc_invite_num"].ToString();

                if (DB.GetTableValue("select count(*) from cmodb.children where contact_id=:param1;", new List<object> { pref.CONTACTID }) != "0")
                {

                    int ii = 0;
                    foreach (DataRow row in DB.QueryTableMultipleParams(pref.GetChildSql, new List<object> { pref.CONTACTID }).Rows)
                    {                       
                        ii++;
                        oDoc.Bookmarks["d" + ii.ToString() + "f"].Range.Text = row.ItemArray[0].ToString();
                        oDoc.Bookmarks["d" + ii.ToString() + "d"].Range.Text = row.ItemArray[1].ToString();
                        oDoc.Bookmarks["d" + ii.ToString() + "g"].Range.Text = row.ItemArray[4].ToString();
                        oDoc.Bookmarks["d" + ii.ToString() + "a"].Range.Text = row.ItemArray[2].ToString();

                    }
                    if (ii != 4)
                    {
                        if (ii == 0) ii = 1;
                        for (int k = ii + 1; k <= 4; k++)
                        {
                            oDoc.Bookmarks["d" + k.ToString() + "f"].Range.Text = String.Empty;
                            oDoc.Bookmarks["d" + k.ToString() + "d"].Range.Text = String.Empty;
                            oDoc.Bookmarks["d" + k.ToString() + "g"].Range.Text = String.Empty;
                            oDoc.Bookmarks["d" + k.ToString() + "a"].Range.Text = String.Empty;
                        }
                    }
                }
                else
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        oDoc.Bookmarks["d" + k.ToString() + "f"].Range.Text = String.Empty;
                        oDoc.Bookmarks["d" + k.ToString() + "d"].Range.Text = String.Empty;
                        oDoc.Bookmarks["d" + k.ToString() + "g"].Range.Text = String.Empty;
                        oDoc.Bookmarks["d" + k.ToString() + "a"].Range.Text = String.Empty;
                    }
                }
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                oDoc.SaveAs(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName);
                oDoc.Close();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaAnketa\n Error:" + e);
                oDoc.Close(ref falseObj, ref missingObj, ref missingObj);
                ErrMsg = e.Message;
            }
            finally
            {
                oDoc = null;
                application.Quit(SaveChanges: false);
                application = null;
            }
            return ErrMsg;
        }

      

        
        public string GenerateNotifyXls()
        {
           
            /*Уведомление о прибытии*/
            string TemplateName = "Notify Template.xlsx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
           // string pathFont = Directory.GetCurrentDirectory() + "\\ARIALUNI.ttf";
            string ErrMsg = "";
            string ReportName = GETNOW + "_Уведомление_о_прибытии.xlsx";

            DataTable pfhost = DB.QueryTableMultipleParams(pref.PfHost, null);
            if (pfhost.Rows.Count == 0)
            {
                return "Нет активного исполнителя!";
            }
            DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });

            string NewFile = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;

            Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
            File.Copy(TemplatePath, NewFile, true);

            //Excel.Application excelApp = new Excel.Application();            
            //excelApp.DisplayAlerts = false;
            //excelApp.Visible = false;            
            //Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(TemplatePath,
            //    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
            //    true, false, 0, true, false, false);
            //Logger.Log.Debug("Шаблон открыт:" + DateTime.Now.ToString());
            //Excel.Sheets excelSheets = excelWorkbook.Worksheets;
            XLWorkbook excelApp1XML = new ClosedXML.Excel.XLWorkbook(NewFile);
            string currentSheet = "стр.1";
            string currentSheet2 = "стр.2";
            //Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(currentSheet);
            //Excel.Worksheet excelWorksheet2 = (Excel.Worksheet)excelSheets.get_Item(currentSheet2);
            IXLWorksheet sheetExcel1XML = excelApp1XML.Worksheet(currentSheet);
            IXLWorksheet sheetExcel2XML = excelApp1XML.Worksheet(currentSheet2);

            try
            {                
               
                string str="";
                /*
                for (int i = 1; i <= str.Length; i++)
                    sheetExcel1XML.Range(arAdr[i]).Value = str[i];*/
                //фамилия
                str = pfreq.Rows[0]["con_last_name"].ToString();
                for (int i = 0; i < str.Length; i++)
                {                    
                    sheetExcel1XML.Range(arName[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arNameB[i]).Value = str[i].ToString();                    
                }
                str = "";
                //имя +отчество
                str = pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arLastName[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arLastNameB[i]).Value = str[i].ToString();
                }
                str = "";
                //гражданство
                str = pfreq.Rows[0]["con_nat"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arPoddan[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arPoddanB[i]).Value = str[i].ToString();
                }
                str = "";

                //дата рождения
                str = pfreq.Rows[0]["con_birthday"].ToString();
                if (str!="")
                { 
                    sheetExcel1XML.Range(arDateBirth[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arDateBirth[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arDateBirth[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arDateBirth[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arDateBirth[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arDateBirth[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arDateBirth[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arDateBirth[7]).Value = str[9].ToString();
                    //----------------------
                    sheetExcel1XML.Range(arDateBirthB[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arDateBirthB[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arDateBirthB[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arDateBirthB[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arDateBirthB[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arDateBirthB[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arDateBirthB[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arDateBirthB[7]).Value = str[9].ToString();
                   
                }
                str = "";
                //пол
                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    sheetExcel1XML.Range(arSex[1]).Value = 'X'.ToString();
                    sheetExcel1XML.Range(arSexB[1]).Value = 'X'.ToString();
                }
                else
                {
                    sheetExcel1XML.Range(arSex[0]).Value = 'X'.ToString();
                    sheetExcel1XML.Range(arSexB[0]).Value = 'X'.ToString();
                }

                //страна рождения
                str = pfreq.Rows[0]["con_birth_country"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arCountryBirth[i]).Value = str[i].ToString();                    
                }
                str = "";

                //город рождения
                str = pfreq.Rows[0]["con_birth_town"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arTownBirth[i]).Value = str[i].ToString();
                }
                str = "";

                //документ
                str = pfreq.Rows[0]["dul_type"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arDocView[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arDocViewB[i]).Value = str[i].ToString();
                }
                str = "";

                //серия
                str = pfreq.Rows[0]["dul_ser"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arDocSer[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arDocSerB[i]).Value = str[i].ToString();
                }
                str = "";

                //номер
                str = pfreq.Rows[0]["dul_num"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arDocNum[i]).Value = str[i].ToString();
                    sheetExcel1XML.Range(arDocNumB[i]).Value = str[i].ToString();
                }
                str = "";

                //дата выдачи ДУЛ
                str = pfreq.Rows[0]["dul_issue"].ToString();
                if ( str != "")
                {
                    sheetExcel1XML.Range(arDateVid[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arDateVid[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arDateVid[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arDateVid[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arDateVid[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arDateVid[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arDateVid[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arDateVid[7]).Value = str[9].ToString();                    
                }
                str = "";

                //срок действия ДУЛ
                str = pfreq.Rows[0]["dul_validity"].ToString();
                if (str != "")
                {
                    sheetExcel1XML.Range(arDateExpired[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arDateExpired[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arDateExpired[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arDateExpired[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arDateExpired[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arDateExpired[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arDateExpired[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arDateExpired[7]).Value = str[9].ToString();
                }
                str = "";

                //документ
                switch (pfreq.Rows[0]["doc_type"].ToString())
                {
                    case "Виза":
                        sheetExcel1XML.Range("W37").Value = 'X'.ToString();
                        sheetExcel1XML.Range("AY37").Value = String.Empty;
                        sheetExcel1XML.Range("CM37").Value = String.Empty;
                        break;
                    case "ВНЖ":
                        sheetExcel1XML.Range("W37").Value = String.Empty;
                        sheetExcel1XML.Range("AY37").Value = 'X'.ToString();
                        sheetExcel1XML.Range("CM37").Value = String.Empty;
                        break;
                    case "РВП":
                        sheetExcel1XML.Range("W37").Value = String.Empty;
                        sheetExcel1XML.Range("AY37").Value = String.Empty;
                        sheetExcel1XML.Range("CM37").Value = 'X'.ToString();
                        break;
                }


                //серия
                str = pfreq.Rows[0]["doc_ser"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arVisDocSer[i]).Value = str[i].ToString();                   
                }
                str = "";


                //номер
                str = pfreq.Rows[0]["doc_num"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arVisDocNum[i]).Value = str[i].ToString();
                }
                str = "";

                //дата выдачи
                str = pfreq.Rows[0]["doc_issue_dt"].ToString();
                if (str != "")
                {
                    sheetExcel1XML.Range(arVisDateVid[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arVisDateVid[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arVisDateVid[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arVisDateVid[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arVisDateVid[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arVisDateVid[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arVisDateVid[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arVisDateVid[7]).Value = str[9].ToString();
                }
                str = "";

                //срок действия
                str = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                if (str != "")
                {
                    sheetExcel1XML.Range(arVisDateExpired[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arVisDateExpired[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arVisDateExpired[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arVisDateExpired[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arVisDateExpired[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arVisDateExpired[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arVisDateExpired[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arVisDateExpired[7]).Value = str[9].ToString();
                }
                str = "";

                //цель въезда
                switch (pfreq.Rows[0]["card_purpose_entry"].ToString())
                {
                    case "СЛУЖЕБНАЯ":
                        sheetExcel1XML.Range("AM43").Value = 'X'.ToString();
                        break;
                    case "ТУРИЗМ":
                        sheetExcel1XML.Range("AY43").Value = 'X'.ToString();
                        break;
                    case "ДЕЛОВАЯ":
                        sheetExcel1XML.Range("BO43").Value = 'X'.ToString();
                        break;
                    case "УЧЕБА":
                        sheetExcel1XML.Range("CA43").Value = 'X'.ToString();
                        break;
                    case "РАБОТА":
                        sheetExcel1XML.Range("CM43").Value = 'X'.ToString();
                        break;
                    case "ЧАСТНАЯ":
                        sheetExcel1XML.Range("CY43").Value = 'X'.ToString();
                        break;
                    case "ТРАНЗИТ":
                        sheetExcel1XML.Range("DK43").Value = 'X'.ToString();
                        break;
                    case "ГУМАНИТАРНАЯ":
                        sheetExcel1XML.Range("EE43").Value = 'X'.ToString();
                        break;
                    case "ДРУГАЯ":
                        sheetExcel1XML.Range("EQ43").Value = 'X'.ToString();
                        break;
                }

                //профессия (пока пусто)
                str = "";
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arProf[i]).Value = str[i].ToString();
                }
                str = "";

                //дата въезда
                str = pfreq.Rows[0]["card_entry_dt"].ToString();
                if (str != "")
                {
                    sheetExcel1XML.Range(arEnt[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arEnt[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arEnt[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arEnt[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arEnt[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arEnt[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arEnt[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arEnt[7]).Value = str[9].ToString();
                }
                str = "";

                //срок пребывания по
                str = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                if (str != "")
                {
                    sheetExcel1XML.Range(arTenure[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arTenure[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arTenure[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arTenure[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arTenure[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arTenure[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arTenure[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arTenure[7]).Value = str[9].ToString();
                    


                    sheetExcel1XML.Range(arTenureB[0]).Value = str[0].ToString();
                    sheetExcel1XML.Range(arTenureB[1]).Value = str[1].ToString();

                    sheetExcel1XML.Range(arTenureB[2]).Value = str[3].ToString();
                    sheetExcel1XML.Range(arTenureB[3]).Value = str[4].ToString();

                    sheetExcel1XML.Range(arTenureB[4]).Value = str[6].ToString();
                    sheetExcel1XML.Range(arTenureB[5]).Value = str[7].ToString();
                    sheetExcel1XML.Range(arTenureB[6]).Value = str[8].ToString();
                    sheetExcel1XML.Range(arTenureB[7]).Value = str[9].ToString();

                }
                str = "";

                //серия
                str = pfreq.Rows[0]["card_ser"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arMigrSer[i]).Value = str[i].ToString();
                }
                str = "";

                //номер
                str = pfreq.Rows[0]["card_num"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arMigrNum[i]).Value = str[i].ToString();
                }
                str = "";

                //законный представитель
                str = "";
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arSved[i]).Value = str[i].ToString();
                }
                str = "";
                //прежний адрес
                str = "";
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arAdr[i]).Value = str[i].ToString();
                }
                str = "";
                /*-----------линия отрыва-----------------*/

                //область
                str = pfreq.Rows[0]["ad_obl"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arOblB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arObl2[i]).Value = str[i].ToString();
                    
                }
                str = "";

                //район                
                if (pfreq.Rows[0]["ad_socr_rayon"].ToString() != "мкр.")
                {
                    str = pfreq.Rows[0]["ad_rayon"].ToString().ToUpper();
                    for (int i = 0; i < str.Length; i++)
                    {
                        sheetExcel1XML.Range(arRayonB[i]).Value = str[i].ToString();
                        sheetExcel2XML.Range(arRayon2[i]).Value = str[i].ToString();

                    }                   
                }
                else
                {
                    str = "МКР. " + pfreq.Rows[0]["ad_rayon"].ToString().ToUpper();
                    for (int i = 0; i < str.Length; i++)
                    {
                        sheetExcel1XML.Range(arRayonB[i]).Value = str[i].ToString();
                        sheetExcel2XML.Range(arRayon2[i]).Value = str[i].ToString();

                    }                   
                }
                str = "";


                //город/населенный пункт
                str = pfreq.Rows[0]["ad_socr_town"].ToString() + " " + pfreq.Rows[0]["ad_town"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arTownB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arTown2[i]).Value = str[i].ToString();
                }
                str = "";

                //улица
                switch (pfreq.Rows[0]["ad_socr_street"].ToString())
                {
                    case "пр-кт":
                        str = "ПРОСПЕКТ " + pfreq.Rows[0]["ad_street"].ToString().ToUpper();
                        for (int i = 0; i < str.Length; i++)
                        {
                            sheetExcel1XML.Range(arStreetB[i]).Value = str[i].ToString();
                            sheetExcel2XML.Range(arStreet2[i]).Value = str[i].ToString();
                        }                        
                        break;

                    default:                        
                        str = pfreq.Rows[0]["ad_socr_street"].ToString().ToUpper() + " " + pfreq.Rows[0]["ad_street"].ToString().ToUpper();
                        for (int i = 0; i < str.Length; i++)
                        {
                            sheetExcel1XML.Range(arStreetB[i]).Value = str[i].ToString();
                            sheetExcel2XML.Range(arStreet2[i]).Value = str[i].ToString();
                        }
                        break;
                }
                str = "";


                //дом
                str = pfreq.Rows[0]["ad_house"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arHouseB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arHouse2[i]).Value = str[i].ToString();
                }
                str = "";

                //корпус
                str = pfreq.Rows[0]["ad_corp"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arKorpB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arKorp2[i]).Value = str[i].ToString();
                }
                str = "";

                //строение
                str = pfreq.Rows[0]["ad_stroenie"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arStroB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arStro2[i]).Value = str[i].ToString();
                }
                str = "";

                //квартира
                str = pfreq.Rows[0]["ad_flat"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel1XML.Range(arFlatB[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arFlat2[i]).Value = str[i].ToString();
                }
                str = "";

                /*Сведения о принимающей стороне*/

                if (pfhost.Rows[0]["org_phis"].ToString() == "Организация")
                {                    
                    sheetExcel2XML.Range("EE26").Value = 'X'.ToString();
                    sheetExcel2XML.Range("FC26").Value = String.Empty;
                }
                else
                {
                    sheetExcel2XML.Range("EE26").Value = String.Empty;
                    sheetExcel2XML.Range("FC26").Value = 'X'.ToString();
                }

                //фамилия
                str = pfhost.Rows[0]["last_name"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arNameP[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arName2B[i]).Value = str[i].ToString();
                }
                str = "";


                //имя+отчество
                str = pfhost.Rows[0]["first_name"].ToString().ToUpper() + " " + pfhost.Rows[0]["second_name"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arLastNameP[i]).Value = str[i].ToString();
                    sheetExcel2XML.Range(arLastName2B[i]).Value = str[i].ToString();
                }
                str = "";


                //дата рождения
                str = pfhost.Rows[0]["birthday"].ToString();
                if (str != "")
                {
                    sheetExcel2XML.Range(arDateBirthP[0]).Value = str[0].ToString();
                    sheetExcel2XML.Range(arDateBirthP[1]).Value = str[1].ToString();

                    sheetExcel2XML.Range(arDateBirthP[2]).Value = str[3].ToString();
                    sheetExcel2XML.Range(arDateBirthP[3]).Value = str[4].ToString();

                    sheetExcel2XML.Range(arDateBirthP[4]).Value = str[6].ToString();
                    sheetExcel2XML.Range(arDateBirthP[5]).Value = str[7].ToString();
                    sheetExcel2XML.Range(arDateBirthP[6]).Value = str[8].ToString();
                    sheetExcel2XML.Range(arDateBirthP[7]).Value = str[9].ToString();
                   
                }
                str = "";

                //ДУЛ
                str = pfhost.Rows[0]["doc"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arDocViewP[i]).Value = str[i].ToString();                    
                }
                str = "";

                //серия
                str = pfhost.Rows[0]["doc_ser"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arDocSerP[i]).Value = str[i].ToString();
                }
                str = "";

                //номер
                str = pfhost.Rows[0]["doc_num"].ToString();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arDocNumP[i]).Value = str[i].ToString();
                }
                str = "";

                //дата выдачи
                str = pfhost.Rows[0]["date_issue"].ToString();
                if (str != "")
                {
                    sheetExcel2XML.Range(arDateVidP[0]).Value = str[0].ToString();
                    sheetExcel2XML.Range(arDateVidP[1]).Value = str[1].ToString();

                    sheetExcel2XML.Range(arDateVidP[2]).Value = str[3].ToString();
                    sheetExcel2XML.Range(arDateVidP[3]).Value = str[4].ToString();

                    sheetExcel2XML.Range(arDateVidP[4]).Value = str[6].ToString();
                    sheetExcel2XML.Range(arDateVidP[5]).Value = str[7].ToString();
                    sheetExcel2XML.Range(arDateVidP[6]).Value = str[8].ToString();
                    sheetExcel2XML.Range(arDateVidP[7]).Value = str[9].ToString();
                }
                str = "";

                //срок действия
                str = pfhost.Rows[0]["date_valid"].ToString();
                if (str != "")
                {
                    sheetExcel2XML.Range(arDateExpiredP[0]).Value = str[0].ToString();
                    sheetExcel2XML.Range(arDateExpiredP[1]).Value = str[1].ToString();

                    sheetExcel2XML.Range(arDateExpiredP[2]).Value = str[3].ToString();
                    sheetExcel2XML.Range(arDateExpiredP[3]).Value = str[4].ToString();

                    sheetExcel2XML.Range(arDateExpiredP[4]).Value = str[6].ToString();
                    sheetExcel2XML.Range(arDateExpiredP[5]).Value = str[7].ToString();
                    sheetExcel2XML.Range(arDateExpiredP[6]).Value = str[8].ToString();
                    sheetExcel2XML.Range(arDateExpiredP[7]).Value = str[9].ToString();
                }
                str = "";

                //область
                str = pfhost.Rows[0]["obl"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arOblP[i]).Value = str[i].ToString();
                }
                str = "";

                //район
                str = pfhost.Rows[0]["rayon"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arRayonP[i]).Value = str[i].ToString();
                }
                str = "";

                //город
                str = pfhost.Rows[0]["town"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arTownP[i]).Value = str[i].ToString();
                }
                str = "";

                //улица
                str = pfhost.Rows[0]["street"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arStreetP[i]).Value = str[i].ToString();
                }
                str = "";

                //дом
                str = pfhost.Rows[0]["house"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arHouseP[i]).Value = str[i].ToString();
                }
                str = "";

                //корпус
                str = pfhost.Rows[0]["korp"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arKorpP[i]).Value = str[i].ToString();
                }
                str = "";

                //строение
                str = pfhost.Rows[0]["stro"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arStroP[i]).Value = str[i].ToString();
                }
                str = "";

                //квартира
                str = pfhost.Rows[0]["flat"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arFlatP[i]).Value = str[i].ToString();
                }
                str = "";


                //телефон
                str = pfhost.Rows[0]["phone"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arPhoneP[i]).Value = str[i].ToString();
                }
                str = "";

                //инн
                str = pfhost.Rows[0]["inn"].ToString().ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arInnP[i]).Value = str[i].ToString();
                }
                str = "";

               //перенести  - наименование орг.
               string tPerenos = pfhost.Rows[0]["org_name"].ToString();
               string res = "";
               string res2 = "";
               if (tPerenos.Length < 24)
               {
                   res = tPerenos;
               }
               else
               {
                   int i;
                   string stemp = "";
                   stemp = tPerenos.Substring(0, 24);
                   i = stemp.LastIndexOf(" ");
                   res = tPerenos.Substring(0, i + 1);
                   res2 = tPerenos.Substring(i, tPerenos.Length - i);
               }
                
                str = res.ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arOrgNameP[i]).Value = str[i].ToString();
                }
                str = "";
                str = res2.ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arOrgNameP2[i]).Value = str[i].ToString();
                }
                str = "";
            
                
                //адрес
                tPerenos = pfhost.Rows[0]["address"].ToString();
                res = "";
                res2 = "";
                if (tPerenos.Length < 24)
                {
                    res = tPerenos;
                }
                else
                {
                    int i;
                    string stemp = "";
                    stemp = tPerenos.Substring(0, 24);
                    i = stemp.LastIndexOf(" ");
                    res = tPerenos.Substring(0, i + 1);
                    res2 = tPerenos.Substring(i, tPerenos.Length - i);
                }
                str = res.ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arFactAdrP[i]).Value = str[i].ToString();
                }
                str = "";
                str = res2.ToUpper();
                for (int i = 0; i < str.Length; i++)
                {
                    sheetExcel2XML.Range(arFactAdrP2[i]).Value = str[i].ToString();
                }
                str = "";

                //excelWorksheet.SaveAs(NewFile);
                excelApp1XML.Save();

                string SavePf = InsertPf(ReportName);
                if (SavePf != "")
                    ErrMsg = SavePf;

            }
            catch (Exception e)
            {               
                ErrMsg = "Ошибка ^_^";
            }
            finally
            {  
                //excelApp.Quit();                                   // exit excel application
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);                

            }
            return ErrMsg;
        }


        /**/
        string[] arName = new string[] { "W13","AA13","AE13","AI13","AM13","AQ13","AU13","AY13","BC13","BG13","BK13","BO13",
                                  "BS13","BW13","CA13","CE13","CI13","CM13","CQ13","CU13","CY13","DC13","DG13","DK13","DO13","DS13",
                                  "DW13","EA13","EE13","EI13","EM13","EQ13","EU13","EY13","FC13" };
        string[] arLastName = new string[]{"W15","AA15","AE15","AI15","AM15","AQ15","AU15","AY15","BC15","BG15","BK15","BO15",
                                  "BS15","BW15","CA15","CE15","CI15","CM15","CQ15","CU15","CY15","DC15","DG15","DK15","DO15","DS15",
                                  "DW15","EA15","EE15","EI15","EM15","EQ15","EU15","EY15","FC15"};
        string[] arPoddan = new string[]{"AA18","AE18","AI18","AM18","AQ18","AU18","AY18","BC18","BG18","BK18","BO18",
                                  "BS18","BW18","CA18","CE18","CI18","CM18","CQ18","CU18","CY18","DC18","DG18","DK18","DO18","DS18",
                                  "DW18","EA18","EE18","EI18","EM18","EQ18","EU18","EY18","FC18"};
        string[] arDateBirth = new string[] { "AE21", "AI21", "AU21", "AY21", "BG21", "BK21", "BO21", "BS21" };
        string[] arSex = new string[] { "CY21", "DS21" };
        string[] arCountryBirth = new string[]{"AE24","AI24","AM24","AQ24","AU24","AY24","BC24","BG24","BK24","BO24",
                                  "BS24","BW24","CA24","CE24","CI24","CM24","CQ24","CU24","CY24","DC24","DG24","DK24","DO24","DS24",
                                  "DW24","EA24","EE24","EI24","EM24","EQ24","EU24","EY24","FC24"};
        string[] arTownBirth = new string[]{"AE27","AI27","AM27","AQ27","AU27","AY27","BC27","BG27","BK27","BO27",
                                  "BS27","BW27","CA27","CE27","CI27","CM27","CQ27","CU27","CY27","DC27","DG27","DK27","DO27","DS27",
                                  "DW27","EA27","EE27","EI27","EM27","EQ27","EU27","EY27","FC27"};
        string[] arDocView = new string[] { "BC30", "BG30", "BK30", "BO30", "BS30", "BW30", "CA30", "CE30", "CI30", "CM30", "CQ30" };
        string[] arDocSer = new string[] { "DC30", "DG30", "DK30", "DO30" };
        string[] arDocNum = new string[] { "DW30", "EA30", "EE30", "EI30", "EM30", "EQ30", "EU30", "EY30", "FC30" };
        string[] arDateVid = new string[] { "AA32", "AE32", "AQ32", "AU32", "BC32", "BG32", "BK32", "BO32" };
        string[] arDateExpired = new string[] { "CM32", "CQ32", "DC32", "DG32", "DO32", "DS32", "DW32", "EA32" };


        string[] arVisDocSer = new string[] { "DC37", "DG37", "DK37", "DO37" };
        string[] arVisDocNum = new string[] { "DW37", "EA37", "EE37", "EI37", "EM37", "EQ37", "EU37", "EY37", "FC37" };
        string[] arVisDateVid = new string[] { "AA40", "AE40", "AQ40", "AU40", "BC40", "BG40", "BK40", "BO40" };
        string[] arVisDateExpired = new string[] { "CM40", "CQ40", "DC40", "DG40", "DO40", "DS40", "DW40", "EA40" };
        string[] arProf = new string[]{"W45","AA45","AE45","AI45","AM45","AQ45","AU45","AY45","BC45","BG45",
                                      "BK45","BO45","BS45","BW45","CA45","CE45","CI45","CM45","CQ45","CU45","CY45","DC45","DG45"};
        string[] arEnt = new string[] { "AI47", "AM47", "AY47", "BC47", "BK47", "BO47", "BS47", "BW47" };
        string[] arTenure = new string[] { "DO47", "DS47", "EE47", "EI47", "EQ47", "EU47", "EY47", "FC47" };
        string[] arMigrSer = new string[] { "AQ49", "AU49", "AY49", "BC49" };
        string[] arMigrNum = new string[] { "BK49", "BO49", "BS49", "BW49", "CA49", "CE49", "CI49" };
        string[] arSved = new string[]{"AA51","AE51","AI51","AM51","AQ51","AU51","AY51","BC51","BG51",
                                      "BK51","BO51","BS51","BW51","CA51","CE51","CI51","CM51","CQ51","CU51",
                                      "AA53","AE53","AI53","AM53","AQ53","AU53","AY53","BC53","BG53",
                                      "BK53","BO53","BS53","BW53","CA53","CE53","CI53","CM53","CQ53","CU53"};
        string[] arAdr = new string[]{"AA57","AE57","AI57","AM57","AQ57","AU57","AY57","BC57","BG57",
                                      "BK57","BO57","BS57","BW57","CA57","CE57","CI57","CM57","CQ57","CU57",
                                      "AA59","AE59","AI59","AM59","AQ59","AU59","AY59","BC59","BG59",
                                      "BK59","BO59","BS59","BW59","CA59","CE59","CI59","CM59","CQ59","CU59",
                                      "AA61","AE61","AI61","AM61","AQ61","AU61","AY61","BC61","BG61",
                                      "BK61","BO61","BS61","BW61","CA61","CE61","CI61","CM61","CQ61","CU61"};

        //----------------------------
        string[] arNameB = new string[]{"W69","AA69","AE69","AI69","AM69","AQ69","AU69","AY69","BC69","BG69","BK69","BO69",
                                  "BS69","BW69","CA69","CE69","CI69","CM69","CQ69","CU69","CY69","DC69","DG69","DK69","DO69","DS69",
                                  "DW69","EA69","EE69","EI69","EM69","EQ69","EU69","EY69","FC69"};
        string[] arLastNameB = new string[]{"W71","AA71","AE71","AI71","AM71","AQ71","AU71","AY71","BC71","BG71","BK71","BO71",
                                  "BS71","BW71","CA71","CE71","CI71","CM71","CQ71","CU71","CY71","DC71","DG71","DK71","DO71","DS71",
                                  "DW71","EA71","EE71","EI71","EM71","EQ71","EU71","EY71","FC71"};
        string[] arPoddanB = new string[]{"AA74","AE74","AI74","AM74","AQ74","AU74","AY74","BC74","BG74","BK74","BO74",
                                  "BS74","BW74","CA74","CE74","CI74","CM74","CQ74","CU74","CY74","DC74","DG74","DK74","DO74","DS74",
                                  "DW74","EA74","EE74","EI74","EM74","EQ74","EU74","EY74","FC74"};
        string[] arDateBirthB = new string[] { "AE77", "AI77", "AU77", "AY77", "BG77", "BK77", "BO77", "BS77" };
        string[] arSexB = new string[] { "DC77", "DW77" };
        string[] arDocViewB = new string[] { "BC80", "BG80", "BK80", "BO80", "BS80", "BW80", "CA80", "CE80", "CI80", "CM80", "CQ80" };
        string[] arDocSerB = new string[] { "DC80", "DG80", "DK80", "DO80" };
        string[] arDocNumB = new string[] { "DW80", "EA80", "EE80", "EI80", "EM80", "EQ80", "EU80", "EY80", "FC80" };
        string[] arOblB = new string[]{"AE83","AI83","AM83","AQ83","AU83","AY83","BC83","BG83","BK83","BO83",
                                  "BS83","BW83","CA83","CE83","CI83","CM83","CQ83","CU83","CY83","DC83","DG83","DK83","DO83","DS83",
                                  "DW83","EA83","EE83","EI83","EM83","EQ83","EU83","EY83","FC83"};
        string[] arRayonB = new string[]{"W86","AA86","AE86","AI86","AM86","AQ86","AU86","AY86","BC86","BG86","BK86","BO86",
                                  "BS86","BW86","CA86","CE86","CI86","CM86","CQ86","CU86","CY86","DC86","DG86","DK86","DO86","DS86",
                                  "DW86","EA86","EE86","EI86","EM86","EQ86","EU86","EY86","FC86"};
        string[] arTownB = new string[]{"AE88","AI88","AM88","AQ88","AU88","AY88","BC88","BG88","BK88","BO88",
                                  "BS88","BW88","CA88","CE88","CI88","CM88","CQ88","CU88","CY88","DC88","DG88","DK88","DO88","DS88",
                                  "DW88","EA88","EE88","EI88","EM88","EQ88","EU88","EY88","FC88"};
        string[] arStreetB = new string[]{"W91","AA91","AE91","AI91","AM91","AQ91","AU91","AY91","BC91","BG91","BK91","BO91",
                                  "BS91","BW91","CA91","CE91","CI91","CM91","CQ91","CU91","CY91","DC91","DG91","DK91","DO91","DS91",
                                  "DW91","EA91","EE91","EI91","EM91","EQ91","EU91","EY91","FC91"};
        string[] arHouseB = new string[] { "S93", "W93", "AA93", "AE93" };
        string[] arKorpB = new string[] { "AQ93", "AU93", "AY93", "BC93" };
        string[] arStroB = new string[] { "BS93", "BW93", "CA93", "CE93" };
        string[] arFlatB = new string[] { "CU93", "CY93", "DC93", "DG93" };
        string[] arTenureB = new string[] { "AQ95", "AU95", "BG95", "BK95", "BS95", "BW95", "CA95", "CE95" };
        //стр.2
        string[] arObl2 = new string[]{"AE14","AI14","AM14","AQ14","AU14","AY14","BC14","BG14","BK14","BO14",
                                  "BS14","BW14","CA14","CE14","CI14","CM14","CQ14","CU14","CY14","DC14","DG14","DK14","DO14","DS14",
                                  "DW14","EA14","EE14","EI14","EM14","EQ14","EU14","EY14","FC14"};
        string[] arRayon2 = new string[]{"W17","AA17","AE17","AI17","AM17","AQ17","AU17","AY17","BC17","BG17","BK17","BO17",
                                  "BS17","BW17","CA17","CE17","CI17","CM17","CQ17","CU17","CY17","DC17","DG17","DK17","DO17","DS17",
                                  "DW17","EA17","EE17","EI17","EM17","EQ17","EU17","EY17","FC17"};
        string[] arTown2 = new string[]{"AE19","AI19","AM19","AQ19","AU19","AY19","BC19","BG19","BK19","BO19",
                                  "BS19","BW19","CA19","CE19","CI19","CM19","CQ19","CU19","CY19","DC19","DG19","DK19","DO19","DS19",
                                  "DW19","EA19","EE19","EI19","EM19","EQ19","EU19","EY19","FC19"};
        string[] arStreet2 = new string[]{"W22","AA22","AE22","AI22","AM22","AQ22","AU22","AY22","BC22","BG22","BK22","BO22",
                                  "BS22","BW22","CA22","CE22","CI22","CM22","CQ22","CU22","CY22","DC22","DG22","DK22","DO22","DS22",
                                  "DW22","EA22","EE22","EI22","EM22","EQ22","EU22","EY22","FC22"};
        string[] arHouse2 = new string[] { "S24", "W24", "AA24", "AE24" };
        string[] arKorp2 = new string[] { "AQ24", "AU24", "AY24", "BC24" };
        string[] arStro2 = new string[] { "BS24", "BW24", "CA24", "CE24" };
        string[] arFlat2 = new string[] { "CU24", "CY24", "DC24", "DG24" };
        string[] arPhone2 = new string[] { "DS24", "DW24", "EA24", "EE24", "EI24", "EM24", "EQ24", "EU24", "EY24", "FC24" };

        //принимающая сторона
        string[] arNameP = new string[]{"W28","AA28","AE28","AI28","AM28","AQ28","AU28","AY28","BC28","BG28","BK28","BO28",
                                  "BS28","BW28","CA28","CE28","CI28","CM28","CQ28"};
        string[] arLastNameP = new string[]{"W31","AA31","AE31","AI31","AM31","AQ31","AU31","AY31","BC31","BG31","BK31","BO31",
                                  "BS31","BW31","CA31","CE31","CI31","CM31","CQ31","CU31","CY31","DC31","DG31","DK31","DO31","DS31",
                                  "DW31","EA31","EE31","EI31","EM31","EQ31","EU31","EY31","FC31"};
        string[] arDateBirthP = new string[] { "DO28", "DS28", "EE28", "EI28", "EQ28", "EU28", "EY28", "FC28" };
        string[] arDocViewP = new string[] { "BC34", "BG34", "BK34", "BO34", "BS34", "BW34", "CA34", "CE34", "CI34", "CM34", "CQ34" };
        string[] arDocSerP = new string[] { "DC34", "DG34", "DK34", "DO34" };
        string[] arDocNumP = new string[] { "DW34", "EA34", "EE34", "EI34", "EM34", "EQ34", "EU34", "EY34", "FC34" };
        string[] arDateVidP = new string[] { "AA36", "AE36", "AQ36", "AU36", "BC36", "BG36", "BK36", "BO36" };
        string[] arDateExpiredP = new string[] { "CM36", "CQ36", "DC36", "DG36", "DO36", "DS36", "DW36", "EA36" };
        string[] arOblP = new string[]{"AE39","AI39","AM39","AQ39","AU39","AY39","BC39","BG39","BK39","BO39",
                                  "BS39","BW39","CA39","CE39","CI39","CM39","CQ39","CU39","CY39","DC39","DG39","DK39","DO39","DS39",
                                  "DW39","EA39","EE39","EI39","EM39","EQ39","EU39","EY39","FC39"};
        string[] arRayonP = new string[]{"W42","AA42","AE42","AI42","AM42","AQ42","AU42","AY42","BC42","BG42","BK42","BO42",
                                  "BS42","BW42","CA42","CE42","CI42","CM42","CQ42","CU42","CY42","DC42","DG42","DK42","DO42","DS42",
                                  "DW42","EA42","EE42","EI42","EM42","EQ42","EU42","EY42","FC42"};
        string[] arTownP = new string[]{"AE44","AI44","AM44","AQ44","AU44","AY44","BC44","BG44","BK44","BO44",
                                  "BS44","BW44","CA44","CE44","CI44","CM44","CQ44","CU44","CY44","DC44","DG44","DK44","DO44","DS44",
                                  "DW44","EA44","EE44","EI44","EM44","EQ44","EU44","EY44","FC44"};
        string[] arStreetP = new string[]{"W47","AA47","AE47","AI47","AM47","AQ47","AU47","AY47","BC47","BG47","BK47","BO47",
                                  "BS47","BW47","CA47","CE47","CI47","CM47","CQ47","CU47","CY47","DC47","DG47","DK47","DO47","DS47",
                                  "DW47","EA47","EE47","EI47","EM47","EQ47","EU47","EY47","FC47"};
        string[] arHouseP = new string[] { "S49", "W49", "AA49", "AE49" };
        string[] arKorpP = new string[] { "AQ49", "AU49", "AY49", "BC49" };
        string[] arStroP = new string[] { "BS49", "BW49", "CA49", "CE49" };
        string[] arFlatP = new string[] { "CU49", "CY49", "DC49", "DG49" };
        string[] arPhoneP = new string[] { "DS49", "DW49", "EA49", "EE49", "EI49", "EM49", "EQ49", "EU49", "EY49", "FC49" };
        string[] arOrgNameP = new string[]{"AA51","AE51","AI51","AM51","AQ51","AU51","AY51","BC51","BG51","BK51","BO51",
                                  "BS51","BW51","CA51","CE51","CI51","CM51","CQ51","CU51","CY51","DC51","DG51","DK51","DO51" };

        string[] arOrgNameP2 = new string[]{"K54","O54","S54","W54","AA54","AE54","AI54","AM54","AQ54","AU54","AY54","BC54","BG54","BK54","BO54",
                                  "BS54","BW54","CA54","CE54","CI54","CM54","CQ54","CU54","CY54","DC54","DG54","DK54","DO54"};
        string[] arFactAdrP = new string[]{"AA56","AE56","AI56","AM56","AQ56","AU56","AY56","BC56","BG56","BK56","BO56",
                                  "BS56","BW56","CA56","CE56","CI56","CM56","CQ56","CU56","CY56","DC56","DG56","DK56","DO56" };
        string[] arFactAdrP2 = new string[]{"K58","O58","S58","W58","AA58","AE58","AI58","AM58","AQ58","AU58","AY58","BC58","BG58","BK58","BO58",
                                  "BS58","BW58","CA58","CE58","CI58","CM58","CQ58","CU58","CY58","DC58","DG58","DK58","DO58"};
        string[] arInnP = new string[] { "S60", "W60", "AA60", "AE60", "AI60", "AM60", "AQ60", "AU60", "AY60", "BC60", "BG60", "BK60" };
        //отрыв
        string[] arDateBirth2B = new string[] { "DO69", "DS69", "EE69", "EI69", "EQ69", "EU69", "EY69", "FC69" };
        string[] arName2B = new string[]{"W71","AA71","AE71","AI71","AM71","AQ71","AU71","AY71","BC71","BG71","BK71","BO71",
                                  "BS71","BW71","CA71","CE71","CI71","CM71","CQ71","CU71","CY71","DC71","DG71","DK71","DO71","DS71",
                                  "DW71","EA71","EE71","EI71","EM71","EQ71","EU71","EY71","FC71"};
        string[] arLastName2B = new string[]{"W73","AA73","AE73","AI73","AM73","AQ73","AU73","AY73","BC73","BG73","BK73","BO73",
                                  "BS73","BW73","CA73","CE73","CI73","CM73","CQ73","CU73","CY73","DC73","DG73","DK73","DO73","DS73",
                                  "DW73","EA73","EE73","EI73","EM73","EQ73","EU73","EY73","FC73"};
    }

   

}
