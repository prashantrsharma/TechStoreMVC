using System;
using System.Collections.Generic;
using System.Text;

namespace TechStoreMvcArchitecture.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TechStoreContext Init();
    }
}
