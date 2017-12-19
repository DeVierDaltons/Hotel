using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ViewModel
{
    public class AddRoomViewModel
    {
        public Room room { get; set; } = new Room();

        private int _roomNumber;

        public int RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }
        
    }
}
