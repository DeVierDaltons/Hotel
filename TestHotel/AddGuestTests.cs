using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Hotel.Model;
using Hotel.Command;
using Hotel.ViewModel;
using Hotel.Repository;
using Hotel.Dao;

namespace TestHotel
{
    [TestClass]
    public class AddGuestTests
    {
        [TestMethod]
        public void AddGuestUsingCommandTest()
        {
            IRepository<Guest> repository = new NHibernateRepository<Guest>();
            RepositoryBackedObservableCollection<Guest> guestList = new RepositoryBackedObservableCollection<Guest>(repository);
            new AddGuestCommand(guestList).Execute(new Guest());
            Assert.IsTrue(guestList.Count > 0);
        }

        [TestMethod]
        public void InvalidGuestCommandFails()
        {
            IRepository<Guest> repository = new NHibernateRepository<Guest>();
            RepositoryBackedObservableCollection<Guest> guestList = new RepositoryBackedObservableCollection<Guest>(repository);
            var guestsVM = new GuestDetailViewModel(new AddGuestCommand(guestList), new Guest());
            Assert.IsFalse(guestsVM.SubmitCommand.CanExecute(null));
        }
    }
}
