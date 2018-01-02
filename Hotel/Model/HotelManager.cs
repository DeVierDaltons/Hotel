﻿using Hotel.Dao;
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

        public RepositoryBackedObservableCollection<Guest> Guests { get; }
        public RepositoryBackedObservableCollection<Room> Rooms { get; }

        public HotelManager()
        {
            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);
            Guests = new RepositoryBackedObservableCollection<Guest>(GuestRepository);
            Rooms = new RepositoryBackedObservableCollection<Room>(RoomRepository);
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> allBookings = new List<Booking>();
            foreach(Room room in Rooms)
            {
                allBookings.AddRange(room.Bookings);
            }
            return allBookings;
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
