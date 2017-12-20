using Hotel.Model;
using Hotel.View;
using Hotel.ViewModel;
using System;
using System.Windows.Input;

namespace Hotel.Command
{
    class ShowAddGuestWindowCommand : ICommand
    {
        private HotelManager _hotelManager;

        public ShowAddGuestWindowCommand(HotelManager vm)
        {
            _hotelManager = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddGuestView view = new AddGuestView();
            ((AddGuestViewModel)view.DataContext).HotelManager = _hotelManager;
            view.Show();
        }
    }
}
