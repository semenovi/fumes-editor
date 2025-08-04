using System;
using System.Collections.Generic;

namespace FumesEditor.Helpers
{
  public static class TypoHandler
  {
    private static readonly Dictionary<string, string> TypoMappings = new Dictionary<string, string>
    {
      ["suspension-tartatrus-stock"] = "suspension-tartarus-stock",
      ["suspension-tartatrus-rodded"] = "suspension-tartarus-rodded", 
      ["suspension-tartatrus-monster"] = "suspension-tartarus-monster",
      ["suspension-tartatrus-mudster"] = "suspension-tartarus-mudster",
      ["suspension-tartatrus-off-road"] = "suspension-tartarus-off-road"
    };

    private static readonly Dictionary<string, string> ReverseMappings = new Dictionary<string, string>();

    static TypoHandler()
    {
      foreach (var kvp in TypoMappings)
      {
        ReverseMappings[kvp.Value] = kvp.Key;
      }
    }

    public static string FixTypo(string itemId, HashSet<string> typoTracker = null)
    {
      if (TypoMappings.TryGetValue(itemId, out string correctedId))
      {
        typoTracker?.Add(correctedId);
        return correctedId;
      }
      return itemId;
    }

    public static string RestoreTypoIfOriginal(string itemId, HashSet<string> originalTypos)
    {
      if (originalTypos.Contains(itemId))
      {
        return ReverseMappings.TryGetValue(itemId, out string originalId) ? originalId : itemId;
      }
      return itemId;
    }

    public static bool IsTypo(string itemId)
    {
      return TypoMappings.ContainsKey(itemId);
    }

    public static bool HasTypoVariant(string itemId)
    {
      return ReverseMappings.ContainsKey(itemId);
    }
  }
}