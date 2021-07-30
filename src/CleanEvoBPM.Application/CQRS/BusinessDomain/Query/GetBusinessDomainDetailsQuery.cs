using System;
using CleanEvoBPM.Application.Models.BusinessDomain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.Query
{
    public class GetBusinessDomainDetailsQuery : IRequest<BusinessDomainResponseModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}