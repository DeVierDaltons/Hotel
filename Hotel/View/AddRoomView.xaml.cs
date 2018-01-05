using Hotel.Model;
using Hotel.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    public partial class AddRoomView : UserControl
    {
        public AddRoomView()
        {
            InitializeComponent();
            RoomQualityDropdown.ItemsSource = Enum.GetValues(typeof(RoomQuality)).Cast<RoomQuality>();
        }
    }
}
