using Hotel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data;
using System.Collections.ObjectModel;

namespace Hotel.Callback
{
    public class CallbackOperations<T> where T: IIdentifiable, ICallback
    {
        ObservableCollection<T> Collection;
        public CallbackOperations(ObservableCollection<T> collection )
        {
            Collection = collection;
        }

        public void Add(T item)
        {
            Collection.Add(item);
        }

        public void Edit(T item)
        {
            T editItem = Collection.First(x => x.Id == item.Id);
            editItem = item;
        }

        public void Remove(T item)
        {
            Collection.Remove(item);
        }
    }
}
