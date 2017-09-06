using JuliusSweetland.OptiKey.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace JuliusSweetland.OptiKey.UI.ValueConverters
{
    public class PhraseIndexed : IMultiValueConverter
    {
        private CSVLogService csvLogService = CSVLogService.Instance;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] != DependencyProperty.UnsetValue)
            {
                var phrases = values[0] as List<string>;
                var phraseIndex = (int)values[1];
                if(phraseIndex == -42)
                {
                    csvLogService.Log_PhraseText("THE EXPERIMENT IS NOW DONE");
                    return "THE EXPERIMENT IS NOW DONE";
                }
                else if (phrases != null)
                {
                    if (phrases.Count > phraseIndex)
                    {
                        csvLogService.Log_PhraseText(phrases[phraseIndex]);
                        return phrases[phraseIndex];
                    }
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
