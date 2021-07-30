using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Common.Exceptions;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectHolidayPlan
{
    public partial class ProjectHolidayPlanTest
    {
        [Theory]
        [MemberData(nameof(UpdateProjectHolidayPlanDataValid))]
        public async Task UpdateProjectHolidayPlan_Success_UniqueCheckReturnsEmptyList(UpdateProjectHolidayPlanCommand command)
        {
            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Get(It.IsAny<GetProjectHolidayPlanQuery>()))
                .Returns(Task.FromResult(new List<ProjectHolidayPlanResponseModel>() as IEnumerable<ProjectHolidayPlanResponseModel>));

            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Update(It.IsAny<UpdateProjectHolidayPlanCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _updateProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _projectHolidayPlanDataServiceMock.Verify(x => x.Update(It.IsAny<UpdateProjectHolidayPlanCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateProjectHolidayPlanDataValid))]
        public async Task UpdateProjectHolidayPlan_Success_UniqueCheckReturnsSameEntity(UpdateProjectHolidayPlanCommand command)
        {
            var fetchResultStub = new List<ProjectHolidayPlanResponseModel>
            {
                new ProjectHolidayPlanResponseModel { Id = command.Id }
            };

            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Get(It.IsAny<GetProjectHolidayPlanQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<ProjectHolidayPlanResponseModel>));

            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Update(It.IsAny<UpdateProjectHolidayPlanCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _updateProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _projectHolidayPlanDataServiceMock.Verify(x => x.Update(It.IsAny<UpdateProjectHolidayPlanCommand>()), Times.Once);
        }
    }
}
