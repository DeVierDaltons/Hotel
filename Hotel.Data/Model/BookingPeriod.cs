using System;
using System.Linq;
using System.Windows.Controls;

namespace Hotel.Model
{
    public class BookingPeriod
    {
        public virtual Guid Id { get; set; }

        private DateTime _startDate;

        public virtual DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;

        public virtual DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        // a constructor so that NHibernate doesn't complain
        public BookingPeriod()
        {
        }

        /// <summary>
        /// Creates a new BookingPeriod based on a start and end date
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        public BookingPeriod(DateTime startDate, DateTime endDate)
        {
            if(endDate >= startDate)
            {
                StartDate = startDate;
                EndDate = endDate;
            } else
            {
                StartDate = endDate;
                EndDate = startDate;
            }
        }

        public BookingPeriod(SelectedDatesCollection selectedDates) : this(selectedDates.FirstOrDefault(), selectedDates.LastOrDefault())
        {
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does not overlap with the current one
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if it they do not overlap. False otherwise.</returns>
        public virtual bool DoesNotoverlapWith(BookingPeriod compareWith)
        {
            return (compareWith.StartDate > EndDate) || (compareWith.EndDate < StartDate);
        }

        /// <summary>
        /// Determines if the specified BookingPeriod does  overlap with the current one
        /// </summary>
        /// <param name="compareWith">BookingPeriod to compare with</param>
        /// <returns>True if it they do  overlap. False otherwise.</returns>
        public virtual bool OverlapsWith(BookingPeriod compareWith)
        {
            return !DoesNotoverlapWith(compareWith);
        }

        public bool IsValid()
        {
            return StartDate != null && EndDate != null && StartDate.Year > 1 && EndDate.Year > 1 && StartDate <= EndDate;
        }
    }

}
