using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hotel.ViewModel;
using Hotel.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddGuestToHotelManagerTest()
        {
            AddGuestViewModel viewmodel = CreateViewModel();
            CreateGuestInViewModel(viewmodel);
            viewmodel.HotelManager = CreateHotelManager();
            viewmodel.AddGuest();
            Assert.IsTrue(viewmodel.HotelManager.Guests.Count > 0);
        }

        [TestMethod]
        public void AddGuestUsingCommandTest()
        {
            AddGuestViewModel viewmodel = CreateViewModel();
            CreateGuestInViewModel(viewmodel);
            viewmodel.HotelManager = CreateHotelManager();
            viewmodel.AddGuestCommand.Execute(viewmodel);
            Assert.IsTrue(viewmodel.HotelManager.Guests.Count > 0);
        }

        [TestMethod]
        public void ValidateInvalidGuestTest()
        {
            AddGuestViewModel viewmodel = CreateViewModel();
            CreateInvalidGuestInViewModel(viewmodel);
            Assert.IsFalse(viewmodel.AddGuestCommand.CanExecute(null));
        }

        [TestMethod]
        public void ValidatValidGuestTest()
        {
            AddGuestViewModel viewmodel = CreateViewModel();
            CreateGuestInViewModel(viewmodel);
            Assert.IsTrue(viewmodel.AddGuestCommand.CanExecute(null));
        }

        public AddGuestViewModel CreateViewModel()
        {
            return new AddGuestViewModel();
        }

        public HotelManager CreateHotelManager()
        {
            return new HotelManager();
        }

        public void CreateGuestInViewModel(AddGuestViewModel vm)
        {
            vm.Guest.FirstName = "Jan";
            vm.Guest.LastName = "klaassen";
            vm.Guest.PhoneNumber = "0630949595";
            vm.Guest.ICEPhoneNumber = "0031231982498";
            vm.Guest.EmailAdress = "janklaassen@hotmail.com";
        }

       
        public void CreateInvalidGuestInViewModel(AddGuestViewModel vm)
        {
            vm.Guest.FirstName = "Jan";
            vm.Guest.ICEPhoneNumber = "0031231982498";
        }
    }
}
