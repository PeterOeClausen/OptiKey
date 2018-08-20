using PresageDBFileCreator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                MessageBox.Show("The file:\n" + dlg.FileName + "\n" + "was sucessfully saved", "Sucess!");
            }
        }

        private void GenerateDBFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
