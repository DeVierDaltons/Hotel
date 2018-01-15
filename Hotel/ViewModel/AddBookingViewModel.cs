using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; private set; }
        public ICommand AddBookingCommand { get; private set; }
        public Booking Booking { get; set; } = new Booking();

        public ObservableCollection<Guest> AllGuests
        {
            get { return HotelManager.Guests; }
        }

        public ObservableCollection<Room> AllRooms
        {
            get { return HotelManager.Rooms; }
        }

        public ObservableCollection<Booking> AllBookings
        {
            get { return HotelManager.Bookings; }
        }

        public Guest Guest
        {
            get { return Booking.Guest; }
            set { Booking.Guest = value; OnPropertyChanged(); }
        }

        public ICollection<Room> Rooms
        {
            get { return Booking.Rooms; }
            set { Booking.Rooms = value; OnPropertyChanged(); }
        }

        public BookingPeriod SelectedDates { get; set; }
        #endregion

        public AddBookingViewModel(HotelManager hotelManager)
        {
            HotelManager = hotelManager;
            AddBookingCommand = new AddBookingCommand(this);
        }

        public bool ValidateInput()
        {
            if( Booking.Guest == null || Booking.Rooms == null || Booking.Rooms.Count == 0 || SelectedDates == null || !SelectedDates.IsValid())
            {
                return false;
            }
            return Booking.Rooms.All(room => room.TimePeriodAvailable(SelectedDates));
        }

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            HotelManager.AddBooking(Booking);
            Booking = new Booking();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}