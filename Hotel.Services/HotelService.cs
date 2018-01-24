﻿using Hotel.Contracts;
using Hotel.Data;
using Hotel.Data.Extensions;
using Hotel.Data.Repository;
using Hotel.Services.Repository;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;

namespace Hotel.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    public class HotelService : IHotelService
    {
        RepositoryBackedObservableCollection<Guest> GuestRepository = new RepositoryBackedObservableCollection<Guest>(new NHibernateRepository<Guest>());
        RepositoryBackedObservableCollection<Room> RoomRepository = new RepositoryBackedObservableCollection<Room>(new NHibernateRepository<Room>());
        RepositoryBackedObservableCollection<Booking> BookingRepository = new RepositoryBackedObservableCollection<Booking>(new NHibernateRepository<Booking>());

        #region add
        public void AddBooking(Booking booking)
        {
            BookingRepository.Add(booking);
        }

        public void AddGuest(Guest guest)
        {
            GuestRepository.Add(guest);
        }

        public void AddRoom(Room room)
        {
            RoomRepository.Add(room);
        }
        #endregion
        #region edit
        public void EditBooking(Booking booking)
        {
            Booking target = BookingRepository.First(candidate => candidate.Id == booking.Id);
            target.CopyDeltaProperties(booking);
        }

        public void EditGuest(Guest guest)
        {
            Guest target = GuestRepository.First(candidate => candidate.Id == guest.Id);
            target.CopyDeltaProperties(guest);
        }

        public void EditRoom(Room room)
        {
            Room target = RoomRepository.First(candidate => candidate.Id == room.Id);
            target.CopyDeltaProperties(room);
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
                    return filteredList.Where(x => x.GuestIds.Contains(guest.Id)) as ObservableCollection<Booking>;
                }
                else
                {
                    return BookingRepository.Where(x => x.GuestIds.Contains(guest.Id)) as ObservableCollection<Booking>;
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
            return BookingRepository;
        }

        public ObservableCollection<Guest> GetAllGuests()
        {
            return GuestRepository;
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            return RoomRepository;
        }
        #endregion
        #region remove
        public void RemoveBooking(Booking booking)
        {
            BookingRepository.Remove(booking);
        }

        public void RemoveGuest(Guest guest)
        {
            GuestRepository.Remove(guest);
        }

        public void RemoveRoom(Room room)
        {
            RoomRepository.Remove(room);
        }
        #endregion
    }
}
