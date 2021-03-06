﻿using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hotel.View;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hotel.Repository;
using NHibernate.Util;

namespace Hotel.ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        private AddBookingView _addBookingView;

        public AddBookingView AddBookingView
        {
            get { return _addBookingView; }
            set { _addBookingView = value; OnPropertyChanged(); }
        }

        private AddBookingViewModel _AddBookingViewDataContext;

        public AddBookingViewModel AddBookingViewDataContext
        {
            get { return _AddBookingViewDataContext; }
            set { _AddBookingViewDataContext = value; OnPropertyChanged(); }
        }

        private Guest filterGuest;

        private ObservableCollection<Booking> _displayedBookings = new ObservableCollection<Booking>();

        public ObservableCollection<Booking> DisplayedBookings
        {
            get { return _displayedBookings; }
            set { _displayedBookings = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public List<BookingStatus> BookingStatusFilters { get;} = new List<BookingStatus>();

        [Unity.Attributes.Dependency]
        public RepositoryBackedObservableCollection<Booking> Bookings { get; set; }

        private RepositoryBackedObservableCollection<Room> _Rooms;

        [Unity.Attributes.Dependency]
        public RepositoryBackedObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set { _Rooms = value; OnPropertyChanged(); }
        }

        [Unity.Attributes.Dependency]
        public RepositoryBackedObservableCollection<Guest> Guests { get; set; }
        public BookingViewModel()
        { 
            InitializeStatusFilterList();
            IsRemoveFilterButtonVisible = Visibility.Hidden;
        }

        public void Initialize()
        {
            foreach (Booking booking in Bookings)
            {
                booking.PropertyChanged += InvalidateOnBookingStatusChanged;
            }
            Bookings.CollectionChanged += Bookings_CollectionChanged;
            FilterDisplayedBookings();
            SetupAddBookingViewModel();
        }

        public void SetupAddBookingViewModel()
        {
            AddBookingViewModel viewModel = new AddBookingViewModel();
            viewModel.AllBookings = Bookings;
            viewModel.AllRooms = Rooms;
            viewModel.AllGuests = Guests;
            viewModel.Initialize();
            AddBookingViewDataContext = viewModel;
        }

        public void SetupAddBookingView()
        {
            AddBookingView.DataContext = AddBookingViewDataContext;
            AddBookingView.Initialize();
        }

        public Dictionary<BookingStatus, Func<bool>> StatusFiltersList = new Dictionary<BookingStatus, Func<bool>>();
          

        private void InitializeStatusFilterList()
        {
            StatusFiltersList.Add(BookingStatus.Cancelled, () => { return ShowCancelledFilter; });
            StatusFiltersList.Add(BookingStatus.CheckedIn, () => { return ShowCheckedInFilter; });
            StatusFiltersList.Add(BookingStatus.CheckedOut, () => { return ShowCheckedOutFilter; });
            StatusFiltersList.Add(BookingStatus.NoShow, () => { return ShowNoShowFilter; });
            StatusFiltersList.Add(BookingStatus.Reserved, () => { return ShowReservedFilter; });
        }

        /// <summary>
        /// If an item gets added to the original list of bookings also add it to the displayed list of bookings.
        /// Or remove it if the booking is removed from the original list of bookings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems.Count > 0) {
                var newBooking = (e.NewItems[0] as Booking);
                newBooking.PropertyChanged += InvalidateOnBookingStatusChanged;
                if( ShouldShowBooking(newBooking))
                {
                    AddBookingToDisplayedIfNew(newBooking);
                }
            }
            else if (e.OldItems.Count > 0)
            {
                var oldBooking = (e.OldItems[0] as Booking);
                RemoveBookingFromDisplayedIfPossible(oldBooking);
            }
        }

       

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public void InvalidateOnBookingStatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(Booking.BookingStatus))
            {
                FilterDisplayedBookings();
            }
        }
        #region FilterProperties
        private Visibility _isRemoveFilterButtonVisible;
        public Visibility IsRemoveFilterButtonVisible
        {
            get { return _isRemoveFilterButtonVisible; }
            set { _isRemoveFilterButtonVisible = value; OnPropertyChanged(); }
        }

        private string _filterString;
        public string FilteredGuestString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged();
            }
        }

        private bool _showReserved = true;
        public bool ShowReservedFilter
        {
            get { return _showReserved; }
            set
            {
                _showReserved = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _showCancelled = false;
        public bool ShowCancelledFilter
        {
            get { return _showCancelled; }
            set
            {
                _showCancelled = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _showCheckedIn = true;
        public bool ShowCheckedInFilter
        {
            get { return _showCheckedIn; }
            set
            {
                _showCheckedIn = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _showCheckedOut = false;
        public bool ShowCheckedOutFilter
        {
            get { return _showCheckedOut; }
            set
            {
                _showCheckedOut = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }


        private bool _showNoShow = false;
        public bool ShowNoShowFilter
        {
            get { return _showNoShow; }
            set
            {
                _showNoShow = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }
        #endregion FilterProperties

        public void FilterDisplayedBookings()
        {
            DisplayedBookings = new ObservableCollection<Booking>();
            IEnumerable<Booking> filteredBookings = Bookings.Where(ShouldShowBooking);
            filteredBookings.ForEach(x =>
            {
                AddBookingToDisplayedIfNew(x);
            });
            if (filterGuest != null)
            {
                DisplayedBookings = new ObservableCollection<Booking>(DisplayedBookings.Where(x =>
                   x.Guests.Contains(filterGuest)
                ));
            }
        }

        private bool ShouldShowBooking(Booking booking)
        {
            return StatusFiltersList[booking.BookingStatus]();
        }

        private void AddBookingToDisplayedIfNew(Booking x)
        {
            if (!DisplayedBookings.Contains(x))
            {
                DisplayedBookings.Add(x);
            }
        }

        private void RemoveBookingFromDisplayedIfPossible(Booking x)
        {
            DisplayedBookings.Remove(x); // ignore result that says whether item was removed
        }

        public void FilterDisplayedBookingsByGuest(Guest g)
        {
            filterGuest = g;
            FilteredGuestString = filterGuest.FirstName + " " + filterGuest.LastName;
            IsRemoveFilterButtonVisible = Visibility.Visible;
            FilterDisplayedBookings();
        }

        public void RemoveGuestFilter()
        {
            filterGuest = null;
            FilteredGuestString = "";
            IsRemoveFilterButtonVisible = Visibility.Hidden;
            FilterDisplayedBookings();
        }
    }
}
