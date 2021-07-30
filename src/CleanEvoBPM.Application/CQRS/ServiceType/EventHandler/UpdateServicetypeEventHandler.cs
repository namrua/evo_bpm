using CleanEvoBPM.Application.CQRS.ServiceType.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ServiceType.EventHandler
{
    public class UpdateServicetypeEventHandler : INotificationHandler<Notification<UpdateServiceTypeLog>>
    {
        private readonly IElasticClient _elasticClient;
        public UpdateServicetypeEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateServiceTypeLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateServiceTypeLog>(data, u => u.Doc(data).Index("servicetype"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
