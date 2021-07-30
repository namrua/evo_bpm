using System;
using System.Collections.Generic;
using CleanEvoBPM.Application.Models.ProjectRisk;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.ProjectRisk.Query
{
    public class FetchProjectRiskQuery : IRequest<IEnumerable<ProjectRiskModel>>
    {
        public Guid ProjectId { get; set; }
    }
}