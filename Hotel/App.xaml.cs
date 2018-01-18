using Hotel.DataAccessObjects;
using Hotel.Model;
using Hotel.Repository;
using Hotel.View;
using Hotel.ViewModel;
using NHibernate.Tool.hbm2ddl;
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
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            MainWindow window = container.Resolve<MainWindow>();
           
            window.Initialize();
            window.Show();
        }

        private void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Guest>), new RepositoryBackedObservableCollection<Guest>(new NHibernateRepository<Guest>()));
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Booking>), new RepositoryBackedObservableCollection<Booking>(new NHibernateRepository<Booking>()));
            container.RegisterInstance(typeof(RepositoryBackedObservableCollection<Room>), new RepositoryBackedObservableCollection<Room>(new NHibernateRepository<Room>()));
        }
    }
}
