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
    public class UpdateProjectTypeEventHandler : INotificationHandler<Notification<UpdateProjectTypeLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateProjectTypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateProjectTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {               
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateProjectTypeLog>(data, u => u.Doc(data).Index("projecttype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
