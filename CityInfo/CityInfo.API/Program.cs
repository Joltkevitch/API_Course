using System;
using System.Collections.Generic;
using NLog.Web;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CityInfo.API.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var host = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<CityInfoContext>();

                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                }
                catch (Exception error)
                {
                    logger.Error("Error at migrating the DB", error);
                    throw error;
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
