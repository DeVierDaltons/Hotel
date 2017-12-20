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
    public class GuestsViewModel : INotifyPropertyChanged
    {
        public ICommand ShowAddGuestWindowCommand { get; }

        private ObservableCollection<Guest> _guests;

        public ObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        public ICommand AddGuestToHotelManagerList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel(ObservableCollection<Guest> guests)
        {
            Guests = guests;
            AddGuestToHotelManagerList = new AddGuestToHotelManagerList(this);
            ShowAddGuestWindowCommand = new ShowAddGuestWindowCommand(Guests);
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
