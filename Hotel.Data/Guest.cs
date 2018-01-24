using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Data
{
    [DataContract]
    public class Guest : INotifyPropertyChanged, IIdentifiable
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public virtual Guid Id { get; set; }

        [DataMember]
        private string _firstName;
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged();  }
        }

        [DataMember]
        private string _lastName;
        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }
        
        [DataMember]
        private string _phoneNumber;
        /// <summary>
        /// Storing phone number as string to perserve preceding zeros
        /// </summary>
        public virtual string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _emailAdress;
        public virtual string EmailAdress
        {
            get { return _emailAdress; }
            set { _emailAdress = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _address;
        public virtual string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _postalCode;
        public virtual string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _city;
        public virtual string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _country;
        public virtual string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(); }
        }

        [DataMember]
        private string _ICEPhoneNumber;
        public virtual string ICEPhoneNumber
        {
            get { return _ICEPhoneNumber; }
            set { _ICEPhoneNumber = value; OnPropertyChanged(); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
