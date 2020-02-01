/**
 * Project: Data.API 
 * FileName: IRepository.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-1-31
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.API.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FullTextSearch(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> expression = null, string searchKey = "");
        IQueryable<T> ContainsSearch(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> expression = null, string searchKey = "");
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
        void Delete(Expression<Func<T, bool>> where);

        void Truncate(string tableName = "");
        void QuerySQL(string sql = "");

        void Save();
    }
}
