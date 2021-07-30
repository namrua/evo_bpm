using CleanEvoBPM.Application.CQRS.Client.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Client.EventHandler
{
    public class UpdateClientEventHandler : INotificationHandler<Notification<UpdateClientLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateClientEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateClientLog> notification, CancellationToken cancellationToken)
        {
            try
            {                
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("client"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
