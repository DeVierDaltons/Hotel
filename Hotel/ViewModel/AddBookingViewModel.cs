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

        private RepositoryBackedObservableCollection<Booking> _allBookings;
        public RepositoryBackedObservableCollection<Booking> AllBookings
        {
            get { return _allBookings; }
            set { _allBookings = value; OnPropertyChanged(); }
        }

        private RepositoryBackedObservableCollection<Room> _roomsRepo;
        public RepositoryBackedObservableCollection<Room> AllRooms
        {
            get { return _roomsRepo; }
            set { _roomsRepo = value; OnPropertyChanged(); }
        }
        
        public RepositoryBackedObservableCollection<Guest> _allGuests { get; set; }
        public RepositoryBackedObservableCollection<Guest> AllGuests
        {
            get { return _allGuests; }
            set { _allGuests = value; OnPropertyChanged(); }
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
            (AllBookings as RepositoryBackedObservableCollection<Booking>).Add(Booking);
            Booking = new Booking();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}