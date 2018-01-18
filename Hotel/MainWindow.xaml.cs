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
        [Dependency("GuestsViewModel")]
        public IViewModel guestsViewModel { get; set; }

        [Dependency("RoomViewModel")]
        public IViewModel RoomViewModel { get; set; }

        [Dependency("BookingViewModel")]
        public IViewModel BookingViewModel { get; set; }
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
            (guestsViewModel as GuestsViewModel).Initialize();
            (guestsViewModel as GuestsViewModel).SwitchToBookingTab = SwitchToBookingTab;
            GuestExplorerTab.DataContext = guestsViewModel;
        }

        public void SetupRoomTab()
        {
            RoomExplorerTab.DataContext = RoomViewModel;
            (RoomViewModel as RoomViewModel).Initialize();
        }

        public void SetupBookingTab()
        {
            (BookingViewModel as BookingViewModel).Initialize();
            BookingExplorerTab.DataContext = BookingViewModel;
        }

        public void SwitchToBookingTab()
        {
            BookingExplorerTab.IsSelected = true;
            (BookingExplorerTab.DataContext as BookingViewModel).FilterDisplayedBookingsByGuest((GuestExplorerTab.DataContext as GuestsViewModel).SelectedGuest);
        }
    }
}
