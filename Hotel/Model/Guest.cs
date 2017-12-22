﻿using System;
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
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged();  }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;

        /// <summary>
        /// Storing phone number as string to perserve preceding zeros
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _emailAdress;

        public string EmailAdress
        {
            get { return _emailAdress; }
            set { _emailAdress = value; OnPropertyChanged(); }
        }
        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set { _adress = value; OnPropertyChanged(); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; OnPropertyChanged(); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(); }
        }

        private string _ICEPhoneNumber;

        public string ICEPhoneNumber
        {
            get { return _ICEPhoneNumber; }
            set { _ICEPhoneNumber = value; OnPropertyChanged(); }
        }

        public void CopyFrom(Guest newGuest)
        {
            FirstName = newGuest._firstName;
            LastName = newGuest._lastName;
            PhoneNumber = newGuest._phoneNumber;
            EmailAdress = newGuest._emailAdress;
            Adress = newGuest.Adress;
            PostalCode = newGuest.PostalCode;
            City = newGuest.City;
            Country = newGuest.Country;
            ICEPhoneNumber = newGuest._ICEPhoneNumber;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
