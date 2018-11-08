using System;
using JuliusSweetland.OptiKey.UI.ViewModels.Keyboards.Base;

namespace JuliusSweetland.OptiKey.UI.ViewModels.Keyboards
{
    public class ExperimentalKeyboardWithoutPhrases : Keyboard
    {
        public ExperimentalKeyboardWithoutPhrases() : base(multiKeySelectionSupported:true)
        {
        }
    }
}
