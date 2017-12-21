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
        public HotelManager HotelManager { get; set; }
        public ICommand ShowAddGuestWindowCommand { get; }
        public ICommand ShowGuestsWindowCommand { get; }
        public ICommand ShowAddRoomWindowCommand { get; }
        public ICommand ShowAddBookingWindowCommand { get; }
        public ICommand ShowAddModifyRoomWindow { get; }
        public MainWindowViewModel()
        {
            HotelManager = new HotelManager();
            ShowAddGuestWindowCommand = new ShowGuestDetailWindowCommand(new AddGuestCommand(HotelManager.Guests));
            ShowGuestsWindowCommand = new ShowGuestsWindowCommand(HotelManager);
            ShowAddRoomWindowCommand = new OpenAddRoomWindowCommand(HotelManager);
            ShowAddBookingWindowCommand = new OpenAddBookingWindowCommand(HotelManager);
            ShowAddModifyRoomWindow = new OpenModifyRoomWindowCommand(HotelManager);
        }
    }
}
