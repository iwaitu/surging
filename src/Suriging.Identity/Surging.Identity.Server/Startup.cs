using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surging.Core.Caching;
using Surging.Core.Caching.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Identity.Database;

namespace Surging.Identity.Server
{
    public class Startup
    {
        public Startup(IConfigurationBuilder config)
        {
            ConfigureEventBus(config);
            ConfigureCache(config);
            ConfigureAppSettings(config);
            Configuration = config.Build();
        }

        public IConfiguration Configuration { get; }

        public IContainer ConfigureServices(ContainerBuilder builder)
        {
            var services = new ServiceCollection();
            ConfigureLogging(services);
            ConfigureDbContext(services);
            builder.Populate(services);
            ServiceLocator.Current = builder.Build();
            return ServiceLocator.Current;
        }

        public void Configure(IContainer app)
        {
            app.Resolve<ILoggerFactory>()
                    .AddConsole((c, l) => (int)l >= 3);
        }

        #region 私有方法
        /// <summary>
        /// 配置日志服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging();
        }

        private void ConfigureDbContext(IServiceCollection service)
        {
            //service.AddTransient<IRepository>(provider => provider.GetService<IdentityRepository>());
            service.AddDbContext<IdentityContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Transient);
            
        }

        private static void ConfigureEventBus(IConfigurationBuilder build)
        {
            build
            .AddEventBusFile("eventBusSettings.json", optional: false);
        }

        /// <summary>
        /// 配置缓存服务
        /// </summary>
        private void ConfigureCache(IConfigurationBuilder build)
        {
            build
              .AddCacheFile("cacheSettings.json", optional: false);
        }

        private void ConfigureAppSettings(IConfigurationBuilder build)
        {
            build
              .AddJsonFile("appSettings.json", optional: false);
        }
        #endregion
    }
}
