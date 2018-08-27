using System.Windows.Input;
using JuliusSweetland.OptiKey.UI.Controls;

namespace JuliusSweetland.OptiKey.UI.Views.Keyboards.English
{
    /// <summary>
    /// Interaction logic for Alpha1.xaml
    /// </summary>
    public partial class ExperimentalKeyboard2 : KeyboardView
    {
        public ExperimentalKeyboard2() : base(shiftAware: true)
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
    }
}
