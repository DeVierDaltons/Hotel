using Hotel.Command;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class GuestsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Guest> _guests;

        public ObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }


        public ICommand AddGuestToHotelManagerList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel()
        {
            AddGuestToHotelManagerList = new AddGuestToHotelManagerList(this);
        }

        public void AddGuest()
        {
            var guest = new Guest();
            Guests.Add(guest);
        }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
