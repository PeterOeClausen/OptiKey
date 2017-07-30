using JuliusSweetland.OptiKey.Enums;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.ViewModels;
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
using System.Windows.Shapes;

namespace JuliusSweetland.OptiKey.UI.Windows
{
    /// <summary>
    /// Interaction logic for ExperimentMenu.xaml
    /// </summary>
    public partial class ExperimentMenu : Window
    {
        private ExperimentMenuViewModel viewModel;

        public ExperimentMenu()
        {
            InitializeComponent();
            viewModel = new ExperimentMenuViewModel();
            this.DataContext = viewModel;
        }

        private void Start_Experiment_Click(object sender, RoutedEventArgs e)
        {
            InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.ExperimentalKeyboard));
            this.Close();
        }
    }
}
