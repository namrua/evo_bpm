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
    public class CreateClientEventHandler : INotificationHandler<Notification<CreateClientLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateClientEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateClientLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("client").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("client");
                }
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
