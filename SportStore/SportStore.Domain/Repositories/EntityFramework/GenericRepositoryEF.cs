using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Database;
using SportStore.Domain.Entities;
using SportStore.Domain.IRepositories;

namespace SportStore.Domain.Repositories
{
    public class GenericRepositoryEF<T> : IGenericRepository<T> where T : class
    {
        private DbContext dbContext;

        public GenericRepositoryEF(DbContext contextParam)
        {
            dbContext = contextParam;
        }

        public T Add(T item)
        {
            return dbContext.Set<T>().Add(item);
        }


        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }
    }
}
