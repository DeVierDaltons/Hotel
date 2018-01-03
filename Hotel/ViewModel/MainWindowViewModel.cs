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
        public ICommand ShowAddGuestWindowCommand { get; }
        public ICommand ShowGuestsWindowCommand { get; }
        public ICommand ShowAddRoomWindowCommand { get; }
        public ICommand ShowAddModifyRoomWindow { get; }
        public ICommand ShowAddBookingWindowCommand { get; }
        public ICommand ShowBookingsWindowCommand { get; }

        public MainWindowViewModel()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            HotelManager = new HotelManager(new NHibernateRepository<Guest>(), 
                                            new NHibernateRepository<Room>(), 
                                            new NHibernateRepository<Booking>());
            ShowAddGuestWindowCommand = new ShowGuestDetailWindowCommand(new AddGuestCommand(HotelManager.Guests));
            ShowGuestsWindowCommand = new ShowGuestsWindowCommand(HotelManager);
            ShowAddRoomWindowCommand = new OpenAddRoomWindowCommand(HotelManager);
            ShowAddBookingWindowCommand = new OpenAddBookingWindowCommand(HotelManager);
            ShowAddModifyRoomWindow = new OpenModifyRoomWindowCommand(HotelManager);
            ShowAddModifyRoomWindow = new OpenModifyRoomWindowCommand(HotelManager);
            ShowBookingsWindowCommand = new OpenModifyBookingWindowCommand(HotelManager);
        }
    }
}
