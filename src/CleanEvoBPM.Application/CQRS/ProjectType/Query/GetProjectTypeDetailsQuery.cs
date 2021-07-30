using CleanEvoBPM.Application.Models.ProjectType;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.ProjectType.Query
{
    public class GetProjectTypeDetailsQuery : IRequest<ProjectTypeResponseModel>
    {
        public Guid? Id { get; set; }
        public String Name { get; set; }
    }
}
