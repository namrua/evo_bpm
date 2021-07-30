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
    public class UpdateProjectEventHandler : INotificationHandler<Notification<UpdateProjectLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateProjectEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateProjectLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("project"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
