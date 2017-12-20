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

        public void SetDates(SelectedDatesCollection selectedDates)
        {
            StartDay = selectedDates.FirstOrDefault();
            EndDay = selectedDates.LastOrDefault();
        }
    }
}