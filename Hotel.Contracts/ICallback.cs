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
        void OnAddedGuest(Guest item);
        [OperationContract]
        void OnRemovedGuest(Guest item);
        [OperationContract]
        void OnEditedGuest(Guest item);
        [OperationContract]
        void OnAddedRoom(Room item);
        [OperationContract]
        void OnRemovedRoom(Room item);
        [OperationContract]
        void OnEditedRoom(Room item);
        [OperationContract]
        void OnAddedBooking(Booking item);
        [OperationContract]
        void OnRemovedBooking(Booking item);
        [OperationContract]
        void OnEditedBooking(Booking item);
    }
}
