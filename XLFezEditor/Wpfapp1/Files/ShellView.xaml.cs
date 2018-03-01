using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using XLFezEditor.Files;

namespace XLFezEditor
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            Title = "The Ultimate XLF Tool";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        XLIFFile xlifFile = new XLIFFile();

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string messageBoxText = "Do you want to save any unsaved changes before you leave?";
            string caption = "The Ultimate XLF Tool";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    xlifFile.Save();
                    break;
                case MessageBoxResult.No:
                    try
                    {
                        Close();
                    }
                    catch
                    {
                        InvalidOperationException exc = new InvalidOperationException();
                    }
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
