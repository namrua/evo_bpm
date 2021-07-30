using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.CommandHandler;
using System;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectHolidayPlan
{
    public partial class ProjectHolidayPlanTest
    {
        public static TheoryData<CreateProjectHolidayPlanCommand> CreateProjectHolidayPlanDataValid
        {
            get
            {
                var data = new TheoryData<CreateProjectHolidayPlanCommand>
                {
                    new CreateProjectHolidayPlanCommand
                    {
                        ProjectId = Guid.NewGuid(),
                        ResourceId = Guid.NewGuid(),
                        ResourceRoleId = Guid.NewGuid(),
                        FromDate = DateTime.UtcNow,
                        ToDate = DateTime.UtcNow,
                        Note = "Some note"
                    }
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
                        FromDate = DateTime.UtcNow,
                        ToDate = DateTime.UtcNow,
                        Note = "Some note"
                    },
                    new UpdateProjectHolidayPlanCommand
                    {
                        ResourceId = Guid.NewGuid(),
                        ResourceRoleId = Guid.NewGuid(),
                        FromDate = DateTime.UtcNow,
                        ToDate = DateTime.UtcNow,
                        Note = "Some note"
                    }
                };

                return data;
            }
        }

        public static TheoryData<DeleteProjectHolidayPlanCommand> DeleteProjectHolidayPlanDataValid
        {
            get
            {
                var data = new TheoryData<DeleteProjectHolidayPlanCommand>
                {
                    new DeleteProjectHolidayPlanCommand { Id = Guid.NewGuid() },
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

        public static TheoryData<GenericResponse> UpdateProjectHolidayPlanResult
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

        public static TheoryData<GenericResponse> DeleteProjectHolidayPlanResult
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
    }
}
