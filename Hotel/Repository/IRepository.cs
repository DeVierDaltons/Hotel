using System;
using System.Collections.Generic;

namespace Hotel.Repository
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get person entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>person</returns>
        T Get(Guid guid);

        /// <summary>
        /// Save person entity
        /// </summary>
        /// <param name="person">person</param>
        void Save(T person);

        /// <summary>
        /// Update person entity
        /// </summary>
        /// <param name="person">person</param>
        void Update(T person);

        /// <summary>
        /// Delete person entity
        /// </summary>
        /// <param name="person">person</param>
        void Delete(T person);

        /// <summary>
        /// Retrieve all guests
        /// </summary>
        /// <returns>All the guests</returns>
        List<T> GetAll();

        /// <summary>
        /// Row count person in db
        /// </summary>
        /// <returns>number of rows</returns>
        long Count();
    }
}
