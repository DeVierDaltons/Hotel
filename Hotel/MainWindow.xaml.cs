using Hotel.Command;
using Hotel.Model;
using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System;
using Hotel.View;
using Unity.Attributes;
using Hotel.Repository;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency("GuestsViewModel")]
        public IViewModel guestsViewModel {
            get;
            set; }

        [Dependency("RoomViewModel")]
        public IViewModel RoomViewModel { get; set; }

        [Dependency("BookingViewModel")]
        public IViewModel BookingViewModel { get; set; }

        [Dependency("AddBookingViewModel")]
        public IViewModel AddBookingViewModel { get; set;}

        [Dependency("AddRoomView")]
        public AddRoomView AddRoomView { get; set; }

        [Dependency("AddRoomViewModel")]
        public IViewModel AddRoomViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            SetupGuestTab();
            SetupRoomTab();
            SetupBookingTab();
        }

        private void SetupGuestTab()
        {
            (guestsViewModel as GuestsViewModel).SwitchToBookingTab = SwitchToBookingTab;
            GuestExplorerTab.DataContext = guestsViewModel;
        }

        public void SetupRoomTab()
        {
            //creating the rooms tab
            AddRoomView addRoomView = new AddRoomView();
            addRoomView.DataContext = AddRoomViewModel;
            (RoomViewModel as RoomViewModel).AddRoomView = AddRoomView as AddRoomView;
            RoomExplorerTab.DataContext = RoomViewModel;
        }

        public void SetupBookingTab( )
        {
            //Creating the tab for bookings with addbooking and modify booking views.
            AddBookingView addBookingView = new AddBookingView();
            addBookingView.DataContext = AddBookingViewModel;
            BookingViewModel modifyBookingViewModel = BookingViewModel as BookingViewModel;
            modifyBookingViewModel.AddBookingView = addBookingView;
            BookingExplorerTab.DataContext = modifyBookingViewModel;
        }

        public void SwitchToBookingTab()
        {
            BookingExplorerTab.IsSelected = true;
            (BookingExplorerTab.DataContext as BookingViewModel).FilterDisplayedBookingsByGuest((GuestExplorerTab.DataContext as GuestsViewModel).SelectedGuest);
        }
    }
}
