using Surging.Identity.ModuleService;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Surging.Identity.xUnitTest
{
    public class IdentityServiceTest
    {
        [Fact]
        public async Task Test1()
        {
            var service = new IdentityService();
            var user = await service.CreateUser("18107718055", "123456");
            Assert.NotNull(user);
            ////var ret = await service.Authentication(new IModuleService.ViewModels.LoginModel { username = "18107711156", password = "scf1013" });
            //var ret = await service.CreateUser("rafael", "fafa888");
            //Assert.NotNull(ret);
        }
    }
}
