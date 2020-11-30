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

            services.AddScoped<ISearchEngineExecute, SearchEngineExecute>();
            services.AddHttpClient<ISearchEngine, GoogleSearchEngine>(client =>
            {
                client.BaseAddress = new Uri($"https://www.googleapis.com/customsearch/");
            });

            /*
            services.AddHttpClient<MsnSeachService>(client =>
            {
                //client.BaseAddress = new Uri("https://api.github.com/");
                //client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                //client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });
            */
        }
    }
}