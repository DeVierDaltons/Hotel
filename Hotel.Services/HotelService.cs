using Hotel.Contracts;
using Hotel.Data;
using Hotel.Data.DataAccessObjects;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;

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
            BookingRepository.Update(booking);
        }

        public void EditGuest(Guest guest)
        {
            GuestRepository.Update(guest);
        }

        public void EditRoom(Room room)
        {
            RoomRepository.Update(room);
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
            return new ObservableCollection<Booking>(BookingRepository.GetAll());
        }

        public ObservableCollection<Guest> GetAllGuests()
        {
            return new ObservableCollection<Guest>(GuestRepository.GetAll());
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            return new ObservableCollection<Room>(RoomRepository.GetAll());
        }
        #endregion
        #region remove
        public void RemoveBooking(Booking booking)
        {
            BookingRepository.Delete(booking);
        }

        public void RemoveGuest(Guest guest)
        {
            GuestRepository.Delete(guest);
        }

        public void RemoveRoom(Room room)
        {
            RoomRepository.Delete(room);
        }
        #endregion
    }
}
