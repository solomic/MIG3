using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Pref;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Reflection;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;


namespace Mig
{
    class pf
    {
        public string GETNOW = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        public string ClassName = "Class: pf.cs\n";


        string CapitalizeString(Match matchString)
        {
            string strTemp;
            try
            {
                strTemp = matchString.ToString();
                strTemp = char.ToUpper(strTemp[0]) + strTemp.Substring(1, strTemp.Length - 1).ToLower();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return strTemp;
        }
        public String FirstUpper(String str) /*Один символ после пробела Заглавный*/
        {
            string res;
            try
            {
                str = str.ToLower();
                string[] s = str.Split(' ');
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i].Length > 1)
                        s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1);
                    else s[i] = s[i].ToUpper();
                }
                res = string.Join(" ", s);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return res;
        }

        public void InsertPf(string name)
        { 
            
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
                throw new Exception(msg.Message);
            }

        }

        public static void InsertIntoBookmark(BookmarkStart bookmarkStart, string in_text, string sz= "28")
        {
            bool repl = false;
            OpenXmlElement elem = bookmarkStart.NextSibling();

            while (elem != null && !(elem is BookmarkEnd))
            {
                OpenXmlElement nextElem = elem.NextSibling();
                if (repl)
                    elem.Remove();
                else
                if (elem is Run && elem.GetFirstChild<Text>() != null)
                {
                    elem.GetFirstChild<Text>().Text = in_text;
                    repl = true;
                }
                elem = nextElem;
            }
            
            //while (elem != null && !(elem is BookmarkEnd))
            //{
            //    if(elem is Text)
            //    {
            //        break;
            //    }
            //    OpenXmlElement nextElem = elem.NextSibling();
            //    elem.Remove();
            //    elem = nextElem;
            //}
            //Run run = new Run();
            //Text currLine = new Text(text);
            //run.AppendChild<Text>(currLine);
            //RunProperties runProp = new RunProperties();          
            //FontSize size = new FontSize();
            //RunFonts fnt = new RunFonts();
            //fnt.Ascii = "Times New Roman";
            //size.Val = new StringValue(sz);           
            //runProp.Append(size);
            //runProp.Append(fnt);
            //run.PrependChild<RunProperties>(runProp);
            //bookmarkStart.Parent.InsertAfter<Run> (run, bookmarkStart);

            //Run run = new Run();
            //RunProperties runProperties = new RunProperties();
            //FontSize fontSize = new FontSize() { Val = "22" };
            //RunFonts runFonts = new RunFonts() { Ascii = "Times New Roman" };
            //runProperties.Append(fontSize);
            //runProperties.Append(runFonts);
            //Text text = new Text();
            //text.Text = in_text;
            //run.Append(runProperties);
            //run.Append(text);
            //bookmarkStart.InsertAfterSelf(run);
        }


        public void FillDoc(string path, Dictionary<string, string> prm, string sz = "28")
        {
            try
            {
                OpenSettings os = new OpenSettings
                {
                    AutoSave = true
                };
                WordprocessingDocument doc = WordprocessingDocument.Open(path, true, os);

                foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                {
                    if (bookmarkStart.Name == "_GoBack") continue;
                    foreach (var item in prm)
                    {
                        if (item.Key == bookmarkStart.Name)
                        {
                            InsertIntoBookmark(bookmarkStart, item.Value, sz);
                            prm.Remove(item.Key);
                            break;
                        }
                    }
                }

                doc.Save();
                doc.Close();
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:FillDoc\n Error:" + e);
                throw new Exception(e.Message);
            }

        }


        public void GeneratePetitionStandart(string s1,string s2)
        {
            /*Регистрация.Ходатайство.Обычное*/
            if (s1 == "" || s2 == "")           
                throw new Exception("Укажите входные параметры");

            string TemplateName = "PETITION.STANDART.docx";
            string TemplatePath = Directory.GetCurrentDirectory()+@"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Ходатайство_Обычное.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath,NewPath );
                        
                param.Add("change", s1);
                param.Add("post", s2);     
                param.Add("gr", pfreq.Rows[0]["gr"].ToString());              
                param.Add("nationality", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("fio", FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("birthday", pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("dul_type", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("dul_ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("dul_num", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dul_issue", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("card_tenure_to_dt", pfreq.Rows[0]["card_tenure_to_dt"].ToString());
                param.Add("full_address", pfreq.Rows[0]["ad_full_address"].ToString());

                FillDoc(NewPath, param);
             
                InsertPf(ReportName);
               
            }
            catch(Exception e)  {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionStandart\n Error:" + e);
                throw new Exception(e.Message);
            }
           
        }
        public void GenerateDepObr()
        {
            
            /*Уведомление о прибытии*/
            string TemplateName = "DEP.OBR.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Уведомление_о_прибытии.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();           

            try
            {
                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                param.Add("fio", FirstUpper( pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("nat", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString))); 
                param.Add("birth", pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("num", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("pfrom", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("pteach", Regex.Replace(pfreq.Rows[0]["teach_pt"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString))); 
                if (pfreq.Rows[0]["con_sex"].ToString() == "МУЖСКОЙ")
                {
                    param.Add("p1", "прибыл следующий иностранный гражданин");                   
                }
                else
                {
                    param.Add("p1","прибыла следующая иностранная гражданка");                   
                }

                FillDoc(NewPath, param);

                InsertPf(ReportName);
               

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateDepObr\n Error:" + e);             
                throw new Exception(e.Message);
            }
            
        }
       
        public void GeneratePetitionDeduct()
        {
           
            /*Уведомление об отчислении*/
            string TemplateName = "PETITION.DEDUCT.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Уведомление_отчисление_УФМС.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                param.Add("fio", FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));                
                param.Add("nat",Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString))); 
                param.Add("birth", pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("dul",pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("n",pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dul_from", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("pr_num", pfreq.Rows[0]["exp_num"].ToString());
                param.Add("pr_from", pfreq.Rows[0]["exp_dt"].ToString());
                param.Add("osn", pfreq.Rows[0]["exp_expelled"].ToString());
                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    param.Add("p1", "отчислена");
                    param.Add("p2","следующая иностранная гражданка");
                }
                else
                {
                    param.Add("p1", "отчислен");
                    param.Add("p2", "следующий иностранный гражданин");
                }
                FillDoc(NewPath, param);

                InsertPf(ReportName);


            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionDeduct\n Error:" + e);
                throw new Exception(e.Message);
            }
            
        }

        //public string GenerateNotify()
        //{
           
        //    /*Уведомление о прибытии*/
        //    string TemplateName = "Notify Template.pdf";
        //    string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
        //    string pathFont = Directory.GetCurrentDirectory() + @"\ARIALUNI.ttf";
        //    string ErrMsg = "";
        //    string ReportName = GETNOW + "_Уведомление_о_прибытии.pdf";

        //    DataTable pfhost = DB.QueryTableMultipleParams(pref.PfHost, null);
        //    if(pfhost.Rows.Count==0)
        //    {
        //        return "Нет активного исполнителя!";
        //    }
        //    DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
            

        //    string NewFile = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;


        //    Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
        //    try
        //    {
        //        // BaseFont baseFont = BaseFont.CreateFont(pathFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        //        BaseFont bf = iTextSharp.text.pdf.BaseFont.CreateFont(pathFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //        iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);

        //       // File.Copy(TemplatePath, NewFile,true);
                

        //        PdfReader reader = new PdfReader(TemplatePath);
        //        using (PdfStamper stamper = new PdfStamper(reader, new FileStream(NewFile, FileMode.Create)))
        //        {
        //            AcroFields fields = stamper.AcroFields;

        //            fields.AddSubstitutionFont(bf);
                   

        //            // set form fields

        //            fields.SetField("comb_1", pfreq.Rows[0]["con_last_name"].ToString());
        //            fields.SetField("comb_2", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());
        //            fields.SetField("comb_3", pfreq.Rows[0]["con_nat"].ToString());
        //            if (pfreq.Rows[0]["con_birthday"].ToString() != "")
        //            {
        //                fields.SetField("comb_4", pfreq.Rows[0]["con_birthday"].ToString().Substring(0, 2));
        //                fields.SetField("comb_5", pfreq.Rows[0]["con_birthday"].ToString().Substring(3, 2));
        //                fields.SetField("comb_6", pfreq.Rows[0]["con_birthday"].ToString().Substring(6, 4));
        //            }
        //            if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
        //                fields.SetField("comb_61", "X");
        //            else
        //                fields.SetField("comb_60", "X");
        //            fields.SetField("comb_8", pfreq.Rows[0]["con_birth_town"].ToString());
        //            fields.SetField("comb_7", pfreq.Rows[0]["con_birth_country"].ToString());
        //            fields.SetField("comb_9", FirstUpper(pfreq.Rows[0]["dul_type"].ToString().ToUpper()));
        //            fields.SetField("comb_10", pfreq.Rows[0]["dul_ser"].ToString());
        //            fields.SetField("comb_11", pfreq.Rows[0]["dul_num"].ToString());
        //            if (pfreq.Rows[0]["dul_issue"].ToString() != "")
        //            {
        //                fields.SetField("comb_12", pfreq.Rows[0]["dul_issue"].ToString().Substring(0, 2));
        //                fields.SetField("comb_13", pfreq.Rows[0]["dul_issue"].ToString().Substring(3, 2));
        //                fields.SetField("comb_14", pfreq.Rows[0]["dul_issue"].ToString().Substring(6, 4));
        //            }
        //            if (pfreq.Rows[0]["dul_validity"].ToString() != "")
        //            {
        //                fields.SetField("comb_15", pfreq.Rows[0]["dul_validity"].ToString().Substring(0, 2));
        //                fields.SetField("comb_16", pfreq.Rows[0]["dul_validity"].ToString().Substring(3, 2));
        //                fields.SetField("comb_17", pfreq.Rows[0]["dul_validity"].ToString().Substring(6, 4));
        //            }
        //            switch (pfreq.Rows[0]["doc_type"].ToString())
        //            {
        //                case "Виза":
        //                    fields.SetField("comb_62", "X");
        //                    break;
        //                case "ВНЖ":
        //                    fields.SetField("comb_63", "X");
        //                    break;
        //                case "РВП":
        //                    fields.SetField("comb_64", "X");
        //                    break;
        //            }
        //            fields.SetField("comb_18", pfreq.Rows[0]["doc_ser"].ToString());
        //            fields.SetField("comb_19", pfreq.Rows[0]["doc_num"].ToString());

        //            if (pfreq.Rows[0]["doc_issue_dt"].ToString() != "")
        //            {
        //                fields.SetField("comb_20", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(0, 2));
        //                fields.SetField("comb_21", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(3, 2));
        //                fields.SetField("comb_22", pfreq.Rows[0]["doc_issue_dt"].ToString().Substring(6, 4));
        //            }
        //            if (pfreq.Rows[0]["doc_validity_to_dt"].ToString() != "")
        //            {
        //                fields.SetField("comb_23", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(0, 2));
        //                fields.SetField("comb_24", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(3, 2));
        //                fields.SetField("comb_25", pfreq.Rows[0]["doc_validity_to_dt"].ToString().Substring(6, 4));
        //            }
        //            switch (pfreq.Rows[0]["card_purpose_entry"].ToString())
        //            {
        //                case "СЛУЖЕБНАЯ":
        //                fields.SetField("comb_65", "X");
        //                break;
        //                case "ТУРИЗМ":
        //                    fields.SetField("comb_66", "X");
        //                    break;
        //                case "ДЕЛОВАЯ":
        //                    fields.SetField("comb_67", "X");
        //                    break;
        //                case "УЧЕБА":
        //                    fields.SetField("comb_68", "X");
        //                    break;
        //                case "РАБОТА":
        //                    fields.SetField("comb_69", "X");
        //                    break;
        //                case "ЧАСТНАЯ":
        //                    fields.SetField("comb_70", "X");
        //                    break;
        //                case "ТРАНЗИТ":
        //                    fields.SetField("comb_71", "X");
        //                    break;
        //                case "ГУМАНИТАРНАЯ":
        //                    fields.SetField("comb_72", "X");
        //                    break;
        //                case "ДРУГАЯ":
        //                    fields.SetField("comb_73", "X");
        //                    break;
        //            }

        //            fields.SetField("comb_26", "");//профессия


        //            if (pfreq.Rows[0]["card_entry_dt"].ToString() != "")
        //            {
        //                fields.SetField("comb_27", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(0, 2));
        //                fields.SetField("comb_28", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(3, 2));
        //                fields.SetField("comb_29", pfreq.Rows[0]["card_entry_dt"].ToString().Substring(6, 4));
        //            }
        //            if (pfreq.Rows[0]["card_tenure_to_dt"].ToString() != "")
        //            {
        //                fields.SetField("comb_30", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(0, 2));
        //                fields.SetField("comb_31", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(3, 2));
        //                fields.SetField("comb_32", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(6, 4));
        //            }
        //            fields.SetField("comb_33", pfreq.Rows[0]["card_ser"].ToString());
        //            fields.SetField("comb_34", pfreq.Rows[0]["card_num"].ToString());


        //            fields.SetField("comb_35", "");//законный представитель
        //            fields.SetField("comb_36", "");//законный представитель

        //            fields.SetField("comb_37", "");//прежний адрес
        //            fields.SetField("comb_38", "");//прежний адрес
        //            fields.SetField("comb_39", "");//прежний адрес


        //            /*-----------линия отрыва-----------------*/
        //            fields.SetField("comb_40", pfreq.Rows[0]["con_last_name"].ToString());
        //            fields.SetField("comb_41", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());
        //            fields.SetField("comb_42", pfreq.Rows[0]["con_nat"].ToString());
        //            if (pfreq.Rows[0]["con_birthday"].ToString() != "")
        //            {
        //                fields.SetField("comb_43", pfreq.Rows[0]["con_birthday"].ToString().Substring(0, 2));
        //                fields.SetField("comb_44", pfreq.Rows[0]["con_birthday"].ToString().Substring(3, 2));
        //                fields.SetField("comb_45", pfreq.Rows[0]["con_birthday"].ToString().Substring(6, 4));
        //            }
        //            if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
        //                fields.SetField("comb_75", "X");
        //            else
        //                fields.SetField("comb_74", "X");
        //            fields.SetField("comb_46", FirstUpper(pfreq.Rows[0]["dul_type"].ToString()));
        //            fields.SetField("comb_47", pfreq.Rows[0]["dul_ser"].ToString());
        //            fields.SetField("comb_48", pfreq.Rows[0]["dul_num"].ToString());

        //            if (pfreq.Rows[0]["card_tenure_to_dt"].ToString() != "")
        //            {
        //                fields.SetField("comb_57", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(0, 2));
        //                fields.SetField("comb_58", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(3, 2));
        //                fields.SetField("comb_59", pfreq.Rows[0]["card_tenure_to_dt"].ToString().Substring(6, 4));
        //            }

        //            fields.SetField("comb_49", pfreq.Rows[0]["ad_obl"].ToString().ToUpper());
        //            fields.SetField("comb_1_2", pfreq.Rows[0]["ad_obl"].ToString().ToUpper());

        //            if (pfreq.Rows[0]["ad_socr_rayon"].ToString() != "мкр.")
        //            {
        //                fields.SetField("comb_50", pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
        //                fields.SetField("comb_2_2", pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
        //            }
        //            else
        //            {
        //                fields.SetField("comb_50", "МКР. " + pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
        //                fields.SetField("comb_2_2", "МКР. " + pfreq.Rows[0]["ad_rayon"].ToString().ToUpper());
        //            }

        //            fields.SetField("comb_51", (pfreq.Rows[0]["ad_socr_town"].ToString()+" "+pfreq.Rows[0]["ad_town"].ToString()).ToUpper());
        //            fields.SetField("comb_3_2", (pfreq.Rows[0]["ad_socr_town"].ToString() + " " + pfreq.Rows[0]["ad_town"].ToString()).ToUpper());

        //            switch (pfreq.Rows[0]["ad_socr_street"].ToString())
        //            {
        //                case "пр-кт":
        //                    fields.SetField("comb_52", "ПРОСПЕКТ "+pfreq.Rows[0]["ad_street"].ToString().ToUpper());
        //                    fields.SetField("comb_4_2", "ПРОСПЕКТ " + pfreq.Rows[0]["ad_street"].ToString().ToUpper());
        //                    break;

        //                default:
        //                    fields.SetField("comb_52", pfreq.Rows[0]["ad_socr_street"].ToString().ToUpper()+" "+pfreq.Rows[0]["ad_street"].ToString().ToUpper());
        //                    fields.SetField("comb_4_2", pfreq.Rows[0]["ad_socr_street"].ToString().ToUpper() + " " + pfreq.Rows[0]["ad_street"].ToString().ToUpper());
        //                    break;
        //            }

        //            fields.SetField("comb_53", pfreq.Rows[0]["ad_house"].ToString());
        //            fields.SetField("comb_5_2", pfreq.Rows[0]["ad_house"].ToString());
        //            fields.SetField("comb_54", pfreq.Rows[0]["ad_corp"].ToString());
        //            fields.SetField("comb_6_2", pfreq.Rows[0]["ad_corp"].ToString());
        //            fields.SetField("comb_55", pfreq.Rows[0]["ad_stroenie"].ToString());
        //            fields.SetField("comb_7_2", pfreq.Rows[0]["ad_stroenie"].ToString());
        //            fields.SetField("comb_56", pfreq.Rows[0]["ad_flat"].ToString());
        //            fields.SetField("comb_8_2", pfreq.Rows[0]["ad_flat"].ToString());

        //            /*Сведения о принимающей стороне*/
        //            if (pfhost.Rows[0]["org_phis"].ToString() == "Организация")
        //            {
        //                fields.SetField("comb_43_2", "X");
        //                fields.SetField("comb_44_2", " ");
        //            }
        //            else
        //            {
        //                fields.SetField("comb_43_2", " ");
        //                fields.SetField("comb_44_2", "X");
        //            }
        //            fields.SetField("comb_10_2", pfhost.Rows[0]["last_name"].ToString().ToUpper());
        //            fields.SetField("comb_14_2", pfhost.Rows[0]["first_name"].ToString().ToUpper() + " "+ pfhost.Rows[0]["second_name"].ToString().ToUpper());

        //            if (pfhost.Rows[0]["birthday"].ToString() != "")
        //            {
        //                fields.SetField("comb_11_2", pfhost.Rows[0]["birthday"].ToString().Substring(0, 2));
        //                fields.SetField("comb_12_2", pfhost.Rows[0]["birthday"].ToString().Substring(3, 2));
        //                fields.SetField("comb_13_2", pfhost.Rows[0]["birthday"].ToString().Substring(6, 4));
        //            }
        //            fields.SetField("comb_15_2", pfhost.Rows[0]["doc"].ToString());
        //            fields.SetField("comb_16_2", pfhost.Rows[0]["doc_ser"].ToString());
        //            fields.SetField("comb_17_2", pfhost.Rows[0]["doc_num"].ToString());
        //            if (pfhost.Rows[0]["date_issue"].ToString() != "")
        //            {
        //                fields.SetField("comb_18_2", pfhost.Rows[0]["date_issue"].ToString().Substring(0, 2));
        //                fields.SetField("comb_19_2", pfhost.Rows[0]["date_issue"].ToString().Substring(3, 2));
        //                fields.SetField("comb_20_2", pfhost.Rows[0]["date_issue"].ToString().Substring(6, 4));
        //            }
        //            if (pfhost.Rows[0]["date_valid"].ToString() != "")
        //            {
        //                fields.SetField("comb_21_2", pfhost.Rows[0]["date_valid"].ToString().Substring(0, 2));
        //                fields.SetField("comb_22_2", pfhost.Rows[0]["date_valid"].ToString().Substring(3, 2));
        //                fields.SetField("comb_23_2", pfhost.Rows[0]["date_valid"].ToString().Substring(6, 4));
        //            }
        //            fields.SetField("comb_24_2", pfhost.Rows[0]["obl"].ToString().ToUpper());
        //            fields.SetField("comb_25_2", pfhost.Rows[0]["rayon"].ToString().ToUpper());
        //            fields.SetField("comb_26_2", pfhost.Rows[0]["town"].ToString().ToUpper());
        //            fields.SetField("comb_27_2", pfhost.Rows[0]["street"].ToString().ToUpper());
        //            fields.SetField("comb_28_2", pfhost.Rows[0]["house"].ToString().ToUpper());
        //            fields.SetField("comb_29_2", pfhost.Rows[0]["korp"].ToString().ToUpper());
        //            fields.SetField("comb_30_2", pfhost.Rows[0]["stro"].ToString().ToUpper());
        //            fields.SetField("comb_31_2", pfhost.Rows[0]["flat"].ToString().ToUpper());

        //            fields.SetField("comb_32_2", pfhost.Rows[0]["phone"].ToString().ToUpper());

        //            //перенести  - наименование орг.
        //            string tPerenos = pfhost.Rows[0]["org_name"].ToString();
        //            string res = "";
        //            string res2 = "";
        //            if (tPerenos.Length < 24)
        //            {
        //                res = tPerenos;
        //            }
        //            else
        //            {
        //                int i;
        //                string stemp = "";
        //                stemp = tPerenos.Substring(0, 24);
        //                i = stemp.LastIndexOf(" ");
        //                res = tPerenos.Substring(0, i + 1);
        //                res2 = tPerenos.Substring(i, tPerenos.Length - i);
        //            }
        //            fields.SetField("comb_33_2", res.ToUpper());
        //            fields.SetField("comb_34_2", res2.ToUpper());

        //            //адрес
        //            tPerenos = pfhost.Rows[0]["address"].ToString();
        //            res = "";
        //            res2 = "";
        //            if (tPerenos.Length < 24)
        //            {
        //                res = tPerenos;
        //            }
        //            else
        //            {
        //                int i;
        //                string stemp = "";
        //                stemp = tPerenos.Substring(0, 24);
        //                i = stemp.LastIndexOf(" ");
        //                res = tPerenos.Substring(0, i + 1);
        //                res2 = tPerenos.Substring(i, tPerenos.Length - i);
        //            }
        //            fields.SetField("comb_35_2", res.ToUpper());
        //            fields.SetField("comb_36_2", res2.ToUpper());

        //            fields.SetField("comb_37_2", pfhost.Rows[0]["inn"].ToString().ToUpper());



        //            fields.SetField("comb_41_2", pfreq.Rows[0]["con_last_name"].ToString());
        //            fields.SetField("comb_42_2", pfreq.Rows[0]["con_first_name"].ToString() + " " + pfreq.Rows[0]["con_second_name"].ToString());


        //            //for (int i = 1; i < 120; i++)
        //             //   fields.SetField("comb_" + i.ToString()+"_2", (i).ToString());
                    
        //            // stamper.FormFlattening = true;
        //            stamper.Close();
        //         }
                

        //        InsertPf(ReportName);
                

        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Log.Error(ClassName + "Function:GenerateNotify\n Error:" + e);
        //        ErrMsg = "Ошибка ^_^";
        //    }
        //    finally
        //    {
                
        //    }
        //    return ErrMsg;
        //}
        public void GeneratePetitionDeductDo()
        {
            
            /*Уведомление об отчислении*/
            string TemplateName = "PETITION.DEDUCT.DO.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;            
            string ReportName = GETNOW + "_Уведомление_отчисление_ДО.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                param.Add("fio", FirstUpper( pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("nat", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("birth", pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("dul", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("n", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dul_from", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("pr_num", pfreq.Rows[0]["exp_num"].ToString());
                param.Add("pr_from", pfreq.Rows[0]["exp_dt"].ToString());
                param.Add("osn", pfreq.Rows[0]["exp_expelled"].ToString());
                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    param.Add("p1", "отчислена");
                    param.Add("p2", "следующая иностранная гражданка");
                }
                else
                {
                    param.Add("p1", "отчислен");
                    param.Add("p2", "следующий иностранный гражданин");
                }
                FillDoc(NewPath, param);

                InsertPf(ReportName);

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionDeductDo\n Error:" + e);
                throw new Exception(e.Message);
            }
           
        }
        public void GenerateSprBank()
        {
            
            /*Справка в банк*/
            string TemplateName = "SPR_TO_BANK.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;            
            string ReportName = GETNOW + "_Справка_в_банк.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;   //Путь к заполненному шаблону
                File.Copy(TemplatePath, NewPath);

                param.Add("fio",FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("to", pfreq.Rows[0]["card_tenure_to_dt"].ToString());
                param.Add("addr", pfreq.Rows[0]["ad_full_address"].ToString());

                FillDoc(NewPath, param);

                InsertPf(ReportName);


            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateSprBank\n Error:" + e);
                throw new Exception(e.Message);
            }
            
        }
        public void GeneratePetitionOut(int s1)
        {           
            /*уведомление об отчислении*/           
            if (s1 == -1)
            {
                throw new Exception("Укажите входные параметры");               
            }
            string TemplateName = "PETITION.OUT.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Уведомление(отчисление)УФМС.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;   //Путь к заполненному шаблону
                File.Copy(TemplatePath, NewPath);

                if (s1 == 0 )
                {
                    param.Add("p1", "X");
                    param.Add("p2", string.Empty);
                }
                else
                {
                    param.Add("p1", string.Empty);
                    param.Add("p2", "X");
                }
                param.Add("l",pfreq.Rows[0]["con_last_name"].ToString());
                param.Add("lu", pfreq.Rows[0]["con_last_enu"].ToString());
                param.Add("f", pfreq.Rows[0]["con_first_name"].ToString());
                param.Add("fu", pfreq.Rows[0]["con_first_enu"].ToString());
                param.Add("m", pfreq.Rows[0]["con_second_name"].ToString());
                param.Add("mu", pfreq.Rows[0]["con_second_enu"].ToString());
                param.Add("br",pfreq.Rows[0]["con_birthday"].ToString());
                
                if (pfreq.Rows[0]["con_birth_town"].ToString() == "")
                    param.Add("brcon",pfreq.Rows[0]["con_birth_country"].ToString());
                else
                    param.Add("brcon", pfreq.Rows[0]["con_birth_country"].ToString()+", " + pfreq.Rows[0]["con_birth_town"].ToString() );

                param.Add("nat", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));

                if (pfreq.Rows[0]["con_sex"].ToString() == "ЖЕНСКИЙ")
                {
                    param.Add("p3", " ");
                    param.Add("p4", "X");
                }
                else
                {
                    param.Add("p3", "X");
                    param.Add("p4", " ");
                }

                param.Add("dul", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("num", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dfr", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("dto", pfreq.Rows[0]["dul_validity"].ToString());

                param.Add("addr",pfreq.Rows[0]["ad_full_address"].ToString());
                
                switch(pfreq.Rows[0]["doc_type"].ToString())
                {
                    case "РВП":
                        param.Add("post", "РВП");
                        break;
                    case "ВНЖ":
                        param.Add("post", "ВНЖ");
                        break;
                    default:
                        param.Add("post","месту пребывания");
                        break;
                }
                param.Add("mfr", pfreq.Rows[0]["card_entry_dt"].ToString());
                param.Add("mto",pfreq.Rows[0]["card_tenure_to_dt"].ToString());
                //заполняем только если виза
                if (pfreq.Rows[0]["doc_type"].ToString() == "Виза")
                {
                    param.Add("kr", "многократная");
                    param.Add("cat", "обыкновенная");
                    switch (pfreq.Rows[0]["con_pos"].ToString())
                    {
                        case "студент":
                            param.Add("entity","учеба");
                            break;
                        case "аспирант":
                            param.Add("entity", "аспирантура");
                            break;
                        case "стажер":
                            param.Add("entity", "стажировка");
                            break;
                        case "курсант":
                            param.Add("entity", "курсы");
                            break;
                        default:
                            param.Add("entity", "");
                            break;
                    }
                    param.Add("vser", pfreq.Rows[0]["doc_ser"].ToString());
                    param.Add("vnum", pfreq.Rows[0]["doc_num"].ToString());
                    param.Add("ident", pfreq.Rows[0]["doc_ident"].ToString());
                    param.Add("fr", pfreq.Rows[0]["doc_validity_from_dt"].ToString());
                    param.Add("to", pfreq.Rows[0]["doc_validity_to_dt"].ToString());
                }
                else
                {
                    param.Add("kr", "");
                    param.Add("cat", "");
                    param.Add("entity", "");
                    param.Add("vser", "");
                    param.Add("vnum", "");
                    param.Add("ident", "");
                    param.Add("fr", "");
                    param.Add("to","");
                }

                param.Add("dd", pfreq.Rows[0]["agr_dt"].ToString());
                param.Add("dn", pfreq.Rows[0]["agr_num"].ToString());
                param.Add("ddfr", pfreq.Rows[0]["agr_from_dt"].ToString());
                param.Add("ddto", pfreq.Rows[0]["agr_to_dt"].ToString());

                param.Add("exp", pfreq.Rows[0]["exp_expelled"].ToString());

                string s = pfreq.Rows[0]["exp_num"].ToString() != "" ? pfreq.Rows[0]["exp_num"].ToString() : string.Empty;
                param.Add("expnum", s);
                param.Add("expdt", pfreq.Rows[0]["exp_dt"].ToString());

                if (pfreq.Rows[0]["teach_fp"].ToString() == "НАПРАВЛЕНИЕ")
                    param.Add("bud", "(студент обучался по направлению Минобрнауки России)");
                else
                    param.Add("bud", "");

                FillDoc(NewPath, param,"22");

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionOut\n Error:" + e);
                throw new Exception(e.Message);
            }
           
        }
        public void GeneratePetitionVisa(string s1, string s2)
        {
            /*Регистрация.Ходатайство.Смена визы*/
            if (s1 == "" || s2 == "")
                throw new Exception("Укажите входные параметры");                  
            
            string TemplateName = "PETITION.VISA.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Ходатайство_Смена_визы.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);
               
                param.Add("change",s1);
                param.Add("post",s2);
                param.Add("gr",pfreq.Rows[0]["gr"].ToString());
                param.Add("nationality", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("fio",FirstUpper(pfreq.Rows[0]["con_fio"].ToString()) );
                param.Add("birthday",pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("doc_ser",pfreq.Rows[0]["doc_ser"].ToString());
                param.Add("doc_num",pfreq.Rows[0]["doc_num"].ToString());
                param.Add("doc_issue_dt",pfreq.Rows[0]["doc_issue_dt"].ToString());
                param.Add("doc_validity_to_dt",pfreq.Rows[0]["doc_validity_to_dt"].ToString());
                param.Add("full_address",pfreq.Rows[0]["ad_full_address"].ToString());
                param.Add("card_tenure_to_dt",pfreq.Rows[0]["card_tenure_to_dt"].ToString());
                param.Add("p3",pfreq.Rows[0]["p3"].ToString());
                param.Add("p4",pfreq.Rows[0]["p4"].ToString());

                FillDoc(NewPath, param);
                

                InsertPf(ReportName);


            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionVisa\n Error:" + e);
                throw new Exception(e.Message);
            }
           
           
        }
        public void GenerateVisaGuarant()
        {           
            /*Гарантия*/
            string TemplateName = "VISA.GUARANT.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;           
            string ReportName = GETNOW + "_Гарантия.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {            
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                if (pfreq.Rows[0]["con_sex"].ToString() == "МУЖСКОЙ")
                {
                    param.Add("p1","иностранному гражданину");
                    param.Add("p2", "его");
                }
                else
                {
                    param.Add("p1", "иностранной гражданке");
                    param.Add("p2", "её");
                }

                param.Add("fio",FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("nat", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("pass", pfreq.Rows[0]["dul_ser"].ToString() + pfreq.Rows[0]["dul_num"].ToString());
                param.Add("pfrom",pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("ptill",pfreq.Rows[0]["dul_validity"].ToString());
                param.Add("med", pfreq.Rows[0]["con_med"].ToString());
                param.Add("addr",pfreq.Rows[0]["ad_full_address"].ToString());

                FillDoc(NewPath, param);

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaGuarant\n Error:" + e);
                throw new Exception(e.Message);
            }
           
        }
        public void GenerateVisaPetition(string s1,string s2)
        {
            /*Ходатайство на визу*/
            if (s1 == "" || s2 == "")
            {
                throw new Exception("Укажите входные параметры");                
            }

            string TemplateName = "";
            string TemplatePath = "";            
            string ReportName = GETNOW + "_Ходатайство_на_визу.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;

                TemplateName = "VISA.PETITION.docx";
                //if (pfreq.Rows[0]["teach_fp"].ToString() == "НАПРАВЛЕНИЕ")
                //    TemplateName = "VISA.PETITION.BUDGET.docx";
                //else
                //    TemplateName = "VISA.PETITION.CONTRACT.docx";
                TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
                File.Copy(TemplatePath, NewPath);

                string teach_fp = pfreq.Rows[0]["teach_fp"].ToString();

                if (teach_fp == "НАПРАВЛЕНИЕ")
                {                   
                    param.Add("docs", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='VISA.PET.BUD' AND code='<param1>';", null));
                }
                if (teach_fp == "КОНТРАКТ")
                {                   
                    param.Add("docs", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='VISA.PET.CONTR' AND code='<param1>';", null));
                }
                if (teach_fp == "ОБЩИЙ БЮДЖЕТ (ПРИКАЗ)")
                {                    
                    param.Add("docs", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='VISA.PET.TOTAL.BUD' AND code='<param1>';", null));
                }

                param.Add("fio", FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("p1",s1);
                param.Add("p2", s2);
                param.Add("birthday",pfreq.Rows[0]["con_birthday"].ToString());
                if (pfreq.Rows[0]["con_sex"].ToString()=="МУЖСКОЙ")
                {
                     param.Add("gr","гражданину");
                     param.Add("p3", "поставлен");
                     param.Add("p4", "имеющему");
                    param.Add("p5", "прибывшему");
                }
                else
                {
                     param.Add("gr", "гражданке");
                     param.Add("p3", "поставлена");
                     param.Add("p4", "имеющей");
                    param.Add("p5", "прибывшей");
                }
                
                param.Add("nat", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("dul", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("pass", pfreq.Rows[0]["dul_ser"].ToString() + pfreq.Rows[0]["dul_num"].ToString());

                string dul_issue = pfreq.Rows[0]["dul_issue"].ToString();
                param.Add("d1",dul_issue.Substring(0, 2));
                param.Add("d2", GetMonthPad(dul_issue.Substring(3, 2)));
                param.Add("d3", dul_issue.Substring(6, 4));

                string dul_validity = pfreq.Rows[0]["dul_validity"].ToString();
                param.Add("d4", dul_validity.Substring(0, 2));
                param.Add("d5", GetMonthPad(dul_validity.Substring(3, 2)));
                param.Add("d6", dul_validity.Substring(6, 4));

                param.Add("addr", pfreq.Rows[0]["ad_full_address"].ToString());
                param.Add("kpp", pfreq.Rows[0]["card_kpp"].ToString());
                param.Add("vser", pfreq.Rows[0]["doc_ser"].ToString());
                param.Add("vnum", pfreq.Rows[0]["doc_num"].ToString());

                string doc_validity_from_dt = pfreq.Rows[0]["doc_validity_from_dt"].ToString();
                param.Add("v1", doc_validity_from_dt.Substring(0, 2));
                param.Add("v2", GetMonthPad(doc_validity_from_dt.Substring(3, 2)));
                param.Add("v3",doc_validity_from_dt.Substring(6, 4));

                string doc_validity_to_dt = pfreq.Rows[0]["doc_validity_to_dt"].ToString();
                param.Add("v4", doc_validity_to_dt.Substring(0, 2));
                param.Add("v5",GetMonthPad(doc_validity_to_dt.Substring(3, 2)));
                param.Add("v6", doc_validity_to_dt.Substring(6, 4));

                string doc_validity_to_dt_1 = pfreq.Rows[0]["doc_validity_to_dt_1"].ToString();
                param.Add("s1",doc_validity_to_dt_1.Substring(0, 2));
                param.Add("s2", GetMonthPad(doc_validity_to_dt_1.Substring(3, 2)));
                param.Add("s3",doc_validity_to_dt_1.Substring(6, 4));

                //проверить!
                string card_tenure_to_dt = pfreq.Rows[0]["card_tenure_to_dt"].ToString();
                param.Add("m1",card_tenure_to_dt.Substring(0, 2));
                param.Add("m2", GetMonthPad(card_tenure_to_dt.Substring(3, 2)));
                param.Add("m3", card_tenure_to_dt.Substring(6, 4));

                param.Add("mfrom", pfreq.Rows[0]["card_entry_dt"].ToString());

                FillDoc(NewPath, param);

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaPetition\n Error:" + e);
                throw new Exception(e.Message);
            }
           
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
        public void GeneratePetitionPassport(string s1, string s2)
        {
            /*Регистрация.Ходатайство.Смена паспорта*/
            if (s1 == "" || s2== "")
            {
                throw new Exception("Укажите входные параметры");                
            }

            string TemplateName = "PETITION.PASSPORT.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;            
            string ReportName = GETNOW + "_Ходатайство_Смена_паспорта.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                param.Add("gr",pfreq.Rows[0]["gr"].ToString());
                param.Add("nationality", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("fio",FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));     
                param.Add("dul_ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("dul_num", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dul_issue", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("dul_validity", pfreq.Rows[0]["dul_validity"].ToString());
                param.Add("doc_ser", pfreq.Rows[0]["doc_ser"].ToString());
                param.Add("doc_num", pfreq.Rows[0]["doc_num"].ToString());
                param.Add("doc_issue_dt", pfreq.Rows[0]["doc_issue_dt"].ToString());
                param.Add("doc_validity_to_dt", pfreq.Rows[0]["doc_validity_to_dt"].ToString());
                param.Add("full_address",pfreq.Rows[0]["ad_full_address"].ToString());
                param.Add("card_tenure_to_dt",pfreq.Rows[0]["card_tenure_to_dt"].ToString());
                param.Add("p3", pfreq.Rows[0]["p3"].ToString());
                param.Add("p4",pfreq.Rows[0]["p4"].ToString());
                param.Add("change", s1);
                param.Add("post", s2);

                FillDoc(NewPath, param);

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionPassport\n Error:" + e);
                throw new Exception(e.Message);
            }
            
        }
        public void GeneratePetitionLost()
        {
            
            /*Регистрация.Ходатайство.Утеря уведомления*/
            string TemplateName = "ARRIVAL.LOST.docx";
            string TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;            
            string ReportName = GETNOW + "_Ходатайство_Утеря_уведомления.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;
                File.Copy(TemplatePath, NewPath);

                param.Add("gr", pfreq.Rows[0]["gr"].ToString());
                param.Add("nationality", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("fio", FirstUpper(pfreq.Rows[0]["con_fio"].ToString()));
                param.Add("birthday", pfreq.Rows[0]["con_birthday"].ToString());
                param.Add("dul_type", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("dul_ser", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("dul_num",pfreq.Rows[0]["dul_num"].ToString());
                param.Add("dul_issue",pfreq.Rows[0]["dul_issue"].ToString());               
                param.Add("full_address", pfreq.Rows[0]["ad_full_address"].ToString());
                //param.Add("card_entry_dt", pfreq.Rows[0]["card_entry_dt"].ToString());
                param.Add("card_tenure_from_dt", pfreq.Rows[0]["card_tenure_from_dt"].ToString()); 
                param.Add("card_tenure_to_dt",pfreq.Rows[0]["card_tenure_to_dt"].ToString());
               

                FillDoc(NewPath, param);

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GeneratePetitionLost\n Error:" + e);
                throw new Exception(e.Message);
            }
           
        }
        public void GenerateVisaAnketa(string s1, string s2, string s3, string s4, string s5)
        {
            //if (cmbVisaAction.Text == "" || cmbVisaKrat.Text == "" || cmbVisaCat.Text == "" || cmbVisaPurpose.Text == "" || (cmbVisaSubCat.Text == "" && cmbVisaCat.SelectedIndex == 0))
             
            /*Визовая анкета: анкета*/
            if (s1 == "" || s2 == "" || s3 == "" || s5 == "" || (s4 == "" && s3 == "0"))
            {
               throw new Exception("Укажите входные параметры");               
            }

            string TemplateName = "";           
            string TemplatePath ="";           
            string ReportName = GETNOW + "_Визовая_анкета.docx";
            Dictionary<string, string> param = new Dictionary<string, string>();

            try
            {
                DataTable pfreq = DB.QueryTableMultipleParams(pref.PfRequest, new List<object> { pref.CONTACTID });
                Directory.CreateDirectory(pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO);
                string NewPath = pref.FULLREPORTPATCH + pfreq.Rows[0]["con_nat"].ToString().ToUpper() + @"\" + pref.CONFIO + ReportName;

                TemplateName = "Anketa.docx";
                
                TemplatePath = Directory.GetCurrentDirectory() + @"\template\" + TemplateName;
                File.Copy(TemplatePath, NewPath);

                string teach_fp = pfreq.Rows[0]["teach_fp"].ToString();
                string tp = "";
                if (teach_fp == "НАПРАВЛЕНИЕ")
                {
                    tp = "ANK.BUD.INV";
                    //param.Add("inv1", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.BUD.INV' AND code='<param1>';", null));
                    //param.Add("inv2", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.BUD.INV' AND code='<param2>';", null));
                    //param.Add("inv3", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='ANK.BUD.INV' AND code='<param3>';", null));
                }
                if (teach_fp == "КОНТРАКТ")
                {
                    tp = "ANK.KONTR.INV";
                    //param.Add("inv1", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.KONTR.INV' AND code='<param1>';", null));
                    //param.Add("inv2", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.KONTR.INV' AND code='<param2>';", null));
                    //param.Add("inv3", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='ANK.KONTR.INV' AND code='<param3>';", null));
                }
                if (teach_fp == "ОБЩИЙ БЮДЖЕТ (ПРИКАЗ)")
                {
                    tp = "ANK.TOTAL.BUD";
                    //param.Add("inv1", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.TOTAL.BUD' AND code='<param1>';", null));
                    //param.Add("inv2", DB.GetTableValue("SELECT value FROM cmodb.lov where type='ANK.TOTAL.BUD' AND code='<param2>';", null));
                    //param.Add("inv3", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='ANK.TOTAL.BUD' AND code='<param3>';", null));
                }

                param.Add("inv1", DB.GetTableValue("SELECT value FROM cmodb.lov where type='"+tp+"' AND code='<param1>';", null));
                param.Add("inv2", DB.GetTableValue("SELECT value FROM cmodb.lov where type='" + tp + "' AND code='<param2>';", null));
                param.Add("inv3", DB.GetTableValue("SELECT msg FROM cmodb.lov where type='" + tp + "' AND code='<param3>';", null));

                //tp = DB.GetTableValue("SELECT msg FROM cmodb.lov where type='" + tp + "' AND code='<param3>';", null);
                //tp = tp + new string(' ', 850 - tp.Length)+"_";
                //param.Add("inv3", tp);

                switch (s1)
                {   //оформить
                    case "0":                       
                        param.Add("p43", string.Empty);
                        param.Add("p44", "оформить");                        
                        param.Add("p45", "продлить");
                        param.Add("p46", string.Empty);                        
                        param.Add("p47", "восстановить");
                        param.Add("p48", string.Empty);
                        break;
                    //продлить
                    case "1":                       
                        
                        param.Add("p43", "оформить");
                        param.Add("p44", string.Empty);
                        param.Add("p45", string.Empty);
                        param.Add("p46", "продлить");                        
                        param.Add("p47", "восстановить");
                        param.Add("p48", string.Empty);
                        break;
                    //восстановить
                    case "2":                       
                        
                        param.Add("p43", "оформить");
                        param.Add("p44", string.Empty);                       
                        param.Add("p45", "продлить");
                        param.Add("p46", string.Empty);
                        param.Add("p47", string.Empty);
                        param.Add("p48", "восстановить");
                        break;
                }
                param.Add("p15",pfreq.Rows[0]["con_last_name"].ToString());
                param.Add("p16",pfreq.Rows[0]["con_last_enu"].ToString());
                param.Add("p17",pfreq.Rows[0]["con_first_name"].ToString());
                param.Add("p18",pfreq.Rows[0]["con_first_enu"].ToString());
                if (pfreq.Rows[0]["con_second_name"].ToString() != "")
                    param.Add("p19", pfreq.Rows[0]["con_second_name"].ToString());
                else
                    param.Add("p19", string.Empty);
                if (pfreq.Rows[0]["con_second_enu"].ToString() != "")
                    param.Add("p20", pfreq.Rows[0]["con_second_enu"].ToString());
                else
                    param.Add("p20", string.Empty);

                param.Add("p41", s5);/*цель продления*/

                switch (s2)
                {   
                    case "Однократная":
                        param.Add("p1", "X");
                        param.Add("p2", string.Empty);
                        param.Add("p3", string.Empty);
                        break;
                    case "Двукратная":
                        param.Add("p1", string.Empty);
                        param.Add("p2", "X");
                        param.Add("p3", string.Empty);
                        break;
                    case "Многократная":
                        param.Add("p1", string.Empty);
                        param.Add("p2", string.Empty);
                        param.Add("p3", "X");
                        break;
                }
                switch (s3) /*категория*/
                {
                    case "0":
                        param.Add("p4", "X");
                        param.Add("p5", string.Empty);
                        param.Add("p6", string.Empty);
                        break;
                    case "1":
                        param.Add("p4", string.Empty);
                        param.Add("p5", "X");
                        param.Add("p6", string.Empty);
                        break;
                    case "2":
                        param.Add("p4", string.Empty);
                        param.Add("p5", string.Empty);
                        param.Add("p6", "X");
                        break;
                }
                

                if (s3 == "0") /*если обыкновенная*/
                {
                    switch (s4) /*подкатегория*/
                    {
                        case "0":
                            param.Add("p7", "X");
                            param.Add("p8", string.Empty);
                            param.Add("p9", string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "1":
                            param.Add("p7", string.Empty);
                            param.Add("p8", "X");
                            param.Add("p9", string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "2":
                            param.Add("p7",string.Empty);
                            param.Add("p8",string.Empty);
                            param.Add("p9","X");
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "3":
                            param.Add("p7", string.Empty);
                            param.Add("p8", string.Empty);
                            param.Add("p9", string.Empty);
                            param.Add("p10", "X");
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "4":
                            param.Add("p7", string.Empty);
                            param.Add("p8", string.Empty);
                            param.Add("p9", string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", "X");
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "5":
                            param.Add("p7", string.Empty);
                            param.Add("p8", string.Empty);
                            param.Add("p9", string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", "X");
                            param.Add("p13", string.Empty);
                            param.Add("p14", string.Empty);
                            break;
                        case "6":
                            param.Add("p7", string.Empty);
                            param.Add("p8", string.Empty);
                            param.Add("p9", string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", "X");
                            param.Add("p14", string.Empty);
                            break;
                        case "7":
                            param.Add("p7",string.Empty);
                            param.Add("p8",string.Empty);
                            param.Add("p9",string.Empty);
                            param.Add("p10", string.Empty);
                            param.Add("p11", string.Empty);
                            param.Add("p12", string.Empty);
                            param.Add("p13", string.Empty);
                            param.Add("p14", "X");
                            break;
                    }
                }
                else
                {
                    param.Add("p7", string.Empty);
                    param.Add("p8", string.Empty);
                    param.Add("p9", string.Empty);
                    param.Add("p10", string.Empty);
                    param.Add("p11", string.Empty);
                    param.Add("p12", string.Empty);
                    param.Add("p13", string.Empty);
                    param.Add("p14", string.Empty);
                }

                param.Add("p21", pfreq.Rows[0]["con_birthday"].ToString());
                //Regex.Replace(StringToCap, @"\w+", new MatchEvaluator(CapitalizeString))

                string con_birth_country = Regex.Replace(pfreq.Rows[0]["con_birth_country"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                string con_birth_town = Regex.Replace(pfreq.Rows[0]["con_birth_town"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString));
                if (con_birth_town == "")
                    param.Add("p22", con_birth_country);
                else
                    param.Add("p22", con_birth_country + "," + con_birth_town);

                if (pfreq.Rows[0]["con_sex"].ToString()=="МУЖСКОЙ")
                {
                    param.Add("p28", "X");
                    param.Add("p29", string.Empty);
                }
                else
                {
                    param.Add("p28", string.Empty);
                    param.Add("p29","X");
                }

                param.Add("p23", Regex.Replace(pfreq.Rows[0]["con_nat"].ToString(), @"\w+", new MatchEvaluator(CapitalizeString)));
                param.Add("p40", pfreq.Rows[0]["dul_type"].ToString().ToLower());
                param.Add("p24", pfreq.Rows[0]["dul_ser"].ToString());
                param.Add("p25", pfreq.Rows[0]["dul_num"].ToString());
                param.Add("p26", pfreq.Rows[0]["dul_issue"].ToString());
                param.Add("p27", pfreq.Rows[0]["dul_validity"].ToString());
                param.Add("p30", "г. Владимир");

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
                param.Add("p31", res);
                param.Add("p42", res2);

                param.Add("p32",pfreq.Rows[0]["con_pos"].ToString());
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
                param.Add("p49", res);
                param.Add("p50", res2);
                /*--------------------------------*/
                //адрес проживания
                tPerenos = pfreq.Rows[0]["ad_full_address"].ToString();
                res = "";
                res2 = "";
                if (tPerenos.Length < 35)
                {
                    res = tPerenos;
                }
                else
                {
                    int i;
                    string stemp = "";
                    stemp = tPerenos.Substring(0, 35);
                    i = stemp.LastIndexOf(" ");
                    res = tPerenos.Substring(0, i + 1);
                    res2 = tPerenos.Substring(i, tPerenos.Length - i);
                }
                param.Add("ADDR1", res);
                param.Add("ADDR2", res2);
                /*--------------------------------*/
                param.Add("p34", pfreq.Rows[0]["doc_ser"].ToString());
                param.Add("p35", pfreq.Rows[0]["doc_num"].ToString());
                param.Add("p36", pfreq.Rows[0]["doc_ident"].ToString());
                param.Add("p37", pfreq.Rows[0]["doc_validity_from_dt"].ToString());
                param.Add("p38", pfreq.Rows[0]["doc_validity_to_dt"].ToString());
                param.Add("p39", pfreq.Rows[0]["doc_invite_num"].ToString());

                if (DB.GetTableValue("select count(*) from cmodb.children where contact_id=:param1;", new List<object> { pref.CONTACTID }) != "0")
                {

                    int ii = 0;
                    foreach (DataRow row in DB.QueryTableMultipleParams(pref.GetChildSql, new List<object> { pref.CONTACTID }).Rows)
                    {                       
                        ii++;
                        param.Add("d" + ii.ToString() + "f", row.ItemArray[0].ToString());
                        param.Add("d" + ii.ToString() + "d", row.ItemArray[1].ToString());
                        param.Add("d" + ii.ToString() + "g", row.ItemArray[4].ToString());
                        param.Add("d" + ii.ToString() + "a", row.ItemArray[2].ToString());

                    }
                    if (ii != 4)
                    {
                        if (ii == 0) ii = 1;
                        for (int k = ii + 1; k <= 4; k++)
                        {
                            param.Add("d" + k.ToString() + "f", String.Empty);
                            param.Add("d" + k.ToString() + "d", String.Empty);
                            param.Add("d" + k.ToString() + "g", String.Empty);
                            param.Add("d" + k.ToString() + "a", String.Empty);
                        }
                    }
                }
                else
                {
                    for (int k = 1; k <= 4; k++)
                    {
                        param.Add("d" + k.ToString() + "f", String.Empty);
                        param.Add("d" + k.ToString() + "d", String.Empty);
                        param.Add("d" + k.ToString() + "g", String.Empty);
                        param.Add("d" + k.ToString() + "a", String.Empty);
                    }
                }
                FillDoc(NewPath, param,"24");

                InsertPf(ReportName);
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateVisaAnketa\n Error:" + e);
                throw new Exception(e.Message);
            }
           
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
           
            XLWorkbook excelApp1XML = new ClosedXML.Excel.XLWorkbook(NewFile);
            string currentSheet = "стр.1";
            string currentSheet2 = "стр.2";          
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
                if (pref.DELEGATE == "Y")
                {
                    str = pfreq.Rows[0]["delegate_last_name"].ToString().ToUpper();
                    for (int i = 0; i < str.Length; i++)
                    {
                        sheetExcel1XML.Range(arSved[i]).Value = str[i].ToString();
                    }
                    str = pfreq.Rows[0]["delegate_first_name"].ToString().ToUpper();
                    for (int i = 0; i < str.Length; i++)
                    {
                        sheetExcel1XML.Range(arSved2[i]).Value = str[i].ToString();
                    }
                    str = "";
                }
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
                //for (int i = 0; i < str.Length; i++)
                //{
                    sheetExcel1XML.Range(arFlatB[0]).Value = str;
                    sheetExcel2XML.Range(arFlat2[0]).Value = str;
                //}
                str = "";

                /*Сведения о принимающей стороне*/

                if (pfhost.Rows[0]["org_phis"].ToString() == "Организация")
                {                    
                    sheetExcel2XML.Range("EE30").Value = 'X'.ToString();
                    sheetExcel2XML.Range("FC30").Value = String.Empty;
                }
                else
                {
                    sheetExcel2XML.Range("EE30").Value = String.Empty;
                    sheetExcel2XML.Range("FC30").Value = 'X'.ToString();
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

                InsertPf(ReportName);
               

            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:GenerateNotifyXls\n Error:" + e);
                ErrMsg = "Ошибка ^_^";
            }
            finally
            {  
               

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
                                      "BK51","BO51","BS51","BW51","CA51","CE51","CI51","CM51","CQ51","CU51" };
        string[] arSved2 = new string[]{"AA53","AE53","AI53","AM53","AQ53","AU53","AY53","BC53","BG53",
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
        string[] arHouseB = new string[] { "AQ93", "AU93", "AY93", "BC93" };
        string[] arKorpB = new string[] { "BS93", "BW93", "CA93", "CE93" };
        string[] arStroB = new string[] { "CU93", "CY93", "DC93", "DG93" };
        string[] arFlatB = new string[] { "DK93" };
        string[] arTenureB = new string[] { "AQ97", "AU97", "BG97", "BK97", "BS97", "BW97", "CA97", "CE97" };
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
        string[] arHouse2 = new string[] { "AY24", "BC24", "BG24", "BK24" };
        string[] arKorp2 = new string[] { "CA24", "CE24", "CI24", "CM24" };
        string[] arStro2 = new string[] { "DC24", "DG24", "DK24", "DO24" };
        string[] arFlat2 = new string[] { "DS24" };
        string[] arPhone2 = new string[] { "W28", "AA28", "AE28", "AI28", "AM28", "AQ28", "AU28", "AY28", "BC28", "BG28" };

        //принимающая сторона
        string[] arNameP = new string[]{"W32","AA32","AE32","AI32","AM32","AQ32","AU32","AY32","BC32","BG32","BK32","BO32",
                                  "BS32","BW32","CA32","CE32","CI32","CM32","CQ32"};
        string[] arLastNameP = new string[]{"W35","AA35","AE35","AI35","AM35","AQ35","AU35","AY35","BC35","BG35","BK35","BO35",
                                  "BS35","BW35","CA35","CE35","CI35","CM35","CQ35","CU35","CY35","DC35","DG35","DK35","DO35","DS35",
                                  "DW35","EA35","EE35","EI35","EM35","EQ35","EU35","EY35","FC35"};
        string[] arDateBirthP = new string[] { "DO32", "DS32", "EE32", "EI32", "EQ32", "EU32", "EY32", "FC32" };
        string[] arDocViewP = new string[] { "BC38", "BG38", "BK38", "BO38", "BS38", "BW38", "CA38", "CE38", "CI38", "CM38", "CQ38" };
        string[] arDocSerP = new string[] { "DC38", "DG38", "DK38", "DO38" };
        string[] arDocNumP = new string[] { "DW38", "EA38", "EE38", "EI38", "EM38", "EQ38", "EU38", "EY38", "FC38" };
        string[] arDateVidP = new string[] { "AA40", "AE40", "AQ40", "AU40", "BC40", "BG40", "BK40", "BO40" };
        string[] arDateExpiredP = new string[] { "CM40", "CQ40", "DC40", "DG40", "DO40", "DS40", "DW40", "EA40" };
        string[] arOblP = new string[]{"AE43","AI43","AM43","AQ43","AU43","AY43","BC43","BG43","BK43","BO43",
                                  "BS43","BW43","CA43","CE43","CI43","CM43","CQ43","CU43","CY43","DC43","DG43","DK43","DO43","DS43",
                                  "DW43","EA43","EE43","EI43","EM43","EQ43","EU43","EY43","FC43"};
        string[] arRayonP = new string[]{"W46","AA46","AE46","AI46","AM46","AQ46","AU46","AY46","BC46","BG46","BK46","BO46",
                                  "BS46","BW46","CA46","CE46","CI46","CM46","CQ46","CU46","CY46","DC46","DG46","DK46","DO46","DS46",
                                  "DW46","EA46","EE46","EI46","EM46","EQ46","EU46","EY46","FC46"};
        string[] arTownP = new string[]{"AE48","AI48","AM48","AQ48","AU48","AY48","BC48","BG48","BK48","BO48",
                                  "BS48","BW48","CA48","CE48","CI48","CM48","CQ48","CU48","CY48","DC48","DG48","DK48","DO48","DS48",
                                  "DW48","EA48","EE48","EI48","EM48","EQ48","EU48","EY48","FC48"};
        string[] arStreetP = new string[]{"W51","AA51","AE51","AI51","AM51","AQ51","AU51","AY51","BC51","BG51","BK51","BO51",
                                  "BS51","BW51","CA51","CE51","CI51","CM51","CQ51","CU51","CY51","DC51","DG51","DK51","DO51","DS51",
                                  "DW51","EA51","EE51","EI51","EM51","EQ51","EU51","EY51","FC51"};
        string[] arHouseP = new string[] { "S53", "W53", "AA53", "AE53" };
        string[] arKorpP = new string[] { "AQ53", "AU53", "AY53", "BC53" };
        string[] arStroP = new string[] { "BS53", "BW53", "CA53", "CE53" };
        string[] arFlatP = new string[] { "CU53", "CY53", "DC53", "DG53" };
        string[] arPhoneP = new string[] { "DS53", "DW53", "EA53", "EE53", "EI53", "EM53", "EQ53", "EU53", "EY53", "FC53" };
        string[] arOrgNameP = new string[]{"AA55","AE55","AI55","AM55","AQ55","AU55","AY55","BC55","BG55","BK55","BO55",
                                  "BS55","BW55","CA55","CE55","CI55","CM55","CQ55","CU55","CY55","DC55","DG55","DK55","DO55"   };

        string[] arOrgNameP2 = new string[]{"K58","O58","S58","W58","AA58","AE58","AI58","AM58","AQ58","AU58","AY58","BC58","BG58","BK58","BO58",
                                  "BS58","BW58","CA58","CE58","CI58","CM58","CQ58","CU58","CY58","DC58","DG58","DK58","DO58"};
        string[] arFactAdrP = new string[]{"AA60","AE60","AI60","AM60","AQ60","AU60","AY60","BC60","BG60","BK60","BO60",
                                  "BS60","BW60","CA60","CE60","CI60","CM60","CQ60","CU60","CY60","DC60","DG60","DK60","DO60"
                                  };
        string[] arFactAdrP2 = new string[]{"K62","O62","S62","W62","AA62","AE62","AI62","AM62","AQ62","AU62","AY62","BC62","BG62","BK62","BO62",
                                  "BS62","BW62","CA62","CE62","CI62","CM62","CQ62","CU62","CY62","DC62","DG62","DK62","DO62"};
        string[] arInnP = new string[] { "S64", "W64", "AA64", "AE64", "AI64", "AM64", "AQ64", "AU64", "AY64", "BC64", "BG64", "BK64" };
        //отрыв
        string[] arDateBirth2B = new string[] { "DO72", "DS72", "EE72", "EI72", "EQ72", "EU72", "EY72", "FC72" };
        string[] arName2B = new string[]{"W74","AA74","AE74","AI74","AM74","AQ74","AU74","AY74","BC74","BG74","BK74","BO74",
                                  "BS74","BW74","CA74","CE74","CI74","CM74","CQ74","CU74","CY74","DC74","DG74","DK74","DO74","DS74",
                                  "DW74","EA74","EE74","EI74","EM74","EQ74","EU74","EY74","FC74"};
        string[] arLastName2B = new string[]{"W76","AA76","AE76","AI76","AM76","AQ76","AU76","AY76","BC76","BG76","BK76","BO76",
                                  "BS76","BW76","CA76","CE76","CI76","CM76","CQ76","CU76","CY76","DC76","DG76","DK76","DO76","DS76",
                                  "DW76","EA76","EE76","EI76","EM76","EQ76","EU76","EY76","FC76"};
    }

   

}
