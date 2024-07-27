using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FumesEditor.Models
{
  public class Configuration
  {
    public List<string> Bodies { get; set; } = new List<string>();
    public List<string> Engines { get; set; } = new List<string>();
    public List<string> Suspensions { get; set; } = new List<string>();
    public List<string> Skins { get; set; } = new List<string>();
    public List<string> Biomes { get; set; } = new List<string>();
    public List<string> Weapons { get; set; } = new List<string>();

    public static Configuration LoadFromFile(string filePath)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
      using (FileStream fs = new FileStream(filePath, FileMode.Open))
      {
        return (Configuration)serializer.Deserialize(fs);
      }
    }

    public void SaveToFile(string filePath)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
      using (FileStream fs = new FileStream(filePath, FileMode.Create))
      {
        serializer.Serialize(fs, this);
      }
    }

    public static Configuration CreateFromSaves(List<SaveModel> saves)
    {
      Configuration config = new Configuration();

      foreach (var save in saves)
      {
        if (!string.IsNullOrEmpty(save.Kit?.Body) && !config.Bodies.Contains(save.Kit.Body))
          config.Bodies.Add(save.Kit.Body);

        if (!string.IsNullOrEmpty(save.Kit?.Engine) && !config.Engines.Contains(save.Kit.Engine))
          config.Engines.Add(save.Kit.Engine);

        if (!string.IsNullOrEmpty(save.Kit?.Suspension) && !config.Suspensions.Contains(save.Kit.Suspension))
          config.Suspensions.Add(save.Kit.Suspension);

        if (!string.IsNullOrEmpty(save.Kit?.Skin) && !config.Skins.Contains(save.Kit.Skin))
          config.Skins.Add(save.Kit.Skin);

        if (!string.IsNullOrEmpty(save.Progress?.Biome) && !config.Biomes.Contains(save.Progress.Biome))
          config.Biomes.Add(save.Progress.Biome);

        if (save.Kit?.Modules != null)
        {
          foreach (var weapon in save.Kit.Modules)
          {
            if (!string.IsNullOrEmpty(weapon) && !config.Weapons.Contains(weapon))
              config.Weapons.Add(weapon);
          }
        }
      }

      config.Bodies.Sort();
      config.Engines.Sort();
      config.Suspensions.Sort();
      config.Skins.Sort();
      config.Biomes.Sort();
      config.Weapons.Sort();

      return config;
    }
  }
}