using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hotel.Contracts
{
    [DataContract]
    public class BookingData
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
