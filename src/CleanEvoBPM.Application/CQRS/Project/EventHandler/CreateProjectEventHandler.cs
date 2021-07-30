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
    public class CreateProjectEventHandler : INotificationHandler<Notification<CreateProjectLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateProjectEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateProjectLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("project").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("project");
                }
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
