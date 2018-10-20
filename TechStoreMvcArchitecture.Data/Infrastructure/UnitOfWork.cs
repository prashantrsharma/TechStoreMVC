using System;
using System.Collections.Generic;
using System.Text;

namespace TechStoreMvcArchitecture.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private TechStoreContext _context;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public TechStoreContext Context
        {
            get { return _context ?? (_context = _dbFactory.Init()); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _context.Commit();
        }
    }
}
