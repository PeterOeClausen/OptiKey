using System;
using JuliusSweetland.OptiKey.UI.ViewModels.Keyboards.Base;

namespace JuliusSweetland.OptiKey.UI.ViewModels.Keyboards
{
    class ExperimentalKeyboardWithPhrasesNumbersAndSymbolsKeyboard3 : BackActionKeyboard, IConversationKeyboard
    {
        public ExperimentalKeyboardWithPhrasesNumbersAndSymbolsKeyboard3(Action backAction)
            : base(backAction, simulateKeyStrokes: false, multiKeySelectionSupported: true)
        {
        }
    }
}
