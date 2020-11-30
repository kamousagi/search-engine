using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Implementation;
using Search.Fight.Application.Service.Interface;
using Search.Fight.Infraestructure.Container.Extension;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Fight.Console
{
    class Program
    {
        //public static IConfiguration configuration;

        static async Task Main(string[] args)
        {
            args = new string[] {".net", "java"};

            // IConfigurationBuilder builder = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .AddEnvironmentVariables();
            // Configuration = builder.Build();

            IHost host = CreateHostBuilder(args).Build();
            await ExemplifyScoping(host.Services, args);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.ConfigureSearchFightServices());

        static async Task ExemplifyScoping(IServiceProvider services, string[] args)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            ISearchEngineExecute searchService = serviceProvider.GetRequiredService<ISearchEngineExecute>();
            List<SearchResult> result = await searchService.Search(args);

            foreach (var item in result)
            {
                System.Console.WriteLine($"{item.SearchTerm}: {item.SeachEngine}: {item.TotalResults}");
            }
        }
    }
}