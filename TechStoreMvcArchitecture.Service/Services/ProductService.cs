using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreMvcArchitecture.Data.Infrastructure;
using TechStoreMvcArchitecture.Model;
using TechStoreMvcArchitecture.Service.Abstract;

namespace TechStoreMvcArchitecture.Service.Services
{
    public class ProductService :ServiceBase<Product>,IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository)
                            :base(unitOfWork, repository)
        {
            
        }

        public void Validate(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
