/**
 * Project: Data.API 
 * FileName: RepositoryBase.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-1-31
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */
using effts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.API.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        public DBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        //public RepositoryBase(IDatabaseFactory databaseFactory)
        //{
        //    DatabaseFactory = databaseFactory;
        //    _dbSet = DataContext.Set<T>();
        //}

        //public RepositoryBase(DBContext dbContext)
        //{
        //    this._dbContext = dbContext;
        //    _dbSet = dbContext.Set<T>();
        //}

        public RepositoryBase(IUnitOfWork uOW)
        {
            DbInterception.Add(new FtsInterceptor());
            this._dbContext = uOW.dbContext;
            _dbSet = uOW.dbContext.Set<T>();
        }

        //protected IDatabaseFactory DatabaseFactory
        //{
        //    get;
        //    private set;
        //}

        //protected DBContext DataContext
        //{
        //    get { return _dbContext ?? (_dbContext = DatabaseFactory.Get()); }
        //}

        /// <summary>
        /// Get Entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetEntry(int id)
        {
            return _dbSet.Find(id);
        }


        public virtual IQueryable<T> FullTextSearch(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> expression = null, string searchKey = "")
        {
            IQueryable<T> list = _dbSet;

            if (where != null)
            {
                list = list.Where(where);
            }

            return list.FreeTextSearch(expression, searchKey);
        }

        public virtual IQueryable<T> ContainsSearch(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> expression = null, string searchKey = "")
        {
            IQueryable<T> list = _dbSet;

            if (where != null)
            {
                list = list.Where(where);
            }

            return list.FreeTextSearch(expression, searchKey);
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> list = _dbSet;

            if (where != null)
            {
                list = list.Where(where);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                var includeList = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var includeProperty in includeList)
                {
                    list = list.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(list);
            }
            else
            {
                return list;
            }
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            var entry = _dbContext.Entry<T>(entity);

            var pkey = _dbSet.Create().GetType().GetProperty("ID").GetValue(entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _dbContext.Set<T>();
                T attachedEntity = set.Find(pkey);
                if (attachedEntity != null)
                {
                    var attachedEntry = _dbContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            else
            {
                entry.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var entities = GetList(where);
            if (entities.Count() > 0)
            {
                foreach (var entity in entities)
                {
                    _dbSet.Remove(entity);
                }
            }
        }


        #region "Truncate"

        public virtual void Truncate(string tableName = "")
        {
            var context = (_dbContext as IObjectContextAdapter).ObjectContext;

            if (string.IsNullOrEmpty(tableName))
            {

            }
            var type = typeof(T);
            var entityName = context.CreateObjectSet<T>().EntitySet.Name;
            var tableAttribute = type.GetCustomAttributes(false).OfType<TableAttribute>().FirstOrDefault();

            tableName = tableAttribute == null ? entityName : tableAttribute.Name;

            var sqlCommand = String.Format("DELETE FROM {0}", tableName);
            var sqlResetID = string.Format("DBCC CHECKIDENT ('{0}', RESEED, 0)", tableName);

            context.ExecuteStoreCommand(sqlCommand);
            context.ExecuteStoreCommand(sqlResetID);
            context.SaveChanges();
        }

        public virtual void QuerySQL(string sql = "")
        {
            var context = (_dbContext as IObjectContextAdapter).ObjectContext;
            context.ExecuteStoreCommand(sql);
            context.SaveChanges();
        }

        #endregion
        /// <summary>
        /// Save
        /// </summary>
        public virtual void Save()
        {
            _dbContext.Commit();
        }
    }
}
