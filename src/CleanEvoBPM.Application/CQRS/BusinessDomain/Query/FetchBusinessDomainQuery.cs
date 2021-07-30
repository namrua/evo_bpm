using System.Collections.Generic;
using CleanEvoBPM.Application.Models.BusinessDomain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Query
{
    public class FetchBusinessDomainQuery : IRequest<IEnumerable<BusinessDomainResponseModel>>
    {
#nullable enable
        public string? OrderBy { get; set; }
        public string? Search { get; set; }
        public bool? Status { get; set; }
    }
}