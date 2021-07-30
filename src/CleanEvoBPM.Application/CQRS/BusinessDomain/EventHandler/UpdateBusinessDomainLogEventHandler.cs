using CleanEvoBPM.Application.CQRS.BusinessDomain.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.EventHandler
{
    public class UpdateBusinessDomainLogEventHandler : INotificationHandler<Notification<UpdateBusinessDomainLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateBusinessDomainLogEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateBusinessDomainLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateBusinessDomainLog>(data, u => u.Doc(data).Index("businessdomain"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
