using System.IO;
using Hotel.Model;
using NHibernate.Tool.hbm2ddl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotel.Dao.Test
{
    [TestClass]
    public class NHibernateGuestRepositoryTest
    {
        private IGuestRepository _personRepo;

        [TestInitialize]
        public void CreateSchema()
        {
            DeleteDatabaseIfExists();

            var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
            schemaUpdate.Execute(false, true);

            _personRepo = new NHibernateGuestRepository();
        }

        [TestMethod]
        public void CanSaveGuest()
        {
            _personRepo.Save(new Guest());
            Assert.AreEqual(1, _personRepo.RowCount());
        }

        [TestMethod]
        public void CanGetGuest()
        {
            var person = new Guest();
            _personRepo.Save(person);
            Assert.AreEqual(1, _personRepo.RowCount());

            person = _personRepo.Get(person.Id);
            Assert.IsNotNull(person);
        }

        [TestMethod]
        public void CanUpdateGuest()
        {
            var person = new Guest();
            _personRepo.Save(person);
            Assert.AreEqual(1, _personRepo.RowCount());

            person = _personRepo.Get(person.Id);
            person.FirstName = "Test";
            _personRepo.Update(person);

            Assert.AreEqual(1, _personRepo.RowCount());
            Assert.AreEqual("Test", _personRepo.Get(person.Id).FirstName);
        }

        [TestMethod]
        public void CanDeleteGuest()
        {
            var person = new Guest();
            _personRepo.Save(person);
            Assert.AreEqual(1, _personRepo.RowCount());

            _personRepo.Delete(person);
            Assert.AreEqual(0, _personRepo.RowCount());
        }

        [TestCleanup]
        public void DeleteDatabaseIfExists()
        {
            if (File.Exists("test.db"))
                File.Delete("test.db");
        }
    }
}