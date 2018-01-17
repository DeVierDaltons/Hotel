using Hotel.Model;
using Hotel.Repository;
using Hotel.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Attributes;

namespace Hotel.ViewModel
{
    public class RoomViewModel : IViewModel, INotifyPropertyChanged
    {
       

        private AddRoomViewModel _AddRoomViewDataContext;

        public AddRoomViewModel AddRoomViewDataContext
        {
            get { return _AddRoomViewDataContext; }
            set { _AddRoomViewDataContext = value; OnNotifyPropertyChanged(); }
        }


        private RepositoryBackedObservableCollection<Room> _Rooms;

        public event PropertyChangedEventHandler PropertyChanged;

        [Unity.Attributes.Dependency("RoomRepository")]
        public RepositoryBackedObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; OnNotifyPropertyChanged(); }
        }
        public RoomViewModel()
        {
            AddRoom();
        }

        public void AddRoom()
        {
            AddRoomViewDataContext = new AddRoomViewModel();
            AddRoomViewDataContext.SetCallback(() =>
            {
                Rooms.Add(AddRoomViewDataContext.Room);
            });
        }

        private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
