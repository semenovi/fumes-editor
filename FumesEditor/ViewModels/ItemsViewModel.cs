using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using FumesEditor.Models;
using FumesEditor.Commands;

namespace FumesEditor.ViewModels
{
  public class ItemsViewModel : INotifyPropertyChanged
  {
    private SaveModel _saveModel;
    private ObservableCollection<string> _allItems;
    private ObservableCollection<string> _unlockedItems;
    private string _selectedLockedItem;
    private string _selectedUnlockedItem;

    public ItemsViewModel()
    {
      AllItems = new ObservableCollection<string>();
      UnlockedItems = new ObservableCollection<string>();
    }

    public SaveModel SaveModel
    {
      get => _saveModel;
      set
      {
        _saveModel = value;
        if (_saveModel != null)
        {
          AllItems = new ObservableCollection<string>(_saveModel.Items?.OrderBy(i => i) ?? Enumerable.Empty<string>());
          UnlockedItems = new ObservableCollection<string>(_saveModel.UnlockedItems?.OrderBy(i => i) ?? Enumerable.Empty<string>());
        }
        OnPropertyChanged();
        OnPropertyChanged(nameof(LockedItems));
      }
    }

    public ObservableCollection<string> AllItems
    {
      get => _allItems;
      set
      {
        _allItems = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(LockedItems));
      }
    }

    public ObservableCollection<string> UnlockedItems
    {
      get => _unlockedItems;
      set
      {
        _unlockedItems = value;
        OnPropertyChanged();
        OnPropertyChanged(nameof(LockedItems));
      }
    }

    public IEnumerable<string> LockedItems => AllItems.Except(UnlockedItems).OrderBy(i => i);

    public string SelectedLockedItem
    {
      get => _selectedLockedItem;
      set
      {
        _selectedLockedItem = value;
        OnPropertyChanged();
      }
    }

    public string SelectedUnlockedItem
    {
      get => _selectedUnlockedItem;
      set
      {
        _selectedUnlockedItem = value;
        OnPropertyChanged();
      }
    }

    public ICommand MoveToUnlockedCommand => new RelayCommand(MoveToUnlocked, CanMoveToUnlocked);
    public ICommand MoveToLockedCommand => new RelayCommand(MoveToLocked, CanMoveToLocked);
    public ICommand UnlockAllCommand => new RelayCommand(UnlockAll);
    public ICommand LockAllCommand => new RelayCommand(LockAll);

    private bool CanMoveToUnlocked(object parameter) => !string.IsNullOrEmpty(SelectedLockedItem);
    private bool CanMoveToLocked(object parameter) => !string.IsNullOrEmpty(SelectedUnlockedItem);

    private void MoveToUnlocked()
    {
      if (SelectedLockedItem != null && !UnlockedItems.Contains(SelectedLockedItem))
      {
        UnlockedItems.Add(SelectedLockedItem);
        SortUnlockedItems();
        OnPropertyChanged(nameof(LockedItems));
        UpdateSaveModel();
      }
    }

    private void MoveToLocked()
    {
      if (SelectedUnlockedItem != null)
      {
        UnlockedItems.Remove(SelectedUnlockedItem);
        OnPropertyChanged(nameof(LockedItems));
        UpdateSaveModel();
      }
    }

    private bool CanMoveToUnlocked() => !string.IsNullOrEmpty(SelectedLockedItem);
    private bool CanMoveToLocked() => !string.IsNullOrEmpty(SelectedUnlockedItem);


    private void UnlockAll()
    {
      foreach (var item in AllItems)
      {
        if (!UnlockedItems.Contains(item))
        {
          UnlockedItems.Add(item);
        }
      }
      SortUnlockedItems();
      OnPropertyChanged(nameof(LockedItems));
      UpdateSaveModel();
    }

    private void LockAll()
    {
      UnlockedItems.Clear();
      OnPropertyChanged(nameof(LockedItems));
      UpdateSaveModel();
    }

    private void SortUnlockedItems()
    {
      var sorted = new ObservableCollection<string>(UnlockedItems.OrderBy(i => i));
      UnlockedItems.Clear();
      foreach (var item in sorted)
      {
        UnlockedItems.Add(item);
      }
    }

    private void UpdateSaveModel()
    {
      if (_saveModel != null)
      {
        _saveModel.Items = new System.Collections.Generic.List<string>(AllItems);
        _saveModel.UnlockedItems = new System.Collections.Generic.List<string>(UnlockedItems);
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}