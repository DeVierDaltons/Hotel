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
            GuestExplorerTab.DataContext = new GuestsViewModel(HotelManager.Guests);
			
            //creating the rooms tab
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
    }
}
