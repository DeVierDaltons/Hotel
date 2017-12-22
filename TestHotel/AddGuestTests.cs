using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;
using Hotel.View;
using Hotel.ViewModel;

namespace TestHotel
{
    [TestClass]
    public class AddGuestTests
    {
        [TestMethod]
        public void AddGuestUsingCommandTest()
        {
            ObservableCollection<Guest> guestList = new ObservableCollection<Guest>();
            new AddGuestCommand(guestList).Execute(new Guest());
            Assert.IsTrue(guestList.Count > 0);
        }

        [TestMethod]
        public void InvalidGuestCommandFails()
        {
            var guestsVM = new GuestDetailViewModel(new AddGuestCommand(new ObservableCollection<Guest>()), new Guest());
            Assert.IsFalse(guestsVM.SubmitCommand.CanExecute(null));
        }
    }
}
