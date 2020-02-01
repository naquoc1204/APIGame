/**
 * Project: Data.API 
 * FileName: UnitOfWork.cs 
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
using System.Text;
using System.Threading.Tasks;

namespace Data.API.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseFactory _databaseFactory;
        private DBContext _dbContext;

        public UnitOfWork(IDatabaseFactory databaseFatory)
        {
            this._databaseFactory = databaseFatory;
        }
        public DBContext dbContext
        {
            get { return _dbContext ?? (_dbContext = _databaseFactory.Get()); }
        }
        public void Commit()
        {
            dbContext.Commit();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
