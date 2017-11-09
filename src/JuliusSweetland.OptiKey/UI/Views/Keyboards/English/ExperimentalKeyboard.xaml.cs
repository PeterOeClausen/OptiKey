using System.Windows.Input;
using JuliusSweetland.OptiKey.UI.Controls;
using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Enums;

namespace JuliusSweetland.OptiKey.UI.Views.Keyboards.English
{
    /// <summary>
    /// Interaction logic for ExperimentalKeyboard.xaml
    /// </summary>
    public partial class ExperimentalKeyboard : KeyboardView
    {
        public ExperimentalKeyboard() : base(shiftAware: true)
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            /*
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.EscKeyPressed));
            }
            */
        }
    }
}
