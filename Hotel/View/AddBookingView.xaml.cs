using Hotel.ViewModel;
using System.Windows;

namespace Hotel.View
{
    public partial class AddBookingView : Window
    {
        public AddBookingView()
        {
            InitializeComponent();
        }

        public void SetDropdownContents() {
            AddBookingViewModel viewModel = DataContext as AddBookingViewModel;
            GuestDropdown.ItemsSource = viewModel.HotelManager.Guests;
            RoomDropdown.ItemsSource = viewModel.HotelManager.Rooms;
        }

        private void SaveBooking(object sender, RoutedEventArgs e)
        {

        }
    }
}
