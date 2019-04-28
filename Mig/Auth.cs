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
using System.Xml;

namespace Mig
{
    public partial class fAuth : Form
    {
        public string ClassName = "Class: Auth.cs\n";
        public fAuth()
        {
            InitializeComponent();
        }
        public void LoadXMLPreference()
        {
            XmlDocument doc;
            try
            {
                doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\Pref\prefDB.xml");
                XmlNodeList nl = doc.GetElementsByTagName("DBNAME");
                pref.DBNAME = nl[0].InnerText;
                nl = doc.GetElementsByTagName("HOST");
                pref.HOST = nl[0].InnerText;
                nl = doc.GetElementsByTagName("DBPORT");
                pref.PORT = nl[0].InnerText;
                nl = doc.GetElementsByTagName("MIGDATA");
                pref.MIGDATA = nl[0].InnerText;
                nl = doc.GetElementsByTagName("REPORTFOLDER");
                pref.REPORTFOLDER = nl[0].InnerText;
                nl = doc.GetElementsByTagName("NOTIFYTEMPLATE");
                pref.NOTIFYTEMPLATE = nl[0].InnerText;
                nl = doc.GetElementsByTagName("INVCHECKDUL");
                int.TryParse(nl[0].InnerText, out pref.INVCHECKDUL);
                nl = doc.GetElementsByTagName("INVREPORTFOLDER");
                pref.INVREPORTFOLDER = nl[0].InnerText;
                
                pref.FULLREPORTPATCH = pref.MIGDATA + pref.REPORTFOLDER;
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:LoadXMLPreference\n Error:" + e);                
                throw new Exception("Не могу загрузить файл настроек prefDB.xml!");
            }
            finally
            {
                doc = null;
            }
        }
        public void Auth()
        {
            pref.AUTH = false;
            // pref.CONSTR = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", pref.HOST, pref.PORT, pref.USER, pref.PASS, pref.DBNAME);
            LoadXMLPreference();
            DB.Open(pref.USER, pref.PASS, pref.HOST, pref.PORT, pref.DBNAME);              
            pref.AUTH = true;
            Logger.Log.Info("Пользователь: " + pref.USER);    
            MessageBox.Show("Успешное подключение", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                pref.USER = comboBox1.Text;
                pref.PASS = tPass.Text;
                Auth();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       

       
    }
}
