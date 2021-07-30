using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class GetProjectsByManagerQuery : IRequest<IEnumerable<ProjectResponseModel>>
    {
        public GetProjectsByManagerQuery(Guid id)
        {
            ManagerId = id;
        }

        public Guid ManagerId { get; set; }
    }
}
