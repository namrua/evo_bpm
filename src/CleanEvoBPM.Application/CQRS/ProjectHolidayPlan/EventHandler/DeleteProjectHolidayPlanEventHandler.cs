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
    public class DeleteProjectHolidayPlanEventHandler : INotificationHandler<Notification<DeleteProjectHolidayPlanLog>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly string IndexName = "projectholidayplan";

        public DeleteProjectHolidayPlanEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Handle(Notification<DeleteProjectHolidayPlanLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                await _elasticClient.DeleteAsync<DeleteProjectHolidayPlanLog>(notification.Event, idx => idx.Index(IndexName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
