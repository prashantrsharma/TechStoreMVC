using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TechStoreMvcArchitecture.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private TechStoreContext _context;
        private readonly IDbSet<T> _dbSet;

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = Context.Set<T>();
        }

        protected TechStoreContext Context
        {
            get { return _context ?? (_context = DbFactory.Init()); }
        }

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
           IEnumerable<T> entities =  _dbSet.Where<T>(predicate).AsEnumerable();
            foreach (T entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T,bool>> predicate)
        {
            return _dbSet.Where<T>(predicate).FirstOrDefault<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList()
        {
            return _dbSet.ToList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where<T>(predicate).AsEnumerable<T>();
        }

    }
}
