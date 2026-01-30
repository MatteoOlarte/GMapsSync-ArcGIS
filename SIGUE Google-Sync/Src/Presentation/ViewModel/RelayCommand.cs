namespace GMapsSync.Src.Presentation.ViewModel;

using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    /// <summary>
    /// Creates a new relay command
    /// </summary>
    /// <param name="execute">The execution logic</param>
    public RelayCommand(Action execute) : this(execute, null)
    {
    }

    /// <summary>
    /// Creates a new relay command with can execute function
    /// </summary>
    /// <param name="execute">The execution logic</param>
    /// <param name="canExecute">The execution status logic</param>
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Event raised when can execute changed
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Determines if command can be executed
    /// </summary>
    /// <param name="parameter">Data used by the command</param>
    /// <returns>True if command can be executed</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute();
    }

    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="parameter">Data used by the command</param>
    public void Execute(object parameter)
    {
        _execute();
    }
}
