using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class HolidayPlanControllerTest : BaseControllerTest
    {
        private HolidayPlanController _holidayPlanController;

        public HolidayPlanControllerTest() : base(new HolidayPlanController())
        {
            _holidayPlanController = (HolidayPlanController)_apiController;
        }

        [Theory]
        [MemberData(nameof(GetProjectHolidayPlanData))]

        public async Task GetProjectHolidayPlan_Success_DataReturnsNotNullList(GetProjectHolidayPlanQuery query)
        {
            var returnedData = new List<ProjectHolidayPlanResponseModel>
            {
                new ProjectHolidayPlanResponseModel { Id = Guid.NewGuid() },
            };

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetProjectHolidayPlanQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(returnedData as IEnumerable<ProjectHolidayPlanResponseModel>));

            var result = await _holidayPlanController.Get(query);

            Assert.Equal(returnedData.Count, result.Count());
        }

        [Theory]
        [MemberData(nameof(CreateProjectHolidayPlanDataValid))]
        public async Task CreateProjectHolidayPlan_Success(CreateProjectHolidayPlanCommand command)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _holidayPlanController.HttpContext.User = user;

            var responseStubbed = GenericResponse.SuccessResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _holidayPlanController.Post(command);
            Assert.True(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(CreateProjectHolidayPlanDataValid))]
        public async Task CreateProjectHolidayPlan_Failure(CreateProjectHolidayPlanCommand command)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _holidayPlanController.HttpContext.User = user;

            var responseStubbed = GenericResponse.FailureResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _holidayPlanController.Post(command);

            Assert.False(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(UpdateProjectHolidayPlanDataValid))]
        public async Task UpdateProjectHolidayPlan_Success(UpdateProjectHolidayPlanCommand command)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _holidayPlanController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _holidayPlanController.Put(command.Id, command);

            Assert.True(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(UpdateProjectHolidayPlanDataValid))]
        public async Task UpdateProjectHolidayPlan_Failure(UpdateProjectHolidayPlanCommand command)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }, "mock"));

            _holidayPlanController.HttpContext.User = user;

            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GenericResponse.FailureResult()));

            var result = await _holidayPlanController.Put(command.Id, command);

            Assert.False(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(DeleteProjectHolidayPlanDataValid))]
        public async Task DeleteProjectHolidayPlan_Success(Guid id)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<DeleteProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _holidayPlanController.Delete(id);
            Assert.True(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(DeleteProjectHolidayPlanDataValid))]
        public async Task DeleteProjectHolidayPlan_Failure(Guid id)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<DeleteProjectHolidayPlanCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GenericResponse.FailureResult()));

            var result = await _holidayPlanController.Delete(id);
            Assert.False(result.Value.Success);
        }

        public static TheoryData<GetProjectHolidayPlanQuery> GetProjectHolidayPlanData
        {
            get
            {
                var data = new TheoryData<GetProjectHolidayPlanQuery>
                {
                    new GetProjectHolidayPlanQuery(),
                    new GetProjectHolidayPlanQuery
                    {
                        ProjectId = Guid.NewGuid()
                    }
                };

                return data;
            }
        }

        public static TheoryData<CreateProjectHolidayPlanCommand> CreateProjectHolidayPlanDataValid
        {
            get
            {
                var data = new TheoryData<CreateProjectHolidayPlanCommand>
                {
                    new CreateProjectHolidayPlanCommand
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = Guid.NewGuid(),
                        ResourceId = Guid.NewGuid(),
                        ResourceRoleId = Guid.NewGuid(),
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now,
                        Note = "Some note",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = Guid.NewGuid(),
                        LastUpdatedBy = Guid.NewGuid()
    },
                    new CreateProjectHolidayPlanCommand()
                };

                return data;
            }
        }

        public static TheoryData<GenericResponse> CreateProjectHolidayPlanResult
        {
            get
            {
                var data = new TheoryData<GenericResponse>
                {
                    new GenericResponse(),
                    new GenericResponse
                    {
                        Success = false
                    },
                    GenericResponse.FailureResult()
                };

                return data;
            }
        }

        public static TheoryData<UpdateProjectHolidayPlanCommand> UpdateProjectHolidayPlanDataValid
        {
            get
            {
                var data = new TheoryData<UpdateProjectHolidayPlanCommand>
                {
                    new UpdateProjectHolidayPlanCommand
                    {
                        Id = Guid.NewGuid(),
                        ResourceId = Guid.NewGuid(),
                        ResourceRoleId = Guid.NewGuid(),
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now,
                        Note = "Some note",
                        UpdatedDate = DateTime.Now,
                        LastUpdatedBy = Guid.NewGuid()
                    },
                    new UpdateProjectHolidayPlanCommand()
                };

                return data;
            }
        }

        public static TheoryData<Guid> DeleteProjectHolidayPlanDataValid
        {
            get
            {
                var data = new TheoryData<Guid>
                {
                    new Guid("EB69F33B-FF49-409B-A4F2-011AC0E809A8"),
                    new Guid("274451E4-6057-45CF-AC38-1AAE77AA0A66"),
                };

                return data;
            }
        }
    }
}
