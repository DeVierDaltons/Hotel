using Hotel.Command;
using Hotel.Model;
using Hotel.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public ICommand AddBookingCommand { get; private set; }
        public Booking Booking { get; set; } = new Booking();

        private RepositoryBackedObservableCollection<Booking> _Bookings;

        public RepositoryBackedObservableCollection<Booking> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; OnPropertyChanged(); }
        }

       
        private RepositoryBackedObservableCollection<Room> _RoomsRepo;
        public RepositoryBackedObservableCollection<Room> RoomsRepo
        {
            get { return _RoomsRepo; }
            set { _RoomsRepo = value; OnPropertyChanged(); }
        }
        
        public RepositoryBackedObservableCollection<Guest> _AllGuests { get; set; }

        public RepositoryBackedObservableCollection<Guest> AllGuests
        {
            get { return _AllGuests; }
            set { _AllGuests = value; OnPropertyChanged(); }
        }

        public RepositoryBackedObservableCollection<Room> AllRooms
        {
            get { return RoomsRepo; }
        }

        public RepositoryBackedObservableCollection<Booking> AllBookings
        {
            get { return Bookings as RepositoryBackedObservableCollection<Booking>; }
        }

        public ICollection<Guest> Guests
        {
            get { return Booking.Guests; }
            set { Booking.Guests = value; OnPropertyChanged(); }
        }

        public ICollection<Room> Rooms
        {
            get { return Booking.Rooms; }
            set { Booking.Rooms = value; OnPropertyChanged(); }
        }

        public BookingPeriod SelectedDates { get; set; }
        #endregion

        public void Initialize()
        {
            AddBookingCommand = new AddBookingCommand(this);
        }

        private bool GuestsValid()
        {
            return Booking.Guests != null && Booking.Guests.Count > 0;
        }

        private bool RoomsValid()
        {
            return Booking.Rooms != null && Booking.Rooms.Count > 0;
        }

        private bool DatesValid()
        {
            return SelectedDates != null && SelectedDates.IsValid();
        }

        public bool ValidateInput()
        {
            if( !GuestsValid() || !RoomsValid() || !DatesValid() )
            {
                return false;
            }
            return Booking.Rooms.All(room => room.TimePeriodAvailable(SelectedDates));
        }

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            (Bookings as RepositoryBackedObservableCollection<Booking>).Add(Booking);
            Booking = new Booking();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}