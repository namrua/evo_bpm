using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class GetMyProjectByIdQuery : IRequest<GenericResponse<ProjectResponseModel>>
    {
        public GetMyProjectByIdQuery(Guid managerId, Guid projectId)
        {
            ManagerId = managerId;
            ProjectId = projectId;
        }

        public Guid ManagerId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
