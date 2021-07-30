using CleanEvoBPM.Application.Models.ServiceType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ServiceType.Query
{
    public class GetServiceTypeQuery : IRequest<IEnumerable<ServiceTypeResponseModel>>
    {
        #nullable enable
        public bool? RecordStatus { get; set; }
        public Guid? Id { get; set; }
        public string? ServiceTypeName { get; set; }
        public string? Search { get; set; }
    }
}
