using Hotel.Command;
using Hotel.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class AddBookingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; set; }
        public ICommand AddBookingCommand { get; set; }
        public Booking Booking { get; set; } = new Booking();
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
            HotelManager.Bookings.Add(Booking);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}