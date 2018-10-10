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

namespace Mig
{
    public partial class fPrefDB : Form
    {
        public string ClassName = "Class: PrefDB.cs\n";
        public fPrefDB()
        {
            InitializeComponent();

        }

        private void fPrefDB_Load(object sender, EventArgs e)
        {
            XmlDocument doc;
            try
            {
                doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\prefDB.xml");
                XmlNodeList nl = doc.GetElementsByTagName("DBNAME");
                tDB.Text = nl[0].InnerText;
                nl = doc.GetElementsByTagName("HOST");
                tHost.Text = nl[0].InnerText;
                nl = doc.GetElementsByTagName("DBPORT");
                tPort.Text = nl[0].InnerText;
                nl = doc.GetElementsByTagName("MIGDATA");
                tMig.Text = nl[0].InnerText;
                nl = doc.GetElementsByTagName("REPORTFOLDER");
                tRep.Text = nl[0].InnerText;
            }
            catch (Exception err)
            {

                Logger.Log.Error(ClassName + "Function:fPrefDB_Load\n Error:" + err.Message);
                MessageBox.Show("Не могу загрузить файл настроек!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlDocument doc;
            try {
                doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\prefDB.xml");
                doc.GetElementsByTagName("DBNAME")[0].InnerText = tDB.Text;
                doc.GetElementsByTagName("HOST")[0].InnerText = tHost.Text;
                doc.GetElementsByTagName("DBPORT")[0].InnerText = tPort.Text;
                doc.GetElementsByTagName("MIGDATA")[0].InnerText = tMig.Text;
                doc.GetElementsByTagName("REPORTFOLDER")[0].InnerText = tRep.Text;

                doc.Save(Application.StartupPath + @"\prefDB.xml");
                MessageBox.Show("Успешно сохранено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);


                this.DialogResult = DialogResult.OK;
            }
            catch(Exception err)
            {
                Logger.Log.Error(ClassName + "Function:fPrefDB_Load\n Error:" + err.Message);
                MessageBox.Show("Ошибка при сохранении настроек!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
