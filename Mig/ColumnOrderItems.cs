using Pref;
using System;
using System.Collections.Generic;
using System.IO;
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
        public static Dictionary<string, List<ColumnOrderItem>> LoadColumnOrderXml(string path,string name)
        {
            Dictionary<string, List<ColumnOrderItem>> d = new Dictionary<string, List<ColumnOrderItem>>();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //string filename = Application.StartupPath + @"\Pref\" + pref.USER + "_filters.xml";
                string filename = path + @"\"+ name + "_filters.xml";
                if (File.Exists(filename))
                {
                    xmlDoc.Load(filename);
                    foreach (XmlNode childnode in xmlDoc.ChildNodes)
                    {
                        //Console.WriteLine(childnode.Name);
                        if (childnode.Name == "pref")
                        {
                            //цикл по фильтрам <filter>
                            foreach (XmlNode cn1 in childnode.ChildNodes)
                            {
                                //Console.WriteLine(cn1.Name);
                                List<ColumnOrderItem> columnOrder = new List<ColumnOrderItem>();
                                foreach (XmlNode colitems in cn1.ChildNodes)
                                {
                                    columnOrder.Add(new ColumnOrderItem
                                    {
                                        ColumnIndex = Convert.ToInt16(colitems.Attributes["ColumnIndex"].Value),
                                        DisplayIndex = Convert.ToInt16(colitems.Attributes["DisplayIndex"].Value),
                                        Visible = Convert.ToBoolean(colitems.Attributes["Visible"].Value),
                                        Width = Convert.ToInt16(colitems.Attributes["Width"].Value),
                                        ColumnName = colitems.Attributes["ColumnName"].Value

                                    });
                                }
                                d.Add(cn1.Attributes["name"].Value, columnOrder);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:LoadColumnOrderXml\n Error:" + ex.ToString());
            }
            return d;
        }
        static public void SaveColumnOrderXml(Dictionary<string, List<ColumnOrderItem>> dct, string path, string name)
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
                xmlDoc.Save(path + @"\" + name + "_filters.xml");
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ClassName + "Function:SaveColumnOrderXml\n Error:" + ex.ToString());
            }


        }

    }
}
