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
            this.SetSubTitle("");
        }

        public void SetSubTitle(string subTitle)
        {
            this.Title = "The Ultimate XLF Tool" + (subTitle != "" ? " - " + subTitle : "");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (ShellViewModel.isDirty)
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
}
