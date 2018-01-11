using System;
using System.Windows.Input;
using Hotel.Model;
using Hotel.View;
using Hotel.Command;
using Hotel.Extensions;
namespace Hotel.ViewModel
{
    public class EditGuestCommand : ICommand
    {
        private Guest selectedGuest;

        public EditGuestCommand(Guest selectedGuest)
        {
            this.selectedGuest = selectedGuest;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object newGuestData)
        {
            selectedGuest.CopyDelta(newGuestData as Guest);
        }
    }
}