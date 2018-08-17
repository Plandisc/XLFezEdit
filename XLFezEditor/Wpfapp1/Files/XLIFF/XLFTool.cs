using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLFezEditor.Files.XLIFF
{
    public class XLFTool
    {
        public static string GetElementValue(XElement parent, string nodeName)
        {
            XElement xElement = parent.Element(ShellViewModel.xnamespace + nodeName);

            if (xElement == null)
            {
                return null;
            }
            else
            {
                var nodeRegex = new Regex("</?" + nodeName + "[^>]*>");
                return nodeRegex.Replace(xElement.ToString(), "");
            }
        }

        public static void SetElementValue(XElement parent, string nodeName, string value)
        {
            XElement newNodeContent = XElement.Parse("<" + nodeName + " xmlns=\"" + ShellViewModel.xnamespace + "\">" + value + "</" + nodeName + ">");
            newNodeContent.RemoveAttributes();
            parent.Element(ShellViewModel.xnamespace + nodeName).ReplaceWith(newNodeContent);
        }
    }
}
