using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FumesEditor.ViewModels;
using Microsoft.Win32;
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

    private void AddModule_Click(object sender, RoutedEventArgs e)
    {
      var selectedModule = ModuleComboBox.SelectedItem as string;
      if (!string.IsNullOrEmpty(selectedModule))
      {
        var viewModel = (KitViewModel)DataContext;
        var modules = viewModel.ModulesList.ToList();
        modules.Add(selectedModule);
        viewModel.ModulesList = modules;
      }
    }

    private void RemoveModule_Click(object sender, RoutedEventArgs e)
    {
      if (sender is System.Windows.Controls.Button button)
      {
        var stackPanel = button.Parent as System.Windows.Controls.StackPanel;
        var parentStackPanel = stackPanel?.Parent as System.Windows.Controls.StackPanel;
        var listBox = parentStackPanel?.Children.OfType<System.Windows.Controls.ListBox>().FirstOrDefault();
        
        if (listBox?.SelectedItem is string selectedModule)
        {
          var viewModel = (KitViewModel)DataContext;
          var modules = viewModel.ModulesList.ToList();
          modules.Remove(selectedModule);
          viewModel.ModulesList = modules;
        }
      }
    }
  }
}