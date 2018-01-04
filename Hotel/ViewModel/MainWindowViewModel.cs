using Hotel.Dao;
using Hotel.Model;
using NHibernate.Tool.hbm2ddl;

namespace Hotel.ViewModel
{
    public class MainWindowViewModel
    {
        public HotelManager HotelManager { get; set; }

        public MainWindowViewModel()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            HotelManager = new HotelManager(new NHibernateRepository<Guest>(), 
                                            new NHibernateRepository<Room>(), 
                                            new NHibernateRepository<Booking>());
        }
    }
}
