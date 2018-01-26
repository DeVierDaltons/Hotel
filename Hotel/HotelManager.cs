using Hotel.Callback;
using Hotel.Data;
using Hotel.Proxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.Utilities;

namespace Hotel
{
    public static class HotelManager
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

        private static CallbackOperations<Guest> guestCallback;
        private static CallbackOperations<Room> roomCallback;
        private static CallbackOperations<Booking> bookingCallback;
        private static HotelServiceProxy guestProxy;
        private static HotelServiceProxy roomProxy;
        private static HotelServiceProxy bookingProxy;

        public static void Initialize()
        {
            AllGuests = new ObservableCollection<Guest>(); 
            guestCallback = new CallbackOperations<Guest>(AllGuests);
            HotelServiceProxy proxy = new HotelServiceProxy(new System.ServiceModel.InstanceContext(guestCallback));
            AllGuests = new ObservableCollection<Guest>(proxy.GetAllGuests());
            AllRooms = new ObservableCollection<Room>(proxy.GetAllRooms());
            AllBookings = new ObservableCollection<Booking>(proxy.GetAllBookings());
            proxy.Close();

            roomCallback = new CallbackOperations<Room>(AllRooms);
            bookingCallback = new CallbackOperations<Booking>(AllBookings);
            guestCallback = new CallbackOperations<Guest>(AllGuests);
            guestProxy = new HotelServiceProxy(new System.ServiceModel.InstanceContext(guestCallback));
            roomProxy = new HotelServiceProxy(new System.ServiceModel.InstanceContext(roomCallback));
            bookingProxy = new HotelServiceProxy(new System.ServiceModel.InstanceContext(bookingCallback));
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
                guestProxy.EditGuest((Guest)sender);
            };
        }

        private static void SubscribeToRoom(Room room)
        {
            room.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                roomProxy.EditRoom((Room)sender);
            };
        }

        private static void SubscribeToBooking(Booking booking)
        {
            booking.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                bookingProxy.EditBooking((Booking)sender);
            };
        }

        #endregion

        #region Collection change events

        private static void AllGuests_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                guestProxy.RemoveGuest((Guest)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Guest newGuest = (Guest)e.NewItems[0];
                guestProxy.AddGuest(newGuest);
                SubscribeToGuest(newGuest);
            }
        }

        private static void AllRooms_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                roomProxy.RemoveRoom((Room)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Room newRoom = (Room)e.NewItems[0];
                roomProxy.AddRoom(newRoom);
                SubscribeToRoom(newRoom);
                newRoom.Bookings = new List<Booking>();
            }
        }

        private static void AllBookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                bookingProxy.RemoveBooking((Booking)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Booking newBooking = (Booking)e.NewItems[0];
                bookingProxy.AddBooking(newBooking);
                SubscribeToBooking(newBooking);
            }
        }

        #endregion
    }
}
