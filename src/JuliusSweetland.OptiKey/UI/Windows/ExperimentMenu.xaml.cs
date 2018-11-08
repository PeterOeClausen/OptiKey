using JuliusSweetland.OptiKey.Enums;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.ViewModels;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;
using JuliusSweetland.OptiKey.Properties;
using System.Threading.Tasks;

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

                //Update label and settings:
                viewModel.PhrasesFilePath = filename;
            }
        }

        private void ResetPhraseFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Update label and settings:
            viewModel.PhrasesFilePath = "default_phrases.txt";
        }

        private async void StartExperimentButton_Click(object sender, RoutedEventArgs e)
        {
            InstanceGetter.Instance.PhraseStateService.SetPhraseFile(viewModel.PhrasesFilePath);
            CSVLogService.Instance.StartLogging();

            //Changing keyboards to one with or without phrases:
            switch (viewModel.SelectedExperimentKeyboardType)
            {
                case ExperimentalKeyboardTypes.WithPhrases:
                    InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.KeyboardWithPhrases));
                    break;
                case ExperimentalKeyboardTypes.WithoutPhrases:
                    InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.KeyboardWithoutPhrases));
                    break;
            }

            //Changing to fullscreen or halfscreen based on what user picked.
            switch (viewModel.SelectedScreenState)
            {
                case ScreenStates.FullScreen:
                    InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.FullScreen));
                    break;

                case ScreenStates.HalfScreen:
                    InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.HalfScreen));
                    //TODO: Check that it restores fullscreen after.
                    break;
            }

            // Instead of 2 seconds of sleep, keyboard is set to sleep till the user activates it by looking at sleep again
            InstanceGetter.Instance.KeyStateService.KeyDownStates[KeyValues.SleepKey].Value = KeyDownStates.LockedDown;
            //Showing main window:
            mainWindow.Show();
            /*//Pausing writing for 2 seconds, to avoid typing before user interface is ready:
            InstanceGetter.Instance.KeyStateService.KeyDownStates[KeyValues.SleepKey].Value = KeyDownStates.LockedDown;
            //Showing main window:
            mainWindow.Show();
            //Delay for 2 seconds:
            await Task.Delay(2000);
            //Unpausing:
            InstanceGetter.Instance.KeyStateService.KeyDownStates[KeyValues.SleepKey].Value = KeyDownStates.Up;*/

            //Activates multiKey button based on the setting in the menu:
            if (this.viewModel.EnableMultikeySwipeFeature)
            {
                Settings.Default.MultiKeySelectionEnabled = true;
                InstanceGetter.Instance.KeyStateService.SetMultiKeyState(Enums.KeyDownStates.LockedDown);
            }
            else
            {
                Settings.Default.MultiKeySelectionEnabled = false;
                InstanceGetter.Instance.KeyStateService.SetMultiKeyState(Enums.KeyDownStates.Up);
            }
            
            //Hiding the experiment Menu.
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
            }
        }
    }
}
