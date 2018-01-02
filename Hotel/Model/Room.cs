using System;
using System.Collections.Generic;

namespace Hotel.Model
{
    public class Room
    {
        public string RoomNumber { get; set; }
        public int Beds { get; set; }
        public RoomQuality Quality { get; set; }
        public bool HasNiceView { get; set; }
        public decimal PricePerDay { get; set; }

        public List<Booking> Bookings = new List<Booking>();

        public override string ToString()
        {
            return RoomNumber;
        }

        public bool TimePeriodAvailable(BookingPeriod period)
        {
            foreach(BookingPeriod bookingPeriod in Bookings.ConvertAll((Booking booking) => booking.BookingPeriod))
            {
                if(bookingPeriod.OverlapsWith(period))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
