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
    public class Address : INotifyPropertyChanged
    {
        private string _street;
        public virtual string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged(); }
        }

        private string _postalCode;
        public virtual string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; OnPropertyChanged(); }
        }

        private string _city;
        public virtual string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }

        private string _country;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
