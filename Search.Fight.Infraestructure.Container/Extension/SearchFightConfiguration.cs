using Microsoft.Extensions.DependencyInjection;
using Search.Fight.Applicacion.Model;
using Search.Fight.Application.Service.Implementation;
using Search.Fight.Application.Service.Implementation.SearchEngine;
using Search.Fight.Application.Service.Interface;
using System;
using Microsoft.Extensions.Configuration;

namespace Search.Fight.Infraestructure.Container.Extension
{
    public static class SearchFightConfiguration
    {

        public static void SearchFightServicesConfigure(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var configuracion = configurationSection.Get<Configuration>();

            services.AddScoped<ISearcher, Searcher>();
            services.AddHttpClient<ISearchEngine, GoogleSearchEngine>(client =>
            {
                client.BaseAddress = new Uri(configuracion.GoogleConfiguration.Uri);
            });

            services.AddHttpClient<ISearchEngine, BingEngine>(client =>
            {
                client.BaseAddress = new Uri(configuracion.BingConfiguration.Uri);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configuracion.BingConfiguration.OcpApimSubscriptionKey);
            });

        }
    }
}