using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FumesEditor.Models
{
  [XmlRoot("Save")]
  public class SaveModel
  {
    public int Version { get; set; }
    public Progress Progress { get; set; }
    public Stats Stats { get; set; }
    [XmlArrayItem("Item")]
    public List<string> Items { get; set; }
    [XmlArrayItem("Item")]
    public List<string> UnlockedItems { get; set; }
    [XmlArrayItem("Skin")]
    public List<CustomSkin> CustomSkins { get; set; }
    public Kit Kit { get; set; }
  }

  public class Progress
  {
    public int Boss { get; set; }
    public int Run { get; set; }
    public bool GameFinished { get; set; }
    public string Biome { get; set; }
    public float BiomeProgress { get; set; }
    [XmlArrayItem("Mission")]
    public List<string> Missions { get; set; }
  }

  public class Stats
  {
    public float TotalPlayTime { get; set; }
    public float TotalRunsTime { get; set; }
    public float TotalBossTime { get; set; }
    public float TotalMileage { get; set; }
    public float FreeRoamMileage { get; set; }
    public float RoadMileage { get; set; }
    public float OffRoadMileage { get; set; }
    public float RaceMileage { get; set; }
    public float AirTime { get; set; }
    public int ScrappersVisits { get; set; }
    public int FiredBullets { get; set; }
    public int ScrappedEnemies { get; set; }
    public int ScrappedBosses { get; set; }
    public int Deaths { get; set; }
  }

  public class CustomSkin
  {
    public int Version { get; set; }
    public string Id { get; set; }
    public string Label { get; set; }
    public string Body { get; set; }
    public Color BodyColor { get; set; }
    public Color LampsColor { get; set; }
    [XmlArrayItem("Layer")]
    public List<Layer> TopLayers { get; set; }
    [XmlArrayItem("Layer")]
    public List<Layer> FrontLayers { get; set; }
    [XmlArrayItem("Layer")]
    public List<Layer> BackLayers { get; set; }
    [XmlArrayItem("Layer")]
    public List<Layer> RightLayers { get; set; }
    [XmlArrayItem("Layer")]
    public List<Layer> LeftLayers { get; set; }
  }

  public class Layer
  {
    public string Sticker { get; set; }
    public Position Position { get; set; }
    public Scale Scale { get; set; }
    public float Rotation { get; set; }
    public Color Color { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public bool Smooth { get; set; }
    public int Mirror { get; set; }
  }

  public class Position
  {
    public float x { get; set; }
    public float y { get; set; }
  }

  public class Scale
  {
    public float x { get; set; }
    public float y { get; set; }
  }

  public class Kit
  {
    public string Body { get; set; }
    public string Engine { get; set; }
    public string Suspension { get; set; }
    public string Skin { get; set; }
    public Color Color { get; set; }
    public string LicensePlate { get; set; }
    [XmlElement("Modules")]
    public List<string> Modules { get; set; }
    [XmlElement("FireGroups")]
    public List<int> FireGroups { get; set; }
  }

  public class Color
  {
    [XmlElement(ElementName = "R")]
    public float R { get; set; }
    [XmlElement(ElementName = "G")]
    public float G { get; set; }
    [XmlElement(ElementName = "B")]
    public float B { get; set; }
    [XmlElement(ElementName = "A")]
    public float A { get; set; }
  }
}