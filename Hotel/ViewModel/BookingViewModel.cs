using Hotel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hotel.View;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Hotel.Extensions;
using NHibernate.Util;

namespace Hotel.ViewModel
{
    public class BookingViewModel : INotifyPropertyChanged
    {


        private AddBookingView _addBookingView;

        public AddBookingView AddBookingView
        {
            get { return _addBookingView; }
            set { _addBookingView = value; }
        }

        private ObservableCollection<Booking> Bookings;

        private ObservableCollection<Booking> _displayedBookings = new ObservableCollection<Booking>();

        public ObservableCollection<Booking> DisplayedBookings
        {
            get { return _displayedBookings; }
            set { _displayedBookings = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<BookingStatus, Func<bool>> StatusFiltersList = new Dictionary<BookingStatus, Func<bool>>();
        public BookingViewModel(ObservableCollection<Booking> bookings)
        {
            InitializeStatusFilterList();
            Bookings = bookings;
            foreach(Booking booking in Bookings)
            {
                booking.PropertyChanged += InvalidateOnBookingStatusChanged;
            }
            bookings.CollectionChanged += Bookings_CollectionChanged;
            FilterDisplayedBookings();
        }

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

        #region FilterProperties
        private Visibility _IsRemoveFilterButtonVisible;
        public Visibility IsRemoveFilterButtonVisible
        {
            get { return _IsRemoveFilterButtonVisible; }
            set { _IsRemoveFilterButtonVisible = value; OnPropertyChanged(); }
        }

        private string _filterString;
        public string FilteredGuestString
        {
            get { return _filterString; }
            set {
                _filterString = value;
                OnPropertyChanged();
            }
        }

        private bool _ShowReserved = true;
        public bool ShowReservedFilter
        {
            get { return _ShowReserved; }
            set
            {
                _ShowReserved = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _ShowCancelled = false;
        public bool ShowCancelledFilter
        {
            get { return _ShowCancelled; }
            set
            {
                _ShowCancelled = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _ShowCheckedIn = true;
        public bool ShowCheckedInFilter
        {
            get { return _ShowCheckedIn; }
            set
            {
                _ShowCheckedIn = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }

        private bool _ShowCheckedOut = false;
        public bool ShowCheckedOutFilter
        {
            get { return _ShowCheckedOut; }
            set
            {
                _ShowCheckedOut = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }


        private bool _ShowNoShow = false;
        public bool ShowNoShowFilter
        {
            get { return _ShowNoShow; }
            set
            {
                _ShowNoShow = value;
                OnPropertyChanged();
                FilterDisplayedBookings();
            }
        }
        #endregion FilterProperties

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

        private Guest filterGuest;

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
                   x.Guest == filterGuest
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
