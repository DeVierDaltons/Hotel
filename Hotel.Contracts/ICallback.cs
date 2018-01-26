using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Contracts
{
    [ServiceContract]
    public interface ICallback
    {
        [OperationContract]
        void AddGuest(Guest item);
        [OperationContract]
        void RemoveGuest(Guest item);
        [OperationContract]
        void EditGuest(Guest item);
        [OperationContract]
        void AddRoom(Room item);
        [OperationContract]
        void RemoveRoom(Room item);
        [OperationContract]
        void EditRoom(Room item);
        [OperationContract]
        void AddBooking(Booking item);
        [OperationContract]
        void RemoveBooking(Booking item);
        [OperationContract]
        void EditBooking(Booking item);
    }
}
