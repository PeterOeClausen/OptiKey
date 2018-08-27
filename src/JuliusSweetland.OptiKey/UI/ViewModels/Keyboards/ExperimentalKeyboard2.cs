using System;
using JuliusSweetland.OptiKey.UI.ViewModels.Keyboards.Base;

namespace JuliusSweetland.OptiKey.UI.ViewModels.Keyboards
{
    public class ExperimentalKeyboard2 : BackActionKeyboard, IConversationKeyboard
    {
        public ExperimentalKeyboard2(Action backAction) : base(backAction, simulateKeyStrokes: false, multiKeySelectionSupported:true)
        {
        }
    }
}
