using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;
using System.Windows.Media;

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

        public string Color1 { get { return Day1 ? "Green" : "Red";  } }
        public string Color2 { get { return Day2 ? "Green" : "Red";  } }

        private Room room;

        public RoomAvailability(Room room, DateTime startDate)
        {
            this.room = room;
            Day1 = room.DayAvailable(startDate);
            Day2 = room.DayAvailable(startDate.AddDays(1d));
        }
    }
}
