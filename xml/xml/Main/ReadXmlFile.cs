using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xml.Main
{
    public class ReadXmlFile
    {
        public XmlNode SearchSpecificNode(string path, string itemValue, string itemName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);

            XmlNode releasesNode = xmlDocument.DocumentElement;

            XmlNodeList itemNodes = xmlDocument.SelectNodes("//features//feature");

            foreach (XmlNode itemNode in itemNodes)
            {
                string itemValueXml = itemName.Equals("uid") ? itemNode.Attributes.GetNamedItem(itemName).InnerText :
                                                               itemNode.LastChild.SelectSingleNode(itemName).InnerText;
                if (itemValueXml != itemValue)
                {
                    releasesNode.RemoveChild(itemNode);
                }
            }

            return releasesNode;
        }

        public int NumberOfNodes(string path, string itemValue, string itemName)
        {
            XmlNode releasesNode = SearchSpecificNode(path, itemValue, itemName);
            return releasesNode.ChildNodes.Count;
        }

        public XmlNode NewReleasesNode(XmlNode oldToggle, XmlNode newToggle, int cont)
        {
            if (cont == 0)
            {
                return newToggle;
            }
            else
            {
                foreach (XmlNode node in newToggle)
                {
                    foreach (XmlNode node2 in oldToggle)
                    {
                        if (node.Attributes.GetNamedItem("uid").InnerText.Equals(node2.Attributes.GetNamedItem("uid").InnerText))
                        {
                            newToggle.RemoveChild(node);
                            break;
                        }
                    }
                    cont--;
                }
                return NewReleasesNode(oldToggle, newToggle, cont);
            }
        }

        public XmlNode DeletedReleasesNode(XmlNode oldToggle, XmlNode newToggle, int cont)
        {
            if (cont == 0)
            {
                return oldToggle;
            }
            else
            {
                foreach (XmlNode node in oldToggle)
                {
                    foreach (XmlNode node2 in newToggle)
                    {
                        if (node.Attributes.GetNamedItem("uid").InnerText.Equals(node2.Attributes.GetNamedItem("uid").InnerText))
                        {
                            oldToggle.RemoveChild(node);
                            break;
                        }
                    }
                    cont--;
                }
                return DeletedReleasesNode(oldToggle, newToggle, cont);
            }
        }

        public XmlNode UpdatedReleasesNode(XmlNode oldToggle, XmlNode newToggle, int cont)
        {
            if (cont < 0)
            {
                return oldToggle;
            }
            else
            {
                foreach (XmlNode node in oldToggle)
                {
                    foreach (XmlNode node2 in newToggle)
                    {
                        if (node.Attributes.GetNamedItem("uid").InnerText.Equals(node2.Attributes.GetNamedItem("uid").InnerText))
                        {
                            if (node.InnerXml.Equals(node2.InnerXml))
                            {
                                oldToggle.RemoveChild(node);
                                break;
                            }
                        }
                    }
                    cont--;
                }
                return UpdatedReleasesNode(oldToggle, newToggle, cont);
            }
        }

        public XmlNode DisplayUpdatedFields(XmlNode updatedNodes, string pathNewToggle, string oldFile, string newFile)
        {
            XmlNode param = null;
            foreach (XmlNode node in updatedNodes)
            {
                XmlNode currentNode = SearchSpecificNode(pathNewToggle, node.Attributes.GetNamedItem("uid").InnerText, "uid");
                foreach (XmlNode node2 in currentNode)
                {
                    Console.WriteLine("################### UPDATED FIELDS ###################");
                    Console.WriteLine("Feature uid: " + node2.Attributes.GetNamedItem("uid").InnerText);
                    if (node.Attributes.GetNamedItem("description").InnerText != node2.Attributes.GetNamedItem("description").InnerText)
                    {
                        Console.WriteLine(oldFile + " Description: " + node.Attributes.GetNamedItem("description").InnerText);
                        Console.WriteLine(newFile + " Description: " + node2.Attributes.GetNamedItem("description").InnerText);
                        Console.WriteLine("");
                    }
                    if (node.FirstChild != node2.FirstChild)
                    {
                        if (node.FirstChild.FirstChild.Attributes.GetNamedItem("value").InnerText != node2.FirstChild.FirstChild.Attributes.GetNamedItem("value").InnerText)
                        {
                            Console.WriteLine(oldFile + node.FirstChild.FirstChild.Attributes.GetNamedItem("name").InnerText + " : " + node.FirstChild.FirstChild.Attributes.GetNamedItem("value").InnerText);
                            Console.WriteLine(newFile + node2.FirstChild.FirstChild.Attributes.GetNamedItem("name").InnerText + " : " + node2.FirstChild.FirstChild.Attributes.GetNamedItem("value").InnerText);
                            Console.WriteLine("");
                        }
                        if (node.FirstChild.LastChild.Attributes.GetNamedItem("value").InnerText != node2.FirstChild.LastChild.Attributes.GetNamedItem("value").InnerText)
                        {
                            Console.WriteLine(oldFile + node.FirstChild.LastChild.Attributes.GetNamedItem("name").InnerText + " : " + node.FirstChild.LastChild.Attributes.GetNamedItem("value").InnerText);
                            Console.WriteLine(newFile + node2.FirstChild.LastChild.Attributes.GetNamedItem("name").InnerText + " : " + node2.FirstChild.LastChild.Attributes.GetNamedItem("value").InnerText);
                            Console.WriteLine("");
                        }

                    }
                    if (node.LastChild != node2.LastChild)
                    {
                        if (node.LastChild.SelectSingleNode("release").InnerText != node2.LastChild.SelectSingleNode("release").InnerText)
                        {
                            Console.WriteLine(oldFile + " release: " + node.LastChild.SelectSingleNode("release").InnerText);
                            Console.WriteLine(newFile + " release: " + node2.LastChild.SelectSingleNode("release").InnerText);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            return param;
        }

        public void Test()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("a", "a");
                try
                {
                    Uri contoso = new Uri("https://github.com/tanialop/curso-web-angular/raw/master/README.md");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.ReadKey();
                Console.ReadLine();
            }
        }
    }
}
