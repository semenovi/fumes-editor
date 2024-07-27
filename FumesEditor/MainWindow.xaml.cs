using System;
using System.Windows;
using Microsoft.Win32;

namespace FumesEditor
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = "Save Files (*.save)|*.save|All files (*.*)|*.*"
      };

      if (openFileDialog.ShowDialog() == true)
      {
        // TODO: Implement file opening logic
        MessageBox.Show($"File opened: {openFileDialog.FileName}");
      }
    }

    private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog
      {
        Filter = "Save Files (*.save)|*.save|All files (*.*)|*.*"
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        // TODO: Implement file saving logic
        MessageBox.Show($"File saved: {saveFileDialog.FileName}");
      }
    }
  }
}