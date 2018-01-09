using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;
using Hotel.ViewModel;
using Hotel.Repository;
using Hotel.DataAccessObjects;
using System.Windows.Controls;
using System;

namespace TestHotel
{
    [TestClass]
    public class AddBookingTests
    {
        [TestMethod]
        public void AddBookingUsingCommand()
        {
            AddBookingViewModel addBookingViewModel = CreateBookingViewModel();
            addBookingViewModel.AddBooking();
            Assert.IsTrue(addBookingViewModel.HotelManager.Bookings.Count > 0);
        }

        private static AddBookingViewModel CreateBookingViewModel()
        {
            var hotelManager = CreateTestHotelManager();
            AddBookingViewModel addBookingViewModel = new AddBookingViewModel(hotelManager)
            {
                Room = hotelManager.Rooms[0],
                Guest = hotelManager.Guests[0],
                SelectedDates = CreateSelectedDates()
            };
            return addBookingViewModel;
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
            AddBookingViewModel vm = new AddBookingViewModel(null)
            {
                Booking = null,
                Guest = null,
                SelectedDates = null
            };
            Assert.IsFalse(new AddBookingCommand(vm).CanExecute(null));
        }

        [TestMethod]
        public void CantDoubleBook()
        {
            AddBookingViewModel addBookingViewModel = CreateBookingViewModel();
            addBookingViewModel.AddBooking();
            Assert.IsFalse(addBookingViewModel.ValidateInput());
        }

        public static HotelManager CreateTestHotelManager()
        {
            IRepository<Guest> guestsRepo = new TestRepository<Guest>();
            IRepository<Room> roomsRepo = new TestRepository<Room>();
            IRepository<Booking> bookingsRepo = new TestRepository<Booking>();
            var hotelManager = new HotelManager(guestsRepo, roomsRepo, bookingsRepo);
            hotelManager.AddRoom(new Room());
            hotelManager.AddGuest(new Guest());
            return hotelManager;
        }
    }
}
