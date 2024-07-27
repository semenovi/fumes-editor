using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Media;
using FumesEditor.Models;

namespace FumesEditor.ViewModels
{
  public class KitViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;
    private Kit _kit;

    public KitViewModel()
    {
      _kit = new Kit();
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

    public Kit Kit
    {
      get => _kit;
      set
      {
        if (_kit != value)
        {
          _kit = value;
          OnPropertyChanged();
          // Обновляем все свойства, связанные с Kit
          OnPropertyChanged(nameof(Body));
          OnPropertyChanged(nameof(Engine));
          OnPropertyChanged(nameof(Suspension));
          OnPropertyChanged(nameof(Skin));
          OnPropertyChanged(nameof(Color));
          OnPropertyChanged(nameof(LicensePlate));
          OnPropertyChanged(nameof(Modules));
          OnPropertyChanged(nameof(FireGroups));
          OnPropertyChanged(nameof(Red));
          OnPropertyChanged(nameof(Green));
          OnPropertyChanged(nameof(Blue));
        }
      }
    }

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

    public List<string> Modules
    {
      get => Kit.Modules;
      set
      {
        if (Kit.Modules != value)
        {
          Kit.Modules = value;
          OnPropertyChanged();
        }
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