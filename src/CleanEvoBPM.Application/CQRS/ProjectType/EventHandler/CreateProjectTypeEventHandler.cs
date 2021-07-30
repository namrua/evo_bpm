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
    public class CreateProjectTypeEventHandler : INotificationHandler<Notification<CreateProjectTypeLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateProjectTypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateProjectTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("projecttype").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("projecttype");
                }
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("projecttype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
