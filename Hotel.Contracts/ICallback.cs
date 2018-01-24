using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Contracts
{
    [ServiceContract]
    public interface ICallback
    {
        [OperationContract]
        [ServiceKnownType(typeof(Guest))]
        [ServiceKnownType(typeof(Booking))]
        [ServiceKnownType(typeof(Room))]
        void Add(object item);
        [OperationContract]
        [ServiceKnownType(typeof(Guest))]
        [ServiceKnownType(typeof(Booking))]
        [ServiceKnownType(typeof(Room))]
        void Remove(object item);
        [OperationContract]
        [ServiceKnownType(typeof(Guest))]
        [ServiceKnownType(typeof(Booking))]
        [ServiceKnownType(typeof(Room))]
        void Edit(object item);
    }
}
