using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hotel.View;

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
