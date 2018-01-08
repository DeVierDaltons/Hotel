using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;

namespace Hotel.ViewModel
{
    public class RoomAvailability
    {
        private readonly double BookingViewDaysRange = 2d;

        public string RoomName
        {
            get
            {
                return room.RoomNumber;
            }
        }

        public bool Day1 { get; private set; }
        public bool Day2 { get; private set; }

        private Room room;

        public RoomAvailability(Room room, DateTime startDate)
        {
            this.room = room;
            Day1 = room.DayAvailable(startDate);
            Day2 = room.DayAvailable(startDate.AddDays(1d));
        }
    }
}
