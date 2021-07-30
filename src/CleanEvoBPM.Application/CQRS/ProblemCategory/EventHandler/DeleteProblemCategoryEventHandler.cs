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
    public class DeleteProblemCategoryEventHandler : INotificationHandler<Notification<DeleteProblemCategoryLog>>
    {
        private readonly IElasticClient _elasticClient;

        public DeleteProblemCategoryEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<DeleteProblemCategoryLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var respose = _elasticClient.DeleteAsync<DeleteProblemCategoryLog>(notification.Event, idx => idx.Index("problemcategory"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Task.CompletedTask;
        }
    }
}
