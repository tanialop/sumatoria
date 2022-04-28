using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using xml.Main;

namespace xml.UseCase
{
    public class UseCase1 : ReadXmlFile
    {
        protected const string ReleaseName1 = "22.2";
        protected const string ReleaseName2 = "22.1";
        protected const string PathNewToggle = @"C:\Toggle\git\ToggleFeature\ToggleFeature\Resources\newToggles.xml";
        protected const string PathOldToggle = @"C:\Toggle\git\ToggleFeature\ToggleFeature\Resources\toggles.xml";

        [Test]
        public void Number_Of_NewToggle_OldToggle()
        {
            var numberOfNodesNewToggle1 = NumberOfNodes(PathNewToggle, ReleaseName1, "release");
            var numberOfNodesOldToggle1 = NumberOfNodes(PathOldToggle, ReleaseName1, "release");

            var numberOfNodesNewToggle2 = NumberOfNodes(PathNewToggle, ReleaseName2, "release");
            var numberOfNodesOldToggle2 = NumberOfNodes(PathOldToggle, ReleaseName2, "release");

            Console.WriteLine("####### Release:  " + ReleaseName1 + "  Repository: Staging Feature Toggles #######");
            Console.WriteLine("In the new Toggle.xml file there is: " + numberOfNodesNewToggle1 + " features nodes");
            Console.WriteLine("In the old Toggle.xml file there is: " + numberOfNodesOldToggle1 + " features nodes");

            Console.WriteLine("####### Release:  " + ReleaseName2 + "  Repository: Staging Feature Toggles #######");
            Console.WriteLine("In the new Toggle.xml file there is: " + numberOfNodesNewToggle2 + " features nodes");
            Console.WriteLine("In the old Toggle.xml file there is: " + numberOfNodesOldToggle2 + " features nodes");
        }

        [Test]
        public void New_Features_Toggle()
        {
            XmlNode oldNode = SearchSpecificNode(PathOldToggle, ReleaseName1, "release");
            XmlNode newNode = SearchSpecificNode(PathNewToggle, ReleaseName1, "release");

            XmlNode nodes = NewReleasesNode(oldNode, newNode, newNode.ChildNodes.Count);
            foreach (XmlNode node in nodes)
            {
                Console.WriteLine("##### In the new Toggle.xml file the following Features were added #####");
                Console.WriteLine("Feature uid: " + node.Attributes.GetNamedItem("uid").InnerText);
                Console.WriteLine("Feature param: " + node.SelectSingleNode("flipstrategy").InnerXml);
                Console.WriteLine("Feature owner: " + node.InnerText);
            }
        }

        [Test]
        public void Deleted_Features_Toggle()
        {
            XmlNode oldNode = SearchSpecificNode(PathOldToggle, ReleaseName1, "release");
            XmlNode newNode = SearchSpecificNode(PathNewToggle, ReleaseName1, "release");

            XmlNode nodes = DeletedReleasesNode(oldNode, newNode, oldNode.ChildNodes.Count);
            foreach (XmlNode node in nodes)
            {
                Console.WriteLine("###### In the new Toggle.xml file the following Features were deleted #####");
                Console.WriteLine("Feature uid: " + node.Attributes.GetNamedItem("uid").InnerText);
                Console.WriteLine("Feature param: " + node.SelectSingleNode("flipstrategy").InnerXml);
                Console.WriteLine("Feature owner: " + node.InnerText);
            }
        }

        [Test]
        public void Updated_Features_Toggle()
        {
            XmlNode oldNode = SearchSpecificNode(PathOldToggle, ReleaseName1, "release");
            XmlNode newNode = SearchSpecificNode(PathNewToggle, ReleaseName2, "release");

            XmlNode nodes = UpdatedReleasesNode(oldNode, newNode, oldNode.ChildNodes.Count);
            DisplayUpdatedFields(nodes, PathNewToggle, "Old: ", "New: ");
        }

        [Test]
        public void Updated_Test()
        {
            Test();
        }
    }
}
