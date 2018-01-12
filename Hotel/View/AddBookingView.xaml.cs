using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel.View
{
    public partial class AddBookingView : UserControl
    {
        public AddBookingView()
        {
            InitializeComponent();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            AddBookingViewModel viewModel = (AddBookingViewModel)DataContext;
            viewModel.SelectedDates = BookingRangeCalendar.SelectedDates;
            Mouse.Capture(null);
        }
    }
}
