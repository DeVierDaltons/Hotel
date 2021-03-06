﻿using Hotel.ViewModel;
using System;
using System.Windows.Input;

namespace Hotel.Command
{
    public class AddBookingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        AddBookingViewModel viewModel;

        public AddBookingCommand(AddBookingViewModel vm)
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
            viewModel.AddBooking();
        }
    }
}
