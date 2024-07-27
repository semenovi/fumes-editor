using System.Windows.Controls;
using FumesEditor.ViewModels;

namespace FumesEditor.Views
{
  public partial class SkinsView : UserControl
  {
    public SkinsView()
    {
      InitializeComponent();
      DataContext = new SkinsViewModel();
    }
  }
}