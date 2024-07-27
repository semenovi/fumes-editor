using System.Windows.Controls;
using FumesEditor.ViewModels;

namespace FumesEditor.Views
{
  public partial class GeneralView : UserControl
  {
    public GeneralView()
    {
      InitializeComponent();
      DataContext = new GeneralViewModel();
    }
  }
}