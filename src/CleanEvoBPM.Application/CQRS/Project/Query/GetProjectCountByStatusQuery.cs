using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Project;
using MediatR;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class GetProjectCountByStatusQuery : IRequest<IEnumerable<ProjectCountByStatusModel>>
    {
    }
}
