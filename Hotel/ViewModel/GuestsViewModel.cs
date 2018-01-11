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
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private RepositoryBackedObservableCollection<Guest> _guests;

        public RepositoryBackedObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Guest> _DisplayedGuests;

        public ObservableCollection<Guest> DisplayedGuests
        {
            get { return _DisplayedGuests; }
            set { _DisplayedGuests = value; OnPropertyChanged(); }
        }

        private string _FilterGuestString;

        public string FilterGuestString
        {
            get { return _FilterGuestString; }
            set { _FilterGuestString = value.ToLower(); OnPropertyChanged(); FilterGuests(); }
        }
        #endregion

        public void EditGuest(object selectedGuest, System.Windows.Controls.StackPanel stackpanel)
        {
            var guest = selectedGuest as Guest;
            if(guest == null)
            {
                guest = new Guest();
                Guests.Add(guest);
            }
            var GuestDetailView = new View.GuestDetailView();
            GuestDetailView.DataContext = new GuestDetailViewModel(new EditGuestCommand(guest), guest, null);            
            stackpanel.Children.Clear();
            stackpanel.Children.Add(GuestDetailView);
        }
        public void AddGuest(System.Windows.Controls.StackPanel stackpanel)
        {
            EditGuest(null,stackpanel);
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
