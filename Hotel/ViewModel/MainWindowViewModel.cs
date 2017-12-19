using Hotel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.ViewModel
{
    class MainWindowViewModel
    {
        public ICommand ShowAddGuestWindowCommand { get; }
        public MainWindowViewModel()
        {
            ShowAddGuestWindowCommand = new Command.OpenAddGuestWindowCommand();
        }
    }
}
