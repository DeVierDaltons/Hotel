﻿using Hotel.Model;
using Hotel.Repository;
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

        private RepositoryBackedObservableCollection<Guest> Guests;

        public AddGuestCommand(RepositoryBackedObservableCollection<Guest> guests)
        {
            Guests = guests;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Guests.Add(parameter as Guest);
        }
    }
}
