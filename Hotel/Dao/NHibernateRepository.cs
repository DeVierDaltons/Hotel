using System;
using NHibernate;
using System.Linq;
using System.Collections.Generic;
using Hotel.Repository;
using Hotel.Model;

namespace Hotel.Dao
{
    public class NHibernateRepository<T> : IRepository<T> where T : class
    {
        public void Save(T item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(item);
                transaction.Commit();
            }
        }

        public T Get(Guid id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<T>(id);
        }

        public void Update(T item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
        }

        public List<T> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
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