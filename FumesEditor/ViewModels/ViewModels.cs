using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FumesEditor.ViewModels
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

  public class SkinsViewModel : BaseViewModel
  {
    // Add properties and methods for Skins tab
  }

  public class KitViewModel : BaseViewModel
  {
    // Add properties and methods for Kit tab
  }
}