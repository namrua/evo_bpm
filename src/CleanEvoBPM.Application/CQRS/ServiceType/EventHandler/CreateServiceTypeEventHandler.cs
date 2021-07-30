using CleanEvoBPM.Application.CQRS.ServiceType.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ServiceType.EventHandler
{
    public class CreateServiceTypeEventHandler : INotificationHandler<Notification<CreateServiceTypeLog>>
    {
        private readonly IElasticClient _elasticClient;
        public CreateServiceTypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public Task Handle(Notification<CreateServiceTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("servicetype").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("servicetype");
                }
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("servicetype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
