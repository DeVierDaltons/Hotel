using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Contracts
{
    [ServiceContract]
    public interface IHotelService
    {
        //Guest operations
        [OperationContract]
        void AddGuest(Guest guest);
        [OperationContract]
        void RemoveGuest(Guest guest);
        [OperationContract]
        void EditGuest(Guest guest);
        [OperationContract]
        ObservableCollection<Guest> GetAllGuests();
        [OperationContract]
        ObservableCollection<Guest> FilterGuests(string filterString);

        //Booking operations
        [OperationContract]
        void AddBooking(Booking booking);
        [OperationContract]
        void RemoveBooking(Booking booking);
        [OperationContract]
        void EditBooking(Booking booking);
        [OperationContract]
        ObservableCollection<Booking> GetAllBookings();
        [OperationContract]
        ObservableCollection<Booking> FilterBookings(BookingStatus? status = null, Guest guest = null);

        //Room operations
        [OperationContract]
        void AddRoom(Room room);
        [OperationContract]
        void RemoveRoom(Room room);
        [OperationContract]
        void EditRoom(Room room);
        [OperationContract]
        ObservableCollection<Room> GetAllRooms();
    }
}
