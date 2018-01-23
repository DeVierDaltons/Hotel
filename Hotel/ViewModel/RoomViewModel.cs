using Hotel.Data;
using Hotel.Proxy;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hotel.ViewModel
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        private AddRoomViewModel _AddRoomViewDataContext;

        public AddRoomViewModel AddRoomViewDataContext
        {
            get { return _AddRoomViewDataContext; }
            set { _AddRoomViewDataContext = value; OnNotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Room> _Rooms;
        
        public ObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; OnNotifyPropertyChanged(); }
        }

        public void Initialize()
        {
            _Rooms = new HotelServiceProxy().GetAllRooms();
            AddRoom();
        }

        public void AddRoom()
        {
            AddRoomViewDataContext = new AddRoomViewModel();
            AddRoomViewDataContext.Initialize();
            AddRoomViewDataContext.SetCallback(() =>
            {
                new HotelServiceProxy().AddRoom(AddRoomViewDataContext.Room);
                Rooms.Add(AddRoomViewDataContext.Room);
            });
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
