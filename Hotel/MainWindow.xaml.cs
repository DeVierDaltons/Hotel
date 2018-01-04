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
            AddGuestTab.DataContext = new GuestDetailViewModel(new AddGuestCommand(HotelManager.Guests), null, SwitchToGuestExplorer);
            GuestExplorerTab.DataContext = new GuestsViewModel(HotelManager.Guests, EditGuest, AddGuest);
            AddRoomTab.DataContext = new AddRoomViewModel(HotelManager);
            RoomExplorerTab.DataContext = new ModifyRoomViewModel(HotelManager.Rooms);
            AddBookingTab.DataContext = new AddBookingViewModel(HotelManager);
            BookingExplorerTab.DataContext = new ModifyBookingViewModel(HotelManager.Bookings);
        }

        public void EditGuest(Guest guest)
        {
            AddGuestTab.DataContext = new GuestDetailViewModel(new EditGuestCommand(guest), guest, SwitchToGuestExplorer);
            AddGuestTab.IsSelected = true;
        }

        private void AddGuest()
        {
            AddGuestTab.DataContext = new GuestDetailViewModel(new AddGuestCommand(HotelManager.Guests), null, SwitchToGuestExplorer);
            AddGuestTab.IsSelected = true;
        }

        private void SwitchToGuestExplorer()
        {
            GuestExplorerTab.IsSelected = true;
        }
    }
}
