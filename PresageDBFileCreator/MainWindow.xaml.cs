using PresageDBFileCreator.ViewModels;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;

namespace PresageDBFileCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.ViewModel = new MainWindowViewModel();
            this.DataContext = ViewModel;
        }

        private void FileLocateButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;

                Console.WriteLine("File name chosen: " + filename);

                //Update label and settings:
                ViewModel.InputFilePath = filename;
            }
        }

        private void FormatFileButton_Click(object sender, RoutedEventArgs e)
        {
            string sb = ViewModel.FileContent.ToLower();
            //Removing newlines:
            sb = sb.Replace(System.Environment.NewLine, " ");
            //Removing digits and special characters:
            Regex digitRegex = new Regex(@"\d|[.,:()?!'-\\\/|<>@#£¤$%&{\[\]}=+-_´^*]");
            sb = digitRegex.Replace(sb, "");
            sb = sb.Replace("\"", "");
            sb = sb.Replace("  ", " ");
            ViewModel.FileContent = sb;
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create SaveFileDialog
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Title = "Save the formatted file as...";
            dlg.Filter = "Text File | *.txt";
                
            // Display SaveFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                Console.WriteLine(dlg.FileName);
                System.IO.File.WriteAllText(dlg.FileName, ViewModel.FileContent);
                System.Windows.MessageBox.Show("The file:\n" + dlg.FileName + "\n" + "was sucessfully saved", "Sucess!");
            }
        }

        private void GenerateDBFile_Click(object sender, RoutedEventArgs e)
        {
            string command = @"cd C:\Program Files (x86)\presage\bin" + " && " +
                @"text2ngram -n5 -f sqlite -o C:\Users\Peter\Desktop\DanishDB\database_dk.db " + ViewModel.InputFilePath + " && " +
                @"text2ngram -n4 -f sqlite -o C:\Users\Peter\Desktop\DanishDB\database_dk.db " + ViewModel.InputFilePath + " && " +
                @"text2ngram -n3 -f sqlite -o C:\Users\Peter\Desktop\DanishDB\database_dk.db " + ViewModel.InputFilePath + " && " +
                @"text2ngram -n2 -f sqlite -o C:\Users\Peter\Desktop\DanishDB\database_dk.db " + ViewModel.InputFilePath + " && " +
                @"text2ngram -n1 -f sqlite -o C:\Users\Peter\Desktop\DanishDB\database_dk.db " + ViewModel.InputFilePath;
            Process.Start("cmd", "/k " + command);
            //Process.Start("chrome.exe");
            //Process generateDBProcess = new Process();
            
        }

        private void ChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    ViewModel.DirectoryPathForSavingFileAndGeneratingDBTo = fbd.SelectedPath;
                    //TODO: Delete db file in directory!
                }
            }
        }

        private void SaveFormattedFileAndGenerateDBFileButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
