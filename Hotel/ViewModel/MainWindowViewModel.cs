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

            SetupHotelManager();
        }

        private void SetupHotelManager()
        {
            Room r = new Room()
            {
                RoomNumber = "0",
                Beds = 2,
                HasNiceView = true,
                PricePerDay = 100,
                Quality = RoomQuality.Budget
            };
            Room r2 = new Room()
            {
                RoomNumber = "1",
                Beds = 2,
                HasNiceView = true,
                PricePerDay = 100,
                Quality = RoomQuality.Budget
            };

            Room r3 = new Room()
            {
                RoomNumber = "2",
                Beds = 2,
                HasNiceView = false,
                PricePerDay = 50,
                Quality = RoomQuality.Comfort
            };

            Room r4 = new Room()
            {
                RoomNumber = "3",
                Beds = 4,
                HasNiceView = false,
                PricePerDay = 200,
                Quality = RoomQuality.Luxe
            };

            HotelManager.Rooms.Add(r);
            HotelManager.Rooms.Add(r2);
            HotelManager.Rooms.Add(r3);
            HotelManager.Rooms.Add(r4);
        }
    }
}
