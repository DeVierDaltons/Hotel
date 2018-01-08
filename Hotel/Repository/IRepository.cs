using System;
using System.Collections.Generic;

namespace Hotel.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get item entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>item</returns>
        T Get(Guid guid);

        /// <summary>
        /// Save item entity
        /// </summary>
        /// <param name="item">item</param>
        void Save(T item);

        /// <summary>
        /// Update item entity
        /// </summary>
        /// <param name="item">item</param>
        void Update(T item);

        /// <summary>
        /// Delete item entity
        /// </summary>
        /// <param name="item">item</param>
        void Delete(T item);

        /// <summary>
        /// Retrieve all guests
        /// </summary>
        /// <returns>All the guests</returns>
        List<T> GetAll();
    }
}
