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
        public HotelManager HotelManager { get; set; }
        public ObservableCollection<Room> rooms { get; set; }
        public ModifyRoomViewModel()
        {
        }
        
        
    }
}
