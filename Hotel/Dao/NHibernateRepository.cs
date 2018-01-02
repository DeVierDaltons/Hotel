using System;
using NHibernate;
using System.Linq;
using System.Collections.Generic;
using Hotel.Repository;

namespace Hotel.Dao
{
    public class NHibernateRepository<T> : IRepository<T> where T : class
    {
        public void Save(T person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(person);
                transaction.Commit();
            }
        }

        public T Get(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<T>(id);
        }

        public void Update(T person)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(person);
                transaction.Commit();
            }
        }

        public List<T> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<T>().ToList();
            }
        }

        public void Delete(T person)
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
                return session.QueryOver<T>().RowCountInt64();
            }
        }
    }
}