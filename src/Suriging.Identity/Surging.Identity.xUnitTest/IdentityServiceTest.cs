using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Surging.Identity.Database;
using Surging.Identity.IModuleService.ViewModels;
using Surging.Identity.ModuleService;
using System.Threading.Tasks;
using Xunit;

namespace Surging.Identity.xUnitTest
{
    public class IdentityServiceTest
    {
        
        public IConfiguration configuration;
        public IdentityServiceTest()
        {
            configuration = InitConfiguration();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
        [Fact]
        public async Task CreateUser()
        {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

            var service = new UserService(options);
            var user = await service.CreateUser("18107718055", "123456");
            Assert.NotNull(user);
            ////var ret = await service.Authentication(new IModuleService.ViewModels.LoginModel { username = "18107711156", password = "scf1013" });
            //var ret = await service.CreateUser("rafael", "fafa888");
            //Assert.NotNull(ret);
        }

        [Fact]
        public async Task UserLogin()
        {
            var loginModel = new AuthenticationRequestData { UserName = "18107718055", Password = "123456" };
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

            var service = new UserService(options);
            var user = await service.Authentication(loginModel);
            Assert.NotNull(user);
            Assert.True(user.UserName == "18107718055");
        }

        [Fact]
        public async Task CheckUserExist()
        {
            var options = new DbContextOptionsBuilder<IdentityContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

            var service = new UserService(options);
            var ret = await service.CheckUserExist("18107718055");
            Assert.True(ret);
        }

        [Fact]
        public void ParallelTest()
        {
            var loginModel = new AuthenticationRequestData { UserName = "18107718055", Password = "123456" };
            var options = new DbContextOptionsBuilder<IdentityContext>()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;
            using (var service = new UserService(options))
            {
                Parallel.For(0, 20, s =>
                {
                    var ret = service.Authentication(loginModel).Result;
                    Assert.NotNull(ret);
                });
                
            }
            
        }
    }
}
