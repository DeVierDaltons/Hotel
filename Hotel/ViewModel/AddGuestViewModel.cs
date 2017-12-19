using Hotel.Command;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class AddGuestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        private ICommand AddGuestCommand { get; set; }
        public List<Guest> HotelGuestsList { get; set; } = new List<Guest>();
        public Guest guest { get; private set; } = new Guest();
        public string FirstName
        {
            get { return guest.FirstName; }
            set { guest.FirstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get { return guest.LastName; }
            set { guest.LastName = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get { return guest.PhoneNumber; }
            set { guest.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string EmailAdress
        {
            get { return guest.EmailAdress; }
            set { guest.EmailAdress = value; OnPropertyChanged(); }
        }
        public string ICEPhoneNumber
        {
            get { return guest.ICEPhoneNumber; }
            set { guest.ICEPhoneNumber = value; OnPropertyChanged(); }
        }

        #endregion Properties

        public AddGuestViewModel()
        {
            AddGuestCommand = new AddGuestCommand(this);
        }

        /// <summary>
        /// Checks if the userinput is correct
        /// </summary>
        /// <returns></returns>
        public bool ValidateInput()
        {
            if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(PhoneNumber) || String.IsNullOrEmpty(EmailAdress) || String.IsNullOrEmpty(ICEPhoneNumber))
            {
                return false;
            }
            return true ;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            AddGuestCommand.CanExecute(null);
        }
    }
}
