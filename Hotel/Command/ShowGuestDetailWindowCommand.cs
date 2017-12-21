using Hotel.Model;
using Hotel.View;
using Hotel.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hotel.Command
{
    class ShowGuestDetailWindowCommand : ICommand
    {
        private ICommand SubmitCommand;

        public ShowGuestDetailWindowCommand(ICommand submitCommand)
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
            GuestDetailView view = new GuestDetailView
            {
                DataContext = new GuestDetailViewModel(SubmitCommand)
            };
            view.Show();
        }
    }
}
