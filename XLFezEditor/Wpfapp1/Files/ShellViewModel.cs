using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Xml.Linq;
using XLFezEditor.Files;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace XLFezEditor
{
    public class ShellViewModel : PropertyChangedBase
    {
        public static XNamespace xnamespace = "urn:oasis:names:tc:xliff:document:1.2";

        public IEventAggregator events;
        private XLFDataViewModel _xlfDataVM;
        private XLIFFile xlifFile;
        private static string fileName = "";

        private static bool _isDirty = false;
        public static bool isDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                SetWindowTitle();
            }
        }

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
            isDirty = false;
        }

        public void SaveBeforeOpen()
        {
            if (isDirty)
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
                Mouse.OverrideCursor = Cursors.Wait;
                xlifFile = new XLIFFile();
                fileName = openFileDialog.FileName;
                xlifFile.Load(fileName);

                XLFDataVM.bindList(xlifFile.TransUnits);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new System.Action(() =>
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                }));
                isDirty = false;

                SetWindowTitle();
            }
        }

        private static void SetWindowTitle()
        {
            var currentApp = Application.Current as App;
            if (currentApp != null)
            {
                var window = currentApp.MainWindow as ShellView;
                window.SetSubTitle(fileName + (isDirty ? "*" : ""));
            }
        }

        public void BtnSaveXLFFile()
        {
            if (xlifFile != null)
            {
                xlifFile.Save();
                isDirty = false;
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
                isDirty = false;
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
                    Mouse.OverrideCursor = Cursors.Wait;
                    var master = new XLIFFile();
                    master.Load(openFileDialog.FileName);
                    xlifFile.Update(master);
                    XLFDataVM.bindList(xlifFile.TransUnits);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.ApplicationIdle, new System.Action(() =>
                    {
                        Mouse.OverrideCursor = Cursors.Arrow;
                    }));
                    isDirty = true;
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

