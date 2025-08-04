using System;
using System.Globalization;
using System.Windows.Data;
using FumesEditor.Models;

namespace FumesEditor.Converters
{
  public class ComponentDisplayNameConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is string id)
      {
        return GameComponents.GetDisplayName(id);
      }
      return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}