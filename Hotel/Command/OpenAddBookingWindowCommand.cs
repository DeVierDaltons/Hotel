using System;
using System.Windows.Input;
using Hotel.Model;
using Hotel.View;

namespace Hotel.ViewModel
{
    internal class OpenAddBookingWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private HotelManager hotelManager;

        public OpenAddBookingWindowCommand(HotelManager hotelManager)
        {
            this.hotelManager = hotelManager;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddBookingView view = new AddBookingView();
            ((AddBookingViewModel)view.DataContext).HotelManager = hotelManager;
            view.Show();
        }
    }
}