/**
 * Project: Data.API 
 * FileName: Disposable.cs 
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
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        protected virtual void DisposeCore()
        { }
    }
}
