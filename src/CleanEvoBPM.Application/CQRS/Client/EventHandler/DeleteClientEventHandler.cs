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
    public class DeleteClientEventHandler : INotificationHandler<Notification<DeleteClientLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteClientEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteClientLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var respose = _elasticClient.DeleteAsync<DeleteClientLog>(notification.Event, idx => idx.Index("client"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
