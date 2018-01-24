using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.Command;
using Hotel.ViewModel;
using System;
using Hotel.Data;

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
            Assert.IsTrue(addBookingViewModel.AllBookings.Count > 0);
        }

        private static AddBookingViewModel CreateBookingViewModel()
        {
            AddBookingViewModel addBookingViewModel = new AddBookingViewModel()
            {
                SelectedDates = CreateSelectedDates()
            };
            return addBookingViewModel;
        }

        private static BookingPeriod CreateSelectedDates()
        {
            return new BookingPeriod(DateTime.Now, DateTime.Now.AddDays(2d));
        }

        [TestMethod]
        public void InvalidBookingCommandFails()
        {
            AddBookingViewModel vm = new AddBookingViewModel()
            {
                Booking = new Booking(),
                Guests = null,
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

      
    }
}
