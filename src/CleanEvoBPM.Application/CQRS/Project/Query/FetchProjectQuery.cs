using System;
using System.Collections.Generic;
using CleanEvoBPM.Application.Models.Project;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class FetchProjectQuery : IRequest<IEnumerable<ProjectResponseModel>>
    {
        public string Search { get; set; }
    }
}
