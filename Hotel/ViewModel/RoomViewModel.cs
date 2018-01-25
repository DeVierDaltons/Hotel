using Hotel.Common;
using Hotel.Model;
using Hotel.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hotel.ViewModel
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        [Unity.Attributes.Dependency]
        public Logger Logger { get; set; }

        private AddRoomViewModel _AddRoomViewDataContext;

        public AddRoomViewModel AddRoomViewDataContext
        {
            get { return _AddRoomViewDataContext; }
            set { _AddRoomViewDataContext = value; OnNotifyPropertyChanged(); }
        }

        private RepositoryBackedObservableCollection<Room> _Rooms;

        public event PropertyChangedEventHandler PropertyChanged;

        [Unity.Attributes.Dependency]
        public RepositoryBackedObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; OnNotifyPropertyChanged(); }
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
                Logger.AddLogMessage("Room added!");
                Rooms.Add(AddRoomViewDataContext.Room);
            });
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
