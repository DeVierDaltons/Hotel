using Hotel.ViewModel;
using System.Windows;

namespace Hotel.View
{
    public partial class AddBookingView : Window
    {
        public AddBookingView()
        {
            InitializeComponent();
            DataContext = new AddBookingViewModel();
        }

        private void SaveBooking(object sender, RoutedEventArgs e)
        {
        }
    }
}
