﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using Hotel.Data.Repository;

namespace Hotel.Services.Repository
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