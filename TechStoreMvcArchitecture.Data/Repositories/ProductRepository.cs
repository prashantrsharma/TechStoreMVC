using System;
using System.Collections.Generic;
using System.Text;
using TechStoreMvcArchitecture.Data.Infrastructure;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Data.Repositories
{
    public  class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
