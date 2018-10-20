using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TechStoreMvcArchitecture.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate);
    }
}
