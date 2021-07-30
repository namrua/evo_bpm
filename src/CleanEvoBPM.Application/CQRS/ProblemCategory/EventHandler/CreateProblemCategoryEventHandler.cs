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
    public class CreateProblemCategoryEventHandler : INotificationHandler<Notification<CreateProblemCategoryLog>>    
    {
        private readonly IElasticClient _elasticClient;

        public CreateProblemCategoryEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public Task Handle(Notification<CreateProblemCategoryLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists("problemcategory").Exists;
                if (!xval)
                {
                    _elasticClient.Indices.Create("problemcategory");
                }
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
