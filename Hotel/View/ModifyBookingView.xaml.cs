using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for ModifyBooking.xaml
    /// </summary>
    public partial class ModifyBookingView : UserControl
    {
        public ModifyBookingView()
        {
            InitializeComponent();
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }
    }
}
