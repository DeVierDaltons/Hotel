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
using Hotel.DataAccessObjects;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public GuestsViewModel guestsViewModel { get; set; }

        [Dependency]
        public RoomViewModel RoomViewModel { get; set; }

        [Dependency]
        public BookingViewModel BookingViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            SetupGuestTab();
            GuestExplorerTab.DataContext = guestsViewModel;
            SetupRoomTab();
            SetupBookingTab();
        }

        private void SetupGuestTab()
        {
            guestsViewModel.Initialize();
            guestsViewModel.SwitchToBookingTab = SwitchToBookingTab;
            GuestExplorerTab.DataContext = guestsViewModel;
        }

        public void SetupRoomTab()
        {
            RoomExplorerTab.DataContext = RoomViewModel;
            RoomViewModel.Initialize();
        }

        public void SetupBookingTab( )
        {
            BookingViewModel.Initialize();
            BookingExplorerTab.DataContext = BookingViewModel;
        }

        public void SwitchToBookingTab()
        {
            BookingExplorerTab.IsSelected = true;
            (BookingExplorerTab.DataContext as BookingViewModel).FilterDisplayedBookingsByGuest((GuestExplorerTab.DataContext as GuestsViewModel).SelectedGuest);
        }
    }
}
