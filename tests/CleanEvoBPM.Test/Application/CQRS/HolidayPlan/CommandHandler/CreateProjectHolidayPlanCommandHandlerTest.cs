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
        [MemberData(nameof(CreateProjectHolidayPlanDataValid))]
        public async Task CreateProjectHolidayPlan_Success_UniqueCheckReturnsNull(CreateProjectHolidayPlanCommand command)
        {
            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Get(It.IsAny<GetProjectHolidayPlanQuery>()))
                .Returns(Task.FromResult(null as IEnumerable<ProjectHolidayPlanResponseModel>));

            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Create(It.IsAny<CreateProjectHolidayPlanCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _projectHolidayPlanDataServiceMock.Verify(x => x.Create(It.IsAny<CreateProjectHolidayPlanCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateProjectHolidayPlanDataValid))]
        public async Task CreateProjectHolidayPlan_Success_UniqueCheckReturnsEmptyList(CreateProjectHolidayPlanCommand command)
        {
            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Get(It.IsAny<GetProjectHolidayPlanQuery>()))
                .Returns(Task.FromResult(new List<ProjectHolidayPlanResponseModel>() as IEnumerable<ProjectHolidayPlanResponseModel>));

            _projectHolidayPlanDataServiceMock
                .Setup(x => x.Create(It.IsAny<CreateProjectHolidayPlanCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createProjectHolidayPlanCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _projectHolidayPlanDataServiceMock.Verify(x => x.Create(It.IsAny<CreateProjectHolidayPlanCommand>()), Times.Once);
        }
    }
}
