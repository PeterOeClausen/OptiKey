using JuliusSweetland.OptiKey.UI.Controls;

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
    }
}
