using System;
using System.IO;
using System.Windows;
using FumesEditor.Helpers;

namespace FumesEditor
{
  public partial class App : System.Windows.Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      try
      {
        var originalFilePath = "FUMES_SAVE_SLOT0_VER4 - Copy.save";
        var testFilePath = "FUMES_SAVE_SLOT0_VER4_TEST.save";

        if (File.Exists(originalFilePath))
        {
          var originalXml = File.ReadAllText(originalFilePath);
          var saveModel = SaveDeserializer.Deserialize(originalXml);
          var newXml = SaveDeserializer.Serialize(saveModel);
          File.WriteAllText(testFilePath, newXml);
          
          Console.WriteLine($"Original size: {originalXml.Length} characters");
          Console.WriteLine($"New size: {newXml.Length} characters");
          Console.WriteLine($"Test file saved as: {testFilePath}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Testing error: {ex.Message}");
      }
      
      base.OnStartup(e);
    }
  }
}