using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Forecast.it.Common
{
    public class RelayArgCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<bool> _canExecute;


        public event EventHandler CanExecuteChanged;
        
        public RelayArgCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayArgCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (!typeof(T).Equals(parameter.GetType())) return false;
            return _canExecute == null ? true : _canExecute();


        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
