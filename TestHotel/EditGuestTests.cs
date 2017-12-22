using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;

namespace TestHotel
{
    [TestClass]
    public class EditGuestTests
    {
        [TestMethod]
        public void AddGuestUsingCommandTest()
        {
            ObservableCollection<Guest> guestList = new ObservableCollection<Guest>();
            new AddGuestCommand(guestList).Execute(new Guest());
            Assert.IsTrue(guestList.Count > 0);
        }
    }
}
