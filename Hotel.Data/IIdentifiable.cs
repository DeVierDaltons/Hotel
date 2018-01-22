using System;

namespace Hotel.Data
{
    public interface IIdentifiable
    {
        Guid Id
        { get; set; }
    }
}
