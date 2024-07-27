using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FumesEditor.Models;
using FumesEditor.Commands;
using System.Linq;

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

    public ICommand SaveChangesCommand => new RelayCommand(SaveChanges);

    private void SaveChanges()
    {
      if (_saveModel != null && CustomSkins != null)
      {
        _saveModel.CustomSkins = CustomSkins.ToList();
        // Here you might want to trigger saving to file or updating the main SaveModel
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