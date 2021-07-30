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
   public  class CreateBusinessDomainLogEventHandler : INotificationHandler<Notification<CreateBusinessDomainLog>>
    {
        private readonly IElasticClient _elasticClient;

        public CreateBusinessDomainLogEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateBusinessDomainLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("businessdomain").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("businessdomain");
                }
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("businessdomain"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
