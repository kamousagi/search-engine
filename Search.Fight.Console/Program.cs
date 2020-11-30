using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Interface;
using Search.Fight.Infraestructure.Container.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Search.Fight.Console
{
    class Program
    {
        //public static IConfiguration configuration;
        private static IConfiguration Configuration { get; set; }


        static async Task Main(string[] args)
        {
            args = new string[] { ".net", "java" };

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            IHost host = CreateHostBuilder(args).Build();
            await ExemplifyScoping(host.Services, args);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.Configure<Configuration>(Configuration.GetSection("Configuration"));
                    services.ConfigureSearchFightServices();
                });

        static async Task ExemplifyScoping(IServiceProvider services, string[] args)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            ISearcher searchService = serviceProvider.GetRequiredService<ISearcher>();
            List<SearchResult> result = await searchService.Search(args);

            foreach (var item in result)
            {
                System.Console.WriteLine($"{item.SearchTerm}: {item.SeachEngine}: {item.TotalResults}");
            }
        }
    }
}