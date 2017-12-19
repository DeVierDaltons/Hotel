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
        public HotelManager HotelManager { get; set; }
        public ICommand AddGuestCommand { get; set; }
        public Guest guest { get; set; } = new Guest();

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
            if (String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(PhoneNumber))
            {
                return false;
            }
            return true;
        }

        public void AddGuest()
        {
            HotelManager.Guests.Add(guest);
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
