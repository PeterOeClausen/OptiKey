using System;
using System.Globalization;
using System.Windows.Data;

namespace JuliusSweetland.OptiKey.UI.ValueConverters
{
    public class ScreenStateToFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var screenState = (Enums.ScreenStates) value;
            switch (screenState)
            {
                case Enums.ScreenStates.FullScreen:
                    return 100;
                case Enums.ScreenStates.HalfScreen:
                    return 50;
                default:
                    return 10;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
