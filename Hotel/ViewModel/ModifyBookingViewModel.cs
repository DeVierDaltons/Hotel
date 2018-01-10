﻿using Hotel.Model;
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

namespace Hotel.ViewModel
{
    public class ModifyBookingViewModel : INotifyPropertyChanged

    {
        private ObservableCollection<Booking> Bookings;

        private ObservableCollection<Booking> _DisplayedBookings;

        public ObservableCollection<Booking> DisplayedBookings
        {
            get { return _DisplayedBookings; }
            set { _DisplayedBookings = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<BookingStatus> BookingStatusFilters { get;} = new List<BookingStatus>();

        public List<KeyValuePair<BookingStatus, Func<bool>>> StatusFiltersList { get; set; } = new List<KeyValuePair<BookingStatus, Func<bool>>>();
        public ModifyBookingViewModel(ObservableCollection<Booking> bookings)
        {
            InitializeStatusFilterList();
            Bookings = bookings;
            DisplayedBookings = new ObservableCollection<Booking>(bookings);
            bookings.CollectionChanged += Bookings_CollectionChanged;
        }

        private void InitializeStatusFilterList()
        {
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.Available, () => { return ShowAvailableFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.Cancelled, () => { return ShowCancelledFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.CheckedIn, () => { return ShowCheckedInFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.CheckedOut, () => { return ShowCheckedOutFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.NoShow, () => { return ShowNoShowFilter; }));
            StatusFiltersList.Add(new KeyValuePair<BookingStatus, Func<bool>>(BookingStatus.Reserved, () => { return ShowReservedFilter; }));
        }

        /// <summary>
        /// If an item gets added to the original list of bookings also add it to the displayed list of bookings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bookings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
            if (e.NewItems.Count > 0) {
                
                var newBooking = (e.NewItems[0] as Booking);
                foreach (KeyValuePair<BookingStatus, Func<bool>> kvp in StatusFiltersList)
                {
                    //check if the item should be displayed or not, if not return otherwise add it to the displayed items.
                    if (newBooking.BookingStatus == kvp.Key && !kvp.Value())
                    {
                        return;
                    }
                }
                if (!DisplayedBookings.Contains(newBooking))
                {
                    DisplayedBookings.Add(newBooking);
                }
            }
            else
            {
                var oldBooking = (e.OldItems[0] as Booking);
                if (DisplayedBookings.Contains(oldBooking))
                {
                    DisplayedBookings.Remove(oldBooking);
                }
            }
        }

        #region FilterProperties
        private bool _ShowAvailable = true;
        public bool ShowAvailableFilter {
            get
            {
                return _ShowAvailable;
            } 
            set
            {
                _ShowAvailable = value;
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

        private bool _ShowCancelled = true;

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

        private bool _ShowCheckedOut = true;

        public bool ShowCheckedOutFilter
        {
            get { return _ShowCheckedOut; }
            set
            {
                _ShowCheckedOut = value;
                OnPropertyChanged();
            }
        }


        private bool _ShowNoShow = true;
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

        public void FilterDisplayedBookings()
        {
            DisplayedBookings = new ObservableCollection<Booking>();
            foreach(KeyValuePair<BookingStatus, Func<bool>> kvp in StatusFiltersList)
            {
                //the filter is set to true so filter for this bookingstatus
                if (kvp.Value())
                {
                    List<Booking> filteredBookings = Bookings.Where(x => x.BookingStatus == kvp.Key).ToList();
                    filteredBookings.ForEach(x => {
                        if (!DisplayedBookings.Contains(x))
                        {
                            DisplayedBookings.Add(x);
                        }
                    });
                }
            }
        }

    }
}
