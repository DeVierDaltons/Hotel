using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Controls;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; set; }
        public ICommand AddBookingCommand { get; set; }
        public Booking Booking { get; set; } = new Booking();

        public Guest Guest
        {
            get { return Booking.Guest; }
            set { Booking.Guest = value; OnPropertyChanged(); }
        }

        public Room Room
        {
            get { return Booking.Room; }
            set { Booking.Room = value; }
        }

        public DateTime StartDay
        {
            get { return Booking.BookingPeriod.StartDate; }
            set { Booking.BookingPeriod.StartDate = value; }
        }

        public DateTime EndDay
        {
            get { return Booking.BookingPeriod.EndDate; }
            set { Booking.BookingPeriod.EndDate = value; }
        }

        public SelectedDatesCollection SelectedDates { get; set; }
        #endregion

        public AddBookingViewModel()
        {
            AddBookingCommand = new AddBookingCommand(this);
        }

        public bool ValidateInput()
        {
            return true;
        }

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            HotelManager.Bookings.Add(Booking);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}