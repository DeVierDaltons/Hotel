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

        public ObservableCollection<Guest> FilterBookings(BookingStatus status, string filterString)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Guest> FilterGuests(string filterString)
        {
            throw new NotImplementedException();
        }

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
    }
}
