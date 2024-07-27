using System.Windows.Controls;
using FumesEditor.ViewModels;

namespace FumesEditor.Views
{
  public partial class ItemsView : UserControl
  {
    public ItemsView()
    {
      InitializeComponent();
      DataContext = new ItemsViewModel();
    }
  }
}