using Pref;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Mig
{
    

    public class ColumnOrderItem
    {
        public int DisplayIndex { get; set; }
        public int Width { get; set; }
        public bool Visible { get; set; }
        public int ColumnIndex { get; set; }
        public String ColumnName { get; set; }
    }
    public static class XMLMeth
    {
        static string ClassName = "XMLMeth";
        static public void SaveColumnOrderXml(Dictionary<string, List<ColumnOrderItem>> dct)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                XmlNode rootNode = xmlDoc.CreateElement("pref");

                xmlDoc.AppendChild(rootNode);
                foreach (var dctitem in dct)
                {
                    XmlNode userNode = xmlDoc.CreateElement("filter");
                    XmlAttribute attribute = xmlDoc.CreateAttribute("name");
                    attribute.Value = dctitem.Key;
                    userNode.Attributes.Append(attribute);
                    //userNode.InnerText = "John Doe";
                    rootNode.AppendChild(userNode);

                    if (dctitem.Value != null)
                    {
                        foreach (var colitem in dctitem.Value)
                        {
                            XmlNode column = xmlDoc.CreateElement("column");
                            XmlAttribute attribute1 = xmlDoc.CreateAttribute("ColumnName");
                            attribute1.Value = colitem.ColumnName;
                            XmlAttribute attribute2 = xmlDoc.CreateAttribute("ColumnIndex");
                            attribute2.Value = colitem.ColumnIndex.ToString();
                            XmlAttribute attribute3 = xmlDoc.CreateAttribute("Visible");
                            attribute3.Value = colitem.Visible.ToString();
                            XmlAttribute attribute4 = xmlDoc.CreateAttribute("Width");
                            attribute4.Value = colitem.Width.ToString();
                            XmlAttribute attribute5 = xmlDoc.CreateAttribute("DisplayIndex");
                            attribute5.Value = colitem.DisplayIndex.ToString();
                            column.Attributes.Append(attribute1);
                            column.Attributes.Append(attribute2);
                            column.Attributes.Append(attribute3);
                            column.Attributes.Append(attribute4);
                            column.Attributes.Append(attribute5);
                            userNode.AppendChild(column);
                        }
                    }
                }
                XmlDeclaration dcl;
                dcl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                xmlDoc.InsertBefore(dcl, rootNode);
                xmlDoc.Save(Application.StartupPath + @"\Pref\" + pref.USER + "_filters.xml");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:SaveColumnOrderXml\n Error:" + ex.ToString());
            }


        }

    }
}
