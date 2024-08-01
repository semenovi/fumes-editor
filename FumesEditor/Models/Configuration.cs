using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace FumesEditor.Models
{
  [XmlRoot("Configuration")]
  public class Configuration
  {
    [XmlArray("Bodies")]
    [XmlArrayItem("Body")]
    public List<BodyConfig> Bodies { get; set; } = new List<BodyConfig>();

    [XmlArray("Engines")]
    [XmlArrayItem("Engine")]
    public List<EngineConfig> Engines { get; set; } = new List<EngineConfig>();

    [XmlArray("Suspensions")]
    [XmlArrayItem("Suspension")]
    public List<SuspensionConfig> Suspensions { get; set; } = new List<SuspensionConfig>();

    [XmlArray("Skins")]
    [XmlArrayItem("Skin")]
    public List<SkinConfig> Skins { get; set; } = new List<SkinConfig>();

    [XmlArray("Weapons")]
    [XmlArrayItem("Weapon")]
    public List<WeaponConfig> Weapons { get; set; } = new List<WeaponConfig>();

    [XmlArray("Biomes")]
    [XmlArrayItem("string")]
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

    public void MergeFromSaves(List<SaveModel> saves)
    {
      foreach (var save in saves)
      {
        if (!string.IsNullOrEmpty(save.Kit?.Body) && !Bodies.Exists(b => b.Name == save.Kit.Body))
          Bodies.Add(new BodyConfig { Name = save.Kit.Body });

        if (!string.IsNullOrEmpty(save.Kit?.Engine) && !Engines.Exists(e => e.Name == save.Kit.Engine))
          Engines.Add(new EngineConfig { Name = save.Kit.Engine });

        if (!string.IsNullOrEmpty(save.Kit?.Suspension) && !Suspensions.Exists(s => s.Name == save.Kit.Suspension))
          Suspensions.Add(new SuspensionConfig { Name = save.Kit.Suspension });

        if (!string.IsNullOrEmpty(save.Kit?.Skin) && !Skins.Exists(s => s.Name == save.Kit.Skin))
          Skins.Add(new SkinConfig { Name = save.Kit.Skin });

        if (!string.IsNullOrEmpty(save.Progress?.Biome) && !Biomes.Contains(save.Progress.Biome))
          Biomes.Add(save.Progress.Biome);

        if (save.Kit?.Modules != null)
        {
          foreach (var weapon in save.Kit.Modules)
          {
            if (!string.IsNullOrEmpty(weapon) && !Weapons.Exists(w => w.Name == weapon))
              Weapons.Add(new WeaponConfig { Name = weapon });
          }
        }
      }

      Bodies = Bodies.OrderBy(b => b.Name).ToList();
      Engines = Engines.OrderBy(e => e.Name).ToList();
      Suspensions = Suspensions.OrderBy(s => s.Name).ToList();
      Skins = Skins.OrderBy(s => s.Name).ToList();
      Biomes = Biomes.OrderBy(b => b).ToList();
      Weapons = Weapons.OrderBy(w => w.Name).ToList();
    }

    // Nested classes
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