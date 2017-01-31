using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace JuliusSweetland.OptiKey.UI.Controls
{
    public class Scratchpad : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (Scratchpad), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set {
                Trace.WriteLine("text");
                SetValue(TextProperty, value);
            }
        }
    }
}
