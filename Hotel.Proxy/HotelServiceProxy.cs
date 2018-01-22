using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;

namespace Hotel.Proxy
{
    public class HotelServiceProxy : ClientBase<IHotelService>, IHotelService
    {
        public void AddGuest(Guest guest)
        {
            throw new NotImplementedException();
        }
    }
}
