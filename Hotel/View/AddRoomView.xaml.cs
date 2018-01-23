using Hotel.Model;
using Hotel.UIValidators;
using Hotel.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            Binding binding = BindingOperations.GetBinding(RoomNr, TextBox.TextProperty);
            ((RoomValidator)binding.ValidationRules.First()).Rooms = viewModel.Rooms;
        }
    }
}
