using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Command
{
    public class ActionCommand : ICommand
    {
        private Action ActionToDo;
        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action actionToExecute)
        {
            ActionToDo = actionToExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ActionToDo?.Invoke();
        }
    }
}
