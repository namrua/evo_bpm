using CleanEvoBPM.Application.CQRS.Technology.Event;
using MediatR;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.EventHandler
{
    public class DeleteTechnologyEventHandler : INotificationHandler<Notification<DeleteTechnologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteTechnologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteTechnologyLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                //_elasticClient.DeleteAsync<DeleteTechnologyLog>(notification.Event);
                var respose = _elasticClient.DeleteAsync<DeleteTechnologyLog>(notification.Event,idx=>idx.Index("technology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
