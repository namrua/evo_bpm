using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace CleanEvoBPM.WebAPI.Utility
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var listOfUrls = new Uri[]
            {
              // here we can set multple connectionn URL's...
              new Uri(url)
            };

            StaticConnectionPool connPool = new StaticConnectionPool(listOfUrls);
            //var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex);
            var settings = new ConnectionSettings(connPool);

            //AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        //private static void AddDefaultMappings(ConnectionSettings settings)
        //{
        //    settings
        //        .DefaultMappingFor<Product>(m => m
        //            .Ignore(p => p.Price)
        //            .Ignore(p => p.Quantity)
        //            .Ignore(p => p.Rating)
        //        );
        //}

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName);
        }
    }
}
