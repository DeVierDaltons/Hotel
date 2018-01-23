using System.Windows;
using Unity;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            MainWindow window = container.Resolve<MainWindow>();
            //InitializeRoomBookings(container);
           
            window.Initialize();
            window.Show();
        }
        /*
        private void InitializeRoomBookings(IUnityContainer container)
        {
            var bookings = container.Resolve<RepositoryBackedObservableCollection<Booking>>();
            bookings.ForEach((booking) =>
            {
                booking.Rooms.ForEach(room => room.Bookings.Add(booking));
            });
            bookings.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (e.OldItems != null)
                {
                    Booking removedBooking = (Booking)e.OldItems[0];
                    removedBooking.Rooms.ForEach(room => room.Bookings.Remove(removedBooking));
                }
                if (e.NewItems != null)
                {
                    Booking addedBooking = (Booking)e.NewItems[0];
                    addedBooking.Rooms.ForEach(room => room.Bookings.Add(addedBooking));
                }
            };*/
        }
    }

