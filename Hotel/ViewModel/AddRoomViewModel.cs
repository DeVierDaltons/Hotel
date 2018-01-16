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
        public RepositoryBackedObservableCollection<Room> Rooms { get; set; }
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

        public AddRoomViewModel([Unity.Attributes.Dependency("RoomRepository")] RepositoryBackedObservableCollection<Room> rooms)
        {
            Rooms = rooms;
            Beds = 1;
            AddRoomCommand = new AddRoomCommand(this);
        }

        public bool ValidateInput()
        {
            return !string.IsNullOrEmpty(Room.RoomNumber);
        }

        public void AddRoom()
        {
            Rooms.Add(Room);
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
    }
}
