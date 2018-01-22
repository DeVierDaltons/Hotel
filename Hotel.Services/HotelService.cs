using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;
using System.Collections.ObjectModel;

namespace Hotel.Services
{
    public class HotelService : IHotelService
    {
        #region add
        public void AddBooking(Booking booking)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void EditGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void EditRoom(Room room)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ObservableCollection<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region remove
        public void RemoveBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void RemoveGuest(Guest guest)
        {
            throw new NotImplementedException();
        }

        public void RemoveRoom(Room room)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
