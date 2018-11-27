using System;
using JuliusSweetland.OptiKey.UI.ViewModels.Keyboards.Base;

namespace JuliusSweetland.OptiKey.UI.ViewModels.Keyboards
{
    class ExperimentalKeyboardWithPhrasesNumbersAndSymbolsKeyboard2 : BackActionKeyboard, IConversationKeyboard
    {
        public ExperimentalKeyboardWithPhrasesNumbersAndSymbolsKeyboard2(Action backAction)
            : base(backAction, simulateKeyStrokes: false, multiKeySelectionSupported: true)
        {
        }
    }
}
