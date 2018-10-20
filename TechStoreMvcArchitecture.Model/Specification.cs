using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreMvcArchitecture.Model
{

    public class Specification
    {
        public int SpecificationId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
