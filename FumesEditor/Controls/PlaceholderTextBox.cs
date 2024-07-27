using System.Windows;
using System.Windows.Controls;

namespace FumesEditor.Controls
{
  public class PlaceholderTextBox : TextBox
  {
    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

    public string PlaceholderText
    {
      get { return (string)GetValue(PlaceholderTextProperty); }
      set { SetValue(PlaceholderTextProperty, value); }
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
      base.OnGotFocus(e);
      if (Text == PlaceholderText)
      {
        Text = string.Empty;
      }
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
      base.OnLostFocus(e);
      if (string.IsNullOrEmpty(Text))
      {
        Text = PlaceholderText;
      }
    }
  }
} 