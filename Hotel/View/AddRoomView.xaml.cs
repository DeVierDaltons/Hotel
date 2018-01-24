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
                SetHiddenIfEmptyString(RoomNumberError, viewModel.RoomNrErrorMessage);
                SetHiddenIfEmptyString(RoomBedsError, viewModel.RoomBedsErrorMessage);
            };
        }

        private void SetHiddenIfEmptyString(UIElement element, string stringProperty)
        {
            if (stringProperty == "")
            {
                element.Visibility = Visibility.Hidden;
            }
            else
            {
                element.Visibility = Visibility.Visible;
            }
        }
    }
}
