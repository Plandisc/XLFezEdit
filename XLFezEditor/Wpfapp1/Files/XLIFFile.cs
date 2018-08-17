using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XLFezEditor.Files.XLIFF;

namespace XLFezEditor.Files
{
    class XLIFFile
    {
        private XDocument document;
        public List<XElement> xElements { get; private set; }
        private string filePath;

        public void Load(string filename)
        {
            filePath = filename;
            document = XDocument.Load(filename);
            xElements = GetDocumentUnits().ToList();
            TransUnits = xElements.Select(tu => new TransUnit(tu)).ToList();
        }

        private IEnumerable<XElement> GetDocumentUnits()
        {
            return document.Root.Element(ShellViewModel.xnamespace + "file").Element(ShellViewModel.xnamespace + "body").Elements();
        }
        private XElement GetBodyElement()
        {
            return document.Root.Element(ShellViewModel.xnamespace + "file").Element(ShellViewModel.xnamespace + "body");
        }


        public List<TransUnit> TransUnits { get; private set; }

        public void Update(XLIFFile master)
        {
            RemoveNodes(master);

            UpdateSourceTextInExistingNodes(master);

            AddNewNodes(master);
        }

        private void UpdateSourceTextInExistingNodes(XLIFFile master)
        {
            foreach (var node in xElements)
            {
                var masterNode = master.xElements.FirstOrDefault(e => e.Attribute("id").Value == node.Attribute("id").Value);
                if (masterNode != null)
                {
                    var masterValue = XLFTool.GetElementValue(masterNode, "source");
                    XLFTool.SetElementValue(node, "source", masterValue);
                }
            }
        }

        private void RemoveNodes(XLIFFile master)
        {
            var masterIds = master.xElements.Select(e => e.Attribute("id").Value).ToList();
            var elementsToRemove = xElements.Where(e => !masterIds.Contains(e.Attribute("id").Value)).ToList();
            foreach (var node in elementsToRemove)
            {
                node.Remove();
                TransUnits = TransUnits.Where(unit => unit.Id != node.Attribute("id").Value).ToList();
                xElements.Remove(node);
            }
        }

        private void AddNewNodes(XLIFFile master)
        {
            var ids = xElements.Select(e => e.Attribute("id").Value).ToList();
            var elementsToAdd = master.xElements.Where(id => !ids.Contains(id.Attribute("id").Value)).ToList();
            foreach (var node in elementsToAdd)
            {
                CheckIfTarget(node);

                IEnumerable<XElement> units = GetDocumentUnits();
                if (units.Any())
                {
                    units.Last().AddAfterSelf(node);
                }
                else
                {
                    GetBodyElement().Add(node);
                }

                var insertedNode = GetDocumentUnits().Last();
                TransUnits.Add(new TransUnit(insertedNode));
                xElements.Add(insertedNode);
            }
        }

        private void CheckIfTarget(XElement node)
        {
            if(node.Element(ShellViewModel.xnamespace + "target") == null)
            {
                XElement target = XElement.Parse("<target xmlns=\"" + ShellViewModel.xnamespace + "\">" + "</target>");
                node.Element(ShellViewModel.xnamespace + "source").AddAfterSelf(target);
                target.RemoveAttributes();
                var tmp = node.Element(ShellViewModel.xnamespace + "target");
                var tmp1 = node.Element(ShellViewModel.xnamespace + "source");
            }
        }

        public void Save()
        {           
            document.Save(filePath);
        }

        public void SaveAs(string filename)
        {
            document.Save(filename);
            Console.WriteLine("Saved to " + filename);
        } 
    }
}
