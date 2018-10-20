using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreMvcArchitecture.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberInStock { get; set; }
        public virtual List<Specification> Specifications { get; set; }
    }
}
