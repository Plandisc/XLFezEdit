using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using XLFezEditor.Files;

namespace XLFezEditor
{
    public class ShellViewModel : PropertyChangedBase
    {
        public IEventAggregator events;
        private XLFDataViewModel _xlfDataVM;
        public XLFDataViewModel XLFDataVM
        {
            get { return _xlfDataVM; }
            set
            {
                _xlfDataVM = value;
                NotifyOfPropertyChange(() => XLFDataVM);
            }
        }

        public ShellViewModel()
        {
            events = new EventAggregator();

            XLFDataVM = new XLFDataViewModel();
        }

        public void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                XNamespace xnamespace = "urn:oasis:names:tc:xliff:document:1.2";
                XDocument tmp = XDocument.Load(openFileDialog.FileName);

                var doc = tmp.Root.ToString();
                doc = doc.Replace("&gt;", ">").Replace("&lt;", "<");
                File.WriteAllText(openFileDialog.FileName, doc);               

                IEnumerable<XElement> reader = tmp.Root.Element(xnamespace + "file").Element(xnamespace + "body").Elements();

                var xElements = reader.ToList();
                XDocument tmp2 = new XDocument(xElements);
                var transUnits = xElements.Select(tu => new TransUnit(tu)).ToList();
                XLFDataVM.bindList(transUnits);
            }
        }
        public void BtnSaveXLFFile()
        {

        }

    }
}

