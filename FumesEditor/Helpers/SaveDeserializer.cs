using System;
using System.Xml.Linq;
using System.Linq;
using FumesEditor.Models;
using System.Globalization;
using System.Collections.Generic;

namespace FumesEditor.Helpers
{
  public static class SaveDeserializer
  {
    public static SaveModel Deserialize(string xmlContent)
    {
      try
      {
        XDocument doc = XDocument.Parse(xmlContent);
        XElement root = doc.Root;

        if (root?.Name.LocalName != "Save")
        {
          throw new Exception("Invalid save file format.");
        }

        var save = new SaveModel
        {
          Progress = DeserializeProgress(root.Element("Progress")),
          Stats = DeserializeStats(root.Element("Stats")),
          Config = DeserializeConfig(root.Element("Config")),
          Configs = root.Element("Configs")?.Elements("Config").Select(DeserializeConfig).ToList() ?? new List<Config>(),
          Items = root.Element("Items")?.Elements("Item").Select(DeserializeItem).ToList() ?? new List<Item>(),
          UnlockedSkins = root.Element("UnlockedSkins")?.Elements("Skin").Select(e => e.Value).ToList() ?? new List<string>(),
          CustomSkins = root.Element("CustomSkins")?.Elements("Skin").Select(DeserializeCustomSkin).ToList() ?? new List<CustomSkin>(),
          CargoCount = int.TryParse(GetElementValue(root, "CargoCount"), out int cargoCount) ? cargoCount : 0
        };

        return save;
      }
      catch (Exception ex)
      {
        throw new Exception($"Error loading save file: {ex.Message}", ex);
      }
    }

