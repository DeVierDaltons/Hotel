using Hotel.DataAccessObjects;
using Hotel.Model;
using Hotel.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            MainWindow window = container.Resolve<MainWindow>();
            window.Show();
        }

        private void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IRepository<Room>, NHibernateRepository<Room>>("NHibernateRepository");
            container.RegisterType<IRepository<Booking>, NHibernateRepository<Booking>>("NHibernateRepository");
            container.RegisterType<IRepository<Guest>, NHibernateRepository<Guest>>("NHibernateRepository");
            container.RegisterType<IRepositoryBackedObservableCollection, RepositoryBackedObservableCollection<Guest>>("GuestRepository");
            container.RegisterType<IRepositoryBackedObservableCollection, RepositoryBackedObservableCollection<Booking>>("BookingRepository");
            container.RegisterType<IRepositoryBackedObservableCollection, RepositoryBackedObservableCollection<Room>>("RoomRepository");
        }
    }
}
