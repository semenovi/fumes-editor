using System;
using System.Collections.Generic;

namespace FumesEditor
{
  public class SaveModel
  {
    public int Version { get; set; }
    public Progress Progress { get; set; }
    public Stats Stats { get; set; }
    public List<string> Items { get; set; }
    public List<string> UnlockedItems { get; set; }
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
    public List<string> Missions { get; set; }
  }

  public class Stats
  {
    public float TotalPlayTime { get; set; }
    public float TotalRunsTime { get; set; }
    public float TotalBossTime { get; set; }
    public float TotalMileage { get; set; }
    // Add other stats as needed
  }

  public class CustomSkin
  {
    public int Version { get; set; }
    public string Id { get; set; }
    public string Label { get; set; }
    public string Body { get; set; }
    // Add other skin properties as needed
  }

  public class Kit
  {
    public string Body { get; set; }
    public string Engine { get; set; }
    public string Suspension { get; set; }
    public string Skin { get; set; }
    public Color Color { get; set; }
    public string LicensePlate { get; set; }
    public List<string> Modules { get; set; }
    public List<int> FireGroups { get; set; }
  }

  public class Color
  {
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
  }
}