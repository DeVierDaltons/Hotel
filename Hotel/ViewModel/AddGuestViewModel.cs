using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    class AddGuestViewModel
    {
        public Guest guest { get; set; } = new Guest();

        #region Properties
        public string FirstName
        {
            get { return guest.FirstName; }
            set { guest.FirstName = value; }
        }

        public string LastName
        {
            get { return guest.LastName; }
            set { guest.LastName = value; }
        }

        public string PhoneNumber
        {
            get { return guest.PhoneNumber; }
            set { guest.PhoneNumber = value; }
        }

        public string EmailAdress
        {
            get { return guest.EmailAdress; }
            set { guest.EmailAdress = value; }
        }
        public string ICEPhoneNumber
        {
            get { return guest.ICEPhoneNumber; }
            set { guest.ICEPhoneNumber = value; }
        }

        #endregion Properties
        public AddGuestViewModel()
        {
        }

    }
}
