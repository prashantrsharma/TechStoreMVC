using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreMvcArchitecture.Data.Configuration;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Data
{
    public class TechStoreContext : DbContext
    {
        public TechStoreContext():base("TechStoreConn")
        {
            Database.SetInitializer(new TechStoreSeedData());
            this.Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Specification> Specifications { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
         
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new SpecificationConfiguration());
        }
    }
}
