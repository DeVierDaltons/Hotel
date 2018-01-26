using Hotel.Contracts;
using Hotel.Data;
using Hotel.Data.Extensions;
using Hotel.Data.Repository;
using Hotel.Services.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;

namespace Hotel.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public class HotelService : IHotelService
    {
        RepositoryBackedObservableCollection<Guest> GuestRepository = new RepositoryBackedObservableCollection<Guest>(new NHibernateRepository<Guest>());
        RepositoryBackedObservableCollection<Room> RoomRepository = new RepositoryBackedObservableCollection<Room>(new NHibernateRepository<Room>());
        RepositoryBackedObservableCollection<Booking> BookingRepository = new RepositoryBackedObservableCollection<Booking>(new NHibernateRepository<Booking>());
        List<ICallback> CallbackChannels = new List<ICallback>();

        
        public HotelService()
        {
           
        }

        /// <summary>
        /// client should call this method before being notified to some event
        /// </summary>
        public void SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<ICallback>();
            
            if (!CallbackChannels.Contains(channel)) //if CallbackChannels not contain current one.
            {
                (channel as ICommunicationObject).Closed += (object sender, EventArgs e) => UnSubscribeClient(channel);
                CallbackChannels.Add(channel);
            }
        }

        public void UnSubscribeClient(ICallback callback)
        {
             CallbackChannels.Remove(callback);
        }
        
        #region add
        public void AddBooking(Booking booking)
        {
            BookingRepository.Add(booking);
        }

        public void AddGuest(Guest guest)
        {
            GuestRepository.Add(guest);
            foreach(ICallback client in CallbackChannels)
            {
                
                client.Add(guest);
            }
        }

        public void AddRoom(Room room)
        {
            RoomRepository.Add(room);
            foreach (ICallback client in CallbackChannels)
            {
                client.Add(room);
            }
        }
        #endregion
        #region edit
        public void EditBooking(Booking booking)
        {
            Booking target = BookingRepository.First(candidate => candidate.Id == booking.Id);
            target.CopyDeltaProperties(booking);
            foreach (ICallback client in CallbackChannels)
            {
                client.Edit(booking);
            }
        }

        public void EditGuest(Guest guest)
        {
            Guest target = GuestRepository.First(candidate => candidate.Id == guest.Id);
            target.CopyDeltaProperties(guest);
            foreach (ICallback client in CallbackChannels)
            {
                client.Edit(guest);
            }
        }

        public void EditRoom(Room room)
        {
            Room target = RoomRepository.First(candidate => candidate.Id == room.Id);
            target.CopyDeltaProperties(room);
            foreach (ICallback client in CallbackChannels)
            {
                client.Edit(room);
            }
        }

        #endregion
        #region Filter
        public List<Booking> FilterBookings(BookingStatus? status = null, Guest guest = null)
        {
            List<Booking> filteredList = new List<Booking>();
            if (status == null && guest==null)
            {
                return null;
            }

            if(status != null)
            {
                filteredList = BookingRepository.Where(x => x.BookingStatus == status).ToList();
            }

            if(guest != null)
            {
                if (filteredList.Count > 0)
                {
                    return filteredList.Where(x => x.GuestIds.Contains(guest.Id)).ToList();
                }
                else
                {
                    return BookingRepository.Where(x => x.GuestIds.Contains(guest.Id)).ToList();
                }
            }

            return filteredList;
        }

        public List<Guest> FilterGuests(string filterString)
        {
            List<Guest> filteredGuests = new List<Guest>();
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
        public List<Booking> GetAllBookings()
        {
            return BookingRepository.ToList();
        }

        public List<Guest> GetAllGuests()
        {
            return GuestRepository.ToList();
        }

        public List<Room> GetAllRooms()
        {
            return RoomRepository.ToList();
        }
        #endregion
        #region remove
        public void RemoveBooking(Booking booking)
        {
            BookingRepository.Remove(booking);
            foreach (ICallback client in CallbackChannels)
            {
                client.Remove(booking);
            }
        }

        public void RemoveGuest(Guest guest)
        {
            GuestRepository.Remove(guest);
            foreach (ICallback client in CallbackChannels)
            {
                client.Remove(guest);
            }
        }

        public void RemoveRoom(Room room)
        {
            RoomRepository.Remove(room);
            foreach (ICallback client in CallbackChannels)
            {
                client.Remove(room);
            }
        }

       

        #endregion
    }
}
