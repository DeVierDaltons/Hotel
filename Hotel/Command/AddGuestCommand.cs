using Hotel.ViewModel;
using System;
using System.Windows.Input;

namespace Hotel.Command
{
    public class AddGuestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private AddGuestViewModel viewModel;

        public AddGuestCommand(AddGuestViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.ValidateInput();
        }

        public void Execute(object parameter)
        {
            viewModel.AddGuest();
        }
    }
}
