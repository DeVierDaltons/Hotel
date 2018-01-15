using Hotel.Command;
using Hotel.Extensions;
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
        private bool _IsSaveButtonEnabled = true;

        public bool IsSaveButtonEnabled
        {
            get { return _IsSaveButtonEnabled; }
            set { _IsSaveButtonEnabled = value; OnPropertyChanged(); }
        }

        public Guest Guest { get; set; }

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

        public string Address
        {
            get { return Guest.Address; }
            set { Guest.Address = value; OnPropertyChanged(); }
        }

        public string PostalCode
        {
            get { return Guest.PostalCode; }
            set { Guest.PostalCode = value; OnPropertyChanged(); }
        }

        public string City
        {
            get { return Guest.City; }
            set { Guest.City = value; OnPropertyChanged(); }
        }

        public string Country
        {
            get { return Guest.Country; }
            set { Guest.Country = value; OnPropertyChanged(); }
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
            Guest = new Guest();
            Guest.CopyDelta(currentGuestData);
            GuestCommand = guestCommand;
            SubmitCommand = new RelayCommand(OnSubmitClicked, (_) => ValidateInput());
            AfterSubmitAction = afterSubmitAction;
        }

        private void OnSubmitClicked(object _)
        {
            GuestCommand.Execute(Guest);
            AfterSubmitAction?.Invoke();
            if (AfterSubmitAction != null)
            {
                IsSaveButtonEnabled = false;
            }
            AfterSubmitAction = null;
        }

        private void ClearAllFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            EmailAdress = string.Empty;
            Address = string.Empty;
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
