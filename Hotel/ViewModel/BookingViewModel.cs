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

        public List<BookingStatus> BookingStatusFilters { get;} = new List<BookingStatus>();

        public List<KeyValuePair<BookingStatus, Func<bool>>> StatusFiltersList { get; set; } = new List<KeyValuePair<BookingStatus, Func<bool>>>();
        public BookingViewModel(ObservableCollection<Booking> bookings)
        {
            InitializeStatusFilterList();
            Bookings = bookings;
            foreach(Booking booking in Bookings)
            {
                booking.PropertyChanged += InvalidateOnBookingStatusChanged;
            }
            bookings.CollectionChanged += Bookings_CollectionChanged;
           
        }

        private void InitializeStatusFilterList()
        {
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.Cancelled, () => { return ShowCancelledFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.CheckedIn, () => { return ShowCheckedInFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.CheckedOut, () => { return ShowCheckedOutFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.NoShow, () => { return ShowNoShowFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.Reserved, () => { return ShowReservedFilter; }));
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
                foreach (KeyValuePair<BookingStatus, Func<bool>> kvp in StatusFiltersList)
                {
                    //check if the item should be displayed or not, if not return otherwise add it to the displayed items.
                    if (newBooking.BookingStatus == kvp.Key && !kvp.Value())
                    {
                        return;
                    }
                }
                AddBookingToDisplayedIfNew(newBooking);
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

        private string _FilterString;
        public string FilteredGuestString
        {
            get { return _FilterString; }
            set {
                _FilterString = value;
                if(value == "")
                {
                    IsRemoveFilterButtonVisible = Visibility.Hidden;
                    filterGuest = null;
                }
                else
                {
                    IsRemoveFilterButtonVisible = Visibility.Visible;
                }
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
            }
        }
        #endregion FilterProperties

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            if (name.Contains("Filter"))
            {
                FilterDisplayedBookings();
            }
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
            List<Booking> filteredBookings = Bookings.Where(x =>
            {
                foreach (KeyValuePair<BookingStatus, Func<bool>> kvp in StatusFiltersList)
                {
                    //the filter is set to true so filter for this bookingstatus
                    if (x.BookingStatus == kvp.Key)
                    {
                        return kvp.Value();
                    }
                }
                return false;
            }).ToList();
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
        }
    }
}
