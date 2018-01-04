using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class GuestDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Action AfterSubmitAction;

        #region Properties
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

        public string Adress
        {
            get { return Guest.Address.Street; }
            set { Guest.Address.Street = value; OnPropertyChanged(); }
        }

        public string PostalCode
        {
            get { return Guest.Address.PostalCode; }
            set
            {
                Guest.Address.PostalCode = value; OnPropertyChanged();
            }
        }

        public string City
        {
            get { return Guest.Address.City; }
            set
            {
                Guest.Address.City = value; OnPropertyChanged();
            }
        }

        public string Country
        {
            get { return Guest.Address.Country; }
            set { Guest.Address.Country = value; OnPropertyChanged(); }
        }

        public string ICEPhoneNumber
        {
            get { return Guest.ICEPhoneNumber; }
            set { Guest.ICEPhoneNumber = value; OnPropertyChanged(); }
        }

        public ICommand SubmitCommand { get; set; }

        private ICommand GuestCommand;

        #endregion Properties

        public GuestDetailViewModel(ICommand guestCommand, Guest currentGuestData, Action afterSubmitAction)
        {
            if (currentGuestData != null)
            {
                Guest.CopyFrom(currentGuestData);
            }
            GuestCommand = guestCommand;
            SubmitCommand = new RelayCommand(OnSubmitClicked, (_) => ValidateInput());
            AfterSubmitAction = afterSubmitAction;
        }

        private void OnSubmitClicked(object _)
        {
            GuestCommand.Execute(Guest);
            Guest = new Guest();
            ClearAllFields();
            AfterSubmitAction();
        }

        private void ClearAllFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            EmailAdress = string.Empty;
            Adress = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            ICEPhoneNumber = string.Empty;
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

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
