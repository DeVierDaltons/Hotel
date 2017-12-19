using Hotel.Model;
using Hotel.View;
using Hotel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Command
{
    class OpenAddGuestWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        MainWindowViewModel viewModel;
        public OpenAddGuestWindowCommand(MainWindowViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddGuestView view = new AddGuestView();
            view.Show();
            ((AddGuestViewModel)view.DataContext).HotelManager = parameter as HotelManager;
        }
    }
}
