using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;
using Hotel.ViewModel;
using Hotel.Repository;
using Hotel.Dao;
using System.Diagnostics;

namespace TestHotel
{
    [TestClass]
    public class AddRoomTests
    {
        [TestMethod]
        public void AddRoomUsingCommandTest()
        {
            IRepository<Room> repository = new TestRepository<Room>();
            AddRoomViewModel addRoomViewModel = new AddRoomViewModel(AddBookingTests.CreateTestHotelManager());
            new AddRoomCommand(addRoomViewModel).Execute(new Room());
            Assert.IsTrue(addRoomViewModel.HotelManager.Rooms.Count > 0);
        }

        [TestMethod]
        public void InvalidRoomCommandFails()
        {
            AddRoomViewModel addRoomViewModel = new AddRoomViewModel(null);
            Assert.IsFalse(addRoomViewModel.ValidateInput());
        }
    }
}
