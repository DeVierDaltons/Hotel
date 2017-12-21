using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.ViewModel;
using Hotel.Model;
using Hotel.Command;
using System.Collections.ObjectModel;

namespace UnitTests
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
    }
}
