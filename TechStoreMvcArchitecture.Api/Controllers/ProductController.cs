using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechStoreMvcArchitecture.Data.Infrastructure;
using TechStoreMvcArchitecture.Data.Repositories;
using TechStoreMvcArchitecture.Model;
using TechStoreMvcArchitecture.Service.Abstract;
using TechStoreMvcArchitecture.Service.Services;

namespace TechStoreMvcArchitecture.Api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            List<Product> products = _productService.GetList().ToList();
            return Ok(products);
        }
    }
}
