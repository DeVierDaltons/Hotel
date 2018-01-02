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
    public class ModifyBookingViewModel
    {
        public List<Booking> Bookings { get; set; }
        public ModifyBookingViewModel(List<Booking> bookings)
        {
            Bookings = bookings;
        }
        
    }
}
