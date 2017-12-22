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
    public class AddRoomTests
    {
        [TestMethod]
        public void AddRoomUsingCommandTest()
        {
            HotelManager hotelManager = new HotelManager();
            AddRoomViewModel addRoomViewModel = new AddRoomViewModel();
            addRoomViewModel.HotelManager = hotelManager;
            new AddRoomCommand(addRoomViewModel).Execute(null);
            Assert.IsTrue(hotelManager.Rooms.Count > 0);
        }

        [TestMethod]
        public void InvalidRoomCommandFails()
        {
            HotelManager hotelManager = new HotelManager();
            AddRoomViewModel addRoomViewModel = new AddRoomViewModel();
            addRoomViewModel.HotelManager = hotelManager;
            Assert.IsFalse(new AddRoomCommand(addRoomViewModel).CanExecute(null));
        }
    }
}
