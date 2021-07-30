using CleanEvoBPM.Application.Models.ProjectLLBP;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectLLBP.Query
{
    public class ProjectLLBPQuery : IRequest<IEnumerable<ProjectLLBPResponseModel>>
    {
        public Guid ProjectId { get; set; }
    }
}
