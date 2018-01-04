using Hotel.Command;
using Hotel.Model;
using Hotel.ViewModel;
using System.Windows;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HotelManager hotelManager = (DataContext as MainWindowViewModel).HotelManager;
            AddGuest.DataContext = new GuestDetailViewModel(new AddGuestCommand(hotelManager.Guests), null);
            GuestExplorer.DataContext = new GuestsViewModel(hotelManager.Guests);
            AddRoom.DataContext = new AddRoomViewModel(hotelManager);
            RoomExplorer.DataContext = new ModifyRoomViewModel(hotelManager.Rooms);
            AddBooking.DataContext = new AddBookingViewModel(hotelManager);
            BookingExplorer.DataContext = new ModifyBookingViewModel(hotelManager.Bookings);
        }
    }
}
