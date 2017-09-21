using JuliusSweetland.OptiKey.Enums;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.ViewModels;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;

namespace JuliusSweetland.OptiKey.UI.Windows
{
    /// <summary>
    /// Interaction logic for ExperimentMenu.xaml
    /// </summary>
    public partial class ExperimentMenu : Window
    {
        private MainWindow mainWindow;
        private ExperimentMenuViewModel viewModel;

        public ExperimentMenu(MainWindow mainWindow, ExperimentMenuViewModel viewModel)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.DataContext = viewModel;
            this.viewModel = viewModel; //Just so I can get instance in this class.
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }

        private void ChangePhraseFileButton_Click(object sender, RoutedEventArgs e)
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

                //Update label:
                viewModel.PhrasesFilePath = filename;

                //Change PhraseStateService
                InstanceGetter.Instance.PhraseStateService.SetPhraseFile(viewModel.PhrasesFilePath);
            }
        }

        private void StartExperimentButton_Click(object sender, RoutedEventArgs e)
        {
            CSVLogService.Instance.StartLogging();

            mainWindow.Show();
            InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.ExperimentalKeyboard));
            if (this.viewModel.EnableMultikeySwipeFeature)
            {
                InstanceGetter.Instance.MainViewModel.KeyStateService.ProgressKeyDownState(new KeyValue(FunctionKeys.MultiKeySelectionIsOn));
                InstanceGetter.Instance.MainViewModel.KeyStateService.ProgressKeyDownState(new KeyValue(FunctionKeys.MultiKeySelectionIsOn));
            }
            this.Hide();
        }

        private void ChangeOptiKeyLogsFolder_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();

            fbd.Description = "The application will create a folder called 'OptiKeyLogs' inside the folder you choose here.\n";

            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = Path.Combine(fbd.SelectedPath, "OptiKeyLogs");
                //Update UI:
                viewModel.OptiKeyLogPath = folderPath;
                //Opdate CSVLogService:
                CSVLogService.Instance.SetOptiKeyLogPath(folderPath);
            }
        }
    }
}
