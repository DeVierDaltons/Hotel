using Hotel.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Unity.Attributes;
using Hotel.View;
using Hotel.Proxy;
using System.Collections.Generic;

namespace Hotel.ViewModel
{
    public class GuestsViewModel : INotifyPropertyChanged
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
        
        private ObservableCollection<Guest> _DisplayedGuests;

        public ObservableCollection<Guest> DisplayedGuests
        {
            get { return _DisplayedGuests; }
            set { _DisplayedGuests = value; OnPropertyChanged(); }
        }

        private string _FilterGuestString = "";

        public string FilterGuestString
        {
            get { return _FilterGuestString; }
            set { _FilterGuestString = value.ToLower(); OnPropertyChanged(); FilterGuests(); }
        }

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


        #endregion

        public void StartEditingGuest(object selectedItem)
        {
            if (selectedItem != null)
            {
                var guest = selectedItem as Guest;
                GroupBoxName = string.Format("Editing {0}", guest.FirstName);
                var g = new AddGuestViewModel();
                g.Initialize(new EditGuestCommand(guest), () => { StartAddingGuest(); }, guest, () =>
                {
                    //HotelServiceProxy proxy = new HotelServiceProxy();
                    //proxy.EditGuest(guest);
                    //proxy.Close();
                });
                CurrentGuest = g;
            }
        }

        public void StartAddingGuest()
        {
            
            var guest = new Guest();
            GroupBoxName = "New Guest";
            var g = new AddGuestViewModel();
            g.Initialize(new EditGuestCommand(guest), () => { StartAddingGuest(); }, guest, () =>
            {
                HotelManager.AllGuests.Add(guest);
                FilterGuests();
                StartAddingGuest();
            });
            CurrentGuest = g;
        }

        private bool MatchesFilter(Guest g)
        {
            return    ((g.FirstName != null && g.FirstName.ToLower().Contains(FilterGuestString)) ||
                       (g.LastName != null && g.LastName.ToLower().Contains(FilterGuestString)) ||
                       (g.PhoneNumber != null && g.PhoneNumber.ToLower().Contains(FilterGuestString)) ||
                       (g.EmailAdress != null && g.EmailAdress.ToLower().Contains(FilterGuestString)) ||
                       (g.Address != null && g.Address.ToLower().Contains(FilterGuestString)) ||
                       (g.PostalCode != null && g.PostalCode.ToLower().Contains(FilterGuestString)) ||
                       (g.City != null && g.City.ToLower().Contains(FilterGuestString)) ||
                       (g.Country != null && g.Country.ToLower().Contains(FilterGuestString)) ||
                       (g.ICEPhoneNumber != null && g.ICEPhoneNumber.ToLower().Contains(FilterGuestString)));
        }

        public void FilterGuests()
        {
            DisplayedGuests = new ObservableCollection<Guest>(HotelManager.AllGuests.Where(MatchesFilter));
        }

        public void Initialize()
        {
            FilterGuests();
            HotelManager.AllGuests.CollectionChanged += delegate { FilterGuests(); };
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
