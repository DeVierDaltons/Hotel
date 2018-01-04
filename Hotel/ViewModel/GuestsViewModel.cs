using Hotel.Command;
using Hotel.Model;
using Hotel.Repository;
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

        private RepositoryBackedObservableCollection<Guest> _guests;

        public RepositoryBackedObservableCollection<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GuestsViewModel(RepositoryBackedObservableCollection<Guest> guests)
        {
            Guests = guests;
            //ShowAddGuestWindowCommand = new ShowGuestDetailWindowCommand(new AddGuestCommand(guests));
        }

        public void ShowEditGuestWindow(object selectedGuest)
        {
            Guest currentGuest = (Guest)selectedGuest;
            //new ShowGuestDetailWindowCommand(new EditGuestCommand(currentGuest), currentGuest).Execute(null);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
