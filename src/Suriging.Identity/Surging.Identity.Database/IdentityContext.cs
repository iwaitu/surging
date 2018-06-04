using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Surging.Identity.Core.Models;
using System;

namespace Surging.Identity.Database
{

    public class IdentityContext : IdentityDbContext<SurgingUser, SurgingRole, string>
    {
        //private static readonly ILoggerFactory _loggerFactory = new LoggerFactory();
        //public IdentityRepository(IOptions<IdentitySetting>)
        //{
        //    //_loggerFactory.AddConsole((s, l) => l == LogLevel.Debug && s.EndsWith("Command"));
        //}

        [ThreadStatic]
        protected static IdentityContext current;
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            //_loggerFactory.AddConsole((s, l) => l == LogLevel.Debug && s.EndsWith("Command"));
        }

        public static IdentityContext Current(DbContextOptions<IdentityContext> options)
        {
            if (current == null)
                current = new IdentityContext(options);

            return current;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.EnableSensitiveDataLogging()
        //        .UseLoggerFactory(_loggerFactory)
        //        .UseSqlServer(@"Data Source=www.nngeo.com,52983;Initial Catalog=usertest;Integrated Security=True;User ID=sa;Password=Nlis@1204;Persist Security Info=True;Integrated Security=False;MultipleActiveResultSets=true;");
        //}
    }
}
