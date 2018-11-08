using System;
using JuliusSweetland.OptiKey.UI.ViewModels.Keyboards.Base;

namespace JuliusSweetland.OptiKey.UI.ViewModels.Keyboards
{
    public class ExperimentalKeyboardWithPhrases : BackActionKeyboard, IConversationKeyboard
    {
        public ExperimentalKeyboardWithPhrases(Action backAction)
            : base(backAction, simulateKeyStrokes: false, multiKeySelectionSupported: true)
        {
        }
    }
}
