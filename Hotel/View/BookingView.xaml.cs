﻿using Hotel.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel.View
{
    /// <summary>
    /// Interaction logic for ModifyBooking.xaml
    /// </summary>
    public partial class BookingView : UserControl
    {
        public DataGrid BookingsGrid
        {
            get
            {
                return BookingsList;
            }
        }

        public BookingView()
        {
            InitializeComponent();
        }

        void DataGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as BookingViewModel).RemoveGuestFilter();
        }
        
        private void AddBookingView_Loaded(object sender, RoutedEventArgs e)
        {
            BookingViewModel bookingViewModel = (DataContext as BookingViewModel);
            if (bookingViewModel.AddBookingView == null) {
                var addBookingView = (AddBookingView)sender;
                addBookingView.BookingView = this;
                bookingViewModel.AddBookingView = addBookingView;
                bookingViewModel.SetupAddBookingView();
            }
        }
    }
}
