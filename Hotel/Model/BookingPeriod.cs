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
        /// <param name="compareWithStartDate"></param>
        /// <param name="compareWithEndDate"></param>
        /// <returns></returns>
        private bool BookingPeriodOfArgIsLeftOfCurrentBookingPeriod(Date compareWithStartDate, Date compareWithEndDate)
        {
            return ((compareWithStartDate.TheDate < StartTime.TheDate) && (compareWithEndDate.TheDate <= StartTime.TheDate));
        }

        /// <summary>
        /// Helper method to determine if a specified BookingPeriod is to the 'right' of the current BookingPeriod
        /// </summary>
        /// <param name="compareWithStartDate"></param>
        /// <param name="compareWithEndDate"></param>
        /// <returns></returns>
        private bool BookingPeriodOfArgIsRightOfCurrentBookingPeriod(Date compareWithStartDate, Date compareWithEndDate)
        {
            return ((compareWithStartDate.TheDate >= EndTime.TheDate) && (compareWithEndDate.TheDate > EndTime.TheDate));
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does not overlap with the current one
        /// </summary>
        /// <param name="compareWithStartDate">Start Date of BookingPeriod to compare with</param>
        /// <param name="compareWithEndDate">End Date of BookingPeriod to compare with</param>
        /// <returns>True if it they do not overlap. False otherwise.</returns>
        public bool DoesNotoverlapWith(Date compareWithStartDate, Date compareWithEndDate)
        {
            return (BookingPeriodOfArgIsLeftOfCurrentBookingPeriod(compareWithStartDate, compareWithEndDate) || BookingPeriodOfArgIsRightOfCurrentBookingPeriod(compareWithStartDate, compareWithEndDate));
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does  overlap with the current one
        /// </summary>
        /// <param name="compareWithStartDate">Start Date of BookingPeriod to compare with</param>
        /// <param name="compareWithEndDate">End Date of BookingPeriod to compare with</param>
        /// <returns>True if it they do  overlap. False otherwise.</returns>
        public bool OverlapWith(Date compareWithStartDate, Date compareWithEndDate)
        {
            return !DoesNotoverlapWith(compareWithStartDate, compareWithEndDate);
        }

    }

}
