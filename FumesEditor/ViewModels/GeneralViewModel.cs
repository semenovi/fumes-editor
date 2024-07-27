using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using FumesEditor.Models;

namespace FumesEditor.ViewModels
{
  public class GeneralViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;
    private Configuration _configuration;

    public SaveModel SaveModel
    {
      get => _saveModel;
      set
      {
        _saveModel = value;
        OnPropertyChanged(nameof(SaveModel));
        OnPropertyChanged(nameof(Version));
        OnPropertyChanged(nameof(Boss));
        OnPropertyChanged(nameof(Run));
        OnPropertyChanged(nameof(GameFinished));
        OnPropertyChanged(nameof(Biome));
        OnPropertyChanged(nameof(BiomeProgress));
      }
    }

    public Configuration Configuration
    {
      get => _configuration;
      set
      {
        _configuration = value;
        OnPropertyChanged(nameof(Configuration));
        OnPropertyChanged(nameof(Biomes));
      }
    }

    public List<string> Biomes => Configuration?.Biomes ?? new List<string>();

    public int Version
    {
      get => SaveModel?.Version ?? 0;
      set
      {
        if (SaveModel != null && SaveModel.Version != value)
        {
          SaveModel.Version = value;
          OnPropertyChanged();
        }
      }
    }

    public int Boss
    {
      get => SaveModel?.Progress?.Boss ?? 0;
      set
      {
        if (SaveModel?.Progress != null && SaveModel.Progress.Boss != value)
        {
          SaveModel.Progress.Boss = value;
          OnPropertyChanged();
        }
      }
    }

    public int Run
    {
      get => SaveModel?.Progress?.Run ?? 0;
      set
      {
        if (SaveModel?.Progress != null && SaveModel.Progress.Run != value)
        {
          SaveModel.Progress.Run = value;
          OnPropertyChanged();
        }
      }
    }

    public bool GameFinished
    {
      get => SaveModel?.Progress?.GameFinished ?? false;
      set
      {
        if (SaveModel?.Progress != null && SaveModel.Progress.GameFinished != value)
        {
          SaveModel.Progress.GameFinished = value;
          OnPropertyChanged();
        }
      }
    }

    public string Biome
    {
      get => SaveModel?.Progress?.Biome;
      set
      {
        if (SaveModel?.Progress != null && SaveModel.Progress.Biome != value)
        {
          SaveModel.Progress.Biome = value;
          OnPropertyChanged();
        }
      }
    }

    public float BiomeProgress
    {
      get => SaveModel?.Progress?.BiomeProgress ?? 0;
      set
      {
        if (SaveModel?.Progress != null && SaveModel.Progress.BiomeProgress != value)
        {
          SaveModel.Progress.BiomeProgress = value;
          OnPropertyChanged();
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}