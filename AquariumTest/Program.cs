using AquariumTest.Repositories;
using AquariumTest.SeedData;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AquariumTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            var scopeFactory = host.Services;

            try
            {

                var repo = host.Services.GetRequiredService<IRepository>();
                var manager = new SeedDataManager(repo);
                manager.Seed();


            }
            catch (System.Exception e)
            {
                throw e;
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
                .UseDefaultServiceProvider(options =>
            options.ValidateScopes = false)
               .Build();
    }
}
