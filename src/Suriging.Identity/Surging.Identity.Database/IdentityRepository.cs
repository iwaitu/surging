using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Surging.Identity.Core.Models;
using System;

namespace Surging.Identity.Database
{

    public class IdentityRepository : IdentityDbContext<SurgingUser, SurgingRole, string>
    {
        private static readonly ILoggerFactory _loggerFactory = new LoggerFactory();

        //public IdentityRepository()
        //{
        //    //_loggerFactory.AddConsole((s, l) => l == LogLevel.Debug && s.EndsWith("Command"));
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging()
                .UseLoggerFactory(_loggerFactory)
                .UseSqlServer(@"");
        }
    }
}
