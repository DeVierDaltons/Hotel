using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;
using Hotel.ViewModel;
using Hotel.Repository;
using Hotel.Dao;
using System.Windows.Controls;
using System;

namespace TestHotel
{
    [TestClass]
    public class AddBookingTests
    {
        [TestMethod]
        public void AddBookingUsingCommandTest()
        {
            var hotelManager = CreateTestHotelManager();

            Room room = new Room();
            hotelManager.AddRoom(room);

            SelectedDatesCollection dates = CreateSelectedDates();
            Booking booking = new Booking();
            booking.SetDates(dates);
            booking.Room = room;
            booking.Guest = new Guest();

            AddBookingViewModel addBookingViewModel = new AddBookingViewModel();
            addBookingViewModel.HotelManager = hotelManager;
            addBookingViewModel.SelectedDates = dates;
            new AddBookingCommand(addBookingViewModel).Execute(booking);
            Assert.IsTrue(addBookingViewModel.HotelManager.Bookings.Count > 0);
        }

        private static SelectedDatesCollection CreateSelectedDates()
        {
            Calendar calendar = new Calendar();
            calendar.SelectionMode = CalendarSelectionMode.SingleRange;
            var dates = new SelectedDatesCollection(calendar);
            dates.AddRange(DateTime.Now, DateTime.Now.AddDays(2d));
            return dates;
        }

        [TestMethod]
        public void InvalidBookingCommandFails()
        {
            Assert.IsFalse(new AddBookingCommand(new AddBookingViewModel()).CanExecute(null));
        }

        public static HotelManager CreateTestHotelManager()
        {
            IRepository<Guest> guestsRepo = new TestRepository<Guest>();
            IRepository<Room> roomsRepo = new TestRepository<Room>();
            IRepository<Booking> bookingsRepo = new TestRepository<Booking>();
            return new HotelManager(guestsRepo, roomsRepo, bookingsRepo);
        }
    }
}
