using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class Booking
    {
        private Guest _guest;

        public Guest Guest
        {
            get { return _guest; }
            set { _guest = value; }
        }

        private Room _room;

        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        private BookingPeriod _bookingPeriod;

        public BookingPeriod BookingPeriod
        {
            get { return _bookingPeriod; }
            set { _bookingPeriod = value; }
        }


        enum Status
        {
            available,
            occupied
        };



    }

}
