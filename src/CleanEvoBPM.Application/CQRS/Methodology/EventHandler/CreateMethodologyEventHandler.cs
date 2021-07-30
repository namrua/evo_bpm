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
    public class CreateMethodologyEventHandler : INotificationHandler<Notification<CreateMethodologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateMethodologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateMethodologyLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("methodology").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("methodology");
                }
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("methodology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
