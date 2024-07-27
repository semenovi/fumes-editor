using System;
using System.Xml.Linq;
using System.Linq;
using FumesEditor.Models;
using System.Diagnostics;
using System.Globalization;

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

        if (root == null)
        {
          throw new Exception("XML root element is null.");
        }

        if (root.Name.LocalName != "Save")
        {
          root = root.Element("Save");
          if (root == null)
          {
            throw new Exception("Cannot find 'Save' element in the XML.");
          }
        }

        SaveModel save = new SaveModel
        {
          Version = int.Parse(GetElementValue(root, "Version")),
          Progress = DeserializeProgress(root.Element("Progress")),
          Stats = DeserializeStats(root.Element("Stats")),
          Items = root.Element("Items")?.Elements("Item").Select(e => e.Value).ToList() ?? new List<string>(),
          UnlockedItems = root.Element("UnlockedItems")?.Elements("Item").Select(e => e.Value).ToList() ?? new List<string>(),
          CustomSkins = root.Element("CustomSkins")?.Elements("Skin").Select(DeserializeCustomSkin).ToList() ?? new List<CustomSkin>(),
          Kit = DeserializeKit(root.Element("Kit"))
        };

        return save;
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Error deserializing XML: {ex.Message}");
        Debug.WriteLine($"XML Content: {xmlContent}");
        throw new Exception($"Error deserializing save file: {ex.Message}", ex);
      }
    }

    private static string GetElementValue(XElement parent, string elementName)
    {
      return parent.Element(elementName)?.Value ?? string.Empty;
    }

    private static Progress DeserializeProgress(XElement element)
    {
      if (element == null) return new Progress();

      return new Progress
      {
        Boss = int.Parse(GetElementValue(element, "Boss")),
        Run = int.Parse(GetElementValue(element, "Run")),
        GameFinished = bool.Parse(GetElementValue(element, "GameFinished")),
        Biome = GetElementValue(element, "Biome"),
        BiomeProgress = float.Parse(GetElementValue(element, "BiomeProgress"), CultureInfo.InvariantCulture),
        Missions = element.Element("Missions")?.Elements("Mission").Select(e => e.Value).ToList() ?? new List<string>()
      };
    }

    private static Stats DeserializeStats(XElement element)
    {
      if (element == null) return new Stats();

      return new Stats
      {
        TotalPlayTime = float.Parse(GetElementValue(element, "TotalPlayTime"), CultureInfo.InvariantCulture),
        TotalRunsTime = float.Parse(GetElementValue(element, "TotalRunsTime"), CultureInfo.InvariantCulture),
        TotalBossTime = float.Parse(GetElementValue(element, "TotalBossTime"), CultureInfo.InvariantCulture),
        TotalMileage = float.Parse(GetElementValue(element, "TotalMileage"), CultureInfo.InvariantCulture),
        FreeRoamMileage = float.Parse(GetElementValue(element, "FreeRoamMileage"), CultureInfo.InvariantCulture),
        RoadMileage = float.Parse(GetElementValue(element, "RoadMileage"), CultureInfo.InvariantCulture),
        OffRoadMileage = float.Parse(GetElementValue(element, "OffRoadMileage"), CultureInfo.InvariantCulture),
        RaceMileage = float.Parse(GetElementValue(element, "RaceMileage"), CultureInfo.InvariantCulture),
        AirTime = float.Parse(GetElementValue(element, "AirTime"), CultureInfo.InvariantCulture),
        ScrappersVisits = int.Parse(GetElementValue(element, "ScrappersVisits")),
        FiredBullets = int.Parse(GetElementValue(element, "FiredBullets")),
        ScrappedEnemies = int.Parse(GetElementValue(element, "ScrappedEnemies")),
        ScrappedBosses = int.Parse(GetElementValue(element, "ScrappedBosses")),
        Deaths = int.Parse(GetElementValue(element, "Deaths"))
      };
    }

    private static CustomSkin DeserializeCustomSkin(XElement element)
    {
      if (element == null) return new CustomSkin();

      return new CustomSkin
      {
        Version = int.Parse(GetElementValue(element, "Version")),
        Id = GetElementValue(element, "Id"),
        Label = GetElementValue(element, "Label"),
        Body = GetElementValue(element, "Body"),
        BodyColor = DeserializeColor(element.Element("BodyColor")),
        LampsColor = DeserializeColor(element.Element("LampsColor")),
        TopLayers = DeserializeLayers(element.Element("TopLayers")),
        FrontLayers = DeserializeLayers(element.Element("FrontLayers")),
        BackLayers = DeserializeLayers(element.Element("BackLayers")),
        RightLayers = DeserializeLayers(element.Element("RightLayers")),
        LeftLayers = DeserializeLayers(element.Element("LeftLayers"))
      };
    }

    private static List<Layer> DeserializeLayers(XElement element)
    {
      if (element == null) return new List<Layer>();

      return element.Elements("Layer").Select(layerElement => new Layer
      {
        Sticker = GetElementValue(layerElement, "Sticker"),
        Position = DeserializePosition(layerElement.Element("Position")),
        Scale = DeserializeScale(layerElement.Element("Scale")),
        Rotation = float.Parse(GetElementValue(layerElement, "Rotation"), CultureInfo.InvariantCulture),
        Color = DeserializeColor(layerElement.Element("Color")),
        FlipX = bool.Parse(GetElementValue(layerElement, "FlipX")),
        FlipY = bool.Parse(GetElementValue(layerElement, "FlipY")),
        Smooth = bool.Parse(GetElementValue(layerElement, "Smooth")),
        Mirror = int.Parse(GetElementValue(layerElement, "Mirror"))
      }).ToList();
    }

    private static Position DeserializePosition(XElement element)
    {
      if (element == null) return new Position();

      return new Position
      {
        x = float.Parse(GetElementValue(element, "x"), CultureInfo.InvariantCulture),
        y = float.Parse(GetElementValue(element, "y"), CultureInfo.InvariantCulture)
      };
    }

    private static Scale DeserializeScale(XElement element)
    {
      if (element == null) return new Scale();

      return new Scale
      {
        x = float.Parse(GetElementValue(element, "x"), CultureInfo.InvariantCulture),
        y = float.Parse(GetElementValue(element, "y"), CultureInfo.InvariantCulture)
      };
    }

    private static Kit DeserializeKit(XElement element)
    {
      if (element == null) return new Kit();

      return new Kit
      {
        Body = GetElementValue(element, "Body"),
        Engine = GetElementValue(element, "Engine"),
        Suspension = GetElementValue(element, "Suspension"),
        Skin = GetElementValue(element, "Skin"),
        Color = DeserializeColor(element.Element("Color")),
        LicensePlate = GetElementValue(element, "LicensePlate"),
        Modules = element.Elements("Modules").Select(e => e.Value).ToList(),
        FireGroups = element.Elements("FireGroups").Select(e => int.Parse(e.Value)).ToList()
      };
    }

    private static Color DeserializeColor(XElement element)
    {
      if (element == null) return new Color();

      return new Color
      {
        R = float.Parse(GetElementValue(element, "R"), CultureInfo.InvariantCulture),
        G = float.Parse(GetElementValue(element, "G"), CultureInfo.InvariantCulture),
        B = float.Parse(GetElementValue(element, "B"), CultureInfo.InvariantCulture),
        A = float.Parse(GetElementValue(element, "A"), CultureInfo.InvariantCulture)
      };
    }
  }
}