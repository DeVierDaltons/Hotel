using System;
using System.Collections.Generic;

namespace Hotel.Repository
{
    public interface IRepository<T>
    {
        T Get(Guid guid);
        void Save(T item);
        void Update(T item);
        void Delete(T item);
        List<T> GetAll();
    }
}
