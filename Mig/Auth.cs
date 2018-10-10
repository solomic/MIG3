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
                doc.Load(Application.StartupPath + @"\prefDB.xml");
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

                pref.FULLREPORTPATCH = pref.MIGDATA + pref.REPORTFOLDER;
            }
            catch (Exception e)
            {
                Logger.Log.Error(ClassName + "Function:LoadXMLPreference\n Error:" + e);
                MessageBox.Show("Не могу загрузить файл настроек!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc = null;
            }
        }
        public void Auth()
        {


            // pref.CONSTR = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", pref.HOST, pref.PORT, pref.USER, pref.PASS, pref.DBNAME);
            try
            {
                LoadXMLPreference();
                string res = DB.Open(pref.USER, pref.PASS, pref.HOST, pref.PORT, pref.DBNAME);
                if (res != "")
                {
                    MessageBox.Show(res, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pref.POSITION = DB.GetTableValue("select pos from cmodb.pos where username=:param1;", new List<object> { pref.USER });
                pref.SUBPROGRAM = "migr";
                pref.AUTH = true;
                Logger.Log.Info("Пользователь: " + pref.USER);
                Logger.Log.Info("Позиция: " + pref.POSITION);

                MessageBox.Show("Успешное подключение", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception msg)
            {
                pref.AUTH = false;
                MessageBox.Show(msg.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            pref.USER = comboBox1.Text;           
            Auth();


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
          
        }

        private void fAuth_Load(object sender, EventArgs e)
        {

        }

       
    }
}
