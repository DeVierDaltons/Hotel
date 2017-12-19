using Hotel.ViewModel;
using System;
using System.Windows.Input;

namespace Hotel.Command
{
    public class AddRoomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private AddRoomViewModel viewModel;

        public AddRoomCommand(AddRoomViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.ValidateInput();
        }

        public void Execute(object parameter)
        {
            viewModel.AddRoom();
        }
    }
}
