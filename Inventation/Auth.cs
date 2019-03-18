using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

namespace Inventation
{
    public partial class fAuth : Form
    {
        //public string ClassName = "Class: Auth.cs\n";
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
                doc.Load(Application.StartupPath + @"\Pref\pref.xml");
                XmlNodeList nl = doc.GetElementsByTagName("DBNAME");
                pref.Database = nl[0].InnerText;
                nl = doc.GetElementsByTagName("HOST");
                pref.HostName = nl[0].InnerText;                
                nl = doc.GetElementsByTagName("MIGDATA");
                pref.SharedFolder = nl[0].InnerText;               
               // nl = doc.GetElementsByTagName("NOTIFYTEMPLATE");
               

               // pref.FULLREPORTPATCH = pref.MIGDATA + pref.REPORTFOLDER;
            }
            catch (Exception e)
            {
                //Logger.Log.Error(ClassName + "Function:LoadXMLPreference\n Error:" + e);                
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
            DB.Open(pref.USER, pref.PASS, pref.HostName, pref.Database);              
            pref.AUTH = true;
           // Logger.Log.Info("Пользователь: " + pref.USER);    
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
