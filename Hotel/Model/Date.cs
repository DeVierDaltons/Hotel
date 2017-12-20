using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class Date
    {
        private DateTime _dateTime;

        public DateTime TheDate
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        public Date(int day, int month, int year)
        {
            TheDate = new DateTime(year, month, day);
        }

        /// <summary>
        /// Returns a new Date object that is the sum of the current date and a certain time span
        /// </summary>
        /// <param name="days">Time span in days</param>
        /// <returns>New date</returns>
        public Date Add(int days)
        {
            DateTime temp = TheDate.Add(TimeSpan.FromDays((double)days));

            return new Date(temp.Day, temp.Month, temp.Year);
        }

    }
}
