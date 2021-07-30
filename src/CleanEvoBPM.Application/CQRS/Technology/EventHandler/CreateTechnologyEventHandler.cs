using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.Models;
using MediatR;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.EventHandler
{
    public class CreateTechnologyEventHandler : INotificationHandler<Notification<CreateTechnologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateTechnologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateTechnologyLog> notification, CancellationToken cancellationToken)        
        {
            try
            {                
                bool xval = _elasticClient.Indices.Exists("technology").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("technology");
                }  
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx=>idx.Index("technology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}


