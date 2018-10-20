using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Data.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");
            Property(p => p.Sku).IsRequired().HasMaxLength(16);
            Property(p => p.Name).IsRequired().HasMaxLength(64);
            Property(p => p.Description).IsRequired().HasMaxLength(512);
            Property(p => p.NumberInStock).IsRequired();
        }
    }
}
