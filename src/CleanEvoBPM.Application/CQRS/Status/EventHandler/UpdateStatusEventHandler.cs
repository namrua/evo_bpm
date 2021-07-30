using CleanEvoBPM.Application.CQRS.Status.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Status.EventHandler
{
    public class UpdateStatusEventHandler : INotificationHandler<Notification<UpdateStatusLog>>
    {
        private readonly IElasticClient _elasticClient;
        public UpdateStatusEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateStatusLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateStatusLog>(data, u => u.Doc(data).Index("status"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
