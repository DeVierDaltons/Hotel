using Hotel.Command;
using Hotel.Dao;
using Hotel.Model;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
