﻿using Hotel.Model;
using Hotel.View;
using Hotel.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hotel.Command
{
    class ShowAddGuestWindowCommand : ICommand
    {
        private ObservableCollection<Guest> Guests;

        public ShowAddGuestWindowCommand(ObservableCollection<Guest> guests)
        {
            Guests = guests;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            AddGuestView view = new AddGuestView();
            ((AddGuestViewModel)view.DataContext).Guests = Guests;
            view.Show();
        }
    }
}
