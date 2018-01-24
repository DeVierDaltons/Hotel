using System;
using System.Runtime.Serialization;

namespace Hotel.Data
{
    public interface IIdentifiable
    {
        [DataMember]
        Guid Id
        { get; set; }
    }
}
