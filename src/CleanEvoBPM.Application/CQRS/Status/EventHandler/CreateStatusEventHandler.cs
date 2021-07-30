using CleanEvoBPM.Application.CQRS.Status.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Status.EventHandler
{
    public class CreateStatusEventHandler : INotificationHandler<Notification<CreateStatusLog>>
    {
        private readonly IElasticClient _elasticClient;
        public CreateStatusEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public Task Handle(Notification<CreateStatusLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("status").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("status");
                }
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("status"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
