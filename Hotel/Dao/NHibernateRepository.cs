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
            {
                var result = session.Query<T>().ToList();
                foreach(T item in result)
                {
                    Booking booking = item as Booking;
                    if( booking != null)
                    {
                        NHibernateUtil.Initialize(booking.Guest);
                        NHibernateUtil.Initialize(booking.Room);
                    }
                }
                return result;
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