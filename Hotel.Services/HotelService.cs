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
using Hotel.Data.Repository;

namespace Hotel.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class HotelService : IHotelService
    {
        RepositoryBackedObservableCollection<Guest> GuestRepository = new RepositoryBackedObservableCollection<Guest>(new NHibernateRepository<Guest>());
        RepositoryBackedObservableCollection<Room> RoomRepository = new RepositoryBackedObservableCollection<Room>(new NHibernateRepository<Room>());
        RepositoryBackedObservableCollection<Booking> BookingRepository = new RepositoryBackedObservableCollection<Booking>(new NHibernateRepository<Booking>());

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
            GuestRepository.Save(guest);
        }

        public void AddRoom(Room room)
        {
            RoomRepository.Save(room);
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
        public ObservableCollection<Booking> FilterBookings(BookingStatus? status = null, Guest guest = null)
        {
            ObservableCollection<Booking> filteredList = new ObservableCollection<Booking>();
            if (status == null && guest==null)
            {
                return null;
                
            }

            if(status != null)
            {
                filteredList = BookingRepository.Where(x => x.BookingStatus == status) as ObservableCollection<Booking>;
            }

            if(guest != null)
            {
                if (filteredList.Count > 0)
                {
                    return filteredList.Where(x => x.Guests.Contains(guest)) as ObservableCollection<Booking>;
                }
                else
                {
                    return BookingRepository.Where(x => x.Guests.Contains(guest)) as ObservableCollection<Booking>;
                }
            }

            return filteredList;
        }

        public ObservableCollection<Guest> FilterGuests(string filterString)
        {
            ObservableCollection<Guest> filteredGuests = new ObservableCollection<Guest>();
            foreach (Guest g in GuestRepository)
            {
                 if((g.FirstName != null && g.FirstName.ToLower().Contains(filterString)) ||
                (g.LastName != null && g.LastName.ToLower().Contains(filterString)) ||
                (g.PhoneNumber != null && g.PhoneNumber.ToLower().Contains(filterString)) ||
                (g.PostalCode != null && g.PostalCode.ToLower().Contains(filterString)) ||
                (g.EmailAdress != null && g.EmailAdress.ToLower().Contains(filterString)) ||
                (g.City != null && g.City.ToLower().Contains(filterString)) ||
                (g.Country != null && g.Country.ToLower().Contains(filterString)))
                {
                    filteredGuests.Add(g);
                }
            }
            return filteredGuests;
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
