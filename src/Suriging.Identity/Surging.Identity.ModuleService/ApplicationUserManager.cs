using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Surging.Identity.Core.Models;
using Surging.Identity.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Identity.ModuleService
{
    public class ApplicationUserManager : UserManager<SurgingUser>
    {
        //private IPasswordHasher<SurgingUser> passwordHasher = new PasswordHasher<SurgingUser>();
        //private UpperInvariantLookupNormalizer lookupNormalizer = new UpperInvariantLookupNormalizer();

        [ThreadStatic]
        private static ApplicationUserManager _current;

        public ApplicationUserManager(IUserStore<SurgingUser> store)
            : base(store, null, new PasswordHasher<SurgingUser>(), null, null, new UpperInvariantLookupNormalizer(), null, null, null)
        {
        }

        public static ApplicationUserManager Create(DbContextOptions<IdentityContext> options)
        {
            
            var manager = new ApplicationUserManager(new UserStore<SurgingUser>(new IdentityContext(options)));
            return manager;

        }

        public IUserStore<SurgingUser> GetUserStore()
        {
            return this.Store;
        }
    }
}
