using CleanEvoBPM.Application.CQRS.ProblemCategory.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.EventHandler
{
    public class UpdateProblemCategoryEventHandler : INotificationHandler<Notification<UpdateProblemCategoryLog>>
    {
        private readonly IElasticClient _elasticClient;

        public UpdateProblemCategoryEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<UpdateProblemCategoryLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                var indexResponseAsync1 = _elasticClient.IndexAsync(data, idx => idx.Index("problemcategory"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
