using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Extensions;

namespace Hotel.Repository
{
    public class RepositoryBackedObservableCollection<T> : ObservableCollection<T>
    {
        private IRepository<T> repository;

        public RepositoryBackedObservableCollection(IRepository<T> repository)
        {
            this.repository = repository;
            this.AddRange(repository.GetAll());
        }

        public void AddItem(T item)
        {
            Add(item);
            repository.Save(item);
        }
    }
}
