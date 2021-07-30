using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;

namespace CleanEvoBPM.Application.CQRS.Technology.Query
{
    public class GetTechnologyDetailsQuery : IRequest<TechnologyResponseModel>
    {
        public Guid? Id { get; set; }
        public String Name { get; set; }
    }
}
