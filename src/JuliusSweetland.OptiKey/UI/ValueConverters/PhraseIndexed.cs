using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace JuliusSweetland.OptiKey.UI.ValueConverters
{
    public class PhraseIndexed : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                var phrases = values[0] as List<string>;
                var phraseIndex = (int)values[1];
                if (phrases != null)
                {
                    if (phrases.Count > phraseIndex)
                    {
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
