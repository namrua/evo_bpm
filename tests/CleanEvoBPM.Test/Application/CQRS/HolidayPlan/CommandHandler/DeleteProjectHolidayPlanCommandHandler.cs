using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectHolidayPlan
{
    public partial class ProjectHolidayPlanTest
    {
        [Theory]
        [MemberData(nameof(DeleteProjectHolidayPlanDataValid))]
        public async Task DeleteProjectHolidayPlan_Success(DeleteProjectHolidayPlanCommand command)
        {
            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _deleteProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());
            Assert.True(result.Success);
        }

        [Theory]
        [MemberData(nameof(DeleteProjectHolidayPlanDataValid))]
        public async Task DeleteProjectHolidayPlan_Failure(DeleteProjectHolidayPlanCommand command)
        {
            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Delete(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GenericResponse.FailureResult()));

            var result = await _deleteProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());
            Assert.False(result.Success);
        }
    }
}
