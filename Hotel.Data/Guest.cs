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
    [KnownType(typeof(Guest))]
    [DataContract]
    public class Guest : INotifyPropertyChanged, IIdentifiable
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public virtual Guid Id { get; set; }

        private string _firstName;
        [DataMember]
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged();  }
        }
        
        private string _lastName;
        [DataMember]
        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }
        
        private string _phoneNumber;
        /// <summary>
        /// Storing phone number as string to perserve preceding zeros
        /// </summary>
        [DataMember]
        public virtual string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }
        
        private string _emailAdress;
        [DataMember]
        public virtual string EmailAdress
        {
            get { return _emailAdress; }
            set { _emailAdress = value; OnPropertyChanged(); }
        }
        
        private string _address;
        [DataMember]
        public virtual string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }
        private string _postalCode;
        [DataMember]
        public virtual string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; OnPropertyChanged(); }
        }
        
        private string _city;
        [DataMember]
        public virtual string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }
        
        private string _country;
        [DataMember]
        public virtual string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(); }
        }
        
        private string _ICEPhoneNumber;
        [DataMember]
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
