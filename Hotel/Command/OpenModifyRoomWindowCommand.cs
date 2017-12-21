using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Model;
using System.Windows.Input;
using Hotel.View;
using Hotel.ViewModel;

namespace Hotel.Command
{
    public class OpenModifyRoomWindowCommand : ICommand
    {
        private HotelManager hotelManager;

        public OpenModifyRoomWindowCommand(HotelManager hotelManager)
        {
            this.hotelManager = hotelManager;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ModifyRoom view = new ModifyRoom();
            ModifyRoomViewModel viewmodel = new ModifyRoomViewModel();
            viewmodel.HotelManager = hotelManager;
            view.DataContext = viewmodel;
            view.Show();
        }
    }
}
