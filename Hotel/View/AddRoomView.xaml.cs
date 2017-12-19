using Hotel.Model;
using Hotel.ViewModel;
using System;
using System.Linq;
using System.Windows;

namespace Hotel.View
{
    public partial class AddRoomView : Window
    {
        public AddRoomView()
        {
            InitializeComponent();
            DataContext = new AddRoomViewModel();
            RoomQualityDropdown.ItemsSource = Enum.GetValues(typeof(RoomQuality)).Cast<RoomQuality>();
        }

        private void SaveRoom(object sender, RoutedEventArgs e)
        {
            (DataContext as AddRoomViewModel).SaveRoom();
        }
    }
}
