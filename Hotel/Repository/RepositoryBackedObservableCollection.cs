using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;

namespace Hotel.Repository
{
    public class RepositoryBackedObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private IRepository<T> repository;

        public RepositoryBackedObservableCollection(IRepository<T> repository) : base(repository.GetAll())
        {
            this.repository = repository;
            foreach(T item in this)
            {
                item.PropertyChanged += OnItemChanged;
            }
            CollectionChanged += RepositoryBackedObservableCollection_CollectionChanged;
        }

        private void RepositoryBackedObservableCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<T> oldItems = e.OldItems != null ? e.OldItems.Cast<T>() : new List<T>();
            IEnumerable<T> newItems = e.NewItems != null ? e.NewItems.Cast<T>() : new List<T>();
            var toAdd = newItems.Where(item => !oldItems.Contains(item));
            foreach(T item in toAdd)
            {
                repository.Save(item);
                item.PropertyChanged += OnItemChanged;
            }
            var toRemove = oldItems.Where(item => !newItems.Contains(item));
            foreach(T item in toRemove)
            {
                repository.Delete(item);
                item.PropertyChanged -= OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            T changedItem = (T)sender;
            repository.Update(changedItem);
        }
    }
}
