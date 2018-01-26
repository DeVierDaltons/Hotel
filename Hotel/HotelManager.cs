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

        #region Proxy calls

        public static void AddGuest(Guest newGuest)
        {
            Task.Run(() =>
            {
                ServiceProxy.AddGuest(newGuest);
            });
        }

        public static void RemoveGuest(Guest guest)
        {
            Task.Run(() =>
            {
                ServiceProxy.RemoveGuest(guest);
            });
        }

        public static void EditGuest(Guest guest)
        {
            Task.Run(() =>
            {
                ServiceProxy.EditGuest(guest);
            });
        }

        public static void AddRoom(Room newRoom)
        {
            Task.Run(() =>
            {
                ServiceProxy.AddRoom(newRoom);
            });
        }

        public static void RemoveRoom(Room room)
        {
            Task.Run(() =>
            {
                ServiceProxy.RemoveRoom(room);
            });
        }

        public static void EditRoom(Room room)
        {
            Task.Run(() =>
            {
                ServiceProxy.EditRoom(room);
            });
        }

        public static void AddBooking(Booking newBooking)
        {
            Task.Run(() =>
            {
                ServiceProxy.AddBooking(newBooking);
            });
        }

        public static void RemoveBooking(Booking booking)
        {
            Task.Run(() =>
            {
                ServiceProxy.RemoveBooking(booking);
            });
        }

        public static void EditBooking(Booking booking)
        {
            Task.Run(() =>
            {
                ServiceProxy.EditBooking(booking);
            });
        }
        #endregion
        #region ICallback

        public void OnAddedGuest(Guest item)
        {
            AllGuests.Add(item);
        }
        
        public void OnRemovedGuest(Guest item)
        {
            AllGuests.Remove(item);
        }

        public void OnEditedGuest(Guest item)
        {
            AllGuests.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
        }

        public void OnAddedRoom(Room item)
        {
            item.Bookings = AllBookings.Where(booking => booking.RoomIds.Contains(item.Id)).ToList();
            AllRooms.Add(item);
        }

        public void OnRemovedRoom(Room item)
        {
            AllRooms.Remove(item);
        }

        public void OnEditedRoom(Room item)
        {
            AllRooms.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
        }

        public void OnAddedBooking(Booking item)
        {
            item.SetGuestsAndRooms(AllGuests, AllRooms);
            AllBookings.Add(item);
        }

        public void OnRemovedBooking(Booking item)
        {
            AllBookings.Remove(item);
        }

        public void OnEditedBooking(Booking item)
        {
            AllBookings.First(candidate => candidate.Id == item.Id).CopyDeltaProperties(item);
        }

        #endregion
    }
}
