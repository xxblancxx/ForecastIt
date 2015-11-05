using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Forecast.it.Infrastructure
{
    public class RoutedCommand : ICommand
    {
        private Action<object> _handlerExecution;
        private Func<object, bool> _canHandleExecution;
        public RoutedCommand(Action<object> h, Func<object, bool> c = null)
        {
            _handlerExecution = h;
            _canHandleExecution = c;
        }
        public bool CanExecute(object parameter)
        {
            return (_canHandleExecution == null) ||
             _canHandleExecution.Invoke(parameter);
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            _handlerExecution.Invoke(parameter);
        }
    }
}
