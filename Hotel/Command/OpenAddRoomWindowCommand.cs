using System;
using System.Windows.Input;
using Hotel.Model;
using Hotel.View;

namespace Hotel.ViewModel
{
    internal class OpenAddRoomWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private HotelManager hotelManager;

        public OpenAddRoomWindowCommand(HotelManager hm)
        {
            hotelManager = hm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddRoomView view = new AddRoomView();
            ((AddRoomViewModel)view.DataContext).HotelManager = hotelManager;
            view.Show();
        }
    }
}