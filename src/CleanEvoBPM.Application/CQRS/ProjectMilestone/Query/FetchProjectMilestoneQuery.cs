using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectMilestone.Query
{
    public class FetchProjectMilestoneQuery : IRequest<IEnumerable<ProjectMilestoneModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
