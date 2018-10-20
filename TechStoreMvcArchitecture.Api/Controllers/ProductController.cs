using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechStoreMvcArchitecture.Data.Infrastructure;
using TechStoreMvcArchitecture.Data.Repositories;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            List<Product> products = _productRepository.GetList().ToList();
            return Ok(products);
        }
    }
}
