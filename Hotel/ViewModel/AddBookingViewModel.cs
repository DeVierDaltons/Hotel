using Hotel.Command;
using Hotel.Data;
using Hotel.Data.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Booking> AllBookings
        {
            get { return HotelManager.AllBookings; }
        }

        public ObservableCollection<Room> AllRooms
        {
            get { return HotelManager.AllRooms; }
        }

        public ObservableCollection<Guest> AllGuests
        {
            get { return HotelManager.AllGuests; }
        }

        public BookingPeriod SelectedDates { get; set; }
        #endregion

        public AddBookingViewModel()
        {
            AddBookingCommand = new AddBookingCommand(this);
        }

        private bool GuestsValid()
        {
            return Booking.GuestIds != null && Booking.GuestIds.Count > 0;
        }

        private bool RoomsValid()
        {
            return Booking.RoomIds != null && Booking.RoomIds.Count > 0;
        }

        private bool DatesValid()
        {
            return SelectedDates != null && SelectedDates.IsValid();
        }

        public bool ValidateInput()
        {
            if (!GuestsValid() || !RoomsValid() || !DatesValid())
            {
                return false;
            }
            return Booking.RoomIds.All(id => AllRooms.First(room => room.Id == id).TimePeriodAvailable(SelectedDates));
        }

        public void AddBooking()
        {
            Booking.SetDates(SelectedDates);
            AllBookings.Add(Booking);
            Booking = new Booking();
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        public void SetGuests(IEnumerable<Guest> selectedGuests)
        {
            Booking.GuestIds = selectedGuests.ConvertEnumerable(guest => guest.Id).ToList();
        }

        public void SetRooms(List<Room> selectedRooms)
        {
            Booking.RoomIds = selectedRooms.ConvertEnumerable(room => room.Id).ToList();
        }
    }
}