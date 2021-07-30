using CleanEvoBPM.Application.CQRS.ProjectType.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectType.EventHandler
{
    public class DeleteProjectTypeEventHandler : INotificationHandler<Notification<DeleteProjectTypeLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteProjectTypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteProjectTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {                
                var respose = _elasticClient.DeleteAsync<DeleteProjectTypeLog>(notification.Event, idx => idx.Index("projecttype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
