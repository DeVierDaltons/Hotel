using Hotel.Model;
using Hotel.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Unity.Attributes;
using Hotel.View;

namespace Hotel.ViewModel
{
    public class GuestsViewModel : INotifyPropertyChanged, IViewModel
    {
        #region Properties
        private string _groupBoxName;

        public string GroupBoxName
        {
            get { return _groupBoxName; }
            set { _groupBoxName = value; OnPropertyChanged(); }
        }

        private Hotel.ViewModel.AddGuestViewModel _currentGuest;
        public AddGuestViewModel CurrentGuest
        {
            get { return _currentGuest; }
            set { _currentGuest = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private RepositoryBackedObservableCollection<Guest> _guests;

        [Unity.Attributes.Dependency]
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
      

        private Guest _SelectedGuest;

        public Guest SelectedGuest
        {
            get { return _SelectedGuest; }
            set { _SelectedGuest = value; OnPropertyChanged(); HasSelectedGuest = true; }
        }

        private bool _HasSelectedGuest;
        public bool HasSelectedGuest
        {
            get { return _HasSelectedGuest; }
            set { _HasSelectedGuest = value; OnPropertyChanged(); }
        }

        private Action _SwitchToBookingTab;
        public Action SwitchToBookingTab
        {
            get { return _SwitchToBookingTab; }
            set { _SwitchToBookingTab = value; }
        }


        public void ViewBookingsForGuest()
        {
        }

        public void StartEditingGuest(object selectedItem)
        {
            var guest = selectedItem as Guest;
            GroupBoxName = string.Format("Editing {0}",guest.FirstName);
            CurrentGuest = new AddGuestViewModel();
            CurrentGuest.Initialize(new EditGuestCommand(guest), () => { StartAddingGuest(); }, guest, null);
        }

        public void StartAddingGuest()
        {
            var guest = new Guest();
            GroupBoxName = "New Guest";
            CurrentGuest = new AddGuestViewModel();
            CurrentGuest.Initialize(new EditGuestCommand(guest), () => { StartAddingGuest(); }, guest, () =>
            {
                Guests.Add(guest);
                StartAddingGuest();
            });
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


        public GuestsViewModel()
        {
        }

        /// <summary>
        /// DO NOT REMOVE, has to be done after the dependencies have been injected, so NOT in the constructor.
        /// </summary>
        public void Initialize()
        {
            DisplayedGuests = Guests;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
