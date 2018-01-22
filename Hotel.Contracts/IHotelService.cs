using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Contracts
{
    [ServiceContract]
    public interface IHotelService
    {
        [OperationContract]
        void DummyOperation();
    }
}
