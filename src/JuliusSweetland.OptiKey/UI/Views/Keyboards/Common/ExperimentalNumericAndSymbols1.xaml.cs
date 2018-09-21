using JuliusSweetland.OptiKey.UI.Controls;
using JuliusSweetland.OptiKey.Services;

namespace JuliusSweetland.OptiKey.UI.Views.Keyboards.Common
{
    /// <summary>
    /// Interaction logic for ExperimentalNumericAndSymbols1.xaml
    /// </summary>
    public partial class ExperimentalNumericAndSymbols1 : KeyboardView
    {
        public ExperimentalNumericAndSymbols1()
        {
            this.DataContext = InstanceGetter.Instance.MainViewModel.ExperimentMenuViewModel;
            InitializeComponent();
        }
    }
}
