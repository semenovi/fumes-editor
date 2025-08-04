using System.Collections.Generic;
using System.Linq;

namespace FumesEditor.Models
{
  public static class GameComponents
  {
    public static readonly List<string> Bodies = new List<string>
    {
      "body-cub", "body-jook", "body-caro", "body-polon", "body-tartarus", "body-flakwagon", 
      "body-warhorse", "body-griffin", "body-warthog", "body-unitrack", "body-cricket", "body-bizon"
    };

    public static readonly List<string> Engines = new List<string>
    {
      "engine-inline-3", "engine-boxer-4", "engine-v8-classic", "engine-crossplane-4", 
      "engine-diesel-3", "engine-v8", "engine-diesel-4"
    };

    public static readonly Dictionary<string, List<string>> SuspensionsByVehicle = new Dictionary<string, List<string>>
    {
      ["Cricket"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Polon"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Caro"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Jook"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Tartarus"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Flakwagon"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Warhorse"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Griffin"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Warthog"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Unitrack"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
      ["Bizon"] = new List<string> {
        "stock",
        "monster",
        "digger",
        "off-road",
        "rodded",
        "mud",
        "bullet",
        "half-track",
        "rover",
        "truck"
      },
    };

    public static readonly List<string> Weapons = new List<string>
    {
      "weapon-automat", "weapon-auto-shellgun", "weapon-shellgun", "weapon-orkan-battery", 
      "weapon-charge-thrower", "weapon-pyreblitz", "weapon-minekraft", "weapon-rotor", 
      "weapon-rocket-launcher", "weapon-auto-cannon", "weapon-mortar-engine"
    };

    public static readonly List<string> CargoModules = new List<string>
    {
      "cargo-module-small", "cargo-module-medium", "cargo-module-large"
    };

    public static List<string> GetAllSuspensions()
    {
      var suspensions = new List<string>();
      foreach (var vehicle in SuspensionsByVehicle)
      {
        foreach (var type in vehicle.Value)
        {
          suspensions.Add($"suspension-{vehicle.Key.ToLower()}-{type}");
        }
      }
      return suspensions;
    }

    public static List<string> GetAllModules()
    {
      var modules = new List<string>();
      modules.AddRange(Weapons);
      modules.AddRange(CargoModules);
      return modules;
    }

    public static string GetDisplayName(string id)
    {
      if (id.StartsWith("suspension-"))
      {
        var parts = id.Replace("suspension-", "").Split('-');
        if (parts.Length >= 2)
        {
          var vehicle = parts[0];
          var type = string.Join("-", parts.Skip(1));
          return $"{char.ToUpper(vehicle[0])}{vehicle.Substring(1)} - {char.ToUpper(type[0])}{type.Substring(1).Replace("-", " ")}";
        }
      }
      
      if (id.StartsWith("body-"))
        return char.ToUpper(id[5]) + id.Substring(6).Replace("-", " ");
      
      if (id.StartsWith("engine-"))
        return char.ToUpper(id[7]) + id.Substring(8).Replace("-", " ");
      
      if (id.StartsWith("weapon-"))
        return char.ToUpper(id[7]) + id.Substring(8).Replace("-", " ");
      
      if (id.StartsWith("cargo-"))
        return char.ToUpper(id[6]) + id.Substring(7).Replace("-", " ");

      return id;
    }

    public static List<string> GetAllComponents()
    {
      var components = new List<string>();
      components.AddRange(Bodies);
      components.AddRange(Engines);
      components.AddRange(GetAllSuspensions());
      components.AddRange(Weapons);
      components.AddRange(CargoModules);
      return components;
    }
  }
}