using Hotel.Model;
using Hotel.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace Hotel.ViewModel
{
    public class GuestsViewModel : INotifyPropertyChanged
    {
        private RepositoryBackedObservableCollection<Guest> _guests;
        private Action<Guest> EditGuestAction;
        private Action AddGuestAction;

        public RepositoryBackedObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        public void EditGuest(object selectedItem)
        {
            EditGuestAction(selectedItem as Guest);
        }

        public void AddGuest()
        {
            AddGuestAction();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel(RepositoryBackedObservableCollection<Guest> guests, Action<Guest> editGuestAction, Action addGuestAction)
        {
            EditGuestAction = editGuestAction;
            AddGuestAction = addGuestAction;
            Guests = guests;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
