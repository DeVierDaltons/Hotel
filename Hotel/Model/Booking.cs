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

        enum status
        {
            available,
            occupied
        };



    }

    public class BookingPeriod
    {
        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public BookingPeriod(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            StartTime = new DateTime(startYear, startMonth, startDay);
            EndTime = new DateTime(endYear, endMonth, endDay);
        }

        public BookingPeriod(int startDay, int startMonth, int startYear, int days)
        {
            StartTime = new DateTime(startYear, startMonth, startDay);
            EndTime =  StartTime.Add((double)days);
        }

        public bool overlaps(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            DateTime start = new DateTime(startYear, startMonth, startDay);
            DateTime end = new DateTime(endYear, endMonth, endDay);



        }


    }
}
