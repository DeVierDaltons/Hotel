using Hotel.Model;
using Hotel.ViewModel;
using System;
using System.ComponentModel;
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

        public void Initialize(AddRoomViewModel viewModel)
        {
            viewModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (viewModel.RoomNrErrorMessage == "")
                {
                    RoomNumberError.Visibility = Visibility.Hidden;
                } else
                {
                    RoomNumberError.Visibility = Visibility.Visible;
                }
            };
        }
    }
}
