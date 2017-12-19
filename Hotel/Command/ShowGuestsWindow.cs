using System.Windows.Input;
using Hotel.ViewModel;
using Hotel.View;
using System;
using Hotel.Model;

namespace Hotel.Command
{
    internal class ShowGuestsWindow : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private HotelManager _hotelManager;
        
        public ShowGuestsWindow(HotelManager hotelmanager)
        {
            this._hotelManager = hotelmanager;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            GuestsView view = new GuestsView(_hotelManager);
            view.Show();
        }
    }
}

