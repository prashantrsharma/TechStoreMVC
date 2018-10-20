using System;
using System.Collections.Generic;
using System.Text;

namespace TechStoreMvcArchitecture.Data.Infrastructure
{
    public class DbFactory : IDbFactory
    {
       TechStoreContext _dbContext;

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public TechStoreContext Init()
        {
            return _dbContext ?? (_dbContext = new TechStoreContext());
        }
    }
}
