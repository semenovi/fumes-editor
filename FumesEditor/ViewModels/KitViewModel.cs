using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows.Input;
using FumesEditor.Models;
using System.Windows.Media;

namespace FumesEditor.ViewModels
{
  public class KitViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;

    public SaveModel SaveModel
    {
      get => _saveModel;
      set
      {
        _saveModel = value;
        OnPropertyChanged();
        UpdateCurrentConfig();
      }
    }

    private Config CurrentConfig => SaveModel?.Config;

    public List<string> Bodies => GameComponents.Bodies;
    public List<string> Engines => GameComponents.Engines;
    public List<string> Suspensions => GameComponents.GetAllSuspensions();
    public List<string> Modules => GameComponents.GetAllModules();

    public string SelectedBody
    {
      get => CurrentConfig?.Body?.Id;
      set
      {
        if (CurrentConfig?.Body != null && CurrentConfig.Body.Id != value)
        {
          CurrentConfig.Body.Id = value;
          OnPropertyChanged();
        }
      }
    }

    public string SelectedEngine
    {
      get => CurrentConfig?.Engine?.Id;
      set
      {
        if (CurrentConfig?.Engine != null && CurrentConfig.Engine.Id != value)
        {
          CurrentConfig.Engine.Id = value;
          OnPropertyChanged();
        }
      }
    }

    public string SelectedSuspension
    {
      get => CurrentConfig?.Suspension?.Id;
      set
      {
        if (CurrentConfig?.Suspension != null && CurrentConfig.Suspension.Id != value)
        {
          CurrentConfig.Suspension.Id = value;
          OnPropertyChanged();
        }
      }
    }

    public System.Windows.Media.Color BodyColor
    {
      get
      {
        var color = CurrentConfig?.BodyColor;
        if (color == null) return Colors.White;
        return System.Windows.Media.Color.FromScRgb(color.A, color.R, color.G, color.B);
      }
      set
      {
        if (CurrentConfig?.BodyColor != null)
        {
          CurrentConfig.BodyColor.R = value.ScR;
          CurrentConfig.BodyColor.G = value.ScG;
          CurrentConfig.BodyColor.B = value.ScB;
          CurrentConfig.BodyColor.A = value.ScA;
          OnPropertyChanged();
        }
      }
    }

    public string LicensePlate
    {
      get => CurrentConfig?.LicensePlate ?? string.Empty;
      set
      {
        if (CurrentConfig != null && CurrentConfig.LicensePlate != value)
        {
          CurrentConfig.LicensePlate = value;
          OnPropertyChanged();
        }
      }
    }

    public List<string> ModulesList
    {
      get => CurrentConfig?.Modules?.Select(m => m.Id).ToList() ?? new List<string>();
      set
      {
        if (CurrentConfig?.Modules != null)
        {
          CurrentConfig.Modules.Clear();
          foreach (var moduleId in value)
          {
            CurrentConfig.Modules.Add(new ComponentWithStats { Id = moduleId });
          }
          OnPropertyChanged();
        }
      }
    }

    public bool HasBodyStats => CurrentConfig?.Body?.Stats?.Count >= 3;
    public bool HasSuspensionStats => CurrentConfig?.Suspension?.Stats?.Count >= 5;

    public float BodyStat1
    {
      get => CurrentConfig?.Body?.Stats?.ElementAtOrDefault(0) ?? 0f;
      set
      {
        if (CurrentConfig?.Body?.Stats != null && CurrentConfig.Body.Stats.Count > 0)
        {
          CurrentConfig.Body.Stats[0] = value;
          OnPropertyChanged();
        }
      }
    }

    public float BodyStat2
    {
      get => CurrentConfig?.Body?.Stats?.ElementAtOrDefault(1) ?? 0f;
      set
      {
        if (CurrentConfig?.Body?.Stats != null && CurrentConfig.Body.Stats.Count > 1)
        {
          CurrentConfig.Body.Stats[1] = value;
          OnPropertyChanged();
        }
      }
    }

