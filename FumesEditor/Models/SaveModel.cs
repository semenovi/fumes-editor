using System.Collections.Generic;

namespace FumesEditor.Models
{
  public class SaveModel
  {
    public Progress Progress { get; set; }
    public Stats Stats { get; set; }
    public Config Config { get; set; }
    public List<Config> Configs { get; set; } = new List<Config>();
    public List<Item> Items { get; set; } = new List<Item>();
    public List<string> UnlockedSkins { get; set; } = new List<string>();
    public List<CustomSkin> CustomSkins { get; set; } = new List<CustomSkin>();
    public int CargoCount { get; set; }
  }

  public class Progress
  {
    public List<string> Nodes { get; set; } = new List<string>();
    public List<Anomaly> Anomalies { get; set; } = new List<Anomaly>();
    public int EndgameBossSeed { get; set; }
  }

  public class Anomaly
  {
    public string Id { get; set; }
    public int Cargo { get; set; }
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

  public class Item
  {
    public string Id { get; set; }
    public List<float> Stats { get; set; } = new List<float>();
  }

  public class CustomSkin
  {
    public int Version { get; set; }
    public long Id { get; set; }
    public string Label { get; set; }
    public string Body { get; set; }
    public Color BodyColor { get; set; }
    public Color LampsColor { get; set; }
    public List<SkinLayer> TopLayers { get; set; } = new List<SkinLayer>();
    public List<SkinLayer> FrontLayers { get; set; } = new List<SkinLayer>();
    public List<SkinLayer> BackLayers { get; set; } = new List<SkinLayer>();
    public List<SkinLayer> RightLayers { get; set; } = new List<SkinLayer>();
    public List<SkinLayer> LeftLayers { get; set; } = new List<SkinLayer>();
  }

  public class SkinLayer
  {
    public string Sticker { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
    public Color Color { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public bool Smooth { get; set; }
    public int Mirror { get; set; }
  }

  public class Vector2
  {
    public float X { get; set; }
    public float Y { get; set; }
  }
}