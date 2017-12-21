using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class Guest
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _phoneNumber;

        /// <summary>
        /// Storing phone number as string to perserve preceding zeros
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private string _emailAdress;

        public string EmailAdress
        {
            get { return _emailAdress; }
            set { _emailAdress = value; }
        }
        private string _ICEPhoneNumber;

        public string ICEPhoneNumber
        {
            get { return _ICEPhoneNumber; }
            set { _ICEPhoneNumber = value; }
        }

        public void CopyFrom(Guest newGuest)
        {
            _firstName = newGuest._firstName;
            _lastName = newGuest._lastName;
            _phoneNumber = newGuest._phoneNumber;
            _emailAdress = newGuest._emailAdress;
            _ICEPhoneNumber = newGuest._ICEPhoneNumber;
        }
    }
}
