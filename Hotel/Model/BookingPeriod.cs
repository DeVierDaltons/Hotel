using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class BookingPeriod
    {

        private Date _startTime;

        public Date StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private Date _endTime;

        public Date EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// Creates a new BookingPeriod based on a start and end date
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="end">End date</param>
        public BookingPeriod(Date start, Date end)
        {
            StartTime = start;
            EndTime = end;
        }

        /// <summary>
        /// Creates a new BookingPeriod based on a start date and timespan
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="days">Timespan</param>
        public BookingPeriod(Date start, int days)
        {
            StartTime = start;
            EndTime = StartTime.Add(days);
        }

        /// <summary>
        /// Helper method to determine if a specified BookingPeriod is to the 'left' of the current BookingPeriod
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if the BookingPeriod is to the left of the current one. False otherwise.</returns>
        private bool BookingPeriodOfArgIsLeftOfCurrentBookingPeriod(BookingPeriod compareWith)
        {
            return ((compareWith.StartTime.TheDate < StartTime.TheDate) && (compareWith.EndTime.TheDate <= StartTime.TheDate));
        }

        /// <summary>
        /// Helper method to determine if a specified BookingPeriod is to the 'right' of the current BookingPeriod
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if the BookingPeriod is to the right of the current one. False otherwise.</returns>
        private bool BookingPeriodOfArgIsRightOfCurrentBookingPeriod(BookingPeriod compareWith)
        {
            return ((compareWith.StartTime.TheDate >= EndTime.TheDate) && (compareWith.EndTime.TheDate > EndTime.TheDate));
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does not overlap with the current one
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if it they do not overlap. False otherwise.</returns>
        public bool DoesNotoverlapWith(BookingPeriod compareWith)
        {
            return (BookingPeriodOfArgIsLeftOfCurrentBookingPeriod(compareWith) || BookingPeriodOfArgIsRightOfCurrentBookingPeriod(compareWith));
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
