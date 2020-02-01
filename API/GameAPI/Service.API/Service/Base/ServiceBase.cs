using Data.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Service.API.Base
{
    public class ServiceBase<TIRepository, T>
       where TIRepository : IRepository<T>
       where T : class
    {
        public TIRepository _mRepository;
        //private readonly UnitOfWork _uoW;

        public ServiceBase(TIRepository paramRepository)///, UnitOfWork uoW)
        {
            this._mRepository = paramRepository;
            //this._uoW = uoW;
        }

        public ServiceBase() { }

        public virtual T GetEntry(int id)
        {
            if (id == 0) return null;
            return _mRepository.GetEntry(id);
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return _mRepository.GetList(where, orderBy, includeProperties);
        }

        public virtual void Insert(T entity)
        {
            _mRepository.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            _mRepository.Update(entity);
        }

        public virtual void Delete(T entityToDelete)
        {
            _mRepository.Delete(entityToDelete);
        }

        public virtual void Delete(int id)
        {
            _mRepository.Delete(id);
        }
        public virtual void Delete(int[] arrayID)
        {
            foreach (var id in arrayID)
            {
                _mRepository.Delete(id);
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            _mRepository.Delete(where);
        }

        public virtual void Truncate(string tableName = "")
        {
            _mRepository.Truncate(tableName);
        }

        public virtual void QuerySQL(string sql = "")
        {
            _mRepository.QuerySQL(sql);
        }


        public virtual void Save()
        {
            _mRepository.Save();
        }

    }
}