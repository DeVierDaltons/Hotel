using Hotel.DataAccessObjects;
using Hotel.Repository;
using System.Diagnostics;
using Unity.Attributes;

namespace Hotel.Model
{
    public class HotelManager
    {
        private IRepository<Guest> GuestRepository;
        private IRepository<Room> RoomRepository;
        private IRepository<Booking> BookingRepository;

        public RepositoryBackedObservableCollection<Guest> Guests { get; }
        public RepositoryBackedObservableCollection<Room> Rooms { get; }
        public RepositoryBackedObservableCollection<Booking> Bookings { get; }

        public HotelManager([Dependency]IRepository<Guest> guestRepository, [Dependency]IRepository<Room> roomRepository, [Dependency]IRepository<Booking> bookingRepository)
        {
            GuestRepository = guestRepository;
            RoomRepository = roomRepository;
            BookingRepository = bookingRepository;
            Guests = new RepositoryBackedObservableCollection<Guest>(GuestRepository);
            Rooms = new RepositoryBackedObservableCollection<Room>(RoomRepository);
            Bookings = new RepositoryBackedObservableCollection<Booking>(BookingRepository);
            AddAllBookingsToRoom();
        }

        private void AddAllBookingsToRoom()
        {
            foreach(Booking booking in Bookings)
            {
                AddBookingToRoom(booking);
            }
        }

        private void AddBookingToRoom(Booking booking)
        {
            Debug.Assert(Rooms.Contains(booking.Room));
            booking.Room.Bookings.Add(booking);
        }

        public void AddBooking(Booking booking)
        {
            Bookings.Add(booking);
            AddBookingToRoom(booking);
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void AddGuest(Guest guest)
        {
            Guests.Add(guest);
        }
    }
}
