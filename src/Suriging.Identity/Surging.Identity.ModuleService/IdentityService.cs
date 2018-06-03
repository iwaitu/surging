using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.ProxyGenerator;
using Surging.Identity.Core.Models;
using Surging.Identity.Database;
using Surging.Identity.IModuleService;
using Surging.Identity.IModuleService.ViewModels;
using Surging.Identity.ModuleService.Extensions;
using System;
using System.Threading.Tasks;

namespace Surging.Identity.ModuleService
{
    [ModuleName("Identity")]
    public class IdentityService : ProxyServiceBase, IIdentityService
    {
        private IdentityRepository _db = new IdentityRepository();
        private UserManager<SurgingUser> _usrManager;
        private UserStore<SurgingUser> _usrStore;
        private IPasswordHasher<SurgingUser> passwordHasher = new PasswordHasher<SurgingUser>();
        private UpperInvariantLookupNormalizer lookupNormalizer = new UpperInvariantLookupNormalizer();

        public IdentityService()
        {
            _usrStore = new UserStore<SurgingUser>(_db);
            _usrManager = new UserManager<SurgingUser>(_usrStore, null, null, null, null, null, null, null, null);
            _usrManager.PasswordHasher = passwordHasher;
            _usrManager.KeyNormalizer = lookupNormalizer;
        }

        public async Task<UserModel> Authentication(LoginModel requestData)
        {
            try
            {
                var user = await _usrManager.FindByNameAsync(requestData.username);
                if (user != null)
                {
                    var ret = await _usrManager.CheckPasswordAsync(user, requestData.password);
                    if (ret)
                    {
                        user.LastLoginDate = DateTime.Now;
                        await _usrManager.UpdateAsync(user);
                        return user.ToModel();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        private IUserPasswordStore<SurgingUser> GetPasswordStore()
        {
            var cast = _usrStore as IUserPasswordStore<SurgingUser>;
            if (cast == null)
            {
                throw new NotSupportedException("fuck");
            }
            return cast;
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
            var exituser = await _usrManager.FindByNameAsync(username);
            if (exituser != null)
                return null;
            var user = new SurgingUser();
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = username;
            user.NormalizedUserName = _usrManager.KeyNormalizer.Normalize(username);
            user.PhoneNumber = username;
            
            var pwhash = passwordHasher.HashPassword(user, password);
            user.PasswordHash = pwhash;
            var ret = await _usrStore.CreateAsync(user);
            if (ret.Succeeded)
            {
                return user.ToModel();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CheckUserExist(string username)
        {
            var user = await _usrManager.FindByNameAsync(username);
            return user != null;
        }

        public Task<UserModel> GetUserInfo(string id)
        {
            return Task.FromResult(new UserModel());
        }

        public Task<bool> ResetPassword(string password1, string password2)
        {
            return Task.FromResult(false);
        }
    }
}
