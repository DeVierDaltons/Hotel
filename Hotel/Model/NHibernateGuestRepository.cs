using System;
using Hotel.Model;
using NHibernate;
using System.Linq;
using System.Collections.Generic;

namespace Hotel.Dao
{
    public class NHibernateGuestRepository : IGuestRepository
    {
        public void Save(Guest person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(person);
                transaction.Commit();
            }
        }

        public Guest Get(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Guest>(id);
        }

        public void Update(Guest person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(person);
                transaction.Commit();
            }
        }

        public List<Guest> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<Guest>().ToList();
            }
        }

        public void Delete(Guest person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(person);
                transaction.Commit();
            }
        }

        public long RowCount()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<Guest>().RowCountInt64();
            }
        }
    }
}