using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using Unity.Attributes;

namespace Hotel.Repository
{
    public class RepositoryBackedObservableCollection<T> : ObservableCollection<T>, IRepositoryBackedObservableCollection where T : INotifyPropertyChanged
    {
        private IRepository<T> repository;

        public RepositoryBackedObservableCollection([Dependency("NHibernateRepository")]IRepository<T> repository) : base(repository.GetAll())
        {
            this.repository = repository;
            foreach(T item in this)
            {
                item.PropertyChanged += OnItemChanged;
            }
            CollectionChanged += RepositoryBackedObservableCollection_CollectionChanged;
        }

        private void RepositoryBackedObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                T oldItem = (T) e.OldItems[0];
                repository.Delete(oldItem);
                oldItem.PropertyChanged -= OnItemChanged;
            }
            else
            {
                T newItem = (T)e.NewItems[0];
                repository.Save(newItem);
                newItem.PropertyChanged += OnItemChanged;
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            T changedItem = (T)sender;
            repository.Update(changedItem);
        }
    }
}
