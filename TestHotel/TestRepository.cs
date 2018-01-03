using System;
using System.Collections.Generic;
using Hotel.Model;
using Hotel.Repository;
using System.Diagnostics;

namespace TestHotel
{
    internal class TestRepository<T> : IRepository<Guest>
    {
        public List<Guest> guests = new List<Guest>();

        public void Delete(Guest person)
        {
            Debug.Assert(guests.Contains(person));
            guests.Remove(person);
        }

        public Guest Get(Guid guid)
        {
            return guests.Find(g => g.Id == guid);
        }

        public List<Guest> GetAll()
        {
            return new List<Guest>(guests);
        }

        public long RowCount()
        {
            return guests.Count;
        }

        public void Save(Guest person)
        {
            guests.Add(person);
        }

        public void Update(Guest person)
        {
            Delete(person);
            Save(person);
        }
    }
}