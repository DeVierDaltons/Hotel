using Hotel.Data;
using Hotel.Proxy;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void Initialize()
        {
            HotelServiceProxy proxy = new HotelServiceProxy();
            AllGuests = proxy.GetAllGuests();
            AllRooms = proxy.GetAllRooms();
            AllBookings = proxy.GetAllBookings();
            proxy.Close();
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
                HotelServiceProxy proxy = new HotelServiceProxy();
                proxy.EditGuest((Guest)sender);
                proxy.Close();
            };
        }

        private static void SubscribeToRoom(Room room)
        {
            room.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                HotelServiceProxy proxy = new HotelServiceProxy();
                proxy.EditRoom((Room)sender);
                proxy.Close();
            };
        }

        private static void SubscribeToBooking(Booking booking)
        {
            booking.PropertyChanged += delegate (object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                HotelServiceProxy proxy = new HotelServiceProxy();
                proxy.EditBooking((Booking)sender);
                proxy.Close();
            };
        }

        #endregion

        #region Collection change events

        private static void AllGuests_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HotelServiceProxy proxy = new HotelServiceProxy();
            if (e.OldItems != null)
            {
                proxy.RemoveGuest((Guest)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Guest newGuest = (Guest)e.NewItems[0];
                proxy.AddGuest(newGuest);
                SubscribeToGuest(newGuest);
            }
            proxy.Close();
        }

        private static void AllRooms_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HotelServiceProxy proxy = new HotelServiceProxy();
            if (e.OldItems != null)
            {
                proxy.RemoveRoom((Room)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Room newRoom = (Room)e.NewItems[0];
                proxy.AddRoom(newRoom);
                SubscribeToRoom(newRoom);
            }
            proxy.Close();
        }

        private static void AllBookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HotelServiceProxy proxy = new HotelServiceProxy();
            if (e.OldItems != null)
            {
                proxy.RemoveBooking((Booking)e.OldItems[0]);
            }
            else if (e.NewItems != null)
            {
                Booking newBooking = (Booking)e.NewItems[0];
                proxy.AddBooking(newBooking);
                SubscribeToBooking(newBooking);
            }
            proxy.Close();
        }

        #endregion
    }
}
