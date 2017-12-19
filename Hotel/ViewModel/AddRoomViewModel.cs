using Hotel.Command;
using Hotel.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public HotelManager HotelManager { get; set; }
        public ICommand AddRoomCommand { get; set; }
        public Room room { get; set; } = new Room();

        public string RoomNumber
        {
            get { return room.RoomNumber; }
            set { room.RoomNumber = value; OnNotifyPropertyChanged(); }
        }

        public int Beds
        {
            get { return room.Beds; }
            set { room.Beds = value; OnNotifyPropertyChanged(); }
        }

        public RoomQuality Quality
        {
            get { return room.Quality; }
            set { room.Quality = value; OnNotifyPropertyChanged(); }
        }

        public bool HasNiceView
        {
            get { return room.HasNiceView; }
            set { room.HasNiceView = value; OnNotifyPropertyChanged(); }
        }

        public Decimal PricePerDay
        {
            get { return room.PricePerDay; }
            set { room.PricePerDay = value; OnNotifyPropertyChanged(); }
        }
        #endregion

        public AddRoomViewModel()
        {
           AddRoomCommand = new AddRoomCommand(this);
        }

        public bool ValidateInput()
        {
            return !string.IsNullOrEmpty(room.RoomNumber);
        }

        public void AddRoom()
        {
            HotelManager.Rooms.Add(room);
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
