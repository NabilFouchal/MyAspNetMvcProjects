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
    public class ProductRepositoryEF : GenericRepositoryEF<Product>, IProductRepository<Product>
    {
        public ProductRepositoryEF(DbContext contextParam) : base(contextParam)
        {
        }
    }
}
