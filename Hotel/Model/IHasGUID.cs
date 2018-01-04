using System;

namespace Hotel.Model
{
    public interface IHasGUID
    {
        Guid Id
        { get; set; }
    }
}
