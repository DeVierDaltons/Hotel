using System;
using System.Collections.Generic;

namespace Hotel.Model
{
    public interface IGuestRepository
    {
        /// <summary>
        /// Get person entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>person</returns>
        Guest Get(Guid id);

        /// <summary>
        /// Save person entity
        /// </summary>
        /// <param name="person">person</param>
        void Save(Guest person);

        /// <summary>
        /// Update person entity
        /// </summary>
        /// <param name="person">person</param>
        void Update(Guest person);

        /// <summary>
        /// Delete person entity
        /// </summary>
        /// <param name="person">person</param>
        void Delete(Guest person);

        /// <summary>
        /// Retrieve all guests
        /// </summary>
        /// <returns>All the guests</returns>
        List<Guest> GetAll();

        /// <summary>
        /// Row count person in db
        /// </summary>
        /// <returns>number of rows</returns>
        long RowCount();
    }
}
