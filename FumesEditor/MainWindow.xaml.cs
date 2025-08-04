using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using FumesEditor.Helpers;
using FumesEditor.ViewModels;

namespace FumesEditor
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      LoadPathTextBox.Text = "1.save";
      SavePathTextBox.Text = "1.save";
    }

    private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (e.ClickCount == 2)
      {
        MaximizeRestoreWindow();
      }
      else
      {
        DragMove();
      }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
      WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
      MaximizeRestoreWindow();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void MaximizeRestoreWindow()
    {
      if (WindowState == WindowState.Maximized)
      {
        WindowState = WindowState.Normal;
      }
      else
      {
        WindowState = WindowState.Maximized;
      }
    }

    private void LoadButton_Click(object sender, RoutedEventArgs e)
    {
      string filePath = LoadPathTextBox.Text.Trim();
      if (string.IsNullOrEmpty(filePath))
      {
        System.Windows.MessageBox.Show("please enter a file path to load.", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        return;
      }

      if (!File.Exists(filePath))
      {
        System.Windows.MessageBox.Show($"file not found: {filePath}", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        return;
      }

      try
      {
        string xmlContent = File.ReadAllText(filePath);
        var saveModel = SaveDeserializer.Deserialize(xmlContent);
        
        ((KitViewModel)KitView.DataContext).SaveModel = saveModel;
        
        Title = $"fumes editor - {Path.GetFileName(filePath)}";
      }
      catch (Exception ex)
      {
        System.Windows.MessageBox.Show($"error loading file: {ex.Message}", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
      }
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      var viewModel = (KitViewModel)KitView.DataContext;
      if (viewModel.SaveModel == null)
      {
        System.Windows.MessageBox.Show("no save file is open.", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        return;
      }

      string filePath = SavePathTextBox.Text.Trim();
      if (string.IsNullOrEmpty(filePath))
      {
        System.Windows.MessageBox.Show("please enter a file path to save.", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        return;
      }

      try
      {
        string xmlContent = SaveDeserializer.Serialize(viewModel.SaveModel);
        File.WriteAllText(filePath, xmlContent);
        
        System.Windows.MessageBox.Show("file saved successfully!", "success", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
      }
      catch (Exception ex)
      {
        System.Windows.MessageBox.Show($"error saving file: {ex.Message}", "error", 
                       System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
      }
    }
  }
}