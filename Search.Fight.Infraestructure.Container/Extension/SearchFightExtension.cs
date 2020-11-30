using Microsoft.Extensions.DependencyInjection;
using Search.Fight.Application.Service.Implementation;
using Search.Fight.Application.Service.Implementation.SearchEngine;
using Search.Fight.Application.Service.Interface;
using System;

namespace Search.Fight.Infraestructure.Container.Extension
{
    public static class SearchFightExtension
    {

        public static void ConfigureSearchFightServices(this IServiceCollection services)
        {
            services.AddScoped<ISearcher, SearchEngineExecute>();
            services.AddHttpClient<ISearchEngine, GoogleSearchEngine>(client =>
            {
                client.BaseAddress = new Uri($"https://www.googleapis.com/customsearch/");
            });

            services.AddHttpClient<ISearchEngine, BingEngine>(client =>
            {
                client.BaseAddress = new Uri("https://api.bing.microsoft.com/v7.0/");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "f547cd29f9254e2e9afdf86464da42d5");
            });
        }
    }
}