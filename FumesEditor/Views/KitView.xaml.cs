using System;
using System.Windows;
using FumesEditor.ViewModels;
using System.Windows.Media;

namespace FumesEditor.Views
{
  public partial class KitView : System.Windows.Controls.UserControl
  {
    public KitView()
    {
      InitializeComponent();
      DataContext = new KitViewModel();
    }

    private void ColorButton_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new System.Windows.Forms.ColorDialog();
      var currentColor = ((KitViewModel)DataContext).BodyColor;
      
      dialog.Color = System.Drawing.Color.FromArgb(
        (int)Math.Round((double)Math.Clamp(currentColor.A * 255, 0, 255)),
        (int)Math.Round((double)Math.Clamp(currentColor.R * 255, 0, 255)),
        (int)Math.Round((double)Math.Clamp(currentColor.G * 255, 0, 255)),
        (int)Math.Round((double)Math.Clamp(currentColor.B * 255, 0, 255))
      );

      if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        var newColor = System.Windows.Media.Color.FromScRgb(
          dialog.Color.A / 255f,
          dialog.Color.R / 255f,
          dialog.Color.G / 255f,
          dialog.Color.B / 255f
        );
        ((KitViewModel)DataContext).BodyColor = newColor;
      }
    }
  }
}