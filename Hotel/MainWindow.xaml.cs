using Hotel.Command;
using Hotel.Model;
using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HotelManager HotelManager;

        public MainWindow()
        {
            InitializeComponent();
            HotelManager = (DataContext as MainWindowViewModel).HotelManager;
            CreateAddGuestDataContext();
            GuestExplorerTab.DataContext = new GuestsViewModel(HotelManager.Guests, SwitchToEditGuest, SwitchToAddGuest);
            AddRoomTab.DataContext = new AddRoomViewModel(HotelManager);
            RoomExplorerTab.DataContext = new ModifyRoomViewModel(HotelManager.Rooms);
            AddBookingTab.DataContext = new AddBookingViewModel(HotelManager);
            BookingExplorerTab.DataContext = new ModifyBookingViewModel(HotelManager.Bookings);
        }

        public void SwitchToEditGuest(Guest guest)
        {
            AddGuestTab.DataContext = new GuestDetailViewModel(new EditGuestCommand(guest), guest, SwitchToGuestExplorer);
            AddGuestTab.IsSelected = true;
        }

        private void SwitchToAddGuest()
        {
            CreateAddGuestDataContext();
            AddGuestTab.IsSelected = true;
        }

        private void CreateAddGuestDataContext()
        {
            AddGuestTab.DataContext = new GuestDetailViewModel(new AddGuestCommand(HotelManager.Guests), null, SwitchToGuestExplorer);
        }

        private void SwitchToGuestExplorer()
        {
            CreateAddGuestDataContext();
            GuestExplorerTab.IsSelected = true;
        }
    }
}
