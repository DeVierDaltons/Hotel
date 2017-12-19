using Hotel.Model;
using System;
using System.ComponentModel;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel : INotifyPropertyChanged
    {
        public Room room { get; set; } = new Room();

        public event PropertyChangedEventHandler PropertyChanged;

        public string RoomNumber
        {
            get { return room.RoomNumber; }
            set { room.RoomNumber = value; OnNotifyPropertyChanged(nameof(RoomNumber)); }
        }

        public int Beds
        {
            get { return room.Beds; }
            set { room.Beds = value; OnNotifyPropertyChanged(nameof(Beds)); }
        }

        public RoomQuality Quality
        {
            get { return room.Quality; }
            set { room.Quality = value; OnNotifyPropertyChanged(nameof(Quality)); }
        }

        public bool HasNiceView
        {
            get { return room.HasNiceView; }
            set { room.HasNiceView = value; OnNotifyPropertyChanged(nameof(HasNiceView)); }
        }

        public Decimal PricePerDay
        {
            get { return room.PricePerDay; }
            set { room.PricePerDay = value; OnNotifyPropertyChanged(nameof(PricePerDay)); }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
