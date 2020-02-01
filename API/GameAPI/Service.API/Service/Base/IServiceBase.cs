using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Service.API.Base
{
    public interface IServiceBase<T> where T : class, new()
    {
        IQueryable<T> GetList(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");


        /// <summary>
        /// CRUD METHOD
        /// </summary>
        /// <param name="item"></param>
        T GetEntry(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(T entityToDelete);
        void Delete(int id);
        void Delete(int[] id);
        void Delete(Expression<Func<T, bool>> where);

        void Truncate(string tableName = "");
        void QuerySQL(string sql = "");

        void Save();
    }
}