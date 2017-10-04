using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


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

        public List<TransUnit> TransUnits { get; private set; }

        public void Update(XLIFFile master)
        {
            var masterIds = master.xElements.Select(e => e.Attribute("id").Value).ToList();
            var elementsToRemove = xElements.Where(e => !masterIds.Contains(e.Attribute("id").Value)).ToList();
            foreach (var node in elementsToRemove)
            {
                node.Remove();
                TransUnits = TransUnits.Where(unit => unit.Id != node.Attribute("id").Value).ToList();
                xElements.Remove(node);
            }

            var ids = xElements.Select(e => e.Attribute("id").Value).ToList();
            var elementsToAdd = master.xElements.Where(id => !ids.Contains(id.Attribute("id").Value)).ToList();
            foreach (var node in elementsToAdd)
            {
                CheckIfTarget(node);
                TransUnits.Add(new TransUnit(node));
                xElements.Add(node);
                GetDocumentUnits().Last().AddAfterSelf(node);
            }
        }

        private void CheckIfTarget(XElement node)
        {
            if(node.Element(ShellViewModel.xnamespace + "target") == null)
            { 
                node.Element(ShellViewModel.xnamespace + "source").AddAfterSelf(XElement.Parse("<target xmlns=\"" + ShellViewModel.xnamespace + "\"/>"));
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
