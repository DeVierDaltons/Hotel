using Hotel.Model;
using Hotel.Repository;
using Hotel.View;
using System.Collections.ObjectModel;
using Unity.Attributes;

namespace Hotel.ViewModel
{
    public class RoomViewModel : IViewModel
    {
        private AddRoomView _AddRoomView;

        public AddRoomView AddRoomView
        {
            get { return _AddRoomView; }
            set { _AddRoomView = value; }
        }

        public ObservableCollection<Room> Rooms { get; set; }
        public RoomViewModel([Dependency("RoomRepository")] RepositoryBackedObservableCollection<Room> rooms)
        {
            Rooms = rooms;
        }
    }
}
