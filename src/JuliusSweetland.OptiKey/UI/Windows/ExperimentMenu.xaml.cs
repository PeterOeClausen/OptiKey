using JuliusSweetland.OptiKey.Enums;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace JuliusSweetland.OptiKey.UI.Windows
{
    /// <summary>
    /// Interaction logic for ExperimentMenu.xaml
    /// </summary>
    public partial class ExperimentMenu : Window
    {
        private ExperimentMenuViewModel viewModel;
        private MainWindow mainWindow;

        public ExperimentMenu(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            viewModel = new ExperimentMenuViewModel();
            this.DataContext = viewModel;
        }

        private void Start_Experiment_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.ExperimentalKeyboard));
            this.Hide();
        }

        private void Load_Phrases_File_Button_Click(object sender, RoutedEventArgs e)
        {
            //Create PhraseStateService:
            List<string> phraseList = File.ReadAllLines(@"phrases2.txt").ToList();
            var phraseStateService = new PhraseStateService() { Phrases = phraseList, PhraseNumber = 1 };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }
    }
}
