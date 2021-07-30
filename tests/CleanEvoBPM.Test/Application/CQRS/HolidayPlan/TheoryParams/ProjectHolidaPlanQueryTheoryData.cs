using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using System;
using System.Collections.Generic;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProjectHolidayPlan
{
    public partial class ProjectHolidayPlanTest
    {
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

        public static TheoryData<List<ProjectHolidayPlanResponseModel>> GetResultMocked = new TheoryData<List<ProjectHolidayPlanResponseModel>>
        {
            new List<ProjectHolidayPlanResponseModel>(),
            new List<ProjectHolidayPlanResponseModel>
            {
                new ProjectHolidayPlanResponseModel {
                    Id = Guid.NewGuid(),
                    ProjectId = Guid.NewGuid(),
                    ResourceId = Guid.NewGuid(),
                    ResourceRoleId = Guid.NewGuid(),
                    FromDate = DateTime.UtcNow,
                    ToDate = DateTime.UtcNow,
                    Note = "Some note",
                    CreatedBy = Guid.NewGuid(),
                    LastUpdatedBy = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
            }
        };
    }
}
