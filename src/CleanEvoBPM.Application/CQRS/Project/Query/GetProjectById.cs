using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class GetProjectById : IRequest<GenericResponse<ProjectResponseByIdModel>>
    {
        public Guid Id { get; set; }
    }
}
