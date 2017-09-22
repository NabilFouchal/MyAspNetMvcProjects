using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.IRepositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Add item in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>item added in the database.</returns>
        T Add(T item);

        /// <summary>
        /// Get all items from the database.
        /// </summary>
        /// <returns>List of items.</returns>
        IEnumerable<T> GetAll();
    }
}
