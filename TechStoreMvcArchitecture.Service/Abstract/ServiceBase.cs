using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechStoreMvcArchitecture.Data.Infrastructure;

namespace TechStoreMvcArchitecture.Service.Abstract
{
    public abstract class ServiceBase<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork,IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            _unitOfWork.Commit();
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _repository.Delete(predicate);
            _unitOfWork.Commit();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<T> GetList()
        {
           return _repository.GetList();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetMany(predicate);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWork.Commit();
        }
      
    }
}
