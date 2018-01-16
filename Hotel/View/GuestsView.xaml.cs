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
            (DataContext as GuestsViewModel).EditGuest(GuestsList.SelectedItem);
        }

        private void OnAddGuestClicked(object sender, RoutedEventArgs e)
        {
            (DataContext as GuestsViewModel).AddGuest();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as GuestsViewModel).AddGuest();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as GuestsViewModel).SwitchToBookingTab.Invoke();
        }
    }
}
