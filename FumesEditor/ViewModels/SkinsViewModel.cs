using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FumesEditor.Models;

namespace FumesEditor.ViewModels
{
  public class SkinsViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;
    private CustomSkin _selectedSkin;
    private ObservableCollection<CustomSkin> _customSkins;

    public SaveModel SaveModel
    {
      get => _saveModel;
      set
      {
        _saveModel = value;
        UpdateCustomSkins();
        OnPropertyChanged();
      }
    }

    public ObservableCollection<CustomSkin> CustomSkins
    {
      get => _customSkins;
      set
      {
        _customSkins = value;
        OnPropertyChanged();
      }
    }

    public CustomSkin SelectedSkin
    {
      get => _selectedSkin;
      set
      {
        _selectedSkin = value;
        OnPropertyChanged();
      }
    }

    private void UpdateCustomSkins()
    {
      if (_saveModel != null && _saveModel.CustomSkins != null)
      {
        CustomSkins = new ObservableCollection<CustomSkin>(_saveModel.CustomSkins);
      }
      else
      {
        CustomSkins = new ObservableCollection<CustomSkin>();
      }
      SelectedSkin = CustomSkins.FirstOrDefault();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}