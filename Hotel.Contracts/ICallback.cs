using Hotel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Contracts
{
    public interface ICallback
    {
        void AddGuest(Guest item);
        void RemoveGuest(Guest item);
        void EditGuest(Guest item);
    }
}
