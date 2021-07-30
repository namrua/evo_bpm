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
    public class DeleteBusinessDomainLogEventHandler : INotificationHandler<Notification<DeleteBusinessDomainLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteBusinessDomainLogEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteBusinessDomainLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var respose = _elasticClient.DeleteAsync<DeleteBusinessDomainLog>(notification.Event, idx => idx.Index("businessdomain"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
