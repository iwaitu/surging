﻿using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Surging.Core.Caching;
using Surging.Core.Caching.Configurations;
using Surging.Core.Codec.MessagePack;
using Surging.Core.Consul;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.DotNetty;
using Surging.Core.EventBusRabbitMQ;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Core.Nlog;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting;
using Surging.Core.ServiceHosting.Internal.Implementation;
using System;
using System.IO;
using System.Text;

namespace Surging.Identity.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var host = new ServiceHostBuilder()
                .RegisterServices(builder =>
                {
                    builder.AddMicroService(option =>
                    {
                        option.AddServiceRuntime()
                        .AddRelateService()
                        .AddConfigurationWatch()
                        //option.UseZooKeeperManager(new ConfigInfo("127.0.0.1:2181"));
                        .UseConsulManager(new ConfigInfo("10.0.75.1:8500", reloadOnChange: true))
                        .UseDotNettyTransport()
                        .UseRabbitMQTransport()
                        .AddRabbitMQAdapt()
                        .AddCache()
                        //.UseKafkaMQTransport(kafkaOption =>
                        //{
                        //    kafkaOption.Servers = "127.0.0.1";
                        //    kafkaOption.LogConnectionClose = false;
                        //    kafkaOption.MaxQueueBuffering = 10;
                        //    kafkaOption.MaxSocketBlocking = 10;
                        //    kafkaOption.EnableAutoCommit = false;
                        //})
                        //.AddKafkaMQAdapt()
                        //.UseProtoBufferCodec()
                        .UseMessagePackCodec();
                        builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
                    });
                })
                .SubscribeAt()
                //.UseLog4net(LogLevel.Error, "Configs/log4net.config")
                .UseNLog(LogLevel.Error, "Configs/NLog.config")
                //.UseServer("127.0.0.1", 98)
                //.UseServer("127.0.0.1", 98，“true”) //自动生成Token
                //.UseServer("127.0.0.1", 98，“123456789”) //固定密码Token
                .UseServer(options =>
                {
                    // options.IpEndpoint = new IPEndPoint(IPAddress.Any, 98);  
                    options.Token = "True";
                    options.ExecutionTimeoutInMilliseconds = 30000;
                    options.MaxConcurrentRequests = 200;
                })
                .UseServiceCache()
                .Configure(build =>
                build.AddEventBusFile("eventBusSettings.json", optional: false))
                .Configure(build =>
                build.AddCacheFile("cacheSettings.json", optional: false, reloadOnChange: true))
                  .Configure(build =>
                build.AddCPlatformFile("surgingSettings.json", optional: false, reloadOnChange: true))
                .UseProxy()
                .UseStartup<Startup>()
                .Build();

            using (host.Run())
            {
                Console.WriteLine($"用户服务端启动成功，{DateTime.Now}。");
            }
        }
    }
}
