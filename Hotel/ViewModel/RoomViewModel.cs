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

        private ObservableCollection<Room> _Rooms;
        
        public ObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; OnNotifyPropertyChanged(); }
        }

        public void Initialize()
        {
            CallbackOperations<Room> callback = new CallbackOperations<Room>(Rooms);
            var p = new HotelServiceProxy(new System.ServiceModel.InstanceContext(callback));
            _Rooms = p.GetAllRooms();
            p.Close();
            AddRoom();
        }
        
        public void AddRoom()
        {
            AddRoomViewDataContext = new AddRoomViewModel(Rooms);
            AddRoomViewDataContext.Initialize();
            AddRoomViewDataContext.SetCallback(() =>
            {
                CallbackOperations<Room> callback = new CallbackOperations<Room>(Rooms);
                HotelServiceProxy proxy = new HotelServiceProxy(new System.ServiceModel.InstanceContext(callback));
                Task.Run(() =>
                {
                    proxy.AddRoom(AddRoomViewDataContext.Room);
                    proxy.Close();
                });
            });
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
