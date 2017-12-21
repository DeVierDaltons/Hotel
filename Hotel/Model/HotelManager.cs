using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class HotelManager
    {
        public ObservableCollection<Guest> Guests { get; } = new ObservableCollection<Guest>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();

        public HotelManager()
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

            Rooms.Add(r);
            Rooms.Add(r2);
            Rooms.Add(r3);
            Rooms.Add(r4);
        }
    }
}
