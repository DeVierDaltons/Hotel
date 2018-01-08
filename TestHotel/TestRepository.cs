using System;
using System.Collections.Generic;
using Hotel.Repository;
using Hotel.Model;
using System.Diagnostics;

namespace TestHotel
{
    public class TestRepository<T> : IRepository<T> where T : IHasGUID
    {
        private List<T> items = new List<T>();

        public void Delete(T item)
        {
            Debug.Assert(items.Contains(item));
            items.Remove(item);
        }

        public T Get(Guid guid)
        {
            return items.Find(g => g.Id == guid);
        }

        public List<T> GetAll()
        {
            return new List<T>(items);
        }

        public void Save(T item)
        {
            items.Add(item);
        }

        public void Update(T item)
        {
            Delete(item);
            Save(item);
        }
    }
}