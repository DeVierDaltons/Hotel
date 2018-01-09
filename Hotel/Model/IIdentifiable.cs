using System;

namespace Hotel.Model
{
    public interface IIdentifiable
    {
        Guid Id
        { get; set; }
    }
}