    public float BodyStat3
    {
      get => CurrentConfig?.Body?.Stats?.ElementAtOrDefault(2) ?? 0f;
      set
      {
        if (CurrentConfig?.Body?.Stats != null && CurrentConfig.Body.Stats.Count > 2)
        {
          CurrentConfig.Body.Stats[2] = value;
          OnPropertyChanged();
        }
      }
    }

    public float SuspensionStat1
    {
      get => CurrentConfig?.Suspension?.Stats?.ElementAtOrDefault(0) ?? 0f;
      set
      {
        if (CurrentConfig?.Suspension?.Stats != null && CurrentConfig.Suspension.Stats.Count > 0)
        {
          CurrentConfig.Suspension.Stats[0] = value;
          OnPropertyChanged();
        }
      }
    }

    public float SuspensionStat2
    {
      get => CurrentConfig?.Suspension?.Stats?.ElementAtOrDefault(1) ?? 0f;
      set
      {
        if (CurrentConfig?.Suspension?.Stats != null && CurrentConfig.Suspension.Stats.Count > 1)
        {
          CurrentConfig.Suspension.Stats[1] = value;
          OnPropertyChanged();
        }
      }
    }

    public float SuspensionStat3
    {
      get => CurrentConfig?.Suspension?.Stats?.ElementAtOrDefault(2) ?? 0f;
      set
      {
        if (CurrentConfig?.Suspension?.Stats != null && CurrentConfig.Suspension.Stats.Count > 2)
        {
          CurrentConfig.Suspension.Stats[2] = value;
          OnPropertyChanged();
        }
      }
    }

    public float SuspensionStat4
    {
      get => CurrentConfig?.Suspension?.Stats?.ElementAtOrDefault(3) ?? 0f;
      set
      {
        if (CurrentConfig?.Suspension?.Stats != null && CurrentConfig.Suspension.Stats.Count > 3)
        {
          CurrentConfig.Suspension.Stats[3] = value;
          OnPropertyChanged();
        }
      }
    }

    public float SuspensionStat5
    {
      get => CurrentConfig?.Suspension?.Stats?.ElementAtOrDefault(4) ?? 0f;
      set
      {
        if (CurrentConfig?.Suspension?.Stats != null && CurrentConfig.Suspension.Stats.Count > 4)
        {
          CurrentConfig.Suspension.Stats[4] = value;
          OnPropertyChanged();
        }
      }
    }

    private void UpdateCurrentConfig()
    {
      OnPropertyChanged(nameof(SelectedBody));
      OnPropertyChanged(nameof(SelectedEngine));
      OnPropertyChanged(nameof(SelectedSuspension));
      OnPropertyChanged(nameof(BodyColor));
      OnPropertyChanged(nameof(LicensePlate));
      OnPropertyChanged(nameof(ModulesList));
      OnPropertyChanged(nameof(HasBodyStats));
      OnPropertyChanged(nameof(HasSuspensionStats));
      OnPropertyChanged(nameof(BodyStat1));
      OnPropertyChanged(nameof(BodyStat2));
      OnPropertyChanged(nameof(BodyStat3));
      OnPropertyChanged(nameof(SuspensionStat1));
      OnPropertyChanged(nameof(SuspensionStat2));
      OnPropertyChanged(nameof(SuspensionStat3));
      OnPropertyChanged(nameof(SuspensionStat4));
      OnPropertyChanged(nameof(SuspensionStat5));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class RelayCommand : ICommand
  {
    private readonly System.Action _execute;
    private readonly System.Func<bool> _canExecute;

    public RelayCommand(System.Action execute, System.Func<bool> canExecute = null)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
    public void Execute(object parameter) => _execute?.Invoke();

    public event System.EventHandler CanExecuteChanged
    {
      add => CommandManager.RequerySuggested += value;
      remove => CommandManager.RequerySuggested -= value;
    }
  }
}