using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Xml.Linq;
using XLFezEditor.Files;
using System.ComponentModel;

namespace XLFezEditor
{
    public class ShellViewModel : PropertyChangedBase
    {
        public static XNamespace xnamespace = "urn:oasis:names:tc:xliff:document:1.2";

        public IEventAggregator events;
        private XLFDataViewModel _xlfDataVM;
        private XLIFFile xlifFile;

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

        public void Save()
        {
            xlifFile.Save();
        }

        public void SaveBeforeOpen()
        {
            string messageBoxText = "Do you want to save any unsaved changes?";
            string caption = "Before you close this file";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    xlifFile.Save();
                    break;
            }
        }

        public void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (xlifFile != null)
            {
                SaveBeforeOpen();
            }

                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    xlifFile = new XLIFFile();
                    xlifFile.Load(openFileDialog.FileName);

                    XLFDataVM.bindList(xlifFile.TransUnits);
                }
        }

        public void BtnSaveXLFFile()
        {
            if (xlifFile != null)
            {
                xlifFile.Save();
            }
            else
            {
                string message = "No File Opened";
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
        }

        public void BtnSaveAs()
        {
            if (xlifFile != null)
            {
                var saveAsFileDialog = new SaveFileDialog();
                if (saveAsFileDialog.ShowDialog() == true)
                {
                    xlifFile.SaveAs(saveAsFileDialog.FileName);
                }
            }
            else
            {
                string message = "No File Opened";
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }

        }
        public void BtnUpdateFrom()
        {
            if (xlifFile != null)
            {
                var openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    var master = new XLIFFile();
                    master.Load(openFileDialog.FileName);
                    xlifFile.Update(master);
                    XLFDataVM.bindList(xlifFile.TransUnits);
                }

            }
            else
            {
                string message = "No File Opened";
                string caption = "Error";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
        }

    }
}

