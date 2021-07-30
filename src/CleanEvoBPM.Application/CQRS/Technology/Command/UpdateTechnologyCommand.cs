using CleanEvoBPM.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Technology.Command
{
    public class UpdateTechnologyCommand : IRequest<GenericResponse>
    {
        public Guid Id { get; set; }
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        public bool TechnologyActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public string UpdatedBy { get; set; }
    }
}
