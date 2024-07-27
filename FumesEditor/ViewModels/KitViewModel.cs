using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Media;
using FumesEditor.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FumesEditor.Commands;
using System.Linq;

namespace FumesEditor.ViewModels
{
  public class KitViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;
    private Kit _kit;
    private Configuration _configuration;
    private ObservableCollection<string> _modules;
    private int _selectedModuleIndex;
    private string _selectedAvailableModule;

    public KitViewModel()
    {
      _kit = new Kit();
      Modules = new ObservableCollection<string>();
    }

    public SaveModel SaveModel
    {
      get => _saveModel;
      set
      {
        _saveModel = value;
        Kit = _saveModel?.Kit ?? new Kit();
        OnPropertyChanged();
      }
    }

    public Configuration Configuration
    {
      get => _configuration;
      set
      {
        _configuration = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Bodies));
        OnPropertyChanged(nameof(Engines));
        OnPropertyChanged(nameof(Suspensions));
        OnPropertyChanged(nameof(Skins));
        OnPropertyChanged(nameof(AvailableModules));
      }
    }

    public Kit Kit
    {
      get => _kit;
      set
      {
        if (_kit != value)
        {
          _kit = value;
          Modules = new ObservableCollection<string>(_kit.Modules ?? Enumerable.Repeat(string.Empty, 8).ToList());
          OnPropertyChanged();
          OnPropertyChanged(nameof(Body));
          OnPropertyChanged(nameof(Engine));
          OnPropertyChanged(nameof(Suspension));
          OnPropertyChanged(nameof(Skin));
          OnPropertyChanged(nameof(Color));
          OnPropertyChanged(nameof(LicensePlate));
          OnPropertyChanged(nameof(FireGroups));
          OnPropertyChanged(nameof(Red));
          OnPropertyChanged(nameof(Green));
          OnPropertyChanged(nameof(Blue));
        }
      }
    }

    public List<string> Bodies => Configuration?.Bodies ?? new List<string>();
    public List<string> Engines => Configuration?.Engines ?? new List<string>();
    public List<string> Suspensions => Configuration?.Suspensions ?? new List<string>();
    public List<string> Skins => Configuration?.Skins ?? new List<string>();
    public List<string> AvailableModules => Configuration?.Weapons ?? new List<string>();

    public string Body
    {
      get => Kit.Body;
      set
      {
        if (Kit.Body != value)
        {
          Kit.Body = value;
          OnPropertyChanged();
        }
      }
    }

    public string Engine
    {
      get => Kit.Engine;
      set
      {
        if (Kit.Engine != value)
        {
          Kit.Engine = value;
          OnPropertyChanged();
        }
      }
    }

    public string Suspension
    {
      get => Kit.Suspension;
      set
      {
        if (Kit.Suspension != value)
        {
          Kit.Suspension = value;
          OnPropertyChanged();
        }
      }
    }

    public string Skin
    {
      get => Kit.Skin;
      set
      {
        if (Kit.Skin != value)
        {
          Kit.Skin = value;
          OnPropertyChanged();
        }
      }
    }

    public System.Windows.Media.Color Color
    {
      get => System.Windows.Media.Color.FromArgb(
          (byte)(Kit.Color.A * 255),
          (byte)(Kit.Color.R * 255),
          (byte)(Kit.Color.G * 255),
          (byte)(Kit.Color.B * 255));
      set
      {
        Kit.Color = new Models.Color
        {
          A = value.A / 255f,
          R = value.R / 255f,
          G = value.G / 255f,
          B = value.B / 255f
        };
        OnPropertyChanged();
        OnPropertyChanged(nameof(Red));
        OnPropertyChanged(nameof(Green));
        OnPropertyChanged(nameof(Blue));
      }
    }

    public byte Red
    {
      get => (byte)(Kit.Color.R * 255);
      set
      {
        Kit.Color.R = value / 255f;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Color));
      }
    }

    public byte Green
    {
      get => (byte)(Kit.Color.G * 255);
      set
      {
        Kit.Color.G = value / 255f;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Color));
      }
    }

    public byte Blue
    {
      get => (byte)(Kit.Color.B * 255);
      set
      {
        Kit.Color.B = value / 255f;
        OnPropertyChanged();
        OnPropertyChanged(nameof(Color));
      }
    }

    public string LicensePlate
    {
      get => Kit.LicensePlate;
      set
      {
        if (Kit.LicensePlate != value)
        {
          Kit.LicensePlate = value;
          OnPropertyChanged();
        }
      }
    }

    public ObservableCollection<string> Modules
    {
      get => _modules;
      set
      {
        _modules = value;
        OnPropertyChanged();
      }
    }

    public int SelectedModuleIndex
    {
      get => _selectedModuleIndex;
      set
      {
        _selectedModuleIndex = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(CanEditSelectedModule));
      }
    }

    public string SelectedAvailableModule
    {
      get => _selectedAvailableModule;
      set
      {
        _selectedAvailableModule = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(CanEditSelectedModule));
      }
    }

    public bool CanEditSelectedModule => SelectedModuleIndex >= 0 && SelectedModuleIndex < Modules.Count && !string.IsNullOrEmpty(SelectedAvailableModule);

    public ICommand EditModuleCommand => new RelayCommand(EditModule, () => CanEditSelectedModule);

    private void EditModule()
    {
      if (CanEditSelectedModule)
      {
        Modules[SelectedModuleIndex] = SelectedAvailableModule;
        Kit.Modules = Modules.ToList();
        OnPropertyChanged(nameof(Modules));
      }
    }

    public List<int> FireGroups
    {
      get => Kit.FireGroups;
      set
      {
        if (Kit.FireGroups != value)
        {
          Kit.FireGroups = value;
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