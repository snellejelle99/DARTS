using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DARTS.ViewModel.Command
{
    class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this?.canExecute(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
