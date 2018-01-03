using Hotel.Model;
using System.Collections.ObjectModel;

namespace Hotel.ViewModel
{
    public class ModifyRoomViewModel
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public ModifyRoomViewModel(ObservableCollection<Room> rooms)
        {
            Rooms = rooms;
        }
    }
}
