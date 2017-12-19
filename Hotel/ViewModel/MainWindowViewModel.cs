using Hotel.Command;
using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    public class MainWindowViewModel
    {
        private HotelManager HotelManager = new HotelManager();
        public ICommand ShowAddGuestWindowCommand { get; }

        public MainWindowViewModel()
        {
            ShowAddGuestWindowCommand = new OpenAddGuestWindowCommand(HotelManager);
        }
    }
}
