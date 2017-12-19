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
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();
        public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();
    }
}
