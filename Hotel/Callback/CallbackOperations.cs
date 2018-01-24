using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Hotel.Callback
{ 
    public class CallbackOperations<T> : ICallback where T: IIdentifiable
    {
        ObservableCollection<T> Collection;
        public CallbackOperations(ObservableCollection<T> collection )
        {
            Collection = collection;
        }

        public void Add(object item)
        {
            Collection.Add((T)item);
        }

        public void Edit(object item)
        {
            T editItem = Collection.First(x => x.Id == ((T)item).Id);
            editItem = (T)item;
        }

        public void Remove(object item)
        {
            Collection.Remove((T)item);
        }
    }
}
