using CleanEvoBPM.Application.CQRS.Methodology.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Methodology.EventHandler
{
    public class DeleteMethodologyEventHandler : INotificationHandler<Notification<DeleteMethodologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteMethodologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteMethodologyLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var respose = _elasticClient.DeleteAsync<DeleteMethodologyLog>(notification.Event, idx => idx.Index("methodology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
