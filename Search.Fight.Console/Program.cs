using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Implementation;
using Search.Fight.Infraestructure.Container.Extension;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Search.Fight.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await ExemplifyScoping(host.Services, args);

            System.Console.ReadLine();
        }

        //IHostBuilder -> Microsoft.Extensions.Hosting.Abstraction
        //Host -> Microsoft.Extensions.Hosting
        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((_, services) =>
                  services.ConfigureSearchFightServices());

        static async Task ExemplifyScoping(IServiceProvider services, string[] args)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            SearchEngineExecute searchService = serviceProvider.GetRequiredService<SearchEngineExecute>();
            List<SearchResult> result = await searchService.Search(args);

            foreach (var item in result)
            {
                System.Console.WriteLine($"{item.SearchTerm}: {item.SeachEngine}: {item.TotalResults}");
            }
        }
    }
}