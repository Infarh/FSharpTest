using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfTest.Infrastructure.Commands.Base
{
    internal abstract class CommandAsync : ICommand
    {
        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => !_Executing && CanExecute(parameter);

        private bool _Executing;
        async void ICommand.Execute(object parameter)
        {
            if (!((ICommand)this).CanExecute(parameter)) return;
            _Executing = true;
            CommandManager.InvalidateRequerySuggested();
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                _Executing = false;
                CommandManager.InvalidateRequerySuggested();
            }

        }

        protected virtual bool CanExecute(object parameter) => !_Executing;

        protected abstract Task ExecuteAsync(object parameter);
    }
}