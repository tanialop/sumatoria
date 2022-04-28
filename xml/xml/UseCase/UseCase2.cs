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
    public class UseCase2 : ReadXmlFile
    {
        protected const string ReleaseName = "22.2";
        protected const string StagingToggle = @"C:\Toggle\git\ToggleFeature\ToggleFeature\Resources\newToggles.xml";
        protected const string ProductionToggle = @"C:\Toggle\git\ToggleFeature\ToggleFeature\Resources\toggles.xml";

        [Test]
        public void Number_Of_NewToggle_OldToggle()
        {
            var numberOfNodesNewToggle1 = NumberOfNodes(StagingToggle, ReleaseName, "release");
            var numberOfNodesOldToggle1 = NumberOfNodes(ProductionToggle, ReleaseName, "release");

            Console.WriteLine("################### Release  " + ReleaseName + "  ###################");
            Console.WriteLine("In the Staging Toggle.xml file there is: " + numberOfNodesNewToggle1 + " features nodes");
            Console.WriteLine("In the Production Toggle.xml file there is: " + numberOfNodesOldToggle1 + " features nodes");
        }

        [Test]
        public void Updated_Features_Toggle()
        {
            XmlNode oldNode = SearchSpecificNode(ProductionToggle, ReleaseName, "release");
            XmlNode newNode = SearchSpecificNode(StagingToggle, ReleaseName, "release");

            XmlNode nodes = UpdatedReleasesNode(oldNode, newNode, oldNode.ChildNodes.Count);
            DisplayUpdatedFields(nodes, StagingToggle, "Staging: ", "Production: ");
        }
    }
}
