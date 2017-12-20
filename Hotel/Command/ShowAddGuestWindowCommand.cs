using Hotel.Model;
using Hotel.View;
using Hotel.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hotel.Command
{
    class ShowAddGuestWindowCommand : ICommand
    {
        private ICommand SubmitCommand;

        public ShowAddGuestWindowCommand(ICommand submitCommand)
        {
            SubmitCommand = submitCommand;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddGuestView view = new AddGuestView
            {
                DataContext = new AddGuestViewModel(SubmitCommand)
            };
            view.Show();
        }
    }
}
