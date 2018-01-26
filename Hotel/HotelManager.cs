using Hotel.Contracts;
using Hotel.Data;
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

        private static HotelServiceProxy proxy;

        public static void Initialize()
        {
            new HotelManager().Init();
        }

        private void Init()
        {
            proxy = new HotelServiceProxy(new InstanceContext(this));
            AllGuests = new ObservableCollection<Guest>(proxy.GetAllGuests());
            AllRooms = new ObservableCollection<Room>(proxy.GetAllRooms());
            AllBookings = new ObservableCollection<Booking>(proxy.GetAllBookings());
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
                proxy.EditGuest((Guest)sender);
            };
        }

        private static void SubscribeToRoom(Room room)
        {
            room.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                proxy.EditRoom((Room)sender);
            };
        }

        private static void SubscribeToBooking(Booking booking)
        {
            booking.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                proxy.EditBooking((Booking)sender);
            };
        }

        #endregion

        #region Collection change events

        private static void AllGuests_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                proxy.RemoveGuest((Guest)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Guest newGuest = (Guest)e.NewItems[0];
                Task.Run(() =>
                {
                    proxy.AddGuest(newGuest);
                });
                SubscribeToGuest(newGuest);
            }
        }

        private static void AllRooms_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                proxy.RemoveRoom((Room)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Room newRoom = (Room)e.NewItems[0];
                Task.Run(() =>
                {
                    proxy.AddRoom(newRoom);
                });
                SubscribeToRoom(newRoom);
                newRoom.Bookings = new List<Booking>();
            }
        }

        private static void AllBookings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                proxy.RemoveBooking((Booking)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Booking newBooking = (Booking)e.NewItems[0];
                Task.Run(() =>
                {
                    proxy.AddBooking(newBooking);
                });
                SubscribeToBooking(newBooking);
            }
        }

        public void AddGuest(Guest item)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuest(Guest item)
        {
            throw new NotImplementedException();
        }

        public void EditGuest(Guest item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
