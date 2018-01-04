using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        public AddBookingView()
        {
            InitializeComponent();
        }

        public void SetDropdownContents() {
            AddBookingViewModel viewModel = DataContext as AddBookingViewModel;
            GuestDropdown.ItemsSource = viewModel.HotelManager.Guests;
            RoomDropdown.ItemsSource = viewModel.HotelManager.Rooms;
            viewModel.SelectedDates = BookingRangeCalendar.SelectedDates;
        }
    }
}
