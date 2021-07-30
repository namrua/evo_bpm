using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Event;
using MediatR;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.EventHandler
{
    public class UpdateProjectHolidayPlanEventHandler : INotificationHandler<Notification<UpdateProjectHolidayPlanLog>>
    {
        private readonly IElasticClient _elasticClient;
        private readonly string IndexName = "projectholidayplan";

        public UpdateProjectHolidayPlanEventHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Handle(Notification<UpdateProjectHolidayPlanLog> notification, CancellationToken cancellationToken)
        {
            try
            {
                var data = notification.Event;
                await _elasticClient.UpdateAsync<UpdateProjectHolidayPlanLog>(data, u => u.Doc(data).Index(IndexName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
