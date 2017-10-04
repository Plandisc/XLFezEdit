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
        private XDocument openedXMLFile;
        private string openedDocumentPath;
        private List<XElement> xElements;

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
                openedXMLFile = XDocument.Load(openFileDialog.FileName);
                openedDocumentPath = openFileDialog.FileName;



                IEnumerable<XElement> reader = openedXMLFile.Root.Element(xnamespace + "file").Element(xnamespace + "body").Elements();

                var xElements = reader.ToList();
                var transUnits = xElements.Select(tu => new TransUnit(tu)).ToList();
                XLFDataVM.bindList(transUnits);
            }
        }
        public void BtnSaveXLFFile()
        {
            //var doc = openedXMLFile.Root.ToString();
            //doc = doc.Replace("&gt;", ">").Replace("&lt;", "<");
            //File.WriteAllText(openFileDialog.FileName, doc);
            var tmp = new XDocument(xElements);
            openedXMLFile.Save(openedDocumentPath, SaveOptions.None);
            Console.WriteLine("Saved to " + openedDocumentPath);
        }

        public void BtnSaveAs()
        {

        }

    }
}

