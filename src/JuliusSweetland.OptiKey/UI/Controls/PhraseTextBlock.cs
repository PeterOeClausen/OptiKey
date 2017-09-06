using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace JuliusSweetland.OptiKey.UI.Controls
{
    public class PhraseTextBlock : TextBlock
    {
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            Console.WriteLine(e.Text);
        }
    }
}
