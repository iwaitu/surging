using Surging.Identity.IModuleService.ViewModels;
using Surging.Identity.ModuleService;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Surging.Identity.xUnitTest
{
    public class IdentityServiceTest
    {
        [Fact]
        public async Task CreateUser()
        {
            var service = new IdentityService();
            var user = await service.CreateUser("18107718055", "123456");
            Assert.NotNull(user);
            ////var ret = await service.Authentication(new IModuleService.ViewModels.LoginModel { username = "18107711156", password = "scf1013" });
            //var ret = await service.CreateUser("rafael", "fafa888");
            //Assert.NotNull(ret);
        }

        [Fact]
        public async Task UserLogin()
        {
            var loginModel = new LoginModel { username = "18107718055", password = "123456" };
            var service = new IdentityService();
            var user = await service.Authentication(loginModel);
            Assert.NotNull(user);
            Assert.True(user.UserName == "18107718055");
        }

        [Fact]
        public async Task CheckUserExist()
        {
            var service = new IdentityService();
            var ret = await service.CheckUserExist("18107718055");
            Assert.True(ret);
        }
    }
}
