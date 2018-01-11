using Hotel.Command;
using Hotel.Model;
using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System;
using Hotel.View;

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

            AddRoomView addRoomView = new AddRoomView();
            addRoomView.DataContext = new AddRoomViewModel(HotelManager);
            RoomViewModel roomViewModel = new RoomViewModel(HotelManager.Rooms);
            roomViewModel.AddRoomView = addRoomView;
            RoomExplorerTab.DataContext = roomViewModel;

            //Creating the tab for bookings with addbooking and modify booking views.
            AddBookingView addBookingView = new AddBookingView();
            addBookingView.DataContext = new AddBookingViewModel(HotelManager);
            BookingViewModel modifyBookingViewModel = new BookingViewModel(HotelManager.Bookings);
            modifyBookingViewModel.AddBookingView = addBookingView;
            BookingExplorerTab.DataContext = modifyBookingViewModel;
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
