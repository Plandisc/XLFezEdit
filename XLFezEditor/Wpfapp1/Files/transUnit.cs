using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;
using XLFezEditor.Files.XLIFF;

namespace XLFezEditor.Files
{
    public class TransUnit : PropertyChangedBase
    {
        private readonly XElement element;

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
                SetElementValue("target", value);
                ShellViewModel.isDirty = true;
            }
        }

        public string DetailsString
        {
            get
            {
                StringBuilder detailsString = new StringBuilder();
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
                return element.Elements(ShellViewModel.xnamespace + "context-group").Select(cg => GetFileAndLineNumber(cg)).ToList();
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
            List<XElement> xElements = this.element.Elements(ShellViewModel.xnamespace + nodeParentName).ToList();
            xElements = xElements.Where(el => trimAndReturn(nodeName, attributeName, el, element) != null).ToList();

            var elementValue = GetElementValue(xElements[0], nodeName, attributeName);

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
                return GetElementValue("note", "meaning");
            }
        }

        private string GetElementValue(string nodeName)
        {
            return XLFTool.GetElementValue(element, nodeName);
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
                                if (listElement.Value == "")
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
                XElement xElement = element.Element(ShellViewModel.xnamespace + nodeName);
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
                            if (xElement.Value == "")
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


        private void SetElementValue(string elementName, string value)
        {
            if (element.Element(ShellViewModel.xnamespace + elementName) != null)
            {
                try
                {
                    XLFTool.SetElementValue(element, elementName, value);
                    NotifyOfPropertyChange(() => Target);
                }
                catch (System.Xml.XmlException e)
                {
                    string message = "Input is invalid: Cannot use characters '<', '>', '&'";
                    string caption = "Error";
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show(message, caption, buttons, icon);
                    NotifyOfPropertyChange(() => Target);
                }
            }
        }

    }
}
