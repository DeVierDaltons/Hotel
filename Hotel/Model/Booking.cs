using System;
using System.Linq;
using System.Windows.Controls;

namespace Hotel.Model
{
    public class Booking
    {
        public Guest Guest;
        public Room Room;
        public DateTime StartDay;
        public DateTime EndDay;

        public string StartDayString
        {
            get { return StartDay.ToShortDateString(); }
        }

        public string EndDayString
        {
            get { return EndDay.ToShortDateString(); }
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
            StartDay = selectedDates.FirstOrDefault();
            EndDay = selectedDates.LastOrDefault();
        }
    }
}