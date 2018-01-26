using Hotel.Contracts;
using Hotel.Data;
using Hotel.Data.Extensions;
using Hotel.Services.Repository;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// client should call this method before being notified to some event
        /// </summary>
        public void SubscribeClient()
        {
            var channel = OperationContext.Current.GetCallbackChannel<ICallback>();
            if (!CallbackChannels.Contains(channel)) //if CallbackChannels not contain current one.
            {
                (channel as ICommunicationObject).Closed += (object sender, EventArgs e) => UnsubscribeClient(channel);
                CallbackChannels.Add(channel);
            }
        }

        private void UnsubscribeClient(ICallback callback)
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
            foreach (ICallback client in CallbackChannels)
            {
                client.AddGuest(guest);
            }
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
            foreach (ICallback client in CallbackChannels)
            {
                client.EditGuest(guest);
            }
        }

        public void EditRoom(Room room)
        {
            Room target = RoomRepository.First(candidate => candidate.Id == room.Id);
            target.CopyDeltaProperties(room);
        }

        #endregion
        #region Filter

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
        }

        public void RemoveGuest(Guest guest)
        {
            GuestRepository.Remove(guest);
            foreach (ICallback client in CallbackChannels)
            {
                client.RemoveGuest(guest);
            }
        }

        public void RemoveRoom(Room room)
        {
            RoomRepository.Remove(room);
        }

        #endregion
    }
}
