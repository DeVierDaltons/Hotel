using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;
using System.Windows.Input;
using Hotel.View;
using Hotel.ViewModel;

namespace Hotel.Command
{
    public class OpenModifyBookingWindowCommand : ICommand
    {
        private HotelManager hotelManager;

        public OpenModifyBookingWindowCommand(HotelManager hotelManager)
        {
            this.hotelManager = hotelManager;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ModifyBookingView view = new ModifyBookingView();
            ModifyBookingViewModel viewmodel = new ModifyBookingViewModel(hotelManager.GetAllBookings());
            view.DataContext = viewmodel;
            view.Show();
        }
    }
}
