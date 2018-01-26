using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Data;
using Hotel.Command;
using Hotel.ViewModel;
using System.Diagnostics;

namespace TestHotel
{
    [TestClass]
    public class AddRoomTests
    {
        [TestMethod]
        public void AddRoomUsingCommandTest()
        {
            //IRepository<Room> repository = new TestRepository<Room>();
            //AddRoomViewModel addRoomViewModel = new AddRoomViewModel(AddBookingTests.CreateTestHotelManager().Rooms);
            //new AddRoomCommand(addRoomViewModel).Execute(new Room());
            //Assert.IsTrue(addRoomViewModel.Rooms.Count > 0);
        }

        [TestMethod]
        public void InvalidRoomCommandFails()
        {
        //    AddRoomViewModel addRoomViewModel = new AddRoomViewModel(null);
        //    Assert.IsFalse(addRoomViewModel.ValidateInput());
        }
    }
}
