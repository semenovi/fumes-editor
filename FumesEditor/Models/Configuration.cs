using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FumesEditor.Models
{
  public class Configuration
  {
    public List<BodyConfig> Bodies { get; set; } = new List<BodyConfig>();
    public List<EngineConfig> Engines { get; set; } = new List<EngineConfig>();
    public List<SuspensionConfig> Suspensions { get; set; } = new List<SuspensionConfig>();
    public List<SkinConfig> Skins { get; set; } = new List<SkinConfig>();
    public List<WeaponConfig> Weapons { get; set; } = new List<WeaponConfig>();
    public List<string> Biomes { get; set; } = new List<string>();

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
        if (!string.IsNullOrEmpty(save.Kit?.Body) && !config.Bodies.Exists(b => b.Name == save.Kit.Body))
          config.Bodies.Add(new BodyConfig { Name = save.Kit.Body });

        if (!string.IsNullOrEmpty(save.Kit?.Engine) && !config.Engines.Exists(e => e.Name == save.Kit.Engine))
          config.Engines.Add(new EngineConfig { Name = save.Kit.Engine });

        if (!string.IsNullOrEmpty(save.Kit?.Suspension) && !config.Suspensions.Exists(s => s.Name == save.Kit.Suspension))
          config.Suspensions.Add(new SuspensionConfig { Name = save.Kit.Suspension });

        if (!string.IsNullOrEmpty(save.Kit?.Skin) && !config.Skins.Exists(s => s.Name == save.Kit.Skin))
          config.Skins.Add(new SkinConfig { Name = save.Kit.Skin });

        if (!string.IsNullOrEmpty(save.Progress?.Biome) && !config.Biomes.Contains(save.Progress.Biome))
          config.Biomes.Add(save.Progress.Biome);

        if (save.Kit?.Modules != null)
        {
          foreach (var weapon in save.Kit.Modules)
          {
            if (!string.IsNullOrEmpty(weapon) && !config.Weapons.Exists(w => w.Name == weapon))
              config.Weapons.Add(new WeaponConfig { Name = weapon });
          }
        }
      }

      config.Bodies.Sort((x, y) => string.Compare(x.Name, y.Name));
      config.Engines.Sort((x, y) => string.Compare(x.Name, y.Name));
      config.Suspensions.Sort((x, y) => string.Compare(x.Name, y.Name));
      config.Skins.Sort((x, y) => string.Compare(x.Name, y.Name));
      config.Biomes.Sort();
      config.Weapons.Sort((x, y) => string.Compare(x.Name, y.Name));

      return config;
    }

    public class BodyConfig
    {
      public string Name { get; set; }
      public string ImagePath { get; set; }
    }

    public class EngineConfig
    {
      public string Name { get; set; }
      public string ImagePath { get; set; }
    }

    public class SuspensionConfig
    {
      public string Name { get; set; }
      public string Body { get; set; }
      public string Type { get; set; }
      public string ImagePath { get; set; }
    }

    public class SkinConfig
    {
      public string Name { get; set; }
      public string ImagePath { get; set; }
    }

    public class WeaponConfig
    {
      public string Name { get; set; }
      public string ImagePath { get; set; }
    }
  }
}