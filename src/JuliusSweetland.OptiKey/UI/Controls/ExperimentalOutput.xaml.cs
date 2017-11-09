using System.Windows;
using System.Windows.Controls;

namespace JuliusSweetland.OptiKey.UI.Controls
{
    /// <summary>
    /// Interaction logic for ExperimentalOutput.xaml
    /// </summary>
    public partial class ExperimentalOutput : UserControl
    {
        public ExperimentalOutput()
        {
            InitializeComponent();
            Loaded += (sender, args) => NumberOfSuggestionsDisplayedExperimental = 4;
        }

        public static readonly DependencyProperty NumberOfSuggestionsDisplayedProperty =
            DependencyProperty.Register("NumberOfSuggestionsDisplayedExperimental", typeof (int), typeof (ExperimentalOutput), new PropertyMetadata(default(int)));

        public int NumberOfSuggestionsDisplayedExperimental
        {
            get { return (int) GetValue(NumberOfSuggestionsDisplayedProperty); }
            set { SetValue(NumberOfSuggestionsDisplayedProperty, value); }
        }

        public static readonly DependencyProperty ScratchpadWidthInKeysProperty = DependencyProperty.Register(
            "ScratchpadWidthInKeysExperimental", typeof (int), typeof (ExperimentalOutput), new PropertyMetadata(default(int)));

        public int ScratchpadWidthInKeysExperimental
        {
            get { return (int) GetValue(ScratchpadWidthInKeysProperty); }
            set { SetValue(ScratchpadWidthInKeysProperty, value); }
        }        
    }
}
