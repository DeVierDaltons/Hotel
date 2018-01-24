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
        void Add(object item);
        void Remove(object item);
        void Edit(object item);
    }
}
