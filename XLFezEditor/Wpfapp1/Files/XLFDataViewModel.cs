using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace XLFezEditor.Files
{
    public class XLFDataViewModel : PropertyChangedBase
    {

        private BindableCollection<TransUnit> _data;
        public BindableCollection<TransUnit> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                NotifyOfPropertyChange(() => Data);
            }

            // linq XLF conversion reading and parsing to a list
        }
        public XLFDataViewModel() { }

        public void bindList(List<TransUnit> Data)
        {
            BindableCollection<TransUnit> dataCollection = new BindableCollection<TransUnit>(Data);
            this.Data = dataCollection;
            Console.WriteLine("data bound");
        }
        public void saveToFile()
        {

            //XDocument xDoc = new XDocument(
            //            new XDeclaration("1.0", "UTF-16", null),
            //            new XElement(empNM + "Employees",
            //                new XElement("Employee",
            //                    new XComment("Only 3 elements for demo purposes"),
            //                    new XElement("EmpId", "5"),
            //                    new XElement("Name", "Kimmy"),
            //                    new XElement("Sex", "Female")
            //                    )));

            //StringWriter sw = new StringWriter();
            //xDoc.Save(sw);
            //Console.WriteLine(sw)

        }
    }
}
