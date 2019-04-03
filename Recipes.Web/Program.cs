using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Recipes.Dal;
using Microsoft.Extensions.DependencyInjection;

namespace Recipes.Web
{
    public class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            //Log4net setup
            var log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            //logging "Application - Main is invoked"
            _log.Info("Application - Main is invoked");

            //Seeds database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<RecipeDb>();
                    DbInitializer.Initialize(context);
                    _log.Info("Database is seeded.");
                }
                catch (Exception ex)
                {
                    _log.Error($"An error occurred while seeding the database. {ex}");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
