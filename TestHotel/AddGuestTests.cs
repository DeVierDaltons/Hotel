using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Data;
using Hotel.Command;
using Hotel.ViewModel;
using Hotel.Repository;
using Hotel.DataAccessObjects;
using System.Windows.Controls;

namespace TestHotel
{
    [TestClass]
    public class AddGuestTests
    {
        [TestMethod]
        public void AddGuestUsingCommandTest()
        {
            IRepository<Guest> repository = new TestRepository<Guest>();
            RepositoryBackedObservableCollection<Guest> guestList = new RepositoryBackedObservableCollection<Guest>(repository);
            GuestsViewModel viewModel = new GuestsViewModel();
            viewModel.Initialize();
            viewModel.StartAddingGuest();
            viewModel.CurrentGuest.SubmitCommand.Execute(null);
            Assert.IsTrue(guestList.Count > 0);
        }

        [TestMethod]
        public void InvalidGuestCommandFails()
        {
            IRepository<Guest> repository = new TestRepository<Guest>();
            RepositoryBackedObservableCollection<Guest> guestList = new RepositoryBackedObservableCollection<Guest>(repository);
           // var guestsVM = new GuestDetailViewModel(new AddGuestCommand(guestList), new Guest(), () => { });
            //Assert.IsFalse(guestsVM.SubmitCommand.CanExecute(null));
        }
    }
}