    public static string Serialize(SaveModel save)
    {
      var root = new XElement("Save");
      
      root.SetAttributeValue(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema");
      root.SetAttributeValue(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance");

      if (save.Progress != null)
        root.Add(SerializeProgress(save.Progress));

      if (save.Stats != null)
        root.Add(SerializeStats(save.Stats));

      if (save.Config != null)
        root.Add(SerializeConfig(save.Config, "Config"));

      if (save.Configs?.Any() == true)
      {
        root.Add(new XElement("Configs", save.Configs.Select(c => SerializeConfig(c, "Config"))));
      }

      if (save.Items?.Any() == true)
      {
        root.Add(new XElement("Items", save.Items.Select(SerializeItem)));
      }

      if (save.UnlockedSkins?.Any() == true)
      {
        root.Add(new XElement("UnlockedSkins", save.UnlockedSkins.Select(s => new XElement("Skin", s))));
      }

      if (save.CustomSkins?.Any() == true)
      {
        root.Add(new XElement("CustomSkins", save.CustomSkins.Select(SerializeCustomSkin)));
      }

      root.Add(new XElement("CargoCount", save.CargoCount));

      var doc = new XDocument(
        new XDeclaration("1.0", "utf-8", null),
        root
      );

      return doc.ToString();
    }

    private static string GetElementValue(XElement parent, string elementName)
    {
      return parent.Element(elementName)?.Value ?? string.Empty;
    }

    private static Config DeserializeConfig(XElement element)
    {
      if (element == null) return new Config();

      return new Config
      {
        Body = DeserializeComponentWithStats(element.Element("Body")),
        Engine = DeserializeComponent(element.Element("Engine")),
        Suspension = DeserializeComponentWithStats(element.Element("Suspension")),
        BodyColor = DeserializeColor(element.Element("BodyColor")),
        Skin = GetElementValue(element, "Skin"),
        LicensePlate = GetElementValue(element, "LicensePlate"),
        Modules = element.Elements("Modules").Select(DeserializeComponentWithStats).ToList(),
        FireGroups = element.Elements("FireGroups")
          .Select(e => int.TryParse(e.Value, out int fireGroup) ? fireGroup : 0)
          .ToList(),
        HiddenParts = element.Elements("HiddenParts").Select(e => e.Value).ToList()
      };
    }

    private static Component DeserializeComponent(XElement element)
    {
      if (element == null) return new Component();
      return new Component { Id = GetElementValue(element, "Id") };
    }

    private static ComponentWithStats DeserializeComponentWithStats(XElement element)
    {
      if (element == null) return new ComponentWithStats();
      
      return new ComponentWithStats
      {
        Id = GetElementValue(element, "Id"),
        Stats = element.Element("Stats")?.Elements("Stat")
          .Select(e => float.TryParse(e.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float stat) ? stat : 0f)
          .ToList() ?? new List<float>()
      };
    }

    private static Models.Color DeserializeColor(XElement element)
    {
      if (element == null) return new Models.Color { R = 1, G = 1, B = 1, A = 1 };

      return new Models.Color
      {
        R = float.TryParse(GetElementValue(element, "R"), NumberStyles.Float, CultureInfo.InvariantCulture, out float r) ? r : 1f,
        G = float.TryParse(GetElementValue(element, "G"), NumberStyles.Float, CultureInfo.InvariantCulture, out float g) ? g : 1f,
        B = float.TryParse(GetElementValue(element, "B"), NumberStyles.Float, CultureInfo.InvariantCulture, out float b) ? b : 1f,
        A = float.TryParse(GetElementValue(element, "A"), NumberStyles.Float, CultureInfo.InvariantCulture, out float a) ? a : 1f
      };
    }

    private static XElement SerializeConfig(Config config, string elementName)
    {
      var element = new XElement(elementName);

      if (config.Body != null)
        element.Add(SerializeComponentWithStats(config.Body, "Body"));

      if (config.Engine != null)
        element.Add(SerializeComponent(config.Engine, "Engine"));

      if (config.Suspension != null)
        element.Add(SerializeComponentWithStats(config.Suspension, "Suspension"));

      if (config.BodyColor != null)
        element.Add(SerializeColor(config.BodyColor, "BodyColor"));

      if (!string.IsNullOrEmpty(config.Skin))
        element.Add(new XElement("Skin", config.Skin));

      if (!string.IsNullOrEmpty(config.LicensePlate))
        element.Add(new XElement("LicensePlate", config.LicensePlate));

      foreach (var module in config.Modules ?? new List<ComponentWithStats>())
        element.Add(SerializeComponentWithStats(module, "Modules"));

      foreach (var fireGroup in config.FireGroups ?? new List<int>())
        element.Add(new XElement("FireGroups", fireGroup));

      foreach (var hiddenPart in config.HiddenParts ?? new List<string>())
        element.Add(new XElement("HiddenParts", hiddenPart));

      return element;
    }

    private static XElement SerializeComponent(Component component, string elementName)
    {
      return new XElement(elementName, new XElement("Id", component.Id));
    }

    private static XElement SerializeComponentWithStats(ComponentWithStats component, string elementName)
    {
      var element = new XElement(elementName, new XElement("Id", component.Id));

      if (component.Stats?.Any() == true)
      {
        element.Add(new XElement("Stats", 
          component.Stats.Select(s => new XElement("Stat", s.ToString("R", CultureInfo.InvariantCulture)))
        ));
      }

      return element;
    }

    private static XElement SerializeColor(Models.Color color, string elementName)
    {
      return new XElement(elementName,
        new XElement("R", color.R.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("G", color.G.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("B", color.B.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("A", color.A.ToString("R", CultureInfo.InvariantCulture))
      );
    }

    private static Progress DeserializeProgress(XElement element)
    {
      if (element == null) return new Progress();

      var progress = new Progress
      {
        EndgameBossSeed = int.TryParse(GetElementValue(element, "EndgameBossSeed"), out int seed) ? seed : 0
      };

      var progressElement = element.Element("Progress");
      if (progressElement != null)
      {
        progress.Nodes = progressElement.Elements("Node").Select(e => e.Value).ToList();
      }

      var anomaliesElement = element.Element("Anomalies");
      if (anomaliesElement != null)
      {
        progress.Anomalies = anomaliesElement.Elements("Anomaly").Select(DeserializeAnomaly).ToList();
      }

      return progress;
    }

    private static Anomaly DeserializeAnomaly(XElement element)
    {
      if (element == null) return new Anomaly();

      return new Anomaly
      {
        Id = GetElementValue(element, "id"),
        Cargo = int.TryParse(GetElementValue(element, "cargo"), out int cargo) ? cargo : 0
      };
    }

    private static Stats DeserializeStats(XElement element)
    {
      if (element == null) return new Stats();

      return new Stats
      {
        TotalPlayTime = float.TryParse(GetElementValue(element, "TotalPlayTime"), NumberStyles.Float, CultureInfo.InvariantCulture, out float totalPlayTime) ? totalPlayTime : 0f,
        TotalRunsTime = float.TryParse(GetElementValue(element, "TotalRunsTime"), NumberStyles.Float, CultureInfo.InvariantCulture, out float totalRunsTime) ? totalRunsTime : 0f,
        TotalBossTime = float.TryParse(GetElementValue(element, "TotalBossTime"), NumberStyles.Float, CultureInfo.InvariantCulture, out float totalBossTime) ? totalBossTime : 0f,
        TotalMileage = float.TryParse(GetElementValue(element, "TotalMileage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float totalMileage) ? totalMileage : 0f,
        FreeRoamMileage = float.TryParse(GetElementValue(element, "FreeRoamMileage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float freeRoamMileage) ? freeRoamMileage : 0f,
        RoadMileage = float.TryParse(GetElementValue(element, "RoadMileage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float roadMileage) ? roadMileage : 0f,
        OffRoadMileage = float.TryParse(GetElementValue(element, "OffRoadMileage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float offRoadMileage) ? offRoadMileage : 0f,
        RaceMileage = float.TryParse(GetElementValue(element, "RaceMileage"), NumberStyles.Float, CultureInfo.InvariantCulture, out float raceMileage) ? raceMileage : 0f,
        AirTime = float.TryParse(GetElementValue(element, "AirTime"), NumberStyles.Float, CultureInfo.InvariantCulture, out float airTime) ? airTime : 0f,
        ScrappersVisits = int.TryParse(GetElementValue(element, "ScrappersVisits"), out int scrappersVisits) ? scrappersVisits : 0,
        FiredBullets = int.TryParse(GetElementValue(element, "FiredBullets"), out int firedBullets) ? firedBullets : 0,
        ScrappedEnemies = int.TryParse(GetElementValue(element, "ScrappedEnemies"), out int scrappedEnemies) ? scrappedEnemies : 0,
        ScrappedBosses = int.TryParse(GetElementValue(element, "ScrappedBosses"), out int scrappedBosses) ? scrappedBosses : 0,
        Deaths = int.TryParse(GetElementValue(element, "Deaths"), out int deaths) ? deaths : 0
      };
    }

    private static Item DeserializeItem(XElement element)
    {
      if (element == null) return new Item();

      return new Item
      {
        Id = GetElementValue(element, "Id"),
        Stats = element.Element("Stats")?.Elements("Stat")
          .Select(e => float.TryParse(e.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float stat) ? stat : 0f)
          .ToList() ?? new List<float>()
      };
    }

    private static XElement SerializeProgress(Progress progress)
    {
      var element = new XElement("Progress");

      if (progress.Nodes?.Any() == true)
      {
        element.Add(new XElement("Progress", progress.Nodes.Select(n => new XElement("Node", n))));
      }

      if (progress.Anomalies?.Any() == true)
      {
        element.Add(new XElement("Anomalies", progress.Anomalies.Select(SerializeAnomaly)));
      }

      element.Add(new XElement("EndgameBossSeed", progress.EndgameBossSeed));

      return element;
    }

    private static XElement SerializeAnomaly(Anomaly anomaly)
    {
      return new XElement("Anomaly",
        new XElement("id", anomaly.Id),
        new XElement("cargo", anomaly.Cargo)
      );
    }

    private static XElement SerializeStats(Stats stats)
    {
      return new XElement("Stats",
        new XElement("TotalPlayTime", stats.TotalPlayTime.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("TotalRunsTime", stats.TotalRunsTime.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("TotalBossTime", stats.TotalBossTime.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("TotalMileage", stats.TotalMileage.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("FreeRoamMileage", stats.FreeRoamMileage.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("RoadMileage", stats.RoadMileage.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("OffRoadMileage", stats.OffRoadMileage.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("RaceMileage", stats.RaceMileage.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("AirTime", stats.AirTime.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("ScrappersVisits", stats.ScrappersVisits),
        new XElement("FiredBullets", stats.FiredBullets),
        new XElement("ScrappedEnemies", stats.ScrappedEnemies),
        new XElement("ScrappedBosses", stats.ScrappedBosses),
        new XElement("Deaths", stats.Deaths)
      );
    }

    private static XElement SerializeItem(Item item)
    {
      var element = new XElement("Item", new XElement("Id", item.Id));

      if (item.Stats?.Any() == true)
      {
        element.Add(new XElement("Stats", 
          item.Stats.Select(s => new XElement("Stat", s.ToString("R", CultureInfo.InvariantCulture)))
        ));
      }

      return element;
    }

    private static CustomSkin DeserializeCustomSkin(XElement element)
    {
      if (element == null) return new CustomSkin();

      return new CustomSkin
      {
        Version = int.TryParse(GetElementValue(element, "Version"), out int version) ? version : 0,
        Id = long.TryParse(GetElementValue(element, "Id"), out long id) ? id : 0,
        Label = GetElementValue(element, "Label"),
        Body = GetElementValue(element, "Body"),
        BodyColor = DeserializeColor(element.Element("BodyColor")),
        LampsColor = DeserializeColor(element.Element("LampsColor")),
        TopLayers = element.Element("TopLayers")?.Elements("Layer").Select(DeserializeSkinLayer).ToList() ?? new List<SkinLayer>(),
        FrontLayers = element.Element("FrontLayers")?.Elements("Layer").Select(DeserializeSkinLayer).ToList() ?? new List<SkinLayer>(),
        BackLayers = element.Element("BackLayers")?.Elements("Layer").Select(DeserializeSkinLayer).ToList() ?? new List<SkinLayer>(),
        RightLayers = element.Element("RightLayers")?.Elements("Layer").Select(DeserializeSkinLayer).ToList() ?? new List<SkinLayer>(),
        LeftLayers = element.Element("LeftLayers")?.Elements("Layer").Select(DeserializeSkinLayer).ToList() ?? new List<SkinLayer>()
      };
    }

    private static SkinLayer DeserializeSkinLayer(XElement element)
    {
      if (element == null) return new SkinLayer();

      return new SkinLayer
      {
        Sticker = GetElementValue(element, "Sticker"),
        Position = DeserializeVector2(element.Element("Position")),
        Scale = DeserializeVector2(element.Element("Scale")),
        Rotation = float.TryParse(GetElementValue(element, "Rotation"), NumberStyles.Float, CultureInfo.InvariantCulture, out float rotation) ? rotation : 0f,
        Color = DeserializeColor(element.Element("Color")),
        FlipX = bool.TryParse(GetElementValue(element, "FlipX"), out bool flipX) && flipX,
        FlipY = bool.TryParse(GetElementValue(element, "FlipY"), out bool flipY) && flipY,
        Smooth = bool.TryParse(GetElementValue(element, "Smooth"), out bool smooth) && smooth,
        Mirror = int.TryParse(GetElementValue(element, "Mirror"), out int mirror) ? mirror : 0
      };
    }

    private static Vector2 DeserializeVector2(XElement element)
    {
      if (element == null) return new Vector2();

      return new Vector2
      {
        X = float.TryParse(GetElementValue(element, "x"), NumberStyles.Float, CultureInfo.InvariantCulture, out float x) ? x : 0f,
        Y = float.TryParse(GetElementValue(element, "y"), NumberStyles.Float, CultureInfo.InvariantCulture, out float y) ? y : 0f
      };
    }

    private static XElement SerializeCustomSkin(CustomSkin skin)
    {
      var element = new XElement("Skin",
        new XElement("Version", skin.Version),
        new XElement("Id", skin.Id),
        new XElement("Label", skin.Label),
        new XElement("Body", skin.Body)
      );

      if (skin.BodyColor != null)
        element.Add(SerializeColor(skin.BodyColor, "BodyColor"));

      if (skin.LampsColor != null)
        element.Add(SerializeColor(skin.LampsColor, "LampsColor"));

      element.Add(new XElement("TopLayers", skin.TopLayers?.Select(SerializeSkinLayer) ?? new List<XElement>()));
      element.Add(new XElement("FrontLayers", skin.FrontLayers?.Select(SerializeSkinLayer) ?? new List<XElement>()));
      element.Add(new XElement("BackLayers", skin.BackLayers?.Select(SerializeSkinLayer) ?? new List<XElement>()));
      element.Add(new XElement("RightLayers", skin.RightLayers?.Select(SerializeSkinLayer) ?? new List<XElement>()));
      element.Add(new XElement("LeftLayers", skin.LeftLayers?.Select(SerializeSkinLayer) ?? new List<XElement>()));

      return element;
    }

    private static XElement SerializeSkinLayer(SkinLayer layer)
    {
      var element = new XElement("Layer");

      if (!string.IsNullOrEmpty(layer.Sticker))
        element.Add(new XElement("Sticker", layer.Sticker));

      if (layer.Position != null)
        element.Add(SerializeVector2(layer.Position, "Position"));

      if (layer.Scale != null)
        element.Add(SerializeVector2(layer.Scale, "Scale"));

      element.Add(new XElement("Rotation", layer.Rotation.ToString("R", CultureInfo.InvariantCulture)));

      if (layer.Color != null)
        element.Add(SerializeColor(layer.Color, "Color"));

      element.Add(new XElement("FlipX", layer.FlipX.ToString().ToLower()));
      element.Add(new XElement("FlipY", layer.FlipY.ToString().ToLower()));
      element.Add(new XElement("Smooth", layer.Smooth.ToString().ToLower()));
      element.Add(new XElement("Mirror", layer.Mirror));

      return element;
    }

    private static XElement SerializeVector2(Vector2 vector, string elementName)
    {
      return new XElement(elementName,
        new XElement("x", vector.X.ToString("R", CultureInfo.InvariantCulture)),
        new XElement("y", vector.Y.ToString("R", CultureInfo.InvariantCulture))
      );
    }
  }
}