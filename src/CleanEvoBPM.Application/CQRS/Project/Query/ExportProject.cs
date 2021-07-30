using System.Collections.Generic;
using CleanEvoBPM.Application.Models.Project;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.Query
{
    public class ExportProject : IRequest<IEnumerable<ProjectExportModel>>
    {
        public string Search { get; set; }
    }
}