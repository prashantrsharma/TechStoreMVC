using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Data.Configuration
{
    public class SpecificationConfiguration :EntityTypeConfiguration<Specification>
    {
        public SpecificationConfiguration()
        {
            ToTable("Specifications");
            Property(spec => spec.Name).IsRequired().HasMaxLength(64);
            Property(spec => spec.Value).IsRequired().HasMaxLength(512);
            Property(spec => spec.ProductId).IsRequired();
        }
    }
}
