using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Domain.Entities;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Technology.Command
{
    public class CreateTechnologyCommand : Entity, IRequest<GenericResponse>
    {
        public string TechnologyName { get; set; }
        public string TechnologyDescription { get; set; }
        public bool TechnologyActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
