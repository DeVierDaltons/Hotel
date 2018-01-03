using Hotel.Dao;
using Hotel.Repository;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Hotel.Model
{
    public class HotelManager
    {
        private IRepository<Guest> GuestRepository = new NHibernateRepository<Guest>();
        private IRepository<Room> RoomRepository = new NHibernateRepository<Room>();
        private IRepository<Booking> BookingRepository = new NHibernateRepository<Booking>();

        public RepositoryBackedObservableCollection<Guest> Guests { get; }
        public RepositoryBackedObservableCollection<Room> Rooms { get; }
        public RepositoryBackedObservableCollection<Booking> Bookings { get; }

        public HotelManager()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            Guests = new RepositoryBackedObservableCollection<Guest>(GuestRepository);
            Rooms = new RepositoryBackedObservableCollection<Room>(RoomRepository);
            Bookings = new RepositoryBackedObservableCollection<Booking>(BookingRepository);
            AddBookingsToRooms();
        }

        private void AddBookingsToRooms()
        {
            foreach(Booking booking in Bookings)
            {
                AddBookingToRoom(booking);
            }
        }

        private static void AddBookingToRoom(Booking booking)
        {
            booking.Room.Bookings.Add(booking);
        }

        public void AddBooking(Booking booking)
        {
            Bookings.AddItem(booking);
            AddBookingToRoom(booking);
        }

        public void AddGuest(Guest guest)
        {
            Guests.AddItem(guest);
        }
    }
}
