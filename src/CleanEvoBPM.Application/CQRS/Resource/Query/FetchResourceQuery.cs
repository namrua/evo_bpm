using CleanEvoBPM.Application.Models.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Resource.Query
{
    public class FetchResourceQuery:IRequest<IEnumerable<ResourceResponseModel>>
    {
        public Guid id { get; set; }
    }
}
