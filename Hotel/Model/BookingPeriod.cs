using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class BookingPeriod
    {

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        /// <summary>
        /// Creates a new BookingPeriod based on a start and end date
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public BookingPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Creates a new BookingPeriod based on a start date and timespan
        /// </summary>
        /// <param name="startDays">Start date</param>
        /// <param name="days">Timespan</param>
        public BookingPeriod(DateTime startDate, int days)
        {
            StartDate = startDate;
            EndDate = startDate.Add(TimeSpan.FromDays((double)days));
        }

        /// <summary>
        /// Helper method to determine if the BookingPeriods do not overlap
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if the BookingPeriod is not overlapping. False otherwise.</returns>
        private bool NonOverlapcheck(BookingPeriod compareWith)
        {
            return ((compareWith.StartDate > EndDate) && (compareWith.EndDate > EndDate)
                || ((compareWith.StartDate < StartDate) && (compareWith.EndDate < StartDate))
                );
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does not overlap with the current one
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if it they do not overlap. False otherwise.</returns>
        public bool DoesNotoverlapWith(BookingPeriod compareWith)
        {
            return (NonOverlapcheck(compareWith));
        }


        /// <summary>
        /// Determines if the specified BookingPeriod does  overlap with the current one
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if it they do  overlap. False otherwise.</returns>
        public bool OverlapWith(BookingPeriod compareWith)
        {
            return !DoesNotoverlapWith(compareWith);
        }

    }

}
