using Hotel.Model;
using Hotel.View;
using System.Collections.ObjectModel;

namespace Hotel.ViewModel
{
    public class RoomViewModel
    {
        private AddRoomView _AddRoomView;

        public AddRoomView AddRoomView
        {
            get { return _AddRoomView; }
            set { _AddRoomView = value; }
        }

        public ObservableCollection<Room> Rooms { get; set; }
        public RoomViewModel(ObservableCollection<Room> rooms)
        {
            Rooms = rooms;
        }
    }
}
