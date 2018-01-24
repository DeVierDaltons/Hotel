using Hotel.Data;
using Hotel.ViewModel;
using System.Windows;
using Unity.Attributes;

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
            RoomTabView.GetAddRoomPanel().Initialize(RoomViewModel.AddRoomViewDataContext);
        }

        public void SetupBookingTab()
        {
            BookingViewModel.Initialize();
            BookingExplorerTab.DataContext = BookingViewModel;
        }

        public void SwitchToBookingTab()
        {
            BookingExplorerTab.IsSelected = true;
            Guest selectedGuest = (GuestExplorerTab.DataContext as GuestsViewModel).SelectedGuest;
            (BookingExplorerTab.DataContext as BookingViewModel).FilterDisplayedBookingsByGuest(selectedGuest);
        }
    }
}
