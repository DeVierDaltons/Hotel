using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Collections.ObjectModel;
using Hotel.Repository;
using System.Diagnostics;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; private set; }
        public RepositoryBackedObservableCollection<Booking> Bookings { get; set; }
        public ICommand AddBookingCommand { get; private set; }
        public Booking Booking { get; set; } = new Booking();

        public ObservableCollection<Guest> Guests
        {
            get { return HotelManager.Guests; }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return HotelManager.Rooms; }
        }

        public Guest Guest
        {
            get { return Booking.Guest; }
            set { Booking.Guest = value; OnPropertyChanged(); }
        }

        public Room Room
        {
            get { return Booking.Room; }
            set { Booking.Room = value; OnPropertyChanged(); }
        }

        public SelectedDatesCollection SelectedDates { get; set; }
        #endregion

        public AddBookingViewModel([Unity.Attributes.Dependency("BookingRepository")]IRepositoryBackedObservableCollection bookingRepositoryObservableCollection)
        {
            Bookings = bookingRepositoryObservableCollection as RepositoryBackedObservableCollection<Booking>;
            AddBookingCommand = new AddBookingCommand(this);
        }

        public bool ValidateInput()
        {
            if( Booking.Guest == null || Booking.Room == null || SelectedDates == null)
            {
                return false;
            }
            return Booking.Room.TimePeriodAvailable(new BookingPeriod(SelectedDates));
        }

        //TODO use this function....
        private void AddAllBookingsToRoom()
        {
            foreach (Booking booking in Bookings)
            {
                Debug.Assert(Rooms.Contains(booking.Room));
                booking.Room.Bookings.Add(booking);
            }
        }

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            Bookings.Add(Booking);
            Debug.Assert(Rooms.Contains(Booking.Room));
            Booking.Room.Bookings.Add(Booking);
            Booking = new Booking();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            Guest = null;
            Room = null;
            Booking.BookingPeriod = new BookingPeriod() {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            SelectedDates.Clear();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}