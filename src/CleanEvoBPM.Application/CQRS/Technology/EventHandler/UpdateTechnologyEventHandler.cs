using CleanEvoBPM.Application.CQRS.Technology.Event;
using MediatR;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Technology.EventHandler
{
    public class UpdateTechnologyEventHandler : INotificationHandler<Notification<UpdateTechnologyLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateTechnologyEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateTechnologyLog> notification, CancellationToken cancellationToken)
        {
            try
            {   
                var data = notification.Event;                
                var indexResponseAsync1 = _elasticClient.UpdateAsync<UpdateTechnologyLog>(data, u => u.Doc(data).Index("technology"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
