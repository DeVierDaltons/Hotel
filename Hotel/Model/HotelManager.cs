using Hotel.Dao;
using Hotel.Extensions;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class HotelManager
    {
        public ObservableCollection<Guest> Guests { get; } = new ObservableCollection<Guest>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        IGuestRepository PersonRepo = new NHibernateGuestRepository();

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookingList = new List<Booking>();
            foreach(Room room in Rooms)
            {
                bookingList.AddRange(room.Bookings);
            }
            return bookingList;
        }

        public void AddBooking(Booking booking)
        {
            booking.Room.Bookings.Add(booking);
        }

        public HotelManager()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            Guests.AddRange(PersonRepo.GetAll()); 
        }

        public void AddGuest(Guest guest)
        {
            Guests.Add(guest);
            PersonRepo.Save(guest);
        }
    }
}
