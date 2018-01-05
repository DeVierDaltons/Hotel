using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Collections.ObjectModel;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; private set; }
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

        public DateTime StartDay
        {
            get { return Booking.BookingPeriod.StartDate; }
            set { Booking.BookingPeriod.StartDate = value; OnPropertyChanged(); }
        }

        public DateTime EndDay
        {
            get { return Booking.BookingPeriod.EndDate; }
            set { Booking.BookingPeriod.EndDate = value; OnPropertyChanged(); }
        }

        public SelectedDatesCollection SelectedDates { get; set; }
        #endregion

        public AddBookingViewModel(HotelManager hotelManager)
        {
            HotelManager = hotelManager;
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

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            HotelManager.AddBooking(Booking);
            Booking = new Booking();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            Guest = null;
            Booking.BookingPeriod = new BookingPeriod();
            Room = null;
            StartDay = DateTime.Today;
            EndDay = DateTime.Today;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}