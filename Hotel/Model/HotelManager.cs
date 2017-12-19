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
        public ObservableCollection<Guest> Guests { get; set; } = new ObservableCollection<Guest>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();
    }
}
