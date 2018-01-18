using Hotel.Command;
using Hotel.Model;
using Hotel.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Unity.Attributes;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel : INotifyPropertyChanged, IViewModel
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

        public int Beds
        {
            get { return Room.Beds; }
            set { Room.Beds = value; OnNotifyPropertyChanged(); }
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
        #endregion
        Action Callback;
        public AddRoomViewModel()
        {
          
        }

        public void SetCallback(Action callback)
        {
            this.Callback = callback;
        }

        public bool ValidateInput()
        {
            return !string.IsNullOrEmpty(Room.RoomNumber) && Beds >= 0;
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
            Beds = 1;
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
            Beds = 1;
            AddRoomCommand = new AddRoomCommand(this);
        }
    }
}
