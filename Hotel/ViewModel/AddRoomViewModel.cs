using Hotel.Command;
using Hotel.Model;
using Hotel.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Unity.Attributes;
using System.Collections.Generic;
using Hotel.UIValidators;
using System.Globalization;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public ICommand AddRoomCommand { get; set; }
        public Room Room { get; set; } = new Room();

        public string RoomNumber
        {
            get { return Room.RoomNumber; }
            set { Room.RoomNumber = value; OnNotifyPropertyChanged(); }
        }

        private string _beds;
        public string Beds
        {
            get { return _beds; }
            set {
                int numBeds = -1;
                int.TryParse(value, out numBeds);
                Room.Beds = numBeds;
                _beds = numBeds.ToString();
                OnNotifyPropertyChanged();
            }
        }

        public RoomQuality Quality
        {
            get { return Room.Quality; }
            set { Room.Quality = value; OnNotifyPropertyChanged(); }
        }

        public bool HasNiceView
        {
            get { return Room.HasNiceView; }
            set { Room.HasNiceView = value; OnNotifyPropertyChanged(); }
        }

        public Decimal PricePerDay
        {
            get { return Room.PricePerDay; }
            set { Room.PricePerDay = value; OnNotifyPropertyChanged(); }
        }

        public IEnumerable<Room> Rooms { get; private set; }

        #endregion
        Action Callback;
        public AddRoomViewModel(IEnumerable<Room> rooms)
        {
            Rooms = rooms;
        }

        public void SetCallback(Action callback)
        {
            this.Callback = callback;
        }

        private bool ValidateBeds()
        {
            RoomBedsValidator bedsValidator = new RoomBedsValidator();
            return bedsValidator.Validate(Beds, CultureInfo.CurrentCulture).IsValid;
        }

        public bool ValidateInput()
        {
            return !string.IsNullOrEmpty(Room.RoomNumber) && ValidateBeds();
        }

        public void AddRoom()
        {
            Callback?.Invoke();
            Room = new Room();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            RoomNumber = string.Empty;
            Beds = "";
            Quality = RoomQuality.Budget;
            HasNiceView = false;
            PricePerDay = 0;
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        public void Initialize()
        {
            Beds = "";
            AddRoomCommand = new AddRoomCommand(this);
        }
    }
}
