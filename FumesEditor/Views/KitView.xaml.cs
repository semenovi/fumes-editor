using System.Windows.Controls;
using FumesEditor.ViewModels;

namespace FumesEditor.Views
{
  public partial class KitView : UserControl
  {
    public KitView()
    {
      InitializeComponent();
      DataContext = new KitViewModel();
    }
  }
}