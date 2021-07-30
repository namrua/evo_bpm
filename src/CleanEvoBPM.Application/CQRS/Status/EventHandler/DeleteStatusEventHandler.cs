using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;
using CleanEvoBPM.Application.CQRS.Status.Event;
using CleanEvoBPM.Domain;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CleanEvoBPM.Application.CQRS.Status.EventHandler
{
    public class DeleteStatusEventHandler:INotificationHandler<Notification<DeleteStatusLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteStatusEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        public Task Handle(Notification<DeleteStatusLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                
                var respose = _elasticClient.DeleteAsync<DeleteStatusLog>(notification.Event, idx => idx.Index("status"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}