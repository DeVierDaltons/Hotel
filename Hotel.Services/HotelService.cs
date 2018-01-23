using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;
using System.Collections.ObjectModel;
using Hotel.Data.DataAccessObjects;
using System.ServiceModel;
using NHibernate.Tool.hbm2ddl;

namespace Hotel.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class HotelService : IHotelService
    {
        NHibernateRepository<Guest> GuestRepository = new NHibernateRepository<Guest>();
        NHibernateRepository<Room> RoomRepository = new NHibernateRepository<Room>();
        NHibernateRepository<Booking> BookingRepository = new NHibernateRepository<Booking>();

        public HotelService()
        {
           
        }

        public void CreateDatabaseIfNeeded()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
        }

        #region add
        public void AddBooking(Booking booking)
        {
            BookingRepository.Save(booking);
        }

        public void AddGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void AddRoom(Room room)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region edit
        public void EditBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void EditGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void EditRoom(Room room)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Filter
        public ObservableCollection<Guest> FilterBookings(BookingStatus status, string filterString)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Guest> FilterGuests(string filterString)
        {
            throw new NotImplementedException();
        }
        #endregion Filter
        #region Get
        public ObservableCollection<Booking> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region remove
        public void RemoveBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void RemoveRoom(Room room)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
