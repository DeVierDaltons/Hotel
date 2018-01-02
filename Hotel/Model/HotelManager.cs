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

        public HotelManager()
        {
            AddSomeRooms();
            AddSomeGuests();
        }

        private void AddSomeRooms()
        {
            Room r = new Room()
            {
                RoomNumber = "0",
                Beds = 2,
                HasNiceView = true,
                PricePerDay = 100,
                Quality = RoomQuality.Budget
            };
            Room r2 = new Room()
            {
                RoomNumber = "1",
                Beds = 2,
                HasNiceView = true,
                PricePerDay = 100,
                Quality = RoomQuality.Budget
            };
            Room r3 = new Room()
            {
                RoomNumber = "2",
                Beds = 2,
                HasNiceView = false,
                PricePerDay = 50,
                Quality = RoomQuality.Comfort
            };
            Room r4 = new Room()
            {
                RoomNumber = "3",
                Beds = 4,
                HasNiceView = false,
                PricePerDay = 200,
                Quality = RoomQuality.Luxe
            };
            Rooms.Add(r);
            Rooms.Add(r2);
            Rooms.Add(r3);
            Rooms.Add(r4);
        }

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

        private void AddSomeGuests()
        {
            Guest olaf = new Guest()
            {
                FirstName = "Olaf",
                LastName = "van der Kruk",
                PhoneNumber = "0612345678",
                EmailAdress = "olaf@krukempire.nl",
                Adress = "Nergensniet 12",
                PostalCode = "1234 AB",
                City = "Utrecht",
                Country = "The Netherlands",
                ICEPhoneNumber = "0612345678"
            };
            Guest michael = new Guest()
            {
                FirstName = "Michael",
                LastName = "Paul Kleijn",
                PhoneNumber = "0612345678",
                EmailAdress = "michael@kleinmaarfijn.nl",
                Adress = "Nergensniet 11",
                PostalCode = "1234 AB",
                City = "Verweggie",
                Country = "The Netherlands",
                ICEPhoneNumber = "0612345678"
            };
            Guest dirkjan = new Guest()
            {
                FirstName = "Dirk-Jan",
                LastName = "Sleurink",
                PhoneNumber = "0612345678",
                EmailAdress = "dirkjan@exmilitarybadass.nl",
                Adress = "Nergensniet 13",
                PostalCode = "1234 AB",
                City = "Verweggie",
                Country = "The Netherlands",
                ICEPhoneNumber = "0612345678"
            };
            Guest tama = new Guest()
            {
                FirstName = "Tama",
                LastName = "McGlinn",
                PhoneNumber = "0612345678",
                EmailAdress = "tama@mcglinn.nl",
                Adress = "Nergensniet 14",
                PostalCode = "1234 AB",
                City = "Verweggie",
                Country = "The Netherlands",
                ICEPhoneNumber = "0612345678"
            };
            Guests.Add(olaf);
            Guests.Add(michael);
            Guests.Add(dirkjan);
            Guests.Add(tama);
        }
    }
}
