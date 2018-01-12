using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for ModifyRoom.xaml
    /// </summary>
    public partial class RoomView : UserControl
    {
        public RoomView()
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
