using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Linq;
using System.IO;
using FumesEditor.Models;
using FumesEditor.Helpers;

namespace FumesEditor
{
  public partial class LoadMultipleSavesWindow : Window
  {
    public List<SaveModel> LoadedSaves { get; private set; }

    public LoadMultipleSavesWindow()
    {
      InitializeComponent();
      LoadedSaves = new List<SaveModel>();
    }

    private void AddSaveFiles_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = "Save Files (*.save)|*.save|All files (*.*)|*.*",
        Multiselect = true
      };

      if (openFileDialog.ShowDialog() == true)
      {
        foreach (string fileName in openFileDialog.FileNames)
        {
          SaveFilesList.Items.Add(fileName);
        }
      }
    }

    private void Load_Click(object sender, RoutedEventArgs e)
    {
      LoadedSaves.Clear();
      foreach (string fileName in SaveFilesList.Items)
      {
        try
        {
          string xmlContent = File.ReadAllText(fileName);
          SaveModel save = SaveDeserializer.Deserialize(xmlContent);
          LoadedSaves.Add(save);
        }
        catch (System.Exception ex)
        {
          MessageBox.Show($"Error loading file {fileName}: {ex.Message}");
        }
      }

      if (LoadedSaves.Any())
      {
        DialogResult = true;
      }
      else
      {
        MessageBox.Show("No saves were successfully loaded.");
      }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}