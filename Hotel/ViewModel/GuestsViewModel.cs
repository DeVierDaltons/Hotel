using Hotel.Model;
using Hotel.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hotel.ViewModel
{
    public class GuestsViewModel : INotifyPropertyChanged
    {
        private RepositoryBackedObservableCollection<Guest> _guests;
        private Action<Guest> EditGuestAction;
        private Action AddGuestAction;
        private string _FilterGuestString;

        private ObservableCollection<Guest> _DisplayedGuests;

        public ObservableCollection<Guest> DisplayedGuests
        {
            get { return _DisplayedGuests; }
            set { _DisplayedGuests = value; OnPropertyChanged(); }
        }


        public string FilterGuestString
        {
            get { return _FilterGuestString; }
            set { _FilterGuestString = value; OnPropertyChanged(); FilterGuests(); }
        }


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

        public void FilterGuests()
        {
            DisplayedGuests = new ObservableCollection<Guest>(Guests.Where(g =>
               (g.FirstName != null && g.FirstName.Contains(FilterGuestString)) || 
               (g.LastName != null && g.LastName.Contains(FilterGuestString)) ||
               (g.PhoneNumber != null && g.PhoneNumber.Contains(FilterGuestString)) || 
               (g.PostalCode != null && g.PostalCode.Contains(FilterGuestString)) ||
               (g.EmailAdress != null && g.EmailAdress.Contains(FilterGuestString)) ||
               (g.Country !=null && g.Country.Contains(FilterGuestString))));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel(RepositoryBackedObservableCollection<Guest> guests, Action<Guest> editGuestAction, Action addGuestAction)
        {
            EditGuestAction = editGuestAction;
            AddGuestAction = addGuestAction;
            Guests = guests;
            DisplayedGuests = guests;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
