using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace XLFezEditor.Files
{
    public class OpenFileDialogViewModel : Window
    {
        public OpenFileDialogViewModel()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialogViewModel openFileDialog = new OpenFileDialogViewModel();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }
}
