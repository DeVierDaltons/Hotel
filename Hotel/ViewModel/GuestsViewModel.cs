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
            set { _FilterGuestString = value.ToLower(); OnPropertyChanged(); FilterGuests(); }
        }


        public RepositoryBackedObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        public void EditGuest(object selectedItem, System.Windows.Controls.StackPanel f)
        {
            var item = selectedItem as Guest;
            if (item == null)
            {
                var newGuest = new Guest();
                Guests.Add(newGuest);
                item = newGuest;
            }
            var GuestDetailView = new View.GuestDetailView();
            GuestDetailView.DataContext = new GuestDetailViewModel(new EditGuestCommand(item), item, null);
            f.Children.Clear();
            f.Children.Add(GuestDetailView);
        }
        public void AddGuest(System.Windows.Controls.StackPanel f)
        {
            var newGuest = new Guest();
            Guests.Add(newGuest);
            var GuestDetailView = new View.GuestDetailView();
            GuestDetailView.DataContext = new GuestDetailViewModel(new EditGuestCommand(newGuest), newGuest, null);
            f.Children.Clear();
            f.Children.Add(GuestDetailView);
        }
        public void FilterGuests()
        {
            DisplayedGuests = new ObservableCollection<Guest>(Guests.Where(g =>
               (g.FirstName != null && g.FirstName.ToLower().Contains(FilterGuestString)) ||
               (g.LastName != null && g.LastName.ToLower().Contains(FilterGuestString)) ||
               (g.PhoneNumber != null && g.PhoneNumber.ToLower().Contains(FilterGuestString)) ||
               (g.PostalCode != null && g.PostalCode.ToLower().Contains(FilterGuestString)) ||
               (g.EmailAdress != null && g.EmailAdress.ToLower().Contains(FilterGuestString)) ||
               (g.City != null && g.City.ToLower().Contains(FilterGuestString)) ||
               (g.Country != null && g.Country.ToLower().Contains(FilterGuestString))));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel(RepositoryBackedObservableCollection<Guest> guests)
        {
            Guests = guests;
            DisplayedGuests = guests;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
