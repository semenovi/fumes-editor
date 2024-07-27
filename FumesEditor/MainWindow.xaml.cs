using System;
using System.Windows;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using FumesEditor.Models;
using FumesEditor.ViewModels;
using FumesEditor.Views;
using System.Windows.Controls;
using System.Xml;

namespace FumesEditor
{
  public partial class MainWindow : Window
  {
    private SaveModel _currentSave;

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
        try
        {
          XmlSerializer serializer = new XmlSerializer(typeof(SaveModel));
          using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
          using (XmlReader reader = XmlReader.Create(fs))
          {
            _currentSave = (SaveModel)serializer.Deserialize(reader);
          }

          // Update GeneralView
          var generalView = (GeneralView)((TabItem)MainTabControl.Items[0]).Content;
          ((GeneralViewModel)generalView.DataContext).SaveModel = _currentSave;

          MessageBox.Show($"File opened: {openFileDialog.FileName}");
        }
        catch (XmlException ex)
        {
          MessageBox.Show($"XML Error: {ex.Message}\nLine: {ex.LineNumber}, Position: {ex.LinePosition}");
        }
        catch (InvalidOperationException ex)
        {
          if (ex.InnerException is XmlException xmlEx)
          {
            MessageBox.Show($"XML Error: {xmlEx.Message}\nLine: {xmlEx.LineNumber}, Position: {xmlEx.LinePosition}");
          }
          else
          {
            MessageBox.Show($"Error opening file: {ex.Message}");
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error opening file: {ex.Message}");
        }
      }
    }

    private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
    {
      if (_currentSave == null)
      {
        MessageBox.Show("No save file is currently open.");
        return;
      }

      SaveFileDialog saveFileDialog = new SaveFileDialog
      {
        Filter = "Save Files (*.save)|*.save|All files (*.*)|*.*"
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        try
        {
          XmlSerializer serializer = new XmlSerializer(typeof(SaveModel));
          using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
          {
            serializer.Serialize(fs, _currentSave);
          }

          MessageBox.Show($"File saved: {saveFileDialog.FileName}");
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error saving file: {ex.Message}");
        }
      }
    }
  }
}