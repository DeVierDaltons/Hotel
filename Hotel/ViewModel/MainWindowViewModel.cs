using Hotel.Command;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class MainWindowViewModel
    {
        private HotelManager HotelManager = new HotelManager();
        public ICommand ShowAddGuestWindowCommand { get; }
        public ICommand ShowAddRoomWindowCommand { get; }
        public ICommand ShowAddBookingWindowCommand { get; }
        public ICommand ShowAddModifyRoomWindow { get; }
        public MainWindowViewModel()
        {
            ShowAddGuestWindowCommand = new OpenAddGuestWindowCommand(HotelManager);
            ShowAddRoomWindowCommand = new OpenAddRoomWindowCommand(HotelManager);
            ShowAddBookingWindowCommand = new OpenAddBookingWindowCommand(HotelManager);
            ShowAddModifyRoomWindow = new OpenRoomWindowCommand(HotelManager);
        }
    }
}
