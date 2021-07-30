using CleanEvoBPM.Application.CQRS.Methodology.Event;
using CleanEvoBPM.Application.CQRS.ProjectType.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Methodology.EventHandler
{
    public class UpdateMethodologyEventHandler : INotificationHandler<Notification<UpdateMethodologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateMethodologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateMethodologyLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateMethodologyLog>(data, u => u.Doc(data).Index("methodology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
