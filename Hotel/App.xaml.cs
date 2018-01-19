using Hotel.DataAccessObjects;
using Hotel.Model;
using Hotel.Repository;
using Hotel.View;
using Hotel.ViewModel;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            MainWindow window = container.Resolve<MainWindow>();
            InitializeRoomBookings(container);
           
            window.Initialize();
            window.Show();
        }

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
            };
        }

        private void RegisterTypes(IUnityContainer container)
        {
           
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Guest>), new RepositoryBackedObservableCollection<Guest>(new NHibernateRepository<Guest>()));
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Booking>), new RepositoryBackedObservableCollection<Booking>(new NHibernateRepository<Booking>()));
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Room>), new RepositoryBackedObservableCollection<Room>(new NHibernateRepository<Room>()));

            container.RegisterType<IViewModel, AddBookingViewModel>("AddBookingViewModel");
            container.RegisterType<IViewModel, AddGuestViewModel>("AddGuestViewModel");

            container.RegisterType<IViewModel, RoomViewModel>("RoomViewModel");
            container.RegisterType<IViewModel, GuestsViewModel>("GuestsViewModel");
            container.RegisterType<IViewModel, BookingViewModel>("BookingViewModel");
            container.RegisterType<IRepository<Room>, NHibernateRepository<Room>>("NHibernateRepository");
            container.RegisterType<IRepository<Booking>, NHibernateRepository<Booking>>("NHibernateRepository");
            container.RegisterType<IRepository<Guest>, NHibernateRepository<Guest>>("NHibernateRepository");
        }
    }
}
