using System.Collections.ObjectModel;
using System.ComponentModel;
using Hotel.Model;

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
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            T changedItem = (T)sender;
            repository.Update(changedItem);
        }

        public void AddItem(T item)
        {
            Add(item);
            repository.Save(item);
            item.PropertyChanged += OnItemChanged;
            Logger.LogAction<T>(item, ActionType.Add);
        }

        public void RemoveItem(T item)
        {
            item.PropertyChanged -= OnItemChanged;
            Remove(item);
            repository.Delete(item);
        }
    }
}
