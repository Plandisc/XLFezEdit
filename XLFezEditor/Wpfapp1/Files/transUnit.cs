using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XLFezEditor.Files
{
    public class TransUnit : PropertyChangedBase
    {
        private readonly XElement element;
        private XNamespace xnamespace = "urn:oasis:names:tc:xliff:document:1.2"; // TODO Get namespace from document

        public TransUnit(XElement element)
        {
            this.element = element;
        }

        public string Source
        {
            get
            {
                return GetElementValue("source");
            }
        }
        public string Id
        {
            get
            {
                return element.Attribute("id").Value;
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
                element.Element(xnamespace + "target").Value = value;
                NotifyOfPropertyChange(() => Target);

            }
        }

        public string DetailsString
        {
            get
            {
                System.Text.StringBuilder detailsString = new System.Text.StringBuilder();
                if (Meaning != null && Meaning != "")
                {
                    detailsString.Append("Description: " + Description);
                    detailsString.AppendLine();
                }

                detailsString.Append("-----------------------");
                detailsString.AppendLine();
                foreach (var detail in Details)
                {
                    detailsString.Append("Source File: " + detail.source.ToString());
                    detailsString.Append(detail.source.ToString());
                    detailsString.AppendLine();
                    detailsString.Append("Line Number: " + detail.lineNumber.ToString());
                    detailsString.Append(detail.lineNumber.ToString());
                    detailsString.AppendLine();
                    detailsString.Append("-----------------------");
                    detailsString.AppendLine();
                }
                return detailsString.ToString();
            }
        }

        public List<ContextGroup> Details
        {
            get
            {
                List<ContextGroup> tmp = element.Elements(xnamespace + "context-group").Select(cg => GetFileAndLineNumber(cg)).ToList();
                return element.Elements(xnamespace + "context-group").Select(cg => GetFileAndLineNumber(cg)).ToList();
            }
        }

        private ContextGroup GetFileAndLineNumber(XElement cg)
        {
            var source = GetElementValue("context-group", "context", "sourcefile", cg);
            var lineNumber = GetElementValue("context-group", "context", "linenumber", cg);
            var list = new ContextGroup(source, lineNumber);
            return list;
        }
        private string GetElementValue(string nodeParentName, string nodeName, string attributeName, XElement element)
        {
            List<XElement> xElements = this.element.Elements(xnamespace + nodeParentName).ToList();
            xElements = xElements.Where(el => trimAndReturn(nodeName, attributeName, el, element) != null).ToList();

            var elementValue = GetElementValue(xElements[0], nodeName,attributeName);

            return elementValue;
        }

        private string trimAndReturn(string nodeName, string attributeName, XElement el, XElement element)
        {
            if (element == null)
            {
                return null;
            }
            else
            {
                var value = GetElementValue(element, nodeName, attributeName);
                if (attributeName == "sourcefile")
                {
                    string chars = Regex.Replace(el.Value.ToString(), "[0-9]", "");
                    if (value == chars)
                    {

                        return value;
                    }

                }
                else if (attributeName == "linenumber")
                {
                    string chars = new String(el.Value.ToString().Where(Char.IsDigit).ToArray());

                    if (value == chars)
                    {
                        return value;
                    }
                }
            }

            return null;
        }

        public string Description
        {
            get
            {
                return GetElementValue("note", "description");
            }
        }

        public string Meaning
        {
            get
            {
                var tmp = GetElementValue("note", "meaning");
                return tmp;
            }
        }

        private string GetElementValue(string nodeName)
        {

            XElement xElement = element.Element(xnamespace + nodeName);

            if (xElement == null)
            {
                return null;
            }
            else
            {
                var nodeValue = element.Element(xnamespace + nodeName).Nodes().Select(s => s.NodeType == XmlNodeType.Text ? SecurityElement.Escape(s.ToString()) : s.ToString());
                return string.Join("", nodeValue);
            }
        }

        private string GetElementValue(string nodeName, string attributeName)
        {
            return GetElementValue(element, nodeName, attributeName);
        }

        private string GetElementValue(XElement element, string nodeName, string attributeName)
        {
            if (element.HasElements)
            {
                List<XElement> elementList = element.Elements().ToList();
                if (elementList == null)
                {
                    return null;
                }
                foreach (var listElement in elementList) // iterate through every element in the list
                {
                    if (listElement.HasAttributes)
                    {
                        var list = listElement.Attributes().ToList();
                        foreach (var item in list) // iterate through every attribute in the element
                        {
                            if (item.Value.Equals(attributeName))
                            {
                                if(listElement.Value == "")
                                {
                                    return null;
                                }
                                return listElement.Value;
                            }
                        }
                    }

                }

            }
            else
            {
                XElement xElement = element.Element(xnamespace + nodeName);
                if (xElement == null)
                {
                    return null;
                }
                if (xElement.HasAttributes)
                {
                    var list = xElement.Attributes().ToList();
                    foreach (var item in list)  // iterate through every attribute in the element
                    {
                        if (item.Value.Equals(attributeName))
                        {
                            if(xElement.Value == "")
                            {
                                return null;
                            }
                            return xElement.Value;
                        }
                    }
                }
            }
            return null;
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
