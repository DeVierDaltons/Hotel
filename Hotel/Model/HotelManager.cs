using Hotel.Dao;
using Hotel.Repository;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hotel.Model
{
    public class HotelManager
    {
        private IRepository<Guest> GuestRepository = new NHibernateRepository<Guest>();

        public RepositoryBackedObservableCollection<Guest> Guests { get; }
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookingList = new List<Booking>();
            foreach(Room room in Rooms) {
                bookingList.AddRange(room.Bookings);
            }
            return bookingList;
        }

        public HotelManager()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            Guests = new RepositoryBackedObservableCollection<Guest>(GuestRepository);
        }

        public void AddBooking(Booking booking)
        {
            booking.Room.Bookings.Add(booking);
        }

        public void AddGuest(Guest guest)
        {
            Guests.Add(guest);
        }
    }
}
