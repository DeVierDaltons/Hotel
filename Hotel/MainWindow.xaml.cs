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

        public MainWindow([Dependency("GuestRepository")]IRepositoryBackedObservableCollection guestsRepositoryObservableCollection, [Dependency("RoomRepository")]IRepositoryBackedObservableCollection roomRepositoryObservableCollection, [Dependency("BookingRepository")]IRepositoryBackedObservableCollection bookingRepositoryObservableCollection)
        {
            InitializeComponent();
            SetupGuestTab(guestsRepositoryObservableCollection);
            SetupRoomTab(roomRepositoryObservableCollection);
            SetupBookingTab(bookingRepositoryObservableCollection);
        }

        private void SetupGuestTab(IRepositoryBackedObservableCollection guestsRepositoryObservableCollection)
        {
            GuestsViewModel guestsViewModel = new GuestsViewModel(guestsRepositoryObservableCollection as RepositoryBackedObservableCollection<Guest>);
            guestsViewModel.SwitchToBookingTab = SwitchToBookingTab;
            GuestExplorerTab.DataContext = guestsViewModel;
        }

        public void SetupRoomTab(IRepositoryBackedObservableCollection roomRepositoryObservableCollection)
        {
            //creating the rooms tab
            AddRoomView addRoomView = new AddRoomView();
            addRoomView.DataContext = new AddRoomViewModel(roomRepositoryObservableCollection as RepositoryBackedObservableCollection<Room>);
            RoomViewModel roomViewModel = new RoomViewModel(roomRepositoryObservableCollection as RepositoryBackedObservableCollection<Room>);
            roomViewModel.AddRoomView = addRoomView;
            RoomExplorerTab.DataContext = roomViewModel;
        }

        public void SetupBookingTab(IRepositoryBackedObservableCollection bookingRepositoryObservableCollection )
        {
            //Creating the tab for bookings with addbooking and modify booking views.
            AddBookingView addBookingView = new AddBookingView();
            addBookingView.DataContext = new AddBookingViewModel();
            BookingViewModel modifyBookingViewModel = new BookingViewModel(bookingRepositoryObservableCollection as RepositoryBackedObservableCollection<Booking>);
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
