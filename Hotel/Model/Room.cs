﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Hotel.Model
{
    public class Room : INotifyPropertyChanged
    {
        public virtual Guid Id { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public List<Booking> Bookings = new List<Booking>();

        private string _roomNumber;
        public virtual string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; OnPropertyChanged(); }
        }

        private int _beds;
        public virtual int Beds
        {
            get { return _beds; }
            set { _beds = value; OnPropertyChanged(); }
        }

        private RoomQuality _quality;
        public virtual RoomQuality Quality
        {
            get { return _quality; }
            set { _quality = value; OnPropertyChanged(); }
        }

        private bool _hasNiceView;
        public virtual bool HasNiceView
        {
            get { return _hasNiceView; }
            set { _hasNiceView = value; OnPropertyChanged(); } 
        }

        private decimal _pricePerDay;
        public virtual decimal PricePerDay
        {
            get { return _pricePerDay; }
            set { _pricePerDay = value; OnPropertyChanged(); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }

        public bool TimePeriodAvailable(BookingPeriod period)
        {
            foreach (BookingPeriod bookingPeriod in Bookings.ConvertAll((Booking booking) => booking.BookingPeriod))
            {
                if (bookingPeriod.OverlapsWith(period))
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return RoomNumber;
        }
    }
}
