using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.Controls;

namespace JuliusSweetland.OptiKey.UI.Views.Keyboards.Common
{
    /// <summary>
    /// Interaction logic for ExperimentalNumericAndSymbols3.xaml
    /// </summary>
    public partial class ExperimentalNumericAndSymbols3 : KeyboardView
    {
        public ExperimentalNumericAndSymbols3()
        {
            this.DataContext = InstanceGetter.Instance.MainViewModel.ExperimentMenuViewModel;
            InitializeComponent();
        }
    }
}
