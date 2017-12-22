﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel.Model
{
    public class Booking : INotifyPropertyChanged
    {
        public virtual Guid Id { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        private Guest _guest;
        public virtual Guest Guest
        {
            get { return _guest; }
            set { _guest = value; OnPropertyChanged(); }
        }

        private Room _room;
        public virtual Room Room
        {
            get { return _room; }
            set { _room = value; OnPropertyChanged(); }
        }

        private BookingPeriod _bookingPeriod;

        public virtual BookingPeriod BookingPeriod
        {
            get { return _bookingPeriod; }
            set { _bookingPeriod = value; }
        }

        public bool OverlapsWith(Booking booking)
        {
            return BookingPeriod.OverlapsWith(booking.BookingPeriod);
        }

        public bool DoesNotOverlapWith(Booking booking)
        {
            return BookingPeriod.DoesNotoverlapWith(booking.BookingPeriod);
        }

        public string StartDayString
        {
            get { return BookingPeriod.StartDate.ToShortDateString(); }
        }

        public string EndDayString
        {
            get { return BookingPeriod.EndDate.ToShortDateString(); }
        }

        public string GuestName
        {
            get { return Guest.FirstName ?? "null"; }
        }

        public string RoomNumber
        {
            get { return Room.RoomNumber ?? "null"; }
        }

        public virtual void SetDates(SelectedDatesCollection selectedDates)
        {
            BookingPeriod = new BookingPeriod(selectedDates.FirstOrDefault(), selectedDates.LastOrDefault());
        }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
