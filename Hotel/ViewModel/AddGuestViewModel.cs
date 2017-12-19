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
        public AddGuestCommand AddGuestCommand { get; set; }
        public Guest Guest { get; set; } = new Guest();

        public string FirstName
        {
            get { return Guest.FirstName; }
            set { Guest.FirstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get { return Guest.LastName; }
            set { Guest.LastName = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get { return Guest.PhoneNumber; }
            set { Guest.PhoneNumber = value; OnPropertyChanged(); }
        }

        public string EmailAdress
        {
            get { return Guest.EmailAdress; }
            set { Guest.EmailAdress = value; OnPropertyChanged(); }
        }
        public string ICEPhoneNumber
        {
            get { return Guest.ICEPhoneNumber; }
            set { Guest.ICEPhoneNumber = value; OnPropertyChanged(); }
        }
        #endregion Properties

        public AddGuestViewModel()
        {
           AddGuestCommand  = new AddGuestCommand(this);
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
            HotelManager.Guests.Add(Guest);
        }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
