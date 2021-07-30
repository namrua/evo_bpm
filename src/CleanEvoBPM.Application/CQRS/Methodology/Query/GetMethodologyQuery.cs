using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.Models.Methodology;
using MediatR;
namespace CleanEvoBPM.Application.CQRS.Methodology.Query
{
    public class GetMethodologyQuery : IRequest<IEnumerable<MethodologyResponseModel>>
    {
        #nullable enable
        public bool? RecordStatus { get; set; }
        public Guid? Id { get; set; }        
        public string? MethodologyName { get; set; }
        public string? Search { get; set; }
    }
}
