using Data.API.DAL;
using Data.API.Infrastructure;
using Data.API.Repositorys;
using Service.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.API.AspNetUser
{
    public class AspNetUserService: ServiceBase<IAspNetUserRepository, Data.API.Entity.AspNetUser>, IAspNetUserService
    {
        private readonly AspNetUserRepository _paramRepository;
        //public AspNetUserService()
        //{
        //    DatabaseFactory databaseFactory = new DatabaseFactory();
        //    UnitOfWork ouW = new UnitOfWork(databaseFactory);
        //    this._paramRepository = new AspNetUserRepository(ouW);
        //    base._mRepository = this._paramRepository;
        //}
        public AspNetUserService(AspNetUserRepository paramRepository, UnitOfWork uOW)
            : base(paramRepository)//, uOW)
        {
            this._paramRepository = paramRepository;
            //this._uOW = uOW;
        }
    }
}