using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Resource.Command
{
    public class UpdateResourceCommand:IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public string Name { get; set; }
        public Guid RoleId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Percentage { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; } 
        public string CreatedBy { get; set; }
    }
}
