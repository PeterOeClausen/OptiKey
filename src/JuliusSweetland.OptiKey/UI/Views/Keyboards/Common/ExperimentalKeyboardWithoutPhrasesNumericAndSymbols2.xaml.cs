﻿using JuliusSweetland.OptiKey.Services;
using JuliusSweetland.OptiKey.UI.Controls;

namespace JuliusSweetland.OptiKey.UI.Views.Keyboards.Common
{
    /// <summary>
    /// Interaction logic for ExperimentalNumericAndSymbols2.xaml
    /// </summary>
    public partial class ExperimentalKeyboardWithoutPhrasesNumericAndSymbols2 : KeyboardView
    {
        public ExperimentalKeyboardWithoutPhrasesNumericAndSymbols2()
        {
            this.DataContext = InstanceGetter.Instance.MainViewModel.ExperimentMenuViewModel;
            InitializeComponent();
        }
    }
}
