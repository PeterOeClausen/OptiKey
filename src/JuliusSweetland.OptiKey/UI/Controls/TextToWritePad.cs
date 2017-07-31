using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace JuliusSweetland.OptiKey.UI.Controls
{
    public class TextToWritePad : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("TextToWritePadText", typeof (string), typeof (TextToWritePad), new PropertyMetadata(default(string)));

        public string TextToWritePadText
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
