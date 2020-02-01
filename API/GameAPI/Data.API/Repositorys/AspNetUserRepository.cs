
using Data.API.DAL;
using Data.API.Entity;
using Data.API.Infrastructure;
/**
* Project: Data.API 
* FileName: AspNetUserRepository.cs 
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

namespace Data.API.Repositorys
{
    public class AspNetUserRepository : RepositoryBase<AspNetUser>, IAspNetUserRepository
    {
        public AspNetUserRepository(IUnitOfWork uOW) : base(uOW) { }
    }
}
