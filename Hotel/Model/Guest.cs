using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Model
{
    public class Guest : INotifyPropertyChanged
    {
        public static readonly int MaxLengthForNames = 100;
        public static readonly int MaxLengthForPhoneNumbers = 20;
        public static readonly int MaxLengthForEmailAddresses = 200;

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual Guid Id { get; set; }

        private string _firstName;
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged();  }
        }

        private string _lastName;
        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;
        /// <summary>
        /// Storing phone number as string to perserve preceding zeros
        /// </summary>
        public virtual string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _emailAdress;
        public virtual string EmailAdress
        {
            get { return _emailAdress; }
            set { _emailAdress = value; OnPropertyChanged(); }
        }

        private Address _address = new Address();

        public virtual Address Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private string _ICEPhoneNumber;
        public virtual string ICEPhoneNumber
        {
            get { return _ICEPhoneNumber; }
            set { _ICEPhoneNumber = value; OnPropertyChanged(); }
        }

        public virtual void CopyFrom(Guest newGuest)
        {
            FirstName = newGuest._firstName;
            LastName = newGuest._lastName;
            PhoneNumber = newGuest._phoneNumber;
            EmailAdress = newGuest._emailAdress;
            Address = newGuest.Address;
            ICEPhoneNumber = newGuest._ICEPhoneNumber;
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        public override string ToString()
        {
            return FirstName;
        }
    }
}
