﻿using Hotel.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for GuestsViewModel.xaml
    /// </summary>
    public partial class GuestsView : UserControl
    {
        public GuestsView()
        {
            InitializeComponent();
        }

        private void OnEditGuestClicked(object sender, RoutedEventArgs e)
        {
            (DataContext as GuestsViewModel).EditGuest(GuestsList.SelectedItem, GuestDetail);
        }

        private void OnAddGuestClicked(object sender, RoutedEventArgs e)
        {

            //(DataContext as GuestsViewModel).AddGuest();
        }
    }
}
