using CleanEvoBPM.Application.CQRS.Project.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Project.EventHandler
{
    public class DeleteProjectEventHandler : INotificationHandler<Notification<DeleteProjectLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteProjectEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteProjectLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                //_elasticClient.DeleteAsync<DeleteTechnologyLog>(notification.Event);
                var respose = _elasticClient.DeleteAsync<DeleteProjectLog>(notification.Event, idx => idx.Index("project"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
