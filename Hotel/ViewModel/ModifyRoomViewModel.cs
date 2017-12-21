using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    public class ModifyRoomViewModel
    {
        public HotelManager HotelManager { get; set; } = new HotelManager();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ModifyRoomViewModel()
        {
          
        }
    }
}
