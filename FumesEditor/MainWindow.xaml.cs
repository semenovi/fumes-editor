using System;
using System.Windows;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using FumesEditor.Models;
using FumesEditor.ViewModels;
using FumesEditor.Views;
using System.Windows.Controls;
using FumesEditor.Helpers;
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
          string xmlContent = File.ReadAllText(openFileDialog.FileName);
          _currentSave = SaveDeserializer.Deserialize(xmlContent);

          // Update GeneralView
          var generalView = (GeneralView)((TabItem)MainTabControl.Items[0]).Content;
          ((GeneralViewModel)generalView.DataContext).SaveModel = _currentSave;

          // Update ItemsView
          var itemsView = (ItemsView)((TabItem)MainTabControl.Items[1]).Content;
          ((ItemsViewModel)itemsView.DataContext).SaveModel = _currentSave;

          // Update SkinsView
          var skinsView = (SkinsView)((TabItem)MainTabControl.Items[2]).Content;
          ((SkinsViewModel)skinsView.DataContext).SaveModel = _currentSave;

          // Update KitView
          var kitView = (KitView)((TabItem)MainTabControl.Items[3]).Content;
          ((KitViewModel)kitView.DataContext).SaveModel = _currentSave;
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error opening file: {ex.Message}\n\nStack Trace: {ex.StackTrace}");
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
          XmlWriterSettings settings = new XmlWriterSettings
          {
            Indent = true,
            IndentChars = "  ",
            NewLineChars = "\n",
            NewLineHandling = NewLineHandling.Replace
          };

          XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
          ns.Add("", "");

          using (XmlWriter writer = XmlWriter.Create(saveFileDialog.FileName, settings))
          {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveModel));
            serializer.Serialize(writer, _currentSave, ns);
          }

          MessageBox.Show($"File saved: {saveFileDialog.FileName}");
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error saving file: {ex.Message}\n\nStack Trace: {ex.StackTrace}");
        }
      }
    }
  }
}