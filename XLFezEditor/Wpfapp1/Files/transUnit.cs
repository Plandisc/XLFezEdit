using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLFezEditor.Files
{
    public class TransUnit
    {
        private readonly XElement element;
        private const string xnamespace = "urn:oasis:names:tc:xliff:document:1.2"; // TODO Get namespace from document

        public TransUnit(XElement element)
        {
            this.element = element;
        }

        public string Source
        {
            get
            {
                Console.WriteLine(GetElementValue("source"));
                return GetElementValue("source");
            }
        }


        public string Target
        {
            get
            {
                return GetElementValue("target");
            }
            set
            {
                element.Element("target").Value = value;
            }
        }

        public List<ContextGroup> SourceFile
        {
            get
            {
                return element.Elements("context-group").Select(cg => GetFileAndLineNumber(cg)).ToList();
            }

        }

        private ContextGroup GetFileAndLineNumber(XElement cg)
        {
            var source = GetElementValue("context-group", "context", "sourcefile");
            var lineNumber = GetElementValue("context-group", "context", "linenumber");

            return new ContextGroup(source, lineNumber);
        }

        public string LineNumber
        {
            get
            {
                return GetElementValue("context-group", "context", "linenumber");
            }
        }

        public string Note
        {
            get
            {
                return GetElementValue("note", "description");
            }
        }

        private string GetElementValue(string nodeName)
        {

            XElement xElement = element.Element(xnamespace + nodeName);

            if (xElement == null)
            {
                return "";
            }
            else
            {
                return xElement.Value;
            }
        }

        private string GetElementValue(string nodeName, string attributeName)
        {
            return GetElementValue(element, nodeName, attributeName);
        }

        private string GetElementValue(XElement element, string nodeName, string attributeName)
        {

            XElement xElement = element.Element(xnamespace + nodeName);


            if (xElement == null)
            {
                return "";
            }
            else
            {
                if (xElement.Attribute(attributeName) != null)
                {
                    return xElement.Value;
                }
            }

            Console.WriteLine("no element with attribute name " + attributeName + " found");
            return "";
        }
        private string GetElementValue(string nodeParentName, string nodeName, string attributeName)
        {

            XElement xElement = element.Element(xnamespace + nodeParentName);

            if (xElement == null)
            {
                return "";
            }
            else
            {
                return GetElementValue(nodeName, attributeName);
            }
        }

        private void SetTarget(string value)
        {
            XElement xElement = element.Element(xnamespace + "target");
            if (xElement == null)
            {
                element.Element(xnamespace + "source").AddAfterSelf(XElement.Parse("<target>" + value + "</target>"));
            }
            else
            {
                xElement.Value = value;
            }
        }
    }
}
