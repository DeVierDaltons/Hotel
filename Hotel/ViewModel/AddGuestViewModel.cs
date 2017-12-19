using Hotel.Command;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class AddGuestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public ICommand AddGuestCommand { get; set; }
        public List<Guest> HotelGuestsList { get; set; } = new List<Guest>();
        public Guest guest { get; set; } = new Guest();
        public string FirstName
        {
            get { return guest.FirstName; }
            set { guest.FirstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get { return guest.LastName; }
            set { guest.LastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        public string PhoneNumber
        {
            get { return guest.PhoneNumber; }
            set { guest.PhoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public string EmailAdress
        {
            get { return guest.EmailAdress; }
            set { guest.EmailAdress = value; OnPropertyChanged(nameof(EmailAdress)); }
        }
        public string ICEPhoneNumber
        {
            get { return guest.ICEPhoneNumber; }
            set { guest.ICEPhoneNumber = value; OnPropertyChanged(nameof(ICEPhoneNumber)); }
        }

        #endregion Properties
        public AddGuestViewModel()
        {
            AddGuestCommand = new AddGuestCommand(this);
        }
        internal bool ValidateInput()
        {
            return true ;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
