using PokerGameAPI.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PokerGameAPI
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("ConnectionstringDBuser")
        {
            //Database.SetInitializer(new EfContextDbInitializer());
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<PokerGameAPI.Entities.UserCoin> UserCoins { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public static AuthContext Create()
        {
            return new AuthContext();
        }
        //public AuthContext(string connectionString)
        //    : base(connectionString)
        //{

        //}
        //static AuthContext()
        //{
        //    Database.SetInitializer(new EfContextDbInitializer());
        //}
        //public class EfContextDbInitializer : CreateDatabaseIfNotExists<AuthContext>
        //{
        //    protected override void Seed(AuthContext context)
        //    {
        //        //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Order', RESEED, 1000000)");
        //    }
        //}
    }

}