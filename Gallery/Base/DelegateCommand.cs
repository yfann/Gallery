using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Gallery.Base
{
    public class DelegateCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Func<T, bool> canExecuteMethod, Action<T> executeMethod)
        {
            CanExecuteMethod = canExecuteMethod;
            ExecuteMethod = executeMethod;
        }

        public DelegateCommand(Action<T> executeMethod)
        {
            CanExecuteMethod = T => true;
            ExecuteMethod = executeMethod;
        }

        private Func<T, bool> CanExecuteMethod { get; set; }

        private Action<T> ExecuteMethod { get; set; }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute((T)parameter))
            {
                ExecuteMethod((T)parameter);
            }
        }

        public void NotifyCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}