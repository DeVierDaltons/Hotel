using Hotel.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for ModifyBooking.xaml
    /// </summary>
    public partial class BookingView : UserControl
    {

        public BookingView()
        {
            InitializeComponent();
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as BookingViewModel).RemoveGuestFilter();
        }
        
        private void AddBookingView_Loaded(object sender, RoutedEventArgs e)
        {
            BookingViewModel bookingViewModel = (DataContext as BookingViewModel);
            if (bookingViewModel.AddBookingView == null) { 
                bookingViewModel.AddBookingView = sender as AddBookingView;
                (DataContext as BookingViewModel).SetupAddBookingView(sender as AddBookingView);
            }
        }
    }
}
