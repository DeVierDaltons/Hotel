using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Hotel.Data.Extensions;
using System.Runtime.Serialization;

namespace Hotel.Data
{
    [DataContract]
    public class Room : INotifyPropertyChanged, IIdentifiable
    {
        [DataMember]
        public virtual Guid Id { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        private List<Booking> _bookings = new List<Booking>();

        [DataMember]
        public virtual List<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value ?? new List<Booking>(); }
        }

        private string _roomNumber;
        [DataMember]
        public virtual string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; OnPropertyChanged(); }
        }

        public virtual bool DayAvailable(DateTime startDate)
        {
            return TimePeriodAvailable(new BookingPeriod() { StartDate = startDate, EndDate = startDate });
        }

        private int _beds;
        [DataMember]
        public virtual int Beds
        {
            get { return _beds; }
            set { _beds = value; OnPropertyChanged(); }
        }

        private RoomQuality _quality;
        [DataMember]
        public virtual RoomQuality Quality
        {
            get { return _quality; }
            set { _quality = value; OnPropertyChanged(); }
        }

        private bool _hasNiceView;
        [DataMember]
        public virtual bool HasNiceView
        {
            get { return _hasNiceView; }
            set { _hasNiceView = value; OnPropertyChanged(); } 
        }

        private decimal _pricePerDay;
        [DataMember]
        public virtual decimal PricePerDay
        {
            get { return _pricePerDay; }
            set { _pricePerDay = value; OnPropertyChanged(); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        public virtual bool TimePeriodAvailable(BookingPeriod period)
        {
            var RelevantBookings = Bookings.Where(b => b.BlocksOtherBookings);
            foreach (BookingPeriod bookingPeriod in RelevantBookings.ConvertEnumerable(booking => booking.BookingPeriod))
            {
                if (bookingPeriod.OverlapsWith(period))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return RoomNumber;
        }
    }
}
