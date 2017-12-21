using Hotel.Model;
using Hotel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel.Command
{
    public class AddGuestCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private ObservableCollection<Guest> Guests;

        public AddGuestCommand(ObservableCollection<Guest> guests)
        {
            Guests = guests;
        }

        /// <summary>
        /// Validates the fields in the add guest window
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Adds a new guest to the list of guests in the hotel.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Guests.Add(parameter as Guest);
        }
    }
}
