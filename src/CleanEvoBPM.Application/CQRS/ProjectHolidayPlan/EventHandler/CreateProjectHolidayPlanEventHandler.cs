using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.EventHandler
{
    public class CreateProjectHolidayPlanEventHandler : INotificationHandler<Notification<CreateProjectHolidayPlanLog>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly string IndexName = "projectholidayplan";

        public CreateProjectHolidayPlanEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Handle(Notification<CreateProjectHolidayPlanLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                bool xval = _elasticClient.Indices.Exists(IndexName).Exists;

                if (!xval)
                {
                    _elasticClient.Indices.Create(IndexName);
                }

                var data = notification.Event;
                await _elasticClient.IndexAsync(data, idx => idx.Index(IndexName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
