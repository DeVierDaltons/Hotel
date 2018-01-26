﻿using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;
using System.Collections.ObjectModel;

namespace Hotel.Proxy
{
    public class HotelServiceProxy : DuplexClientBase<IHotelService>, IHotelService
    {
        public HotelServiceProxy(InstanceContext callbackInstance) : base(callbackInstance)
        {
            SubscribeClient();
        }

        public void AddBooking(Booking booking)
        {
            Channel.AddBooking(booking);
        }

        public void AddGuest(Guest guest)
        {
            Channel.AddGuest(guest);
        }

        public void AddRoom(Room room)
        {
            Channel.AddRoom(room);
        }

        public void EditBooking(Booking booking)
        {
            Channel.EditBooking(booking);
        }

        public void EditGuest(Guest guest)
        {
            Channel.EditGuest(guest);
        }

        public void EditRoom(Room room)
        {
            Channel.EditRoom(room);
        }

        public List<Booking> GetAllBookings()
        {
            return Channel.GetAllBookings();
        }

        public List<Guest> GetAllGuests()
        {
            return Channel.GetAllGuests();
        }

        public List<Room> GetAllRooms()
        {
            return Channel.GetAllRooms();
        }

        public void RemoveBooking(Booking booking)
        {
            Channel.RemoveBooking(booking);
        }

        public void RemoveGuest(Guest guest)
        {
            Channel.RemoveGuest(guest);
        }

        public void RemoveRoom(Room room)
        {
            Channel.RemoveRoom(room);
        }

        public void SubscribeClient()
        {
            Channel.SubscribeClient();
        }
    }
}
