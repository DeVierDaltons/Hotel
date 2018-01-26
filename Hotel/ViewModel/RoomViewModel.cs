using Hotel.Callback;
using Hotel.Data;
using Hotel.Proxy;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

        public ObservableCollection<Room> Rooms
        {
            get { return HotelManager.AllRooms; }
        }

        public void Initialize()
        {
            AddRoom();
        }
        
        public void AddRoom()
        {
            AddRoomViewDataContext = new AddRoomViewModel(Rooms);
            AddRoomViewDataContext.Initialize();
            AddRoomViewDataContext.SetCallback(() =>
            {
                Task.Run(() =>
                {
                    HotelManager.RoomCallbackProxy.Add(AddRoomViewDataContext.Room);
                });
            });
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
