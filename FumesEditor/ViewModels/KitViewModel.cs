using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Media;
using FumesEditor.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FumesEditor.Commands;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;

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

    // New properties for each category
    private Configuration.BodyConfig _selectedBody;
    private Configuration.EngineConfig _selectedEngine;
    private Configuration.SuspensionConfig _selectedSuspension;
    private Configuration.SkinConfig _selectedSkin;
    private Configuration.WeaponConfig _selectedWeapon;

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
          UpdateSelectedItems();
        }
      }
    }

    public Configuration.BodyConfig SelectedBody
    {
      get => _selectedBody;
      set
      {
        _selectedBody = value;
        OnPropertyChanged();
        UpdateKitBody();
      }
    }

    public Configuration.EngineConfig SelectedEngine
    {
      get => _selectedEngine;
      set
      {
        _selectedEngine = value;
        OnPropertyChanged();
        UpdateKitEngine();
      }
    }

    public Configuration.SuspensionConfig SelectedSuspension
    {
      get => _selectedSuspension;
      set
      {
        _selectedSuspension = value;
        OnPropertyChanged();
        UpdateKitSuspension();
      }
    }

    public Configuration.SkinConfig SelectedSkin
    {
      get => _selectedSkin;
      set
      {
        _selectedSkin = value;
        OnPropertyChanged();
        UpdateKitSkin();
      }
    }

    public Configuration.WeaponConfig SelectedWeapon
    {
      get => _selectedWeapon;
      set
      {
        _selectedWeapon = value;
        OnPropertyChanged();
        UpdateKitWeapon();
      }
    }

    public List<Configuration.BodyConfig> Bodies => Configuration?.Bodies ?? new List<Configuration.BodyConfig>();
    public List<Configuration.EngineConfig> Engines => Configuration?.Engines ?? new List<Configuration.EngineConfig>();
    public List<Configuration.SuspensionConfig> Suspensions => Configuration?.Suspensions ?? new List<Configuration.SuspensionConfig>();
    public List<Configuration.SkinConfig> Skins => Configuration?.Skins ?? new List<Configuration.SkinConfig>();
    public List<Configuration.WeaponConfig> Weapons => Configuration?.Weapons ?? new List<Configuration.WeaponConfig>();

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

    public List<Configuration.WeaponConfig> AvailableModules => Configuration?.Weapons ?? new List<Configuration.WeaponConfig>();

    public bool CanEditSelectedModule => SelectedModuleIndex >= 0 && SelectedModuleIndex < Modules.Count && SelectedWeapon != null;

    public ICommand EditModuleCommand => new RelayCommand(EditModule, () => CanEditSelectedModule);

    private void EditModule()
    {
      if (CanEditSelectedModule)
      {
        Modules[SelectedModuleIndex] = SelectedWeapon.Name;
        Kit.Modules = Modules.ToList();
        OnPropertyChanged(nameof(Modules));
      }
    }

    private void UpdateSelectedItems()
    {
      SelectedBody = Bodies.FirstOrDefault(b => b.Name == Kit.Body);
      SelectedEngine = Engines.FirstOrDefault(e => e.Name == Kit.Engine);
      SelectedSuspension = Suspensions.FirstOrDefault(s => s.Name == Kit.Suspension);
      SelectedSkin = Skins.FirstOrDefault(s => s.Name == Kit.Skin);
      // For weapons, we don't update SelectedWeapon here as it's handled differently
    }

    private void UpdateKitBody()
    {
      if (SelectedBody != null)
      {
        Kit.Body = SelectedBody.Name;
      }
    }

    private void UpdateKitEngine()
    {
      if (SelectedEngine != null)
      {
        Kit.Engine = SelectedEngine.Name;
      }
    }

    private void UpdateKitSuspension()
    {
      if (SelectedSuspension != null)
      {
        Kit.Suspension = SelectedSuspension.Name;
      }
    }

    private void UpdateKitSkin()
    {
      if (SelectedSkin != null)
      {
        Kit.Skin = SelectedSkin.Name;
      }
    }

    private void UpdateKitWeapon()
    {
      if (SelectedWeapon != null && SelectedModuleIndex >= 0 && SelectedModuleIndex < Modules.Count)
      {
        Modules[SelectedModuleIndex] = SelectedWeapon.Name;
        Kit.Modules = Modules.ToList();
      }
    }

    private BitmapImage LoadImage(string imagePath)
    {
      if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
      {
        return null;
      }

      try
      {
        BitmapImage image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
        image.EndInit();
        return image;
      }
      catch
      {
        return null;
      }
    }

    public BitmapImage BodyImage => LoadImage(SelectedBody?.ImagePath);
    public BitmapImage EngineImage => LoadImage(SelectedEngine?.ImagePath);
    public BitmapImage SuspensionImage => LoadImage(SelectedSuspension?.ImagePath);
    public BitmapImage SkinImage => LoadImage(SelectedSkin?.ImagePath);
    public BitmapImage WeaponImage => LoadImage(SelectedWeapon?.ImagePath);

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

      // Update image properties when selections change
      if (propertyName == nameof(SelectedBody)) OnPropertyChanged(nameof(BodyImage));
      if (propertyName == nameof(SelectedEngine)) OnPropertyChanged(nameof(EngineImage));
      if (propertyName == nameof(SelectedSuspension)) OnPropertyChanged(nameof(SuspensionImage));
      if (propertyName == nameof(SelectedSkin)) OnPropertyChanged(nameof(SkinImage));
      if (propertyName == nameof(SelectedWeapon)) OnPropertyChanged(nameof(WeaponImage));
    }
  }
}