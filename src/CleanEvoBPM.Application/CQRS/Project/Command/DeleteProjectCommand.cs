using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace CleanEvoBPM.Application.CQRS.Project.Command
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public Guid ProjectID { get; set; }
        public string UpdatedBy { get; set; }
    }
}
