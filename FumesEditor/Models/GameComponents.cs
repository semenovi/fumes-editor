using System.Collections.Generic;
using System.Linq;

namespace FumesEditor.Models
{
  public static class GameComponents
  {
    public static readonly List<string> Bodies = new List<string>
    {
      "body-cub", "body-jook", "body-caro", "body-polon", "body-tartarus", "body-flakwagon", 
      "body-warhorse", "body-griffin", "body-warthog", "body-unitrack"
    };

    public static readonly List<string> Engines = new List<string>
    {
      "engine-inline-3", "engine-boxer-4", "engine-v8-classic", "engine-crossplane-4", 
      "engine-diesel-3", "engine-v8", "engine-diesel-4"
    };

    public static readonly Dictionary<string, List<string>> SuspensionsByVehicle = new Dictionary<string, List<string>>
    {
      ["Cricket"] = new List<string> { "stock", "off-road", "monster", "half-track", "bullet", "digger", "rodded", "rover", "truck" },
      ["Polon"] = new List<string> { "stock", "bullet", "truck", "half-track", "off-road", "monster", "mudster", "rover", "digger", "rallye", "rodded" },
      ["Caro"] = new List<string> { "stock", "rodded", "monster", "digger", "half-track", "off-road", "bullet" },
      ["Jook"] = new List<string> { "stock", "digger", "half-track", "off-road", "rodded", "monster", "bullet" },
      ["Tartarus"] = new List<string> { "stock", "bullet", "rodded", "monster", "off-road", "half-track", "digger", "mudster" },
      ["Flakwagon"] = new List<string> { "standard", "apc", "digger", "monster", "off-road", "bullet", "half-track" },
      ["Warhorse"] = new List<string> { "stock", "monster", "digger", "rodded", "off-road", "half-track", "bullet" },
      ["Griffin"] = new List<string> { "stock", "monster", "digger", "off-road", "rodded", "mud", "bullet", "half-track", "rover" },
      ["Warthog"] = new List<string> { "stock", "digger", "off-road", "rodded" },
      ["Unitrack"] = new List<string> { "stock", "digger", "bullet", "half-track" }
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
        return id.Replace("body-", "").Replace("-", " ");
      
      if (id.StartsWith("engine-"))
        return id.Replace("engine-", "").Replace("-", " ");
      
      if (id.StartsWith("weapon-"))
        return id.Replace("weapon-", "").Replace("-", " ");
      
      if (id.StartsWith("cargo-"))
        return id.Replace("cargo-", "").Replace("-", " ");

      return id;
    }
  }
}