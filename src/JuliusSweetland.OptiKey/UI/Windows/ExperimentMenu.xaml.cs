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
        private MainWindow mainWindow;
        private ExperimentMenuViewModel viewModel;

        public ExperimentMenu(MainWindow mainWindow, ExperimentMenuViewModel viewModel)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.DataContext = viewModel;
            this.viewModel = viewModel; //Just so I can get instance in this class.
        }

        private void Start_Experiment_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.ExperimentalKeyboard));
            if (this.viewModel.EnableMultikeySwipeFeature)
            {
                InstanceGetter.Instance.MainViewModel.KeyStateService.ProgressKeyDownState(new KeyValue(FunctionKeys.MultiKeySelectionIsOn));
                InstanceGetter.Instance.MainViewModel.KeyStateService.ProgressKeyDownState(new KeyValue(FunctionKeys.MultiKeySelectionIsOn));
            }
            this.Hide();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Application.Current.Shutdown();
        }
    }
}
