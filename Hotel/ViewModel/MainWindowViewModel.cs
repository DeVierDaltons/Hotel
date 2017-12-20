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
        public HotelManager HotelManager { get; set; }
        public ICommand ShowAddGuestWindowCommand { get; }
        public ICommand ShowGuestsWindow { get; }
        public MainWindowViewModel()
        {
            HotelManager = new HotelManager();
            ShowAddGuestWindowCommand = new Command.OpenAddGuestWindowCommand(HotelManager);
            ShowGuestsWindow = new Command.ShowGuestsWindowCommand(HotelManager);
        }
    }
}
