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

        public IdentityService()
        {
            _usrStore = new UserStore<SurgingUser>(_db);
            _usrManager = new UserManager<SurgingUser>(_usrStore, null, null, null, null, null, null, null, null);

        }

        public async Task<UserModel> Authentication(LoginModel requestData)
        {
            var user = await _usrManager.FindByNameAsync(requestData.username);
            if (user != null)
            {
                var ret = await _usrManager.CheckPasswordAsync(user, requestData.password);
                if (ret)
                {
                    return user.ToModel();
                }
                else
                {
                    return null;
                }
            }
            return null;


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
            var user = new SurgingUser();
            user.SecurityStamp = Guid.NewGuid().ToString();
            var exituser = await _usrManager.FindByNameAsync(username);
            if (exituser != null)
                return null;
            user.UserName = username;
            user.PhoneNumber = username;
            var passwordHasher = new PasswordHasher<SurgingUser>();
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
