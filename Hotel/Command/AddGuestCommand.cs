using Hotel.Model;
using Hotel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Command
{
    class AddGuestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        AddGuestViewModel viewModel;
        public AddGuestCommand(AddGuestViewModel vm)
        {
            viewModel = vm;
        }

        /// <summary>
        /// Validates the fields in the add guest window
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return viewModel.ValidateInput();
        }

        /// <summary>
        /// Adds a new guest to the list of guests in the hotel.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            viewModel.HotelGuestsList.Add(viewModel.guest);
        }
    }
}
