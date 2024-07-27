using System;
using System.Windows.Input;

namespace FumesEditor.Commands
{
  public class RelayCommand : ICommand
  {
    private readonly Action _execute;
    private readonly Action<object> _executeWithParameter;
    private readonly Func<bool> _canExecute;
    private readonly Func<object, bool> _canExecuteWithParameter;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
      _execute = execute ?? throw new ArgumentNullException(nameof(execute));
      _canExecute = canExecute;
    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
      _executeWithParameter = execute ?? throw new ArgumentNullException(nameof(execute));
      _canExecuteWithParameter = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return _canExecute?.Invoke() ?? _canExecuteWithParameter?.Invoke(parameter) ?? true;
    }

    public void Execute(object parameter)
    {
      if (_execute != null)
      {
        _execute();
      }
      else
      {
        _executeWithParameter?.Invoke(parameter);
      }
    }

    public event EventHandler CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }
  }
}