using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;
using CleanEvoBPM.Domain;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ServiceType.EventHandler
{
    public class DeleteServiceTypeEventHandler : INotificationHandler<Notification<DeleteServiceTypeLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteServiceTypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteServiceTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                
                var respose = _elasticClient.DeleteAsync<DeleteServiceTypeLog>(notification.Event, idx => idx.Index("servicetype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
