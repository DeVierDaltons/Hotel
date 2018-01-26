using Hotel.Contracts;
using Hotel.Data;
using Hotel.Data.Extensions;
using Hotel.Proxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity.Interception.Utilities;

namespace Hotel
{
    public class HotelManager : ICallback
    {
        public static ObservableCollection<Guest> AllGuests
        {
            get; private set;
        }
        
        public static ObservableCollection<Room> AllRooms
        {
            get; private set;
        }

        public static ObservableCollection<Booking> AllBookings
        {
            get; private set;
        }

        private static bool ProcessingUpdatesFromServer = false;
        private static HotelServiceProxy ServiceProxy;

        public static void Initialize()
        {
            new HotelManager().Init();
        }

        private void Init()
        {
            ServiceProxy = new HotelServiceProxy(new InstanceContext(this));
            AllGuests = new ObservableCollection<Guest>(ServiceProxy.GetAllGuests());
            AllRooms = new ObservableCollection<Room>(ServiceProxy.GetAllRooms());
            AllBookings = new ObservableCollection<Booking>(ServiceProxy.GetAllBookings());
            LinkBookings();
            Subscribe();
        }

        private static void LinkBookings()
        {
            AllRooms.ForEach(room => room.Bookings = new List<Booking>());
            foreach(Booking booking in AllBookings)
            {
                foreach (Guid roomId in booking.RoomIds)
                {
                    Room matchedRoom = AllRooms.First(candidate => candidate.Id == roomId);
                    matchedRoom.Bookings.Add(booking);
                }
            }
        }

        private static void Subscribe()
        {
            AllGuests.CollectionChanged += AllGuests_CollectionChanged;
            AllGuests.ForEach(SubscribeToGuest);
            AllRooms.CollectionChanged += AllRooms_CollectionChanged;
            AllRooms.ForEach(SubscribeToRoom);
            AllBookings.CollectionChanged += AllBookings_CollectionChanged;
            AllBookings.ForEach(SubscribeToBooking);
        }

        #region Item changed events

        private static void SubscribeToGuest(Guest guest)
        {
            guest.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if( ProcessingUpdatesFromServer) { return; }
                Task.Run(() =>
                {
                    ServiceProxy.EditGuest((Guest)sender);
                });
            };
        }

        private static void SubscribeToRoom(Room room)
        {
            room.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if( ProcessingUpdatesFromServer) { return; }
                Task.Run(() =>
                {
                    ServiceProxy.EditRoom((Room)sender);
                });
            };
        }

        private static void SubscribeToBooking(Booking booking)
        {
            booking.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if( ProcessingUpdatesFromServer) { return; }
                Task.Run(() =>
                {
                    ServiceProxy.EditBooking((Booking)sender);
                });
            };
        }

        #endregion

        #region Collection change events

        private static void AllGuests_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( ProcessingUpdatesFromServer) { return; }
            if (e.OldItems != null)
            {
                Task.Run(() =>
                {
                    ServiceProxy.RemoveGuest((Guest)e.OldItems[0]);
                });
            }
            else if (e.NewItems != null)
            {
                Guest newGuest = (Guest)e.NewItems[0];
                Task.Run(() =>
                {
                    ServiceProxy.AddGuest(newGuest);
                });
                SubscribeToGuest(newGuest);
            }
        }

        private static void AllRooms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( ProcessingUpdatesFromServer) { return; }
            if (e.OldItems != null)
            {
                Task.Run(() =>
                {
                    ServiceProxy.RemoveRoom((Room)e.OldItems[0]);
                });
            }
            else if (e.NewItems != null)
            {
                Room newRoom = (Room)e.NewItems[0];
                Task.Run(() =>
                {
                    ServiceProxy.AddRoom(newRoom);
                });
                SubscribeToRoom(newRoom);
                newRoom.Bookings = new List<Booking>();
            }
        }

        private static void AllBookings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if( ProcessingUpdatesFromServer) { return; }
            if (e.OldItems != null)
            {
                Task.Run(() =>
                {
                    ServiceProxy.RemoveBooking((Booking)e.OldItems[0]);
                });
            }
            else if (e.NewItems != null)
            {
                Booking newBooking = (Booking)e.NewItems[0];
                Task.Run(() =>
                {
                    ServiceProxy.AddBooking(newBooking);
                });
                SubscribeToBooking(newBooking);
            }
        }

        public void AddGuest(Guest item)
        {
            ProcessingUpdatesFromServer = true;
            AllGuests.Add(item);
            SubscribeToGuest(item);
            ProcessingUpdatesFromServer = false;
        }
        
        public void RemoveGuest(Guest item)
        {
            ProcessingUpdatesFromServer = true;
            AllGuests.Remove(item);
            ProcessingUpdatesFromServer = false;
        }

        public void EditGuest(Guest item)
        {
            ProcessingUpdatesFromServer = true;
            AllGuests.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
            ProcessingUpdatesFromServer = false;
        }

        public void AddRoom(Room item)
        {
            ProcessingUpdatesFromServer = true;
            item.Bookings = AllBookings.Where(booking => booking.RoomIds.Contains(item.Id)).ToList();
            AllRooms.Add(item);
            SubscribeToRoom(item);
            ProcessingUpdatesFromServer = false;
        }

        public void RemoveRoom(Room item)
        {
            ProcessingUpdatesFromServer = true;
            AllRooms.Remove(item);
            ProcessingUpdatesFromServer = false;
        }

        public void EditRoom(Room item)
        {
            ProcessingUpdatesFromServer = true;
            AllRooms.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
            ProcessingUpdatesFromServer = false;
        }

        public void AddBooking(Booking item)
        {
            ProcessingUpdatesFromServer = true;
            item.SetGuestsAndRooms(AllGuests, AllRooms);
            AllBookings.Add(item);
            SubscribeToBooking(item);
            ProcessingUpdatesFromServer = false;
        }

        public void RemoveBooking(Booking item)
        {
            ProcessingUpdatesFromServer = true;
            AllBookings.Remove(item);
            ProcessingUpdatesFromServer = false;
        }

        public void EditBooking(Booking item)
        {
            ProcessingUpdatesFromServer = true;
            AllBookings.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
            ProcessingUpdatesFromServer = false;
        }
        #endregion
    }
}
