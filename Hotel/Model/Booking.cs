using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public bool OverlapsWith(Booking booking)
        {
            return BookingPeriod.OverlapWith(booking.BookingPeriod);
        }

        public bool DoesNotOverlapWith(Booking booking)
        {
            return BookingPeriod.DoesNotoverlapWith(booking.BookingPeriod);
        }

        public string StartDayString
        {
            get { return BookingPeriod.StartDate.ToShortDateString(); }
        }

        public string EndDayString
        {
            get { return BookingPeriod.EndDate.ToShortDateString(); }
        }

        public string GuestName
        {
            get { return Guest.FirstName ?? "null"; }
        }

        public string RoomNumber
        {
            get { return Room.RoomNumber ?? "null"; }
        }

        public void SetDates(SelectedDatesCollection selectedDates)
        {
            BookingPeriod = new BookingPeriod(selectedDates.FirstOrDefault(), selectedDates.LastOrDefault());
        }
    }
}
