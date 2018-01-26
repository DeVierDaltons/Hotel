using Hotel.Command;
using Hotel.Data;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Unity.Attributes;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel : INotifyPropertyChanged
    {
        private const int MaximumRoomNumberLength = 15;
        private const int MinimumBeds = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public ICommand AddRoomCommand { get; set; }
        public Room Room { get; set; } = new Room();

        public string RoomNumber
        {
            get { return Room.RoomNumber; }
            set { Room.RoomNumber = value; OnNotifyPropertyChanged(); }
        }

        string _roomNrErrorMessage;
        public string RoomNrErrorMessage
        {
            get { return _roomNrErrorMessage; }
            set { _roomNrErrorMessage = value; OnNotifyPropertyChanged(); }
        }

        string _roomBedsErrorMessage;
        public string RoomBedsErrorMessage
        {
            get { return _roomBedsErrorMessage; }
            set { _roomBedsErrorMessage = value; OnNotifyPropertyChanged(); }
        }

        private string _beds;
        public string Beds
        {
            get { return _beds; }
            set {
                int numBeds;
                if( int.TryParse(value, out numBeds))
                {
                    Room.Beds = numBeds;
                }
                _beds = value;
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

        public bool ValidateInput()
        {
            bool roomNumberValid = ValidateRoomNumber();
            bool bedsValid = ValidateBeds();
            return roomNumberValid && bedsValid;
        }

        private bool ValidateRoomNumber()
        {
            RoomNrErrorMessage = "";
            if (string.IsNullOrEmpty(Room.RoomNumber) || string.IsNullOrWhiteSpace(Room.RoomNumber))
            {
                RoomNrErrorMessage = "Room Number cannot be empty.";
                return false;
            }
            else if (Room.RoomNumber.Length > MaximumRoomNumberLength)
            {
                RoomNrErrorMessage = $"Room Number must be less than {MaximumRoomNumberLength} characters.";
                return false;
            }
            else if (Rooms.Any(room => room.RoomNumber == Room.RoomNumber))
            {
                RoomNrErrorMessage = "Already have a room with that name.";
                return false;
            }
            return true;
        }

        private bool ValidateBeds()
        {
            RoomBedsErrorMessage = "";
            if (string.IsNullOrEmpty(Beds))
            {
                RoomBedsErrorMessage = "Beds cannot be empty.";
                return false;
            }
            else
            {
                int numOfBeds;
                if (!int.TryParse(Beds, out numOfBeds))
                {
                    RoomBedsErrorMessage = $"{Beds} is not a valid integer.";
                    return false;
                }
                else if (numOfBeds < MinimumBeds)
                {
                    RoomBedsErrorMessage = $"Must have at least {MinimumBeds} beds.";
                    return false;
                }
            }
            return true;
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
