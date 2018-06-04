using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using Surging.Identity.Core.Models;
using Surging.Identity.Database;
using Surging.Identity.IModuleService;
using Surging.Identity.IModuleService.ViewModels;
using Surging.Identity.ModuleService.Extensions;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Autofac;

namespace Surging.Identity.ModuleService
{
    [ModuleName("User")]
    public class UserService : ProxyServiceBase, IUserService, IDisposable
    {
        //private IdentityRepository _db = new IdentityRepository();
        private DbContextOptions<IdentityContext> _dbOptions;
        private IComponentContext _iocContext;

        public UserService(IComponentContext iocContext)
        {
            _iocContext = iocContext;
        }

        public UserService(DbContextOptions<IdentityContext> options)
        {
            _dbOptions = options;
        }

        public ApplicationUserManager GenerelUserManager()
        {
            if (_iocContext != null)
            {
                return _iocContext.Resolve<ApplicationUserManager>();
            }
            else
            {
                return ApplicationUserManager.Create(_dbOptions);
            }
        }
        
        public async Task<UserModel> Authentication(AuthenticationRequestData requestData)
        {
            try
            {
                using (var manager = GenerelUserManager())
                {
                    var user = await manager.FindByNameAsync(requestData.UserName);
                    //var user = await _db.Table<SurgingUser>(p => p.NormalizedUserName == lookupNormalizer.Normalize(requestData.UserName)).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        //return user.ToModel();
                        var ret = await manager.CheckPasswordAsync(user, requestData.Password);
                        if (ret)
                        {
                            user.LastLoginDate = DateTime.Now;
                            await manager.GetUserStore().UpdateAsync(user, System.Threading.CancellationToken.None);
                            return user.ToModel();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return null;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        
        public Task<bool> CheckPersonNoUnqine(string id)
        {
            return Task.FromResult(false);
        }

        public Task<bool> CheckPhonenumberUnqine(string id)
        {
            return Task.FromResult(false);
        }

        public async Task<UserModel> CreateUser(string username, string password)
        {
            using (var manager = GenerelUserManager())
            {
                var exituser = await manager.FindByNameAsync(username);
                if (exituser != null)
                    return null;
                var user = new SurgingUser();
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.UserName = username;
                user.NormalizedUserName = manager.KeyNormalizer.Normalize(username);
                user.PhoneNumber = username;

                var pwhash = manager.PasswordHasher.HashPassword(user, password);
                user.PasswordHash = pwhash;
                var ret = await manager.GetUserStore().CreateAsync(user, System.Threading.CancellationToken.None);
                if (ret.Succeeded)
                {
                    return user.ToModel();
                }
                else
                {
                    return null;
                }
            }
                
        }


        public async Task<bool> CheckUserExist(string username)
        {
            using (var manager = GenerelUserManager())
            {
                var user = await manager.FindByNameAsync(username);
                return user != null;
            }
                
        }

        public Task<UserModel> GetUserInfo(string id)
        {

            return Task.FromResult(new UserModel());
        }

        public async Task<bool> ResetPassword(string userid,string token, string newpassword)
        {
            using (var manager = GenerelUserManager())
            {
                var user = await manager.FindByIdAsync(userid);
                if (user != null)
                {
                    var ret = await manager.ResetPasswordAsync(user, token, newpassword);
                    return ret.Succeeded;
                }
                return false;
            }
                
        }

        public void Dispose()
        {
            
        }
    }
}
