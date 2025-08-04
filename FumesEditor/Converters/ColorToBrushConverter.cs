using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FumesEditor.Converters
{
  public class ColorToBrushConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is System.Windows.Media.Color color)
      {
        return new SolidColorBrush(color);
      }
      return new SolidColorBrush(Colors.White);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}