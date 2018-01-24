using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.Serialization;

namespace Hotel.Data
{
    [DataContract]
    public class Booking : INotifyPropertyChanged, IIdentifiable
    {
        [DataMember]
        public virtual Guid Id { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        private ICollection<Guest> _guests;

        [DataMember]
        public virtual ICollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        private ICollection<Room> _rooms = new List<Room>();
        [DataMember]
        public virtual ICollection<Room> Rooms
        {
            get { return _rooms; }
            set { _rooms = value; OnPropertyChanged(); }
        }

        private BookingPeriod _bookingPeriod;

        [DataMember]
        public virtual BookingPeriod BookingPeriod
        {
            get { return _bookingPeriod; }
            set { _bookingPeriod = value; OnPropertyChanged(); }
        }

        private BookingStatus _Status = BookingStatus.Reserved;
        [DataMember]
        public virtual BookingStatus BookingStatus
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged(); }
        }

        public virtual bool OverlapsWith(Booking booking)
        {
            return BookingPeriod.OverlapsWith(booking.BookingPeriod);
        }

        public virtual bool DoesNotOverlapWith(Booking booking)
        {
            return BookingPeriod.DoesNotoverlapWith(booking.BookingPeriod);
        }
        
        public virtual string GuestName
        {
            get { return String.Join(", ", Guests.ToList().ConvertAll(b => b.FirstName)); }
        }

        public virtual string RoomsDescription
        {
            get { return String.Join(", ", Rooms.ToList().ConvertAll(room => room.RoomNumber)); }
        }

        public virtual bool BlocksOtherBookings
        {
            get
            {
                return BookingStatus != BookingStatus.Cancelled && BookingStatus != BookingStatus.NoShow && BookingStatus != BookingStatus.CheckedOut;
            }
        }

        public virtual void SetDates(BookingPeriod selectedDates)
        {
            BookingPeriod = new BookingPeriod(selectedDates.StartDate, selectedDates.EndDate);
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
