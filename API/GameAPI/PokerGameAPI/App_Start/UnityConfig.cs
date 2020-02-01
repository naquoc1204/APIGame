using Data.API.DAL;
using Data.API.Infrastructure;
using Data.API.Repositorys;
using Service.API.AspNetUser;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PokerGameAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDatabaseFactory, DatabaseFactory>();

            container.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
            container.RegisterType<IAspNetUserService, AspNetUserService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}