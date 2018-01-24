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

        [DataMember]
        private List<Guid> _guestIds = new List<Guid>();
        public virtual List<Guid> GuestIds
        {
            get { return _guestIds; }
            set { _guestIds = value; OnPropertyChanged(); }
        }

        [DataMember]
        private List<Guid> _roomIds = new List<Guid>();
        public virtual List<Guid> RoomIds
        {
            get { return _roomIds; }
            set { _roomIds = value; OnPropertyChanged(); }
        }

        [DataMember]
        private BookingPeriod _bookingPeriod;
        public virtual BookingPeriod BookingPeriod
        {
            get { return _bookingPeriod; }
            set { _bookingPeriod = value; OnPropertyChanged(); }
        }

        [DataMember]
        private BookingStatus _Status = BookingStatus.Reserved;
        public virtual BookingStatus BookingStatus
        {
            get { return _Status; }
            set { _Status = value; OnPropertyChanged(); }
        }

        private List<Guest> AllGuests;
        private List<Room> AllRooms;

        public virtual void SetGuestsAndRooms(List<Guest> guests, List<Room> rooms)
        {
            AllGuests = guests;
            AllRooms = rooms;
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
            get { return String.Join(", ", GuestIds.ConvertAll(id => AllGuests.First(guest => guest.Id == id).FirstName)); }
        }

        public virtual string RoomsDescription
        {
            get { return String.Join(", ", RoomIds.ToList().ConvertAll(id => AllRooms.First(room => room.Id == id).RoomNumber)); }
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
