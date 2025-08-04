using System.Collections.Generic;

namespace FumesEditor.Models
{
  public class Config
  {
    public ComponentWithStats Body { get; set; }
    public Component Engine { get; set; }
    public ComponentWithStats Suspension { get; set; }
    public Color BodyColor { get; set; }
    public string Skin { get; set; }
    public string LicensePlate { get; set; }
    public List<ComponentWithStats> Modules { get; set; } = new List<ComponentWithStats>();
    public List<int> FireGroups { get; set; } = new List<int>();
    public List<string> HiddenParts { get; set; } = new List<string>();
  }

  public class Component
  {
    public string Id { get; set; }
  }

  public class ComponentWithStats : Component
  {
    public List<float> Stats { get; set; } = new List<float>();
  }

  public class Color
  {
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; } = 1.0f;
  }
}