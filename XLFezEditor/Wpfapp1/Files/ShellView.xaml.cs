using System;
using System.Windows;
using System.ComponentModel;

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

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string messageBoxText = "Do you want to save any unsaved changes before you leave?";
            string caption = "The Ultimate XLF Tool";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            var model = this.DataContext as ShellViewModel;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    model.Save();
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
